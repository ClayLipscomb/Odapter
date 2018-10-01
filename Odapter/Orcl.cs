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
    /// Oracle specific types and logic
    /// </summary>
    public class Orcl {
        #region PL/SQL Type Constants (as found in SYS.ALL_ARGUMENTS)
        // PL/SQL collection types
        public const string ASSOCIATITVE_ARRAY = "PL/SQL TABLE"; //
        public const string NESTED_TABLE = "TABLE"; //
        public const string VARRAY = "VARRAY"; //

        // PL/SQL-specific scalar types
        public const string REF_CURSOR = "REF CURSOR"; //
        public const string BINARY_INTEGER = "BINARY_INTEGER"; //
        public const string PLS_INTEGER = "PLS_INTEGER"; // same as BINARY_INTEGER
        public const string PLSQL_BOOLEAN = "PL/SQL BOOLEAN"; //
        public const string BOOLEAN = "BOOLEAN"; // found in pls_type column

        // SQL types, etc.
        public const string INTEGER = "INTEGER";
        public const string INT = "INT";
        public const string SMALLINT = "SMALLINT";
        public const string UNSIGNED_INTEGER = "UNSIGNED INTEGER";
        public const string STRING = "STRING";
        public const string NCHAR = "NCHAR"; //
        public const string VARCHAR = "VARCHAR";
        public const string VARCHAR2 = "VARCHAR2"; //
        public const string NVARCHAR2 = "NVARCHAR2";
        public const string CHAR = "CHAR"; //
        public const string BLOB = "BLOB"; //
        public const string CLOB = "CLOB"; //
        public const string NCLOB = "NCLOB";
        public const string LONG = "LONG"; //
        public const string ROWID = "ROWID"; //
        public const string UROWID = "UROWID"; //
        public const string REF = "REF"; //
        public const string XML_TYPE = "XMLTYPE";
        public const string NUMBER = "NUMBER"; //
        public const string NUMERIC = "NUMERIC"; // ?
        public const string FLOAT = "FLOAT"; //
        public const string DECIMAL = "DECIMAL";
        public const string DOUBLE_PRECISION = "DOUBLE PRECISION";
        public const string DATE = "DATE"; //
        public const string TIME_WITH_TIME_ZONE = "TIME WITH TIME ZONE"; // figure out
        public const string RECORD = "PL/SQL RECORD"; //
        public const string OBJECT_TYPE = "OBJECT"; //
        public const string RAW = "RAW"; //
        public const string LONG_RAW = "LONG RAW";  //
        public const string BFILE = "BFILE"; //
        public const string BINARY_DOUBLE = "BINARY_DOUBLE"; //
        public const string BINARY_FLOAT = "BINARY_FLOAT"; //
        public const string TIMESTAMP = "TIMESTAMP"; //
        public const string TIMESTAMP_WITH_LOCAL_TIME_ZONE = "TIMESTAMP WITH LOCAL TIME ZONE"; //
        public const string TIMESTAMP_WITH_TIME_ZONE = "TIMESTAMP WITH TIME ZONE"; //
        public const string INTERVAL_DAY_TO_SECOND = "INTERVAL DAY TO SECOND"; //
        public const string INTERVAL_YEAR_TO_MONTH = "INTERVAL YEAR TO MONTH"; //
        public const string MLSLABEL = "MLSLABEL"; // deprecated
        public const string UNDEFINED = "UNDEFINED"; //
        #endregion

        #region "Any" types
        public const string ANYDATA = "ANYDATA";
        public const string ANYTYPE = "ANYTYPE";
        public const string ANYDATASET = "ANYDATASET";
        #endregion

        #region Miscellaneous
        public const string IN = "IN";
        public const string OUT = "OUT";
        public const string INOUT = IN + "/" + OUT;
        public const string YES = "YES";
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
        public static bool IsOracleNumberEquivalent(String oracleType) {
            if (new List<String>() { Orcl.NUMBER, Orcl.FLOAT, Orcl.BINARY_FLOAT }.Contains(oracleType)) return true;
            return true;
        }
    }

}

