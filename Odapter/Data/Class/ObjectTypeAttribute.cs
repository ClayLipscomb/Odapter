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
    /// <summary>
    /// Attribute of object type
    /// </summary>
    internal sealed class ObjectTypeAttribute : IObjectTypeAttribute {
        // standard attribute properties
        public string EntityName { get { return typeName; } set { typeName = value; } } private string typeName { get; set; }
        public string AttrName { get; set; }
        public string DataType { get => attrTypeName; set { attrTypeName = value; } } private string attrTypeName { get; set; }
        public string AttrTypeOwner { get; set; }
        public string AttrTypeMod { get; set; }
        public int? DataPrecision { get { return precision; } set { precision = value; } } private int? precision { get; set; }
        public int? DataScale { get { return scale; } set { scale = value; } } private int? scale { get; set; }
        public int? CharLength { get { return length; } set { length = value; } } private int? length { get; set; }
        public int Position { get { return attrNo; } set { attrNo = value; } } private int attrNo { get; set; }
        public bool Nullable { get { return true; } }
        public string ContainerClassName { get; set; } // Container class if C# type is nested class

        // ITyped specifc
        public IOrclType OrclType { get; set; }
        public string PreNormalizedValues { get; set; }
        public string Aggregated { get => OrclUtil.BuildAggregateType(this); }
        public string PlsType { get; set; }
        public string Typecode { get; set; }

        // ITypedNameable specific
        public string DataTypeLabel { get => AttrName; }
        public string DataTypeProperName { get => OrclUtil.GetDataTypeProperName(this); }
        public string ContainerType { get => String.Empty; }
        public bool IsDefinedExternally { get => DataType.Equals(Orcl.OBJECT); }
        public string NamingHelpValue { get; }

        public ITyped SubType { get; }
        public ITranslaterType Translater { get; set; }
    }
}