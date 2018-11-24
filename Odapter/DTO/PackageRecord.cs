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
    /// Package record type as type of Entity
    /// </summary>
    internal class PackageRecord : EntityBase, IPackageRecord {

        public string EntityName { get { return SubName; } set { SubName = value; } }   // sub_name is underlying sys view column
        public bool IsInstantiable { get => false; }  // a translated package record should not be instantiable

        // IPackageRecord specific
        public string PackageName { get; set; }
        public string Name { get; set; }
        public string SubName { get; set; }
        public int CompareTo(IPackageRecord r) { return CSharpType.CompareTo(r.CSharpType); }
    }
}
