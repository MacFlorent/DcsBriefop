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
		#endregion

		#region CTOR
		static MapProviders()
		{
			foreach (KnownTileSource s in Enum.GetValues<KnownTileSource>())
				s_providers.Add(new(s.ToString(), () => KnownTileSources.Create(s)));

			WMSProviderFlappie flappie = new();
			s_providers.Add(new(flappie.Name, flappie.CreateTileSource));

			XYZProviderOpenTopoMap openTopoMap = new();
			s_providers.Add(new(openTopoMap.Name, openTopoMap.CreateTileSource));
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
