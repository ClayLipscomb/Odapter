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
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

namespace Odapter {
    /// <summary>
    /// Attribute of object type
    /// </summary>
    internal class ObjectTypeAttribute : IObjectTypeAttribute {
        // standard attribute properties
        public string EntityName { get { return typeName; } set { typeName = value; } } private string typeName { get; set; }
        public string AttrName { get; set; }
        public string DataType { get { return attrTypeName; }  set { attrTypeName = value; } }  private string attrTypeName { get; set; }
        public string AttrTypeOwner { get; set; }
        public string AttrTypeMod { get; set; }
        public int? DataLength { get { return length; } set { length = value; } } private int? length { get; set; }
        public int? DataPrecision { get { return precision; } set { precision = value; } } private int? precision { get; set; }
        public int? DataScale { get { return scale; } set { scale = value; } } private int? scale { get; set; }
        public int Position { get { return attrNo; } set { attrNo = value; } } private int attrNo { get; set; }
        public bool Nullable { get { return true; } }
        public string CSharpType { get; set; }
        public string ContainerClassName { get; set; } // Container class if C# type is nested class

        // IDatabaseDataType specifc
        public string PlsType { get; set; }
    }
}
