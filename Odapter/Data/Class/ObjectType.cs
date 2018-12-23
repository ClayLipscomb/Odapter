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

using System;

namespace Odapter {
    /// <summary>
    /// Object Type as type of Entity
    /// </summary>
    internal sealed class ObjectType : EntityBase, IObjectType {
        public IOrclEntity OrclEntity { get => new OrclObjectEntity(); }
        public string EntityType { get => OrclEntity.EntityType; }

        public string EntityName { get { return typeName; } set { typeName = value; } } private string typeName { get; set; }  // type_name is underlying sys view column
        public bool IsInstantiable { get { return (instantiable == Orcl.YES ? true : false); } } private string instantiable { get; set; }   // sys view value is YES or NO
        public ITranslaterEntity Translater { get; set; }
        public string DbAncestorTypeName { get { return supertypeName; } set { supertypeName = value; } }  private string supertypeName { get; set; } // super_type_name is underlying sys view column

        // IEntityNameable specific
        public string ContainerType { get => String.Empty; }
        public bool IsDefinedExternally { get => false; }

        public override string ToString() { return EntityName; }
    }
}