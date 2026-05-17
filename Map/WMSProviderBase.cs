using BruTile;
using BruTile.Predefined;
using BruTile.Web;
using System.Text;

namespace DcsBriefop.Map
{
	internal abstract class WMSProviderBase
	{
		#region Fields
		public string Name { get; set; }
		public string Url { get; set; }
		public string WmsLayer { get; set; }
		#endregion

		#region Methods
		public ITileSource CreateTileSource()
		{
			return new HttpTileSource(new GlobalSphericalMercator(), new WmsUrlBuilder(Url, WmsLayer), Name, null, null, null);
		}
		#endregion

		#region WmsUrlBuilder
		private class WmsUrlBuilder : IUrlBuilder
		{
			private readonly string _sUrl;
			private readonly string _sWmsLayer;

			public WmsUrlBuilder(string sUrl, string sWmsLayer)
			{
				_sUrl = sUrl;
				_sWmsLayer = sWmsLayer;
			}

			public Uri GetUrl(TileInfo tileInfo)
			{
				string sBbox = FormattableString.Invariant($"{tileInfo.Extent.MinX},{tileInfo.Extent.MinY},{tileInfo.Extent.MaxX},{tileInfo.Extent.MaxY}");
				StringBuilder sb = new(_sUrl);
				sb.Append('&').Append(_sWmsLayer);
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
