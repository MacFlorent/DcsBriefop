using BruTile;
using BruTile.Predefined;
using BruTile.Web;

namespace DcsBriefop.Map
{
	internal class MapTileSourceOpenAip : MapTileSource
	{
		#region Properties
		private string ApiKey => PreferencesManager.Preferences.Map.OpenAipApiKey;
		public override bool Active => !string.IsNullOrEmpty(ApiKey);
		#endregion

		#region CTOR
		public MapTileSourceOpenAip() : base("OpenAIP", null)
		{
			TileFactory = CreateTileSource;
		}
		#endregion

		#region Methods
		private HttpTileSource CreateTileSource()
		{
			return new HttpTileSource(new GlobalSphericalMercator(YAxis.OSM, 7, 18), new OpenAIPUrlBuilder(ApiKey), Name, null, null, null);
		}
		#endregion

		#region OpenAIPUrlBuilder
		private class OpenAIPUrlBuilder(string sApiKey) : IUrlBuilder
		{
			private readonly string m_sApiKey = sApiKey;

			public Uri GetUrl(TileInfo tileInfo)
			{
				return new Uri($"https://a.api.tiles.openaip.net/api/data/openaip/{tileInfo.Index.Level}/{tileInfo.Index.Col}/{tileInfo.Index.Row}.png?apiKey={m_sApiKey}");
			}
		}
		#endregion
	}
}
