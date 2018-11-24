﻿//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2019 Clay Lipscomb
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

namespace Odapter {
    /// <summary>
    /// Handle translation from Oracle to C#
    /// </summary>
    public class Translater {

        internal static readonly IList<string> OracleTypesIgnored = new List<string> {
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

        internal static readonly IList<string> TypesImplementedForAssociativeArrays = new List<string> {
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

        internal static void Initialize(IParameterTranslation param) {
            CSharpTypeUsedForOracleRefCursor                = param.CSharpTypeUsedForOracleRefCursor;
            CSharpTypeUsedForOracleAssociativeArray         = param.CSharpTypeUsedForOracleAssociativeArray;
            CSharpTypeUsedForOracleInteger                  = param.CSharpTypeUsedForOracleInteger;
            CSharpTypeUsedForOracleNumber                   = param.CSharpTypeUsedForOracleNumber;
            IsConvertOracleNumberToIntegerIfColumnNameIsId  = param.IsConvertOracleNumberToIntegerIfColumnNameIsId;
            CSharpTypeUsedForOracleDate                     = param.CSharpTypeUsedForOracleDate;
            CSharpTypeUsedForOracleTimeStamp                = param.CSharpTypeUsedForOracleTimeStamp;
            CSharpTypeUsedForOracleIntervalDayToSecond      = param.CSharpTypeUsedForOracleIntervalDayToSecond;
            CSharpTypeUsedForOracleBlob                     = param.CSharpTypeUsedForOracleBlob;
            CSharpTypeUsedForOracleClob                     = param.CSharpTypeUsedForOracleClob;
            NamespaceObjectType                             = param.NamespaceObjectType;
            SchemaFilter                                    = param.Filter;
        }

        #region Data Sets for UI Binding
        public class CSharpVersionOption {
            public CSharpVersion Version { get; private set; }
            public string DisplayDescription { get; private set; }
            public CSharpVersionOption(CSharpVersion version, string displayDescription) {
                Version = version;
                DisplayDescription = displayDescription;
            }
        }

        public static readonly IList<CSharpVersionOption> CSharpOptions = new List<CSharpVersionOption> {
            new CSharpVersionOption(CSharpVersion.ThreeZero, @"3.0 (.NET 3.5, unmanaged ODP.NET)"),
            new CSharpVersionOption(CSharpVersion.FourZero , @"4.0 (.NET 4.0 minimum, managed ODP.NET)")
        };

        public class CustomTranslatedCSharpType {
            public string CSharpType { get; private set; }
            private string TranslationNote { get; set; }
            public string DisplayDescription { get; private set; }
            public CustomTranslatedCSharpType(string cSharpType, string translationNote) {
                CSharpType = cSharpType;
                TranslationNote = translationNote;
                DisplayDescription = cSharpType + (string.IsNullOrWhiteSpace(translationNote) ? "" : " (" + translationNote + ")");
            }
        }

        public static readonly IDictionary<string, List<CustomTranslatedCSharpType>> CustomTypeTranslationOptions = new Dictionary<string, List<CustomTranslatedCSharpType>>() {
            {Orcl.REF_CURSOR, new List<CustomTranslatedCSharpType> {                new CustomTranslatedCSharpType(CSharp.ILIST_OF_T, @""),
                                                                                    new CustomTranslatedCSharpType(CSharp.ICOLLECTION_OF_T, @""),
                                                                                    new CustomTranslatedCSharpType(CSharp.LIST_OF_T, @"concrete, not recommended") } },
            {Orcl.ASSOCIATITVE_ARRAY, new List<CustomTranslatedCSharpType> {        new CustomTranslatedCSharpType(CSharp.ILIST_OF_T, @""),
                                                                                    new CustomTranslatedCSharpType(CSharp.LIST_OF_T, @"concrete, not recommended") } },
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
        #endregion

        #region Properties for Custom Type Translation
        private static string CSharpTypeUsedForOracleRefCursor { get;  set; }
        private static string CSharpTypeUsedForOracleAssociativeArray { get;  set; }
        private static string CSharpTypeUsedForOracleInteger { get; set; }
        private static string CSharpTypeUsedForOracleNumber { get; set; }
        private static bool IsConvertOracleNumberToIntegerIfColumnNameIsId = true;
        private static string CSharpTypeUsedForOracleDate { get; set; }
        private static string CSharpTypeUsedForOracleTimeStamp { get; set; }
        private static string CSharpTypeUsedForOracleIntervalDayToSecond { get; set; }
        private static string CSharpTypeUsedForOracleBlob { get; set; }
        private static string CSharpTypeUsedForOracleClob { get; set; }
        private static string NamespaceObjectType { get; set; }
        private static string SchemaFilter { get; set; }
        #endregion

        #region Properties for Advanced Options
        internal static bool UseGenericListForCursor { get; set; }
        #endregion

        #region General Constants
        private const char UNDERSCORE = '_';
        private const string CHARACTER_ABBREV = "char";
        #endregion

        #region Translation methods
        /// <summary>
        /// Determine whether entity should be ignored due to certain data types
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="reasonMsg"></param>
        /// <returns></returns>
        internal static bool IsIgnoredDueToOracleTypes(IEntity entity, out string reasonMsg) {
            reasonMsg = "";

            foreach (string oraType in OracleTypesIgnored) {
                if (entity.Attributes != null && entity.Attributes.FindIndex(a => a.DataType.Equals(oraType)) != -1) {
                    reasonMsg = GetOracleTypeIgnoredReason(oraType, entity.Attributes[0].GetType().Name.ToLower()); // get reason
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Determine whether procedure should be ignored due to certain data types
        /// </summary>
        /// <param name="proc"></param>
        /// <param name="reasonMsg"></param>
        /// <returns></returns>
        internal static bool IsIgnoredDueToOracleTypes(IProcedure proc, out string reasonMsg) {
            reasonMsg = "";
            string unimplemntedType = "";

            if (proc.HasArgumentOfOracleTypeAssocArrayOfUnimplementedType(out unimplemntedType)) {
                reasonMsg = GetOracleTypeIgnoredReason(Orcl.ASSOCIATITVE_ARRAY, "of a " + unimplemntedType);
                return true;
            } else if (proc.HasInArgumentOfOracleTypeRefCursor()) {
                reasonMsg = GetOracleTypeIgnoredReason(Orcl.REF_CURSOR);
                return true;
            } else {
                foreach (string oraType in OracleTypesIgnored)
                    if (proc.HasArgumentOfOracleType(oraType, !oraType.Equals(Orcl.RECORD))) {
                        reasonMsg = GetOracleTypeIgnoredReason(oraType);
                        return true;
                    }
            }

            return false;
        }

        /// <summary>
        /// Build reason message for why an Oracle type will be ignored during generation. Is is assumed that 
        /// it has already been deteremined that the Oracle type will be ignored.
        /// </summary>
        /// <param name="oracleType"></param>
        /// <param name="reasonMsgAppend"></param>
        /// <returns></returns>
        internal static string GetOracleTypeIgnoredReason(string oracleType, string reasonMsgAppend = "") {
            string reasonMsg = "";
            if (String.IsNullOrWhiteSpace(oracleType)) return reasonMsg;

            // convert types to user friendly language
            string oracleTypeFormatted = oracleType
                .Replace('_', ' ').Replace("PLSQL", @"PL/SQL").Replace(Orcl.OBJECT_TYPE, "OBJECT TYPE").Replace(Orcl.ASSOCIATITVE_ARRAY, "associative array")
                + (String.IsNullOrWhiteSpace(reasonMsgAppend) ? "" : " " + reasonMsgAppend);

            switch (oracleType) { 
                case Orcl.PLSQL_BOOLEAN:
                case Orcl.NESTED_TABLE:
                    reasonMsg = $".NET cannot send/receive a { oracleTypeFormatted }"; // + oracleTypeFormatted;
                    break;
                case Orcl.RECORD:
                    reasonMsg = $".NET cannot send/receive a { oracleTypeFormatted } (apart from cursor)";
                    break;
                case Orcl.REF_CURSOR:           // this case should only be for a cursor IN argument
                    reasonMsg = $".NET cannot send a { oracleTypeFormatted }";
                    break;
                case Orcl.ASSOCIATITVE_ARRAY:   // this case should be for sub type not implemented for an assoc array
                    reasonMsg = $".NET cannot send/receive an { oracleTypeFormatted }";
                    break;
                case Orcl.UNDEFINED:
                    reasonMsg = "At least one Oracle type is undefined";
                    break;
                case Orcl.LONG_RAW:
                case Orcl.RAW:
                    reasonMsg = $"Code generation for { oracleTypeFormatted } will not be implemented due to Oracle deprecation";
                    break;
                default:
                    reasonMsg = $"Code generation for { oracleTypeFormatted } types has not been implemented";
                    break;
            }

            return reasonMsg;
        }

        /// <summary>
        /// Convert an Oracle entity/object name (table, package, argument, column etc.) to a valid C# equivalent 
        /// </summary>
        /// <param name="oracleArgName"></param>
        /// <param name="useCamelCase">convert to camelCase, otherwise defaults to PascalCase</param>
        /// <returns></returns>
        internal static string ConvertOracleNameToCSharpName(string oracleName, bool useCamelCase) {
            if (String.IsNullOrEmpty(oracleName)) return null; // this occurs with a return arg

            string oracleNameAdjusted = oracleName;

            // replace special characters with alphabetic equivalent
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"!", "exclamationpoint" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"@", "at" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"#", "pound" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"$", "dollar" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"%", "percent" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"^", "caret" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"&", "ampersand" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"*", "asterisk" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"-", "dash" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"+", "plus" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"=", "equals" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@".", "period" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@"?", "questionmark" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@":", "colon" + CHARACTER_ABBREV);
            oracleNameAdjusted = oracleNameAdjusted.Replace(@";", "semicolon" + CHARACTER_ABBREV);

            string cSharpName = (useCamelCase
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
        internal static string ConvertOracleProcNameToMethodName(IProcedure proc, IPackage package) {
            string methodName = ConvertOracleNameToCSharpName(proc.ProcedureName, false);

            // prevent identical class name and method name - yes, I've seen this happen in Oracle
            if (proc.PackageName != null && proc.PackageName == proc.ProcedureName) methodName += "Proc";

            // if proc has duplicate sig, append overload number to name to keep unique in C#
            if (package.HasDuplicateSignature(proc)) methodName += proc.Overload;

            return methodName;
        }

        /// <summary>
        /// Convert a record in an Oracle argument into a C# class name
        /// </summary>
        /// <param name="oracleArg"></param>
        /// <returns></returns>
        private static string ConvertOracleRecordNameToCSharpName(IArgument oracleArg) {
            // Type and subtype can be null (e.g., a bug in the view when a record type based on a table). In this case, 
            //      use proc name (which is what subtype usually is anyway) and some extra special text. We need a 
            //      better algorithm to guarantee uniqueness in the C# namespace.
            if (String.IsNullOrEmpty(oracleArg.TypeSubname)) {
                return ConvertOracleNameToCSharpName(oracleArg.ProcedureName + UNDERSCORE + (oracleArg.ArgumentName ?? "RETURN") + UNDERSCORE + "ROW_TYPE", false);
                // if the argument's record is defined in another package and there is a filter, the C# name must be prefixed with the source package name to prevent naming conflict
            } else if (!String.IsNullOrWhiteSpace(SchemaFilter) && !oracleArg.PackageName.Equals(oracleArg.TypeName)) {
                return ConvertOracleNameToCSharpName(oracleArg.TypeName, false) + ConvertOracleNameToCSharpName(oracleArg.TypeSubname, false);
                // normal record type
            } else {
                return ConvertOracleNameToCSharpName(oracleArg.TypeSubname, false);
            }
        }

        /// <summary>
        /// Convert an Oracle record field name to a C# property name
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="recordName"></param>
        /// <param name="usePascalCase"></param>
        /// <returns></returns>
        internal static string ConvertOracleRecordFieldNameToCSharpPropertyName(string fieldName, string recordName, bool usePascalCase) {
            string propertyName = ConvertOracleNameToCSharpName(fieldName, usePascalCase);

            // prevent identical class name and property name which is not allowed by C#
            if (usePascalCase && recordName.Equals(fieldName)) propertyName += "Field";
            return propertyName;
        }

        /// <summary>
        /// Convert Oracle type to the equivalent C# ODP.NET type
        /// </summary>
        /// <param name="oracleType"></param>
        /// <returns></returns>
        internal static string ConvertOracleTypeToOdpNetType(string oracleType) {
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
                //case Orcl.LONG_RAW:
                //case Orcl.RAW:
                //    return CSharp.ORACLE_BINARY;
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
        internal static string ConvertOracleArgTypeToCSharpOracleDbType(IArgument oracleArg) {
            if (oracleArg == null || oracleArg.DataType == null) return null;

            // the DbType needed for an assoc array to work is simply the type of its subsequent nested arg
            if (oracleArg.DataType.Equals(Orcl.ASSOCIATITVE_ARRAY)) return ConvertOracleArgTypeToCSharpOracleDbType(oracleArg.NextArgument);

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
                //case Orcl.LONG_RAW:
                //    return CSharp.ORACLEDBTYPE_LONGRAW;
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
            string cSharpType = ConvertOracleArgTypeToCSharpType(oracleArg, true);

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
        /// Convert the type of an Oracle argument to its equivalent C# argument type
        /// </summary>
        /// <param name="oracleArg">Oracle argument to be converted</param>
        /// <param name="typeNotNullable">Determines whether C# type can not be nullable, defaults to false. </param>
        /// <param name="nonInterfaceType">Do not convert to C# interface type.</param>
        /// <returns></returns>
        internal static string ConvertOracleArgTypeToCSharpType(IArgument oracleArg, bool typeNotNullable, bool nonInterfaceType = false) {
            if (oracleArg == null) return null;

            // a PL/SQL record will have a custom class built for it; here we only need to return that class name as the type
            if (oracleArg.DataType.Equals(Orcl.RECORD)) return ConvertOracleRecordNameToCSharpName(oracleArg);

            // A PL/SQL associative array is technically the equivalent of C# Dictionary. Although the Dicionary behavior can
            // be used within PL/SQL between functions, a Dictionary object cannot be pased from .NET. We can only pass the values
            // of using an array index instead of key. So we treat an associative array as a List of a type or class in C#. The type is in 
            // the subsequent Oracle arg.
            if (oracleArg.DataType.Equals(Orcl.ASSOCIATITVE_ARRAY)) {
                string arrayType = (oracleArg.NextArgument.DataType == Orcl.RECORD 
                    ? ConvertOracleRecordNameToCSharpName(oracleArg)
                    : ConvertOracleArgTypeToCSharpType(oracleArg.NextArgument, false));
                return CSharp.GenericCollectionOf((nonInterfaceType ? CSharp.LIST_OF_T : CSharpTypeUsedForOracleAssociativeArray), arrayType);
            }

            // a nested table to a List (even though we are not handling nested tables yet)
            if (oracleArg.DataType.Equals(Orcl.NESTED_TABLE)) return CSharp.GenericCollectionOf(CSharp.LIST_OF_T, ConvertOracleArgTypeToCSharpType(oracleArg.NextArgument, true));

            // a cursor translates to a List
            // a strongly typed cursor is a generic list, but based on record type
            // a weakly typed cursor is either a generic list or Datatable
            if (oracleArg.DataType.Equals(Orcl.REF_CURSOR)) {
                return (oracleArg.NextArgument == null || oracleArg.NextArgument.DataLevel == oracleArg.DataLevel // is it weakly typed cursor?
                    ? (UseGenericListForCursor
                        // generic list; create informative subtype name that is unique among multilple untyped (cursor) args in proc
                        ? CSharp.GenericCollectionOf((nonInterfaceType ? CSharp.LIST_OF_T : CSharpTypeUsedForOracleRefCursor), CSharp.GENERIC_TYPE_PREFIX
                            + (ConvertOracleNameToCSharpName(oracleArg.ArgumentName, true) ?? "return") + "Untyped")
                        // otherwise, we are configured to use a Datatable
                        : CSharp.DATATABLE) 
                    : (UseGenericListForCursor
                        // generic list of a type based on, or extended from, the cursor's record type; include record type in name
                        ? CSharp.GenericCollectionOf((nonInterfaceType ? CSharp.LIST_OF_T : CSharpTypeUsedForOracleRefCursor), CSharp.GENERIC_TYPE_PREFIX 
                            + ConvertOracleArgTypeToCSharpType(oracleArg.NextArgument, true))
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
                oracleType = Orcl.BuildAggregateOracleType(oracleArg);
            } else if (oracleArg.PlsType != null) {
                oracleType = oracleArg.PlsType.Trim(); // never seen this case even on a large number of schemas including Oracle E-Business apps
            } else {
                oracleType = String.Empty; // this happens when there is a single "arg" row for a proc with no args
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
        internal static string ConvertOracleTypeToCSharpType(string oracleType, string oracleName, bool typeNotNullable, string oracleTypeName) {
            
            // create all type names dependent on typeNotNullable argument
            string cSharpTypeUsedForOracleInteger = CSharpTypeUsedForOracleInteger + (typeNotNullable ? "" : "?");
            string sByteType = CSharp.SBYTE + (typeNotNullable ? "" : "?");
            string int16Type = CSharp.INT16 + (typeNotNullable ? "" : "?");
            string int32Type = CSharp.INT32 + (typeNotNullable ? "" : "?");
            string int64Type = CSharp.INT64 + (typeNotNullable ? "" : "?");
            string singleType = CSharp.SINGLE + (typeNotNullable ? "" : "?");
            string doubleType = CSharp.DOUBLE + (typeNotNullable ? "" : "?");
            string cSharpTypeUsedForOracleDate  = CSharpTypeUsedForOracleDate + (typeNotNullable ? "" : "?");
            string cSharpTypeUsedForOracleTimeStamp = CSharpTypeUsedForOracleTimeStamp + (typeNotNullable ? "" : "?");
            string cSharpTypeUsedForOracleIntervalDayToSecond = CSharpTypeUsedForOracleIntervalDayToSecond + (typeNotNullable ? "" : "?");
            string cSharpTypeUsedForOracleBlob = CSharpTypeUsedForOracleBlob; // no types require ?
            string cSharpTypeUsedForOracleClob = CSharpTypeUsedForOracleClob; // no types require ?
            //string timeSpanType = CSharp.TIME_SPAN + (typeNotNullable ? "" : "?");
            string cSharpTypeUsedForOracleNumber = CSharpTypeUsedForOracleNumber + (typeNotNullable ? "" : "?");

            // ******************************************************
            // Judgment call interpretations for NUMBER to INTEGER 
            //  1. name a) is "id" or b) ends in "_id" 
            if ((oracleType == Orcl.NUMBER)
                    && IsConvertOracleNumberToIntegerIfColumnNameIsId
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
                    // NUMBER(10) and above is type chosen by user
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
                    return NamespaceObjectType + "." + ConvertOracleNameToCSharpName(oracleTypeName, false);
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

        internal static bool CanBeCSharpInterface(string argumentDataType) {
            if (String.IsNullOrWhiteSpace(argumentDataType)) return false;
            return argumentDataType.Equals(Orcl.REF_CURSOR) || argumentDataType.Equals(Orcl.ASSOCIATITVE_ARRAY);
        }
        #endregion
    }
}