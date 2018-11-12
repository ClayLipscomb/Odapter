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
using System.Data;

namespace Odapter {
    public enum CSharpVersion { ThreeZero, FourZero }

    /// <summary>
    /// C# specific types and logic
    /// </summary>
    public class CSharp {
        #region C# Types
        public const string SBYTE = "SByte";
        public const string BYTE = "Byte";
        public const string SBYTE_ARRAY = SBYTE + ARRAY_BRACKETS;
        public const string BYTE_ARRAY = BYTE + ARRAY_BRACKETS;
        public const string INT16 = "Int16";            // short
        public const string UINT16 = "UInt16";          // unsigned short
        public const string INT32 = "Int32";            // int
        public const string UINT32 = "UInt32";          // unsigned int
        public const string INT64 = "Int64";            // long
        public const string UINT64 = "UInt64";          // unsigned long
        public const string BIG_INTEGER = "BigInteger";
        public const string BIG_RATIONAL = "BigRational";
        public const string DATE_TIME = "DateTime";
        public const string TIME_SPAN = "TimeSpan";
        public const string DOUBLE = "Double";
        public const string SINGLE = "Single";
        public const string FLOAT = "float";
        public const string DECIMAL = "Decimal";
        public const string STRING = "String";
        public const string LIST_OF_STRING = LIST + "<" + STRING + ">";
        public const string BOOLEAN = "Boolean";
        public const string OBJECT = "Object";
        public const string XML_DOCUMENT = "XmlDocument";
        public const string DATATABLE = "DataTable";
        private const string OF_T = "<T>";
        private const string LIST = "List";
        private const string ILIST = "IList";
        private const string ICOLLECTION = "ICollection";
        public const string LIST_OF_T = LIST + OF_T;
        public const string ILIST_OF_T = ILIST + OF_T;
        public const string ICOLLECTION_OF_T = ICOLLECTION + OF_T;
        public static readonly List<String> GENERIC_COLLECTION_BASE_TYPES = new List<String>() { LIST, ILIST, ICOLLECTION };  // implemented to date
        public const string VOID = "void";
        public const string GENERIC_TYPE_PREFIX = "T_";
        public const string NULLABLE_SUFFIX = @"?";
        private const string ARRAY_BRACKETS = @"[]";

        // ODP.NET types (important for assoc array out/return args)
        private const string ODP_NET_TYPE_PREFIX = "Oracle";
        public const string ORACLE_DECIMAL = ODP_NET_TYPE_PREFIX + "Decimal";
        public const string ORACLE_DATE = ODP_NET_TYPE_PREFIX + "Date";
        public const string ORACLE_TIMESTAMP = ODP_NET_TYPE_PREFIX + "TimeStamp";
        public const string ORACLE_TIMESTAMP_LTZ = ODP_NET_TYPE_PREFIX + "TimeStampLTZ";
        public const string ORACLE_TIMESTAMP_TZ = ODP_NET_TYPE_PREFIX + "TimeStampTZ";
        public const string ORACLE_INTERVAL_DS = ODP_NET_TYPE_PREFIX + "IntervalDS";
        public const string ORACLE_INTERVAL_YM = ODP_NET_TYPE_PREFIX + "IntervalYM";
        public const string ORACLE_XLM_TYPE = ODP_NET_TYPE_PREFIX + "XmlType";
        public const string ORACLE_STRING = ODP_NET_TYPE_PREFIX + "String";
        public const string ORACLE_BFILE = ODP_NET_TYPE_PREFIX + "BFile";           // class
        public const string ORACLE_BLOB = ODP_NET_TYPE_PREFIX + "Blob";             // class
        public const string ORACLE_CLOB = ODP_NET_TYPE_PREFIX + "Clob";             // class
        public const string ORACLE_BINARY = ODP_NET_TYPE_PREFIX + "Binary";
        public const string ORACLE_REF = ODP_NET_TYPE_PREFIX + "Ref";
        public const string ORACLE_REF_CURSOR = ODP_NET_TYPE_PREFIX + "RefCursor";  // class
        public const string ORACLE_XML_TYPE = ODP_NET_TYPE_PREFIX + "XmlType";

        public const string NULL = "null";
        #endregion

        #region Code Syntax
        public const string ATTRIBUTE_SERIALIZABLE = @"[Serializable()]";
        public const string ATTRIBUTE_DATA_CONTRACT = @"DataContract";
        public const string PARTIAL = "partial";
        public const string ABSTRACT = "abstract";
        public const string GET_ORACLE = "GetOracle";
        public const string USING = "using";
        #endregion

        #region ODP.NET OracleDbType Enum values
        private const string ORACLEDDBTYPE = "OracleDbType.";
        public const string ORACLEDBTYPE_BYTE = ORACLEDDBTYPE + BYTE;
        public const string ORACLEDBTYPE_INT16 = ORACLEDDBTYPE + INT16;
        public const string ORACLEDBTYPE_INT32 = ORACLEDDBTYPE + INT32;
        public const string ORACLEDBTYPE_INT64 = ORACLEDDBTYPE + INT64;
        public const string ORACLEDBTYPE_DECIMAL = ORACLEDDBTYPE + DECIMAL;
        public const string ORACLEDBTYPE_DOUBLE = ORACLEDDBTYPE + DOUBLE;
        public const string ORACLEDBTYPE_DATE = ORACLEDDBTYPE + "Date";
        public const string ORACLEDBTYPE_TIMESTAMP = ORACLEDDBTYPE + "TimeStamp";
        public const string ORACLEDBTYPE_TIMESTAMP_TZ = ORACLEDDBTYPE + "TimeStampTZ";
        public const string ORACLEDBTYPE_TIMESTAMP_LTZ = ORACLEDDBTYPE + "TimeStampLTZ";
        public const string ORACLEDBTYPE_BINARY_DOUBLE = ORACLEDDBTYPE + "BinaryDouble";
        public const string ORACLEDBTYPE_BINARY_FLOAT = ORACLEDDBTYPE + "BinaryFloat";
        public const string ORACLEDBTYPE_VARCHAR2 = ORACLEDDBTYPE + "Varchar2";
        public const string ORACLEDBTYPE_OBJECT = ORACLEDDBTYPE + "Object";
        public const string ORACLEDBTYPE_CURSOR = ORACLEDDBTYPE + "RefCursor";
        public const string ORACLEDBTYPE_RAW = ORACLEDDBTYPE + "Raw";
        public const string ORACLEDBTYPE_LONGRAW = ORACLEDDBTYPE + "LongRaw";
        public const string ORACLEDBTYPE_BFILE = ORACLEDDBTYPE + "BFile";
        public const string ORACLEDBTYPE_BLOB = ORACLEDDBTYPE + "Blob";
        public const string ORACLEDBTYPE_CLOB = ORACLEDDBTYPE + "Clob";
        public const string ORACLEDBTYPE_NCLOB = ORACLEDDBTYPE + "NClob";
        public const string ORACLEDBTYPE_LONG = ORACLEDDBTYPE + "Long";
        //public const string ORACLEDBTYPE_REF = ORACLEDDBTYPE + "Ref";
        public const string ORACLEDBTYPE_XMLTYPE = ORACLEDDBTYPE + "XmlType";
        public const string ORACLEDBTYPE_NCHAR = ORACLEDDBTYPE + "NChar";
        public const string ORACLEDBTYPE_NVARCHAR2 = ORACLEDDBTYPE + "NVarchar2";
        public const string ORACLEDBTYPE_CHAR = ORACLEDDBTYPE + "Char";
        public const string ORACLEDBTYPE_INTERVAL_DAY_TO_SECOND = ORACLEDDBTYPE + "IntervalDS";
        public const string ORACLEDBTYPE_INTERVAL_YEAR_TO_MONTH = ORACLEDDBTYPE + "IntervalYM";
        #endregion

        #region using
        //public const string USING_ORACLE_DATAACCESS_CLIENT = USING + " " + "Oracle.ManagedDataAccess.Client";
        //public const string USING_ORACLE_DATAACCESS_TYPES = USING + " " + "Oracle.ManagedDataAccess.Types";
        #endregion 

        #region Keywords
        private enum Keyword {
            ABSTRACT, EVENT, NEW, STRUCT,
            AS, EXPLICIT, NULL, SWITCH,
            BASE, EXTERN, OBJECT, THIS,
            BOOL, FALSE, OPERATOR, THROW,
            BREAK, FINALLY, OUT, TRUE,
            BYTE, FIXED, OVERRIDE, TRY,
            CASE, FLOAT, PARAMS, TYPEOF,
            CATCH, FOR, PRIVATE, UINT,
            CHAR, FOREACH, PROTECTED, ULONG,
            CHECKED, GOTO, PUBLIC, UNCHECKED,
            CLASS, IF, READONLY, UNSAFE,
            CONST, IMPLICIT, REF, USHORT,
            CONTINUE, IN, RETURN, USING,
            DECIMAL, INT, SBYTE, VIRTUAL,
            DEFAULT, INTERFACE, SEALED, VOLATILE,
            DELEGATE, INTERNAL, SHORT, VOID,
            DO, IS, SIZEOF, WHILE,
            DOUBLE, LOCK, STACKALLOC,
            ELSE, LONG, STATIC,
            ENUM, NAMESPACE, STRING,
            // a few contextual keywords I don't want to allow 
            DYNAMIC, GET, LET, PARTIAL, SET, VALUE, VAR
        }
        #endregion

        #region Misc
        public const string READ_RESULT = "ReadResult";
        #endregion
        /// <summary>
        /// Determine if string is a C# keyword
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        internal static bool IsKeyword(string word) {
            return Enum.IsDefined(typeof(Keyword), word.ToUpper());
        }

        /// <summary>
        /// Accepts generic collection type and returns its sub type
        /// </summary>
        /// <param name="genCollType">Generic collection type</param>
        /// <param name="excludeNullableSymbol">Whether to exclude nullable symbol from returned subtype</param>
        /// <returns></returns>
        internal static string ExtractSubtypeFromGenericCollectionType(string genCollType, bool excludeNullableSymbol) {
            if (!IsValidGenericCollectionType(genCollType)) return "InvalidTypeSentToExtractSubtypeFromGenericCollectionType:" + genCollType;
            string subType = genCollType.Substring(genCollType.IndexOf('<') + 1, genCollType.Length - genCollType.IndexOf('<') - 2);
            return (excludeNullableSymbol ? subType.TrimEnd(NULLABLE_SUFFIX.ToCharArray()) : subType);
        }

        /// <summary>
        /// Determines if type is a valid generic collection type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsValidGenericCollectionType(String type) {
            if (!type.EndsWith(">")) return false;
            foreach (String gcbt in CSharp.GENERIC_COLLECTION_BASE_TYPES) if (type.StartsWith(gcbt + "<")) return true;
            return false;
        }

        /// <summary>
        /// Create complete generic colection type from base and sub type 
        /// </summary>
        /// <param name="genCollectionBaseType">Base type of generic collection</param>
        /// <param name="subType">Subtype; the T type</param>
        /// <returns></returns>
        internal static string GenericCollectionOf(String genCollectionBaseType, string subType) {
            return genCollectionBaseType.Replace(OF_T, "") + "<" + subType + ">";
        }

        /// <summary>
        /// Is C# type an ODP.NET type?
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsOdpNetType(string type) {
            return !String.IsNullOrEmpty(type) && type.StartsWith(ODP_NET_TYPE_PREFIX);
        }

        /// <summary>
        /// Is a C# type nullable
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsTypeNullable(string type) {
            if (String.IsNullOrEmpty(type)) return false;

            return type.EndsWith(NULLABLE_SUFFIX)
                || type.Equals(STRING)
                || type.Equals(DATATABLE)
                || type.StartsWith(LIST)
                || type.StartsWith(ILIST)
                || type.StartsWith(ICOLLECTION);
        }
    }
}

