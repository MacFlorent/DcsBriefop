using BruTile;
using BruTile.Predefined;
using DcsBriefop.Tools;
using Mapsui.Tiling.Layers;

namespace DcsBriefop.Map
{
	internal static class MapProviders
	{
		#region Types
		internal record MapProviderEntry(string Name, Func<ITileSource> Factory);
		#endregion

		#region Fields
		private static readonly List<MapProviderEntry> s_providers;
		#endregion

		#region CTOR
		static MapProviders()
		{
			s_providers =
			[
				new("OpenStreetMap", () => KnownTileSources.Create(KnownTileSource.OpenStreetMap)),
				new("ArcGIS Topo", () => KnownTileSources.Create(KnownTileSource.EsriWorldTopo)),
				new("ArcGIS Physical", () => KnownTileSources.Create(KnownTileSource.EsriWorldPhysical)),
				new("ArcGIS Shaded Relief", () => KnownTileSources.Create(KnownTileSource.EsriWorldShadedRelief)),
				new("WMS DCS", WMSProvider.CreateTileSource),
			];
		}
		#endregion

		#region Methods
		public static void FillCombo(ComboBox cb, EventHandler selectedValueChanged)
		{
			ToolsControls.FillCombo(cb, s_providers, "Name", null, selectedValueChanged);
		}

		public static MapProviderEntry TryGetEntry(string sName)
		{
			return s_providers.Find(p => p.Name == sName);
		}

		public static TileLayer CreateTileLayer(string sName)
		{
			MapProviderEntry entry = TryGetEntry(sName) ?? s_providers[0];
			return new TileLayer(entry.Factory()) { Name = entry.Name };
		}
		#endregion
	}
}
