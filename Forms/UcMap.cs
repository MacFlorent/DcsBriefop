using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using Mapsui;
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
		#endregion

		#region Properties
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MizBopMap MapData { get; set; }
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public IEnumerable<GMapOverlay> StaticOverlays { get; set; } // TODO Phase 3: change to IEnumerable<ILayer> when drawing layers are migrated
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
			// TODO Phase 2: add custom marker layer from MapData.BuildCustomLayer()
			// TODO Phase 3: add static overlay layers (drawing objects) when migrated
		}

		public void ScreenToData()
		{
			OverlayToData();
		}

		public void OverlayToData()
		{
			// TODO Phase 2: read marker positions back from the Mapsui layer into MapData
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
			// TODO Phase 2: start marker drag if a marker is hovered
		}

		private void Map_MouseUp(object sender, MouseEventArgs e)
		{
			// TODO Phase 2: release pressed marker state
		}

		private void Map_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (CkAddMarker.Checked)
				{
					GeoPoint geoPoint = MapProjection.ScreenToGeoPoint(MapControl.Map.Navigator.Viewport, e.X, e.Y);
					// TODO Phase 2: add marker at geoPoint via MemoryLayer
					CkAddMarker.Checked = false;
				}
			}
		}

		private void Map_KeyUp(object sender, KeyEventArgs e)
		{
			// TODO Phase 2: delete hovered marker on Delete key
		}

		private void Map_MouseMove(object sender, MouseEventArgs e)
		{
			// TODO Phase 2: drag pressed marker to new position
		}

		private void BtRefresh_Click(object sender, System.EventArgs e)
		{
			MapControl.Refresh();
		}
		#endregion
	}
}
