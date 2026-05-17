# Package Review — DcsBriefop

Review date: 2026-05-17  
Target: `net10.0-windows`, WinForms

---

## Completed

| # | Change | Notes |
|---|---|---|
| 1 | **PuppeteerSharp → Microsoft.Web.WebView2** 1.0.2849.39 | `HtmlImageRenderer` rewritten using off-screen `Form`/`WebView2`; `CapturePreviewAsync()` replaces headless Chromium. Eliminates ~150 MB runtime download. |
| 2 | **GMap.NET → Mapsui** 5.0.2 + BruTile 6.0.0 | Full rewrite: tile display, custom SkiaSharp renderers (`BriefopMarker`, `BriefopLine`, `BriefopLabel`), offline kneeboard image generation via BruTile. Eliminates `System.Data.SqlClient` CVEs (GHSA-8g2p-5pqh-5jmc, GHSA-98g6-xh36-x2p7), `EntityFramework`, and `SQLite` dead weight. |
| 3 | **GDAL → ProjNet** 2.1.0 | GDAL was only used for Proj4 ↔ WGS84 transforms in 3 files. `Data/SpatialReference.cs` wraps ProjNet. Eliminates large native binary payload. |
| 4 | **DG.AdvancedDataGridView → ObjectListView.Repack.NET6Plus** 2.9.5 | `GridManagerBase<T>` rewritten; 10 subclasses ported to `AspectGetter` lambdas; 12 designer files updated. Per-column `FilterMenuBuilder` + Ctrl+F free-text search. Tested. |

---

## TODO

### HtmlTags → Scriban

| | Current | Proposed |
|---|---|---|
| Package | `HtmlTags` 10.0.0 | `Scriban` |
| Approach | Programmatic HTML building in C# | Template files in `Resources/Html/` + data binding |

Move HTML structure to `.html` template files; render with `Scriban.Template.Parse().Render(model)`. Migration can be done page by page — low risk.

---

## Keep As-Is

| Package | Notes |
|---|---|
| `CoordinateSharp` + `CoordinateSharp.Magnetic` | Good domain fit. |
| `UnitsNet` | Clean and well-maintained. |
| `Newtonsoft.Json` | `System.Text.Json` migration requires auditing all serialization attributes — not urgent. |
| `log4net` | On 3.3.1; not worth migrating to Serilog. |
| `CommandLineParser` | No functional gap vs `System.CommandLine`. |
