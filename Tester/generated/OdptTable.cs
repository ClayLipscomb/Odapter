//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by Odapter 1.07.
//     Direct edits will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Types;
using Schema.Odpt.Type.Object;

namespace Schema.Odpt.Table {
    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public partial class OdptLogs : Schema.Odpt.OdptTable {
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
        public virtual String @Interface { get; set; }
        [DataMember(Order=6, IsRequired=true)][XmlElement(Order=6, IsNullable=true)]
        public virtual String Source { get; set; }
        [DataMember(Order=7, IsRequired=true)][XmlElement(Order=7, IsNullable=true)]
        public virtual String Msg { get; set; }
    } // OdptLogs

    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public partial class OdptTableBig : Schema.Odpt.OdptTable {
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
        public virtual String ColLast { get; set; }
    } // OdptTableBig

    // **TABLE IGNORED** - ANYDATA type is not available in ODP.NET managed
    // public partial class OdptTableIgnoreAny : Schema.Odpt.OdptTable {

    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public partial class OdptTableNamingConflict : Schema.Odpt.OdptTable {
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
    public partial class OdptTableNumber : Schema.Odpt.OdptTable {
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

    // **TABLE IGNORED** - OBJECT type is not available in ODP.NET managed
    // public partial class OdptTableObject : Schema.Odpt.OdptTable {
} // Schema.Odpt.Table