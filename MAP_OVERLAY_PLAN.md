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
**Important**: `MapTileSourceManager` is a static class initialized once at startup. Adding a key in
preferences does not re-add the OpenAIP entry to `s_overlays`. The registry must be re-initialized
(call `InitializeOverlays()` — currently private) after preferences are saved with a new key.

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

### UI — FrmTheatre (test bed, fully wired)
- Provider `ComboBox` — calls `ChangeBasemapLayer`
- Overlays `UcCheckedDropDown` — calls `ChangeOverlayLayers`
- Reads provider + overlay selection from `BopMission.PreferencesMap` (or global prefs if no mission)

### Preferences — `PreferencesMap` (fully implemented)
```csharp
public string ProviderName { get; set; }
public double Zoom { get; set; }
public List<string> OverlayNames { get; set; } = [];
public string OpenAipApiKey { get; set; } = string.Empty;
```

### FrmPreferences — overlay settings UI (fully implemented)
- `CbMapProvider` — basemap selector
- `NudMapZoom` — zoom level
- `CddMapOverlays` (`UcCheckedDropDown`) — overlay multi-select
- `TbMapOpenAipKey` — OpenAIP API key text box
- `ScreenToData` / `DataToScreen` persist all four fields

### UcMap — embedded map user control (used in FrmMissionMaps)
`UcMap.DataToScreen()` calls `MapControl.InitializeMapControl(m_sMapProviderName, null)` — overlay
names are hardcoded `null`; no overlay support wired yet.

### Mapsui logging
`ToolsMap.ConfigureMapsui()` called from `Program.cs` after `ParseCommandLine`:
- Routes all Mapsui log events to log4net via `Mapsui.Logging.Logger.LogDelegate`
- `LoggingWidget.ShowLoggingInMap = ActiveMode.Yes` only when `--debug` flag is passed

---

## Done ✅

| Item | Notes |
|------|-------|
| `MapTileSource` base + `MapTileSourceWms` + `MapTileSourceXyz` | Concrete factory-based tile sources |
| FAA base map providers (×4) | ArcGIS `{z}/{y}/{x}` order handled by URL template |
| `KnownTileSource` filtering | Broken/key-required providers excluded |
| Esri overlay reclassification | 3 Esri sources in `s_knownSourceOverlays` |
| `MapTileSourceOpenAip` | Zoom guard 7-18; `openaip` layer; `Active` reads preferences key at access time |
| OpenAIP activation gate | Works at startup and after preferences save; `UcCheckedDropDown`s already open when key changes are not refreshed (acceptable) |
| `MapTileSourceManager` registry | `Basemaps` + `Overlays` filtered by `Active`; fill helpers |
| `ChangeOverlayLayers` | Extension on `MapControl` in `ToolsMap` |
| `OrderLayers` | basemaps → overlays → memory layers |
| `UcCheckedDropDown` | Generic reusable dropdown-checklist control |
| `FrmTheatre` provider + overlay UI | Fully wired for testing |
| `PreferencesMap` — overlay fields | `OverlayNames` + `OpenAipApiKey` |
| `FrmPreferences` — overlay settings UI | `CddMapOverlays` + `TbMapOpenAipKey` + `LnkMapOpenAipAttribution` fully wired |
| Mapsui log routing + debug widget | `ConfigureMapsui()` in `ToolsMap`, called from `Program.cs` |

---

## Still to do ❌

### 2. FrmMissionMaps — overlay UI
`FrmMissionMaps` uses `UcMap` which calls `InitializeMapControl(providerName, null)`.
- Add a `UcCheckedDropDown` overlay control (like `FrmTheatre`)
- Read/write enabled overlays from `BopMission.PreferencesMap.OverlayNames`
- Pass checked names to `UcMap` (add an `OverlayNames` property or pass via `DataToScreen` overload)
- `UcMap.DataToScreen()` must forward overlay names to `InitializeMapControl`

### 3. OpenAIP attribution — live map forms
`FrmPreferences` has a clickable `LnkMapOpenAipAttribution` (`LinkLabel`) that opens
`https://www.openaip.net` in the default browser. ✅

The OpenAIP license also requires a visible attribution whenever the overlay is **active on a live map**:
> *Map data © [OpenAIP](https://www.openaip.net)*

Add the same `LinkLabel` (or equivalent) to `FrmTheatre` and `FrmMissionMaps`, visible only while
the OpenAIP overlay is checked. Wire `LinkClicked` to open the URL the same way as in `FrmPreferences`.

### 4. Image generation — tile overlay composite (`Tools/ToolsMap.cs`)
`GenerateMapImage` currently accepts a single `ITileSource` (base map only). Overlay tiles are
not composited in generated briefing images.

Approach: add an `IEnumerable<ITileSource> overlaySources = null` parameter. After the base tile
pass, loop through each overlay source and run the same fetch-and-draw block on the same `SKCanvas`.
The caller obtains sources from `MapTileSourceManager.Overlays` filtered by enabled names.

---

## Open questions

- For `FrmMissionMaps`, should overlay selection come from `BopMission.PreferencesMap.OverlayNames`
  (per-mission override) or always from global preferences?
- For image generation, should overlay tile fetches run in parallel with the base tile fetch
  (faster, more complex `Task.WhenAll` grouping) or sequentially after?
