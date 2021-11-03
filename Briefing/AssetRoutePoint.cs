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
			get
			{
				if (!string.IsNullOrEmpty(m_routePoint.Name))
					return m_routePoint.Name;
				else
					return $"WP{Number}";
			}
		}

		public override Coordinate Coordinate
		{
			get { return Theatre.GetCoordinate(m_routePoint.Y, m_routePoint.X); }
		}

		public string AltitudeFeet{ get { return $"{UnitsNet.UnitConverter.Convert(m_routePoint.Altitude, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot):0} ft"; } }
		public string Action { get { return m_routePoint.Action; } }

		public List<RouteTask> RouteTasks
		{
			get { return m_routePoint.RouteTasks; }
		}
		#endregion

		#region CTOR
		public AssetRoutePoint(BriefingPack briefingPack, Asset asset, int iNumber, RoutePoint routePoint) : base(briefingPack, asset, iNumber)
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