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
    internal sealed class TranslaterBinaryDouble : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.BINARY_DOUBLE); }

        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.Double.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
    }

    internal sealed class TranslaterBinaryFloat : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.BINARY_FLOAT); }

        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.Single.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
    }

    internal sealed class TranslaterBinaryInteger : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.BINARY_INTEGER); }

        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.Int32.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
    }

    internal sealed class TranslaterDecimal : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.DECIMAL); }

        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.Decimal.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
    }

    internal sealed class TranslaterDoublePrecision : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.DOUBLE_PRECISION); }

        // translation to C#
        public CS.ITypeTargetable CSharpType { get => cSharpType; } private readonly CS.TypeValueNullable cSharpType;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        internal TranslaterDoublePrecision(CS.TypeValue typeValue) { cSharpType = typeValue.Nullable; }
        private TranslaterDoublePrecision() { }
    }

    internal sealed class TranslaterFloat : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.FLOAT); }

        // translation to C#
        public CS.ITypeTargetable CSharpType { get => cSharpType; } private readonly CS.TypeValueNullable cSharpType;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        internal TranslaterFloat(CS.TypeValue typeValue) { cSharpType = typeValue.Nullable; }
        private TranslaterFloat() { }
    }

    internal sealed class TranslaterInteger : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.INTEGER); }

        // translation to C#
        public CS.ITypeTargetable CSharpType { get => cSharpType; } private readonly CS.TypeValueNullable cSharpType;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) {
            if ((dataType.DataType == Orcl.INTEGER)
                ||
                ((dataType.DataType == Orcl.NUMBER || dataType.DataType == Orcl.DECIMAL) && dataType.DataScale == 0 && dataType.DataPrecision >= 10)  // NUMBER(10,0) or greater
                ||
                ((dataType.DataType == Orcl.NUMBER || dataType.DataType == Orcl.DECIMAL) && dataType.DataScale == null && dataType.DataPrecision == null  // NUMBER
                    && OrclUtil.IsIdLabel(dataType.DataTypeLabel) && IsConvertIdNumberToInteger))
                return true;
            
            return false;
        }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        private bool IsConvertIdNumberToInteger { get; set; }
        internal TranslaterInteger(CS.TypeValue typeValue, bool isConvertIdNumbertoInteger) { cSharpType = typeValue.Nullable; IsConvertIdNumberToInteger = isConvertIdNumbertoInteger; }
        private TranslaterInteger() { }
    }

    internal sealed class TranslaterNatural : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.NATURAL); }

        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.Int32.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
    }

    internal sealed class TranslaterNaturaln : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.NATURALN); }

        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.Int32.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
    }

    internal class TranslaterNumber : ITranslaterType {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.NUMBER); }

        // translation to C#
        public CS.ITypeTargetable CSharpType { get => cSharpType; } private readonly CS.TypeValueNullable cSharpType;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public virtual bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        internal TranslaterNumber(CS.TypeValue typeValue) : this(typeValue, Orcl.NUMBER) { }
        protected TranslaterNumber(CS.TypeValue typeValue, string dataTypeFull) { cSharpType = typeValue.Nullable; DataTypeFull = dataTypeFull; }
        private TranslaterNumber() { }
    }

    internal sealed class TranslaterNumber1 : TranslaterNumber {
        private const sbyte _precision = 1;
        public override bool IsValid(ITyped dataType) { return OrclUtil.IsWholeNumberOfPrecision(dataType, _precision); }
        internal TranslaterNumber1() : base(CS.TypeValue.SByte, OrclUtil.AppendPrecision(Orcl.NUMBER, _precision)) { }  // precision specific
    }

    internal sealed class TranslaterNumber2 : TranslaterNumber {
        private const sbyte _precision = 2;
        public override bool IsValid(ITyped dataType) { return OrclUtil.IsWholeNumberOfPrecision(dataType, _precision); }
        internal TranslaterNumber2() : base(CS.TypeValue.SByte, OrclUtil.AppendPrecision(Orcl.NUMBER, _precision)) { }  // precision specific
    }

    internal sealed class TranslaterNumber3 : TranslaterNumber {
        private const sbyte _precision = 3;
        public override bool IsValid(ITyped dataType) { return OrclUtil.IsWholeNumberOfPrecision(dataType, _precision); }
        internal TranslaterNumber3() : base(CS.TypeValue.Int16, OrclUtil.AppendPrecision(Orcl.NUMBER, _precision)) { }  // precision specific
    }

    internal sealed class TranslaterNumber4 : TranslaterNumber {
        private const sbyte _precision = 4;
        public override bool IsValid(ITyped dataType) { return OrclUtil.IsWholeNumberOfPrecision(dataType, _precision); }
        internal TranslaterNumber4() : base(CS.TypeValue.Int16, OrclUtil.AppendPrecision(Orcl.NUMBER, _precision)) { }  // precision specific
    }

    internal sealed class TranslaterNumber5 : TranslaterNumber {
        private const sbyte _precision = 5;
        public override bool IsValid(ITyped dataType) { return OrclUtil.IsWholeNumberOfPrecision(dataType, _precision); }
        internal TranslaterNumber5() : base(CS.TypeValue.Int32, OrclUtil.AppendPrecision(Orcl.NUMBER, 5)) { }  // precision specific
    }

    internal sealed class TranslaterNumber6 : TranslaterNumber {
        private const sbyte _precision = 6;
        public override bool IsValid(ITyped dataType) { return OrclUtil.IsWholeNumberOfPrecision(dataType, _precision); }
        internal TranslaterNumber6() : base(CS.TypeValue.Int32, OrclUtil.AppendPrecision(Orcl.NUMBER, 6)) { }  // precision specific
    }

    internal sealed class TranslaterNumber7 : TranslaterNumber {
        private const sbyte _precision = 7;
        public override bool IsValid(ITyped dataType) { return OrclUtil.IsWholeNumberOfPrecision(dataType, _precision); }
        internal TranslaterNumber7() : base(CS.TypeValue.Int32, OrclUtil.AppendPrecision(Orcl.NUMBER, 7)) { }  // precision specific
    }

    internal sealed class TranslaterNumber8 : TranslaterNumber {
        private const sbyte _precision = 8;
        public override bool IsValid(ITyped dataType) { return OrclUtil.IsWholeNumberOfPrecision(dataType, _precision); }
        internal TranslaterNumber8() : base(CS.TypeValue.Int32, OrclUtil.AppendPrecision(Orcl.NUMBER, 8)) { }  // precision specific
    }

    internal sealed class TranslaterNumber9 : TranslaterNumber {
        private const sbyte _precision = 9;
        public override bool IsValid(ITyped dataType) { return OrclUtil.IsWholeNumberOfPrecision(dataType, _precision); }
        internal TranslaterNumber9() : base(CS.TypeValue.Int32, OrclUtil.AppendPrecision(Orcl.NUMBER, 9)) { }  // precision specific
    }

    internal sealed class TranslaterNumeric : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.NUMERIC); }

        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.Decimal.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
    }

    internal sealed class TranslaterPlsInteger : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.PLS_INTEGER); }

        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.Int32.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
    }

    internal sealed class TranslaterPositive : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.POSITIVE); }

        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.Int32.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
    }

    internal sealed class TranslaterPositiven : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.POSITIVEN); }

        // translation to C#
        public CS.ITypeTargetable CSharpType => cSharpType; private readonly CS.TypeValueNullable cSharpType = CS.TypeValue.Int32.Nullable;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
    }

    internal sealed class TranslaterReal : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.REAL); }

        // translation to C#
        public CS.ITypeTargetable CSharpType { get => cSharpType; } private readonly CS.TypeValueNullable cSharpType;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
        internal TranslaterReal(CS.TypeValue typeValue) { cSharpType = typeValue.Nullable; }
        private TranslaterReal() { }
    }

    internal sealed class TranslaterSmallint : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.SMALLINT); }

        // translation to C#
        public CS.ITypeTargetable CSharpType { get => cSharpType; } private readonly CS.TypeValueNullable cSharpType;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CSL.NumericOdpNetOracleDbTypeEnum(cSharpType); }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleDecimal; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (false, String.Empty);
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        internal TranslaterSmallint(CS.TypeValue typeValue) { cSharpType = typeValue.Nullable; }
        private TranslaterSmallint() { }
    }
}