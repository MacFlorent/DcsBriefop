using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class AssetMapPoint : BaseBriefing
	{
		#region Fields
		#endregion

		#region Properties
		public int Number { get; set; }
		public string Name { get; set; }
		public Coordinate Coordinate { get; set; }
		#endregion

		#region CTOR
		public AssetMapPoint(BriefingPack briefingPack, int iNumber) : base(briefingPack)
		{
			Number = iNumber;
		}

		public AssetMapPoint(BriefingPack briefingPack, int iNumber, string sName, Coordinate coordinate) : this(briefingPack, iNumber)
		{
			Name = sName;
			Coordinate = coordinate;
		}
		#endregion

		#region Methods
		public virtual bool IsOrbitStart()
		{
			return false;
		}
		#endregion
	}

	internal class AssetRoutePoint : AssetMapPoint
	{
		#region Fields
		protected MizRoutePoint m_routePoint;
		#endregion

		#region Properties
		public string AltitudeFeet { get { return $"{UnitsNet.UnitConverter.Convert(m_routePoint.Altitude, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot):0} ft"; } }
		public string Action { get { return m_routePoint.Action; } }

		public List<MizRouteTask> RouteTasks
		{
			get { return m_routePoint.RouteTasks; }
		}
		#endregion

		#region CTOR
		public AssetRoutePoint(BriefingPack briefingPack, int iNumber, AssetGroup asset, MizRoutePoint routePoint) : base(briefingPack, iNumber)
		{
			m_routePoint = routePoint;

			if (!string.IsNullOrEmpty(m_routePoint.Name))
				Name = m_routePoint.Name;
			else
				Name = $"WP{Number}";

			Coordinate = Theatre.GetCoordinate(m_routePoint.Y, m_routePoint.X);
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