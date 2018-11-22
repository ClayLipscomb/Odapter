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
    /// Column for a table or view
    /// </summary>
    internal class Column : IColumn {
        // standard attribute properties
        public string EntityName { get { return tableName; } set { tableName = value; } } private string tableName { get; set; }
        public string AttrName { get { return columnName; } set { columnName = value; } } private string columnName { get; set; }
        public string DataType { get { return dataType; } set { dataType = value; } } private string dataType { get; set; }
        public string AttrTypeOwner { get { return dataTypeOwner; } set { dataTypeOwner = value; } } private string dataTypeOwner { get; set; }
        public string AttrTypeMod { get { return dataTypeMod; } set { dataTypeMod = value; } } private string dataTypeMod { get; set; }
        public int? DataLength { get { return dataLength; } set { dataLength = value; } } private int? dataLength { get; set; }
        public int? DataPrecision { get { return dataPrecision; } set { dataPrecision = value; } } private int? dataPrecision { get; set; }
        public int? DataScale { get { return dataScale; } set { dataScale = value; } } private int? dataScale { get; set; }
        public int Position { get { return columnId; } set { columnId = value; } } private int columnId { get; set; }
        public bool Nullable { get { return (nullable == "Y" ? true : false); } } private string nullable { get; set; }
        public string CSharpType { get; set; }
        public string ContainerClassName { get; set; } // Container class if C# type is nested class

        // column specific 
        public int CharLength { get; set; }

        // IDatabaseDataType specifc
        public string PlsType { get; set; }
    }
}
