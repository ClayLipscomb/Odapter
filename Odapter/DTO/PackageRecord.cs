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
    /// Package record type as type of Entity
    /// </summary>
    internal class PackageRecord : Entity, IEntity, IComparable<PackageRecord> {
        // standard entity properties
        public string EntityName { get { return CSharpType; } set { CSharpType = value; } }
        public string AncestorTypeName { get; set; }
        public bool Instantiable { get { return true; } }
        public String CSharpType { get; set; }

        // record specific

        /// <summary>
        /// Package containing argument from which record is derived
        /// </summary>
        public string PackageName { get; set; }

        /// <summary>
        /// Package name of record definition; could be different from package with argument using it
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Name of record
        /// </summary>
        public String SubName { get; set; }

        /// <summary>
        /// Schema of record defintion; could be different from schema with argument using it
        /// </summary>
        public String Owner { get; set; }

        public int CompareTo(PackageRecord r) { return CSharpType.CompareTo(r.CSharpType); }
    }
}
