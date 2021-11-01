using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.LsonStructure;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class AssetRoutePoint : AssetMapPoint
	{
		#region Fields
		protected RoutePoint m_routePoint;
		#endregion

		#region Properties
		public int Number { get; set; }

		public override string Name
		{
			get
			{
				if (string.IsNullOrEmpty(m_routePoint.Name))
					return m_routePoint.Name;
				else
					return $"Waypoint {Number}";
			}
		}

		public override Coordinate Coordinate
		{
			get { return Theatre.GetCoordinate(m_routePoint.Y, m_routePoint.X); }
		}

		public decimal Altitude { get { return m_routePoint.Altitude; } }
		public string Action { get { return m_routePoint.Action; } }
		public string Notes { get; set; }

		public List<RouteTask> RouteTasks
		{
			get { return m_routePoint.RouteTasks; }
		}
		#endregion

		#region CTOR
		public AssetRoutePoint(BriefingPack briefingPack, RoutePoint routePoint, int iNumber) : base(briefingPack)
		{
			m_routePoint = routePoint;
			Number = iNumber;
		}
		#endregion

		#region Methods
		public override bool IsOrbitStart()
		{
			return RouteTasks.Where(_rt => _rt.Id == ElementRouteTask.Orbit).Any();
		}
		#endregion
	}
}