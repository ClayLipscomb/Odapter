//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by Odapter 1.06.
//     Direct edits will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Oracle.ManagedDataAccess.Types;

namespace Schema.Odpt.Type.Object {
    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public abstract partial class OdptBaseScTransTypeOt : Schema.Odpt.OdptObjectType {
        private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
        [DataMember(Order=1, IsRequired=false)][XmlElement(Order=1, IsNullable=true)]
        public virtual String FlexibleColumn1 { get; set; }
        [DataMember(Order=2, IsRequired=false)][XmlElement(Order=2, IsNullable=true)]
        public virtual String FlexibleColumn2 { get; set; }
        [DataMember(Order=3, IsRequired=false)][XmlElement(Order=3, IsNullable=true)]
        public virtual String FlexibleColumn3 { get; set; }
        [DataMember(Order=4, IsRequired=false)][XmlElement(Order=4, IsNullable=true)]
        public virtual String FlexibleColumn4 { get; set; }
        [DataMember(Order=5, IsRequired=false)][XmlElement(Order=5, IsNullable=true)]
        public virtual String FlexibleColumn5 { get; set; }
        [DataMember(Order=6, IsRequired=false)][XmlElement(Order=6, IsNullable=true)]
        public virtual String FlexibleColumn6 { get; set; }
        [DataMember(Order=7, IsRequired=false)][XmlElement(Order=7, IsNullable=true)]
        public virtual String FlexibleColumn7 { get; set; }
        [DataMember(Order=8, IsRequired=false)][XmlElement(Order=8, IsNullable=true)]
        public virtual String FlexibleColumn8 { get; set; }
        [DataMember(Order=9, IsRequired=false)][XmlElement(Order=9, IsNullable=true)]
        public virtual String FlexibleColumn9 { get; set; }
        [DataMember(Order=10, IsRequired=false)][XmlElement(Order=10, IsNullable=true)]
        public virtual String FlexibleColumn10 { get; set; }
        [DataMember(Order=11, IsRequired=false)][XmlElement(Order=11, IsNullable=true)]
        public virtual String FlexibleColumn11 { get; set; }
        [DataMember(Order=12, IsRequired=false)][XmlElement(Order=12, IsNullable=true)]
        public virtual String FlexibleColumn12 { get; set; }
        [DataMember(Order=13, IsRequired=false)][XmlElement(Order=13, IsNullable=true)]
        public virtual String FlexibleColumn13 { get; set; }
        [DataMember(Order=14, IsRequired=false)][XmlElement(Order=14, IsNullable=true)]
        public virtual String FlexibleColumn14 { get; set; }
        [DataMember(Order=15, IsRequired=false)][XmlElement(Order=15, IsNullable=true)]
        public virtual String FlexibleColumn15 { get; set; }
        [DataMember(Order=16, IsRequired=false)][XmlElement(Order=16, IsNullable=true)]
        public virtual String FlexibleColumn16 { get; set; }
        [DataMember(Order=17, IsRequired=false)][XmlElement(Order=17, IsNullable=true)]
        public virtual String FlexibleColumn17 { get; set; }
        [DataMember(Order=18, IsRequired=false)][XmlElement(Order=18, IsNullable=true)]
        public virtual DateTime? FlexibleColumn18 { get; set; }
        [DataMember(Order=19, IsRequired=false)][XmlElement(Order=19, IsNullable=true)]
        public virtual DateTime? FlexibleColumn19 { get; set; }
        [DataMember(Order=20, IsRequired=false)][XmlElement(Order=20, IsNullable=true)]
        public virtual DateTime? FlexibleColumn20 { get; set; }
        [DataMember(Order=21, IsRequired=false)][XmlElement(Order=21, IsNullable=true)]
        public virtual Decimal? FlexibleColumn21 { get; set; }
        [DataMember(Order=22, IsRequired=false)][XmlElement(Order=22, IsNullable=true)]
        public virtual Decimal? FlexibleColumn22 { get; set; }
        [DataMember(Order=23, IsRequired=false)][XmlElement(Order=23, IsNullable=true)]
        public virtual Decimal? FlexibleColumn23 { get; set; }
        [DataMember(Order=24, IsRequired=false)][XmlElement(Order=24, IsNullable=true)]
        public virtual Decimal? FlexibleColumn24 { get; set; }
        [DataMember(Order=25, IsRequired=false)][XmlElement(Order=25, IsNullable=true)]
        public virtual Decimal? FlexibleColumn25 { get; set; }
    } // OdptBaseScTransTypeOt

    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public partial class OdptBigOt : Schema.Odpt.OdptObjectType {
        private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
        [DataMember(Order=1, IsRequired=false)][XmlElement(Order=1, IsNullable=true)]
        public virtual Int64? Id { get; set; }
        [DataMember(Order=2, IsRequired=false)][XmlElement(Order=2, IsNullable=true)]
        public virtual Int64? AttrNumberId { get; set; }
        [DataMember(Order=3, IsRequired=false)][XmlElement(Order=3, IsNullable=true)]
        public virtual Int64? AttrInteger { get; set; }
        [DataMember(Order=4, IsRequired=false)][XmlElement(Order=4, IsNullable=true)]
        public virtual Int64? AttrInt { get; set; }
        [DataMember(Order=5, IsRequired=false)][XmlElement(Order=5, IsNullable=true)]
        public virtual Int64? AttrSmallint { get; set; }
        [DataMember(Order=6, IsRequired=false)][XmlElement(Order=6, IsNullable=true)]
        public virtual Decimal? AttrNumber { get; set; }
        [DataMember(Order=7, IsRequired=false)][XmlElement(Order=7, IsNullable=true)]
        public virtual Int64? AttrNumeric { get; set; }
        [DataMember(Order=8, IsRequired=false)][XmlElement(Order=8, IsNullable=true)]
        public virtual Decimal? AttrNumeric31Scale21 { get; set; }
        [DataMember(Order=9, IsRequired=false)][XmlElement(Order=9, IsNullable=true)]
        public virtual Int64? AttrDecimal { get; set; }
        [DataMember(Order=10, IsRequired=false)][XmlElement(Order=10, IsNullable=true)]
        public virtual Decimal? AttrDecima38Scale37 { get; set; }
        [DataMember(Order=11, IsRequired=false)][XmlElement(Order=11, IsNullable=true)]
        public virtual Decimal? AttrFloat { get; set; }
        [DataMember(Order=12, IsRequired=false)][XmlElement(Order=12, IsNullable=true)]
        public virtual Decimal? AttrReal { get; set; }
        [DataMember(Order=13, IsRequired=false)][XmlElement(Order=13, IsNullable=true)]
        public virtual Decimal? AttrDoublePrecision { get; set; }
        [DataMember(Order=14, IsRequired=false)][XmlElement(Order=14, IsNullable=true)]
        public virtual Single? AttrBinaryFloat { get; set; }
        [DataMember(Order=15, IsRequired=false)][XmlElement(Order=15, IsNullable=true)]
        public virtual Double? AttrBinaryDouble { get; set; }
        [DataMember(Order=16, IsRequired=false)][XmlElement(Order=16, IsNullable=true)]
        public virtual String AttrVarchar { get; set; }
        [DataMember(Order=17, IsRequired=false)][XmlElement(Order=17, IsNullable=true)]
        public virtual String AttrVarcharMax { get; set; }
        [DataMember(Order=18, IsRequired=false)][XmlElement(Order=18, IsNullable=true)]
        public virtual String AttrVarchar2 { get; set; }
        [DataMember(Order=19, IsRequired=false)][XmlElement(Order=19, IsNullable=true)]
        public virtual String AttrVarchar2Max { get; set; }
        [DataMember(Order=20, IsRequired=false)][XmlElement(Order=20, IsNullable=true)]
        public virtual String AttrNvarchar2 { get; set; }
        [DataMember(Order=21, IsRequired=false)][XmlElement(Order=21, IsNullable=true)]
        public virtual String AttrNvarchar2Max { get; set; }
        [DataMember(Order=22, IsRequired=false)][XmlElement(Order=22, IsNullable=true)]
        public virtual String AttrChar { get; set; }
        [DataMember(Order=23, IsRequired=false)][XmlElement(Order=23, IsNullable=true)]
        public virtual String AttrCharMax { get; set; }
        [DataMember(Order=24, IsRequired=false)][XmlElement(Order=24, IsNullable=true)]
        public virtual String AttrNchar { get; set; }
        [DataMember(Order=25, IsRequired=false)][XmlElement(Order=25, IsNullable=true)]
        public virtual String AttrNcharMax { get; set; }
        [DataMember(Order=26, IsRequired=false)][XmlElement(Order=26, IsNullable=true)]
        public virtual DateTime? AttrDate { get; set; }
        [DataMember(Order=27, IsRequired=false)][XmlElement(Order=27, IsNullable=true)]
        public virtual DateTime? AttrTimestamp { get; set; }
        [DataMember(Order=28, IsRequired=false)][XmlElement(Order=28, IsNullable=true)]
        public virtual DateTime? AttrTimestampWLTimeZone { get; set; }
        [DataMember(Order=29, IsRequired=false)][XmlElement(Order=29, IsNullable=true)]
        public virtual DateTime? AttrTimestampWTimeZone { get; set; }
        [DataMember(Order=30, IsRequired=false)][XmlElement(Order=30, IsNullable=true)]
        public virtual Byte[] AttrBlob { get; set; }
        [DataMember(Order=31, IsRequired=false)][XmlElement(Order=31, IsNullable=true)]
        public virtual String AttrClob { get; set; }
        [DataMember(Order=32, IsRequired=false)][XmlElement(Order=32, IsNullable=true)]
        public virtual String AttrNclob { get; set; }
        [DataMember(Order=33, IsRequired=false)][XmlElement(Order=33, IsNullable=true)]
        public virtual Int64? AttrLast { get; set; }
    } // OdptBigOt

    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public partial class OdptPoVendorOt : OdptScTtTblUniqueOt {
        private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
        [DataMember(Order=26, IsRequired=false)][XmlElement(Order=26, IsNullable=true)]
        public virtual Int64? VendorId { get; set; }
        [DataMember(Order=27, IsRequired=false)][XmlElement(Order=27, IsNullable=true)]
        public virtual DateTime? LastUpdateDate { get; set; }
        [DataMember(Order=28, IsRequired=false)][XmlElement(Order=28, IsNullable=true)]
        public virtual Decimal? LastUpdatedBy { get; set; }
        [DataMember(Order=29, IsRequired=false)][XmlElement(Order=29, IsNullable=true)]
        public virtual String VendorName { get; set; }
        [DataMember(Order=30, IsRequired=false)][XmlElement(Order=30, IsNullable=true)]
        public virtual String Segment1 { get; set; }
        [DataMember(Order=31, IsRequired=false)][XmlElement(Order=31, IsNullable=true)]
        public virtual String SummaryFlag { get; set; }
        [DataMember(Order=32, IsRequired=false)][XmlElement(Order=32, IsNullable=true)]
        public virtual String EnabledFlag { get; set; }
        [DataMember(Order=33, IsRequired=false)][XmlElement(Order=33, IsNullable=true)]
        public virtual String Segment2 { get; set; }
        [DataMember(Order=34, IsRequired=false)][XmlElement(Order=34, IsNullable=true)]
        public virtual String Segment3 { get; set; }
        [DataMember(Order=35, IsRequired=false)][XmlElement(Order=35, IsNullable=true)]
        public virtual String Segment4 { get; set; }
        [DataMember(Order=36, IsRequired=false)][XmlElement(Order=36, IsNullable=true)]
        public virtual String Segment5 { get; set; }
        [DataMember(Order=37, IsRequired=false)][XmlElement(Order=37, IsNullable=true)]
        public virtual Decimal? LastUpdateLogin { get; set; }
        [DataMember(Order=38, IsRequired=false)][XmlElement(Order=38, IsNullable=true)]
        public virtual DateTime? CreationDate { get; set; }
        [DataMember(Order=39, IsRequired=false)][XmlElement(Order=39, IsNullable=true)]
        public virtual Decimal? CreatedBy { get; set; }
        [DataMember(Order=40, IsRequired=false)][XmlElement(Order=40, IsNullable=true)]
        public virtual Int64? EmployeeId { get; set; }
        [DataMember(Order=41, IsRequired=false)][XmlElement(Order=41, IsNullable=true)]
        public virtual Decimal? ValidationNumber { get; set; }
        [DataMember(Order=42, IsRequired=false)][XmlElement(Order=42, IsNullable=true)]
        public virtual String VendorTypeLookupCode { get; set; }
        [DataMember(Order=43, IsRequired=false)][XmlElement(Order=43, IsNullable=true)]
        public virtual String CustomerNum { get; set; }
        [DataMember(Order=44, IsRequired=false)][XmlElement(Order=44, IsNullable=true)]
        public virtual String OneTimeFlag { get; set; }
        [DataMember(Order=45, IsRequired=false)][XmlElement(Order=45, IsNullable=true)]
        public virtual Int64? ParentVendorId { get; set; }
        [DataMember(Order=46, IsRequired=false)][XmlElement(Order=46, IsNullable=true)]
        public virtual Decimal? MinOrderAmount { get; set; }
        [DataMember(Order=47, IsRequired=false)][XmlElement(Order=47, IsNullable=true)]
        public virtual Int64? ShipToLocationId { get; set; }
        [DataMember(Order=48, IsRequired=false)][XmlElement(Order=48, IsNullable=true)]
        public virtual Int64? BillToLocationId { get; set; }
        [DataMember(Order=49, IsRequired=false)][XmlElement(Order=49, IsNullable=true)]
        public virtual String ShipViaLookupCode { get; set; }
        [DataMember(Order=50, IsRequired=false)][XmlElement(Order=50, IsNullable=true)]
        public virtual String FreightTermsLookupCode { get; set; }
        [DataMember(Order=51, IsRequired=false)][XmlElement(Order=51, IsNullable=true)]
        public virtual String FobLookupCode { get; set; }
        [DataMember(Order=52, IsRequired=false)][XmlElement(Order=52, IsNullable=true)]
        public virtual Int64? TermsId { get; set; }
        [DataMember(Order=53, IsRequired=false)][XmlElement(Order=53, IsNullable=true)]
        public virtual Int64? SetOfBooksId { get; set; }
        [DataMember(Order=54, IsRequired=false)][XmlElement(Order=54, IsNullable=true)]
        public virtual String CreditStatusLookupCode { get; set; }
        [DataMember(Order=55, IsRequired=false)][XmlElement(Order=55, IsNullable=true)]
        public virtual Decimal? CreditLimit { get; set; }
        [DataMember(Order=56, IsRequired=false)][XmlElement(Order=56, IsNullable=true)]
        public virtual String AlwaysTakeDiscFlag { get; set; }
        [DataMember(Order=57, IsRequired=false)][XmlElement(Order=57, IsNullable=true)]
        public virtual String PayDateBasisLookupCode { get; set; }
        [DataMember(Order=58, IsRequired=false)][XmlElement(Order=58, IsNullable=true)]
        public virtual String PayGroupLookupCode { get; set; }
        [DataMember(Order=59, IsRequired=false)][XmlElement(Order=59, IsNullable=true)]
        public virtual Decimal? PaymentPriority { get; set; }
        [DataMember(Order=60, IsRequired=false)][XmlElement(Order=60, IsNullable=true)]
        public virtual String InvoiceCurrencyCode { get; set; }
        [DataMember(Order=61, IsRequired=false)][XmlElement(Order=61, IsNullable=true)]
        public virtual String PaymentCurrencyCode { get; set; }
        [DataMember(Order=62, IsRequired=false)][XmlElement(Order=62, IsNullable=true)]
        public virtual Decimal? InvoiceAmountLimit { get; set; }
        [DataMember(Order=63, IsRequired=false)][XmlElement(Order=63, IsNullable=true)]
        public virtual String ExchangeDateLookupCode { get; set; }
        [DataMember(Order=64, IsRequired=false)][XmlElement(Order=64, IsNullable=true)]
        public virtual String HoldAllPaymentsFlag { get; set; }
        [DataMember(Order=65, IsRequired=false)][XmlElement(Order=65, IsNullable=true)]
        public virtual String HoldFuturePaymentsFlag { get; set; }
        [DataMember(Order=66, IsRequired=false)][XmlElement(Order=66, IsNullable=true)]
        public virtual String HoldReason { get; set; }
        [DataMember(Order=67, IsRequired=false)][XmlElement(Order=67, IsNullable=true)]
        public virtual Int64? DistributionSetId { get; set; }
        [DataMember(Order=68, IsRequired=false)][XmlElement(Order=68, IsNullable=true)]
        public virtual Int64? AcctsPayCodeCombinationId { get; set; }
        [DataMember(Order=69, IsRequired=false)][XmlElement(Order=69, IsNullable=true)]
        public virtual Int64? DiscLostCodeCombinationId { get; set; }
        [DataMember(Order=70, IsRequired=false)][XmlElement(Order=70, IsNullable=true)]
        public virtual Int64? DiscTakenCodeCombinationId { get; set; }
        [DataMember(Order=71, IsRequired=false)][XmlElement(Order=71, IsNullable=true)]
        public virtual Int64? ExpenseCodeCombinationId { get; set; }
        [DataMember(Order=72, IsRequired=false)][XmlElement(Order=72, IsNullable=true)]
        public virtual Int64? PrepayCodeCombinationId { get; set; }
        [DataMember(Order=73, IsRequired=false)][XmlElement(Order=73, IsNullable=true)]
        public virtual String Num1099 { get; set; }
        [DataMember(Order=74, IsRequired=false)][XmlElement(Order=74, IsNullable=true)]
        public virtual String Type1099 { get; set; }
        [DataMember(Order=75, IsRequired=false)][XmlElement(Order=75, IsNullable=true)]
        public virtual String WithholdingStatusLookupCode { get; set; }
        [DataMember(Order=76, IsRequired=false)][XmlElement(Order=76, IsNullable=true)]
        public virtual DateTime? WithholdingStartDate { get; set; }
        [DataMember(Order=77, IsRequired=false)][XmlElement(Order=77, IsNullable=true)]
        public virtual String OrganizationTypeLookupCode { get; set; }
        [DataMember(Order=78, IsRequired=false)][XmlElement(Order=78, IsNullable=true)]
        public virtual String VatCode { get; set; }
        [DataMember(Order=79, IsRequired=false)][XmlElement(Order=79, IsNullable=true)]
        public virtual DateTime? StartDateActive { get; set; }
        [DataMember(Order=80, IsRequired=false)][XmlElement(Order=80, IsNullable=true)]
        public virtual DateTime? EndDateActive { get; set; }
        [DataMember(Order=81, IsRequired=false)][XmlElement(Order=81, IsNullable=true)]
        public virtual Decimal? QtyRcvTolerance { get; set; }
        [DataMember(Order=82, IsRequired=false)][XmlElement(Order=82, IsNullable=true)]
        public virtual String OverToleranceErrorFlag { get; set; }
        [DataMember(Order=83, IsRequired=false)][XmlElement(Order=83, IsNullable=true)]
        public virtual String MinorityGroupLookupCode { get; set; }
        [DataMember(Order=84, IsRequired=false)][XmlElement(Order=84, IsNullable=true)]
        public virtual String PaymentMethodLookupCode { get; set; }
        [DataMember(Order=85, IsRequired=false)][XmlElement(Order=85, IsNullable=true)]
        public virtual String BankAccountName { get; set; }
        [DataMember(Order=86, IsRequired=false)][XmlElement(Order=86, IsNullable=true)]
        public virtual String BankAccountNum { get; set; }
        [DataMember(Order=87, IsRequired=false)][XmlElement(Order=87, IsNullable=true)]
        public virtual String BankNum { get; set; }
        [DataMember(Order=88, IsRequired=false)][XmlElement(Order=88, IsNullable=true)]
        public virtual String BankAccountType { get; set; }
        [DataMember(Order=89, IsRequired=false)][XmlElement(Order=89, IsNullable=true)]
        public virtual String WomenOwnedFlag { get; set; }
        [DataMember(Order=90, IsRequired=false)][XmlElement(Order=90, IsNullable=true)]
        public virtual String SmallBusinessFlag { get; set; }
        [DataMember(Order=91, IsRequired=false)][XmlElement(Order=91, IsNullable=true)]
        public virtual String StandardIndustryClass { get; set; }
        [DataMember(Order=92, IsRequired=false)][XmlElement(Order=92, IsNullable=true)]
        public virtual String AttributeCategory { get; set; }
        [DataMember(Order=93, IsRequired=false)][XmlElement(Order=93, IsNullable=true)]
        public virtual String Attribute1 { get; set; }
        [DataMember(Order=94, IsRequired=false)][XmlElement(Order=94, IsNullable=true)]
        public virtual String Attribute2 { get; set; }
        [DataMember(Order=95, IsRequired=false)][XmlElement(Order=95, IsNullable=true)]
        public virtual String Attribute3 { get; set; }
        [DataMember(Order=96, IsRequired=false)][XmlElement(Order=96, IsNullable=true)]
        public virtual String Attribute4 { get; set; }
        [DataMember(Order=97, IsRequired=false)][XmlElement(Order=97, IsNullable=true)]
        public virtual String Attribute5 { get; set; }
        [DataMember(Order=98, IsRequired=false)][XmlElement(Order=98, IsNullable=true)]
        public virtual String HoldFlag { get; set; }
        [DataMember(Order=99, IsRequired=false)][XmlElement(Order=99, IsNullable=true)]
        public virtual String PurchasingHoldReason { get; set; }
        [DataMember(Order=100, IsRequired=false)][XmlElement(Order=100, IsNullable=true)]
        public virtual Int64? HoldBy { get; set; }
        [DataMember(Order=101, IsRequired=false)][XmlElement(Order=101, IsNullable=true)]
        public virtual DateTime? HoldDate { get; set; }
        [DataMember(Order=102, IsRequired=false)][XmlElement(Order=102, IsNullable=true)]
        public virtual String TermsDateBasis { get; set; }
        [DataMember(Order=103, IsRequired=false)][XmlElement(Order=103, IsNullable=true)]
        public virtual Decimal? PriceTolerance { get; set; }
        [DataMember(Order=104, IsRequired=false)][XmlElement(Order=104, IsNullable=true)]
        public virtual String InspectionRequiredFlag { get; set; }
        [DataMember(Order=105, IsRequired=false)][XmlElement(Order=105, IsNullable=true)]
        public virtual String ReceiptRequiredFlag { get; set; }
        [DataMember(Order=106, IsRequired=false)][XmlElement(Order=106, IsNullable=true)]
        public virtual String QtyRcvExceptionCode { get; set; }
        [DataMember(Order=107, IsRequired=false)][XmlElement(Order=107, IsNullable=true)]
        public virtual String EnforceShipToLocationCode { get; set; }
        [DataMember(Order=108, IsRequired=false)][XmlElement(Order=108, IsNullable=true)]
        public virtual Decimal? DaysEarlyReceiptAllowed { get; set; }
        [DataMember(Order=109, IsRequired=false)][XmlElement(Order=109, IsNullable=true)]
        public virtual Decimal? DaysLateReceiptAllowed { get; set; }
        [DataMember(Order=110, IsRequired=false)][XmlElement(Order=110, IsNullable=true)]
        public virtual String ReceiptDaysExceptionCode { get; set; }
        [DataMember(Order=111, IsRequired=false)][XmlElement(Order=111, IsNullable=true)]
        public virtual Int64? ReceivingRoutingId { get; set; }
        [DataMember(Order=112, IsRequired=false)][XmlElement(Order=112, IsNullable=true)]
        public virtual String AllowSubstituteReceiptsFlag { get; set; }
        [DataMember(Order=113, IsRequired=false)][XmlElement(Order=113, IsNullable=true)]
        public virtual String AllowUnorderedReceiptsFlag { get; set; }
        [DataMember(Order=114, IsRequired=false)][XmlElement(Order=114, IsNullable=true)]
        public virtual String HoldUnmatchedInvoicesFlag { get; set; }
        [DataMember(Order=115, IsRequired=false)][XmlElement(Order=115, IsNullable=true)]
        public virtual String ExclusivePaymentFlag { get; set; }
        [DataMember(Order=116, IsRequired=false)][XmlElement(Order=116, IsNullable=true)]
        public virtual DateTime? TaxVerificationDate { get; set; }
        [DataMember(Order=117, IsRequired=false)][XmlElement(Order=117, IsNullable=true)]
        public virtual String NameControl { get; set; }
        [DataMember(Order=118, IsRequired=false)][XmlElement(Order=118, IsNullable=true)]
        public virtual String StateReportableFlag { get; set; }
        [DataMember(Order=119, IsRequired=false)][XmlElement(Order=119, IsNullable=true)]
        public virtual String FederalReportableFlag { get; set; }
        [DataMember(Order=120, IsRequired=false)][XmlElement(Order=120, IsNullable=true)]
        public virtual String Attribute6 { get; set; }
        [DataMember(Order=121, IsRequired=false)][XmlElement(Order=121, IsNullable=true)]
        public virtual String Attribute7 { get; set; }
        [DataMember(Order=122, IsRequired=false)][XmlElement(Order=122, IsNullable=true)]
        public virtual String Attribute8 { get; set; }
        [DataMember(Order=123, IsRequired=false)][XmlElement(Order=123, IsNullable=true)]
        public virtual String Attribute9 { get; set; }
        [DataMember(Order=124, IsRequired=false)][XmlElement(Order=124, IsNullable=true)]
        public virtual String Attribute10 { get; set; }
        [DataMember(Order=125, IsRequired=false)][XmlElement(Order=125, IsNullable=true)]
        public virtual String Attribute11 { get; set; }
        [DataMember(Order=126, IsRequired=false)][XmlElement(Order=126, IsNullable=true)]
        public virtual String Attribute12 { get; set; }
        [DataMember(Order=127, IsRequired=false)][XmlElement(Order=127, IsNullable=true)]
        public virtual String Attribute13 { get; set; }
        [DataMember(Order=128, IsRequired=false)][XmlElement(Order=128, IsNullable=true)]
        public virtual String Attribute14 { get; set; }
        [DataMember(Order=129, IsRequired=false)][XmlElement(Order=129, IsNullable=true)]
        public virtual String Attribute15 { get; set; }
        [DataMember(Order=130, IsRequired=false)][XmlElement(Order=130, IsNullable=true)]
        public virtual Int64? RequestId { get; set; }
        [DataMember(Order=131, IsRequired=false)][XmlElement(Order=131, IsNullable=true)]
        public virtual Int64? ProgramApplicationId { get; set; }
        [DataMember(Order=132, IsRequired=false)][XmlElement(Order=132, IsNullable=true)]
        public virtual Int64? ProgramId { get; set; }
        [DataMember(Order=133, IsRequired=false)][XmlElement(Order=133, IsNullable=true)]
        public virtual DateTime? ProgramUpdateDate { get; set; }
        [DataMember(Order=134, IsRequired=false)][XmlElement(Order=134, IsNullable=true)]
        public virtual String OffsetVatCode { get; set; }
        [DataMember(Order=135, IsRequired=false)][XmlElement(Order=135, IsNullable=true)]
        public virtual String VatRegistrationNum { get; set; }
        [DataMember(Order=136, IsRequired=false)][XmlElement(Order=136, IsNullable=true)]
        public virtual String AutoCalculateInterestFlag { get; set; }
        [DataMember(Order=137, IsRequired=false)][XmlElement(Order=137, IsNullable=true)]
        public virtual String ExcludeFreightFromDiscount { get; set; }
        [DataMember(Order=138, IsRequired=false)][XmlElement(Order=138, IsNullable=true)]
        public virtual String TaxReportingName { get; set; }
        [DataMember(Order=139, IsRequired=false)][XmlElement(Order=139, IsNullable=true)]
        public virtual String CheckDigits { get; set; }
        [DataMember(Order=140, IsRequired=false)][XmlElement(Order=140, IsNullable=true)]
        public virtual String BankNumber { get; set; }
        [DataMember(Order=141, IsRequired=false)][XmlElement(Order=141, IsNullable=true)]
        public virtual String AllowAwtFlag { get; set; }
        [DataMember(Order=142, IsRequired=false)][XmlElement(Order=142, IsNullable=true)]
        public virtual Int64? AwtGroupId { get; set; }
        [DataMember(Order=143, IsRequired=false)][XmlElement(Order=143, IsNullable=true)]
        public virtual String VendorNameAlt { get; set; }
        [DataMember(Order=144, IsRequired=false)][XmlElement(Order=144, IsNullable=true)]
        public virtual String ApTaxRoundingRule { get; set; }
        [DataMember(Order=145, IsRequired=false)][XmlElement(Order=145, IsNullable=true)]
        public virtual String AutoTaxCalcFlag { get; set; }
        [DataMember(Order=146, IsRequired=false)][XmlElement(Order=146, IsNullable=true)]
        public virtual String AutoTaxCalcOverride { get; set; }
        [DataMember(Order=147, IsRequired=false)][XmlElement(Order=147, IsNullable=true)]
        public virtual String AmountIncludesTaxFlag { get; set; }
        [DataMember(Order=148, IsRequired=false)][XmlElement(Order=148, IsNullable=true)]
        public virtual String GlobalAttribute1 { get; set; }
        [DataMember(Order=149, IsRequired=false)][XmlElement(Order=149, IsNullable=true)]
        public virtual String GlobalAttribute2 { get; set; }
        [DataMember(Order=150, IsRequired=false)][XmlElement(Order=150, IsNullable=true)]
        public virtual String GlobalAttribute3 { get; set; }
        [DataMember(Order=151, IsRequired=false)][XmlElement(Order=151, IsNullable=true)]
        public virtual String GlobalAttribute4 { get; set; }
        [DataMember(Order=152, IsRequired=false)][XmlElement(Order=152, IsNullable=true)]
        public virtual String GlobalAttribute5 { get; set; }
        [DataMember(Order=153, IsRequired=false)][XmlElement(Order=153, IsNullable=true)]
        public virtual String GlobalAttribute6 { get; set; }
        [DataMember(Order=154, IsRequired=false)][XmlElement(Order=154, IsNullable=true)]
        public virtual String GlobalAttribute7 { get; set; }
        [DataMember(Order=155, IsRequired=false)][XmlElement(Order=155, IsNullable=true)]
        public virtual String GlobalAttribute8 { get; set; }
        [DataMember(Order=156, IsRequired=false)][XmlElement(Order=156, IsNullable=true)]
        public virtual String GlobalAttribute9 { get; set; }
        [DataMember(Order=157, IsRequired=false)][XmlElement(Order=157, IsNullable=true)]
        public virtual String GlobalAttribute10 { get; set; }
        [DataMember(Order=158, IsRequired=false)][XmlElement(Order=158, IsNullable=true)]
        public virtual String GlobalAttribute11 { get; set; }
        [DataMember(Order=159, IsRequired=false)][XmlElement(Order=159, IsNullable=true)]
        public virtual String GlobalAttribute12 { get; set; }
        [DataMember(Order=160, IsRequired=false)][XmlElement(Order=160, IsNullable=true)]
        public virtual String GlobalAttribute13 { get; set; }
        [DataMember(Order=161, IsRequired=false)][XmlElement(Order=161, IsNullable=true)]
        public virtual String GlobalAttribute14 { get; set; }
        [DataMember(Order=162, IsRequired=false)][XmlElement(Order=162, IsNullable=true)]
        public virtual String GlobalAttribute15 { get; set; }
        [DataMember(Order=163, IsRequired=false)][XmlElement(Order=163, IsNullable=true)]
        public virtual String GlobalAttribute16 { get; set; }
        [DataMember(Order=164, IsRequired=false)][XmlElement(Order=164, IsNullable=true)]
        public virtual String GlobalAttribute17 { get; set; }
        [DataMember(Order=165, IsRequired=false)][XmlElement(Order=165, IsNullable=true)]
        public virtual String GlobalAttribute18 { get; set; }
        [DataMember(Order=166, IsRequired=false)][XmlElement(Order=166, IsNullable=true)]
        public virtual String GlobalAttribute19 { get; set; }
        [DataMember(Order=167, IsRequired=false)][XmlElement(Order=167, IsNullable=true)]
        public virtual String GlobalAttribute20 { get; set; }
        [DataMember(Order=168, IsRequired=false)][XmlElement(Order=168, IsNullable=true)]
        public virtual String GlobalAttributeCategory { get; set; }
        [DataMember(Order=169, IsRequired=false)][XmlElement(Order=169, IsNullable=true)]
        public virtual String EdiTransactionHandling { get; set; }
        [DataMember(Order=170, IsRequired=false)][XmlElement(Order=170, IsNullable=true)]
        public virtual String EdiPaymentMethod { get; set; }
        [DataMember(Order=171, IsRequired=false)][XmlElement(Order=171, IsNullable=true)]
        public virtual String EdiPaymentFormat { get; set; }
        [DataMember(Order=172, IsRequired=false)][XmlElement(Order=172, IsNullable=true)]
        public virtual String EdiRemittanceMethod { get; set; }
        [DataMember(Order=173, IsRequired=false)][XmlElement(Order=173, IsNullable=true)]
        public virtual String EdiRemittanceInstruction { get; set; }
        [DataMember(Order=174, IsRequired=false)][XmlElement(Order=174, IsNullable=true)]
        public virtual String BankChargeBearer { get; set; }
        [DataMember(Order=175, IsRequired=false)][XmlElement(Order=175, IsNullable=true)]
        public virtual String BankBranchType { get; set; }
        [DataMember(Order=176, IsRequired=false)][XmlElement(Order=176, IsNullable=true)]
        public virtual String MatchOption { get; set; }
        [DataMember(Order=177, IsRequired=false)][XmlElement(Order=177, IsNullable=true)]
        public virtual Int64? FutureDatedPaymentCcid { get; set; }
        [DataMember(Order=178, IsRequired=false)][XmlElement(Order=178, IsNullable=true)]
        public virtual String CreateDebitMemoFlag { get; set; }
        [DataMember(Order=179, IsRequired=false)][XmlElement(Order=179, IsNullable=true)]
        public virtual String OffsetTaxFlag { get; set; }
        [DataMember(Order=180, IsRequired=false)][XmlElement(Order=180, IsNullable=true)]
        public virtual Int64? UniqueTaxReferenceNum { get; set; }
        [DataMember(Order=181, IsRequired=false)][XmlElement(Order=181, IsNullable=true)]
        public virtual Int64? PartnershipUtr { get; set; }
        [DataMember(Order=182, IsRequired=false)][XmlElement(Order=182, IsNullable=true)]
        public virtual String PartnershipName { get; set; }
        [DataMember(Order=183, IsRequired=false)][XmlElement(Order=183, IsNullable=true)]
        public virtual String CisEnabledFlag { get; set; }
        [DataMember(Order=184, IsRequired=false)][XmlElement(Order=184, IsNullable=true)]
        public virtual String FirstName { get; set; }
        [DataMember(Order=185, IsRequired=false)][XmlElement(Order=185, IsNullable=true)]
        public virtual String SecondName { get; set; }
        [DataMember(Order=186, IsRequired=false)][XmlElement(Order=186, IsNullable=true)]
        public virtual String LastName { get; set; }
        [DataMember(Order=187, IsRequired=false)][XmlElement(Order=187, IsNullable=true)]
        public virtual String Salutation { get; set; }
        [DataMember(Order=188, IsRequired=false)][XmlElement(Order=188, IsNullable=true)]
        public virtual String TradingName { get; set; }
        [DataMember(Order=189, IsRequired=false)][XmlElement(Order=189, IsNullable=true)]
        public virtual String WorkReference { get; set; }
        [DataMember(Order=190, IsRequired=false)][XmlElement(Order=190, IsNullable=true)]
        public virtual String CompanyRegistrationNumber { get; set; }
        [DataMember(Order=191, IsRequired=false)][XmlElement(Order=191, IsNullable=true)]
        public virtual String NationalInsuranceNumber { get; set; }
        [DataMember(Order=192, IsRequired=false)][XmlElement(Order=192, IsNullable=true)]
        public virtual String VerificationNumber { get; set; }
        [DataMember(Order=193, IsRequired=false)][XmlElement(Order=193, IsNullable=true)]
        public virtual Int64? VerificationRequestId { get; set; }
        [DataMember(Order=194, IsRequired=false)][XmlElement(Order=194, IsNullable=true)]
        public virtual String MatchStatusFlag { get; set; }
        [DataMember(Order=195, IsRequired=false)][XmlElement(Order=195, IsNullable=true)]
        public virtual DateTime? CisVerificationDate { get; set; }
        [DataMember(Order=196, IsRequired=false)][XmlElement(Order=196, IsNullable=true)]
        public virtual String Individual1099 { get; set; }
        [DataMember(Order=197, IsRequired=false)][XmlElement(Order=197, IsNullable=true)]
        public virtual Int64? CisParentVendorId { get; set; }
        [DataMember(Order=198, IsRequired=false)][XmlElement(Order=198, IsNullable=true)]
        public virtual DateTime? BusClassLastCertifiedDate { get; set; }
        [DataMember(Order=199, IsRequired=false)][XmlElement(Order=199, IsNullable=true)]
        public virtual Decimal? BusClassLastCertifiedBy { get; set; }
    } // OdptPoVendorOt

    [DataContract(Namespace="http://odpt.business.com")][Serializable()]
    public abstract partial class OdptScTtTblUniqueOt : OdptBaseScTransTypeOt {
        private Byte propertyToEnsuresPartialClassNamesAreUniqueAtCompileTime { get; set; }
    } // OdptScTtTblUniqueOt
} // Schema.Odpt.Type.Object