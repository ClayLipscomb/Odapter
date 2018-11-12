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

using System;

namespace Odapter {
    /// <summary>
    /// Column for a table or view
    /// </summary>
    internal class Column : IEntityAttribute {
        // standard attribute properties
        public string EntityName { get { return tableName; } set { tableName = value; } } private string tableName { get; set; }
        public string AttrName { get { return columnName; } set { columnName = value; } } private string columnName { get; set; }
        public string AttrType { get { return dataType; } set { dataType = value; } } private string dataType { get; set; }
        public string AttrTypeOwner { get { return dataTypeOwner; } set { dataTypeOwner = value; } } private string dataTypeOwner { get; set; }
        public string AttrTypeMod { get { return dataTypeMod; } set { dataTypeMod = value; } } private string dataTypeMod { get; set; }
        public int? Length { get { return dataLength; } set { dataLength = value; } } private int? dataLength { get; set; }
        public int? Precision { get { return dataPrecision; } set { dataPrecision = value; } } private int? dataPrecision { get; set; }
        public int? Scale { get { return dataScale; } set { dataScale = value; } } private int? dataScale { get; set; }
        public int Position { get { return columnId; } set { columnId = value; } } private int columnId { get; set; }
        public bool Nullable { get { return (nullable == "Y" ? true : false); } } private string nullable { get; set; }
        public string CSharpType { get; set; }
        public String ContainerClassName { get; set; } // Container class if C# type is nested class

        // column specific properties
        public int CharLength { get; set; }
    }
}
