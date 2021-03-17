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
    /// <summary>
    /// Handles case when there is no translater
    /// </summary>
    internal sealed class TranslaterNoneType : ITranslaterType {
        public string DataTypeFull { get; private set; } = String.Empty;
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.UNDEFINED); }
        // translation to C#
        public CS.ITypeTargetable CSharpType { get; private set; } = CSL.TypeNone;
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum { get => CS.OdpNetOracleDbTypeEnum.Byte; }
        public CS.ITypeTargetable CSharpOdpNetSafeType { get => CSL.TypeNone; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReasonAsParameter { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
        public bool IsIgnoredAsAttribute { get => true; }
        public string IgnoredReasonAsAttribute { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
        internal TranslaterNoneType(string dataTypeFull) {
            DataTypeFull = dataTypeFull;
            CSharpType = String.IsNullOrWhiteSpace(dataTypeFull) ? CSL.TypeNone : Trns.ClassNameOfOracleIdentifier(dataTypeFull) as CS.ITypeTargetable;
        }
        internal TranslaterNoneType() : this(String.Empty) { }
    }
}