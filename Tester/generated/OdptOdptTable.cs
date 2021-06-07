//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by Odapter 2.0.
//     Direct edits will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Oracle.ManagedDataAccess.Types;
using Schema.Odpt.Odpt.Type.Object;

namespace Schema.Odpt.Odpt.Table {
    public interface IOdptLogs {
        Int64? LogId { set; }
        DateTime? LogDt { set; }
        String UserName { set; }
        SByte? Severity { set; }
        String Interface { set; }
        String Source { set; }
        String Msg { set; }
    } // IOdptLogs

    public interface IOdptTableBig {
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
        DateTimeOffset? ColTimestampTZ { set; }
        DateTimeOffset? ColTimestampTZPrec0 { set; }
        DateTimeOffset? ColTimestampTZPrec9 { set; }
        DateTime? ColTimestampLTZ { set; }
        DateTime? ColTimestampLTZPrec0 { set; }
        DateTime? ColTimestampLTZPrec9 { set; }
        Byte[] ColBlob { set; }
        String ColClob { set; }
        String ColNclob { set; }
        String Abstract { set; }
        String Base { set; }
        String Void { set; }
        String Class { set; }
        String Namespace { set; }
        String Readonly { set; }
        String Partial { set; }
        String Const { set; }
        String Using { set; }
        String ColLast { set; }
    } // IOdptTableBig

    public interface IOdptTableCsKeyword {
        String Abstract { set; }
        String Event { set; }
        String New { set; }
        String Struct { set; }
        String As { set; }
        String Explicit { set; }
        String Null { set; }
        String Switch { set; }
        String Base { set; }
        String Extern { set; }
        String Object { set; }
        String This { set; }
        String Bool { set; }
        String False { set; }
        String Operator { set; }
        String Throw { set; }
        String Break { set; }
        String Finally { set; }
        String Out { set; }
        String True { set; }
        String Byte { set; }
        String Fixed { set; }
        String Override { set; }
        String Try { set; }
        String Case { set; }
        String Float { set; }
        String Params { set; }
        String Typeof { set; }
        String Catch { set; }
        String For { set; }
        String Private { set; }
        String Uint { set; }
        String Char { set; }
        String Foreach { set; }
        String Protected { set; }
        String Ulong { set; }
        String Checked { set; }
        String Goto { set; }
        String Public { set; }
        String Unchecked { set; }
        String Class { set; }
        String If { set; }
        String Readonly { set; }
        String Unsafe { set; }
        String Const { set; }
        String Implicit { set; }
        String Ref { set; }
        String Ushort { set; }
        String Continue { set; }
        String In { set; }
        String Return { set; }
        String Using { set; }
        String Decimal { set; }
        String Int { set; }
        String Sbyte { set; }
        String Virtual { set; }
        String Default { set; }
        String Interface { set; }
        String Sealed { set; }
        String Volatile { set; }
        String Delegate { set; }
        String Internal { set; }
        String Short { set; }
        String Void { set; }
        String Do { set; }
        String Is { set; }
        String Sizeof { set; }
        String While { set; }
        String Double { set; }
        String Lock { set; }
        String Stackalloc { set; }
        String Else { set; }
        String Long { set; }
        String Static { set; }
        String Enum { set; }
        String Namespace { set; }
        String String { set; }
        String Dynamic { set; }
        String Get { set; }
        String Let { set; }
        String Partial { set; }
        String Set { set; }
        String Value { set; }
        String Var { set; }
        String Where { set; }
    } // IOdptTableCsKeyword

    // **TABLE IGNORED** - ANYDATA type is not available in ODP.NET managed
    // public interface IOdptTableIgnoreAny {

    public interface IOdptTableNamingConflict {
        Decimal? NamingConflict { set; }
        Decimal? NamingExtraunderscoreConflict { set; }
        Decimal? NamingExtraunderscoreExtraunderscoreConflict { set; }
        Decimal? NamingConflictExtraunderscore { set; }
        Decimal? NamingConflictExtraunderscoreExtraunderscore { set; }
        Decimal? NamingConflict123 { set; }
        Decimal? NamingConflict123Extraunderscore { set; }
        Decimal? NamingConflict123ExtraunderscoreExtraunderscore { set; }
    } // IOdptTableNamingConflict

    public interface IOdptTableNumber {
        Int64? Id { set; }
        Decimal? ColNumber { set; }
        SByte? ColNumber1 { set; }
        SByte? ColNumber2 { set; }
        Int16? ColNumber3 { set; }
        Int16? ColNumber4 { set; }
        Int32? ColNumber5 { set; }
        Int32? ColNumber6 { set; }
        Int32? ColNumber7 { set; }
        Int32? ColNumber8 { set; }
        Int32? ColNumber9 { set; }
        Int64? ColNumber10 { set; }
        Int64? ColNumber18 { set; }
        Int64? ColNumber19 { set; }
        Int64? ColNumber38 { set; }
        SByte? ColNumber1Scale0 { set; }
        SByte? ColNumber2Scale0 { set; }
        Int16? ColNumber3Scale0 { set; }
        Int16? ColNumber4Scale0 { set; }
        Int32? ColNumber5Scale0 { set; }
        Int32? ColNumber9Scale0 { set; }
        Int64? ColNumber10Scale0 { set; }
        Int64? ColNumber18Scale0 { set; }
        Int64? ColNumber19Scale0 { set; }
        Int64? ColNumber28Scale0 { set; }
        Int64? ColNumber29Scale0 { set; }
        Int64? ColNumber38Scale0 { set; }
        Decimal? ColNumber2Scale1 { set; }
        Decimal? ColNumber5Scale3 { set; }
        Decimal? ColNumber15Scale11 { set; }
        Decimal? ColNumber31Scale21 { set; }
        Decimal? ColNumber38Scale37 { set; }
        Decimal? ColNumberLast { set; }
    } // IOdptTableNumber

    // **TABLE IGNORED** - Code generation for OBJECT type has not been implemented
    // public interface IOdptTableObject {
} // Schema.Odpt.Odpt.Table