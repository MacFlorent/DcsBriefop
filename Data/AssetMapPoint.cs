using CoordinateSharp;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;

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
		public virtual string GetOrbitPattern()
		{
			return null;
		}

		public virtual string GetLocalisationString()
		{
			//sLocalisation = $"{point.Coordinate.ToStringDMS()}{Environment.NewLine}{point.Coordinate.ToStringDDM()}{Environment.NewLine}{point.Coordinate.ToStringMGRS()}";
			return Coordinate.ToStringMGRS();
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

		public override string GetOrbitPattern()
		{
			string sOrbitPattern = null;
			if (MizRoutePoint.RouteTaskHolder is object)
			{
				foreach (MizRouteTask mizTask in MizRoutePoint.RouteTaskHolder.Tasks)
				{
					if (mizTask.Id == ElementRouteTask.Orbit)
					{
						sOrbitPattern = mizTask.Params.Pattern;
						break;
					}
					else if (mizTask.Params.Task is object && mizTask.Params.Task.Id == ElementRouteTask.Orbit)
					{
						sOrbitPattern = mizTask.Params.Task.Params.Pattern;
						break;
					}
				}
			}

			return sOrbitPattern;
		}

		public override string GetLocalisationString()
		{
			//sLocalisation = $"{point.Coordinate.ToStringDMS()}{Environment.NewLine}{point.Coordinate.ToStringDDM()}{Environment.NewLine}{point.Coordinate.ToStringMGRS()}";
			return $"{Coordinate.ToStringMGRS()}{Environment.NewLine}{AltitudeFeet} ft";
		}

		public void SetYX(decimal dY, decimal dX)
		{
			MizRoutePoint.Y = dY;
			MizRoutePoint.X = dX;
			Coordinate = Core.Theatre.GetCoordinate(MizRoutePoint.Y, MizRoutePoint.X);
		}
		#endregion
	}
}