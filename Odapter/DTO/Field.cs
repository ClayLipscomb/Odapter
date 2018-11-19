//------------------------------------------------------------------------------
//    Odapter - a C# code generator for Oracle packages
//    Copyright(C) 2018 Clay Lipscomb
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
    ///  Field for package record type
    /// </summary>
    internal class Field : IField {
        // standard entity properties
        public string EntityName { get { return Name; } set { Name = value; } }
        public string AttrName { get { return Name; } set { Name = value; } }
        public string AttrType { get { return DataType; } set { DataType = value; } }
        public string AttrTypeOwner { get { return TypeOwner; } set { TypeOwner = value; } }
        public string AttrTypeMod { get; set; }
        public int? Length { get { return DataLength; } set { DataLength = value; } }
        public int? Precision { get { return DataPrecision; } set { DataPrecision = value; } }
        public int? Scale { get { return DataScale; } set { DataScale = value; } }
        public int Position { get { return MapPosition; } set { MapPosition = value; } }
        public string CSharpType { get; set; }
        public bool Nullable { get { return true; } }
        public string ContainerClassName { get; set; } // Container class if C# type is nested class

        // field specific
        public string Name { get; set; }
        public int MapPosition { get; set; }
        public int CompareTo(IField f) { return Name.CompareTo(f.Name); }
        public string DataType { get; set; }
        public string TypeOwner { get; set; }
        public int? DataLength { get; set; }
        public int? DataPrecision { get; set; }
        public int? DataScale { get; set; }
    }
}
