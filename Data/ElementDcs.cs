namespace DcsBriefop.Data
{
	internal static class ElementCoalition
	{
		public static readonly string Blue = "blue";
		public static readonly string Red = "red";
		public static readonly string Neutral = "neutrals";
	}

	internal static class ElementTheater
	{
		public static readonly string PersianGulf = "PersianGulf";
	}

	internal static class ElementSkill
	{
		public static readonly string Player = "Player";
		public static readonly string Client = "Client";
	}

	internal static class ElementTask
	{
		public static readonly string Refueling = "Refueling";
		public static readonly string Awacs = "AWACS";
		public static readonly string Intercept = "Intercept";
		public static readonly string Cap = "CAP";
		public static readonly string GroundAttack = "Ground Attack";
		public static readonly string Sead = "SEAD";
		public static readonly string Cas = "CAS";

		public static readonly List<string> Supports = new List<string>() { Refueling, Awacs };
	}

	internal static class ElementRouteTask
	{
		public static readonly string Orbit = "Orbit";
		public static readonly string Fac = "FAC";
		public static readonly string FacAttackGroup = "FAC_AttackGroup";
		public static readonly string FacEngageGroup = "FAC_EngageGroup";

	}

	internal static class ElementRouteTaskAction
	{
		public static readonly string ActivateBeacon = "ActivateBeacon";
		public static readonly string ActivateIcls = "ActivateICLS";
		public static readonly string ActivateLink4 = "ActivateLink4";
		public static readonly string SetCallsign = "SetCallsign";
		public static readonly string SetFrequency = "SetFrequency";
	}


	internal static class ElementRoutePointType
	{
		public static readonly string TakeOffParking = "TakeOffParking";
		public static readonly string TakeOffParkingHot = "TakeOffParkingHot";
		public static readonly string TakeOff = "TakeOff";
		public static readonly string Land = "Land";
	}

	internal static class ElementRadioModulation
	{
		public static readonly int AM = 0;
		public static readonly int FM = 1;
	}

	internal static class ElementRadio
	{
		public static readonly double UnitFrequencyRatio = 1000000;
	}

	internal static class ElementUnitCategory
	{
		public static readonly string Heliport = "Heliports";
	}

	internal static class ElementDrawingLayer
	{
		public static readonly string Common = "Common";
		public static readonly string Red = "Red";
		public static readonly string Blue = "Blue";
		public static readonly string Neutral = "Neutral";
	}

	internal static class ElementDrawingPrimitive
	{
		public static readonly string Line = "Line";
		public static readonly string Icon = "Icon";
		public static readonly string TextBox = "TextBox";
		public static readonly string Polygon = "Polygon";
	}

	internal static class ElementDrawingPolygonMode
	{
		public static readonly string Rectangle = "rect";
		public static readonly string Oval = "oval";
		public static readonly string Circle = "circle";
		public static readonly string Free = "free";
	}

}
