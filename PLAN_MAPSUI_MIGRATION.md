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
| Register custom renderer | `MapRenderer.RegisterStyleRenderer(Type, IStyleRenderer)` — **static**, call once in `InitializeMapControl` |
| `viewport.WorldToScreen(MPoint)` | Returns `Mapsui.Manipulations.ScreenPosition` (not `MPoint`) — access `.X` / `.Y` |
| `IFeature` namespace | `Mapsui` (not `Mapsui.Layers`) |
| `PointFeature`, `MemoryLayer` | `Mapsui.Layers` |
| Set map renderer | `mapControl.SetMapRenderer(IMapRenderer)` — not needed when using static `RegisterStyleRenderer` |
| `MemoryLayer` redraw | `layer.DataHasChanged()` — fires `DataChanged` event, triggers repaint |
| Drag a feature | Mutate `feature.Point.X`/`.Y` then call `feature.Modified()` + `layer.DataHasChanged()` |

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
public readonly struct GeoPoint(double dLat, double dLng)  // public: required by BriefopMarker public API
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

## Phase 2 — Markers ✅ Done

**Goal:** Port `GMarkerBriefop` to Mapsui point features with SkiaSharp rendering. Restore marker display in `UcMap`. Wire marker click/hover/drag.

**Files created:**
- [Map/BriefopMarker.cs](Map/BriefopMarker.cs) ✅ — `public class BriefopMarker`; `GetSkBitmap()` converts GDI+ → SkiaSharp lazily; `GetSizeWidth/Height/OffsetX/OffsetY()` expose template geometry
- [Map/BriefopMarkerStyle.cs](Map/BriefopMarkerStyle.cs) ✅ — `internal class BriefopMarkerStyle : BaseStyle`; holds ref to `BriefopMarker`
- [Map/BriefopMarkerStyleRenderer.cs](Map/BriefopMarkerStyleRenderer.cs) ✅ — `ISkiaStyleRenderer`; ports GDI+ `OnRender` to SkiaSharp; draws rotated bitmap, label, selection/hover rect

**Files updated:**
- [DataMiz/MizBopMap.cs](DataMiz/MizBopMap.cs) ✅ — `CustomMarkers: List<BriefopMarker>`; `BuildCustomLayer() -> MemoryLayer` (new, Mapsui); `BuildCustomMapOverlay()` kept as TODO Phase 4 stub for `GenerateMapImage`
- [JsonSerializers.cs](JsonSerializers.cs) ✅ — added `BriefopMarkerJsonConverter`; removed `GMarkerBriefopJsonConverter`; `GMapOverlayJsonConverter` kept with TODO Phase 3 comment (no longer serializes markers)
- [DataMiz/BaseMizBopSerializable.cs](DataMiz/BaseMizBopSerializable.cs) ✅ — `m_converterBriefopMarker` replaces `m_converterGMarkerBriefop`
- [Tools/ToolsMap.cs](Tools/ToolsMap.cs) ✅ — added `MapRenderer.RegisterStyleRenderer(typeof(BriefopMarkerStyle), new BriefopMarkerStyleRenderer())` call in `InitializeMapControl`
- [Forms/UcMap.cs](Forms/UcMap.cs) ✅ — full marker interaction: `DataToOverlay`, `HitTestMarker`, `SelectMarker`, mouse down/up/click/move/keyup; `m_markerFeatures` dict for fast feature lookup during drag
- [Forms/UcMarkerDetail.cs](Forms/UcMarkerDetail.cs) ✅ — constructor changed to `(BriefopMarker, Action refreshMap)`

**Verified API facts (Mapsui 5.0.2):**
- `MapRenderer.RegisterStyleRenderer(Type, IStyleRenderer)` is **static** — no `mapControl.Renderer` needed
- `viewport.WorldToScreen(MPoint)` returns `Mapsui.Manipulations.ScreenPosition` (not `MPoint`) — use `.X` and `.Y` properties
- `IFeature` is in `Mapsui` namespace; `PointFeature`, `MemoryLayer` are in `Mapsui.Layers`
- `mapControl.SetMapRenderer(IMapRenderer)` exists but not needed since `RegisterStyleRenderer` is static

**Deferred to later phases:**
- `GTextBriefop` → `BriefopTextMarker` (Phase 3, static overlays)
- Theatre centre + airdrome markers in `FrmTheatre.cs` (still TODO Phase 2 comment in code)
- Drag in Mapsui: `MPoint.X`/`.Y` are mutable, `feature.Point.X/Y = newVal` + `feature.Modified()` triggers redraw

---

## Phase 3 — Lines ✅ Done

**Goal:** Port `GLineBriefop` to `GeometryFeature` (NTS LineString) with custom SkiaSharp renderer. Port `GTextBriefop` to `BriefopLabel`. Update `ToolsMap.BuildMizDrawingLayer`. Change `UcMap.StaticOverlays` type.

**Files created:**
- [Map/BriefopLine.cs](Map/BriefopLine.cs) ✅ — data class; `ToGeometryFeature() -> GeometryFeature`; lazy SKBitmap from template; factory methods matching `GLineBriefop`
- [Map/BriefopLineStyle.cs](Map/BriefopLineStyle.cs) ✅ — `class BriefopLineStyle : BaseStyle`; holds `BriefopLine`
- [Map/BriefopLineStyleRenderer.cs](Map/BriefopLineStyleRenderer.cs) ✅ — `ISkiaStyleRenderer`; ports all 3 render paths: dash/solid stroke, bitmap texture tiling, text panel with optional arrow; fill drawn before outline
- [Map/BriefopLabel.cs](Map/BriefopLabel.cs) ✅ — replaces `GTextBriefop`; anchored bottom-left; `ToPointFeature()` → `PointFeature` with `BriefopLabelStyle`
- [Map/BriefopLabelStyle.cs](Map/BriefopLabelStyle.cs) ✅
- [Map/BriefopLabelStyleRenderer.cs](Map/BriefopLabelStyleRenderer.cs) ✅ — `ISkiaStyleRenderer`; bottom-left anchor; background fill + border

**Files updated:**
- [Tools/ToolsMap.cs](Tools/ToolsMap.cs) ✅ — `BuildMizDrawingLayer(Theatre, List<MizDrawingLayer>) -> MemoryLayer`; all `AddMizDrawingObject*` fill `List<IFeature>`; icons → `BriefopMarker`+`PointFeature`; text → `BriefopLabel.ToPointFeature()`; lines/polys → `BriefopLine.ToGeometryFeature()`; registered `BriefopLineStyle` and `BriefopLabelStyle` renderers in `InitializeMapControl`
- [DataBopMission/BopCoalition.cs](DataBopMission/BopCoalition.cs) ✅ — `BuildStaticMapOverlay` → `BuildStaticLayer() -> MemoryLayer`; bullseye uses `BriefopMarker`+`PointFeature`+`BriefopMarkerStyle`
- [DataBopMission/BopMission.cs](DataBopMission/BopMission.cs) ✅ — `BuildStaticMapOverlay` → `BuildStaticLayer() -> MemoryLayer`
- [DataBopBriefing/BopBriefingPage.cs](DataBopBriefing/BopBriefingPage.cs) ✅ — added `GetMapAdditionalLayers() -> IEnumerable<ILayer>`; `GetMapAdditionalOverlays` kept (stripped of base layers) for `BuildMapImage` (Phase 4); `BuildMapImage` marked TODO Phase 4
- [Forms/UcMap.cs](Forms/UcMap.cs) ✅ — `StaticOverlays: IEnumerable<ILayer>`; `m_staticLayers` field tracks layers for cleanup; `DataToOverlay` removes+re-adds static layers under custom marker layer for correct z-order
- [Forms/FrmMissionMaps.cs](Forms/FrmMissionMaps.cs) ✅ — calls `BuildStaticLayer()`
- [Forms/UcBriefingPage.cs](Forms/UcBriefingPage.cs) ✅ — calls `GetMapAdditionalLayers()`

**Verified API facts (Mapsui 5.0.2):**
- `WorldToScreen` is an extension method in `Mapsui.Extensions` — requires `using Mapsui.Extensions`
- `Mapsui.Styles.Color` conflicts with `System.Drawing.Color` — resolve with `using Color = System.Drawing.Color` alias in renderer files
- `GeometryFeature(Geometry)` ctor + `Geometry.Coordinates` returns `Coordinate[]` (NTS)
- NTS `GeometryFactory.CreateLineString(Coordinate[])` requires ≥ 2 coordinates

**Deferred to Phase 5:**
- `GLineBriefop.cs`, `GTextBriefop.cs`, `GMarkerBriefop.cs` — still compiled for Phase 4 image generation stubs
- `BaseBopBriefingPart.BuildMapOverlays` (and subclass impls) — still returns `GMapOverlay`; not yet added to `GetMapAdditionalLayers` (TODO Phase 5 comment in `BopBriefingPage`)
- `JsonSerializers.GMapOverlayJsonConverter` — kept for backward-compat read of old JSON

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
                 Phase 2 (markers) ✅
                     │
                     ▼
                 Phase 3 (lines)  ✅              Phase 4 (offline gen) ⬜  ← next
                     │                           │
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
| `SkiaSharp.Views.WindowsForms` restored from net462 instead of net8.0-windows | **Fixed** — target changed to `net10.0-windows10.0.19041`; NuGet now picks `net8.0-windows10.0.19041` build |
