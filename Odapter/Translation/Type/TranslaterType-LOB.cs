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
//    along with this program. If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;

namespace Odapter {
    internal class TranslaterBfile : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.BFILE); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(_cSharpType); } private readonly string _cSharpType = CSharp.BYTE_ARRAY;
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_BFILE; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_BFILE; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReason { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
    }

    internal class TranslaterBlob : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.BLOB); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(_cSharpType); } private readonly string _cSharpType;
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_BLOB; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_BLOB; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReason { get => String.Empty; }

        internal TranslaterBlob(string cSharpType) { _cSharpType = cSharpType; }
        private TranslaterBlob() { }
    }

    internal class TranslaterClob : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.CLOB); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(_cSharpType); } private readonly string _cSharpType;
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_CLOB; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_CLOB; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReason { get => String.Empty; }

        internal TranslaterClob(string cSharpType) { _cSharpType = cSharpType; }
        private TranslaterClob() { }
    }

    internal class TranslaterLongRaw : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.LONG_RAW); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(CSharp.BYTE_ARRAY); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_LONGRAW; }
        public string CSharpOdpNetType { get => String.Empty; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReason { get => TranslaterMessage.IgnoreOracleDeprecation(OrclType); }
    }

    internal class TranslaterNclob : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.NCLOB); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(_cSharpType); } private readonly string _cSharpType;
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_NCLOB; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_CLOB; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReason { get => String.Empty; }

        internal TranslaterNclob(string cSharpType) { _cSharpType = cSharpType; }
        private TranslaterNclob() { }
    }

    internal class TranslaterRaw : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.RAW); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(CSharp.BYTE_ARRAY); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_RAW; }
        public string CSharpOdpNetType { get => String.Empty; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReason { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
    }
}
