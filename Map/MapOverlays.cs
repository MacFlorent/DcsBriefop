using BruTile;
using BruTile.Predefined;
using DcsBriefop.Forms;
using DcsBriefop.Tools;

namespace DcsBriefop.Map
{
	internal static class MapOverlays
	{
		#region Types
		internal record MapOverlayRecord(string Name, Func<ITileSource> Factory);
		#endregion

		#region Fields
		private static readonly List<MapOverlayRecord> s_mapOverlays = [];
		private static readonly KnownTileSource[] s_knownSources =
		[
			KnownTileSource.EsriWorldReferenceOverlay,
			KnownTileSource.EsriWorldTransportation,
			KnownTileSource.EsriWorldBoundariesAndPlaces,
		];
		#endregion

		#region Properties
		public static IReadOnlyList<MapOverlayRecord> All => s_mapOverlays;
		#endregion

		#region CTOR
		static MapOverlays()
		{
			foreach (KnownTileSource s in s_knownSources)
				s_mapOverlays.Add(new(s.ToString(), () => KnownTileSources.Create(s)));

			OverlayProviderOpenAIP openAip = new();
			s_mapOverlays.Add(new(openAip.Name, openAip.CreateTileSource));
		}
		#endregion

		#region Methods
		public static void FillCheckDropDown(UcCheckedDropDown cdd)
		{
			cdd.SetDataSource(s_mapOverlays, "Name");
		}
		#endregion
	}
}
