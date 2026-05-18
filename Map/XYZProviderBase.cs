using BruTile;
using BruTile.Predefined;
using BruTile.Web;

namespace DcsBriefop.Map
{
	internal abstract class XYZProviderBase
	{
		#region Fields
		public string Name { get; set; }
		public string UrlTemplate { get; set; }
		public int MinZoomLevel { get; set; } = 0;
		public int MaxZoomLevel { get; set; } = 18;
		#endregion

		#region Methods
		public ITileSource CreateTileSource()
		{
			return new HttpTileSource(new GlobalSphericalMercator(YAxis.OSM, MinZoomLevel, MaxZoomLevel), new XyzUrlBuilder(UrlTemplate), Name, null, null, null);
		}
		#endregion

		#region XyzUrlBuilder
		private class XyzUrlBuilder : IUrlBuilder
		{
			private readonly string _sUrlTemplate;

			public XyzUrlBuilder(string sUrlTemplate) => _sUrlTemplate = sUrlTemplate;

			public Uri GetUrl(TileInfo tileInfo)
			{
				string sUrl = _sUrlTemplate
					.Replace("{z}", tileInfo.Index.Level.ToString())
					.Replace("{x}", tileInfo.Index.Col.ToString())
					.Replace("{y}", tileInfo.Index.Row.ToString());
				return new Uri(sUrl);
			}
		}
		#endregion
	}
}
