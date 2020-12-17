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
    /// Table as type of Entity
    /// </summary>
    internal sealed class Table : EntityBase, ITable {
        public IOrclEntity OrclEntity { get => new OrclTable(); }
        public string EntityType { get => OrclEntity.EntityType; }
        public ITranslaterEntity Translater { get; set; }
        public bool IsInstantiable { get => true; }  // a translated table is always instantiable

        public string EntityName { get { return tableName; } set { tableName = value; } } private string tableName { get; set; }   // table_name is underlying sys view column

        // IEntityNameable specific
        public string ContainerType { get => String.Empty; }
        public bool IsDefinedExternally { get => false; }

        public override string ToString() { return EntityName; }
    }
}