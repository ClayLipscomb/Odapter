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
    internal class TranslaterDate : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.DATE); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return (typeNotNullable ? CSharpType.TrimEnd('?') : CSharpType); }
        private string CSharpType { get => CSharp.AsNullable(_cSharpType); } private readonly string _cSharpType;
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_DATE; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_DATE; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReason { get => String.Empty; }

        internal TranslaterDate(string cSharpType) { _cSharpType = cSharpType; }
        private TranslaterDate() { }
    }

    internal class TranslaterIntervalDayToSecond : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.INTERVAL_DAY_TO_SECOND); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return (typeNotNullable ? CSharpType.TrimEnd('?') : CSharpType); }
        private string CSharpType { get => CSharp.AsNullable(CSharp.TIME_SPAN); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_INTERVAL_DAY_TO_SECOND; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_INTERVAL_DS; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReason { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
    }

    internal class TranslaterIntervalYearToMonth : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.INTERVAL_YEAR_TO_MONTH); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return (typeNotNullable ? CSharpType.TrimEnd('?') : CSharpType); }
        private string CSharpType { get => CSharp.AsNullable(CSharp.TIME_SPAN); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_INTERVAL_YEAR_TO_MONTH; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_INTERVAL_YM; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReason { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
    }

    internal class TranslaterTimestamp : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.TIMESTAMP); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return (typeNotNullable ? CSharpType.TrimEnd('?') : CSharpType); }
        private string CSharpType { get => CSharp.AsNullable(_cSharpType); } private readonly string _cSharpType;
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_TIMESTAMP; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_TIMESTAMP; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReason { get => String.Empty; }

        internal TranslaterTimestamp(string cSharpType) { _cSharpType = cSharpType; }
        private TranslaterTimestamp() { }
    }

    internal class TranslaterTimestampWithLocalTimeZone : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.TIMESTAMP_WITH_LOCAL_TIME_ZONE); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return (typeNotNullable ? CSharpType.TrimEnd('?') : CSharpType); }
        private string CSharpType { get => CSharp.AsNullable(CSharp.DATE_TIME); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_TIMESTAMP_LTZ; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_TIMESTAMP_LTZ; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReason { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
    }

    internal class TranslaterTimestampWithTimeZone : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.TIMESTAMP_WITH_TIME_ZONE); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return (typeNotNullable ? CSharpType.TrimEnd('?') : CSharpType); }
        private string CSharpType { get => CSharp.AsNullable(CSharp.DATE_TIME); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_TIMESTAMP_TZ; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_TMESTAMP_TZ; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReason { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
    }
}