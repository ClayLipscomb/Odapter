using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Odapter {
    internal interface IField : IComparable<IField>, IEntityAttribute {

        // field specific
        String Name { get; set; }
        int MapPosition { get; set; }
        //int CompareTo(IField f);
        String DataType { get; set; }
        String TypeOwner { get; set; }
        int? DataLength { get; set; }
        int? DataPrecision { get; set; }
        int? DataScale { get; set; }
    }
}
