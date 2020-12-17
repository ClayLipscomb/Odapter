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
    internal sealed class OrclChar : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.CHAR; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclChar() : base(new NormalizableCharacter()) { }
    }

    internal sealed class OrclLong : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.LONG; }
        public bool IsImplementedForAssociativeArray { get => false; }
        public OrclLong() : base(new NormalizableCharacter()) { }
    }

    internal sealed class OrclNchar : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.NCHAR; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclNchar() : base(new NormalizableCharacter()) { }
    }

    internal sealed class OrclNvarchar2 : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.NVARCHAR2; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclNvarchar2() : base(new NormalizableCharacter()) { }
    }

    internal sealed class OrclString : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.STRING; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclString() : base(new NormalizableCharacter()) { }
    }

    internal sealed class OrclVarchar : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.VARCHAR; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclVarchar() : base(new NormalizableCharacter()) { }
    }

    internal sealed class OrclVarchar2 : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.VARCHAR2; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclVarchar2() : base(new NormalizableCharacter()) { }
    }
}