//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by Odapter 2.01.
//     Direct edits will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Oracle.ManagedDataAccess.Types;
using Schema.Odpt.Odpt.Type.Object;

namespace Schema.Odpt.Odpt.View {
    public interface IOdptViewBigV {
        Int64? Id { set; }
        Int64? ColNumberId { set; }
        Int64? ColInteger { set; }
        Int64? ColInt { set; }
        Int64? ColSmallint { set; }
        Int64? ColNumeric { set; }
        Int64? ColDecimal { set; }
        Decimal? ColNumber { set; }
        Decimal? ColDoublePrecision { set; }
        Decimal? ColFloat { set; }
        Decimal? ColReal { set; }
        Single? ColBinaryFloat { set; }
        Double? ColBinaryDouble { set; }
        String ColVarcharMin { set; }
        String ColVarcharMax { set; }
        String ColVarchar2Min { set; }
        String ColVarchar2Max { set; }
        String ColNvarchar2Min { set; }
        String ColNvarchar2Max { set; }
        String ColCharMin { set; }
        String ColCharMax { set; }
        String ColNcharMin { set; }
        String ColNcharMax { set; }
        DateTime? ColDate { set; }
        DateTime? ColTimestamp { set; }
        DateTime? ColTimestampPrec0 { set; }
        DateTime? ColTimestampPrec9 { set; }
        Byte[] ColBlob { set; }
        String ColClob { set; }
        String ColNclob { set; }
        String ColLast { set; }
    } // IOdptViewBigV
} // Schema.Odpt.Odpt.View