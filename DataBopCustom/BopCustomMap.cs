using DcsBriefop.Data;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.DataBopCustom
{
	internal class BopCustomMap
	{
		public string Provider { get; set; }
		public double CenterLatitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Zoom { get; set; }

		public GMapOverlay MapOverlayCustom { get; set; }
		[JsonIgnore]
		public List<GMapOverlay> AdditionalMapOverlays { get; private set; } = new List<GMapOverlay>();

		public GMapProvider GetMapProvider()
		{
			string sMapProvider = Provider;
			if (string.IsNullOrEmpty(sMapProvider))
				sMapProvider = Preferences.PreferencesManager.Preferences.Map.DefaultProvider;

			return GMapProviders.TryGetProvider(sMapProvider);
		}

		public GMapOverlay GetStaticOverlay()
		{
			return AdditionalMapOverlays?.Where(_o => _o.Id == ElementMapValue.OverlayStatic).FirstOrDefault();
		}
	}
}
