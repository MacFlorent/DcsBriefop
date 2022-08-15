﻿using DcsBriefop.Data;
using DcsBriefop.DataBopCustom;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.Windows.Forms;

//https://stackoverflow.com/questions/9308673/how-to-draw-circle-on-the-map-using-gmap-net-in-c-sharp
//http://www.independent-software.com/gmap-net-beginners-tutorial-maps-markers-polygons-routes-updated-for-vs2015-and-gmap1-7.html
//https://icon-icons.com/fr/icone/bullseye/73647https://icon-icons.com/fr/icone/bullseye/73647

namespace DcsBriefop.FormBop
{
	internal partial class UcMap : UserControl, ICustomStylable
	{
		#region Fields
		private bool m_bViewOnly;
		#endregion

		#region Properties
		public BopCustomMap MapData { get; private set; }
		#endregion

		#region CTOR
		public UcMap()
		{
			InitializeComponent();

			Map.MapProvider = GMapProviders.TryGetProvider(Preferences.PreferencesManager.Preferences.Map.DefaultProvider);
			GMaps.Instance.Mode = AccessMode.ServerOnly;
			//Map.ShowTileGridLines = true;
			Map.Position = new PointLatLng(26.1702778, 56.24);
			Map.MinZoom = ElementMapValue.MinZoom;
			Map.MaxZoom = ElementMapValue.MaxZoom;

			CbMapProvider.ValueMember = "Name";
			CbMapProvider.DataSource = GMapProviders.List;
			CbMapProvider.SelectedItem = Map.MapProvider;			
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
		public void SetMapData(BopCustomMap mapData, string sTheater, bool bViewOnly)
		{
			m_bViewOnly = bViewOnly;
			LbTheater.Text = sTheater;
			LbTitle.Text = mapData.DisplayName;

			CkAddMarker.Enabled = BtAreaSet.Enabled = BtAreaRecall.Enabled = !m_bViewOnly;
			MapData = mapData;
			RefreshMapData();
		}

		public void RefreshMapData()
		{
			RefreshOverlays();
			Map.MapProvider = MapData.GetMapProvider();
			Map.Position = new PointLatLng(MapData.CenterLatitude, MapData.CenterLongitude);
			Map.Zoom = MapData.Zoom;

			CbMapProvider.SelectedItem = Map.MapProvider;
		}

		public void ForceRefresh()
		{
			RefreshOverlays();
			Map.Refresh();

			// TODO find a better way to refresh...
			Map.Zoom += 1;
			Map.Zoom -= 1;
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
			MapData.MapOverlayCustom.Markers.Add(GMarkerBriefop.NewFromTemplateName(p, ElementMapTemplateMarker.DefaultMark, Color.Orange, null, 1, 0));
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
			foreach (var marker in MapData.MapOverlayCustom.Markers)
			{
				if (marker is GMarkerBriefop markerBriefop)
					markerBriefop.IsSelected = false;
			}

			// hide detail
			PnSelectionDetail.Controls.Clear();
		}

		private GMarkerBriefop GetMarkerHovered()
		{
			foreach (var marker in MapData.MapOverlayCustom.Markers)
			{
				if (marker is GMarkerBriefop markerBriefop && markerBriefop.IsHovered)
					return markerBriefop;
			}

			return null;
		}

		private GMarkerBriefop GetMarkerPressed()
		{
			foreach (var marker in MapData.MapOverlayCustom.Markers)
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

			foreach (var marker in MapData.MapOverlayCustom.Markers)
			{
				if (marker is GMarkerBriefop markerBriefop)
					markerBriefop.IsPressed = false;

			}
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

		private void BtRefresh_Click(object sender, System.EventArgs e)
		{
			ForceRefresh();
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

		private void CbMapProvider_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			Map.MapProvider = CbMapProvider.SelectedItem as GMapProvider;
			MapData.Provider = Map.MapProvider.Name;
		}
		#endregion
	}
}