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

namespace Odapter {
    internal sealed class TranslaterXmltype : ITranslaterType {
        public string DataTypeFull { get => OrclType.DataType; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.XMLTYPE); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get => CSharp.AsNullable(CSharp.XML_DOCUMENT); }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_XMLTYPE; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_XML_TYPE; }
        public bool IsIgnoredAsParameter { get => true; }
        //public string IgnoredReason { get => TranslaterMessage.IgnoreNotAvailableOdpNetMananged(DataType); }
        public string IgnoredReason { get => TranslaterMessage.IgnoreNotImplemented(OrclType); }
    }
}