using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

//https://stackoverflow.com/questions/9308673/how-to-draw-circle-on-the-map-using-gmap-net-in-c-sharp
//http://www.independent-software.com/gmap-net-beginners-tutorial-maps-markers-polygons-routes-updated-for-vs2015-and-gmap1-7.html
//https://icon-icons.com/fr/icone/bullseye/73647https://icon-icons.com/fr/icone/bullseye/73647

namespace DcsBriefop.Forms
{
	internal partial class UcMap : UserControl, ICustomStylable
	{
		#region Fields
		private GMapProvider m_mapProvider;
		#endregion

		#region Properties
		public MizBopMap MapData { get; set; }
		public IEnumerable<GMapOverlay> StaticOverlays { get; set; }
		public string MapProviderName
		{
			get { return m_mapProvider.Name; }
			set { m_mapProvider = GMapProviders.TryGetProvider(value); }
		}
		#endregion

		#region CTOR
		public UcMap()
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			PnSelectionDetail.Visible = false;
			MapControl.InitializeMapControl(MapControl.MapProvider);
		}
		#endregion

		#region ICustomStylable
		public void ApplyCustomStyle()
		{
		}
		#endregion

		#region Methods
		public void RefreshMapData()
		{
			RefreshOverlays();
			MapControl.MapProvider = m_mapProvider;
			MapControl.Position = new PointLatLng(MapData.CenterLatitude, MapData.CenterLongitude);
			MapControl.Zoom = MapData.Zoom;
		}

		public void RefreshOverlays()
		{
			ClearOverlays();
			UnselectAll();
			if (StaticOverlays is object)
			{
				foreach (GMapOverlay staticOverlay in StaticOverlays)
					MapControl.Overlays.Add(staticOverlay);
			}
			if (MapData is object)
				MapControl.Overlays.Add(MapData.MapOverlay);
		}

		public void ClearOverlays()
		{
			MapControl.Overlays.Clear();
		}

		private void AddMarker(double dLat, double dLng)
		{
			PointLatLng p = new PointLatLng(dLat, dLng);
			MapData.MapOverlay.Markers.Add(GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.DefaultMark, Color.Orange, null, 1, 0));
			CkAddMarker.Checked = false;
		}

		private void DeleteMarker(GMarkerBriefop gmb)
		{
			if (gmb.Overlay == MapData.MapOverlay)
				MapData.MapOverlay.Markers.Remove(gmb);
		}

		private void SelectMarker(GMarkerBriefop gmb)
		{
			gmb.IsSelected = true;

			// display detail
			PnSelectionDetail.Controls.Clear();
			UcMarkerDetail ucDetail = new UcMarkerDetail(gmb, MapControl);
			ucDetail.Dock = DockStyle.Fill;
			PnSelectionDetail.Controls.Add(ucDetail);
			PnSelectionDetail.Visible = true;
		}

		private void UnselectAll()
		{
			foreach (var marker in MapData.MapOverlay.Markers)
			{
				if (marker is GMarkerBriefop markerBriefop)
					markerBriefop.IsSelected = false;
			}

			// hide detail
			PnSelectionDetail.Controls.Clear();
			PnSelectionDetail.Visible = false;
		}

		private GMarkerBriefop GetMarkerHovered()
		{
			foreach (var marker in MapData.MapOverlay.Markers)
			{
				if (marker is GMarkerBriefop markerBriefop && markerBriefop.IsHovered)
					return markerBriefop;
			}

			return null;
		}

		private GMarkerBriefop GetMarkerPressed()
		{
			foreach (var marker in MapData.MapOverlay.Markers)
			{
				if (marker is GMarkerBriefop markerBriefop && markerBriefop.IsPressed)
					return markerBriefop;
			}

			return null;
		}
		#endregion

		#region Events
		private void BtAreaSet_Click(object sender, System.EventArgs e)
		{
			MapData.CenterLatitude = MapControl.Position.Lat;
			MapData.CenterLongitude = MapControl.Position.Lng;
			MapData.Zoom = (int)MapControl.Zoom;
		}

		private void BtAreaRecall_Click(object sender, System.EventArgs e)
		{
			MapControl.Position = new PointLatLng(MapData.CenterLatitude, MapData.CenterLongitude);
			MapControl.Zoom = MapData.Zoom;
		}

		private void Map_MouseDown(object sender, MouseEventArgs e)
		{
			if (GetMarkerHovered() is GMarkerBriefop gmbHovered)
			{
				gmbHovered.IsPressed = true;
			}
		}

		private void Map_MouseUp(object sender, MouseEventArgs e)
		{
			foreach (var marker in MapData.MapOverlay.Markers)
			{
				if (marker is GMarkerBriefop markerBriefop)
					markerBriefop.IsPressed = false;

			}
		}

		private void Map_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (CkAddMarker.Checked)
					AddMarker(MapControl.FromLocalToLatLng(e.X, e.Y).Lat, MapControl.FromLocalToLatLng(e.X, e.Y).Lng);
				else if (GetMarkerHovered() is null)
					UnselectAll();
			}
		}

		private void Map_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				GMarkerBriefop gmb = GetMarkerHovered();

				if (gmb is object)
				{
					DeleteMarker(gmb);
				}
			}
		}

		private void Map_OnMarkerClick(GMapMarker item, MouseEventArgs e)
		{
			CkAddMarker.Checked = false;

			if (item.Overlay != MapData.MapOverlay)
				return;

			UnselectAll();

			if (item is GMarkerBriefop selectedGmb)
				SelectMarker(selectedGmb);
		}

		private void Map_OnMarkerEnter(GMapMarker item)
		{
			if (item.Overlay == MapData.MapOverlay && item is GMarkerBriefop gmb)
				gmb.IsHovered = true;

		}

		private void Map_OnMarkerLeave(GMapMarker item)
		{
			if (item is GMarkerBriefop gmb)
				gmb.IsHovered = false;
		}

		private void Map_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (GetMarkerPressed() is GMarkerBriefop gmb)
				{
					var pnew = MapControl.FromLocalToLatLng(e.X, e.Y);
					gmb.Position = pnew;
				}

				MapControl.Refresh();
			}
		}

		private void BtRefresh_Click(object sender, System.EventArgs e)
		{
			MapControl.ForceRefresh();
		}
		#endregion
	}
}
