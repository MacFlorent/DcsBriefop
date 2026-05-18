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
		private static readonly List<MapProviderRecord> s_providers = [];

		// Providers that require API keys or have broken URLs or are overlays are excluded from the registry
		private static readonly HashSet<KnownTileSource> s_excludedKnownSources =
		[
			KnownTileSource.BingAerial,
			KnownTileSource.BingHybrid,
			KnownTileSource.BingRoads,
			KnownTileSource.BingAerialStaging,
			KnownTileSource.BingHybridStaging,
			KnownTileSource.BingRoadsStaging,
			KnownTileSource.HereNormal,
			KnownTileSource.HereSatellite,
			KnownTileSource.HereHybrid,
			KnownTileSource.HereTerrain,
			KnownTileSource.StamenToner,
			KnownTileSource.StamenTonerLite,
			KnownTileSource.StamenWatercolor,
			KnownTileSource.StamenTerrain,
			KnownTileSource.OpenCycleMap,
			KnownTileSource.OpenCycleMapTransport,
			KnownTileSource.EsriWorldReferenceOverlay,
			KnownTileSource.EsriWorldTransportation,
			KnownTileSource.EsriWorldBoundariesAndPlaces,
		];
		#endregion

		#region CTOR
		static MapProviders()
		{
			foreach (KnownTileSource s in Enum.GetValues<KnownTileSource>().Where(_s => !s_excludedKnownSources.Contains(_s)))
				s_providers.Add(new(s.ToString(), () => KnownTileSources.Create(s)));

			WMSProviderFlappie flappie = new();
			s_providers.Add(new(flappie.Name, flappie.CreateTileSource));

			XYZProviderOpenTopoMap openTopoMap = new();
			s_providers.Add(new(openTopoMap.Name, openTopoMap.CreateTileSource));

			XYZProviderFaaVfrSectional faaVfrSectional = new();
			s_providers.Add(new(faaVfrSectional.Name, faaVfrSectional.CreateTileSource));

			XYZProviderFaaVfrTerminal faaVfrTerminal = new();
			s_providers.Add(new(faaVfrTerminal.Name, faaVfrTerminal.CreateTileSource));

			XYZProviderFaaIfrLow faaIfrLow = new();
			s_providers.Add(new(faaIfrLow.Name, faaIfrLow.CreateTileSource));

			XYZProviderFaaIfrHigh faaIfrHigh = new();
			s_providers.Add(new(faaIfrHigh.Name, faaIfrHigh.CreateTileSource));
		}
		#endregion

		#region Methods
		public static void FillCombo(ComboBox cb, EventHandler selectedValueChanged)
		{
			ToolsControls.FillCombo(cb, s_providers, "Name", null, selectedValueChanged);
		}

		public static MapProviderRecord TryGetProvider(string sName)
		{
			return s_providers.Find(_p => _p.Name == sName);
		}

		public static MapProviderRecord TryGetProviderOrDefault(string sName)
		{
			return TryGetProvider(sName) ?? TryGetProvider(PreferencesManager.Preferences.Map.ProviderName) ?? s_providers[0];
		}

		public static TileLayer CreateTileLayer(string sName)
		{
			MapProviderRecord entry = TryGetProviderOrDefault(sName);
			return new TileLayer(entry.Factory()) { Name = entry.Name };
		}
		#endregion
	}
}
