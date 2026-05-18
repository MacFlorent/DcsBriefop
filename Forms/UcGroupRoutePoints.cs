using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Map;
using Mapsui;
using Mapsui.Layers;

namespace DcsBriefop.Forms
{
	internal partial class UcGroupRoutePoints : UcGroupBase
	{
		#region Fields
		private GridManagerRoutePoints m_gridManagerRoutePoints;
		private UcRoutePoint m_ucRoutePoint;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public UcGroupRoutePoints(BriefopManager briefopManager, BopGroup bopGroup, Mapsui.UI.WindowsForms.MapControl mapControl) : base(briefopManager, bopGroup, mapControl)
		{
			InitializeComponent();

			m_gridManagerRoutePoints = new(DgvRoutePoints, null);
			m_gridManagerRoutePoints.SelectionChanged += SelectionChangedEvent;
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			m_gridManagerRoutePoints.SelectionChanged -= SelectionChangedEvent;

			m_gridManagerRoutePoints.Elements = m_bopGroup.RoutePoints;
			m_gridManagerRoutePoints.Refresh();
			DataToScreenDetail();

			m_gridManagerRoutePoints.SelectionChanged += SelectionChangedEvent;
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopRoutePoint> selectedBopRoutePoints = m_gridManagerRoutePoints.GetSelectedElements();
			if (selectedBopRoutePoints.Count() == 1)
			{
				BopRoutePoint selectedBopRoutePoint = selectedBopRoutePoints.First();
				if (PnRoutePointDetail.Controls.Count > 0 && !(PnRoutePointDetail.Controls[0] is UcRoutePoint))
				{
					PnRoutePointDetail.Controls.Clear();
				}

				m_ucRoutePoint ??= new UcRoutePoint(m_briefopManager);
				
				if (PnRoutePointDetail.Controls.Count == 0)
				{
					PnRoutePointDetail.Controls.Add(m_ucRoutePoint);
					m_ucRoutePoint.Dock = DockStyle.Fill;
				}

				m_ucRoutePoint.BopRoutePoint = selectedBopRoutePoint;
			}
			else
			{
				PnRoutePointDetail.Controls.Clear();
			}
		}

		public override void DataToScreenMap()
		{
			if (!Visible)
				return;

			BopRoutePoint selectedBopRoutePoint = m_gridManagerRoutePoints.GetSelectedElements().FirstOrDefault();
			Coordinate coordinate = selectedBopRoutePoint?.Coordinate ?? m_bopGroup.Coordinate;

			foreach (MemoryLayer mapLayer in m_mapControl.Map.Layers.OfType<MemoryLayer>().ToList())
				m_mapControl.Map.Layers.Remove(mapLayer);

			m_mapControl.Map.Layers.Add(m_bopGroup.GetRouteMapLayer(selectedBopRoutePoint?.Number, ElementMapLayerRouteDisplay.PointLabelLight, PreferencesManager.Preferences.Briefing.MeasurementSystem));

			MPoint center = MapProjection.ToMPoint(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);
			m_mapControl.Map.Navigator.CenterOnAndZoomTo(center, MapProjection.ZoomToResolution((int)PreferencesManager.Preferences.Map.Zoom), 0, null);
		}

		public override void ScreenToData()
		{
			ScreenToDataDetail();
		}

		public void ScreenToDataDetail()
		{
			m_ucRoutePoint?.ScreenToData();
		}
		#endregion

		#region Events
		private void SelectionChangedEvent(object sender, EventArgs e)
		{
			ScreenToDataDetail();
			DataToScreenMap();
			DataToScreenDetail();
		}
		#endregion
	}
}
