using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;

namespace DcsBriefop.DataBriefop
{
	internal class BriefopMapPoint : BaseBriefop
	{
		#region Fields
		#endregion

		#region Properties
		public string Name { get; set; }
		public Coordinate Coordinate { get; set; }
		#endregion

		#region CTOR
		public BriefopMapPoint(BriefopManager parentManager) : base(parentManager) { }

		public BriefopMapPoint(BriefopManager parentManager, string sName, Coordinate coordinate) : this(parentManager)
		{
			Coordinate = coordinate;
		}
		#endregion

		#region Methods
		public virtual string ToStringLocalisation()
		{
			//sLocalisation = $"{point.Coordinate.ToStringDMS()}{Environment.NewLine}{point.Coordinate.ToStringDDM()}{Environment.NewLine}{point.Coordinate.ToStringMGRS()}";
			return Coordinate.ToStringMGRS();
		}
		#endregion
	}

	internal class BriefopRoutePoint : BriefopMapPoint
	{
		#region Fields
		private MizRoutePoint m_mizRoutePoint;
		#endregion

		#region Properties
		public MizRoutePoint MizRoutePoint { get { return m_mizRoutePoint; } }

		public string DisplayName
		{
			get
			{
				if (!string.IsNullOrEmpty(Name))
					return Name;
				else
					return $"WP{Number}";
			}
		}
		public int Number { get; set; }
		public string Type { get; set; }
		public string Action { get; set; }
		public decimal AltitudeFeet { get; set; }
		public int? AirdromeId { get; set; }
		public int? HelipadId { get; set; }
		#endregion

		#region CTOR
		public BriefopRoutePoint(BriefopManager parentManager, int iNumber, MizRoutePoint mizRoutePoint) : base(parentManager)
		{
			m_mizRoutePoint = mizRoutePoint;

			Number = iNumber;
			Name = m_mizRoutePoint.Name;
			Coordinate = ParentManager.Theatre.GetCoordinate(m_mizRoutePoint.Y, m_mizRoutePoint.X);

			AltitudeFeet = (decimal)UnitsNet.UnitConverter.Convert(m_mizRoutePoint.Altitude, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot);
			Action = m_mizRoutePoint.Action;
			Type = m_mizRoutePoint.Type;
			AirdromeId = m_mizRoutePoint.AirdromeId;
			HelipadId = m_mizRoutePoint.HelipadId;
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			base.Persist();
			//Core.Theatre.GetYX(out decimal dY, out decimal dX, Coordinate);
			//MizRoutePoint.Y = dY;
			//MizRoutePoint.X = dX;

			m_mizRoutePoint.Name = Name;
			m_mizRoutePoint.Action = Action;
			m_mizRoutePoint.Type = Type;
		}

		public string GetOrbitPattern()
		{
			string sOrbitPattern = null;
			if (m_mizRoutePoint.RouteTaskHolder is object)
			{
				foreach (MizRouteTask mizTask in m_mizRoutePoint.RouteTaskHolder.Tasks)
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

		public override string ToStringLocalisation()
		{
			//sLocalisation = $"{point.Coordinate.ToStringDMS()}{Environment.NewLine}{point.Coordinate.ToStringDDM()}{Environment.NewLine}{point.Coordinate.ToStringMGRS()}";
			return $"{base.ToStringLocalisation()}{Environment.NewLine}{AltitudeFeet} ft";
		}

		public void SetYX(decimal dY, decimal dX)
		{
			MizRoutePoint.Y = dY;
			MizRoutePoint.X = dX;
			Coordinate = ParentManager.Theatre.GetCoordinate(MizRoutePoint.Y, MizRoutePoint.X);
		}
		#endregion
	}
}