//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2021 Clay Lipscomb
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
using System.Data;
using System.Linq;

namespace Odapter {
    public enum CSharpVersion { ThreeZero, FourZero }

    /// <summary>
    /// C# specific types and logic
    /// </summary>
    internal sealed class CSharp {
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
        public const string DATE_TIME_OFFSET = "DateTimeOffset";
        public const string TIME_SPAN = "TimeSpan";
        public const string DOUBLE = "Double";
        public const string SINGLE = "Single";
        public const string FLOAT = "float";
        public const string DECIMAL = "Decimal";
        public const string STRING = "String";
        public const string LIST_OF_STRING = LIST + "<" + STRING + ">";
        public const string BOOLEAN = "Boolean";
        public const string OBJECT_TYPE = "Object";
        public const string XML_DOCUMENT = "XmlDocument";
        public const string DATATABLE = "DataTable";
        private const string INTERFACE_PREFIX = "I";
        internal const string LIST = "List";
        internal const string ILIST = INTERFACE_PREFIX + LIST;
        private const string COLLECTION = "Collection";
        internal const string ICOLLECTION = INTERFACE_PREFIX + COLLECTION;
        public static readonly List<string> GENERIC_COLLECTION_BASE_TYPES = new List<string>() { LIST, ILIST, ICOLLECTION };  // implemented to date
        public const string VOID = "void";
        public const string GENERIC_TYPE_PREFIX = "T_";
        public const string NULLABLE_SUFFIX = @"?";
        private const string ARRAY_BRACKETS = @"[]";

        // ODP.NET types (important for assoc array out/return args)
        private const string ODP_NET_TYPE_PREFIX = "Oracle";
        public const string ODP_NET_SAFE_TYPE_DECIMAL       = ODP_NET_TYPE_PREFIX + "Decimal";
        public const string ODP_NET_SAFE_TYPE_DATE          = ODP_NET_TYPE_PREFIX + "Date";
        public const string ODP_NET_SAFE_TYPE_TIMESTAMP     = ODP_NET_TYPE_PREFIX + "TimeStamp";
        public const string ODP_NET_SAFE_TYPE_TIMESTAMP_LTZ = ODP_NET_TYPE_PREFIX + "TimeStampLTZ";
        public const string ODP_NET_SAFE_TYPE_TMESTAMP_TZ   = ODP_NET_TYPE_PREFIX + "TimeStampTZ";
        public const string ODP_NET_SAFE_TYPE_INTERVAL_DS   = ODP_NET_TYPE_PREFIX + "IntervalDS";
        public const string ODP_NET_SAFE_TYPE_INTERVAL_YM   = ODP_NET_TYPE_PREFIX + "IntervalYM";
        public const string ODP_NET_SAFE_TYPE_STRING        = ODP_NET_TYPE_PREFIX + "String";
        public const string ODP_NET_SAFE_TYPE_REF_CURSOR    = ODP_NET_TYPE_PREFIX + "RefCursor";    // class
        public const string ODP_NET_SAFE_TYPE_BFILE         = ODP_NET_TYPE_PREFIX + "BFile";        // class
        public const string ODP_NET_SAFE_TYPE_BLOB          = ODP_NET_TYPE_PREFIX + "Blob";         // class
        public const string ODP_NET_SAFE_TYPE_CLOB          = ODP_NET_TYPE_PREFIX + "Clob";         // class
        public const string ODP_NET_SAFE_TYPE_BINARY        = ODP_NET_TYPE_PREFIX + "Binary";
        public const string ODP_NET_SAFE_TYPE_REF           = ODP_NET_TYPE_PREFIX + "Ref";
        public const string ODP_NET_SAFE_TYPE_XML_TYPE      = ODP_NET_TYPE_PREFIX + "XmlType";

        public const string NULL = "null";
        internal const string CLASS = "class";
        #endregion

        #region Access modifiers
        internal const string PUBLIC = "public";
        internal const string PROTECTED = "protected";
        internal const string PRIVATE = "private";
        #endregion

        #region Type categories
        internal static readonly IList<string> StructTypes = new List<string> {
            SBYTE, BYTE, INT16, UINT16, INT32, UINT32, INT64, UINT64, DATE_TIME, DATE_TIME_OFFSET, TIME_SPAN, DOUBLE, SINGLE, FLOAT, DECIMAL,
            ODP_NET_SAFE_TYPE_DECIMAL, ODP_NET_SAFE_TYPE_STRING,
            ODP_NET_SAFE_TYPE_DATE, ODP_NET_SAFE_TYPE_TIMESTAMP, ODP_NET_SAFE_TYPE_TIMESTAMP_LTZ, ODP_NET_SAFE_TYPE_TMESTAMP_TZ,
            ODP_NET_SAFE_TYPE_INTERVAL_DS, ODP_NET_SAFE_TYPE_INTERVAL_YM,
            ODP_NET_SAFE_TYPE_BINARY, ODP_NET_SAFE_TYPE_REF, ODP_NET_SAFE_TYPE_XML_TYPE
        };

        private static readonly Dictionary<string, string> NumericOracleDbTypeEnums = new Dictionary<string, string>() {
            { CSharp.SBYTE,                         ORACLEDBTYPE_BYTE },
            { CSharp.BYTE,                          ORACLEDBTYPE_BYTE },
            { CSharp.INT16,                         ORACLEDBTYPE_INT16 },
            { CSharp.UINT16,                        ORACLEDBTYPE_INT16 },
            { CSharp.INT32,                         ORACLEDBTYPE_INT32 },
            { CSharp.UINT32,                        ORACLEDBTYPE_INT32 },
            { CSharp.INT64,                         ORACLEDBTYPE_INT64 },
            { CSharp.UINT64,                        ORACLEDBTYPE_INT64 },
            { CSharp.DECIMAL,                       ORACLEDBTYPE_DECIMAL },
            { CSharp.ODP_NET_SAFE_TYPE_DECIMAL,     ORACLEDBTYPE_DECIMAL },
            { CSharp.DOUBLE,                        ORACLEDBTYPE_BINARY_DOUBLE },
            { CSharp.SINGLE,                        ORACLEDBTYPE_BINARY_FLOAT }
        };
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
        private const string ORACLEDBTYPE = "OracleDbType.";
        private const string ORACLEDBTYPE_BYTE = ORACLEDBTYPE + BYTE;
        private const string ORACLEDBTYPE_INT16 = ORACLEDBTYPE + INT16;
        private const string ORACLEDBTYPE_INT32 = ORACLEDBTYPE + INT32;
        private const string ORACLEDBTYPE_INT64 = ORACLEDBTYPE + INT64;
        private const string ORACLEDBTYPE_DECIMAL = ORACLEDBTYPE + DECIMAL;
        private const string ORACLEDBTYPE_DOUBLE = ORACLEDBTYPE + DOUBLE;
        public const string ORACLEDBTYPE_DATE = ORACLEDBTYPE + "Date";
        public const string ORACLEDBTYPE_TIMESTAMP = ORACLEDBTYPE + "TimeStamp";
        public const string ORACLEDBTYPE_TIMESTAMP_TZ = ORACLEDBTYPE + "TimeStampTZ";
        public const string ORACLEDBTYPE_TIMESTAMP_LTZ = ORACLEDBTYPE + "TimeStampLTZ";
        public const string ORACLEDBTYPE_BINARY_DOUBLE = ORACLEDBTYPE + "BinaryDouble";
        public const string ORACLEDBTYPE_BINARY_FLOAT = ORACLEDBTYPE + "BinaryFloat";
        public const string ORACLEDBTYPE_VARCHAR2 = ORACLEDBTYPE + "Varchar2";
        public const string ORACLEDBTYPE_OBJECT = ORACLEDBTYPE + "Object";
        public const string ORACLEDBTYPE_REF_CURSOR = ORACLEDBTYPE + "RefCursor";
        public const string ORACLEDBTYPE_RAW = ORACLEDBTYPE + "Raw";
        public const string ORACLEDBTYPE_LONGRAW = ORACLEDBTYPE + "LongRaw";
        public const string ORACLEDBTYPE_BFILE = ORACLEDBTYPE + "BFile";
        public const string ORACLEDBTYPE_BLOB = ORACLEDBTYPE + "Blob";
        public const string ORACLEDBTYPE_CLOB = ORACLEDBTYPE + "Clob";
        public const string ORACLEDBTYPE_NCLOB = ORACLEDBTYPE + "NClob";
        public const string ORACLEDBTYPE_LONG = ORACLEDBTYPE + "Long";
        public const string ORACLEDBTYPE_REF = ORACLEDBTYPE + "Ref";
        public const string ORACLEDBTYPE_XMLTYPE = ORACLEDBTYPE + "XmlType";
        public const string ORACLEDBTYPE_NCHAR = ORACLEDBTYPE + "NChar";
        public const string ORACLEDBTYPE_NVARCHAR2 = ORACLEDBTYPE + "NVarchar2";
        public const string ORACLEDBTYPE_CHAR = ORACLEDBTYPE + "Char";
        public const string ORACLEDBTYPE_INTERVAL_DAY_TO_SECOND = ORACLEDBTYPE + "IntervalDS";
        public const string ORACLEDBTYPE_INTERVAL_YEAR_TO_MONTH = ORACLEDBTYPE + "IntervalYM";
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

        internal static string GetNumericOracleDbTypeEnum(string cSharp) {
            string oracleDbTypeEnum;
            if (NumericOracleDbTypeEnums.TryGetValue(cSharp, out oracleDbTypeEnum)) {
                return oracleDbTypeEnum;
            } else {
                return "C#type_" + cSharp + "_NotFoundInNumericOracleDbTypeEnums";
            }
        }

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
        internal static bool IsValidGenericCollectionType(string type) {
            foreach (string gcbt in CSharp.GENERIC_COLLECTION_BASE_TYPES) if (type.StartsWith(gcbt + "<") && type.EndsWith(">")) return true;
            return false;
        }

        /// <summary>
        /// Create complete generic colection type from base and sub type 
        /// </summary>
        /// <param name="genCollectionBaseType">Base type of generic collection</param>
        /// <param name="subType">Subtype; the T type</param>
        /// <returns></returns>
        internal static string GenericCollectionOf(string genCollectionBaseType, string subType) {
            return $"{genCollectionBaseType}<{subType}>";
        }

        /// <summary>
        /// For a valid interface name, return equivalent class name
        /// </summary>
        /// <param name="interfaceName"></param>
        /// <returns></returns>
        internal static string DeInterface(string interfaceName) {
            if (!String.IsNullOrWhiteSpace(interfaceName) && interfaceName.StartsWith(INTERFACE_PREFIX) && interfaceName.Substring(0, 2).All(char.IsUpper)) {
                return interfaceName.TrimStart(INTERFACE_PREFIX.ToCharArray());
            } else {
                return interfaceName;   // not an interface, return argument as-is
            }
        }

        internal static string ToInterface(string className) {
            if (String.IsNullOrWhiteSpace(className)) {
                return className;
            } else {
                return INTERFACE_PREFIX + className;
            }
        }

        /// <summary>
        /// Is C# type an ODP.NET type?
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool IsOdpNetType(string type) {
            return !String.IsNullOrEmpty(type) && type.StartsWith(ODP_NET_TYPE_PREFIX);
        }

        internal static string AsNullable(string cSharpType) {
            return cSharpType.Trim() + (StructTypes.Contains(cSharpType) ? NULLABLE_SUFFIX  : String.Empty);
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

