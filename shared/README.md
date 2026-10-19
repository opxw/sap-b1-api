# SAP B1 shared DB library

`SapB1.Shared.Db` contains one domain model per table with a local detail page and one linq2db `AppDbContext` per BiUan SQL category. All source files use the ordinary `.cs` extension. The host supplies company-scoped `DataOptions`; this library stores no server address or credentials. It adds no write methods, HTTP endpoints, or automatic associations. Because the OPX base context exposes a database connection, the host must also enforce a SELECT-only database account and authorized read services.

The source is the SAP Business One 10.0 HTML mirror at `D:/projects/git/scrapped-biuan/biuan.com`. The index lists 2,709 tables in 12 categories. The generator maps the 2,547 available detail pages and records 162 unavailable pages in [`catalog.generated.json`](SapB1.Shared.Db/catalog.generated.json). Missing tables are **not** exposed as `ITable<T>` because their columns cannot be verified from this source.

| Domain | Tabel indeks | Model tersedia | Keterangan domain |
| --- | ---: | ---: | --- |
| Administration | 324 | 324 | Konfigurasi perusahaan, pengguna, otorisasi, dan parameter sistem. |
| Banking | 135 | 135 | Rekening bank, pembayaran, penerimaan, dan proses perbankan. |
| Business Partners | 78 | 78 | Master pelanggan, pemasok, kontak, alamat, dan atribut mitra bisnis. |
| Finance | 230 | 230 | Akun buku besar, jurnal, pajak, aset, dan konfigurasi keuangan. |
| Human Resources | 28 | 28 | Karyawan dan referensi sumber daya manusia. |
| Inventory and Production | 365 | 365 | Master item, persediaan, gudang, produksi, dan pergerakan stok. |
| Marketing Documents and Receipts | 928 | 928 | Dokumen pembelian, penjualan, baris dokumen, dan penerimaan barang. |
| MRP | 10 | 10 | Perencanaan kebutuhan material dan hasil perhitungannya. |
| Reports | 107 | 107 | Definisi, parameter, dan keluaran laporan. |
| Sales Opportunities | 20 | 20 | Peluang penjualan, tahapan, dan aktivitasnya. |
| Service | 45 | 45 | Panggilan layanan, kontrak, solusi, dan aktivitas servis. |
| Others | 439 | 277 | Tabel lintas fungsi; 162 entri indeks belum mempunyai halaman detail lokal. |

Table names come from each category index (for example, AADM “Administration - Log” becomes `AdministrationLogRow`). Field descriptions produce readable C# property names; `[Table]` and `[Column]` preserve SAP SQL identifiers. Table/category and property descriptions are available as `DescriptionAttribute`, and the catalog retains each field's physical name, source type, size, constraints, Related links, and enum options. Complete coded option lists become nullable enum properties with linq2db `MapValue` attributes. Ambiguous or incomplete constraints remain source metadata, not fabricated enum values.

`Num` maps to `decimal?`, `Int`/`Identity` to `int?`, `Date` to `DateTime?`, and text types to `string?`. The HTML contains no populated primary-key markers and does not establish nullability or precise numeric scale, so generated models do not claim keys, required fields, or physical foreign keys. Validate against the actual company schema and SAP version before using a model in a runtime query. User-defined `U_*` fields are excluded.

[`relationship-map.generated.json`](SapB1.Shared.Db/relationship-map.generated.json) preserves the source diagram nodes and directed arrows. `arrows:'from'` points toward the `from` node, so the stored direction is reversed to match the drawn arrow. These edges are table-level documentation, not verified column joins. The source SELECT example for each available table is saved under `Samples/<Category>/<Code>.sql` for inspection only; the library never executes it automatically.

Regenerate and verify locally:

```powershell
python scripts/generate_shared_db.py
python scripts/generate_shared_db.py --verify
dotnet build shared/SapB1.Shared.Db/SapB1.Shared.Db.csproj -c Release
```

The generator reads only local HTML. It does not connect to SAP or fetch the web.
