# Map Overlay Plan — Status & Remaining Work

## Architecture (as built)

### Provider hierarchy
```
Map/
  WMSProviderBase.cs            — abstract; nested WmsUrlBuilder gets Url+WmsLayer via ctor
  WMSProviderFlappie.cs         — Name="WMS DCS", Flappie DCS server
  XYZProviderBase.cs            — abstract; nested XyzUrlBuilder with {z}/{x}/{y} template
  XYZProviderOpenTopoMap.cs     — Name="OpenTopoMap"
  XYZProviderFAA.cs             — 4 classes: FAA VFR Sectional/Terminal + IFR Low/High
  OverlayProviderBase.cs        — abstract; Name + CreateTileSource()
  OverlayProviderOpenAIP.cs     — static ApiKey; zoom 7-18; uses "openaip" combined layer
  MapProviders.cs               — static registry: filtered KnownTileSource + Flappie + OpenTopoMap + 4 FAA
  MapOverlays.cs                — static registry: OverlayRecord(Name, Func<ITileSource>)
```

### Registry design
**Base maps** (`MapProviders`) use `MapProviderRecord(string Name, Func<ITileSource> Factory)`.

**Overlays** (`MapOverlays`) use `OverlayRecord(string Name, Func<ITileSource> Factory)`.
Both use the same factory pattern. `OverlayProviderBase` is used as a concrete type to call
`CreateTileSource`, then its method reference is stored as the factory:
```csharp
OverlayProviderOpenAIP openAip = new();
s_overlays.Add(new(openAip.Name, openAip.CreateTileSource));
```
The three Esri KnownTileSource overlays are registered directly with a lambda.

### OpenAIP tile API (confirmed from official OpenAPI spec)
The PNG TMS endpoint only accepts two `layer` values — no per-type filtering is possible:
- `openaip` — combined layer (airports, airspaces, navaids, reporting points, hand-glidings)
- `hotspots` — hotspots only

Per-layer endpoints (`airspaces`, `reporting-points`, `obstacles`, etc.) were discontinued in 2023.

### KnownTileSource exclusions
Providers requiring API keys or with broken URLs are excluded from `MapProviders`:
Bing (6 entries), Here (4), Stamen (4), OpenCycleMap (2), and the three Esri overlays below.

### Overlay toggles (currently registered in MapOverlays)
| Name | Source |
|------|--------|
| OpenAIP | `OverlayProviderOpenAIP` — `openaip` layer, zoom 7-18 |
| EsriWorldReferenceOverlay | `KnownTileSources.Create(...)` |
| EsriWorldTransportation | `KnownTileSources.Create(...)` |
| EsriWorldBoundariesAndPlaces | `KnownTileSources.Create(...)` |

### Live map overlay flow
`ToolsMap.RefreshOverlayLayers(MapControl, IEnumerable<string> enabledNames)`:
- Removes all `TileLayer`s whose name starts with `"overlay:"`
- Re-adds a `TileLayer` per enabled overlay, named `"overlay:{name}"`

### UI — FrmTheatre (test bed, fully wired)
- Provider `ComboBox` — changes base map, calls `InitializeMapControl` + `RefreshOverlayLayers`
- Overlays `UcCheckedDropDown` — toggles overlays, calls `RefreshOverlayLayers`
- OpenAIP API key hardcoded in `FrmTheatre` constructor (temporary)

### Mapsui logging
`ToolsMap.ConfigureMapsui()` called from `Program.cs` after `ParseCommandLine`:
- Routes all Mapsui log events to log4net via `Mapsui.Logging.Logger.LogDelegate`
- `LoggingWidget.ShowLoggingInMap = ActiveMode.Yes` only when `--debug` flag is passed

---

## Done ✅

| Item | Notes |
|------|-------|
| WMS provider base + Flappie | `WMSProviderBase` / `WMSProviderFlappie` |
| XYZ provider base + OpenTopoMap | `XYZProviderBase` / `XYZProviderOpenTopoMap` |
| FAA base map providers (×4) | ArcGIS `{z}/{y}/{x}` order handled by template, no base class change needed |
| KnownTileSource filtering | 16 broken providers excluded from `MapProviders` |
| Esri overlay reclassification | 3 Esri sources moved from base maps to overlays |
| `OverlayProviderBase` + `OverlayProviderOpenAIP` | Zoom guard 7-18; `openaip` layer |
| `MapOverlays` registry | `OverlayRecord(Name, Func<ITileSource>)` |
| `RefreshOverlayLayers` | Extension on `MapControl` in `ToolsMap` |
| `UcCheckedDropDown` | Generic reusable dropdown-checklist control with count + tooltip feedback |
| `FrmTheatre` provider + overlay UI | Fully wired for testing |
| Mapsui log routing + debug widget | `ConfigureMapsui()` in `ToolsMap`, called from `Program.cs` |

---

## Still to do ❌

### 1. Preferences — overlay persistence (`Data/Preferences.cs`)
`PreferencesMap` currently has only `ProviderName` and `Zoom`. Add:
```csharp
public List<string> EnabledOverlayNames { get; set; } = [];
public string OpenAipApiKey { get; set; } = string.Empty;
```

### 2. Move OpenAIP API key out of FrmTheatre
`OverlayProviderOpenAIP.ApiKey` is set to a hardcoded string in `FrmTheatre`'s constructor.
Move it to `PreferencesMap.OpenAipApiKey` and wire `OverlayProviderOpenAIP.ApiKey` from preferences
at startup and whenever preferences are saved.

### 3. FrmPreferences — overlay settings UI
Add a section to `FrmPreferences` (and its designer):
- `UcCheckedDropDown` (or `CheckedListBox`) of available overlays — bound to `EnabledOverlayNames`
- `TextBox` for OpenAIP API key — enabled when OpenAIP overlay is checked
- Save/load via `ScreenToData` / `DataToScreen`

### 4. FrmMissionMaps — overlay UI
`FrmMissionMaps` has a map provider selector. Add the same `UcCheckedDropDown` overlay control
that `FrmTheatre` uses. The enabled overlays come from `BopMission.PreferencesMap.EnabledOverlayNames`
(per-mission override) rather than global preferences.

### 5. Image generation — tile overlay composite (`Tools/ToolsMap.cs`)
`GenerateMapImage` currently only composites `MemoryLayer` (vector features). Overlay tile layers
are not rendered in generated briefing images.

Approach: add an `IEnumerable<ITileSource> overlaySources = null` parameter. After the base tile
pass, loop through each overlay source and do the same fetch-and-draw loop, compositing on the
same `SKCanvas`. The caller obtains sources from `MapOverlays` filtered by enabled names.

---

## Open questions

- Should `FrmMissionMaps` overlay selection override or merge with global preferences?
- For image generation, should overlay tile fetches run in parallel with the base tile fetch,
  or sequentially after? (Parallel is faster but adds complexity to the Task.WhenAll grouping.)
