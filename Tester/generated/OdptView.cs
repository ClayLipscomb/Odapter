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

namespace Schema.Odpt.View {
    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public partial class OdptViewBigV : Schema.Odpt.OdptView {
        private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
        [DataMember(Order=1, IsRequired=true)][XmlElement(Order=1, IsNullable=true)]
        public virtual Int64? Id { get { return this.id; } set { this.id = value; } } protected Int64? id;
        [DataMember(Order=2, IsRequired=false)][XmlElement(Order=2, IsNullable=true)]
        public virtual Int64? ColNumberId { get { return this.colNumberId; } set { this.colNumberId = value; } } protected Int64? colNumberId;
        [DataMember(Order=3, IsRequired=false)][XmlElement(Order=3, IsNullable=true)]
        public virtual Int64? ColInteger { get { return this.colInteger; } set { this.colInteger = value; } } protected Int64? colInteger;
        [DataMember(Order=4, IsRequired=false)][XmlElement(Order=4, IsNullable=true)]
        public virtual Int64? ColInt { get { return this.colInt; } set { this.colInt = value; } } protected Int64? colInt;
        [DataMember(Order=5, IsRequired=false)][XmlElement(Order=5, IsNullable=true)]
        public virtual Int64? ColSmallint { get { return this.colSmallint; } set { this.colSmallint = value; } } protected Int64? colSmallint;
        [DataMember(Order=6, IsRequired=false)][XmlElement(Order=6, IsNullable=true)]
        public virtual Int64? ColNumeric { get { return this.colNumeric; } set { this.colNumeric = value; } } protected Int64? colNumeric;
        [DataMember(Order=7, IsRequired=false)][XmlElement(Order=7, IsNullable=true)]
        public virtual Int64? ColDecimal { get { return this.colDecimal; } set { this.colDecimal = value; } } protected Int64? colDecimal;
        [DataMember(Order=8, IsRequired=false)][XmlElement(Order=8, IsNullable=true)]
        public virtual Decimal? ColNumber { get { return this.colNumber; } set { this.colNumber = value; } } protected Decimal? colNumber;
        [DataMember(Order=9, IsRequired=false)][XmlElement(Order=9, IsNullable=true)]
        public virtual Decimal? ColDoublePrecision { get { return this.colDoublePrecision; } set { this.colDoublePrecision = value; } } protected Decimal? colDoublePrecision;
        [DataMember(Order=10, IsRequired=false)][XmlElement(Order=10, IsNullable=true)]
        public virtual Decimal? ColFloat { get { return this.colFloat; } set { this.colFloat = value; } } protected Decimal? colFloat;
        [DataMember(Order=11, IsRequired=false)][XmlElement(Order=11, IsNullable=true)]
        public virtual Decimal? ColReal { get { return this.colReal; } set { this.colReal = value; } } protected Decimal? colReal;
        [DataMember(Order=12, IsRequired=false)][XmlElement(Order=12, IsNullable=true)]
        public virtual Single? ColBinaryFloat { get { return this.colBinaryFloat; } set { this.colBinaryFloat = value; } } protected Single? colBinaryFloat;
        [DataMember(Order=13, IsRequired=false)][XmlElement(Order=13, IsNullable=true)]
        public virtual Double? ColBinaryDouble { get { return this.colBinaryDouble; } set { this.colBinaryDouble = value; } } protected Double? colBinaryDouble;
        [DataMember(Order=14, IsRequired=false)][XmlElement(Order=14, IsNullable=true)]
        public virtual String ColVarcharMin { get { return this.colVarcharMin; } set { this.colVarcharMin = value; } } protected String colVarcharMin;
        [DataMember(Order=15, IsRequired=false)][XmlElement(Order=15, IsNullable=true)]
        public virtual String ColVarcharMax { get { return this.colVarcharMax; } set { this.colVarcharMax = value; } } protected String colVarcharMax;
        [DataMember(Order=16, IsRequired=false)][XmlElement(Order=16, IsNullable=true)]
        public virtual String ColVarchar2Min { get { return this.colVarchar2Min; } set { this.colVarchar2Min = value; } } protected String colVarchar2Min;
        [DataMember(Order=17, IsRequired=false)][XmlElement(Order=17, IsNullable=true)]
        public virtual String ColVarchar2Max { get { return this.colVarchar2Max; } set { this.colVarchar2Max = value; } } protected String colVarchar2Max;
        [DataMember(Order=18, IsRequired=false)][XmlElement(Order=18, IsNullable=true)]
        public virtual String ColNvarchar2Min { get { return this.colNvarchar2Min; } set { this.colNvarchar2Min = value; } } protected String colNvarchar2Min;
        [DataMember(Order=19, IsRequired=false)][XmlElement(Order=19, IsNullable=true)]
        public virtual String ColNvarchar2Max { get { return this.colNvarchar2Max; } set { this.colNvarchar2Max = value; } } protected String colNvarchar2Max;
        [DataMember(Order=20, IsRequired=false)][XmlElement(Order=20, IsNullable=true)]
        public virtual String ColCharMin { get { return this.colCharMin; } set { this.colCharMin = value; } } protected String colCharMin;
        [DataMember(Order=21, IsRequired=false)][XmlElement(Order=21, IsNullable=true)]
        public virtual String ColCharMax { get { return this.colCharMax; } set { this.colCharMax = value; } } protected String colCharMax;
        [DataMember(Order=22, IsRequired=false)][XmlElement(Order=22, IsNullable=true)]
        public virtual String ColNcharMin { get { return this.colNcharMin; } set { this.colNcharMin = value; } } protected String colNcharMin;
        [DataMember(Order=23, IsRequired=false)][XmlElement(Order=23, IsNullable=true)]
        public virtual String ColNcharMax { get { return this.colNcharMax; } set { this.colNcharMax = value; } } protected String colNcharMax;
        [DataMember(Order=24, IsRequired=false)][XmlElement(Order=24, IsNullable=true)]
        public virtual DateTime? ColDate { get { return this.colDate; } set { this.colDate = value; } } protected DateTime? colDate;
        [DataMember(Order=25, IsRequired=false)][XmlElement(Order=25, IsNullable=true)]
        public virtual DateTime? ColTimestamp { get { return this.colTimestamp; } set { this.colTimestamp = value; } } protected DateTime? colTimestamp;
        [DataMember(Order=26, IsRequired=false)][XmlElement(Order=26, IsNullable=true)]
        public virtual DateTime? ColTimestampPrec0 { get { return this.colTimestampPrec0; } set { this.colTimestampPrec0 = value; } } protected DateTime? colTimestampPrec0;
        [DataMember(Order=27, IsRequired=false)][XmlElement(Order=27, IsNullable=true)]
        public virtual DateTime? ColTimestampPrec9 { get { return this.colTimestampPrec9; } set { this.colTimestampPrec9 = value; } } protected DateTime? colTimestampPrec9;
        [DataMember(Order=28, IsRequired=false)][XmlElement(Order=28, IsNullable=true)]
        public virtual Byte[] ColBlob { get { return this.colBlob; } set { this.colBlob = value; } } protected Byte[] colBlob;
        [DataMember(Order=29, IsRequired=false)][XmlElement(Order=29, IsNullable=true)]
        public virtual String ColClob { get { return this.colClob; } set { this.colClob = value; } } protected String colClob;
        [DataMember(Order=30, IsRequired=false)][XmlElement(Order=30, IsNullable=true)]
        public virtual String ColNclob { get { return this.colNclob; } set { this.colNclob = value; } } protected String colNclob;
        [DataMember(Order=31, IsRequired=false)][XmlElement(Order=31, IsNullable=true)]
        public virtual String ColLast { get { return this.colLast; } set { this.colLast = value; } } protected String colLast;
    } // OdptViewBigV
} // Schema.Odpt.View