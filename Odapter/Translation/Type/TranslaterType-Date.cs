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
using CS = Odapter.CSharp;
using CSL = Odapter.CSharp.Logic.Api;

namespace Odapter {
    internal class TranslaterDate : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.DATE); }
        // translation to C#
        public CS.ITypeTargetable CSharpType { get; private set; }
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CS.OdpNetOracleDbTypeEnum.Date; }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDate; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        internal TranslaterDate(CS.TypeValue typeValue) { CSharpType = typeValue.Nullable; } 
        private TranslaterDate() { }
    }

    internal class TranslaterIntervalDayToSecond : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.INTERVAL_DAY_TO_SECOND); }
        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.TimeSpan.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CS.OdpNetOracleDbTypeEnum.IntervalDS; }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleIntervalDS; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReasonAsParameter { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
        public bool IsIgnoredAsAttribute { get => true; }
        public string IgnoredReasonAsAttribute { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
    }

    internal class TranslaterIntervalYearToMonth : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.INTERVAL_YEAR_TO_MONTH); }
        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.TimeSpan.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CS.OdpNetOracleDbTypeEnum.IntervalYM; }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleIntervalYM; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReasonAsParameter { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
        public bool IsIgnoredAsAttribute { get => true; }
        public string IgnoredReasonAsAttribute { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
    }

    internal class TranslaterTimestamp : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.TIMESTAMP); }
        // translation to C#
        public CS.ITypeTargetable CSharpType { get; private set; }
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CS.OdpNetOracleDbTypeEnum.TimeStamp; }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleTimeStamp; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        internal TranslaterTimestamp(CS.TypeValue typeValue) { CSharpType = typeValue.Nullable; }
        private TranslaterTimestamp() { }
    }

    internal class TranslaterTimestampLTZ : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.TIMESTAMP_WITH_LOCAL_TIME_ZONE); }
        // translation to C#
        public CS.ITypeTargetable CSharpType { get; private set; }
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CS.OdpNetOracleDbTypeEnum.TimeStampLTZ; }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleTimeStampLTZ; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        internal TranslaterTimestampLTZ(CS.TypeValue typeValue) { CSharpType = typeValue.Nullable; }
        private TranslaterTimestampLTZ() { }
    }

    internal class TranslaterTimestampTZ : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.TIMESTAMP_WITH_TIME_ZONE); }
        // translation to C#
        public CS.ITypeTargetable CSharpType { get; private set; }
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CS.OdpNetOracleDbTypeEnum.TimeStampTZ; }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleTimeStampTZ; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        internal TranslaterTimestampTZ(CS.TypeValue typeValue) { CSharpType = typeValue.Nullable; }
        private TranslaterTimestampTZ() { }
    }
}