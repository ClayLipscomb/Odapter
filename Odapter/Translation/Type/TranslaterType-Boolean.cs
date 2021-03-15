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
    internal class TranslaterBoolean : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.BOOLEAN); }
        // translation to C#
        public CS.ITypeTargetable CSharpType { get => CS.TypeReference.Boolean; }
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum => CS.OdpNetOracleDbTypeEnum.Byte; // unused
        public CS.ITypeTargetable CSharpOdpNetSafeType => CSL.TypeNone;
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReasonAsParameter { get => TranslaterMessage.IgnoreNoSendReceive(OrclType); }
        public bool IsIgnoredAsAttribute { get => true; }
        public string IgnoredReasonAsAttribute { get => TranslaterMessage.IgnoreNoSendReceive(OrclType); }
    }

    internal class TranslaterPlsqlBoolean : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.PLSQL_BOOLEAN); }
        // translation to C#
        public CS.ITypeTargetable CSharpType { get => CS.TypeReference.Boolean; }
        public CS.ITypeTargetable CSharpSubType { get => CSL.TypeNone; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public CS.OdpNetOracleDbTypeEnum CSharpOracleDbTypeEnum => CS.OdpNetOracleDbTypeEnum.Byte; // unused
        public CS.ITypeTargetable CSharpOdpNetSafeType => CSL.TypeNone;
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReasonAsParameter { get => TranslaterMessage.IgnoreNoSendReceive(OrclType); }
        public bool IsIgnoredAsAttribute { get => true; }
        public string IgnoredReasonAsAttribute { get => TranslaterMessage.IgnoreNoSendReceive(OrclType); }
    }
}