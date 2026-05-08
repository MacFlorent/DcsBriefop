# Grid Migration: ADGV → FastObjectListView

Tracking document for replacing `DG.AdvancedDataGridView` with `ObjectListView.Repack.NET6Plus` (`FastObjectListView`).

---

## Decisions

| Topic | Decision |
|---|---|
| Filter UX | **Option B** — `FilterMenuBuilder` on column headers (per-column, value-based) |
| Free-text search | **Ctrl+F** search bar, injected programmatically by the base class — shows a `ToolStrip` above the grid, drives `TextMatchFilter` across all columns, dismissed with ESC. Zero designer or subclass changes. |

---

## Phase 1 — NuGet swap

- [x] Remove `DG.AdvancedDataGridView` from the project
- [x] Add `ObjectListView.Repack.NET6Plus` 2.9.5 (targets net6.0-windows, computed compatible with net10.0-windows)
- [ ] Build to establish the full compiler error baseline

---

## Phase 2 — Rewrite `GridManagerBase<T>`

The only medium-complexity change. Public API surface is unchanged; all internals change.

### Fields / constructor

- [x] Replace `AdvancedDataGridView m_dgv` with `FastObjectListView m_dgv`
- [x] Remove `DataTable m_dtSource`
- [x] Remove `BindingSource m_bindingSource`
- [x] Update constructor signature

### Abstract / virtual methods

- [x] Replace `abstract InitializeDataSourceColumns()` + `abstract RefreshDataSourceRowContent(DataRow, T)` with `abstract InitializeColumns()` — columns defined with `OLVColumn` + `AspectGetter` lambdas
- [x] Remove `PostInitializeColumns()` — fold into `InitializeColumns()`
- [x] Rename `CellFormattingInternal()` override point → `FormatRowInternal(FormatRowEventArgs)` + `FormatCellInternal(FormatCellEventArgs)`
- [x] Rename `CellEndEditInternal()` override point → `CellEditFinishedInternal(CellEditEventArgs)`

### Public API (keep identical)

- [x] `Refresh()` → `m_dgv.SetObjects(Elements)`
- [x] `RefreshDataSourceRows()` → `m_dgv.RefreshObjects(Elements)`
- [x] `GetSelectedElements()` → `m_dgv.SelectedObjects.Cast<T>()`
- [x] `SelectRow(T)` → `m_dgv.SelectObject(element)`
- [x] `CheckedElements` → backed by `m_dgv.CheckedObjects`
- [x] `ColumnsDisplayed` → `ApplyColumnsDisplayed()` sets `col.IsVisible` then calls `RebuildColumns()`

### Filtering

- [x] Remove `CleanFilterAndSort()` call from `Refresh()`; reset `m_dgv.ModelFilter = null` instead
- [x] Enable `m_dgv.UseFiltering = true` and assign `FilterMenuBuildStrategy = new FilterMenuBuilder()` for per-column header dropdowns
- [x] Inject hidden `ToolStrip` with `TextBox` above the grid programmatically (no designer changes); show/hide on Ctrl+F / ESC; wire to `TextMatchFilter`

### Events

- [x] Map `CellFormatting` → `FormatRow` / `FormatCell`
- [x] Map `CellEndEdit` → `CellEditFinishing` / `CellEditFinished`
- [x] Keep public `SelectionChanged` and `CellEndEdit` events (same signature)

---

## Phase 3 — Update concrete GridManager subclasses

Each subclass collapses `InitializeDataSourceColumns` + `RefreshDataSourceRowContent` into a single `InitializeColumns()` override with `AspectGetter` lambdas.

- [x] `GridManagerAirbases`
- [x] `GridManagerAirbaseRadios` _(has editable cells + `CellEditFinishedInternal`; `CellEditStarting` cancels edit of read-only Radio rows)_
- [x] `GridManagerBriefingPages`
- [x] `GridManagerBriefingFolders` _(row coloring for inactive folders)_
- [x] `GridManagerBriefingParts`
- [x] `GridManagerDcsObjects`
- [x] `GridManagerGroups` _(row coloring by coalition)_
- [x] `GridManagerGroupOrUnits` _(row coloring by coalition + group background)_
- [x] `GridManagerRoutePoints` _(measurement unit in column headers; altitude/distance/speed conversions inline)_
- [x] `GridManagerUnits` _(flight-only columns use `obj is BopUnitFlight f` pattern)_

---

## Phase 4 — Update designer files

Mechanical swap in each `.Designer.cs`: replace `Zuby.ADGV.AdvancedDataGridView` instantiation and ADGV-specific properties with `FastObjectListView` boilerplate. `ISupportInitialize` removed for the grid control (kept for SplitContainer, NumericUpDown, PictureBox). `CellDoubleClick` changed to `DoubleClick` where present.

- [x] `FrmDcsObjects.Designer.cs`
- [x] `FrmMissionGroups.Designer.cs`
- [x] `FrmMissionAirbases.Designer.cs`
- [x] `FrmBriefingFolder.Designer.cs`
- [x] `FrmNormalize.Designer.cs`
- [x] `UcAirbase.Designer.cs`
- [x] `UcBriefingPage.Designer.cs`
- [x] `UcBriefingPartGroups.Designer.cs` _(two grids; `CellDoubleClick` → `DoubleClick`)_
- [x] `UcBriefingPartAirbases.Designer.cs` _(two grids; `CellDoubleClick` → `DoubleClick`)_
- [x] `UcBriefop.Designer.cs` _(`CellDoubleClick` → `DoubleClick`)_
- [x] `UcGroupUnits.Designer.cs`
- [x] `UcGroupRoutePoints.Designer.cs`

Handler signatures updated in code files:

- [x] `UcBriefingPartGroups.cs` — `DgvMultiAvailable_CellDoubleClick` / `DgvMultiSelected_CellDoubleClick`: `DataGridViewCellEventArgs` → `EventArgs`
- [x] `UcBriefingPartAirbases.cs` — same
- [x] `UcBriefop.cs` — `DgvBriefingFolders_CellDoubleClick`: `DataGridViewCellEventArgs` → `EventArgs`

---

## Phase 5 — Cleanup

- [x] Remove all `using Zuby.ADGV;` references
- [x] Verify `DG.AdvancedDataGridView` is no longer referenced anywhere in `.cs` files
- [ ] Build clean (zero errors, zero warnings from migration)
- [ ] Smoke-test each grid view: load data, sort, filter, select, checkbox (where applicable), edit (where applicable)

---

## Effort estimate

| Phase | Effort |
|---|---|
| 1 — NuGet | ~5 min |
| 2 — Base class rewrite | ~2–3 h |
| 3 — 10 subclasses | ~1 h |
| 4 — Designer files | ~30 min |
| 5 — Cleanup + test | ~1 h |
