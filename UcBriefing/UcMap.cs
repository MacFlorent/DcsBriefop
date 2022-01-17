using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

//https://stackoverflow.com/questions/9308673/how-to-draw-circle-on-the-map-using-gmap-net-in-c-sharp
//http://www.independent-software.com/gmap-net-beginners-tutorial-maps-markers-polygons-routes-updated-for-vs2015-and-gmap1-7.html
//https://icon-icons.com/fr/icone/bullseye/73647https://icon-icons.com/fr/icone/bullseye/73647

namespace DcsBriefop.UcBriefing
{
	internal partial class UcMap : UserControl, ICustomStylable
	{
		#region Fields
		private bool m_bViewOnly;
		#endregion

		#region Properties
		public BriefopCustomMap MapData { get; private set; }
		#endregion

		#region CTOR
		public UcMap()
		{
			InitializeComponent();

			Map.MapProvider = ElementMapValue.MapProvider;
			GMaps.Instance.Mode = AccessMode.ServerOnly;
			//Map.ShowTileGridLines = true;
			Map.Position = new PointLatLng(26.1702778, 56.24);
			Map.MinZoom = ElementMapValue.MinZoom;
			Map.MaxZoom = ElementMapValue.MaxZoom;
		}
		#endregion

		#region ICustomStylable
		public void ApplyCustomStyle()
		{
			ToolsStyle.LabelHeader(LbTheater);
			ToolsStyle.LabelTitle(LbTitle);
		}
		#endregion

		#region Methods
		public void SetMapData(BriefopCustomMap mapData, string sTheater, string sTitle, bool bViewOnly)
		{
			m_bViewOnly = bViewOnly;
			LbTheater.Text = sTheater;
			LbTitle.Text = sTitle;

			CkAddMarker.Enabled = BtAreaSet.Enabled = BtAreaRecall.Enabled = !m_bViewOnly;

			MapData = mapData;
			RefreshOverlays();
			Map.Position = new PointLatLng(MapData.CenterLatitude, MapData.CenterLongitude);
			Map.Zoom = MapData.Zoom;
		}

		public void RefreshOverlays()
		{
			Map.Overlays.Clear();
			if (MapData is object)
			{
				UnselectAll();

				Map.Overlays.Add(MapData.MapOverlayCustom);

				foreach (GMapOverlay gmo in MapData.AdditionalMapOverlays)
					Map.Overlays.Add(gmo);
			}
		}

		public void ClearOverlays()
		{
			Map.Overlays.Clear();
		}

		private void AddMarker(double dLat, double dLng)
		{
			if (m_bViewOnly)
				return;

			PointLatLng p = new PointLatLng(dLat, dLng);
			MapData.MapOverlayCustom.Markers.Add(new GMarkerBriefop(p, MarkerBriefopType.circle.ToString(), Color.Orange, null));
			CkAddMarker.Checked = false;
		}

		private void DeleteMarker(GMarkerBriefop gmb)
		{
			if (m_bViewOnly)
				return;

			if (gmb.Overlay == MapData.MapOverlayCustom)
				MapData.MapOverlayCustom.Markers.Remove(gmb);
		}

		private void SelectMarker(GMarkerBriefop gmb)
		{
			if (m_bViewOnly)
				return;

			gmb.IsSelected = true;

			// display detail
			PnSelectionDetail.Controls.Clear();
			UcMarkerDetail ucDetail = new UcMarkerDetail(gmb, Map);
			ucDetail.Dock = DockStyle.Fill;
			PnSelectionDetail.Controls.Add(ucDetail);
		}

		private void UnselectAll()
		{
			foreach (GMarkerBriefop gmb in MapData.MapOverlayCustom.Markers.OfType<GMarkerBriefop>())
				gmb.IsSelected = false;

			// hide detail
			PnSelectionDetail.Controls.Clear();
		}

		private GMarkerBriefop GetMarkerHovered()
		{
			foreach (GMarkerBriefop gmb in MapData.MapOverlayCustom.Markers.OfType<GMarkerBriefop>())
				if (gmb.IsHovered)
					return gmb;

			return null;
		}

		private GMarkerBriefop GetMarkerPressed()
		{
			foreach (GMarkerBriefop gmb in MapData.MapOverlayCustom.Markers.OfType<GMarkerBriefop>())
				if (gmb.IsPressed)
					return gmb;

			return null;
		}
		#endregion

		#region Events
		private void BtAreaSet_Click(object sender, System.EventArgs e)
		{
			MapData.CenterLatitude = Map.Position.Lat;
			MapData.CenterLongitude = Map.Position.Lng;
			MapData.Zoom = (int)Map.Zoom;
		}

		private void BtAreaRecall_Click(object sender, System.EventArgs e)
		{
			Map.Position = new PointLatLng(MapData.CenterLatitude, MapData.CenterLongitude);
			Map.Zoom = MapData.Zoom;
		}

		private void Map_MouseDown(object sender, MouseEventArgs e)
		{
			if (m_bViewOnly)
				return;

			if (GetMarkerHovered() is GMarkerBriefop gmbHovered)
			{
				gmbHovered.IsPressed = true;
			}
		}

		private void Map_MouseUp(object sender, MouseEventArgs e)
		{
			if (m_bViewOnly)
				return;

			foreach (GMarkerBriefop gmb in MapData.MapOverlayCustom.Markers.OfType<GMarkerBriefop>())
				gmb.IsPressed = false;
		}

		private void Map_MouseClick(object sender, MouseEventArgs e)
		{
			if (m_bViewOnly)
				return;

			if (e.Button == MouseButtons.Left)
			{
				if (CkAddMarker.Checked)
					AddMarker(Map.FromLocalToLatLng(e.X, e.Y).Lat, Map.FromLocalToLatLng(e.X, e.Y).Lng);
				else if (GetMarkerHovered() is null)
					UnselectAll();
			}
		}

		private void Map_KeyUp(object sender, KeyEventArgs e)
		{
			if (m_bViewOnly)
				return;

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
			if (m_bViewOnly)
				return;

			CkAddMarker.Checked = false;

			if (item.Overlay != MapData.MapOverlayCustom)
				return;

			UnselectAll();

			if (item is GMarkerBriefop selectedGmb)
				SelectMarker(selectedGmb);
		}

		private void Map_OnMarkerEnter(GMapMarker item)
		{
			if (m_bViewOnly)
				return;

			if (item.Overlay == MapData.MapOverlayCustom && item is GMarkerBriefop gmb)
				gmb.IsHovered = true;

		}

		private void Map_OnMarkerLeave(GMapMarker item)
		{
			if (m_bViewOnly)
				return;

			if (item is GMarkerBriefop gmb)
				gmb.IsHovered = false;
		}

		private void Map_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_bViewOnly)
				return;

			if (e.Button == MouseButtons.Left)
			{
				if (GetMarkerPressed() is GMarkerBriefop gmb)
				{
					var pnew = Map.FromLocalToLatLng(e.X, e.Y);
					gmb.Position = pnew;
				}

				Map.Refresh();
			}
		}
		#endregion

		private void BtRefresh_Click(object sender, System.EventArgs e)
		{
			RefreshOverlays();
			Map.Refresh();

			// TODO fin a better way to refresh...
			Map.Zoom += 1;
			Map.Zoom -= 1;
		}

		private void BtZoomOut_Click(object sender, System.EventArgs e)
		{
			Map.Zoom -= 0.1;
			Map.Refresh();
		}

		private void BtZoomIn_Click(object sender, System.EventArgs e)
		{
			Map.Zoom += 0.1;
			Map.Refresh();
		}
	}
}
