# Map Overlay Plan — Status & Remaining Work

## Architecture (as built)

### Tile source hierarchy
```
Map/
  MapTileSource.cs              — base: Name, Active (virtual bool), TileFactory (Func<ITileSource>)
  MapTileSourceWms.cs           — concrete WMS; nested WmsUrlBuilder builds GetMap requests
  MapTileSourceXyz.cs           — concrete XYZ; nested XyzUrlBuilder with {z}/{x}/{y} template
  MapTileSourceOpenAip.cs       — concrete overlay; Active = key non-empty; zoom 7-18; openaip layer
  MapTileSourceManager.cs       — static registry: s_basemaps (16) + s_overlays (4)
```

### Registry design
`MapTileSourceManager` holds two static lists filtered by `Active`:
- **`Basemaps`** — 11 `KnownTileSource` entries + Flappie WMS + OpenTopoMap + 4 FAA XYZ
- **`Overlays`** — 3 Esri `KnownTileSource` + `MapTileSourceOpenAip`

`MapTileSource` uses `Func<ITileSource> TileFactory` — factory is invoked each time a layer is added.

`MapTileSourceOpenAip.Active` reads `PreferencesManager.Preferences.Map.OpenAipApiKey` at access time,
so the overlay is silently absent from `Overlays` when no key is set.
**Note**: `MapTileSourceManager` is a static class initialized once at startup. The `Active`-based
filter on `Overlays` is evaluated at access time, so adding a key in preferences works without
re-initialization. `UcCheckedDropDown`s already open at that moment won't reflect the new entry
until they are next populated — this is acceptable.

### OpenAIP tile API
PNG TMS endpoint accepts two `layer` values only (per-type filtering discontinued 2023):
- `openaip` — combined (airports, airspaces, navaids, reporting points, hand-glidings)
- `hotspots` — hotspots only

### KnownTileSource exclusions
Providers requiring API keys or with broken URLs are excluded from `s_knownSourcesBasemaps`:
OpenCycleMap (2), Bing Staging (3), Stamen (4), Here (4). The three Esri overlay entries are
in `s_knownSourceOverlays` instead.

### Overlay layer management — `ToolsMap` extensions
`ChangeOverlayLayers(MapControl, IEnumerable<string> overlayNames)`:
- Resolves names to `MapTileSource` objects via `MapTileSourceManager.Overlays`
- Removes all `TileLayer`s whose name starts with `"overlay:"`
- Re-adds a `TileLayer` per resolved overlay, named `"overlay:{name}"`
- Calls `OrderLayers()` (basemaps → overlays → memory layers)

### Image generation — `ToolsMap.GenerateMapImage`
Accepts `MapTileSource basemapSource` + `IEnumerable<MapTileSource> overlaySources`.
Draws basemap tiles first, then each overlay in order, then vector `MemoryLayer`s on top.
The tile fetch-and-draw logic is extracted into `DrawTileLayer(SKCanvas, ITileSource, ...)`.

### Preferences — `PreferencesMap` (fully implemented)
```csharp
public string ProviderName { get; set; }
public double Zoom { get; set; }
public List<string> OverlayNames { get; set; } = [];
public string OpenAipApiKey { get; set; } = string.Empty;
```

### UcMap — embedded map user control (used in FrmMissionMaps)
`MapOverlayNames` property forwarded to `MapControl.ChangeOverlayLayers` in `DataToScreen`.
`ChangeOverlayLayers(IEnumerable<string>)` public method exposed for live toggle.

---

## Done ✅

| Item | Notes |
|------|-------|
| `MapTileSource` base + `MapTileSourceWms` + `MapTileSourceXyz` | Concrete factory-based tile sources |
| FAA base map providers (×4) | ArcGIS `{z}/{y}/{x}` order handled by URL template |
| `KnownTileSource` filtering | Broken/key-required providers excluded |
| Esri overlay reclassification | 3 Esri sources in `s_knownSourceOverlays` |
| `MapTileSourceOpenAip` | Zoom guard 7-18; `openaip` layer; `Active` reads preferences key at access time |
| OpenAIP activation gate | Works at startup and after preferences save; already-open dropdowns not refreshed (acceptable) |
| `MapTileSourceManager` registry | `Basemaps` + `Overlays` filtered by `Active`; fill helpers |
| `ChangeOverlayLayers` | Extension on `MapControl` in `ToolsMap` |
| `OrderLayers` | basemaps → overlays → memory layers |
| `UcCheckedDropDown` | Generic reusable dropdown-checklist control |
| `FrmTheatre` provider + overlay UI | Fully wired for testing |
| `PreferencesMap` — overlay fields | `OverlayNames` + `OpenAipApiKey` |
| `FrmPreferences` — overlay settings UI | `CddMapOverlays` + `TbMapOpenAipKey` + `LnkMapOpenAipAttribution` fully wired |
| `FrmMissionMaps` — overlay UI | `CddMapOverlays` + `LnkMapOpenAipAttribution`; reads/writes `BopMission.PreferencesMap.OverlayNames` |
| `UcMap` overlay support | `MapOverlayNames` property + `ChangeOverlayLayers` method |
| Image generation — overlay compositing | `DrawTileLayer` helper; `GenerateMapImage` draws basemap → overlays → vectors |
| `BopBriefingPage.BuildMapImage` | Passes enabled `MapTileSource` overlays filtered by `OverlayNames` |
| Mapsui log routing + debug widget | `ConfigureMapsui()` in `ToolsMap`, called from `Program.cs` |

---

## All items complete ✅

OpenAIP attribution `LnkMapOpenAipAttribution` is present and wired in all three map forms:
`FrmPreferences` (static), `FrmMissionMaps` (toggled by overlay state), `FrmTheatre` (toggled by overlay state).
