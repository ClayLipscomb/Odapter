//------------------------------------------------------------------------------
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
using System.Linq;

namespace Odapter {
    /// <summary>
    /// Oracle specific types and logic
    /// </summary>
    public static class Orcl {
        #region PL/SQL Type Constants (as found in SYS.ALL_ARGUMENTS)
        // PL/SQL collection types
        public const string ASSOCIATITVE_ARRAY = "PL/SQL TABLE"; 
        internal const string NESTED_TABLE = "TABLE"; 
        internal const string VARRAY = "VARRAY"; 

        // PL/SQL-specific scalar types
        public const string REF_CURSOR = "REF CURSOR"; 
        internal const string BINARY_INTEGER = "BINARY_INTEGER"; 
        internal const string NATURAL = "NATURAL"; 
        internal const string NATURALN = "NATURALN"; 
        internal const string POSITIVE = "POSITIVE"; 
        internal const string POSITIVEN = "POSITIVEN";

        internal const string PLS_INTEGER = "PLS_INTEGER"; // same as BINARY_INTEGER
        internal const string PLSQL_BOOLEAN = "PL/SQL BOOLEAN"; 
        internal const string BOOLEAN = "BOOLEAN"; // found in pls_type column

        // PL/SQL and SQL "entity" types
        internal const string TABLE = "TABLE";
        internal const string VIEW = "VIEW";
        internal const string OBJECT = "OBJECT"; 
        internal const string RECORD = "PL/SQL RECORD"; 

        // SQL types, etc.
        public const string INTEGER = "INTEGER";
        internal const string INT = "INT";
        internal const string SMALLINT = "SMALLINT";
        internal const string UNSIGNED_INTEGER = "UNSIGNED INTEGER";
        internal const string STRING = "STRING";
        internal const string NCHAR = "NCHAR"; 
        internal const string VARCHAR = "VARCHAR";
        internal const string VARCHAR2 = "VARCHAR2"; 
        internal const string NVARCHAR2 = "NVARCHAR2";
        internal const string CHAR = "CHAR"; 
        public const string BLOB = "BLOB"; 
        public const string CLOB = "CLOB";
        internal const string NCLOB = "NCLOB";
        internal const string LONG = "LONG"; 
        internal const string ROWID = "ROWID"; 
        internal const string UROWID = "UROWID"; 
        internal const string REF = "REF"; 
        internal const string XMLTYPE = "XMLTYPE";
        public const string NUMBER = "NUMBER"; 
        internal const string NUMERIC = "NUMERIC"; 
        internal const string FLOAT = "FLOAT"; 
        internal const string DECIMAL = "DECIMAL";
        internal const string DOUBLE_PRECISION = "DOUBLE PRECISION";
        internal const string REAL = "REAL";
        public const string DATE = "DATE"; 
        internal const string TIME_WITH_TIME_ZONE = "TIME WITH TIME ZONE"; 
        internal const string RAW = "RAW"; 
        internal const string LONG_RAW = "LONG RAW";  
        internal const string BFILE = "BFILE"; 
        internal const string BINARY_DOUBLE = "BINARY_DOUBLE"; 
        internal const string BINARY_FLOAT = "BINARY_FLOAT"; 
        public const string TIMESTAMP = "TIMESTAMP"; 
        internal const string TIMESTAMP_WITH_LOCAL_TIME_ZONE = "TIMESTAMP WITH LOCAL TIME ZONE"; 
        internal const string TIMESTAMP_WITH_TIME_ZONE = "TIMESTAMP WITH TIME ZONE"; 
        public const string INTERVAL_DAY_TO_SECOND = "INTERVAL DAY TO SECOND"; 
        internal const string INTERVAL_YEAR_TO_MONTH = "INTERVAL YEAR TO MONTH"; 
        internal const string MLSLABEL = "MLSLABEL"; // deprecated
        internal const string UNDEFINED = "UNDEFINED"; 
        internal const string NULL = "NULL";        // represents a NULL return type found only a procedure "paramter"
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
        internal const string OF = "OF";
        internal const string RETURN = "RETURN";
        internal const string UNDERSCORE = "_";
        internal const string PERIOD = ".";
        #endregion

        private static readonly List<String> _oracleKeywords = new List<String>(){  "SELECT", "FROM", "WHERE", "ORDER BY", "GROUP BY", "HAVING",
                                                                                    "UNION", "AS", "LIKE", "INSERT", "INTO", "TRUNC", 
                                                                                    "TO_DATE", "TO_CHAR", "TO_NUMBER", "TO_TIMESTAMP",
                                                                                    "VALUES", "SUBSTR", "LOWER", "UPPER", "NVL", "NVL2",
                                                                                    "REPLACE", "CHR", "LENGTH", "INSTR", "SYSDATE",
                                                                                    "SYSTIMESTAMP", "COUNT", "MIN", "MAX", "ROWNUM"};
    }
}