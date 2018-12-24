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
//    along with this program. If not, see<http://www.gnu.org/licenses/>.
//------------------------------------------------------------------------------

using System;

namespace Odapter {
    internal sealed class OrclBinaryDouble : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.BINARY_DOUBLE; }
        public bool IsImplementedForAssociativeArray { get => true; }
    }

    internal sealed class OrclBinaryFloat : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.BINARY_FLOAT; }
        public bool IsImplementedForAssociativeArray { get => true; }
    }

    internal sealed class OrclBinaryInteger : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.BINARY_INTEGER; }
        public bool IsImplementedForAssociativeArray { get => false; }
    }

    internal sealed class OrclDecimal : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.DECIMAL; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclDecimal() : base(new NormalizableDecimal()) { }
    }

    internal sealed class OrclDoublePrecision : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.DOUBLE_PRECISION; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclDoublePrecision() : base(new NormalizableFloat126()) { }
    }

    internal class OrclFloat : OrclTypeBase, IOrclType {
        public virtual string DataType { get => Orcl.FLOAT; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclFloat() : base(new NormalizableFloat126()) { }
    }

    internal sealed class OrclInteger : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.INTEGER; }
        public bool IsImplementedForAssociativeArray { get => true; }
    }

    internal sealed class OrclNatural : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.NATURAL; }
        public bool IsImplementedForAssociativeArray { get => false; }
    }

    internal sealed class OrclNaturaln : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.NATURALN; }
        public bool IsImplementedForAssociativeArray { get => false; }
    }

    internal sealed class OrclNumber : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.NUMBER; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclNumber() : base(new NormalizableDecimal()) { }
    }

    internal sealed class OrclNumeric : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.NUMERIC; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclNumeric() : base(new NormalizableDecimal()) { }
    }

    internal sealed class OrclPlsInteger : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.PLS_INTEGER; }
        public bool IsImplementedForAssociativeArray { get => true; }
    }

    internal sealed class OrclPositive : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.POSITIVE; }
        public bool IsImplementedForAssociativeArray { get => false; }
    }

    internal sealed class OrclPositiven : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.POSITIVEN; }
        public bool IsImplementedForAssociativeArray { get => false; }
    }

    internal sealed class OrclReal : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.REAL; }
        public bool IsImplementedForAssociativeArray { get => true; }
        public OrclReal() : base(new NormalizableFloat63()) { }
    }

    internal sealed class OrclSmallint : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.SMALLINT; }
        public bool IsImplementedForAssociativeArray { get => true; }
    }
}