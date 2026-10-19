# SAP Business One READ API

ASP.NET Core 10 API using `Opx.Api.Web` and linq2db. Its API pattern follows the local OPX Chinook sample at `D:/projects/git/opx.api/api-samples/chinook/src`; table definitions come from the local BiUan HTML mirror at `D:/projects/git/scrapped-biuan/biuan.com`. No SAP write operation is implemented.

## Surface

- `GET /api/v1/catalog/domains`: 12 category names, descriptions, routes, and counts.
- `GET /api/v1/{category}/tables`: table summaries in one category.
- `GET /api/v1/{category}/tables/{code}`: description and field metadata, including physical column, readable property, source type, size, related-table references, and enum options.
- `GET /api/v1/{category}/tables/{code}/rows?skip=0&take=20`: one configured table read, with `skip` from 0 to 10000 and `take` from 1 to 100. The response preserves the OPX envelope and returns readable property names.

Category routes are `administration`, `banking`, `business-partners`, `finance`, `human-resources`, `inventory-and-production`, `marketing-documents-and-receipts`, `mrp`, `reports`, `sales-opportunities`, `service`, and `others`. The catalog has 2709 tables. Local detail pages provide mappings for 2547; the other 162 are metadata-only until their source pages are available. Each category has its own authorized controller. OpenAPI JSON is mapped in Development.

## Configuration and authorization

Set `Authentication__Authority`, `Authentication__Audience`, and `SapB1__CompanyId` in deployment configuration. Set `SapB1__ConnectionString` only in a secure environment/secret store, using a SQL account with SELECT-only permissions on the approved company database. `SapB1__SqlServerVersion` accepts `2019` or `2022`. There are no credentials in repository configuration. The service does not connect to SAP when only metadata is requested.

Every controller requires a valid bearer token with a `company` claim equal to `SapB1:CompanyId`. The catalog requires `sap-b1.catalog.read`; a category requires `sap-b1.{category-route}.read`, supplied as a `permission` claim or a space-separated `scope` claim. A category permission covers its metadata and any enabled row reads.

Row reads are disabled by default. For each approved table, configure its physical columns and a selected ordering column under `SapB1:Tables`. For example:

```json
{
  "SapB1": {
    "Tables": {
      "OITM": {
        "Columns": ["ItemCode", "ItemName"],
        "OrderBy": "ItemCode"
      }
    }
  }
}
```

Only those columns are projected by linq2db; the HTTP caller cannot provide SQL, table names outside the generated registry, or arbitrary columns. Choose an order column that is unique and stable for the intended paging semantics. The service fetches at most `take + 1` rows to calculate `hasMore` and does not issue a count query.

The BiUan HTML does not establish complete SQL keys, nullability, decimal scale, or every target schema variation. Before enabling a table in a company, compare its mapped columns and types with that company's schema and approve the returned fields. Relationship references and diagram arrows are retained as metadata and never automatically joined.

## Build and offline verification

```powershell
python scripts/generate_shared_db.py --verify
python scripts/generate_api_registry.py --verify
python scripts/generate_api_controllers.py --verify
python scripts/test_shared_db.py
python scripts/test_api_contract.py
dotnet build src/presentation/SapB1.Api.csproj -c Release --nologo
```

The generators write ordinary `.cs` files, never `*.g.cs`. No command above accesses SAP. A successful build or unauthenticated HTTP probe does not establish live schema compatibility, identity-provider integration, database authorization, or query performance.
