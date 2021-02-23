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
    internal sealed class OrclAnydata : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.ANYDATA; }
        public bool IsImplementedForAssociativeArray { get => false; }
        public OrclAnydata() : base(new NormalizableDefault()) { }
    }
    internal sealed class OrclAnydataset : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.ANYDATASET; }
        public bool IsImplementedForAssociativeArray { get => false; }
        public OrclAnydataset() : base(new NormalizableDefault()) { }
    }
    internal sealed class OrclAnytype : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.ANYTYPE; }
        public bool IsImplementedForAssociativeArray { get => false; }
        public OrclAnytype() : base(new NormalizableDefault()) { }
    }
}