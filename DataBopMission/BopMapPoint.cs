using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using UnitsNet.Units;

namespace DcsBriefop.DataBopMission
{
	internal class BopMapPoint : BaseBop
	{
		#region Fields
		private decimal m_mizY;
		private decimal m_mizX;
		#endregion

		#region Properties
		public string Name { get; set; }
		public Coordinate Coordinate { get; set; }
		#endregion

		#region CTOR
		public BopMapPoint(Miz miz, Theatre theatre, decimal Y, decimal X) : base(miz, theatre)
		{
			m_mizY = Y;
			m_mizX = X;
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			Coordinate = Theatre.GetCoordinate(m_mizY, m_mizX);
		}
		#endregion

		#region Methods
		#endregion
	}

	internal class BopRoutePoint : BopMapPoint
	{
		#region Fields
		private MizRoutePoint m_mizRoutePoint;
		#endregion

		#region Properties
		public MizRoutePoint MizRoutePoint { get { return m_mizRoutePoint; } }
		public int Number { get; set; }
		public string Type { get; set; }
		public string Action { get; set; }
		public decimal AltitudeFeet { get; set; }
		public int? AirdromeId { get; set; }
		public int? HelipadId { get; set; }
		#endregion

		#region CTOR
		public BopRoutePoint(Miz miz, Theatre theatre, int iNumber, MizRoutePoint mizRoutePoint) : base(miz, theatre, mizRoutePoint.Y, mizRoutePoint.X)
		{
			Number = iNumber;
			m_mizRoutePoint = mizRoutePoint;

			Name = m_mizRoutePoint.Name;
			Action = m_mizRoutePoint.Action;
			Type = m_mizRoutePoint.Type;
			AirdromeId = m_mizRoutePoint.AirdromeId;
			HelipadId = m_mizRoutePoint.HelipadId;
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			m_mizRoutePoint.Name = Name;
			m_mizRoutePoint.Action = Action;
			m_mizRoutePoint.Type = Type;
		}

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			AltitudeFeet = (decimal)UnitsNet.UnitConverter.Convert(m_mizRoutePoint.Altitude, LengthUnit.Meter, LengthUnit.Foot);
		}
		#endregion

		#region Methods
		//public string GetOrbitPattern()
		//{
		//	string sOrbitPattern = null;
		//	if (m_mizRoutePoint.RouteTaskHolder is object)
		//	{
		//		foreach (MizRouteTask mizTask in m_mizRoutePoint.RouteTaskHolder.Tasks)
		//		{
		//			if (mizTask.Id == ElementRouteTask.Orbit)
		//			{
		//				sOrbitPattern = mizTask.Params.Pattern;
		//				break;
		//			}
		//			else if (mizTask.Params.Task is object && mizTask.Params.Task.Id == ElementRouteTask.Orbit)
		//			{
		//				sOrbitPattern = mizTask.Params.Task.Params.Pattern;
		//				break;
		//			}
		//		}
		//	}

		//	return sOrbitPattern;
		//}
		#endregion
	}
}