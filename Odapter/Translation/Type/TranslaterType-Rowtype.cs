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
    internal sealed class TranslaterRowtype : ITranslaterType {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.ROWTYPE); }

        // translation to C#
        public CS.ITypeTargetable CSharpType { get; private set; }
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum => CS.OdpNetOracleDbTypeEnum.Byte; // unused
        public CS.ITypeTargetable CSharpOdpNetSafeType => CSL.TypeNone;
        public bool IsIgnoredAsParameter { get => true; }  // as stand-alone rowtype
        public string IgnoredReasonAsParameter { get => TranslaterMessage.IgnoreNoSendReceiveRowtype(); }
        public bool IsIgnoredAsAttribute { get => true; }  // as stand-alone rowtype
        public string IgnoredReasonAsAttribute { get => TranslaterMessage.IgnoreNoSendReceiveRowtype(); }

        internal TranslaterRowtype(string dataTypeFull) {
            DataTypeFull = dataTypeFull;
            CSharpType = TranslaterName.ClassNameOfOracleIdentifier(dataTypeFull);
        }

        public override string ToString() { return DataTypeFull; }
    }
}