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
    internal sealed class TranslaterObjectType : ITranslaterType {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.OBJECT); }

        // translation to C#
        public CS.ITypeTargetable CSharpType { get; private set; }
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CS.OdpNetOracleDbTypeEnum.Byte; }    // undefined
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CS.TypeValue.OracleBinary; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReasonAsParameter { get => TranslaterMessage.IgnoreNotAvailableOdpNetMananged(OrclType); }
        public (bool isIgnored, string reasonMsg) IsIgnoredAsRecordField() => (true, TranslaterMessage.IgnoreNotAvailableOdpNetMananged(OrclType));
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }
        internal TranslaterObjectType(string dataTypeFull) {
            DataTypeFull = dataTypeFull;
            CSharpType = Trns.InterfaceNameOfOracleIdentifier(dataTypeFull);
        }
        private TranslaterObjectType() { }
        public override string ToString() { return DataTypeFull; }
    }
}