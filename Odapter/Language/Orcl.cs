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

namespace Odapter {
    /// <summary>
    /// Oracle specific types and logic
    /// </summary>
    public class Orcl {
        #region PL/SQL Type Constants (as found in SYS.ALL_ARGUMENTS)
        // PL/SQL collection types
        public const string ASSOCIATITVE_ARRAY = "PL/SQL TABLE"; //
        internal const string NESTED_TABLE = "TABLE"; //
        internal const string VARRAY = "VARRAY"; //

        // PL/SQL-specific scalar types
        public const string REF_CURSOR = "REF CURSOR"; //
        internal const string BINARY_INTEGER = "BINARY_INTEGER"; //
        internal const string PLS_INTEGER = "PLS_INTEGER"; // same as BINARY_INTEGER
        internal const string PLSQL_BOOLEAN = "PL/SQL BOOLEAN"; //
        internal const string BOOLEAN = "BOOLEAN"; // found in pls_type column

        // SQL types, etc.
        public const string INTEGER = "INTEGER";
        internal const string INT = "INT";
        internal const string SMALLINT = "SMALLINT";
        internal const string UNSIGNED_INTEGER = "UNSIGNED INTEGER";
        internal const string STRING = "STRING";
        internal const string NCHAR = "NCHAR"; //
        internal const string VARCHAR = "VARCHAR";
        internal const string VARCHAR2 = "VARCHAR2"; //
        internal const string NVARCHAR2 = "NVARCHAR2";
        internal const string CHAR = "CHAR"; //
        public const string BLOB = "BLOB"; //
        public const string CLOB = "CLOB"; //
        internal const string NCLOB = "NCLOB";
        internal const string LONG = "LONG"; //
        internal const string ROWID = "ROWID"; //
        internal const string UROWID = "UROWID"; //
        internal const string REF = "REF"; //
        internal const string XML_TYPE = "XMLTYPE";
        public const string NUMBER = "NUMBER"; //
        internal const string NUMERIC = "NUMERIC"; // ?
        internal const string FLOAT = "FLOAT"; //
        internal const string DECIMAL = "DECIMAL";
        internal const string DOUBLE_PRECISION = "DOUBLE PRECISION";
        public const string DATE = "DATE"; //
        internal const string TIME_WITH_TIME_ZONE = "TIME WITH TIME ZONE"; // figure out
        internal const string RECORD = "PL/SQL RECORD"; //
        internal const string OBJECT_TYPE = "OBJECT"; //
        internal const string RAW = "RAW"; //
        internal const string LONG_RAW = "LONG RAW";  //
        internal const string BFILE = "BFILE"; //
        internal const string BINARY_DOUBLE = "BINARY_DOUBLE"; //
        internal const string BINARY_FLOAT = "BINARY_FLOAT"; //
        public const string TIMESTAMP = "TIMESTAMP"; //
        internal const string TIMESTAMP_WITH_LOCAL_TIME_ZONE = "TIMESTAMP WITH LOCAL TIME ZONE"; //
        internal const string TIMESTAMP_WITH_TIME_ZONE = "TIMESTAMP WITH TIME ZONE"; //
        public const string INTERVAL_DAY_TO_SECOND = "INTERVAL DAY TO SECOND"; //
        internal const string INTERVAL_YEAR_TO_MONTH = "INTERVAL YEAR TO MONTH"; //
        internal const string MLSLABEL = "MLSLABEL"; // deprecated
        internal const string UNDEFINED = "UNDEFINED"; //
        #endregion

        #region "Any" types
        internal const string ANYDATA = "ANYDATA";
        internal const string ANYTYPE = "ANYTYPE";
        internal const string ANYDATASET = "ANYDATASET";
        #endregion

        #region Miscellaneous
        internal const string IN = "IN";
        internal const string OUT = "OUT";
        internal const string INOUT = IN + "/" + OUT;
        internal const string YES = "YES";
        #endregion

        private static readonly List<String> _oracleKeywords = new List<String>(){  "SELECT", "FROM", "WHERE", "ORDER BY", "GROUP BY", "HAVING",
                                                                                    "UNION", "AS", "LIKE", "INSERT", "INTO", "TRUNC", 
                                                                                    "TO_DATE", "TO_CHAR", "TO_NUMBER", "TO_TIMESTAMP",
                                                                                    "VALUES", "SUBSTR", "LOWER", "UPPER", "NVL", "NVL2",
                                                                                    "REPLACE", "CHR", "LENGTH", "INSTR", "SYSDATE",
                                                                                    "SYSTIMESTAMP", "COUNT", "MIN", "MAX", "ROWNUM"};

        /// <summary>
        /// Determine if Oracle type is an unqualified NUMBER or an equivalent (i.e., no precision or scale)
        /// </summary>
        /// <param name="oracleType"></param>
        /// <returns></returns>
        internal static bool IsOracleNumberEquivalent(String oracleType) {
            if (new List<String>() { Orcl.NUMBER, Orcl.FLOAT, Orcl.BINARY_FLOAT }.Contains(oracleType)) return true;
            return true;
        }

        /// <summary>
        /// Build an oracle type with any precision, length, etc. qualifiers included.
        /// </summary>
        /// <param name="attr"></param>
        /// <returns></returns>
        internal static string BuildAggregateOracleType(IEntityAttribute attr) {
            if (String.IsNullOrEmpty(attr.AttrType)) return attr.AttrType;

            string oracleType = attr.AttrType.Trim();

            // handle Oracle aliasing
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
        /// Return the CharLength value of an Oracle argument. 
        /// </summary>
        /// <param name="oracleArg"></param>
        /// <returns></returns>
        internal static Int32? GetCharLength(IArgument oracleArg) {
            // for an associative array we must look at subsequent arg for the value
            return (oracleArg.DataType == Orcl.ASSOCIATITVE_ARRAY ? oracleArg.NextArgument : oracleArg).CharLength;
        }
    }
}

