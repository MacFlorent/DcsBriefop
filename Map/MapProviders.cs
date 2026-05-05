using BruTile;
using BruTile.Predefined;
using DcsBriefop.Tools;
using Mapsui.Tiling.Layers;

namespace DcsBriefop.Map
{
	internal static class ElementMapProviderName
	{
		public static readonly string OpenStreetMap = "OpenStreetMap";
		public static readonly string ArcGISTopo = "ArcGIS Topo";
		public static readonly string ArcGISPhysical = "ArcGIS Physical";
		public static readonly string ArcGISShadedRelief = "ArcGIS Shaded Relief";
		public static readonly string WmsDcs = "WMS DCS";
	}

	internal static class MapProviders
	{
		#region Types
		internal record MapProviderRecord(string Name, Func<ITileSource> Factory);
		#endregion

		#region Fields
		private static readonly List<MapProviderRecord> s_providers;
		#endregion

		#region CTOR
		static MapProviders()
		{
			s_providers =
			[
				new(ElementMapProviderName.OpenStreetMap, () => KnownTileSources.Create(KnownTileSource.OpenStreetMap)),
				new(ElementMapProviderName.ArcGISTopo, () => KnownTileSources.Create(KnownTileSource.EsriWorldTopo)),
				new(ElementMapProviderName.ArcGISPhysical, () => KnownTileSources.Create(KnownTileSource.EsriWorldPhysical)),
				new(ElementMapProviderName.ArcGISShadedRelief, () => KnownTileSources.Create(KnownTileSource.EsriWorldShadedRelief)),
				new(ElementMapProviderName.WmsDcs, WMSProvider.CreateTileSource),
			];
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
