using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Manipulations;
using System.ComponentModel;

namespace DcsBriefop.Forms
{
	internal partial class UcMap : UserControl, ICustomStylable
	{
		#region Fields
		private string m_sMapProviderName;
		private MemoryLayer m_customMapLayer;
		private BriefopMarker m_selectedMarker;
		private BriefopMarker m_hoveredMarker;
		private BriefopMarker m_draggedMarker;
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
				MapControl.Map.Navigator.CenterOnAndZoomTo(MapProjection.ToMPoint(MapData.CenterLatitude, MapData.CenterLongitude), MapProjection.ZoomToResolution((int)MapData.Zoom), 0, null);
			}

			DataToScreenMapLayers();
		}

		private void DataToScreenMapLayers()
		{
			foreach (MemoryLayer mapLayer in MapControl.Map.Layers.OfType<MemoryLayer>().ToList())
				MapControl.Map.Layers.Remove(mapLayer);

			if (StaticOverlays is not null)
			{
				foreach (ILayer staticMapLayer in StaticOverlays)
					MapControl.Map.Layers.Add(staticMapLayer);
			}

			m_customMapLayer = null;
			m_hoveredMarker = null;
			m_draggedMarker = null;

			if (MapData is not null)
			{
				m_customMapLayer = MapData.BuildCustomMapLayer();
				MapControl.Map.Layers.Add(m_customMapLayer);
			}
		}

		private BriefopMarker HitTestMarker(MPoint worldPoint)
		{
			if (m_customMapLayer is null)
				return null;

			double dResolution = MapControl.Map.Navigator.Viewport.Resolution;
			foreach (PointFeature mapFeature in m_customMapLayer.Features.OfType<PointFeature>())
			{
				BriefopMarker marker = mapFeature.Styles.OfType<BriefopMarkerStyle>().FirstOrDefault()?.Marker;
				if (marker is null)
					continue;
				double dWorldHalfW = marker.GetSizeWidth() / 2.0 * dResolution;
				double dWorldHalfH = marker.GetSizeHeight() / 2.0 * dResolution;
				if (Math.Abs(worldPoint.X - mapFeature.Point.X) <= dWorldHalfW &&
					Math.Abs(worldPoint.Y - mapFeature.Point.Y) <= dWorldHalfH)
					return marker;
			}
			return null;
		}

		private void SelectMarker(BriefopMarker marker)
		{
			if (m_selectedMarker == marker)
				return;

			m_selectedMarker?.IsSelected = false;

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
			m_customMapLayer?.DataHasChanged();
		}
		#endregion

		#region Events
		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Delete && m_hoveredMarker is not null && MapData is not null)
			{
				BriefopMarker toDelete = m_hoveredMarker;
				m_hoveredMarker = null;
				if (toDelete == m_selectedMarker)
					SelectMarker(null);
				MapData.CustomMarkers.Remove(toDelete);
				DataToScreenMapLayers();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void BtAreaSet_Click(object sender, System.EventArgs e)
		{
			if (MapData is null)
				return;

			MPoint center = new(MapControl.Map.Navigator.Viewport.CenterX, MapControl.Map.Navigator.Viewport.CenterY);
			GeoPoint geoPoint = MapProjection.ToGeoPoint(center);
			MapData.CenterLatitude = geoPoint.Latitude;
			MapData.CenterLongitude = geoPoint.Longitude;
			MapData.Zoom = MapProjection.ResolutionToZoom(MapControl.Map.Navigator.Viewport.Resolution);
		}

		private void BtAreaRecall_Click(object sender, System.EventArgs e)
		{
			if (MapData is null)
				return;

			MapControl.Map.Navigator.CenterOnAndZoomTo(MapProjection.ToMPoint(MapData.CenterLatitude, MapData.CenterLongitude),	MapProjection.ZoomToResolution((int)MapData.Zoom), 0, null);
		}

		private void BtRefresh_Click(object sender, System.EventArgs e)
		{
			MapControl.Refresh();
		}

		private void MapControl_MapPointerPressed(object sender, MapEventArgs e)
		{
			if (!Control.MouseButtons.HasFlag(MouseButtons.Right))
				return;

			BriefopMarker marker = HitTestMarker(e.WorldPosition);
			if (marker is not null)
			{
				m_draggedMarker = marker;
				m_draggedMarker.IsPressed = true;
				RefreshCustomLayer();
			}
		}

		private void MapControl_MapPointerReleased(object sender, MapEventArgs e)
		{
			if (m_draggedMarker is not null)
			{
				m_draggedMarker.IsPressed = false;
				m_draggedMarker = null;
				DataToScreenMapLayers();
			}
		}

		private void MapControl_MapPointerMoved(object sender, MapEventArgs e)
		{
			if (m_draggedMarker is not null)
			{
				m_draggedMarker.Position = MapProjection.ToGeoPoint(e.WorldPosition);
				
				PointFeature draggedMapFeature = m_customMapLayer.Features.OfType<PointFeature>().FirstOrDefault(_f => (_f.Styles.OfType<BriefopMarkerStyle>().FirstOrDefault()?.Marker) == m_draggedMarker);
				draggedMapFeature?.Point.X = e.WorldPosition.X;
				draggedMapFeature?.Point.Y = e.WorldPosition.Y;
				draggedMapFeature?.Modified();
				RefreshCustomLayer();
			}
			else
			{
				BriefopMarker hovered = HitTestMarker(e.WorldPosition);
				if (hovered != m_hoveredMarker)
				{
					m_hoveredMarker?.IsHovered = false;
					m_hoveredMarker = hovered;
					m_hoveredMarker?.IsHovered = true;
					RefreshCustomLayer();
				}
			}
		}

		private void MapControl_MapTapped(object sender, MapEventArgs e)
		{
			if (e.GestureType != GestureType.SingleTap)
				return;

			if (CkAddMarker.Checked)
			{
				if (MapData is null)
					return;

				GeoPoint geoPoint = MapProjection.ToGeoPoint(e.WorldPosition);
				BriefopMarker newMarker = BriefopMarker.NewFromTemplateName(geoPoint, ElementMapTemplateMarker.DefaultMark, null, "", 1, 0);
				MapData.CustomMarkers.Add(newMarker);
				DataToScreenMapLayers();
				CkAddMarker.Checked = false;
				SelectMarker(newMarker);
			}
			else
			{
				BriefopMarker hit = HitTestMarker(e.WorldPosition);
				SelectMarker(hit);
			}
		}
		#endregion
	}
}
