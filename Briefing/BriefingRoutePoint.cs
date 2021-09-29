﻿using CoordinateSharp;
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
			get { return Theatre.GetCoordinate(m_routePoint.Y, m_routePoint.X); }
		}


		public BriefingRoutePoint(BriefingPack bp, RoutePoint rp) : base(bp)
		{
			m_routePoint = rp;
		}
	}
}