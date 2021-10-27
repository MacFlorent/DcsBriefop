using System.Collections.Generic;

namespace DcsBriefop.MasterData
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
		public static readonly string ActivateBeacon = "ActivateBeacon";
		public static readonly string Orbit = "Orbit";
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

	internal static class ElementAircraftType
	{
	}
}
