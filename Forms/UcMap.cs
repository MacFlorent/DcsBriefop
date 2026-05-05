using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using Mapsui;
using Mapsui.Layers;
using System.ComponentModel;

//https://stackoverflow.com/questions/9308673/how-to-draw-circle-on-the-map-using-gmap-net-in-c-sharp
//http://www.independent-software.com/gmap-net-beginners-tutorial-maps-markers-polygons-routes-updated-for-vs2015-and-gmap1-7.html
//https://icon-icons.com/fr/icone/bullseye/73647

namespace DcsBriefop.Forms
{
	internal partial class UcMap : UserControl, ICustomStylable
	{
		#region Fields
		private string m_sMapProviderName;
		private MemoryLayer m_customMarkerLayer;
		private List<ILayer> m_staticLayers = [];
		private Dictionary<BriefopMarker, PointFeature> m_markerFeatures = [];
		private BriefopMarker m_hoveredMarker;
		private BriefopMarker m_draggedMarker;
		private BriefopMarker m_selectedMarker;
		#endregion

		#region Properties
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MizBopMap MapData { get; set; }
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<ILayer> StaticOverlays { get; set; }
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string MapProviderName
		{
			get { return m_sMapProviderName; }
			set { m_sMapProviderName = value; }
		}
		#endregion

		#region CTOR
		public UcMap()
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			PnSelectionDetail.Visible = false;
		}
		#endregion

		#region ICustomStylable
		public void ApplyCustomStyle()
		{
		}
		#endregion

		#region Methods
		public void DataToScreen()
		{
			MapControl.InitializeMapControl(m_sMapProviderName);

			if (MapData is not null)
			{
				MapControl.Map.Navigator.CenterOnAndZoomTo(
					MapProjection.ToMPoint(MapData.CenterLatitude, MapData.CenterLongitude),
					MapProjection.ZoomToResolution((int)MapData.Zoom), 0, null);
			}

			DataToOverlay();
		}

		public void DataToOverlay()
		{
			foreach (ILayer staticLayer in m_staticLayers)
				MapControl.Map.Layers.Remove(staticLayer);
			m_staticLayers.Clear();

			if (m_customMarkerLayer is not null)
				MapControl.Map.Layers.Remove(m_customMarkerLayer);
			m_customMarkerLayer = null;
			m_markerFeatures.Clear();
			m_hoveredMarker = null;
			m_draggedMarker = null;

			if (StaticOverlays is not null)
			{
				foreach (ILayer staticLayer in StaticOverlays)
				{
					MapControl.Map.Layers.Add(staticLayer);
					m_staticLayers.Add(staticLayer);
				}
			}

			if (MapData is not null)
			{
				List<IFeature> features = new();
				foreach (BriefopMarker marker in MapData.CustomMarkers)
				{
					PointFeature feature = new(MapProjection.ToMPoint(marker.Position));
					feature.Styles.Add(new BriefopMarkerStyle(marker));
					m_markerFeatures[marker] = feature;
					features.Add(feature);
				}
				m_customMarkerLayer = new MemoryLayer("CustomMarkers") { Features = features };
				MapControl.Map.Layers.Add(m_customMarkerLayer);
			}
		}

		public void ScreenToData()
		{
			OverlayToData();
		}

		public void OverlayToData()
		{
			// BriefopMarker instances in CustomMarkers are the same references used in features' styles.
			// Positions are updated in-place during drag so no explicit sync is needed here.
		}
		#endregion

		#region Marker interactions
		private BriefopMarker HitTestMarker(MouseEventArgs e)
		{
			if (MapData is null || m_markerFeatures.Count == 0)
				return null;

			double dResolution = MapControl.Map.Navigator.Viewport.Resolution;
			GeoPoint geoPoint = MapProjection.ScreenToGeoPoint(MapControl.Map.Navigator.Viewport, e.X, e.Y);
			MPoint worldPoint = MapProjection.ToMPoint(geoPoint);

			foreach (BriefopMarker marker in MapData.CustomMarkers)
			{
				MPoint markerWorld = MapProjection.ToMPoint(marker.Position);
				double dWorldHalfW = marker.GetSizeWidth() / 2.0 * dResolution;
				double dWorldHalfH = marker.GetSizeHeight() / 2.0 * dResolution;
				if (Math.Abs(worldPoint.X - markerWorld.X) <= dWorldHalfW &&
					Math.Abs(worldPoint.Y - markerWorld.Y) <= dWorldHalfH)
					return marker;
			}
			return null;
		}

		private void SelectMarker(BriefopMarker marker)
		{
			if (m_selectedMarker == marker)
				return;

			if (m_selectedMarker is not null)
				m_selectedMarker.IsSelected = false;

			m_selectedMarker = marker;

			if (m_selectedMarker is not null)
			{
				m_selectedMarker.IsSelected = true;
				PnSelectionDetail.Controls.Clear();
				UcMarkerDetail ucDetail = new(m_selectedMarker, RefreshCustomLayer);
				ucDetail.Dock = DockStyle.Fill;
				PnSelectionDetail.Controls.Add(ucDetail);
				PnSelectionDetail.Visible = true;
			}
			else
			{
				PnSelectionDetail.Controls.Clear();
				PnSelectionDetail.Visible = false;
			}

			RefreshCustomLayer();
		}

		private void RefreshCustomLayer()
		{
			m_customMarkerLayer?.DataHasChanged();
		}
		#endregion

		#region Events
		private void BtAreaSet_Click(object sender, System.EventArgs e)
		{
			if (MapData is null)
				return;

			MPoint center = new MPoint(MapControl.Map.Navigator.Viewport.CenterX, MapControl.Map.Navigator.Viewport.CenterY);
			GeoPoint geoPoint = MapProjection.ToGeoPoint(center);
			MapData.CenterLatitude = geoPoint.Latitude;
			MapData.CenterLongitude = geoPoint.Longitude;
			MapData.Zoom = MapProjection.ResolutionToZoom(MapControl.Map.Navigator.Viewport.Resolution);
		}

		private void BtAreaRecall_Click(object sender, System.EventArgs e)
		{
			if (MapData is null)
				return;

			MapControl.Map.Navigator.CenterOnAndZoomTo(
				MapProjection.ToMPoint(MapData.CenterLatitude, MapData.CenterLongitude),
				MapProjection.ZoomToResolution((int)MapData.Zoom), 0, null);
		}

		private void Map_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			BriefopMarker hit = HitTestMarker(e);
			if (hit is not null)
			{
				m_draggedMarker = hit;
				m_draggedMarker.IsPressed = true;
				RefreshCustomLayer();
			}
		}

		private void Map_MouseUp(object sender, MouseEventArgs e)
		{
			if (m_draggedMarker is not null)
			{
				m_draggedMarker.IsPressed = false;
				m_draggedMarker = null;
				RefreshCustomLayer();
			}
		}

		private void Map_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left)
				return;

			if (CkAddMarker.Checked)
			{
				if (MapData is null)
					return;

				GeoPoint geoPoint = MapProjection.ScreenToGeoPoint(MapControl.Map.Navigator.Viewport, e.X, e.Y);
				BriefopMarker newMarker = BriefopMarker.NewFromTemplateName(geoPoint, ElementMapTemplateMarker.DefaultMark, null, "", 1, 0);
				MapData.CustomMarkers.Add(newMarker);
				DataToOverlay();
				CkAddMarker.Checked = false;
				SelectMarker(newMarker);
			}
			else
			{
				BriefopMarker hit = HitTestMarker(e);
				SelectMarker(hit);
			}
		}

		private void Map_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && m_hoveredMarker is not null && MapData is not null)
			{
				BriefopMarker toDelete = m_hoveredMarker;
				m_hoveredMarker = null;
				if (toDelete == m_selectedMarker)
					SelectMarker(null);
				MapData.CustomMarkers.Remove(toDelete);
				DataToOverlay();
			}
		}

		private void Map_MouseMove(object sender, MouseEventArgs e)
		{
			if (m_draggedMarker is not null)
			{
				GeoPoint newPos = MapProjection.ScreenToGeoPoint(MapControl.Map.Navigator.Viewport, e.X, e.Y);
				m_draggedMarker.Position = newPos;
				if (m_markerFeatures.TryGetValue(m_draggedMarker, out PointFeature feature))
				{
					MPoint newWorld = MapProjection.ToMPoint(newPos);
					feature.Point.X = newWorld.X;
					feature.Point.Y = newWorld.Y;
					feature.Modified();
				}
				RefreshCustomLayer();
			}
			else
			{
				BriefopMarker hovered = HitTestMarker(e);
				if (hovered != m_hoveredMarker)
				{
					if (m_hoveredMarker is not null)
						m_hoveredMarker.IsHovered = false;
					m_hoveredMarker = hovered;
					if (m_hoveredMarker is not null)
						m_hoveredMarker.IsHovered = true;
					RefreshCustomLayer();
				}
			}
		}

		private void BtRefresh_Click(object sender, System.EventArgs e)
		{
			MapControl.Refresh();
		}
		#endregion
	}
}
