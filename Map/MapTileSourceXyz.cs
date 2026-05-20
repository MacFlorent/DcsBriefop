using BruTile;
using BruTile.Predefined;
using BruTile.Web;

namespace DcsBriefop.Map
{
	internal class MapTileSourceXyz : MapTileSource
	{
		// ArcGIS tile URL order is {z}/{y}/{x} (level/row/col).
		// XyzUrlBuilder maps {y}→Row and {x}→Col, so the template below produces the correct path without any axis swap.

		#region Fields
		private readonly string m_sUrlTemplate;
		private readonly int m_iMinZoomLevel = 0;
		private readonly int m_iMaxZoomLevel = 18;
		#endregion

		#region CTOR
		public MapTileSourceXyz(string sName, string sUrlTemplate) : base(sName, null)
		{
			m_sUrlTemplate = sUrlTemplate;
			TileFactory = CreateTileSource;
		}
		#endregion

		#region Methods
		private HttpTileSource CreateTileSource()
		{
			return new HttpTileSource(new GlobalSphericalMercator(YAxis.OSM, m_iMinZoomLevel, m_iMaxZoomLevel), new XyzUrlBuilder(m_sUrlTemplate), Name, null, null, null);
		}
		#endregion

		#region XyzUrlBuilder
		private class XyzUrlBuilder(string sUrlTemplate) : IUrlBuilder
		{
			private readonly string _sUrlTemplate = sUrlTemplate;

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
