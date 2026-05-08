# Package Review — DcsBriefop

Review date: 2026-05-03  
Current target: `net10.0-windows`, WinForms

---

## Priority Changes

### 1. HTML-to-Image: PuppeteerSharp → WebView2 ⚡ HIGH

| | Current | Proposed |
|---|---|---|
| Package | `PuppeteerSharp` 24.40.0 | `Microsoft.Web.WebView2` |
| Binary overhead | ~150 MB Chromium download on first run | 0 — Edge runtime ships with Win 10/11 |
| Cold start | Slow (browser launch) | Near-instant |

**How:** Replace `HtmlImageRenderer.cs` — use `CoreWebView2.CapturePreviewAsync()` to render HTML to a stream.  
**Risk:** WebView2 runtime must be present (it is on Win 10/11 by default; verify min OS requirement).  
**Status:** ✅ Done — `PuppeteerSharp` removed, `Microsoft.Web.WebView2` 1.0.2849.39 added. `HtmlImageRenderer` rewritten as a static class using an off-screen `Form`/`WebView2` host; `BopBriefingPage.BuildHtmlImage` simplified to a one-liner.

---

### 2. Maps: GMap.NET → Mapsui 🗺️ HIGH (large effort)

| | Current | Proposed |
|---|---|---|
| Package | `GMap.NET.WinForms` 2.1.7 | `Mapsui` + `Mapsui.Rendering.Skia` |
| Tile caching | Broken — forced `ServerOnly` mode due to thread cleanup bug | Proper async tile caching via BruTile |
| Polygon rendering | Commented out in bulk generation (`ToolsMap.cs:395-433`) | Supported natively |
| Maintenance | Community, sporadic | Actively maintained, supports WinForms/WPF/MAUI |
| **CVE driver** | Pulls `System.Data.SqlClient` 4.8.3 (2 CVEs: GHSA-8g2p-5pqh-5jmc + GHSA-98g6-xh36-x2p7) via GMap.NET.Core's MSSQL tile cache feature (never used) | No SqlClient dependency |

**Note on CVE:** No safe version of `System.Data.SqlClient` to pin to — the package tops out at 4.8.3 on NuGet; all security fixes went to `Microsoft.Data.SqlClient` instead. Removing GMap.NET is the only real fix. GMap.NET.Core also drags in `EntityFramework 6.4.4` and `Stub.System.Data.SQLite.Core.NetStandard` as unused dead weight.

#### Migration scope (audited)

| Area | Complexity | Notes |
|---|---|---|
| `GMapControl` in `FrmTheatre` + `UcMap` + their designers | Medium | New control API, event rewiring |
| `GMarkerBriefop`, `GTextBriefop` | Medium | Custom GDI+ `OnRender` → SkiaSharp symbol style |
| `GLineBriefop` | **High** | Bitmap textures along paths, text panels with arrows, polygon fill — all GDI+, needs full rewrite in SkiaSharp |
| `ToolsMap.GenerateMapImage()` | **High** | Directly calls GMap tile APIs (`Projection.FromLatLngToPixel`, `GMaps.Instance.GetImageFrom`) for offline kneeboard image generation — no direct Mapsui equivalent |
| `WMSProvider` (custom `GMapProvider`) | Medium | Port WMS URL builder to BruTile `WmsTileSource` |
| `JsonSerializers.cs` (overlay serializers) | Low | Type changes only |
| ~20 files passing `GMapOverlay` / `PointLatLng` | Low | Type substitutions |

#### Proposed migration phases

1. **Tile display** — Replace `GMapControl` in `FrmTheatre` and `UcMap` with Mapsui control + tile layers. Keep GMap types for overlays temporarily.
2. **Markers** — Port `GMarkerBriefop` and `GTextBriefop` to Mapsui `PointFeature` + `SymbolStyle`.
3. **Lines** — Port `GLineBriefop` to Mapsui `LineString` feature with custom SkiaSharp renderer.
4. **Offline generation** — Rewrite `ToolsMap.GenerateMapImage()` using BruTile tile fetching directly (no Mapsui control needed for this path).
5. **Cleanup** — Remove `GMapOverlay`, `PointLatLng` from data/briefing layers; update JSON serializers.

**Status:** ✅ Done — GMap.NET fully removed; all phases complete including offline map generation via BruTile, custom SkiaSharp renderers, and JSON serializer updates.

---

### 3. HTML Generation: HtmlTags → Scriban 📄 LOW

| | Current | Proposed |
|---|---|---|
| Package | `HtmlTags` 10.0.0 | `Scriban` |
| Approach | Programmatic HTML building in C# | Template files (`.html`) + data binding |
| Readability | Nested imperative code | Separated structure and data |

**How:** Move HTML structure to `Resources/Html/` template files. Render with `Scriban.Template.Parse().Render(model)`.  
**Note:** Already have `Resources/Html/` with CSS/JS — fits naturally.  
**Risk:** Low. Templates are decoupled; migration can be done page by page.  
**Status:** TODO

---

### 4. GDAL → ProjNet ⚡ HIGH (easy win)

| | Current | Proposed |
|---|---|---|
| Packages | `GDAL` 3.12.1 + `MaxRev.Gdal.WindowsRuntime.Minimal` 3.12.3 | `ProjNet` |
| Native binaries | Yes — large native payload, P/Invoke | None — pure managed .NET |
| Usage | `SpatialReference`, `CoordinateTransformation`, `TransformPoint` | Direct equivalents in ProjNet |

**Why GDAL is used:** Exclusively to transform DCS theatre-local coordinates (Proj4 strings per theatre) to/from WGS84 lat/lon. That's it — 3 types, 3 files.  
**Affected files:** `Data/TheatreProjection.cs`, `Data/Theatre.cs`, `Tools/ToolsCoordinate.cs`  
**Risk:** Low. The API is slightly different but the concept is identical. `CoordinateSharp` is unaffected (it handles display formatting, not projection math).  
**Status:** ✅ Done — `GDAL` + `MaxRev.Gdal.WindowsRuntime.Minimal` removed, `ProjNet` 2.1.0 added. New `Data/SpatialReference.cs` wraps Proj4 parsing.

---

### 5. Grid: DG.AdvancedDataGridView → FastObjectListView 📋 MEDIUM

| | Current | Proposed |
|---|---|---|
| Package | `DG.AdvancedDataGridView` 1.2.30115.18 | `BrightIdeasSoftware.ObjectListView2` |
| Renderer | `DataGridView` (GDI+, renders all rows) | `FastObjectListView` (virtual mode, renders only visible rows) |
| Data binding | `DataTable` + `BindingSource` + `DataView` | Direct POCO object list — no intermediate data layer |
| Filtering UI | Column-header filter dropdowns (ADGV feature) | `TextMatchFilter` + TextBox, or `FilterMenuBuilder` for header dropdowns |
| Sorting | Click column header (ADGV feature) | Built-in, click column header |

**Root cause of slowness:** `DataTable` + `BindingSource` + `DataView` pipeline — not ADGV itself. `FastObjectListView` virtual mode renders only visible rows regardless of dataset size.

#### Migration scope (audited)

| Area | Complexity | Notes |
|---|---|---|
| `GridManagerBase<T>` internals | Medium | Full rewrite of private layer — `DataTable`/`BindingSource`/`DataRow` gone; aspect getters replace row-population |
| 12 concrete `GridManager` subclasses | Low | `InitializeDataSourceColumns()` + `RefreshDataSourceRowContent()` collapse into one `InitializeColumns()` override with `AspectGetter` lambdas |
| 12 Designer files | Low | Swap `AdvancedDataGridView` for `FastObjectListView` control |
| Public API of `GridManagerBase` | None | `Elements`, `CheckedElements`, `GetSelectedElements()`, `SelectRow()`, `Refresh()`, events — all unchanged |
| External form code | None | Everything outside `GridManager*` files untouched |

**Open question before starting:** Replace column-header filter dropdowns with a `TextBox` filter above the grid (simpler, common OLV pattern), or use OLV's `FilterMenuBuilder` to keep the header-dropdown UX?

**Status:** TODO

---

## Keep As-Is

| Package | Verdict | Notes |
|---|---|---|
| `CoordinateSharp` + `CoordinateSharp.Magnetic` | ✅ Keep | Good fit for the domain. |
| `UnitsNet` | ✅ Keep | Clean and well-maintained. |
| `Newtonsoft.Json` | Keep for now | `System.Text.Json` (built-in) is faster, but migration requires auditing serialization attributes. Not urgent. |
| `log4net` | ✅ Keep | Just upgraded to 3.3.1. Not worth migrating to Serilog now. |
| `CommandLineParser` | Keep | `System.CommandLine` is the Microsoft alternative, but no functional gap here. |
| `DG.AdvancedDataGridView` | ❌ Replace — see item 5 | still in use |

---

## Open Questions

- [x] What is GDAL actually used for? → Only Proj4 coordinate projection transforms. Replace with ProjNet (see item 4).
- [x] Is tile caching a real user-facing problem? → Currently disabled (`ServerOnly` mode) due to GMap.NET thread bug — users are already online-only. Mapsui's BruTile has proper async caching so this would be restored as part of the migration.
