using BruTile;
using BruTile.Predefined;
using BruTile.Web;
using System.Text;

namespace DcsBriefop.Map
{
	internal class MapTileSourceWms : MapTileSource
	{
		#region Fields
		private readonly string m_sUrl;
		private readonly string m_sWmsLayer;
		#endregion

		#region CTOR
		public MapTileSourceWms(string sName, string sUrl, string sWmsLayer) : base(sName, null)
		{
			m_sUrl = sUrl;
			m_sWmsLayer = sWmsLayer;
			TileFactory = CreateTileSource;
		}
		#endregion

		#region Methods
		private HttpTileSource CreateTileSource()
		{
			return new HttpTileSource(new GlobalSphericalMercator(), new WmsUrlBuilder(m_sUrl, m_sWmsLayer), Name, null, null, null);
		}
		#endregion

		#region WmsUrlBuilder
		private class WmsUrlBuilder(string sUrl, string sWmsLayer) : IUrlBuilder
		{
			private readonly string m_sUrl = sUrl;
			private readonly string m_sWmsLayer = sWmsLayer;

			public Uri GetUrl(TileInfo tileInfo)
			{
				string sBbox = FormattableString.Invariant($"{tileInfo.Extent.MinX},{tileInfo.Extent.MinY},{tileInfo.Extent.MaxX},{tileInfo.Extent.MaxY}");
				StringBuilder sb = new(m_sUrl);
				sb.Append('&').Append(m_sWmsLayer);
				sb.Append("&FORMAT=image/png&TRANSPARENT=TRUE");
				sb.Append("&SERVICE=WMS&VERSION=1.1.1&REQUEST=GetMap&STYLES=");
				sb.Append("&SRS=EPSG:3857");
				sb.Append("&BBOX=").Append(sBbox);
				sb.Append("&WIDTH=256&HEIGHT=256");
				return new Uri(sb.ToString());
			}
		}
		#endregion
	}
}
