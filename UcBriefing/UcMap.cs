using CoordinateSharp;
using GMap.NET.WindowsForms;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcMap : UserControl
	{
		public GMapOverlay OverlayMain { get; set; }
		public List<GMapOverlay> OverlayOthers { get; private set; } = new List<GMapOverlay>();

		public UcMap()
		{
			InitializeComponent();

			Map.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
			GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
			//Map.ShowTileGridLines = true;
			Map.Position = new GMap.NET.PointLatLng(26.1702778, 56.24);

		}

		public void AddOverlay(GMapOverlay gmo)
		{
			if (gmo is null || gmo == OverlayMain)
				return;

			if (OverlayOthers.Contains(gmo))
				return;

			OverlayOthers.Add(gmo);
		}

		public void RemoveOverlay(GMapOverlay gmo)
		{
			if (gmo == OverlayMain)
				OverlayMain = null;

			if (OverlayOthers.Contains(gmo))
				OverlayOthers.Remove(gmo);
		}

		public void ClearOverlays()
		{
			OverlayMain = null;
			OverlayOthers.Clear();
		}

		public void RefreshMap()
		{
			Map.Overlays.Clear();
			
			if (OverlayMain is object)
				Map.Overlays.Add(OverlayMain);
						
			foreach(GMapOverlay gmo in OverlayOthers)
				Map.Overlays.Add(gmo);

			Map.ZoomAndCenterMarkers(null);
		}
	}
}
