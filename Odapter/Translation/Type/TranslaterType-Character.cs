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
    internal class TranslaterChar : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.CHAR); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(CSharp.STRING); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_CHAR; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_STRING; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReason { get => String.Empty; }
    }

    internal class TranslaterLong : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.LONG); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(CSharp.BYTE_ARRAY); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_LONG; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_STRING; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReason { get => TranslaterMessage.IgnoreOracleDeprecation(OrclType); }
    }

    internal class TranslaterNchar : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.NCHAR); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(CSharp.STRING); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_NCHAR; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_STRING; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReason { get => String.Empty; }
    }

    internal class TranslaterNvarchar2 : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.NVARCHAR2); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(CSharp.STRING); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_NVARCHAR2; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_STRING; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReason { get => String.Empty; }
    }

    internal class TranslaterString : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.STRING); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(CSharp.STRING); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_VARCHAR2; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_STRING; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReason { get => String.Empty; }
    }

    internal class TranslaterVarchar : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.VARCHAR); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(CSharp.STRING); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_VARCHAR2; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_STRING; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReason { get => String.Empty; }
    }

    internal class TranslaterVarchar2 : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.VARCHAR2); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(CSharp.STRING); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_VARCHAR2; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_STRING; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReason { get => String.Empty; }
    }
}