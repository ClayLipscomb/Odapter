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
//    along with this program.If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;

namespace Odapter {
    /// <summary>
    ///  Field for package record type
    /// </summary>
    internal sealed class Field : IField {
        // standard attribute properties
        public string EntityName { get; set; }
        public string AttrName { get { return Name; } set { Name = value; } }
        public string DataType { get; set; }
        public string AttrTypeOwner { get { return TypeOwner; } set { TypeOwner = value; } }
        public string AttrTypeMod { get; set; }
        public int? DataLength { get; set; }
        public int? DataPrecision { get; set; }
        public int? DataScale { get; set; }
        public int? CharLength { get; set; }
        public int Position { get { return MapPosition; } set { MapPosition = value; } }
        public bool Nullable { get { return true; } }
        public string ContainerClassName { get; set; } // Container class if C# type is nested class

        // field specific
        public string Name { get; set; }
        public int MapPosition { get; set; }
        public int CompareTo(IField f) { return Name.CompareTo(f.Name); }
        public string TypeOwner { get; set; }
        public IField SubField { get; set; }

        // ITyped specific
        public IOrclType OrclType { get; set; }
        public string PreNormalizedValues { get; set; }
        public string Aggregated { get => OrclUtil.BuildAggregateType(this); }
        public string PlsType { get; set; }

        // ITypedNameable specific
        public string DataTypeLabel { get => AttrName; }
        public string DataTypeProperName { get => OrclUtil.IsExistsType(DataType) ? String.Empty : DataType; }
        public string ContainerType { get => String.Empty; }
        public bool IsDefinedExternally { get => DataType.Equals(Orcl.OBJECT); }
        public string NamingHelpValue { get; }

        public ITyped SubType { get => SubField; }
        public ITranslaterType Translater { get; set; }
    }
}