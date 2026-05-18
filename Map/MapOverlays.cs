using BruTile;
using BruTile.Predefined;

namespace DcsBriefop.Map
{
	internal static class MapOverlays
	{
		#region Types
		internal record OverlayRecord(string Name, Func<ITileSource> Factory);
		#endregion

		#region Fields
		private static readonly List<OverlayRecord> s_overlays = [];

		private static readonly KnownTileSource[] s_knownOverlaySources =
		[
			KnownTileSource.EsriWorldReferenceOverlay,
			KnownTileSource.EsriWorldTransportation,
			KnownTileSource.EsriWorldBoundariesAndPlaces,
		];
		#endregion

		#region Properties
		public static IReadOnlyList<OverlayRecord> All => s_overlays;
		#endregion

		#region CTOR
		static MapOverlays()
		{
			foreach (KnownTileSource s in s_knownOverlaySources)
				s_overlays.Add(new(s.ToString(), () => KnownTileSources.Create(s)));

			OverlayProviderOpenAIP openAip = new();
			s_overlays.Add(new(openAip.Name, openAip.CreateTileSource));
		}
		#endregion
	}
}
