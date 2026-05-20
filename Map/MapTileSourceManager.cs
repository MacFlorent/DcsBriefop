using BruTile.Predefined;
using DcsBriefop.Forms;
using DcsBriefop.Tools;

namespace DcsBriefop.Map
{
	internal static class MapTileSourceManager
	{
		#region Fields
		private static readonly List<MapTileSource> s_basemaps = [];
		private static readonly List<MapTileSource> s_overlays = [];

		// Providers that require API keys or have broken URLs or are overlays are excluded from the registry
		private static readonly KnownTileSource[] s_knownSourcesBasemaps =
		[
			KnownTileSource.OpenStreetMap,
			//KnownTileSource.OpenCycleMap,
			//KnownTileSource.OpenCycleMapTransport,
			KnownTileSource.BingAerial,
			KnownTileSource.BingHybrid,
			KnownTileSource.BingRoads,
			//KnownTileSource.BingAerialStaging,
			//KnownTileSource.BingHybridStaging,
			//KnownTileSource.BingRoadsStaging,
			//KnownTileSource.StamenToner,
			//KnownTileSource.StamenTonerLite,
			//KnownTileSource.StamenWatercolor,
			//KnownTileSource.StamenTerrain,
			KnownTileSource.EsriWorldTopo,
			KnownTileSource.EsriWorldPhysical,
			KnownTileSource.EsriWorldShadedRelief,
			//KnownTileSource.EsriWorldReferenceOverlay,
			//KnownTileSource.EsriWorldTransportation,
			//KnownTileSource.EsriWorldBoundariesAndPlaces,
			KnownTileSource.EsriWorldDarkGrayBase,
			KnownTileSource.BKGTopPlusColor,
			KnownTileSource.BKGTopPlusGrey,
			//KnownTileSource.HereNormal,
			//KnownTileSource.HereSatellite,
			//KnownTileSource.HereHybrid,
			//KnownTileSource.HereTerrain,
		];
		private static readonly KnownTileSource[] s_knownSourceOverlays =
		[
			KnownTileSource.EsriWorldReferenceOverlay,
			KnownTileSource.EsriWorldTransportation,
			KnownTileSource.EsriWorldBoundariesAndPlaces,
		];
		#endregion
		public static IReadOnlyList<MapTileSource> Basemaps
		{
			get
			{
				return [.. s_basemaps.Where(_o => _o.Active)];
			}
		}

		public static IReadOnlyList<MapTileSource> Overlays
		{
			get
			{
				return [.. s_overlays.Where(_o => _o.Active)];
			}
		}
		#region Properties

		#endregion

		#region CTOR
		static MapTileSourceManager()
		{
			try
			{
				InitializeBasemaps();
				InitializeOverlays();
			}
			catch (Exception e)
			{
				ToolsControls.ShowMessageBoxAndLogException("Failed to initialize map tiles sources. Map display will not be available", e);
				Log.Exception(e);
			}
		}

		private static void InitializeBasemaps()
		{
			s_basemaps.Clear();

			foreach (KnownTileSource s in s_knownSourcesBasemaps)
				s_basemaps.Add(new(s.ToString(), () => KnownTileSources.Create(s)));

			s_basemaps.Add(new MapTileSourceXyz("Flappie", "http://dcsmaps.com/caucasus/{z}/{x}/{y}.png", BruTile.YAxis.TMS, 8, 12));
			s_basemaps.Add(new MapTileSourceXyz("OpenTopoMap", "https://tile.opentopomap.org/{z}/{x}/{y}.png"));
			s_basemaps.Add(new MapTileSourceXyz("FAA VFR Sectional", "https://tiles.arcgis.com/tiles/ssFJjBXIUyZDrSYZ/arcgis/rest/services/VFR_Sectional/MapServer/tile/{z}/{y}/{x}"));
			s_basemaps.Add(new MapTileSourceXyz("FAA VFR Terminal", "https://tiles.arcgis.com/tiles/ssFJjBXIUyZDrSYZ/arcgis/rest/services/VFR_Terminal/MapServer/tile/{z}/{y}/{x}"));
			s_basemaps.Add(new MapTileSourceXyz("FAA IFR Low", "https://tiles.arcgis.com/tiles/ssFJjBXIUyZDrSYZ/arcgis/rest/services/IFR_AreaLow/MapServer/tile/{z}/{y}/{x}"));
			s_basemaps.Add(new MapTileSourceXyz("FAA IFR High", "https://tiles.arcgis.com/tiles/ssFJjBXIUyZDrSYZ/arcgis/rest/services/IFR_High/MapServer/tile/{z}/{y}/{x}"));
		}

		private static void InitializeOverlays()
		{
			s_overlays.Clear();

			foreach (KnownTileSource s in s_knownSourceOverlays)
				s_overlays.Add(new(s.ToString(), () => KnownTileSources.Create(s)));

			s_overlays.Add(new MapTileSourceOpenAip());
			s_overlays.Add(new MapTileSourceWms("Flappie Overlays", "http://dcsmaps.com/cgi-bin/mapserv?map=CAUCASUS_MAPFILE", "LAYERS=Isolines,Rivers,Water,Railroad,Powerlines,Roads,LBridges,Tunnels,Bridges,Borders,Landmarks,Derricks,Obstacle,MGRS-grid,MGRS-37T,MGRS-38T,Cities,Towns,Airbases,DB,DME,NDB,TACAN,VOR"));
		}
		#endregion

		#region Methods
		public static void FillComboBasemaps(ComboBox cb, EventHandler selectedValueChanged)
		{
			ToolsControls.FillCombo(cb, Basemaps, "Name", null, selectedValueChanged);
		}

		public static void FillCheckDropDownOverlays(UcCheckedDropDown cdd)
		{
			cdd.SetDataSource(Overlays, "Name");
		}

		public static MapTileSource TryGetBasemap(string sName)
		{
			return s_basemaps.Find(_bm => _bm.Name == sName);
		}

		public static MapTileSource TryGetBasemapOrDefault(string sName)
		{
			return TryGetBasemap(sName) ?? TryGetBasemap(PreferencesManager.Preferences.Map.ProviderName) ?? s_basemaps[0];
		}
		#endregion
	}

}

