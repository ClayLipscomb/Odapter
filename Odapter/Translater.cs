//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2018 Clay Lipscomb
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Odapter {
    /// <summary>
    /// Handle translatiion from Oracle to C#
    /// </summary>
    public class Translater {
        public static Boolean UseGenericListForCursor = false;
        public static Boolean ConvertOracleNumberToIntegerIfColumnNameIsId = true;
        public static String ObjectTypeNamespace = "";
        //public static readonly string GENERIC_TYPE_PREFIX = "T_"; 
        public static readonly List<String> OracleTypesIgnored = new List<String> {
            // types explicitly not implemented in ODP.NET managed: ARRAY (Varray, Nested Table), BOOLEAN, OBJECT, REF, XML_TYPE
            // https://docs.oracle.com/database/121/ODPNT/OracleDbTypeEnumerationType.htm#ODPNT2286
            Orcl.VARRAY, Orcl.NESTED_TABLE, Orcl.PLSQL_BOOLEAN, Orcl.BOOLEAN, Orcl.OBJECT_TYPE, Orcl.REF, Orcl.XML_TYPE,

            // types not implemented by Odapter
            Orcl.LONG, Orcl.LONG_RAW, // deprecated by Oracle
            Orcl.BFILE, Orcl.RAW, Orcl.NUMERIC,
            Orcl.DECIMAL, Orcl.ROWID, Orcl.UROWID, 
            Orcl.TIMESTAMP_WITH_TIME_ZONE, Orcl.TIMESTAMP_WITH_LOCAL_TIME_ZONE,
            Orcl.INTERVAL_YEAR_TO_MONTH, Orcl.INTERVAL_DAY_TO_SECOND, Orcl.UNDEFINED, Orcl.RECORD
        };

        public static readonly List<String> TypesImplementedForAssociativeArrays = new List<String> {
            Orcl.BINARY_FLOAT,
            Orcl.CHAR,
            Orcl.DATE,
            Orcl.NCHAR,
            Orcl.NUMBER, /* equiv or subtypes --> */ Orcl.INTEGER, Orcl.INT, Orcl.SMALLINT, Orcl.FLOAT, Orcl.DOUBLE_PRECISION, Orcl.BINARY_DOUBLE,
            Orcl.BINARY_FLOAT, // not officially supported but it is working for me
            Orcl.NVARCHAR2,
            Orcl.RAW,
            Orcl.ROWID,
            Orcl.UROWID,
            Orcl.VARCHAR2, /* equiv or subtypes --> */ Orcl.VARCHAR, Orcl.STRING

            // associative array types explicitly not implemented in ODP.NET 
            // https://docs.oracle.com/cd/E85694_01/ODPNT/featOraCommand.htm#GUID-05A6D391-E77F-41AF-83A2-FE86A3D98872
        };

        public class CustomTranslatedCSharpType {
            public String CSharpType { get; private set; }
            private String TranslationNote { get; set; }
            public String DisplayDescription { get; private set; }
            public CustomTranslatedCSharpType(String cSharpType, String translationNote) {
                CSharpType = cSharpType;
                TranslationNote = translationNote;
                DisplayDescription = cSharpType + (String.IsNullOrWhiteSpace(translationNote) ? "" : " (" + translationNote + ")");
            }
        }

        public static readonly IDictionary<String, List<CustomTranslatedCSharpType>> CustomTypeTranslationOptions = new Dictionary<String, List<CustomTranslatedCSharpType>>() {
            {Orcl.INTEGER, new List<CustomTranslatedCSharpType> {                   new CustomTranslatedCSharpType(CSharp.INT32, @"9 digit limit, not recommended"),
                                                                                    new CustomTranslatedCSharpType(CSharp.INT64, @"18 digit limit, usually safe"),
                                                                                    new CustomTranslatedCSharpType(CSharp.DECIMAL, @"28 digit limit"),
                                                                                    new CustomTranslatedCSharpType(CSharp.ORACLE_DECIMAL, @"ODP.NET safe type struct") } },
            {Orcl.NUMBER, new List<CustomTranslatedCSharpType> {                    new CustomTranslatedCSharpType(CSharp.DECIMAL, @"28 dig limit, auto rounding"),
                                                                                    new CustomTranslatedCSharpType(CSharp.ORACLE_DECIMAL, @"ODP.NET safe type struct") } },
            {Orcl.DATE, new List<CustomTranslatedCSharpType> {                      new CustomTranslatedCSharpType(CSharp.DATE_TIME, @"no BC"),
                                                                                    new CustomTranslatedCSharpType(CSharp.ORACLE_DATE, @"ODP.NET safe type struct") } },
            {Orcl.TIMESTAMP, new List<CustomTranslatedCSharpType> {                 new CustomTranslatedCSharpType(CSharp.DATE_TIME, @"e-7 max, no BC, no time zone"),
                                                                                    new CustomTranslatedCSharpType(CSharp.ORACLE_TIMESTAMP, @"ODP.NET safe type struct") } },
            {Orcl.INTERVAL_DAY_TO_SECOND, new List<CustomTranslatedCSharpType> {    new CustomTranslatedCSharpType(CSharp.TIME_SPAN, @"e-7 max"),
                                                                                    new CustomTranslatedCSharpType(CSharp.ORACLE_INTERVAL_DS, @"ODP.NET safe type struct") } },
            {Orcl.BLOB, new List<CustomTranslatedCSharpType> {                      new CustomTranslatedCSharpType(CSharp.BYTE_ARRAY, @""),
                                                                                    new CustomTranslatedCSharpType(CSharp.ORACLE_BLOB, @"ODP.NET safe type class") } },
            {Orcl.CLOB, new List<CustomTranslatedCSharpType> {                      new CustomTranslatedCSharpType(CSharp.STRING, @""),
                                                                                    new CustomTranslatedCSharpType(CSharp.ORACLE_CLOB, @"ODP.NET safe type class") } }
        };

        #region Properties
        private static string _cSharpTypeUsedForOracleInteger = CSharp.DECIMAL;
        public static string CSharpTypeUsedForOracleInteger {
            set {
                if (value != CSharp.INT32 && value != CSharp.INT64 && value != CSharp.DECIMAL
                        && value != CSharp.ORACLE_DECIMAL && value != CSharp.BIG_INTEGER)
                    throw new Exception("C# type " + value + " not allowed as translation for Oracle INTEGER.");
                _cSharpTypeUsedForOracleInteger = value;
            }
            get {
                return _cSharpTypeUsedForOracleInteger;
            }
        }

        private static string _cSharpTypeUsedForOracleNumber;
        public static string CSharpTypeUsedForOracleNumber {
            set {
                if (value != CSharp.DECIMAL && value != CSharp.ORACLE_DECIMAL && value != CSharp.STRING)
                    throw new Exception("C# type " + value + " not allowed as translation for Oracle NUMBER.");
                _cSharpTypeUsedForOracleNumber = value;
            }
            get {
                return _cSharpTypeUsedForOracleNumber;
            }
        }

        private static string _cSharpTypeUsedForOracleDate;
        public static string CSharpTypeUsedForOracleDate {
            set {
                if (value != CSharp.DATE_TIME && value != CSharp.ORACLE_DATE)
                    throw new Exception("C# type " + value + " not allowed as translation for Oracle DATE.");
                _cSharpTypeUsedForOracleDate = value;
            }
            get {
                return _cSharpTypeUsedForOracleDate;
            }
        }

        private static string _cSharpTypeUsedForOracleTimeStamp;
        public static string CSharpTypeUsedForOracleTimeStamp {
            set {
                if (value != CSharp.DATE_TIME && value != CSharp.ORACLE_TIMESTAMP)
                    throw new Exception("C# type " + value + " not allowed as translation for Oracle TIMESTAMP.");
                _cSharpTypeUsedForOracleTimeStamp = value;
            }
            get {
                return _cSharpTypeUsedForOracleTimeStamp;
            }
        }

        private static string _cSharpTypeUsedForOracleIntervalDayToSecond;
        public static string CSharpTypeUsedForOracleIntervalDayToSecond {
            set {
                if (value != CSharp.TIME_SPAN && value != CSharp.ORACLE_INTERVAL_DS)
                    throw new Exception("C# type " + value + " not allowed as translation for Oracle INTERVAL DAY TO SECOND.");
                _cSharpTypeUsedForOracleIntervalDayToSecond = value;
            }
            get {
                return _cSharpTypeUsedForOracleIntervalDayToSecond;
            }
        }

        private static string _cSharpTypeUsedForOracleBlob;
        public static string CSharpTypeUsedForOracleBlob {
            set {
                if (value != CSharp.BYTE_ARRAY && value != CSharp.ORACLE_BLOB)
                    throw new Exception("C# type " + value + " not allowed as translation for Oracle BLOB.");
                _cSharpTypeUsedForOracleBlob = value;
            }
            get {
                return _cSharpTypeUsedForOracleBlob;
            }
        }

        private static string _cSharpTypeUsedForOracleClob;
        public static string CSharpTypeUsedForOracleClob {
            set {
                if (value != CSharp.STRING && value != CSharp.ORACLE_CLOB)
                    throw new Exception("C# type " + value + " not allowed as translation for Oracle CLOB and NCLOB.");
                _cSharpTypeUsedForOracleClob = value;
            }
            get {
                return _cSharpTypeUsedForOracleClob;
            }
        }
        #endregion

        #region General Constants
        private const char UNDERSCORE = '_';
        private const string CHARACTER_ABBREV = "char";
        #endregion

        #region Translation methods
        public static bool IsOracleTypeIgnored(String oracleType, out String reasonMsg, String reasonMsgAppend = "") {
            reasonMsg = "";
            if (String.IsNullOrWhiteSpace(oracleType) || !OracleTypesIgnored.Contains(oracleType)) return false;

            String oracleTypeFormatted = oracleType.Replace('_', ' ').Replace("PLSQL", "PL/SQL").Replace(Orcl.OBJECT_TYPE, "OBJECT TYPE") + (String.IsNullOrWhiteSpace(reasonMsgAppend) ? "" : " " + reasonMsgAppend);
            if (oracleType.Equals(Orcl.PLSQL_BOOLEAN) || oracleType.Equals(Orcl.NESTED_TABLE))
                reasonMsg = ".NET cannot send/receive a " + oracleTypeFormatted;
            else if (oracleType.Equals(Orcl.RECORD))
                reasonMsg = ".NET cannot send/receive a " + oracleTypeFormatted + " (apart from cursor) " ;
            else if (oracleType.Equals(Orcl.UNDEFINED))
                reasonMsg = "At least one Oracle type is undefined";
            else if (oracleType.Equals(Orcl.LONG_RAW) || oracleType.Equals(Orcl.RAW))
                reasonMsg = "Code generation for " + oracleTypeFormatted + " will not be implemented due to Oracle deprecation ";
            else
                reasonMsg = "Code generation for " + oracleTypeFormatted + " types has not been implemented ";

            return true;
        }

        /// <summary>
        /// Convert an Oracle entity/object name (table, package, argument, column etc.) to a valid C# equivalent 
        /// </summary>
        /// <param name="oracleArgName"></param>
        /// <param name="useCamelCase">convert to camelCase, otherwise defaults to PascalCase</param>
        /// <returns></returns>
        public static string ConvertOracleNameToCSharpName(string oracleName, bool useCamelCase) {
            if (String.IsNullOrEmpty(oracleName)) return null; // this occurs with a return arg

            String oracleNameAdjusted = oracleName;

            // replace special characters with alphanumerics equivalent
            oracleNameAdjusted = oracleNameAdjusted.Replace("!", "exclamationpoint" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace("@", "atsign" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace("#", "poundsign" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace("$", "dollarsign" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace("%", "percentsign" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace("^", "caret" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace("&", "ampersand" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace("*", "asterisk" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(".", "period" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace("?", "questionmark" + CHARACTER_ABBREV);

            String cSharpName = (useCamelCase
                ? CaseConverter.ConvertUnderscoreDelimitedToCamelCase(oracleNameAdjusted)
                : CaseConverter.ConvertUnderscoreDelimitedToPascalCase(oracleNameAdjusted));
            if (Char.IsDigit(cSharpName, 0)) cSharpName =  (useCamelCase ? "t" : "T") + "he" + cSharpName; // a C# arg cannot start with number
            if (CSharp.IsKeyword(cSharpName)) cSharpName = cSharpName + "Cs"; // append text to avoid the C# keyword
            return cSharpName;
        }
        
        /// <summary>
        /// Create C# method name for a procedure
        /// </summary>
        /// <param name="proc"></param>
        /// <returns></returns>
        public static String ConvertOracleProcNameToMethodName(Procedure proc) {
            String methodName = ConvertOracleNameToCSharpName(proc.ProcedureName, false);

            // prevent identical class name and method name - yes, I've seen this happen in Oracle
            if (proc.PackageName != null && proc.PackageName == proc.ProcedureName) methodName += "Proc";
            return methodName;
        }

        /// <summary>
        /// Convert a record in an Oracle argument into a C# class name
        /// </summary>
        /// <param name="oracleArg"></param>
        /// <returns></returns>
        public static string ConvertOracleRecordNameToCSharpName(Argument oracleArg) {
            // Type and subtype can be null (e.g., a bug in the view bug when a record type based on a table). In this case, 
            //      use proc name (which is what subtype usually is anyway) and some extra special text. We need a 
            //      better algorithm to guarantee uniqueness in the C# namespace.
            if (String.IsNullOrEmpty(oracleArg.TypeSubname)) 
                return ConvertOracleNameToCSharpName(oracleArg.ProcedureName + UNDERSCORE + (oracleArg.ArgumentName ?? "RETURN") + UNDERSCORE + "ROW_TYPE", false);
            // if the argument's record is defined in another package and there is a filter, the C# name must be prefixed with the source package name to prevent naming conflict
            else if (!String.IsNullOrWhiteSpace(Parameter.Instance.Filter) && !oracleArg.PackageName.Equals(oracleArg.TypeName)) 
                return ConvertOracleNameToCSharpName(oracleArg.TypeName, false) + ConvertOracleNameToCSharpName(oracleArg.TypeSubname, false);
            // normal record type
            else 
                return ConvertOracleNameToCSharpName(oracleArg.TypeSubname, false);
        }

        /// <summary>
        /// Convert an Oracle record field name to a C# property name
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="recordName"></param>
        /// <param name="usePascalCase"></param>
        /// <returns></returns>
        public static String ConvertOracleRecordFieldNameToCSharpPropertyName(String fieldName, String recordName, bool usePascalCase) {
            String propertyName = ConvertOracleNameToCSharpName(fieldName, usePascalCase);

            // prevent identical class name and property name which is not allowed by C#
            if (usePascalCase && recordName == fieldName) propertyName += "Field";
            return propertyName;
        }

        /// <summary>
        /// Convert Oracle type to the equivalent C# ODP.NET type
        /// </summary>
        /// <param name="oracleType"></param>
        /// <returns></returns>
        public static string ConvertOracleTypeToOdpNetType(string oracleType) {
            if (String.IsNullOrEmpty(oracleType)) return oracleType;

            switch (oracleType) {
                case Orcl.BFILE:
                    return CSharp.ORACLE_BFILE;
                case Orcl.NUMBER:
                case Orcl.BINARY_INTEGER:
                case Orcl.PLS_INTEGER:
                case Orcl.FLOAT:
                    return CSharp.ORACLE_DECIMAL;
                case Orcl.BINARY_FLOAT:
                case Orcl.BINARY_DOUBLE:
                    return CSharp.ORACLE_DECIMAL;
                case Orcl.BLOB:
                    return CSharp.ORACLE_BLOB;
                case Orcl.CLOB:
                case Orcl.NCLOB:
                    return CSharp.ORACLE_CLOB;
                case Orcl.CHAR:
                case Orcl.LONG:
                case Orcl.NCHAR:
                case Orcl.NVARCHAR2:
                case Orcl.ROWID:
                case Orcl.UROWID:
                case Orcl.VARCHAR2:
                    return CSharp.ORACLE_STRING;
                case Orcl.DATE:
                    return CSharp.ORACLE_DATE;
                case Orcl.INTERVAL_DAY_TO_SECOND:
                    return CSharp.ORACLE_INTERVAL_DS;
                case Orcl.INTERVAL_YEAR_TO_MONTH:
                    return CSharp.ORACLE_INTERVAL_YM;
                case Orcl.LONG_RAW:
                case Orcl.RAW:
                    return CSharp.ORACLE_BINARY;
                case Orcl.REF:
                    return CSharp.ORACLE_REF;
                case Orcl.REF_CURSOR:
                    return CSharp.ORACLE_REF_CURSOR;
                case Orcl.TIMESTAMP:
                    return CSharp.ORACLE_TIMESTAMP;
                case Orcl.TIMESTAMP_WITH_LOCAL_TIME_ZONE:
                    return CSharp.ORACLE_TIMESTAMP_LTZ;
                case Orcl.TIMESTAMP_WITH_TIME_ZONE:
                    return CSharp.ORACLE_TIMESTAMP_TZ;
                case Orcl.XML_TYPE:
                    return CSharp.ORACLE_XML_TYPE;
                default:
                    return "Undetermined_OdpNet_Type";
            }
        }

        /// <summary>
        /// Convert an Oracle argument type to a client side OracleDbType
        /// </summary>
        /// <param name="oracleArg"></param>
        /// <param name="nextArg"></param>
        /// <returns></returns>
        public static string ConvertOracleArgTypeToCSharpOracleDbType(Argument oracleArg, Argument nextArgUnused) {
            if (oracleArg.DataType == null) return null;

            // the DbType needed for an assoc array to work is simply the type of its subsequent nested arg
            if (oracleArg.DataType == Orcl.ASSOCIATITVE_ARRAY) return ConvertOracleArgTypeToCSharpOracleDbType(oracleArg.NextArgument, null);

            // first handle the clear translations
            switch (oracleArg.DataType) {
                case Orcl.STRING:
                case Orcl.VARCHAR:
                case Orcl.VARCHAR2:
                case Orcl.ROWID:
                case Orcl.UROWID:
                    return CSharp.ORACLEDBTYPE_VARCHAR2;
                case Orcl.CHAR:
                    return CSharp.ORACLEDBTYPE_CHAR;
                case Orcl.NVARCHAR2:
                    return CSharp.ORACLEDBTYPE_NVARCHAR2;
                case Orcl.NCHAR:
                    return CSharp.ORACLEDBTYPE_NCHAR;
                case Orcl.XML_TYPE:
                    return CSharp.ORACLEDBTYPE_XMLTYPE;
                case Orcl.LONG:
                    return CSharp.ORACLEDBTYPE_LONG;
//                case Orcl.REF:
//                    return CSharp.ORACLEDBTYPE_REF;
                case Orcl.BFILE:
                    return CSharp.ORACLEDBTYPE_BFILE;
                case Orcl.BLOB:
                    return CSharp.ORACLEDBTYPE_BLOB;
                case Orcl.CLOB:
                    return CSharp.ORACLEDBTYPE_CLOB;
                case Orcl.NCLOB:
                    return CSharp.ORACLEDBTYPE_NCLOB;
                case Orcl.RAW:
                    return CSharp.ORACLEDBTYPE_RAW;
                case Orcl.BINARY_DOUBLE:
                    return CSharp.ORACLEDBTYPE_BINARY_DOUBLE;
                case Orcl.BINARY_FLOAT:
                    return CSharp.ORACLEDBTYPE_BINARY_FLOAT;
                case Orcl.RECORD:
                    return CSharp.ORACLEDBTYPE_OBJECT;
                case Orcl.REF_CURSOR:
                    return CSharp.ORACLEDBTYPE_CURSOR;
                case Orcl.LONG_RAW:
                    return CSharp.ORACLEDBTYPE_LONGRAW;
                case Orcl.TIMESTAMP:
                    return CSharp.ORACLEDBTYPE_TIMESTAMP;
                case Orcl.TIMESTAMP_WITH_TIME_ZONE:
                    return CSharp.ORACLEDBTYPE_TIMESTAMP_TZ;
                case Orcl.TIMESTAMP_WITH_LOCAL_TIME_ZONE:
                    return CSharp.ORACLEDBTYPE_TIMESTAMP_LTZ;
                case Orcl.INTERVAL_DAY_TO_SECOND:
                    return CSharp.ORACLEDBTYPE_INTERVAL_DAY_TO_SECOND;
                case Orcl.INTERVAL_YEAR_TO_MONTH:
                    return CSharp.ORACLEDBTYPE_INTERVAL_YEAR_TO_MONTH;
                case Orcl.DATE:
                    return CSharp.ORACLEDBTYPE_DATE;
                case Orcl.UNDEFINED: // ??
                    return CSharp.ORACLEDBTYPE_OBJECT;
                case Orcl.NESTED_TABLE:
                    return "NestedTable_OracleDbType_Is_Undetermiend";
                case Orcl.OBJECT_TYPE:
                    return "Object_OracleDbType_Is_Undetermiend";
                case Orcl.VARRAY:
                    return "VArray_OracleDbType_Is_Undetermiend";
            }

            // The remaining cases should be a NUMBER or equivalent. We rely on first converting to C# type to determine this.
            String cSharpType = ConvertOracleArgTypeToCSharpType(oracleArg, true);

            if (cSharpType == null) return "Undetermined_OracleDbType";

            // now convert from C# type to the client DBType
            // use "Contains()" in case it is a List type or a nullable "?" is appended to type
            // NOTE: we need to handle more cases here
            if (cSharpType.Contains(CSharp.SBYTE))
                return CSharp.ORACLEDBTYPE_BYTE;
            else if (cSharpType.Contains(CSharp.INT16))
                return CSharp.ORACLEDBTYPE_INT16;
            else if (cSharpType.Contains(CSharp.INT32))
                return CSharp.ORACLEDBTYPE_INT32;
            else if (cSharpType.Contains(CSharp.INT64))
                return CSharp.ORACLEDBTYPE_INT64;
            else if (cSharpType.Contains(CSharp.DECIMAL)
                || cSharpType.Contains(CSharp.DOUBLE)
                || cSharpType.Contains(CSharp.FLOAT)
                || cSharpType.Contains(CSharp.BIG_INTEGER))
                return CSharp.ORACLEDBTYPE_DECIMAL;

            return "Undetermined_OracleDbType_For_CSharp_" + cSharpType;
        }

        /// <summary>
        /// Build an oracle type with any precision, length, etc. qualifiers included.
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        public static string BuildAggregateOracleType(IEntityAttribute attr) {
            if (String.IsNullOrEmpty(attr.AttrType)) return attr.AttrType;

            string oracleType = attr.AttrType.Trim();

            // handle Oracle aliaes
            if (oracleType.Equals(Orcl.DECIMAL) || oracleType.Equals(Orcl.NUMERIC)) oracleType = Orcl.NUMBER;

            if (oracleType == Orcl.NUMBER) { // add precisions and scale, if any, to NUMBER to create complete data type
                if (attr.Precision != null || attr.Scale == 0) oracleType += "(" + (attr.Precision ?? 38).ToString();
                if (attr.Precision != null || attr.Scale == 0) oracleType += "," + (attr.Scale ?? 0).ToString();
                if (attr.Precision != null || attr.Scale == 0) oracleType += ")";
            } else if (oracleType.Contains(Orcl.VARCHAR)) {
                if (attr.Length >= 1) oracleType = oracleType + "(" + attr.Length + ")";
            }

            return oracleType;
        }

        /// <summary>
        /// Convert the type of an Oracle argument to its equivalent C# argument type
        /// </summary>
        /// <param name="oracleArg">Oracle argument to be converted</param>
        /// <param name="nextArg">The Oracle argument succeeding the primary argument in prodedure argument list.</param>
        /// <param name="typeNotNullable">Determines whether C# type can not be nullable, defaults to false. </param>
        /// <returns></returns>
        public static string ConvertOracleArgTypeToCSharpType(Argument oracleArg, bool typeNotNullable) {
            if (oracleArg == null) return null;

            // a PL/SQL record will have a custom class built for it; here we only need to return that class name as the type
            if (oracleArg.DataType == Orcl.RECORD) return ConvertOracleRecordNameToCSharpName(oracleArg);

            // A PL/SQL associative array is technically the equivalent of C# Dictionary. Although the Dicionary behavior can
            // be used within PL/SQL between functions, a Dictionary object cannot be pased from .NET. We can only pass the values
            // of using an array index instead of key. So we treat an associative array as a List of a type or class in C#. The type is in 
            // the subsequent Oracle arg.
            if (oracleArg.DataType == Orcl.ASSOCIATITVE_ARRAY) {
                string arrayType = (oracleArg.NextArgument.DataType == Orcl.RECORD 
                    ? ConvertOracleRecordNameToCSharpName(oracleArg)
                    : ConvertOracleArgTypeToCSharpType(oracleArg.NextArgument, false/*, typeNotNullable: true*/));
                return CSharp.ListOf(arrayType);
            }

            // a nested table to a List (even though we are not handling nested tables yet)
            if (oracleArg.DataType == Orcl.NESTED_TABLE) return CSharp.ListOf(ConvertOracleArgTypeToCSharpType(oracleArg.NextArgument, true));

            // a cursor translates to a List
            // a strongly typed cursor is a generic list, but based on record type
            // a weakly typed cursor is either a generic list or Datatable
            if (oracleArg.DataType == Orcl.REF_CURSOR) {
                return (oracleArg.NextArgument == null || oracleArg.NextArgument.DataLevel == oracleArg.DataLevel // is it weakly typed cursor?
                    ? (UseGenericListForCursor
                        // generic list; create informative subtype name that is unique among multilple untyped (cursor) args in proc
                        ? CSharp.ListOf(CSharp.GENERIC_TYPE_PREFIX
                            + (ConvertOracleNameToCSharpName(oracleArg.ArgumentName, true) ?? "return") + "Untyped")
                        // otherwise, we are configured to use a Datatable
                        : CSharp.DATATABLE) 
                    : (UseGenericListForCursor
                        // generic list of a type based on, or extended from, the cursor's record type; include record type in name
                        ? CSharp.ListOf(CSharp.GENERIC_TYPE_PREFIX + ConvertOracleArgTypeToCSharpType(oracleArg.NextArgument, /*null,*/ true))
                        // a databale for strongly typed cursor is not practical, but technically it could be used in the future
                        : CSharp.DATATABLE)  
                    );
            }

            // XMLTYPE - the arg's DataType will be "UNDEFINED" and PlsType null
            if (!String.IsNullOrWhiteSpace(oracleArg.TypeName) && oracleArg.TypeName.Equals(Orcl.XML_TYPE)) return CSharp.XML_DOCUMENT;

            // Known examples:  data_type       pls_type
            //                  BINARY_INTEGER  PLS_INTEGER
            //                  FLOAT           DOUBLE PRECISION
            //                  FLOAT           REAL
            //                  NCHAR           CHAR
            //                  NCLOB           CLOB
            //                  NUMBER	        INTEGER
            //                  NUMBER	        SMALLINT
            //                  NVARCHAR2       VARCHAR2
            //                  PL/SQL BOOLEAN	BOOLEAN
            //                  VARCHAR2        STRING

            //                  OBJECT	        null
            //                  PL/SQL RECORD	null
            //                  PL/SQL TABLE	null
            //                  REF             null
            //                  REF CURSOR      null
            //                  TABLE           null
            //                  UNDEFINED       null
            //                  VARRAY          null

            // handle simple types
            string oracleType;
            if (oracleArg.DataType != null) {
                oracleType = oracleArg.DataType.Trim();
                if (oracleArg.DataType == Orcl.NUMBER && !(oracleArg.PlsType == Orcl.DECIMAL && oracleArg.DataScale == null)) { // add precisions and scale, if any, to NUMBER to create explicit Oracle type
                    if (oracleArg.DataPrecision != null) oracleType += "(" + oracleArg.DataPrecision.ToString();
                    if (oracleArg.DataScale != null) oracleType += "," + oracleArg.DataScale.ToString();
                    if (oracleArg.DataPrecision != null) oracleType += ")";
                }
                // we are not adding data length since any VARCHAR2 will always be C# string
            } else if (oracleArg.PlsType != null) {
                oracleType = oracleArg.PlsType.Trim(); // never seen this case even on a large number of schemas including Oracle E-Business apps
            } else {
                oracleType = ""; // this happens when there is a single "arg" row for a proc with no args
            }

            return ConvertOracleTypeToCSharpType(oracleType, oracleArg.ArgumentName, typeNotNullable, oracleArg.TypeName);
        }

        /// <summary>
        /// Returns the C# type for a given Oracle type
        /// </summary>
        /// <param name="oracleType">the Oracle type</param>
        /// <param name="oracleName">the Oracle name for the type being converted to C#</param>
        /// <param name="typeNotNullable">make the C# type not nullable</param>
        /// <param name="oracleTypeName">required for Oracle "object" type</param>
        /// <returns></returns>
        public static string ConvertOracleTypeToCSharpType(String oracleType, String oracleName, bool typeNotNullable, String oracleTypeName) {
            
            // create all type names dependent on typeNotNullable argument
            string cSharpTypeUsedForOracleInteger = _cSharpTypeUsedForOracleInteger + (typeNotNullable ? "" : "?");
            string sByteType = CSharp.SBYTE + (typeNotNullable ? "" : "?");
            string int16Type = CSharp.INT16 + (typeNotNullable ? "" : "?");
            string int32Type = CSharp.INT32 + (typeNotNullable ? "" : "?");
            string int64Type = CSharp.INT64 + (typeNotNullable ? "" : "?");
            string singleType = CSharp.SINGLE + (typeNotNullable ? "" : "?");
            string doubleType = CSharp.DOUBLE + (typeNotNullable ? "" : "?");
            string cSharpTypeUsedForOracleDate  = Translater.CSharpTypeUsedForOracleDate + (typeNotNullable ? "" : "?");
            string cSharpTypeUsedForOracleTimeStamp = Translater.CSharpTypeUsedForOracleTimeStamp + (typeNotNullable ? "" : "?");
            string cSharpTypeUsedForOracleIntervalDayToSecond = Translater.CSharpTypeUsedForOracleIntervalDayToSecond + (typeNotNullable ? "" : "?");
            string cSharpTypeUsedForOracleBlob = Translater.CSharpTypeUsedForOracleBlob; // no types require ?
            string cSharpTypeUsedForOracleClob = Translater.CSharpTypeUsedForOracleClob; // no types require ?
            //string timeSpanType = CSharp.TIME_SPAN + (typeNotNullable ? "" : "?");
            string cSharpTypeUsedForOracleNumber = _cSharpTypeUsedForOracleNumber + (typeNotNullable ? "" : "?");

            // ******************************************************
            // Judgment call interpretations for NUMBER to INTEGER 
            //  1. name a) is "id" or b) ends in "_id" 
            if ((oracleType == Orcl.NUMBER)
                    && ConvertOracleNumberToIntegerIfColumnNameIsId
                    && !String.IsNullOrEmpty(oracleName)
                    && (oracleName.ToLower().EndsWith("_id") || oracleName.ToLower().Equals("id"))
                ) 
                return cSharpTypeUsedForOracleInteger;
            // ***********************************

            // a NUMBER type with at least a precision (i.e., it has an opening parenthesis)
            if (oracleType.StartsWith(Orcl.NUMBER + "(")) {
                // with a zero or explicitly no scale, it is a mathematical integer (e.g., NUMBER(10,0), NUMBER(10))
                if (oracleType.IndexOf(",") == -1 || oracleType.IndexOf(",0)") >= 0) {

                    sbyte precision = Convert.ToSByte(oracleType.Substring(oracleType.IndexOf("(") + 1, 
                        oracleType.Replace(",0", "").IndexOf(")") - oracleType.IndexOf("(") - 1));

                    // NUMBER(1) to NUMBER(2) is always sbyte
                    if (1 <= precision && precision <= 2)
                        return sByteType;
                    // NUMBER(3) to NUMBER(4) is always short
                    else if (3 <= precision && precision <= 4)
                        return int16Type;
                    // NUMBER(5) to NUMBER(9) is always int
                    else if (5 <= precision && precision <= 9)
                        return int32Type;
                    // NUMBER(10) to NUMBER(18) should be long, unless int chosen by user
                    //else if (10 <= precision && precision <= 18)
                    //    return (_cSharpTypeUsedForOracleInteger == CSharp.INT32 ? int32Type : int64Type); // !!!!!!!!!
                    // NUMBER(19) to NUMBER(28) should be the type mapped to NUMBER, unless int or long chosen by user
                    //else if (19 <= precision && precision <= 28)
                    //    return (_cSharpTypeUsedForOracleInteger == CSharp.INT32 // !!!!!!!!!
                    //            ? int32Type
                    //            : ( _cSharpTypeUsedForOracleInteger == CSharp.INT64  // !!!!!!!!!!!
                    //                ? int64Type
                    //                : cSharpTypeUsedForOracleInteger)); // !!!!!!!!!
                    // NUMBER(29) and above should be type chosen by user
                    else
                        return cSharpTypeUsedForOracleInteger;

                // otherwise it is a decimal type
                } else {
                    return cSharpTypeUsedForOracleNumber;
                }
            }

            // a VARCHAR, VARCHAR2, NVARCHAR2 type with a length
            if (oracleType.Contains(Orcl.VARCHAR) && oracleType.Contains("(")) return CSharp.STRING;

            if (oracleType.StartsWith(Orcl.TIMESTAMP)) return cSharpTypeUsedForOracleTimeStamp;

            // unqualified types
            switch (oracleType) {
                case Orcl.BINARY_INTEGER:
                case Orcl.PLS_INTEGER:
                    return int32Type;

                case Orcl.INTEGER: // Oracle alias as NUMBER(38)
                case Orcl.INT:
                case Orcl.UNSIGNED_INTEGER:
                case Orcl.SMALLINT:
                    return cSharpTypeUsedForOracleInteger;

                case Orcl.PLSQL_BOOLEAN:
                case Orcl.BOOLEAN:
                    return CSharp.BOOLEAN;

                case Orcl.STRING:
                case Orcl.VARCHAR:
                case Orcl.VARCHAR2:
                case Orcl.NVARCHAR2:
                case Orcl.CHAR:
                case Orcl.NCHAR:
                case Orcl.ROWID:
                case Orcl.UROWID:
                case Orcl.REF:  // a pointer
                    return CSharp.STRING;

                case Orcl.XML_TYPE:
                    return CSharp.XML_DOCUMENT;

                case Orcl.NUMBER:
                case Orcl.NUMERIC:
                case Orcl.FLOAT:    // Oracle alias for NUMBER
                case Orcl.DOUBLE_PRECISION:
                case Orcl.DECIMAL:
                    return cSharpTypeUsedForOracleNumber;

                case Orcl.BINARY_FLOAT:
                    return singleType; 
                case Orcl.BINARY_DOUBLE:
                    return doubleType; 

                case Orcl.DATE:
                    return cSharpTypeUsedForOracleDate;
                case Orcl.TIMESTAMP:
                case Orcl.TIMESTAMP_WITH_LOCAL_TIME_ZONE:
                case Orcl.TIMESTAMP_WITH_TIME_ZONE:
                    return cSharpTypeUsedForOracleTimeStamp;
                case Orcl.INTERVAL_DAY_TO_SECOND:
                case Orcl.INTERVAL_YEAR_TO_MONTH:
                    return cSharpTypeUsedForOracleIntervalDayToSecond;

                case Orcl.CLOB:
                case Orcl.NCLOB:
                    return cSharpTypeUsedForOracleClob;
                case Orcl.BLOB:
                    return cSharpTypeUsedForOracleBlob;
                case Orcl.BFILE:
                case Orcl.RAW:
                case Orcl.LONG:         // deprecated by Oracle
                case Orcl.LONG_RAW:     // deprecated by Oracle
                    return CSharp.BYTE_ARRAY;

                case Orcl.ANYDATA:
                case Orcl.ANYTYPE:
                case Orcl.ANYDATASET:
                    return CSharp.OBJECT;

                case Orcl.OBJECT_TYPE:
                    return Translater.ObjectTypeNamespace + "." + ConvertOracleNameToCSharpName(oracleTypeName, false);
            }

            // SELECT distinct data_type, pls_type
            // FROM sys.all_arguments 
            // WHERE pls_type IS NOT NULL and pls_type != data_type;
            //  FLOAT               REAL
            //  NCLOB               CLOB
            //  NVARCHAR2           VARCHAR2
            //  FLOAT               DOUBLE PRECISION
            //  VARCHAR2            STRING
            //  BINARY_INTEGER      PLS_INTEGER
            //  NUMBER              INTEGER
            //  PL/SQL BOOLEAN      BOOLEAN
            //  NUMBER              SMALLINT

            // SELECT data_type, count(*)
            // FROM all_tab_columns 
            // GROUP BY data_type
            // ORDER BY data_type

            // removed custom types
            // ANYDATA	185
            // BLOB	344
            // CHAR	23,219
            // CLOB	1,192
            // DATE	184,375
            // FLOAT	4951
            // INTERVAL DAY(2) TO SECOND(6)	1
            // INTERVAL DAY(3) TO SECOND(0)	51
            // INTERVAL DAY(3) TO SECOND(2)	12
            // INTERVAL DAY(5) TO SECOND(1)	7
            // INTERVAL DAY(9) TO SECOND(6)	9
            // INTERVAL DAY(9) TO SECOND(9)	18
            // LONG	1163
            // LONG RAW	126
            // NCHAR	4
            // NCLOB	4
            // NUMBER	787,266
            // NVARCHAR2	562
            // RAW	3,255
            // ROWID	7,965
            // ROWPOINTERLIST	2
            // TIMESTAMP(0) WITH TIME ZONE	2
            // TIMESTAMP(13) WITH TIME ZONE	14
            // TIMESTAMP(3)	39
            // TIMESTAMP(3) WITH TIME ZONE	8
            // TIMESTAMP(6)	1167
            // TIMESTAMP(6) WITH TIME ZONE	365
            // TIMESTAMP(9)	56
            // TIMESTAMP(9) WITH TIME ZONE	9
            // UNDEFINED	2,607
            // VARCHAR2	1,101,105
            // XMLTYPE	113

            // If we get to here, assume it is custom type. Else, let the compiler deal with it.
            return ConvertOracleNameToCSharpName(oracleType, false);
        }
        #endregion

        #region Miscellaneous
        /// <summary>
        /// Determine the size(length) for an OracleParameter bound to a C# String
        /// </summary>
        /// <param name="oracleType"></param>
        /// <returns></returns>
        public static short GetStringArgBindSize (String oracleType) { 

            switch (oracleType) {
                // fixed length of 2000
                case Orcl.CHAR:
                case Orcl.NCHAR:
                    return 2000;
            }

            return Parameter.Instance.MaxReturnAndOutArgStringSize;
        }

        /// <summary>
        /// Return the character limit for string types
        /// </summary>
        /// <param name="oracleArg"></param>
        /// <param name="nextArg"></param>
        /// <returns></returns>
        public static Int32? GetCharLength(Argument oracleArg, Argument nextArgUnused) {
            // for an associative array we must look at subsequent arg for the value
            return (oracleArg.DataType == Orcl.ASSOCIATITVE_ARRAY ? oracleArg.NextArgument : oracleArg).CharLength;
        }
        #endregion
    }
}

