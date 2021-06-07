//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by Odapter 2.01.
//     Direct edits will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Oracle.ManagedDataAccess.Types;

namespace Schema.Odpt.Odpt.Type.Object {
    // **OBJECT IGNORED** - ANYDATA type is not available in ODP.NET managed
    // public interface IOdptAnydataOt {

    public interface IOdptBaseScTransTypeOt {
        String FlexibleColumn1 { set; }
        String FlexibleColumn2 { set; }
        String FlexibleColumn3 { set; }
        String FlexibleColumn4 { set; }
        String FlexibleColumn5 { set; }
        String FlexibleColumn6 { set; }
        String FlexibleColumn7 { set; }
        String FlexibleColumn8 { set; }
        String FlexibleColumn9 { set; }
        String FlexibleColumn10 { set; }
        String FlexibleColumn11 { set; }
        String FlexibleColumn12 { set; }
        String FlexibleColumn13 { set; }
        String FlexibleColumn14 { set; }
        String FlexibleColumn15 { set; }
        String FlexibleColumn16 { set; }
        String FlexibleColumn17 { set; }
        DateTime? FlexibleColumn18 { set; }
        DateTime? FlexibleColumn19 { set; }
        DateTime? FlexibleColumn20 { set; }
        Decimal? FlexibleColumn21 { set; }
        Decimal? FlexibleColumn22 { set; }
        Decimal? FlexibleColumn23 { set; }
        Decimal? FlexibleColumn24 { set; }
        Decimal? FlexibleColumn25 { set; }
    } // IOdptBaseScTransTypeOt

    public interface IOdptBigOt {
        Int64? Id { set; }
        Int64? AttrNumberId { set; }
        Int64? AttrInteger { set; }
        Int64? AttrInt { set; }
        Int64? AttrSmallint { set; }
        Decimal? AttrNumber { set; }
        Int64? AttrNumeric { set; }
        Decimal? AttrNumeric31Scale21 { set; }
        Int64? AttrDecimal { set; }
        Decimal? AttrDecima38Scale37 { set; }
        Decimal? AttrFloat { set; }
        Decimal? AttrReal { set; }
        Decimal? AttrDoublePrecision { set; }
        Single? AttrBinaryFloat { set; }
        Double? AttrBinaryDouble { set; }
        String AttrVarchar { set; }
        String AttrVarcharMax { set; }
        String AttrVarchar2 { set; }
        String AttrVarchar2Max { set; }
        String AttrNvarchar2 { set; }
        String AttrNvarchar2Max { set; }
        String AttrChar { set; }
        String AttrCharMax { set; }
        String AttrNchar { set; }
        String AttrNcharMax { set; }
        DateTime? AttrDate { set; }
        DateTime? AttrTimestamp { set; }
        DateTime? AttrTimestampWLTimeZone { set; }
        DateTimeOffset? AttrTimestampWTimeZone { set; }
        Byte[] AttrBlob { set; }
        String AttrClob { set; }
        String AttrNclob { set; }
        Int64? AttrLast { set; }
    } // IOdptBigOt

    // **OBJECT IGNORED** - Code generation for NESTED TABLE type has not been implemented
    // public interface IOdptNestedTableOt {

    public interface IOdptObjectOt {
        IOdptBigOt AttrOdptBigOt { set; }
    } // IOdptObjectOt

    public interface IOdptPoVendorOt : IOdptScTtTblUniqueOt {
        Int64? VendorId { set; }
        DateTime? LastUpdateDate { set; }
        Decimal? LastUpdatedBy { set; }
        String VendorName { set; }
        String Segment1 { set; }
        String SummaryFlag { set; }
        String EnabledFlag { set; }
        String Segment2 { set; }
        String Segment3 { set; }
        String Segment4 { set; }
        String Segment5 { set; }
        Decimal? LastUpdateLogin { set; }
        DateTime? CreationDate { set; }
        Decimal? CreatedBy { set; }
        Int64? EmployeeId { set; }
        Decimal? ValidationNumber { set; }
        String VendorTypeLookupCode { set; }
        String CustomerNum { set; }
        String OneTimeFlag { set; }
        Int64? ParentVendorId { set; }
        Decimal? MinOrderAmount { set; }
        Int64? ShipToLocationId { set; }
        Int64? BillToLocationId { set; }
        String ShipViaLookupCode { set; }
        String FreightTermsLookupCode { set; }
        String FobLookupCode { set; }
        Int64? TermsId { set; }
        Int64? SetOfBooksId { set; }
        String CreditStatusLookupCode { set; }
        Decimal? CreditLimit { set; }
        String AlwaysTakeDiscFlag { set; }
        String PayDateBasisLookupCode { set; }
        String PayGroupLookupCode { set; }
        Decimal? PaymentPriority { set; }
        String InvoiceCurrencyCode { set; }
        String PaymentCurrencyCode { set; }
        Decimal? InvoiceAmountLimit { set; }
        String ExchangeDateLookupCode { set; }
        String HoldAllPaymentsFlag { set; }
        String HoldFuturePaymentsFlag { set; }
        String HoldReason { set; }
        Int64? DistributionSetId { set; }
        Int64? AcctsPayCodeCombinationId { set; }
        Int64? DiscLostCodeCombinationId { set; }
        Int64? DiscTakenCodeCombinationId { set; }
        Int64? ExpenseCodeCombinationId { set; }
        Int64? PrepayCodeCombinationId { set; }
        String Num1099 { set; }
        String Type1099 { set; }
        String WithholdingStatusLookupCode { set; }
        DateTime? WithholdingStartDate { set; }
        String OrganizationTypeLookupCode { set; }
        String VatCode { set; }
        DateTime? StartDateActive { set; }
        DateTime? EndDateActive { set; }
        Decimal? QtyRcvTolerance { set; }
        String OverToleranceErrorFlag { set; }
        String MinorityGroupLookupCode { set; }
        String PaymentMethodLookupCode { set; }
        String BankAccountName { set; }
        String BankAccountNum { set; }
        String BankNum { set; }
        String BankAccountType { set; }
        String WomenOwnedFlag { set; }
        String SmallBusinessFlag { set; }
        String StandardIndustryClass { set; }
        String AttributeCategory { set; }
        String Attribute1 { set; }
        String Attribute2 { set; }
        String Attribute3 { set; }
        String Attribute4 { set; }
        String Attribute5 { set; }
        String HoldFlag { set; }
        String PurchasingHoldReason { set; }
        Int64? HoldBy { set; }
        DateTime? HoldDate { set; }
        String TermsDateBasis { set; }
        Decimal? PriceTolerance { set; }
        String InspectionRequiredFlag { set; }
        String ReceiptRequiredFlag { set; }
        String QtyRcvExceptionCode { set; }
        String EnforceShipToLocationCode { set; }
        Decimal? DaysEarlyReceiptAllowed { set; }
        Decimal? DaysLateReceiptAllowed { set; }
        String ReceiptDaysExceptionCode { set; }
        Int64? ReceivingRoutingId { set; }
        String AllowSubstituteReceiptsFlag { set; }
        String AllowUnorderedReceiptsFlag { set; }
        String HoldUnmatchedInvoicesFlag { set; }
        String ExclusivePaymentFlag { set; }
        DateTime? TaxVerificationDate { set; }
        String NameControl { set; }
        String StateReportableFlag { set; }
        String FederalReportableFlag { set; }
        String Attribute6 { set; }
        String Attribute7 { set; }
        String Attribute8 { set; }
        String Attribute9 { set; }
        String Attribute10 { set; }
        String Attribute11 { set; }
        String Attribute12 { set; }
        String Attribute13 { set; }
        String Attribute14 { set; }
        String Attribute15 { set; }
        Int64? RequestId { set; }
        Int64? ProgramApplicationId { set; }
        Int64? ProgramId { set; }
        DateTime? ProgramUpdateDate { set; }
        String OffsetVatCode { set; }
        String VatRegistrationNum { set; }
        String AutoCalculateInterestFlag { set; }
        String ExcludeFreightFromDiscount { set; }
        String TaxReportingName { set; }
        String CheckDigits { set; }
        String BankNumber { set; }
        String AllowAwtFlag { set; }
        Int64? AwtGroupId { set; }
        String VendorNameAlt { set; }
        String ApTaxRoundingRule { set; }
        String AutoTaxCalcFlag { set; }
        String AutoTaxCalcOverride { set; }
        String AmountIncludesTaxFlag { set; }
        String GlobalAttribute1 { set; }
        String GlobalAttribute2 { set; }
        String GlobalAttribute3 { set; }
        String GlobalAttribute4 { set; }
        String GlobalAttribute5 { set; }
        String GlobalAttribute6 { set; }
        String GlobalAttribute7 { set; }
        String GlobalAttribute8 { set; }
        String GlobalAttribute9 { set; }
        String GlobalAttribute10 { set; }
        String GlobalAttribute11 { set; }
        String GlobalAttribute12 { set; }
        String GlobalAttribute13 { set; }
        String GlobalAttribute14 { set; }
        String GlobalAttribute15 { set; }
        String GlobalAttribute16 { set; }
        String GlobalAttribute17 { set; }
        String GlobalAttribute18 { set; }
        String GlobalAttribute19 { set; }
        String GlobalAttribute20 { set; }
        String GlobalAttributeCategory { set; }
        String EdiTransactionHandling { set; }
        String EdiPaymentMethod { set; }
        String EdiPaymentFormat { set; }
        String EdiRemittanceMethod { set; }
        String EdiRemittanceInstruction { set; }
        String BankChargeBearer { set; }
        String BankBranchType { set; }
        String MatchOption { set; }
        Int64? FutureDatedPaymentCcid { set; }
        String CreateDebitMemoFlag { set; }
        String OffsetTaxFlag { set; }
        Int64? UniqueTaxReferenceNum { set; }
        Int64? PartnershipUtr { set; }
        String PartnershipName { set; }
        String CisEnabledFlag { set; }
        String FirstName { set; }
        String SecondName { set; }
        String LastName { set; }
        String Salutation { set; }
        String TradingName { set; }
        String WorkReference { set; }
        String CompanyRegistrationNumber { set; }
        String NationalInsuranceNumber { set; }
        String VerificationNumber { set; }
        Int64? VerificationRequestId { set; }
        String MatchStatusFlag { set; }
        DateTime? CisVerificationDate { set; }
        String Individual1099 { set; }
        Int64? CisParentVendorId { set; }
        DateTime? BusClassLastCertifiedDate { set; }
        Decimal? BusClassLastCertifiedBy { set; }
    } // IOdptPoVendorOt

    public interface IOdptScTtTblUniqueOt : IOdptBaseScTransTypeOt {
    } // IOdptScTtTblUniqueOt

    // **OBJECT IGNORED** - XMLTYPE type is not available in ODP.NET managed
    // public interface IOdptXmltypeOt {
} // Schema.Odpt.Odpt.Type.Object