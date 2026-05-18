using BruTile;
using BruTile.Predefined;
using BruTile.Web;

namespace DcsBriefop.Map
{
	internal class OverlayProviderOpenAIP : OverlayProviderBase
	{
		// TODO: move to PreferencesMap.OpenAipApiKey
		public static string ApiKey { get; set; } = string.Empty;

		public OverlayProviderOpenAIP()
		{
			Name = "OpenAIP";
		}

		public override ITileSource CreateTileSource()
		{
			return new HttpTileSource(new GlobalSphericalMercator(YAxis.OSM, 7, 18), new OpenAIPUrlBuilder(ApiKey), Name, null, null, null);
		}

		private class OpenAIPUrlBuilder : IUrlBuilder
		{
			private readonly string _sApiKey;

			public OpenAIPUrlBuilder(string sApiKey) => _sApiKey = sApiKey;

			public Uri GetUrl(TileInfo tileInfo)
			{
				return new Uri($"https://a.api.tiles.openaip.net/api/data/openaip/{tileInfo.Index.Level}/{tileInfo.Index.Col}/{tileInfo.Index.Row}.png?apiKey={_sApiKey}");
			}
		}
	}
}
