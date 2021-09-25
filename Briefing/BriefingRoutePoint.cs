using CoordinateSharp;
using DcsBriefop.LsonStructure;

namespace DcsBriefop.Briefing
{
	internal class BriefingRoutePoint : BaseBriefing
	{
		protected RoutePoint m_routePoint;

		public string Name
		{
			get { return m_routePoint.Name; }
		}

		public Coordinate Coordinate
		{
			get { return m_manager.BriefingPack.Theatre.GetCoordinate(m_routePoint.Y, m_routePoint.X); }
		}


		public BriefingRoutePoint(MissionManager manager, RoutePoint rp) : base(manager)
		{
			m_routePoint = rp;
		}
	}
}