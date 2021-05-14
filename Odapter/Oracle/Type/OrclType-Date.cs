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

    internal sealed class OrclDate : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.DATE; }
        public bool IsImplementedForAssociativeArray { get => true; }
    }

    internal sealed class OrclIntervalDayToSecond : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.INTERVAL_DAY_TO_SECOND; }
        public bool IsImplementedForAssociativeArray { get => false; }
    }

    internal sealed class OrclIntervalYearToMonth : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.INTERVAL_YEAR_TO_MONTH; }
        public bool IsImplementedForAssociativeArray { get => false; }
    }

    internal sealed class OrclTimestamp : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.TIMESTAMP; }
        public bool IsImplementedForAssociativeArray { get => false; }
        public OrclTimestamp() : base(new NormalizableTimestamp()) { }
    }

    internal sealed class OrclTimestampLTZ : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.TIMESTAMP_WITH_LOCAL_TIME_ZONE; }
        public bool IsImplementedForAssociativeArray { get => false; }
        public OrclTimestampLTZ() : base(new NormalizableTimestamp()) { }
    }

    internal sealed class OrclTimestampTZ : OrclTypeBase, IOrclType {
        public string DataType { get => Orcl.TIMESTAMP_WITH_TIME_ZONE; }
        public bool IsImplementedForAssociativeArray { get => false; }
        public OrclTimestampTZ() : base(new NormalizableTimestamp()) { }
    }
}