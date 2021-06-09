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
        String FlexibleColumn1 { get; init; }
        String FlexibleColumn2 { get; init; }
        String FlexibleColumn3 { get; init; }
        String FlexibleColumn4 { get; init; }
        String FlexibleColumn5 { get; init; }
        String FlexibleColumn6 { get; init; }
        String FlexibleColumn7 { get; init; }
        String FlexibleColumn8 { get; init; }
        String FlexibleColumn9 { get; init; }
        String FlexibleColumn10 { get; init; }
        String FlexibleColumn11 { get; init; }
        String FlexibleColumn12 { get; init; }
        String FlexibleColumn13 { get; init; }
        String FlexibleColumn14 { get; init; }
        String FlexibleColumn15 { get; init; }
        String FlexibleColumn16 { get; init; }
        String FlexibleColumn17 { get; init; }
        DateTime? FlexibleColumn18 { get; init; }
        DateTime? FlexibleColumn19 { get; init; }
        DateTime? FlexibleColumn20 { get; init; }
        Decimal? FlexibleColumn21 { get; init; }
        Decimal? FlexibleColumn22 { get; init; }
        Decimal? FlexibleColumn23 { get; init; }
        Decimal? FlexibleColumn24 { get; init; }
        Decimal? FlexibleColumn25 { get; init; }
    } // IOdptBaseScTransTypeOt

    public interface IOdptBigOt {
        Int64? Id { get; init; }
        Int64? AttrNumberId { get; init; }
        Int64? AttrInteger { get; init; }
        Int64? AttrInt { get; init; }
        Int64? AttrSmallint { get; init; }
        Decimal? AttrNumber { get; init; }
        Int64? AttrNumeric { get; init; }
        Decimal? AttrNumeric31Scale21 { get; init; }
        Int64? AttrDecimal { get; init; }
        Decimal? AttrDecima38Scale37 { get; init; }
        Decimal? AttrFloat { get; init; }
        Decimal? AttrReal { get; init; }
        Decimal? AttrDoublePrecision { get; init; }
        Single? AttrBinaryFloat { get; init; }
        Double? AttrBinaryDouble { get; init; }
        String AttrVarchar { get; init; }
        String AttrVarcharMax { get; init; }
        String AttrVarchar2 { get; init; }
        String AttrVarchar2Max { get; init; }
        String AttrNvarchar2 { get; init; }
        String AttrNvarchar2Max { get; init; }
        String AttrChar { get; init; }
        String AttrCharMax { get; init; }
        String AttrNchar { get; init; }
        String AttrNcharMax { get; init; }
        DateTime? AttrDate { get; init; }
        DateTime? AttrTimestamp { get; init; }
        DateTime? AttrTimestampWLTimeZone { get; init; }
        DateTimeOffset? AttrTimestampWTimeZone { get; init; }
        Byte[] AttrBlob { get; init; }
        String AttrClob { get; init; }
        String AttrNclob { get; init; }
        Int64? AttrLast { get; init; }
    } // IOdptBigOt

    // **OBJECT IGNORED** - Code generation for NESTED TABLE type has not been implemented
    // public interface IOdptNestedTableOt {

    public interface IOdptObjectOt {
        IOdptBigOt AttrOdptBigOt { get; init; }
    } // IOdptObjectOt

    public interface IOdptPoVendorOt : IOdptScTtTblUniqueOt {
        Int64? VendorId { get; init; }
        DateTime? LastUpdateDate { get; init; }
        Decimal? LastUpdatedBy { get; init; }
        String VendorName { get; init; }
        String Segment1 { get; init; }
        String SummaryFlag { get; init; }
        String EnabledFlag { get; init; }
        String Segment2 { get; init; }
        String Segment3 { get; init; }
        String Segment4 { get; init; }
        String Segment5 { get; init; }
        Decimal? LastUpdateLogin { get; init; }
        DateTime? CreationDate { get; init; }
        Decimal? CreatedBy { get; init; }
        Int64? EmployeeId { get; init; }
        Decimal? ValidationNumber { get; init; }
        String VendorTypeLookupCode { get; init; }
        String CustomerNum { get; init; }
        String OneTimeFlag { get; init; }
        Int64? ParentVendorId { get; init; }
        Decimal? MinOrderAmount { get; init; }
        Int64? ShipToLocationId { get; init; }
        Int64? BillToLocationId { get; init; }
        String ShipViaLookupCode { get; init; }
        String FreightTermsLookupCode { get; init; }
        String FobLookupCode { get; init; }
        Int64? TermsId { get; init; }
        Int64? SetOfBooksId { get; init; }
        String CreditStatusLookupCode { get; init; }
        Decimal? CreditLimit { get; init; }
        String AlwaysTakeDiscFlag { get; init; }
        String PayDateBasisLookupCode { get; init; }
        String PayGroupLookupCode { get; init; }
        Decimal? PaymentPriority { get; init; }
        String InvoiceCurrencyCode { get; init; }
        String PaymentCurrencyCode { get; init; }
        Decimal? InvoiceAmountLimit { get; init; }
        String ExchangeDateLookupCode { get; init; }
        String HoldAllPaymentsFlag { get; init; }
        String HoldFuturePaymentsFlag { get; init; }
        String HoldReason { get; init; }
        Int64? DistributionSetId { get; init; }
        Int64? AcctsPayCodeCombinationId { get; init; }
        Int64? DiscLostCodeCombinationId { get; init; }
        Int64? DiscTakenCodeCombinationId { get; init; }
        Int64? ExpenseCodeCombinationId { get; init; }
        Int64? PrepayCodeCombinationId { get; init; }
        String Num1099 { get; init; }
        String Type1099 { get; init; }
        String WithholdingStatusLookupCode { get; init; }
        DateTime? WithholdingStartDate { get; init; }
        String OrganizationTypeLookupCode { get; init; }
        String VatCode { get; init; }
        DateTime? StartDateActive { get; init; }
        DateTime? EndDateActive { get; init; }
        Decimal? QtyRcvTolerance { get; init; }
        String OverToleranceErrorFlag { get; init; }
        String MinorityGroupLookupCode { get; init; }
        String PaymentMethodLookupCode { get; init; }
        String BankAccountName { get; init; }
        String BankAccountNum { get; init; }
        String BankNum { get; init; }
        String BankAccountType { get; init; }
        String WomenOwnedFlag { get; init; }
        String SmallBusinessFlag { get; init; }
        String StandardIndustryClass { get; init; }
        String AttributeCategory { get; init; }
        String Attribute1 { get; init; }
        String Attribute2 { get; init; }
        String Attribute3 { get; init; }
        String Attribute4 { get; init; }
        String Attribute5 { get; init; }
        String HoldFlag { get; init; }
        String PurchasingHoldReason { get; init; }
        Int64? HoldBy { get; init; }
        DateTime? HoldDate { get; init; }
        String TermsDateBasis { get; init; }
        Decimal? PriceTolerance { get; init; }
        String InspectionRequiredFlag { get; init; }
        String ReceiptRequiredFlag { get; init; }
        String QtyRcvExceptionCode { get; init; }
        String EnforceShipToLocationCode { get; init; }
        Decimal? DaysEarlyReceiptAllowed { get; init; }
        Decimal? DaysLateReceiptAllowed { get; init; }
        String ReceiptDaysExceptionCode { get; init; }
        Int64? ReceivingRoutingId { get; init; }
        String AllowSubstituteReceiptsFlag { get; init; }
        String AllowUnorderedReceiptsFlag { get; init; }
        String HoldUnmatchedInvoicesFlag { get; init; }
        String ExclusivePaymentFlag { get; init; }
        DateTime? TaxVerificationDate { get; init; }
        String NameControl { get; init; }
        String StateReportableFlag { get; init; }
        String FederalReportableFlag { get; init; }
        String Attribute6 { get; init; }
        String Attribute7 { get; init; }
        String Attribute8 { get; init; }
        String Attribute9 { get; init; }
        String Attribute10 { get; init; }
        String Attribute11 { get; init; }
        String Attribute12 { get; init; }
        String Attribute13 { get; init; }
        String Attribute14 { get; init; }
        String Attribute15 { get; init; }
        Int64? RequestId { get; init; }
        Int64? ProgramApplicationId { get; init; }
        Int64? ProgramId { get; init; }
        DateTime? ProgramUpdateDate { get; init; }
        String OffsetVatCode { get; init; }
        String VatRegistrationNum { get; init; }
        String AutoCalculateInterestFlag { get; init; }
        String ExcludeFreightFromDiscount { get; init; }
        String TaxReportingName { get; init; }
        String CheckDigits { get; init; }
        String BankNumber { get; init; }
        String AllowAwtFlag { get; init; }
        Int64? AwtGroupId { get; init; }
        String VendorNameAlt { get; init; }
        String ApTaxRoundingRule { get; init; }
        String AutoTaxCalcFlag { get; init; }
        String AutoTaxCalcOverride { get; init; }
        String AmountIncludesTaxFlag { get; init; }
        String GlobalAttribute1 { get; init; }
        String GlobalAttribute2 { get; init; }
        String GlobalAttribute3 { get; init; }
        String GlobalAttribute4 { get; init; }
        String GlobalAttribute5 { get; init; }
        String GlobalAttribute6 { get; init; }
        String GlobalAttribute7 { get; init; }
        String GlobalAttribute8 { get; init; }
        String GlobalAttribute9 { get; init; }
        String GlobalAttribute10 { get; init; }
        String GlobalAttribute11 { get; init; }
        String GlobalAttribute12 { get; init; }
        String GlobalAttribute13 { get; init; }
        String GlobalAttribute14 { get; init; }
        String GlobalAttribute15 { get; init; }
        String GlobalAttribute16 { get; init; }
        String GlobalAttribute17 { get; init; }
        String GlobalAttribute18 { get; init; }
        String GlobalAttribute19 { get; init; }
        String GlobalAttribute20 { get; init; }
        String GlobalAttributeCategory { get; init; }
        String EdiTransactionHandling { get; init; }
        String EdiPaymentMethod { get; init; }
        String EdiPaymentFormat { get; init; }
        String EdiRemittanceMethod { get; init; }
        String EdiRemittanceInstruction { get; init; }
        String BankChargeBearer { get; init; }
        String BankBranchType { get; init; }
        String MatchOption { get; init; }
        Int64? FutureDatedPaymentCcid { get; init; }
        String CreateDebitMemoFlag { get; init; }
        String OffsetTaxFlag { get; init; }
        Int64? UniqueTaxReferenceNum { get; init; }
        Int64? PartnershipUtr { get; init; }
        String PartnershipName { get; init; }
        String CisEnabledFlag { get; init; }
        String FirstName { get; init; }
        String SecondName { get; init; }
        String LastName { get; init; }
        String Salutation { get; init; }
        String TradingName { get; init; }
        String WorkReference { get; init; }
        String CompanyRegistrationNumber { get; init; }
        String NationalInsuranceNumber { get; init; }
        String VerificationNumber { get; init; }
        Int64? VerificationRequestId { get; init; }
        String MatchStatusFlag { get; init; }
        DateTime? CisVerificationDate { get; init; }
        String Individual1099 { get; init; }
        Int64? CisParentVendorId { get; init; }
        DateTime? BusClassLastCertifiedDate { get; init; }
        Decimal? BusClassLastCertifiedBy { get; init; }
    } // IOdptPoVendorOt

    public interface IOdptScTtTblUniqueOt : IOdptBaseScTransTypeOt {
    } // IOdptScTtTblUniqueOt

    // **OBJECT IGNORED** - XMLTYPE type is not available in ODP.NET managed
    // public interface IOdptXmltypeOt {
} // Schema.Odpt.Odpt.Type.Object