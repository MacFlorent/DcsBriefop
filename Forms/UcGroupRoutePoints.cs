using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
		public UcGroupRoutePoints(BriefopManager briefopManager, BopGroup bopGroup, GMapControl mapControl) : base(briefopManager, bopGroup, mapControl)
		{
			InitializeComponent();

			m_gridManagerRoutePoints = new GridManagerRoutePoints(DgvRoutePoints, m_bopGroup.MapPoints.OfType<BopRoutePoint>());
			m_gridManagerRoutePoints.SelectionChangedTyped += SelectionChangedTypedEvent;

			DataToScreen();

		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			m_gridManagerRoutePoints.RoutePoints = m_bopGroup.MapPoints.OfType<BopRoutePoint>();
			m_gridManagerRoutePoints.Initialize();
			DataToScreenDetail();
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopRoutePoint> selectedBopRoutePoints = m_gridManagerRoutePoints.GetSelected();
			if (selectedBopRoutePoints.Count() == 1)
			{
				BopRoutePoint selectedBopRoutePoint = selectedBopRoutePoints.First();
				if (PnRoutePointDetail.Controls.Count > 0 && !(PnRoutePointDetail.Controls[0] is UcRoutePoint))
				{
					PnRoutePointDetail.Controls.Clear();
				}
				if (m_ucRoutePoint is null)
				{
					m_ucRoutePoint = new UcRoutePoint(m_briefopManager, selectedBopRoutePoint);
				}
				else
				{
					m_ucRoutePoint.BopRoutePoint = selectedBopRoutePoint;
				}

				if (PnRoutePointDetail.Controls.Count == 0)
				{
					PnRoutePointDetail.Controls.Add(m_ucRoutePoint);
					m_ucRoutePoint.Dock = DockStyle.Fill;
				}
			}
			else
			{
				PnRoutePointDetail.Controls.Clear();
			}
		}

		public override void DataToScreenMap()
		{
			BopRoutePoint selectedBopRoutePoint = m_gridManagerRoutePoints.GetSelected().FirstOrDefault();
			Coordinate coordinate = selectedBopRoutePoint?.Coordinate ?? m_bopGroup.Coordinate;
			m_mapControl.Overlays.Clear();
			m_mapControl.Overlays.Add(m_bopGroup.GetMapOverlayRoute(selectedBopRoutePoint?.Number, ElementMapOverlayRouteDisplay.PointLabelLight));

			m_mapControl.Position = new PointLatLng(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);
			m_mapControl.ForceRefresh();
		}

		public override void ScreenToData()
		{
			ScreenToDataDetail();
		}

		public void ScreenToDataDetail()
		{
			m_ucRoutePoint.ScreenToData();
		}
		#endregion

		#region Events
		private void SelectionChangedTypedEvent(object sender, GridManagerRoutePoints.EventArgsBopRoutePoints e)
		{
			ScreenToDataDetail();
			DataToScreenMap();
			DataToScreenDetail();
		}
		#endregion
	}
}
