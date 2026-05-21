using BruTile;
using BruTile.Predefined;
using BruTile.Web;

namespace DcsBriefop.Map
{
	internal class MapTileSourceXyz : MapTileSource
	{
		// ArcGIS tile URL order is {z}/{y}/{x} (level/row/col).
		// XyzUrlBuilder maps {y}→Row and {x}→Col, so the template below produces the correct path without any axis swap.
		// Use YAxis.TMS for servers where y=0 is at the south pole (TMS convention), e.g. Flappie DCS maps.

		#region Fields
		private readonly string m_sUrlTemplate;
		private readonly YAxis m_yAxis;
		private readonly int m_iMinZoomLevel;
		private readonly int m_iMaxZoomLevel;
		#endregion

		#region CTOR
		public MapTileSourceXyz(string sName, string sUrlTemplate) : this(sName, sUrlTemplate, YAxis.OSM, 0, 18) { }

		public MapTileSourceXyz(string sName, string sUrlTemplate, YAxis yAxis, int iMinZoomLevel, int iMaxZoomLevel) : base(sName, null)
		{
			m_sUrlTemplate = sUrlTemplate;
			m_yAxis = yAxis;
			m_iMinZoomLevel = iMinZoomLevel;
			m_iMaxZoomLevel = iMaxZoomLevel;
			TileFactory = CreateTileSource;
		}
		#endregion

		#region Methods
		private HttpTileSource CreateTileSource()
		{
			return new HttpTileSource(new GlobalSphericalMercator(m_yAxis, m_iMinZoomLevel, m_iMaxZoomLevel), new XyzUrlBuilder(m_sUrlTemplate), Name, null, null, null);
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
