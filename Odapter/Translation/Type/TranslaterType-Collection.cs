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
//    along with this program. If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;

namespace Odapter {
    internal sealed class TranslaterAssociativeArray : ITranslaterType {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.ASSOCIATITVE_ARRAY); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) {
            // A PL/SQL associative array is similar to a C# Dictionary. Although the Dicionary type behavior can
            // be used within PL/SQL, a Dictionary object cannot be pased from .NET. We can only pass the values
            // using an array index instead of key. So we treat an associative array as a List of a type in C#. 
            // The sub type is in the subsequent Oracle arg.
            return CSharp.GenericCollectionOf((nonInterfaceType ? CSharp.LIST_OF_T : CSharpType), SubCSharpType);
        }
        private string CSharpType { get; set; } 
        private string SubCSharpType { get; set; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => String.Empty; }
        public string CSharpOdpNetType { get => String.Empty; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        /// <summary>
        /// Attribute only possible for record type field (not table, view or object)
        /// </summary>
        public bool IsIgnoredAsAttribute { get => true; }
        public string IgnoredReasonAsAttribute { get => TranslaterMessage.IgnoreNoSendReceiveRecordFieldTyped(OrclType); }

        internal TranslaterAssociativeArray(string dataTypeFull, string cSharpType, ITyped dbDataType) {
            DataTypeFull = dataTypeFull;
            CSharpType = cSharpType;
            SubCSharpType = TranslaterFactoryType.GetTranslater(dbDataType.SubType).GetCSharpType();
        }
        private TranslaterAssociativeArray() { }

        public override string ToString() { return DataTypeFull; }

        // associative array subtypes officially implemented in ODP.NET https://docs.oracle.com/cd/E85694_01/ODPNT/featOraCommand.htm#GUID-05A6D391-E77F-41AF-83A2-FE86A3D98872
        //      BINARY_FLOAT
        //      CHAR,
        //      DATE,
        //      NCHAR,
        //      NUMBER, equiv or subtypes: INTEGER, INT, SMALLINT, FLOAT, DOUBLE_PRECISION, BINARY_DOUBLE,
        //      NVARCHAR2,
        //      RAW,
        //      ROWID,
        //      UROWID,
        //      VARCHAR2, equiv or subtypes: VARCHAR, STRING
    }

    internal sealed class TranslaterNestedTable : ITranslaterType {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.NESTED_TABLE); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) {
            return CSharp.GenericCollectionOf((nonInterfaceType ? CSharp.LIST_OF_T : CSharpType), SubCSharpType);
        }
        private string CSharpType { get; set; }
        private string SubCSharpType { get; set; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => String.Empty; }
        public string CSharpOdpNetType { get => String.Empty; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReasonAsParameter { get => TranslaterMessage.IgnoreNotImplemented($"NESTED {Orcl.NESTED_TABLE}"); }
        public bool IsIgnoredAsAttribute { get => true; }
        public string IgnoredReasonAsAttribute { get => TranslaterMessage.IgnoreNotImplemented($"NESTED {Orcl.NESTED_TABLE}"); }

        internal TranslaterNestedTable(string dataTypeFull, string cSharpType, ITyped dbDataType) {
            DataTypeFull = dataTypeFull;
            CSharpType = cSharpType;
            SubCSharpType = TranslaterFactoryType.GetTranslater(dbDataType.SubType).GetCSharpType();
        }
        private TranslaterNestedTable() { }

        public override string ToString() { return DataTypeFull; }
    }

    internal sealed class TranslaterVarray : ITranslaterType {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.VARRAY); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) {
            return CSharp.GenericCollectionOf((nonInterfaceType ? CSharp.LIST_OF_T : CSharpType), SubCSharpType);
        }
        private string CSharpType { get; set; }
        private string SubCSharpType { get; set; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => String.Empty; }
        public string CSharpOdpNetType { get => String.Empty; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReasonAsParameter { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
        public bool IsIgnoredAsAttribute { get => true; }
        public string IgnoredReasonAsAttribute { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }

        internal TranslaterVarray(string dataTypeFull, string cSharpType, ITyped dbDataType) {
            DataTypeFull = dataTypeFull;
            CSharpType = cSharpType;
            SubCSharpType = TranslaterFactoryType.GetTranslater(dbDataType.SubType).GetCSharpType();
        }
        private TranslaterVarray() { }

        public override string ToString() { return DataTypeFull; }
    }
}