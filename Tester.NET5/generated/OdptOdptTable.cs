//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by Odapter 2.0.
//     Direct edits will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Types;
using Schema.Odpt.Odpt.Type.Object;

namespace Schema.Odpt.Odpt.Table {
    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public partial class OdptLogs : Schema.Odpt.Odpt.OdptTable {
        private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
        [DataMember(Order=1, IsRequired=true)][XmlElement(Order=1, IsNullable=true)]
        public virtual Int64? LogId { get; set; }
        [DataMember(Order=2, IsRequired=true)][XmlElement(Order=2, IsNullable=true)]
        public virtual DateTime? LogDt { get; set; }
        [DataMember(Order=3, IsRequired=true)][XmlElement(Order=3, IsNullable=true)]
        public virtual String UserName { get; set; }
        [DataMember(Order=4, IsRequired=true)][XmlElement(Order=4, IsNullable=true)]
        public virtual SByte? Severity { get; set; }
        [DataMember(Order=5, IsRequired=false)][XmlElement(Order=5, IsNullable=true)]
        public virtual String Interface { get; set; }
        [DataMember(Order=6, IsRequired=true)][XmlElement(Order=6, IsNullable=true)]
        public virtual String Source { get; set; }
        [DataMember(Order=7, IsRequired=true)][XmlElement(Order=7, IsNullable=true)]
        public virtual String Msg { get; set; }
    } // OdptLogs

    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public partial class OdptTableBig : Schema.Odpt.Odpt.OdptTable {
        private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
        [DataMember(Order=1, IsRequired=true)][XmlElement(Order=1, IsNullable=true)]
        public virtual Int64? Id { get; set; }
        [DataMember(Order=2, IsRequired=false)][XmlElement(Order=2, IsNullable=true)]
        public virtual Int64? ColNumberId { get; set; }
        [DataMember(Order=3, IsRequired=false)][XmlElement(Order=3, IsNullable=true)]
        public virtual Int64? ColInteger { get; set; }
        [DataMember(Order=4, IsRequired=false)][XmlElement(Order=4, IsNullable=true)]
        public virtual Int64? ColInt { get; set; }
        [DataMember(Order=5, IsRequired=false)][XmlElement(Order=5, IsNullable=true)]
        public virtual Int64? ColSmallint { get; set; }
        [DataMember(Order=6, IsRequired=false)][XmlElement(Order=6, IsNullable=true)]
        public virtual Int64? ColNumeric { get; set; }
        [DataMember(Order=7, IsRequired=false)][XmlElement(Order=7, IsNullable=true)]
        public virtual Int64? ColDecimal { get; set; }
        [DataMember(Order=8, IsRequired=false)][XmlElement(Order=8, IsNullable=true)]
        public virtual Decimal? ColNumber { get; set; }
        [DataMember(Order=9, IsRequired=false)][XmlElement(Order=9, IsNullable=true)]
        public virtual Decimal? ColDoublePrecision { get; set; }
        [DataMember(Order=10, IsRequired=false)][XmlElement(Order=10, IsNullable=true)]
        public virtual Decimal? ColFloat { get; set; }
        [DataMember(Order=11, IsRequired=false)][XmlElement(Order=11, IsNullable=true)]
        public virtual Decimal? ColReal { get; set; }
        [DataMember(Order=12, IsRequired=false)][XmlElement(Order=12, IsNullable=true)]
        public virtual Single? ColBinaryFloat { get; set; }
        [DataMember(Order=13, IsRequired=false)][XmlElement(Order=13, IsNullable=true)]
        public virtual Double? ColBinaryDouble { get; set; }
        [DataMember(Order=14, IsRequired=false)][XmlElement(Order=14, IsNullable=true)]
        public virtual String ColVarcharMin { get; set; }
        [DataMember(Order=15, IsRequired=false)][XmlElement(Order=15, IsNullable=true)]
        public virtual String ColVarcharMax { get; set; }
        [DataMember(Order=16, IsRequired=false)][XmlElement(Order=16, IsNullable=true)]
        public virtual String ColVarchar2Min { get; set; }
        [DataMember(Order=17, IsRequired=false)][XmlElement(Order=17, IsNullable=true)]
        public virtual String ColVarchar2Max { get; set; }
        [DataMember(Order=18, IsRequired=false)][XmlElement(Order=18, IsNullable=true)]
        public virtual String ColNvarchar2Min { get; set; }
        [DataMember(Order=19, IsRequired=false)][XmlElement(Order=19, IsNullable=true)]
        public virtual String ColNvarchar2Max { get; set; }
        [DataMember(Order=20, IsRequired=false)][XmlElement(Order=20, IsNullable=true)]
        public virtual String ColCharMin { get; set; }
        [DataMember(Order=21, IsRequired=false)][XmlElement(Order=21, IsNullable=true)]
        public virtual String ColCharMax { get; set; }
        [DataMember(Order=22, IsRequired=false)][XmlElement(Order=22, IsNullable=true)]
        public virtual String ColNcharMin { get; set; }
        [DataMember(Order=23, IsRequired=false)][XmlElement(Order=23, IsNullable=true)]
        public virtual String ColNcharMax { get; set; }
        [DataMember(Order=24, IsRequired=false)][XmlElement(Order=24, IsNullable=true)]
        public virtual DateTime? ColDate { get; set; }
        [DataMember(Order=25, IsRequired=false)][XmlElement(Order=25, IsNullable=true)]
        public virtual DateTime? ColTimestamp { get; set; }
        [DataMember(Order=26, IsRequired=false)][XmlElement(Order=26, IsNullable=true)]
        public virtual DateTime? ColTimestampPrec0 { get; set; }
        [DataMember(Order=27, IsRequired=false)][XmlElement(Order=27, IsNullable=true)]
        public virtual DateTime? ColTimestampPrec9 { get; set; }
        [DataMember(Order=28, IsRequired=false)][XmlElement(Order=28, IsNullable=true)]
        public virtual DateTimeOffset? ColTimestampTZ { get; set; }
        [DataMember(Order=29, IsRequired=false)][XmlElement(Order=29, IsNullable=true)]
        public virtual DateTimeOffset? ColTimestampTZPrec0 { get; set; }
        [DataMember(Order=30, IsRequired=false)][XmlElement(Order=30, IsNullable=true)]
        public virtual DateTimeOffset? ColTimestampTZPrec9 { get; set; }
        [DataMember(Order=31, IsRequired=false)][XmlElement(Order=31, IsNullable=true)]
        public virtual DateTime? ColTimestampLTZ { get; set; }
        [DataMember(Order=32, IsRequired=false)][XmlElement(Order=32, IsNullable=true)]
        public virtual DateTime? ColTimestampLTZPrec0 { get; set; }
        [DataMember(Order=33, IsRequired=false)][XmlElement(Order=33, IsNullable=true)]
        public virtual DateTime? ColTimestampLTZPrec9 { get; set; }
        [DataMember(Order=34, IsRequired=false)][XmlElement(Order=34, IsNullable=true)]
        public virtual Byte[] ColBlob { get; set; }
        [DataMember(Order=35, IsRequired=false)][XmlElement(Order=35, IsNullable=true)]
        public virtual String ColClob { get; set; }
        [DataMember(Order=36, IsRequired=false)][XmlElement(Order=36, IsNullable=true)]
        public virtual String ColNclob { get; set; }
        [DataMember(Order=37, IsRequired=false)][XmlElement(Order=37, IsNullable=true)]
        public virtual String Abstract { get; set; }
        [DataMember(Order=38, IsRequired=false)][XmlElement(Order=38, IsNullable=true)]
        public virtual String Base { get; set; }
        [DataMember(Order=39, IsRequired=false)][XmlElement(Order=39, IsNullable=true)]
        public virtual String Void { get; set; }
        [DataMember(Order=40, IsRequired=false)][XmlElement(Order=40, IsNullable=true)]
        public virtual String Class { get; set; }
        [DataMember(Order=41, IsRequired=false)][XmlElement(Order=41, IsNullable=true)]
        public virtual String Namespace { get; set; }
        [DataMember(Order=42, IsRequired=false)][XmlElement(Order=42, IsNullable=true)]
        public virtual String Readonly { get; set; }
        [DataMember(Order=43, IsRequired=false)][XmlElement(Order=43, IsNullable=true)]
        public virtual String Partial { get; set; }
        [DataMember(Order=44, IsRequired=false)][XmlElement(Order=44, IsNullable=true)]
        public virtual String Const { get; set; }
        [DataMember(Order=45, IsRequired=false)][XmlElement(Order=45, IsNullable=true)]
        public virtual String Using { get; set; }
        [DataMember(Order=46, IsRequired=false)][XmlElement(Order=46, IsNullable=true)]
        public virtual String ColLast { get; set; }
    } // OdptTableBig

    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public partial class OdptTableCsKeyword : Schema.Odpt.Odpt.OdptTable {
        private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
        [DataMember(Order=1, IsRequired=false)][XmlElement(Order=1, IsNullable=true)]
        public virtual String Abstract { get; set; }
        [DataMember(Order=2, IsRequired=false)][XmlElement(Order=2, IsNullable=true)]
        public virtual String Event { get; set; }
        [DataMember(Order=3, IsRequired=false)][XmlElement(Order=3, IsNullable=true)]
        public virtual String New { get; set; }
        [DataMember(Order=4, IsRequired=false)][XmlElement(Order=4, IsNullable=true)]
        public virtual String Struct { get; set; }
        [DataMember(Order=5, IsRequired=false)][XmlElement(Order=5, IsNullable=true)]
        public virtual String As { get; set; }
        [DataMember(Order=6, IsRequired=false)][XmlElement(Order=6, IsNullable=true)]
        public virtual String Explicit { get; set; }
        [DataMember(Order=7, IsRequired=false)][XmlElement(Order=7, IsNullable=true)]
        public virtual String Null { get; set; }
        [DataMember(Order=8, IsRequired=false)][XmlElement(Order=8, IsNullable=true)]
        public virtual String Switch { get; set; }
        [DataMember(Order=9, IsRequired=false)][XmlElement(Order=9, IsNullable=true)]
        public virtual String Base { get; set; }
        [DataMember(Order=10, IsRequired=false)][XmlElement(Order=10, IsNullable=true)]
        public virtual String Extern { get; set; }
        [DataMember(Order=11, IsRequired=false)][XmlElement(Order=11, IsNullable=true)]
        public virtual String Object { get; set; }
        [DataMember(Order=12, IsRequired=false)][XmlElement(Order=12, IsNullable=true)]
        public virtual String This { get; set; }
        [DataMember(Order=13, IsRequired=false)][XmlElement(Order=13, IsNullable=true)]
        public virtual String Bool { get; set; }
        [DataMember(Order=14, IsRequired=false)][XmlElement(Order=14, IsNullable=true)]
        public virtual String False { get; set; }
        [DataMember(Order=15, IsRequired=false)][XmlElement(Order=15, IsNullable=true)]
        public virtual String Operator { get; set; }
        [DataMember(Order=16, IsRequired=false)][XmlElement(Order=16, IsNullable=true)]
        public virtual String Throw { get; set; }
        [DataMember(Order=17, IsRequired=false)][XmlElement(Order=17, IsNullable=true)]
        public virtual String Break { get; set; }
        [DataMember(Order=18, IsRequired=false)][XmlElement(Order=18, IsNullable=true)]
        public virtual String Finally { get; set; }
        [DataMember(Order=19, IsRequired=false)][XmlElement(Order=19, IsNullable=true)]
        public virtual String Out { get; set; }
        [DataMember(Order=20, IsRequired=false)][XmlElement(Order=20, IsNullable=true)]
        public virtual String True { get; set; }
        [DataMember(Order=21, IsRequired=false)][XmlElement(Order=21, IsNullable=true)]
        public virtual String Byte { get; set; }
        [DataMember(Order=22, IsRequired=false)][XmlElement(Order=22, IsNullable=true)]
        public virtual String Fixed { get; set; }
        [DataMember(Order=23, IsRequired=false)][XmlElement(Order=23, IsNullable=true)]
        public virtual String Override { get; set; }
        [DataMember(Order=24, IsRequired=false)][XmlElement(Order=24, IsNullable=true)]
        public virtual String Try { get; set; }
        [DataMember(Order=25, IsRequired=false)][XmlElement(Order=25, IsNullable=true)]
        public virtual String Case { get; set; }
        [DataMember(Order=26, IsRequired=false)][XmlElement(Order=26, IsNullable=true)]
        public virtual String Float { get; set; }
        [DataMember(Order=27, IsRequired=false)][XmlElement(Order=27, IsNullable=true)]
        public virtual String Params { get; set; }
        [DataMember(Order=28, IsRequired=false)][XmlElement(Order=28, IsNullable=true)]
        public virtual String Typeof { get; set; }
        [DataMember(Order=29, IsRequired=false)][XmlElement(Order=29, IsNullable=true)]
        public virtual String Catch { get; set; }
        [DataMember(Order=30, IsRequired=false)][XmlElement(Order=30, IsNullable=true)]
        public virtual String For { get; set; }
        [DataMember(Order=31, IsRequired=false)][XmlElement(Order=31, IsNullable=true)]
        public virtual String Private { get; set; }
        [DataMember(Order=32, IsRequired=false)][XmlElement(Order=32, IsNullable=true)]
        public virtual String Uint { get; set; }
        [DataMember(Order=33, IsRequired=false)][XmlElement(Order=33, IsNullable=true)]
        public virtual String Char { get; set; }
        [DataMember(Order=34, IsRequired=false)][XmlElement(Order=34, IsNullable=true)]
        public virtual String Foreach { get; set; }
        [DataMember(Order=35, IsRequired=false)][XmlElement(Order=35, IsNullable=true)]
        public virtual String Protected { get; set; }
        [DataMember(Order=36, IsRequired=false)][XmlElement(Order=36, IsNullable=true)]
        public virtual String Ulong { get; set; }
        [DataMember(Order=37, IsRequired=false)][XmlElement(Order=37, IsNullable=true)]
        public virtual String Checked { get; set; }
        [DataMember(Order=38, IsRequired=false)][XmlElement(Order=38, IsNullable=true)]
        public virtual String Goto { get; set; }
        [DataMember(Order=39, IsRequired=false)][XmlElement(Order=39, IsNullable=true)]
        public virtual String Public { get; set; }
        [DataMember(Order=40, IsRequired=false)][XmlElement(Order=40, IsNullable=true)]
        public virtual String Unchecked { get; set; }
        [DataMember(Order=41, IsRequired=false)][XmlElement(Order=41, IsNullable=true)]
        public virtual String Class { get; set; }
        [DataMember(Order=42, IsRequired=false)][XmlElement(Order=42, IsNullable=true)]
        public virtual String If { get; set; }
        [DataMember(Order=43, IsRequired=false)][XmlElement(Order=43, IsNullable=true)]
        public virtual String Readonly { get; set; }
        [DataMember(Order=44, IsRequired=false)][XmlElement(Order=44, IsNullable=true)]
        public virtual String Unsafe { get; set; }
        [DataMember(Order=45, IsRequired=false)][XmlElement(Order=45, IsNullable=true)]
        public virtual String Const { get; set; }
        [DataMember(Order=46, IsRequired=false)][XmlElement(Order=46, IsNullable=true)]
        public virtual String Implicit { get; set; }
        [DataMember(Order=47, IsRequired=false)][XmlElement(Order=47, IsNullable=true)]
        public virtual String Ref { get; set; }
        [DataMember(Order=48, IsRequired=false)][XmlElement(Order=48, IsNullable=true)]
        public virtual String Ushort { get; set; }
        [DataMember(Order=49, IsRequired=false)][XmlElement(Order=49, IsNullable=true)]
        public virtual String Continue { get; set; }
        [DataMember(Order=50, IsRequired=false)][XmlElement(Order=50, IsNullable=true)]
        public virtual String In { get; set; }
        [DataMember(Order=51, IsRequired=false)][XmlElement(Order=51, IsNullable=true)]
        public virtual String Return { get; set; }
        [DataMember(Order=52, IsRequired=false)][XmlElement(Order=52, IsNullable=true)]
        public virtual String Using { get; set; }
        [DataMember(Order=53, IsRequired=false)][XmlElement(Order=53, IsNullable=true)]
        public virtual String Decimal { get; set; }
        [DataMember(Order=54, IsRequired=false)][XmlElement(Order=54, IsNullable=true)]
        public virtual String Int { get; set; }
        [DataMember(Order=55, IsRequired=false)][XmlElement(Order=55, IsNullable=true)]
        public virtual String Sbyte { get; set; }
        [DataMember(Order=56, IsRequired=false)][XmlElement(Order=56, IsNullable=true)]
        public virtual String Virtual { get; set; }
        [DataMember(Order=57, IsRequired=false)][XmlElement(Order=57, IsNullable=true)]
        public virtual String Default { get; set; }
        [DataMember(Order=58, IsRequired=false)][XmlElement(Order=58, IsNullable=true)]
        public virtual String Interface { get; set; }
        [DataMember(Order=59, IsRequired=false)][XmlElement(Order=59, IsNullable=true)]
        public virtual String Sealed { get; set; }
        [DataMember(Order=60, IsRequired=false)][XmlElement(Order=60, IsNullable=true)]
        public virtual String Volatile { get; set; }
        [DataMember(Order=61, IsRequired=false)][XmlElement(Order=61, IsNullable=true)]
        public virtual String Delegate { get; set; }
        [DataMember(Order=62, IsRequired=false)][XmlElement(Order=62, IsNullable=true)]
        public virtual String Internal { get; set; }
        [DataMember(Order=63, IsRequired=false)][XmlElement(Order=63, IsNullable=true)]
        public virtual String Short { get; set; }
        [DataMember(Order=64, IsRequired=false)][XmlElement(Order=64, IsNullable=true)]
        public virtual String Void { get; set; }
        [DataMember(Order=65, IsRequired=false)][XmlElement(Order=65, IsNullable=true)]
        public virtual String Do { get; set; }
        [DataMember(Order=66, IsRequired=false)][XmlElement(Order=66, IsNullable=true)]
        public virtual String Is { get; set; }
        [DataMember(Order=67, IsRequired=false)][XmlElement(Order=67, IsNullable=true)]
        public virtual String Sizeof { get; set; }
        [DataMember(Order=68, IsRequired=false)][XmlElement(Order=68, IsNullable=true)]
        public virtual String While { get; set; }
        [DataMember(Order=69, IsRequired=false)][XmlElement(Order=69, IsNullable=true)]
        public virtual String Double { get; set; }
        [DataMember(Order=70, IsRequired=false)][XmlElement(Order=70, IsNullable=true)]
        public virtual String Lock { get; set; }
        [DataMember(Order=71, IsRequired=false)][XmlElement(Order=71, IsNullable=true)]
        public virtual String Stackalloc { get; set; }
        [DataMember(Order=72, IsRequired=false)][XmlElement(Order=72, IsNullable=true)]
        public virtual String Else { get; set; }
        [DataMember(Order=73, IsRequired=false)][XmlElement(Order=73, IsNullable=true)]
        public virtual String Long { get; set; }
        [DataMember(Order=74, IsRequired=false)][XmlElement(Order=74, IsNullable=true)]
        public virtual String Static { get; set; }
        [DataMember(Order=75, IsRequired=false)][XmlElement(Order=75, IsNullable=true)]
        public virtual String Enum { get; set; }
        [DataMember(Order=76, IsRequired=false)][XmlElement(Order=76, IsNullable=true)]
        public virtual String Namespace { get; set; }
        [DataMember(Order=77, IsRequired=false)][XmlElement(Order=77, IsNullable=true)]
        public virtual String String { get; set; }
        [DataMember(Order=78, IsRequired=false)][XmlElement(Order=78, IsNullable=true)]
        public virtual String Dynamic { get; set; }
        [DataMember(Order=79, IsRequired=false)][XmlElement(Order=79, IsNullable=true)]
        public virtual String Get { get; set; }
        [DataMember(Order=80, IsRequired=false)][XmlElement(Order=80, IsNullable=true)]
        public virtual String Let { get; set; }
        [DataMember(Order=81, IsRequired=false)][XmlElement(Order=81, IsNullable=true)]
        public virtual String Partial { get; set; }
        [DataMember(Order=82, IsRequired=false)][XmlElement(Order=82, IsNullable=true)]
        public virtual String Set { get; set; }
        [DataMember(Order=83, IsRequired=false)][XmlElement(Order=83, IsNullable=true)]
        public virtual String Value { get; set; }
        [DataMember(Order=84, IsRequired=false)][XmlElement(Order=84, IsNullable=true)]
        public virtual String Var { get; set; }
        [DataMember(Order=85, IsRequired=false)][XmlElement(Order=85, IsNullable=true)]
        public virtual String Where { get; set; }
    } // OdptTableCsKeyword

    // **TABLE IGNORED** - ANYDATA type is not available in ODP.NET managed
    // public partial class OdptTableIgnoreAny : Schema.Odpt.Odpt.OdptTable {

    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public partial class OdptTableNamingConflict : Schema.Odpt.Odpt.OdptTable {
        private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
        [DataMember(Order=1, IsRequired=false)][XmlElement(Order=1, IsNullable=true)]
        public virtual Decimal? NamingConflict { get; set; }
        [DataMember(Order=2, IsRequired=false)][XmlElement(Order=2, IsNullable=true)]
        public virtual Decimal? NamingExtraunderscoreConflict { get; set; }
        [DataMember(Order=3, IsRequired=false)][XmlElement(Order=3, IsNullable=true)]
        public virtual Decimal? NamingExtraunderscoreExtraunderscoreConflict { get; set; }
        [DataMember(Order=4, IsRequired=false)][XmlElement(Order=4, IsNullable=true)]
        public virtual Decimal? NamingConflictExtraunderscore { get; set; }
        [DataMember(Order=5, IsRequired=false)][XmlElement(Order=5, IsNullable=true)]
        public virtual Decimal? NamingConflictExtraunderscoreExtraunderscore { get; set; }
        [DataMember(Order=6, IsRequired=false)][XmlElement(Order=6, IsNullable=true)]
        public virtual Decimal? NamingConflict123 { get; set; }
        [DataMember(Order=7, IsRequired=false)][XmlElement(Order=7, IsNullable=true)]
        public virtual Decimal? NamingConflict123Extraunderscore { get; set; }
        [DataMember(Order=8, IsRequired=false)][XmlElement(Order=8, IsNullable=true)]
        public virtual Decimal? NamingConflict123ExtraunderscoreExtraunderscore { get; set; }
    } // OdptTableNamingConflict

    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public partial class OdptTableNumber : Schema.Odpt.Odpt.OdptTable {
        private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
        [DataMember(Order=1, IsRequired=true)][XmlElement(Order=1, IsNullable=true)]
        public virtual Int64? Id { get; set; }
        [DataMember(Order=2, IsRequired=false)][XmlElement(Order=2, IsNullable=true)]
        public virtual Decimal? ColNumber { get; set; }
        [DataMember(Order=3, IsRequired=false)][XmlElement(Order=3, IsNullable=true)]
        public virtual SByte? ColNumber1 { get; set; }
        [DataMember(Order=4, IsRequired=false)][XmlElement(Order=4, IsNullable=true)]
        public virtual SByte? ColNumber2 { get; set; }
        [DataMember(Order=5, IsRequired=false)][XmlElement(Order=5, IsNullable=true)]
        public virtual Int16? ColNumber3 { get; set; }
        [DataMember(Order=6, IsRequired=false)][XmlElement(Order=6, IsNullable=true)]
        public virtual Int16? ColNumber4 { get; set; }
        [DataMember(Order=7, IsRequired=false)][XmlElement(Order=7, IsNullable=true)]
        public virtual Int32? ColNumber5 { get; set; }
        [DataMember(Order=8, IsRequired=false)][XmlElement(Order=8, IsNullable=true)]
        public virtual Int32? ColNumber6 { get; set; }
        [DataMember(Order=9, IsRequired=false)][XmlElement(Order=9, IsNullable=true)]
        public virtual Int32? ColNumber7 { get; set; }
        [DataMember(Order=10, IsRequired=false)][XmlElement(Order=10, IsNullable=true)]
        public virtual Int32? ColNumber8 { get; set; }
        [DataMember(Order=11, IsRequired=false)][XmlElement(Order=11, IsNullable=true)]
        public virtual Int32? ColNumber9 { get; set; }
        [DataMember(Order=12, IsRequired=false)][XmlElement(Order=12, IsNullable=true)]
        public virtual Int64? ColNumber10 { get; set; }
        [DataMember(Order=13, IsRequired=false)][XmlElement(Order=13, IsNullable=true)]
        public virtual Int64? ColNumber18 { get; set; }
        [DataMember(Order=14, IsRequired=false)][XmlElement(Order=14, IsNullable=true)]
        public virtual Int64? ColNumber19 { get; set; }
        [DataMember(Order=15, IsRequired=false)][XmlElement(Order=15, IsNullable=true)]
        public virtual Int64? ColNumber38 { get; set; }
        [DataMember(Order=16, IsRequired=false)][XmlElement(Order=16, IsNullable=true)]
        public virtual SByte? ColNumber1Scale0 { get; set; }
        [DataMember(Order=17, IsRequired=false)][XmlElement(Order=17, IsNullable=true)]
        public virtual SByte? ColNumber2Scale0 { get; set; }
        [DataMember(Order=18, IsRequired=false)][XmlElement(Order=18, IsNullable=true)]
        public virtual Int16? ColNumber3Scale0 { get; set; }
        [DataMember(Order=19, IsRequired=false)][XmlElement(Order=19, IsNullable=true)]
        public virtual Int16? ColNumber4Scale0 { get; set; }
        [DataMember(Order=20, IsRequired=false)][XmlElement(Order=20, IsNullable=true)]
        public virtual Int32? ColNumber5Scale0 { get; set; }
        [DataMember(Order=21, IsRequired=false)][XmlElement(Order=21, IsNullable=true)]
        public virtual Int32? ColNumber9Scale0 { get; set; }
        [DataMember(Order=22, IsRequired=false)][XmlElement(Order=22, IsNullable=true)]
        public virtual Int64? ColNumber10Scale0 { get; set; }
        [DataMember(Order=23, IsRequired=false)][XmlElement(Order=23, IsNullable=true)]
        public virtual Int64? ColNumber18Scale0 { get; set; }
        [DataMember(Order=24, IsRequired=false)][XmlElement(Order=24, IsNullable=true)]
        public virtual Int64? ColNumber19Scale0 { get; set; }
        [DataMember(Order=25, IsRequired=false)][XmlElement(Order=25, IsNullable=true)]
        public virtual Int64? ColNumber28Scale0 { get; set; }
        [DataMember(Order=26, IsRequired=false)][XmlElement(Order=26, IsNullable=true)]
        public virtual Int64? ColNumber29Scale0 { get; set; }
        [DataMember(Order=27, IsRequired=false)][XmlElement(Order=27, IsNullable=true)]
        public virtual Int64? ColNumber38Scale0 { get; set; }
        [DataMember(Order=28, IsRequired=false)][XmlElement(Order=28, IsNullable=true)]
        public virtual Decimal? ColNumber2Scale1 { get; set; }
        [DataMember(Order=29, IsRequired=false)][XmlElement(Order=29, IsNullable=true)]
        public virtual Decimal? ColNumber5Scale3 { get; set; }
        [DataMember(Order=30, IsRequired=false)][XmlElement(Order=30, IsNullable=true)]
        public virtual Decimal? ColNumber15Scale11 { get; set; }
        [DataMember(Order=31, IsRequired=false)][XmlElement(Order=31, IsNullable=true)]
        public virtual Decimal? ColNumber31Scale21 { get; set; }
        [DataMember(Order=32, IsRequired=false)][XmlElement(Order=32, IsNullable=true)]
        public virtual Decimal? ColNumber38Scale37 { get; set; }
        [DataMember(Order=33, IsRequired=false)][XmlElement(Order=33, IsNullable=true)]
        public virtual Decimal? ColNumberLast { get; set; }
    } // OdptTableNumber

    // **TABLE IGNORED** - Code generation for OBJECT type has not been implemented
    // public partial class OdptTableObject : Schema.Odpt.Odpt.OdptTable {
} // Schema.Odpt.Odpt.Table