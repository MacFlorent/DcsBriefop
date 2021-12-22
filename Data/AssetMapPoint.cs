using CoordinateSharp;
using DcsBriefop.DataMiz;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Data
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
		public AssetMapPoint(BaseBriefingCore core, int iNumber) : base(core)
		{
			Number = iNumber;
		}

		public AssetMapPoint(BaseBriefingCore core, int iNumber, string sName, Coordinate coordinate) : this(core, iNumber)
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
		public string AltitudeFeet { get; set; }
		public string Action { get; set; }

		public List<MizRouteTask> RouteTasks
		{
			get { return m_routePoint.RouteTasks; }
		}
		#endregion

		#region CTOR
		public AssetRoutePoint(BaseBriefingCore core, int iNumber, AssetGroup asset, MizRoutePoint routePoint) : base(core, iNumber)
		{
			m_routePoint = routePoint;

			if (!string.IsNullOrEmpty(m_routePoint.Name))
				Name = m_routePoint.Name;
			else
				Name = $"WP{Number}";

			Coordinate = Core.Theatre.GetCoordinate(m_routePoint.Y, m_routePoint.X);

			AltitudeFeet = $"{UnitsNet.UnitConverter.Convert(m_routePoint.Altitude, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot):0} ft";
			Action = m_routePoint.Action;
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