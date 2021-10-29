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
		public override string Name
		{
			get { return m_routePoint.Name; }
		}

		public override Coordinate Coordinate
		{
			get { return Theatre.GetCoordinate(m_routePoint.Y, m_routePoint.X); }
		}

		public List<RouteTask> RouteTasks
		{
			get { return m_routePoint.RouteTasks; }
		}
		#endregion

		#region CTOR
		public AssetRoutePoint(BriefingPack briefingPack, RoutePoint routePoint) : base(briefingPack)
		{
			m_routePoint = routePoint;
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