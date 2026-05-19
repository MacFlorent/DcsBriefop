using BruTile;
using BruTile.Predefined;
using DcsBriefop.Tools;
using Mapsui.Tiling.Layers;

namespace DcsBriefop.Map
{
	internal static class MapProviders
	{
		#region Types
		internal record MapProviderRecord(string Name, Func<ITileSource> Factory);
		#endregion

		#region Fields
		private static readonly List<MapProviderRecord> s_mapProviders = [];
		// Providers that require API keys or have broken URLs or are overlays are excluded from the registry
		private static readonly KnownTileSource[] s_knownSources =
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
		#endregion

		#region Properties
		public static IReadOnlyList<MapProviderRecord> All => s_mapProviders;
		#endregion

		#region CTOR
		static MapProviders()
		{
			foreach (KnownTileSource s in s_knownSources)
				s_mapProviders.Add(new(s.ToString(), () => KnownTileSources.Create(s)));

			WMSProviderFlappie flappie = new();
			s_mapProviders.Add(new(flappie.Name, flappie.CreateTileSource));

			XYZProviderOpenTopoMap openTopoMap = new();
			s_mapProviders.Add(new(openTopoMap.Name, openTopoMap.CreateTileSource));

			XYZProviderFaaVfrSectional faaVfrSectional = new();
			s_mapProviders.Add(new(faaVfrSectional.Name, faaVfrSectional.CreateTileSource));

			XYZProviderFaaVfrTerminal faaVfrTerminal = new();
			s_mapProviders.Add(new(faaVfrTerminal.Name, faaVfrTerminal.CreateTileSource));

			XYZProviderFaaIfrLow faaIfrLow = new();
			s_mapProviders.Add(new(faaIfrLow.Name, faaIfrLow.CreateTileSource));

			XYZProviderFaaIfrHigh faaIfrHigh = new();
			s_mapProviders.Add(new(faaIfrHigh.Name, faaIfrHigh.CreateTileSource));
		}
		#endregion

		#region Methods
		public static void FillCombo(ComboBox cb, EventHandler selectedValueChanged)
		{
			ToolsControls.FillCombo(cb, s_mapProviders, "Name", null, selectedValueChanged);
		}

		public static MapProviderRecord TryGetProvider(string sName)
		{
			return s_mapProviders.Find(_p => _p.Name == sName);
		}

		public static MapProviderRecord TryGetProviderOrDefault(string sName)
		{
			return TryGetProvider(sName) ?? TryGetProvider(PreferencesManager.Preferences.Map.ProviderName) ?? s_mapProviders[0];
		}

		public static TileLayer CreateTileLayer(string sName)
		{
			MapProviderRecord entry = TryGetProviderOrDefault(sName);
			return new TileLayer(entry.Factory()) { Name = entry.Name };
		}
		#endregion
	}
}
