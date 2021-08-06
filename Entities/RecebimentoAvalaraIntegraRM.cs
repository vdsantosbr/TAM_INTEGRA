using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class RecebimentoAvalaraIntegraRM
    {
    }
}


public class Rootobject
{
    public bool hasNext { get; set; }
    public Item[] items { get; set; }
}

public class Item
{
    public string internalId { get; set; }
    public int companyId { get; set; }
    public int movementId { get; set; }
    public int branchId { get; set; }
    public string warehouseCode { get; set; }
    public string customerVendorCode { get; set; }
    public string number { get; set; }
    public string series { get; set; }
    public string movementTypeCode { get; set; }
    public string type { get; set; }
    public string status { get; set; }
    public bool printed { get; set; }
    public bool documentPrinted { get; set; }
    public bool billPrinted { get; set; }
    public DateTime registerDate { get; set; }
    public DateTime exitDate { get; set; }
    public DateTime extraDate1 { get; set; }
    public string commercialRepresentativeCode { get; set; }
    public float commercialRepresentativeCharge { get; set; }
    public string paymentTermCode { get; set; }
    public float grossValue { get; set; }
    public float netValue { get; set; }
    public float informedNetValue { get; set; }
    public float otherValues { get; set; }
    public float freightPercentage { get; set; }
    public float freightValue { get; set; }
    public float insurancePercentage { get; set; }
    public float insuranceValue { get; set; }
    public float discountPercentage { get; set; }
    public float discountValue { get; set; }
    public float expensePercentage { get; set; }
    public float expenseValue { get; set; }
    public float extraPercentage1 { get; set; }
    public float extraValue1 { get; set; }
    public float chargePercentage { get; set; }
    public float transportedProductNetWeight { get; set; }
    public float transportedProductGrossWeight { get; set; }
    public string financialOptionalTable2Code { get; set; }
    public string financialOptionalTable3Code { get; set; }
    public int financialEntryMovementId { get; set; }
    public string netValueCurrencyCode { get; set; }
    public DateTime date { get; set; }
    public int generatedEntryNumber { get; set; }
    public bool hasGeneratedBill { get; set; }
    public int openEntryNumber { get; set; }
    public int freightType { get; set; }
    public string auxCustomerVendorCode { get; set; }
    public int auxCustomerVendorCompanyId { get; set; }
    public string costCenterCode { get; set; }
    public string cashAccountCode { get; set; }
    public string salesman1Code { get; set; }
    public float salesman2ChargePercentage { get; set; }
    public int customerVendorCompanyId { get; set; }
    public string userCode { get; set; }
    public int destinyBranchId { get; set; }
    public int cashAccountCompanyId { get; set; }
    public bool lotGenerated { get; set; }
    public int accountingEventCode { get; set; }
    public int accountingExportStatus { get; set; }
    public int exportedAccountingLotCode { get; set; }
    public int fiscalNatureId { get; set; }
    public bool hasGeneratedWorkAccount { get; set; }
    public bool workAccountGenerated { get; set; }
    public DateTime lastEditTime { get; set; }
    public float indicateObjectUse { get; set; }
    public int accountingType { get; set; }
    public string fiscalBookkeepingEntryLot { get; set; }
    public bool bonumIntegrated { get; set; }
    public string entryCurrencyCode { get; set; }
    public bool processedFlag { get; set; }
    public float icmsDeductionValue { get; set; }
    public DateTime registerTime { get; set; }
    public string creationUser { get; set; }
    public DateTime creationDate { get; set; }
    public bool emailStatus { get; set; }
    public float internalGrossValue { get; set; }
    public int storeFrontBound { get; set; }
    public string serviceProviderCityCode { get; set; }
    public string serviceProviderStateCode { get; set; }
    public float otherCompanyINSSBaseValue { get; set; }
    public string documentTypeCode { get; set; }
    public float conditionalDiscountValue { get; set; }
    public float conditionalExpenseValue { get; set; }
    public DateTime accountingDate { get; set; }
    public int commercialAutomationExported { get; set; }
    public string aplicationIntegration { get; set; }
    public DateTime entryDate { get; set; }
    public int extemporaneous { get; set; }
    public string nfeReceiptStatus { get; set; }
    public int customerVendorHistoryId { get; set; }
    public float merchandiseValue { get; set; }
    public bool usesFinancialValueApportionment { get; set; }
    public string nFeAccesskey { get; set; }
    public int conclusionFlag { get; set; }
    public int totvsColabGeneratedStatus { get; set; }
    public bool paradigmaAutoIntegrated { get; set; }
    public float originalGrossValue { get; set; }
    public float originalNetValue { get; set; }
    public float originalOtherValues { get; set; }
    public int operationId { get; set; }
    public int scpBranchId { get; set; }
    public string recCreatedBy { get; set; }
    public DateTime recCreatedOn { get; set; }
    public string recModifiedBy { get; set; }
    public DateTime recModifiedOn { get; set; }
    public Complementaryfields complementaryFields { get; set; }
    public string longHistory { get; set; }
    public Movementitem[] movementItems { get; set; }
    public object[] payments { get; set; }
    public Costcenterapportionment1[] costCenterApportionments { get; set; }
    public object[] departmentApportionments { get; set; }
    public object[] taxes { get; set; }
    public Fiscal1[] fiscal { get; set; }
    public object[] norm { get; set; }
    public object[] cargoComponent { get; set; }
    public object[] thirdPartyNF { get; set; }
    public object[] safetyDevice { get; set; }
    public Nfe[] nfe { get; set; }
    public object[] inputCTRC { get; set; }
    public object[] outputCTRC { get; set; }
    public Ctrc[] ctrc { get; set; }
    public Transportdata[] transportData { get; set; }
    public object[] documentAuthorization { get; set; }
    public object[] judicialProcess { get; set; }
    public object[] serviceOrder { get; set; }
    public Relatedmovement[] relatedMovement { get; set; }
    public object[] exportRelatedMovement { get; set; }
    public object[] linkedMovement { get; set; }
    public object[] cTe { get; set; }
    public object[] eaiIntegration { get; set; }
}

public class Complementaryfields
{
}

public class Movementitem
{
    public int companyId { get; set; }
    public int movementId { get; set; }
    public int sequentialId { get; set; }
    public int sequentialNumber { get; set; }
    public int productId { get; set; }
    public float quantity { get; set; }
    public float unitPrice { get; set; }
    public float tablePrice { get; set; }
    public float discountPercentage { get; set; }
    public float discountValue { get; set; }
    public DateTime registerDate { get; set; }
    public int taxNumber { get; set; }
    public string measureUnitCode { get; set; }
    public float receivableQuantity { get; set; }
    public int contractItemSequentialNumber { get; set; }
    public float partialBillingReceivedValue { get; set; }
    public float completedValue { get; set; }
    public int stockEffectFlag { get; set; }
    public float unitValue { get; set; }
    public float financialValue { get; set; }
    public string costCenterCode { get; set; }
    public float ordinationTaxAliquot { get; set; }
    public float originalQuantity { get; set; }
    public int natureId { get; set; }
    public int flag { get; set; }
    public int blockObject { get; set; }
    public int rebillingFlag { get; set; }
    public float measureUnitConversionRate { get; set; }
    public float totalValue { get; set; }
    public int productCodeType { get; set; }
    public int branchId { get; set; }
    public int compositeProductId { get; set; }
    public float sortedQuantity { get; set; }
    public float commercialRepresentativeChargePercentage { get; set; }
    public float fiscalBookkeepingValue { get; set; }
    public float orderFinancialValue { get; set; }
    public float ctrcFreightValue { get; set; }
    public float optionalFormulaValue1 { get; set; }
    public float optionalFormulaValue2 { get; set; }
    public bool editedPrice { get; set; }
    public int unitaryOccupiedVolumeNumber { get; set; }
    public float conditionalDiscountValue { get; set; }
    public float conditionalExpenseValue { get; set; }
    public float freightApportionment { get; set; }
    public float insuranceApportionment { get; set; }
    public float discountApportionment { get; set; }
    public float expenseApportionment { get; set; }
    public float extraValue1Apportionment { get; set; }
    public float extraValue2Apportionment { get; set; }
    public float ctrcFreightApportionment { get; set; }
    public float materialDeductionApportionment { get; set; }
    public float subcontractingDeductionApportionment { get; set; }
    public float otherDeductionApportionment { get; set; }
    public float budgetUnitPrice { get; set; }
    public float nFeServiceValue { get; set; }
    public string warehouseCode { get; set; }
    public float heritageValue { get; set; }
    public float netValue { get; set; }
    public float grossValue { get; set; }
    public float originalGrossValue { get; set; }
    public float costCenterDepartmentApportionmentValue { get; set; }
    public float replenishmentCost { get; set; }
    public float bReplenishmentCost { get; set; }
    public float thirdPartyFinancialValue { get; set; }
    public float totalQuantity { get; set; }
    public int isSubstituteProduct { get; set; }
    public int unitPriceSelectionType { get; set; }
    public float financialValueApportionment { get; set; }
    public float completedQuantity { get; set; }
    public string aplicationIntegration { get; set; }
    public string productFantasyName { get; set; }
    public string productCode { get; set; }
    public string productAuxCode { get; set; }
    public string productReducedCode { get; set; }
    public string manufacturerProductNumber { get; set; }
    public string manufacturerCode { get; set; }
    public string itemNatureCode { get; set; }
    public Complementaryfields1 complementaryFields { get; set; }
    public string longHistory { get; set; }
    public Costcenterapportionment[] costCenterApportionments { get; set; }
    public object[] departmentApportionments { get; set; }
    public Tax[] taxes { get; set; }
    public object[] itemLots { get; set; }
    public object[] itemGrids { get; set; }
    public object[] itemSerialNumber { get; set; }
    public object[] itemHeritage { get; set; }
    public object[] siscoServFitting { get; set; }
    public Fiscal[] fiscal { get; set; }
    public object[] reserves { get; set; }
    public Relateditem[] relatedItem { get; set; }
    public object[] exportRelatedItem { get; set; }
    public object[] linkedItem { get; set; }
    public object[] exportMemo { get; set; }
    public object[] judicialProcess { get; set; }
}

public class Complementaryfields1
{
    public object bcinss { get; set; }
    public object obspf { get; set; }
}

public class Costcenterapportionment
{
    public int companyId { get; set; }
    public int apportionmentId { get; set; }
    public int movementId { get; set; }
    public int movementItemSequentialId { get; set; }
    public string costCenterCode { get; set; }
    public float value { get; set; }
    public string costCenterName { get; set; }
}

public class Tax
{
    public int companyId { get; set; }
    public int movementId { get; set; }
    public int movementItemSequentialId { get; set; }
    public string taxId { get; set; }
    public float calculationBasis { get; set; }
    public float aliquot { get; set; }
    public float value { get; set; }
    public float reductionFactory { get; set; }
    public float taxSubstitutionFactor { get; set; }
    public float calculatedCalculationBasis { get; set; }
    public int edited { get; set; }
    public string retentionCode { get; set; }
    public string baseTaxCode { get; set; }
    public float icmsPartialDeferralPercent { get; set; }
    public float fullBase { get; set; }
    public string collectionType { get; set; }
}

public class Fiscal
{
    public int companyId { get; set; }
    public int movementId { get; set; }
    public int movementItemSequentialId { get; set; }
    public float contractedQuantity { get; set; }
    public float approximateTotalTaxValue { get; set; }
    public int paaAcquisition { get; set; }
}

public class Relateditem
{
    public int originMovementCompanyId { get; set; }
    public int originMovementId { get; set; }
    public int originMovementItemSequentialId { get; set; }
    public int destinyMovementCompanyId { get; set; }
    public int destinyMovementId { get; set; }
    public int destinyMovementItemSequentialId { get; set; }
    public float quantity { get; set; }
}

public class Costcenterapportionment1
{
    public int companyId { get; set; }
    public int apportionmentId { get; set; }
    public int movementId { get; set; }
    public string costCenterCode { get; set; }
    public float value { get; set; }
    public string costCenterName { get; set; }
}

public class Fiscal1
{
    public int companyId { get; set; }
    public int movementId { get; set; }
    public int accreditedTaxpayer { get; set; }
    public int finalConsumerOperation { get; set; }
    public DateTime creditStartDate { get; set; }
    public int presentialOperation { get; set; }
    public int travelNumber { get; set; }
    public float portExpenseValue { get; set; }
    public float cargoExpenseValue { get; set; }
    public float navyFreightValue { get; set; }
    public float taxedWeight { get; set; }
    public float terrestrianRateValue { get; set; }
    public float adValoremRateValue { get; set; }
}

public class Nfe
{
    public int companyId { get; set; }
    public int movementId { get; set; }
    public float serviceValue { get; set; }
    public float serviceDeduction { get; set; }
    public float issAliquot { get; set; }
    public int retainedISS { get; set; }
    public float issValue { get; set; }
    public float iptuCreditValue { get; set; }
    public float calculationBasis { get; set; }
    public bool edited { get; set; }
}

public class Ctrc
{
    public int companyId { get; set; }
    public int movementId { get; set; }
    public float billValue { get; set; }
    public float apportionedValue { get; set; }
    public int billQuantity { get; set; }
    public int apportionedBillQuantity { get; set; }
    public float apportionCTRCValue { get; set; }
}

public class Transportdata
{
    public int companyId { get; set; }
    public int movementId { get; set; }
    public int withdrawMerchandise { get; set; }
    public int cTeType { get; set; }
    public int takerType { get; set; }
    public int takerICMSTaxpayer { get; set; }
    public int mdFeIssuerType { get; set; }
    public int bPeType { get; set; }
    public int capacity { get; set; }
    public int mdFeTransporterType { get; set; }
}

public class Relatedmovement
{
    public int originCompanyId { get; set; }
    public int originMovementId { get; set; }
    public int destinyCompanyId { get; set; }
    public int destinyMovementId { get; set; }
    public string relationType { get; set; }
    public int processId { get; set; }
}

