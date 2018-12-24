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
    internal sealed class TranslaterObjectType : ITranslaterType {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.OBJECT); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) { return CSharpType; }
        private string CSharpType { get; set; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_OBJECT; }
        public string CSharpOdpNetType { get => String.Empty; }
        public bool IsIgnoredAsParameter { get => true; }
        public string IgnoredReason { get => TranslaterMessage.IgnoreNotAvailableOdpNetMananged(OrclType); }
        //public string IgnoredReason { get => TranslaterMessage.IgnoreNotImplemented(OrclType); } 

        internal TranslaterObjectType(string dataTypeFull) {
            DataTypeFull = dataTypeFull;
            CSharpType = TranslaterName.ConvertToPascal(dataTypeFull);
        }

        public override string ToString() { return DataTypeFull; }
    }
}