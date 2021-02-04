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

namespace Odapter {
    internal sealed class TranslaterRefCursorTyped : ITranslaterType {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.REF_CURSOR); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) {
            return CSharp.GenericCollectionOf((nonInterfaceType ? CSharp.LIST_OF_T : CSharpType), CSharp.GENERIC_TYPE_PREFIX + SubCSharpType);
        }
        private string CSharpType { get => _cSharpType; } private readonly string _cSharpType;
        private string SubCSharpType { get => _subCSharpType; } private readonly string _subCSharpType;
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_REF_CURSOR; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_REF_CURSOR; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }

        internal TranslaterRefCursorTyped(string dataTypeFull, string cSharpType, ITyped dbDataType) {
            DataTypeFull = dataTypeFull;
            _cSharpType = cSharpType;
            _subCSharpType = TranslaterFactoryType.GetTranslater(dbDataType.SubType).GetCSharpType();
        }
        private TranslaterRefCursorTyped() { }

        public override string ToString() { return DataTypeFull; }
    }

    internal sealed class TranslaterRefCursorUntyped : ITranslaterType {
        public string DataTypeFull { get; private set; }
        public IOrclType OrclType { get => OrclUtil.GetType(Orcl.REF_CURSOR); }

        // translation to C#
        public string GetCSharpType(bool typeNotNullable = false, bool nonInterfaceType = false) {
            // a weakly typed cursor is either a generic list or DataTable
            return TranslaterManager.UseDatatableForUntypedCursor
                    // otherwise, we are configured to use a Datatable
                    ? CSharp.DATATABLE
                    // generic list; create informative subtype name that is unique among multiple untyped (cursor) args in proc
                    : CSharp.GenericCollectionOf((nonInterfaceType ? CSharp.LIST_OF_T : CSharpType), CSharp.GENERIC_TYPE_PREFIX + SubCSharpType);
        }
        private string CSharpType { get; set; }
        private string SubCSharpType { get; set; }
        public bool IsValid(ITyped dataType) { return dataType.OrclType.BuildDataTypeFullName(dataType).Equals(DataTypeFull); }
        public string CSharpOracleDbType { get => CSharp.ORACLEDBTYPE_REF_CURSOR; }
        public string CSharpOdpNetType { get => CSharp.ODP_NET_SAFE_TYPE_REF_CURSOR; }
        public bool IsIgnoredAsParameter { get => false; }
        public string IgnoredReasonAsParameter { get => String.Empty; }
        public bool IsIgnoredAsAttribute { get => false; }
        public string IgnoredReasonAsAttribute { get => String.Empty; }

        internal TranslaterRefCursorUntyped(string dataTypeFull, string cSharpType, ITyped dbDataType) {
            DataTypeFull = dataTypeFull;
            CSharpType = cSharpType;
            SubCSharpType = TranslaterName.ConvertToCamel((dbDataType.DataTypeLabel ?? Orcl.RETURN) + "_UNTYPED");
        }
        private TranslaterRefCursorUntyped() { }

        public override string ToString() { return DataTypeFull; }
    }
}