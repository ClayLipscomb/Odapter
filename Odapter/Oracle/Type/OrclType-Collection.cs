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
    internal sealed class OrclAssociativeArray : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.ASSOCIATITVE_ARRAY; }
        public bool IsImplementedForAssociativeArray { get => false; }

        public override string BuildDataTypeFullName(ITyped dbDataType) {
            return String.Join(" ", new string[] { Orcl.ASSOCIATITVE_ARRAY, Orcl.OF, dbDataType.SubType.Aggregated });
        }
    }

    internal sealed class OrclNestedTable : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.NESTED_TABLE; }
        public bool IsImplementedForAssociativeArray { get => false; }
    }

    internal sealed class OrclVarray : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.VARRAY; }
        public bool IsImplementedForAssociativeArray { get => false; }
    }
}