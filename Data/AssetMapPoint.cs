﻿using CoordinateSharp;
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
		#endregion

		#region Properties
		public MizRoutePoint MizRoutePoint { get; set; }

		public string Label
		{
			get
			{
				if (!string.IsNullOrEmpty(Name))
					return Name;
				else
					return $"WP{Number}";
			}
		}
		public string AltitudeFeet { get; set; }
		public string Type { get; set; }
		public string Action { get; set; }
		public int? AirdromeId { get; set; }
		public int? HelipadId { get; set; }

		public List<MizRouteTask> RouteTasks
		{
			get { return MizRoutePoint.RouteTasks; }
		}
		#endregion

		#region CTOR
		public AssetRoutePoint(BaseBriefingCore core, int iNumber, AssetGroup asset, MizRoutePoint routePoint) : base(core, iNumber)
		{
			MizRoutePoint = routePoint;

			Name = MizRoutePoint.Name;
			Coordinate = Core.Theatre.GetCoordinate(MizRoutePoint.Y, MizRoutePoint.X);

			AltitudeFeet = $"{UnitsNet.UnitConverter.Convert(MizRoutePoint.Altitude, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot):0} ft";
			Action = MizRoutePoint.Action;
			Type = MizRoutePoint.Type;
			AirdromeId = MizRoutePoint.AirdromeId;
			HelipadId = MizRoutePoint.HelipadId;
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			base.Persist();
			//Core.Theatre.GetYX(out decimal dY, out decimal dX, Coordinate);
			//MizRoutePoint.Y = dY;
			//MizRoutePoint.X = dX;

			MizRoutePoint.Name = Name;
			MizRoutePoint.Action = Action;
			MizRoutePoint.Type = Type;
		}
		public override bool IsOrbitStart()
		{
			return RouteTasks.Where(_rt => _rt.Id == ElementRouteTask.Orbit).Any();
		}
		#endregion
	}
}