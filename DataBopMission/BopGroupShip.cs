using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.DataBopMission
{
	internal class BopGroupShip : BopGroup
	{
		#region Fields
		#endregion

		#region Properties
		public Tacan Tacan { get; set; }
		public int? Icls { get; set; }
		public decimal? Link4 { get; set; }
		#endregion

		#region CTOR
		public BopGroupShip(Miz miz, Theatre theatre, string sCoalitionName, string sCountryName, MizGroup mizGroup) : base(miz, theatre, sCoalitionName, sCountryName, ElementDcsGroupType.Ship, ElementDcsObjectClass.Sea, mizGroup)
		{
			Radio = (MainUnit as BopUnitShip)?.Radio;

			Tacan = (MainUnit as BopUnitShip)?.Tacan;
			if (Tacan is null)
				Tacan = Units.OfType<BopUnitShip>().Where(_u => _u.Tacan is object).Select(_u => _u.Tacan).FirstOrDefault();
			if (Tacan is null)
				Tacan = GetTacanFromRouteTaskAction(null);

			Icls = (MainUnit as BopUnitShip)?.Icls;
			if (Icls is null)
				Icls = Units.OfType<BopUnitShip>().Where(_u => _u.Icls is object).Select(_u => _u.Icls).FirstOrDefault();
			if (Icls is null)
				Icls = GetIclsFromRouteTaskAction(null);

			Link4 = (MainUnit as BopUnitShip)?.Link4;
			if (Link4 is null)
				Link4 = Units.OfType<BopUnitShip>().Where(_u => _u.Link4 is object).Select(_u => _u.Link4).FirstOrDefault();
			if (Link4 is null)
				Link4 = GetLink4FromRouteTaskAction(null);
		}
		#endregion

		#region Miz
		protected override void FromMizUnits()
		{
			Units = new List<BopUnit>();
			foreach (MizUnit mizUnit in m_mizGroup.Units)
			{
				BopUnitShip bopUnitSea = new BopUnitShip(Miz, Theatre, this, mizUnit);
				Units.Add(bopUnitSea);
				if (MainUnit is null)
					MainUnit = bopUnitSea;
				else if ((MainUnit.Attributes & ElementDcsObjectAttribute.AircraftCarrier) == 0)
				{
					if ((bopUnitSea.Attributes & ElementDcsObjectAttribute.AircraftCarrier) > 0)
						MainUnit = bopUnitSea;
					else if (!MainUnit.MainInGroup && bopUnitSea.MainInGroup)
						MainUnit = MainUnit = bopUnitSea;
				}
			}
		}
		#endregion

		#region Methods
		public override string ToStringAdditionnal()
		{
			string sDescription = base.ToStringDisplayName();
			if (Tacan is object)
				sDescription = $"{sDescription} TACAN:{Tacan}";
			if (Icls is object)
				sDescription = $"{sDescription} ICLS:{Icls}";
			if (Link4 is object)
				sDescription = $"{sDescription} LNK4:{Link4:###.000}";

			return sDescription;
		}

		public int? GetIclsFromRouteTaskAction(int? iUnitId)
		{
			MizRouteTaskAction routeTaskAction = GetRouteTaskAction(new List<string> { ElementRouteTaskAction.ActivateIcls }, iUnitId);
			if (routeTaskAction is object)
				return routeTaskAction.ParamChannel;
			else
				return null;
		}

		public decimal? GetLink4FromRouteTaskAction(int? iUnitId)
		{
			MizRouteTaskAction routeTaskAction = GetRouteTaskAction(new List<string> { ElementRouteTaskAction.ActivateLink4 }, iUnitId);
			if (routeTaskAction is object)
				return (decimal)routeTaskAction.ParamFrequency / ElementRadio.UnitFrequencyRatio;
			else
				return null;
		}

		//public string ToStringRadio()
		//{
		//	if (Playable)
		//		return "initial preset only";
		//	else
		//		return Radio.ToString();
		//}

		//private string ToStringBaseInformation()
		//{
		//	StringBuilder sb = new StringBuilder();
		//	foreach (Airdrome airdrome in GetAirdromeIds().Select(_i => ParentManager.Theatre.GetAirdrome(_i)))
		//	{
		//		if (airdrome is object)
		//		{
		//			if (sb.Length <= 0)
		//			{
		//				sb.Append($"Base={airdrome.Name}");
		//			}
		//			else
		//			{
		//				sb.AppendWithSeparator($"{airdrome.Name}", ",");
		//			}
		//		}
		//	}
		//	foreach (AssetShip carrier in GetCarrierAssets())
		//	{
		//		if (sb.Length <= 0)
		//		{
		//			sb.Append($"Base={carrier.Name}");
		//		}
		//		else
		//		{
		//			sb.AppendWithSeparator($"{carrier.Name}", ",");
		//		}
		//	}

		//	return sb.ToString();
		//}

		//public List<int> GetAirdromeIds()
		//{
		//	IEnumerable<int> grouped = MapPoints.OfType<BopRoutePoint>()
		//		.Where(_rp => _rp.AirdromeId is object
		//			&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
		//		.GroupBy(_rp => _rp.AirdromeId).Select(_g => _g.Key.Value);

		//	return grouped.ToList();
		//}

		//public List<AssetAirdrome> GetAirdromeAssets()
		//{
		//	List<int> flightAirdromes = GetAirdromeIds();
		//	return Coalition.Airdromes.Where(_a => flightAirdromes.Contains(_a.Id)).ToList();
		//}

		//public List<AssetShip> GetCarrierAssets()
		//{
		//	IEnumerable<int> grouped = MapPoints.OfType<AssetRoutePoint>()
		//		.Where(_rp => _rp.HelipadId is object
		//			&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
		//		.GroupBy(_rp => _rp.HelipadId).Select(_g => _g.Key.Value);

		//	return Coalition.OwnAssets.OfType<AssetShip>().Where(_a => grouped.Contains(_a.MainUnit.Id)).ToList();
		//}

		//private void AddBullseyeWaypoint()
		//{
		//	AssetRoutePoint bullsPoint = MapPoints.OfType<AssetRoutePoint>().Where(_mp => _mp.Name == m_sBullsPointName).FirstOrDefault();

		//	if (bullsPoint is null)
		//	{
		//		MizRoutePoint mizRoutePoint = MizRoutePoint.NewFromLuaTemplate();
		//		mizRoutePoint.Y = Coalition.MizCoalition.BullseyeY;
		//		mizRoutePoint.X = Coalition.MizCoalition.BullseyeX;

		//		AssetRoutePoint routePoint = new AssetRoutePoint(Core, 0, this, mizRoutePoint);
		//		routePoint.Name = m_sBullsPointName;

		//		MapPoints.Insert(1, routePoint);
		//		NumberMapPoints();
		//	}
		//	else
		//	{
		//		bullsPoint.SetYX(Coalition.MizCoalition.BullseyeY, Coalition.MizCoalition.BullseyeX);
		//	}
		//}

		//private void RemoveBullseyeWaypoint()
		//{
		//	AssetRoutePoint bullsPoint = MapPoints.OfType<AssetRoutePoint>().Where(_mp => _mp.Name == m_sBullsPointName).FirstOrDefault();
		//	if (bullsPoint is object)
		//		MapPoints.Remove(bullsPoint);

		//	NumberMapPoints();
		//}

		//public void InitializeBullseyeWaypoint(bool bWithWaypoint)
		//{
		//	//dataCartridge F18
		//	// bullseye m2000
		//	Log.Info("Updating BULLS waypoints status");
		//	if (bWithWaypoint && Playable)
		//		AddBullseyeWaypoint();
		//	else
		//		RemoveBullseyeWaypoint();
		//}
		#endregion
	}

}
