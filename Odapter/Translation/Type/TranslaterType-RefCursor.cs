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
using Trns = Odapter.Translation.Api;

namespace Odapter {
    internal sealed class TranslaterRefCursorTyped : ITranslaterType {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.REF_CURSOR); }
        // translation to C#
        public CS.ITypeTargetable CSharpType { get => CSL.TypeCollectionGeneric(_typeCollection, _subTypeGeneric); }
        public CS.ITypeTargetable CSharpSubType { get => _subTypeGeneric; }
        private CS.TypeTarget TypeTargetTest { get; set; }
        private readonly CS.TypeCollection _typeCollection;
        private readonly CS.TypeGenericName _subTypeGeneric;
        private readonly ITranslaterType _translaterSubType;
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CS.OdpNetOracleDbTypeEnum.RefCursor; }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeReference.OracleRefCursor; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        internal TranslaterRefCursorTyped(string dataTypeFull, CS.TypeCollection typeCollection, ITyped dbDataType) {
            //TypeTargetTest = CS.TypeTarget.NewTargetValue(CS.TypeValue.Double);
            DataTypeFull = dataTypeFull;
            _typeCollection = typeCollection;
            _translaterSubType = TranslaterFactoryType.GetTranslater(dbDataType?.SubType);
            //_subTypeGeneric = TranslaterName.TypeGenericNameOfOracleIdentifier(_translaterSubType?.DataTypeFull);
            _subTypeGeneric = CSL.TypeGenericNameOf(_translaterSubType?.CSharpType);
        }
        private TranslaterRefCursorTyped() { }
        public override string ToString() { return DataTypeFull; }
    }

    internal sealed class TranslaterRefCursorUntyped : ITranslaterType {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.REF_CURSOR); }
        // translation to C#
        public CS.ITypeTargetable CSharpType { get =>
            // a weakly typed cursor is either a DataTable or generic list
            TranslaterManager.UseDatatableForUntypedCursor
                ? CS.TypeReference.DataTable
                : CSL.TypeCollectionGeneric(_typeCollection, _subTypeGeneric) as CS.ITypeTargetable; 
        }
        public CS.ITypeTargetable CSharpSubType { get => _subTypeGeneric; }
        private readonly CS.TypeCollection _typeCollection;
        private readonly CS.TypeGenericName _subTypeGeneric;
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CS.OdpNetOracleDbTypeEnum.RefCursor; }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeReference.OracleRefCursor; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        internal TranslaterRefCursorUntyped(string dataTypeFull, CS.TypeCollection typeCollection, ITyped dbDataType) {
            DataTypeFull = dataTypeFull;
            _typeCollection = typeCollection;
            // create informative subtype name that is unique among multiple untyped(cursor) args in proc
            _subTypeGeneric = Trns.TypeGenericNameOfOracleIdentifier((dbDataType.DataTypeLabel ?? Orcl.RETURN) + @"_UNTYPED");
        }
        private TranslaterRefCursorUntyped() { }
        public override string ToString() { return DataTypeFull; }
    }
}