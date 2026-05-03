# GMap.NET → Mapsui Migration Plan

Scope: item 2 of PACKAGE_REVIEW.md  
Audited: 2026-05-03

---

## Goal

Replace `GMap.NET.WinForms` 2.1.7 (and its transitive `System.Data.SqlClient` CVEs, `EntityFramework`, and `SQLite` dead weight) with `Mapsui` + `Mapsui.Rendering.Skia`. Restore proper async tile caching. Enable polygon/fill rendering currently commented out.

---

## NuGet Changes

**Remove (Phase 5):**
- `GMap.NET.Core`
- `GMap.NET.WinForms`
- (transitive: `System.Data.SqlClient`, `EntityFramework`, `Stub.System.Data.SQLite.Core.NetStandard` removed automatically)

**Added (all 5.0.2) ✅:**
- `Mapsui` — core map model, layers, projections
- `Mapsui.WindowsForms` — WinForms control (`Mapsui.UI.WindowsForms.MapControl`)
- `Mapsui.Rendering.Skia` — SkiaSharp renderer
- `Mapsui.Nts` — `GeometryFeature` / NTS LineString, Polygon
- `Mapsui.Tiling` — `TileLayer`, `KnownTileSources`, BruTile integration
- BruTile 6.0.0 pulled transitively

---

## Mapsui 5.x API Reference (verified)

These differ from the 4.x docs and from what the plan originally said.

| Need | API |
|---|---|
| Control type | `Mapsui.UI.WindowsForms.MapControl` |
| Add tile layer | `mapControl.Map.Layers.Add(new TileLayer(source))` |
| Navigate to position + zoom | `mapControl.Map.Navigator.CenterOnAndZoomTo(MPoint, double resolution, 0L, null)` |
| Zoom by integer level | `mapControl.Map.Navigator.ZoomToLevel(int)` |
| Read current centre | `new MPoint(viewport.CenterX, viewport.CenterY)` (no `viewport.Center` property) |
| Screen → world | `ViewportExtensions.ScreenToWorld` exists but doesn't resolve via `using Mapsui` — use `MapProjection.ScreenToGeoPoint(viewport, x, y)` instead |
| `SphericalMercator.FromLonLat` | Returns `(double x, double y)` tuple, not `MPoint` |
| `KnownTileSource` values | `OpenStreetMap`, `EsriWorldTopo`, `EsriWorldPhysical`, `EsriWorldShadedRelief`, `BingAerial/Hybrid/Roads` (no `EsriWorldStreetMap`, `EsriWorldImagery`, `OpenStreetMapDE`) |
| Custom tile URL | Implement `IUrlBuilder` with `Uri GetUrl(TileInfo)` — `TileInfo.Extent` gives EPSG:3857 bbox directly |
| `HttpTileSource` ctor | `new HttpTileSource(ITileSchema, IUrlBuilder, string name, null, null, null)` — last 3 params required, pass null |

---

## Type Mapping Reference

| GMap.NET | Mapsui / replacement |
|---|---|
| `PointLatLng` | `GeoPoint` (data-layer struct, no map lib dependency) |
| `GPoint` | `MPoint` |
| `RectLatLng` | `MRect` |
| `GMapControl` | `Mapsui.UI.WindowsForms.MapControl` |
| `GMapOverlay` | `Mapsui.Layers.MemoryLayer` |
| `GMapMarker` | `PointFeature` + `BriefopMarkerStyle` (Phase 2) |
| `GMapRoute` | `Mapsui.Nts.GeometryFeature` (LineString) (Phase 3) |
| `GMapProvider` | `BruTile.ITileSource` + `TileLayer` |
| `Projection.FromLatLngToPixel` | `MapProjection.ToMPoint(lat, lng)` |
| `Projection.FromPixelToLatLng` | `MapProjection.ToGeoPoint(MPoint)` |
| `MapControl.FromLocalToLatLng(x,y)` | `MapProjection.ScreenToGeoPoint(viewport, x, y)` |

---

## Shared Infrastructure ✅ Done

### `Data/GeoPoint.cs` ✅
```csharp
internal readonly struct GeoPoint(double dLat, double dLng)
{
    public double Latitude { get; } = dLat;
    public double Longitude { get; } = dLng;
}
```

### `Map/MapProjection.cs` ✅
Static helpers: `ToMPoint(GeoPoint)`, `ToMPoint(double lat, double lng)`, `ToGeoPoint(MPoint)`,
`ZoomToResolution(int)`, `ResolutionToZoom(double)`, `ScreenToGeoPoint(Viewport, x, y)`.

`ScreenToGeoPoint` is a manual viewport transform (avoids the unresolvable `ViewportExtensions` extension):
```csharp
double dWorldX = viewport.CenterX + (dScreenX - viewport.Width  / 2.0) * viewport.Resolution;
double dWorldY = viewport.CenterY - (dScreenY - viewport.Height / 2.0) * viewport.Resolution;
```

---

## Phase 1 — Tile Display ✅ Done

**Files changed:**
- [Map/MapProviders.cs](Map/MapProviders.cs) ✅ — `record MapProviderEntry(string Name, Func<ITileSource> Factory)`, `FillCombo`, `TryGetEntry`, `CreateTileLayer`
- [Map/WMSProvider.cs](Map/WMSProvider.cs) ✅ — static factory + `WmsUrlBuilder : IUrlBuilder` using `TileInfo.Extent` for bbox
- [Tools/ToolsMap.cs](Tools/ToolsMap.cs) ✅ — new `InitializeMapControl(MapControl, string)` extension; GMap overloads kept with `// TODO Phase 5` comment
- [Forms/FrmTheatre.cs](Forms/FrmTheatre.cs) + designer ✅
- [Forms/UcMap.cs](Forms/UcMap.cs) + designer ✅
- [Forms/FrmPreferences.cs](Forms/FrmPreferences.cs) ✅ — `TryGetEntry` / `MapProviderEntry`
- [Forms/FrmMissionMaps.cs](Forms/FrmMissionMaps.cs) ✅ — `TryGetEntry` / `MapProviderEntry`

**What was deferred (has TODO comments in code):**
- `UcMap.DataToOverlay()` — no layers added yet; Phase 2 restores marker layer, Phase 3 restores static overlay layers
- `UcMap.OverlayToData()` — stub; Phase 2 restores
- `UcMap.StaticOverlays` property — still typed `IEnumerable<GMapOverlay>` so callers compile; Phase 3 changes the type to `IEnumerable<ILayer>`
- `FrmTheatre.DisplayCurrentTheatre()` — theatre centre marker and airdrome markers removed; Phase 2 restores via `MemoryLayer`
- Marker click/hover/drag in `UcMap` — all GMap events removed; Phase 2 wires `MapControl.Info` event
- `UcMarkerDetail.cs` — untouched; still references `GMarkerBriefop` and `GMapControl`; Phase 2 updates its constructor

**GMap stubs kept in ToolsMap.cs (TODO Phase 5 to remove):**
- `InitializeGMaps()` — still called from `Program.cs` and `FrmMain.cs`
- `InitializeMapControl(GMapControl, string/GMapProvider)` — still called from `UcGroup.cs`, `UcAirbase.cs`
- `ForceRefresh(GMapControl)` — still called from `UcAirbase`, `UcGroupUnits`, `UcGroupRoutePoints`, `UcGroupInformation`

---

## Phase 2 — Markers ⬜ Next

**Goal:** Port `GMarkerBriefop` and `GTextBriefop` to Mapsui point features with SkiaSharp rendering. Restore marker display in `FrmTheatre` and `UcMap`. Wire marker click/hover/drag.

**Files to create:**
- `Map/BriefopMarker.cs` — plain data class, replaces `GMarkerBriefop`
- `Map/BriefopTextMarker.cs` — plain data class, replaces `GTextBriefop`
- `Map/BriefopMarkerStyle.cs` — `class BriefopMarkerStyle : BaseStyle`
- `Map/BriefopMarkerStyleRenderer.cs` — `ISkiaSharpStyleRenderer`, ports GDI+ `OnRender` to SkiaSharp
- `Map/BriefopTextStyle.cs` + `Map/BriefopTextStyleRenderer.cs` — same for text markers

**Files to update:**
- [DataMiz/MizBopMap.cs](DataMiz/MizBopMap.cs) — `CustomMarkers: List<GMarkerBriefop>` → `List<BriefopMarker>`; `BuildCustomMapOverlay()` → `BuildCustomLayer() -> MemoryLayer`
- [JsonSerializers.cs](JsonSerializers.cs) — `GMarkerBriefopJsonConverter` → `BriefopMarkerJsonConverter`; `GMapOverlayJsonConverter` → `BriefopLayerJsonConverter`
- [Forms/FrmTheatre.cs](Forms/FrmTheatre.cs) — restore theatre centre + airdrome markers via `MemoryLayer`; restore airdrome click via `MapControl.Info` event
- [Forms/UcMap.cs](Forms/UcMap.cs) — restore `DataToOverlay`, `OverlayToData`, `AddMarker`, `DeleteMarker`, `SelectMarker`; wire `MapControl.Info` for click/hover
- [Forms/UcMarkerDetail.cs](Forms/UcMarkerDetail.cs) — change constructor `(GMarkerBriefop, GMapControl)` → `(BriefopMarker, Action refreshMap)`

**Key design notes:**

`BriefopMarker` is a plain data holder (no `GMapMarker` base):
```csharp
internal class BriefopMarker
{
    public GeoPoint Position { get; set; }
    public string TemplateName { get; set; }
    public Color? TintColor { get; set; }
    public string Label { get; set; }
    public int iScale { get; set; }
    public int iAngle { get; set; }
    public bool IsHovered { get; set; }
    public bool IsSelected { get; set; }
    public bool IsPressed { get; set; }

    public PointFeature ToPointFeature() { ... }
}
```

`BriefopMarkerStyleRenderer` ports `GMarkerBriefop.OnRender(Graphics g)` to SkiaSharp:
- Load template bitmap as `SKBitmap` (from same `Resources/Markers/` path)
- Rotation: `canvas.RotateDegrees(iAngle)`
- Tint: `SKColorFilter.CreateBlendMode(color, SKBlendMode.SrcIn)`
- Label: `canvas.DrawText(...)` with `SKPaint`

Registration at startup: `SkiaStyleRendererRegistry.Instance.Register(typeof(BriefopMarkerStyle), new BriefopMarkerStyleRenderer())`.

`UcMap` marker interaction replaces the three GMap events (`OnMarkerClick`, `OnMarkerEnter`, `OnMarkerLeave`) with a single `MapControl.Info` event handler that inspects `e.MapInfo.Feature["data"]`.

**JSON compatibility:** Keep field names `lat`, `lng`, `template`, `scale`, `angle`, `color`, `label` — existing saved files stay valid. Drop `ISerializable`/`IDeserializationCallback` (binary serialization, unused).

---

## Phase 3 — Lines ⬜

**Goal:** Port `GLineBriefop` to `GeometryFeature` (NTS LineString) with custom SkiaSharp renderer. Update `ToolsMap.AddMizDrawingObject*`. Change `UcMap.StaticOverlays` type.

**Files to create:**
- `Map/BriefopLine.cs` — plain data class; `ToGeometryFeature() -> GeometryFeature`
- `Map/BriefopLineStyle.cs` + `Map/BriefopLineStyleRenderer.cs`

**Files to update:**
- [Map/GLineBriefop.cs](Map/GLineBriefop.cs) — replace with `BriefopLine.cs`
- [Tools/ToolsMap.cs](Tools/ToolsMap.cs) — rewrite `AddMizDrawingObject*` methods
- [Forms/UcMap.cs](Forms/UcMap.cs) — change `StaticOverlays: IEnumerable<GMapOverlay>` → `IEnumerable<ILayer>`; restore `DataToOverlay` for static layers
- All callers that pass `GMapOverlay` as `StaticOverlays` (BopCoalition, BopMission, BopBriefingPage)

**Renderer complexity note:** `GLineBriefop.Render` has three sub-paths to port:
- `DrawSegment` — dashed/solid stroke → `SKPath` + `SKPaint` + `SKPathEffect.CreateDash()`
- `DrawSegmentBitmap` — repeating bitmap texture along segment → `SKShader.CreateBitmap`
- `DrawPanel` — text annotation box with optional arrow → `SKCanvas.DrawRect` + `DrawText` with rotation

Polygon fill (currently commented out at `ToolsMap.cs:395-433`) restores via closed `Polygon` geometry + `IsFilled = true` on the style.

---

## Phase 4 — Offline Map Image Generation ⬜

**Goal:** Rewrite `ToolsMap.GenerateMapImage()` to use BruTile tile fetching directly (no `GMapControl`). Output is `SKBitmap` instead of `Bitmap`.

**Files to update:**
- [Tools/ToolsMap.cs](Tools/ToolsMap.cs) — `GenerateMapImage` overloads + `DrawRouteBriefop`, `DrawRoute`, `DrawMarker` helpers

**Approach:**
```csharp
static SKBitmap GenerateMapImage(GeoPoint center, int iZoom, ITileSource tileSource,
    IEnumerable<ILayer> overlayLayers, Size outputSize)
{
    // 1. BruTile: ITileSchema.GetTileInfos(extent, levelId) → List<TileInfo>
    // 2. For each TileInfo: tileSource.GetTile(tileInfo) → byte[] → SKBitmap.Decode
    // 3. For each overlayLayer: construct Mapsui.Viewport matching image extent,
    //       call each feature's ISkiaSharpStyleRenderer.Draw(canvas, viewport, ...)
}
```

BruTile key types: `ITileSchema.GetTileInfos(Extent, string levelId)`, `ITileSource.GetTile(TileInfo) -> byte[]` (use `GetTileAsync` + `Task.WhenAll` to parallelise).

Callers of `GenerateMapImage` in the briefing generation pipeline need updating to pass `ITileSource` instead of `GMapProvider`.

---

## Phase 5 — Cleanup ⬜

**Goal:** Remove all remaining GMap.NET references, then remove the packages.

**Stub cleanup in ToolsMap.cs (marked `TODO Phase 5`):**
- Remove `InitializeGMaps()` + call sites in `Program.cs`, `FrmMain.cs`
- Remove `InitializeMapControl(GMapControl,…)` + update `UcGroup.cs`, `UcAirbase.cs` (swap their `GMapControl` for Mapsui `MapControl`)
- Remove `ForceRefresh(GMapControl)` + update `UcAirbase.cs`, `UcGroupUnits.cs`, `UcGroupRoutePoints.cs`, `UcGroupInformation.cs`

**Data model type substitutions** (`PointLatLng` → `GeoPoint`, `GMapOverlay` → `MemoryLayer`, remove `GMap.NET.*` usings):
- [DataBopMission/BopGroup.cs](DataBopMission/BopGroup.cs)
- [DataBopMission/BopUnit.cs](DataBopMission/BopUnit.cs)
- [DataBopMission/BopRoutePoint.cs](DataBopMission/BopRoutePoint.cs)
- [DataBopMission/BopCoalition.cs](DataBopMission/BopCoalition.cs)
- [DataBopMission/BopAirbase.cs](DataBopMission/BopAirbase.cs)
- [DataBopMission/BopGroupOrUnit.cs](DataBopMission/BopGroupOrUnit.cs)
- [DataBopBriefing/BopBriefingPage.cs](DataBopBriefing/BopBriefingPage.cs)
- [DataBopBriefing/BaseBopBriefingPart.cs](DataBopBriefing/BaseBopBriefingPart.cs)
- [DataBopBriefing/BopBriefingPartWaypoints.cs](DataBopBriefing/BopBriefingPartWaypoints.cs)
- [DataBopBriefing/BopBriefingPartGroups.cs](DataBopBriefing/BopBriefingPartGroups.cs)
- [DataBopBriefing/BopBriefingPartAirbases.cs](DataBopBriefing/BopBriefingPartAirbases.cs)

After all substitutions: remove `GMap.NET.Core` and `GMap.NET.WinForms` from `DcsBriefop.csproj`.

---

## Phase Sequencing

```
Shared infra ──► Phase 1 (tiles) ✅
                     │
                     ▼
                 Phase 2 (markers) ⬜  ← next
                     │
                     ▼
                 Phase 3 (lines)  ⬜      Phase 4 (offline gen) ⬜
                     │                           │
                     └──────────┬────────────────┘
                                ▼
                            Phase 5 (cleanup + remove packages) ⬜
```

Phases 3 and 4 are independent and can be worked in parallel.

---

## Risks and Mitigations

| Risk | Mitigation |
|---|---|
| SkiaSharp rendering differs visually from GDI+ | Side-by-side screenshot comparison after Phase 2 and Phase 3 |
| BruTile tile fetch synchronous per call | Use `GetTileAsync` + `Task.WhenAll` in Phase 4 |
| `MapControl.Info` hit-testing differs from GMap per-marker events | Test click, hover, drag in UcMap immediately after Phase 2 |
| `SkiaSharp.Views.WindowsForms 3.119.1` NU1701 warning (not targeting net10.0-windows) | Known Mapsui 5 packaging issue; no action needed unless runtime errors appear |
