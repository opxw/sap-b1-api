# Opx API Docs

Generated at: 2026-10-05 09:52:13 +07:00

## GET /api/v1/administration/tables

Controller: `AdministrationTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/administration/tables/{code}

Controller: `AdministrationTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/administration/tables/{code}/rows

Controller: `AdministrationTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/banking/tables

Controller: `BankingTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/banking/tables/{code}

Controller: `BankingTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/banking/tables/{code}/rows

Controller: `BankingTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/business-partners/tables

Controller: `BusinessPartnersTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/business-partners/tables/{code}

Controller: `BusinessPartnersTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/business-partners/tables/{code}/rows

Controller: `BusinessPartnersTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/catalog/domains

Controller: `Catalog`
Action: `Domains`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/finance/tables

Controller: `FinanceTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/finance/tables/{code}

Controller: `FinanceTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/finance/tables/{code}/rows

Controller: `FinanceTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/human-resources/tables

Controller: `HumanResourcesTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/human-resources/tables/{code}

Controller: `HumanResourcesTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/human-resources/tables/{code}/rows

Controller: `HumanResourcesTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/inventory-and-production/tables

Controller: `InventoryAndProductionTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/inventory-and-production/tables/{code}

Controller: `InventoryAndProductionTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/inventory-and-production/tables/{code}/rows

Controller: `InventoryAndProductionTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/marketing-documents-and-receipts/tables

Controller: `MarketingDocumentsAndReceiptsTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/marketing-documents-and-receipts/tables/{code}

Controller: `MarketingDocumentsAndReceiptsTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/marketing-documents-and-receipts/tables/{code}/rows

Controller: `MarketingDocumentsAndReceiptsTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/mrp/tables

Controller: `MrpTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/mrp/tables/{code}

Controller: `MrpTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/mrp/tables/{code}/rows

Controller: `MrpTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /opx/logs/access

Controller: `OpxLogs`
Action: `Access`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| date | Query | String | False | - |
| take | Query | Int32 | True | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /opx/logs/security-issues

Controller: `OpxLogs`
Action: `SecurityIssues`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| date | Query | String | False | - |
| take | Query | Int32 | True | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /opx/protection/health

Controller: `OpxProtection`
Action: `Health`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /opx/protection/metrics

Controller: `OpxProtection`
Action: `Metrics`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/others/tables

Controller: `OthersTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/others/tables/{code}

Controller: `OthersTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/others/tables/{code}/rows

Controller: `OthersTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/reports/tables

Controller: `ReportsTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/reports/tables/{code}

Controller: `ReportsTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/reports/tables/{code}/rows

Controller: `ReportsTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/sales-opportunities/tables

Controller: `SalesOpportunitiesTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/sales-opportunities/tables/{code}

Controller: `SalesOpportunitiesTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/sales-opportunities/tables/{code}/rows

Controller: `SalesOpportunitiesTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/service/tables

Controller: `ServiceTables`
Action: `Tables`

### Parameters

-

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/service/tables/{code}

Controller: `ServiceTables`
Action: `Table`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

## GET /api/v1/service/tables/{code}/rows

Controller: `ServiceTables`
Action: `Rows`

### Parameters

| Name | Source | Type | Required | Properties |
| --- | --- | --- | --- | --- |
| code | Path | String | False | - |
| skip | Query | Int32 | True | - |
| take | Query | Int32 | True | - |
| cancellationToken | Special | CancellationToken | True | IsCancellationRequested: Boolean<br>CanBeCanceled: Boolean<br>WaitHandle: WaitHandle |

### Output

| StatusCode | Type |
| --- | --- |
| 200 | void |

