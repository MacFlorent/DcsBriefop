﻿using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.DataBopMission
{
	internal class BopGroupFlight : BopGroup
	{
		#region Fields
		#endregion

		#region Properties
		public string Callsign { get; set; }
		public string Task { get; set; }
		public Tacan Tacan { get; set; }
		#endregion

		#region CTOR
		public BopGroupFlight(Miz miz, Theatre theatre, string sCoalitionName, string sCountryName, MizGroup mizGroup) : base(miz, theatre, sCoalitionName, sCountryName, ElementDcsGroupType.Flight, ElementDcsObjectClass.Air, mizGroup)
		{
			MizUnit firstMizUnit = m_mizGroup.Units.FirstOrDefault();

			if (firstMizUnit is object && firstMizUnit.Callsign is MizCallsign callsign)
			{
				string sCallsignName = new string(callsign.Name.TakeWhile(_c => !char.IsDigit(_c)).ToArray());
				Callsign = $"{sCallsignName}-{callsign.Flight}";
			}
			else if (firstMizUnit is object && firstMizUnit.CallsignNumber is object)
				Callsign = firstMizUnit.CallsignNumber.ToString();

			Task = m_mizGroup.Task;

			if (m_mizGroup.RadioFrequency is object && m_mizGroup.RadioModulation is object)
				Radio = new Radio(m_mizGroup.RadioFrequency.Value, m_mizGroup.RadioModulation.Value);

			Tacan = (MainUnit as BopUnitFlight)?.Tacan;
			if (Tacan is null)
				Tacan = Units.OfType<BopUnitFlight>().Where(_u => _u.Tacan is object).Select(_u => _u.Tacan).FirstOrDefault();
			if (Tacan is null)
				Tacan = GetTacanFromRouteTaskAction(null);
		}
		#endregion

		#region Miz
		protected override void FromMizUnits()
		{
			Units = new List<BopUnit>();
			foreach (MizUnit mizUnit in m_mizGroup.Units)
			{
				BopUnitFlight bopUnitAir = new BopUnitFlight(Miz, Theatre, this, mizUnit);
				Units.Add(bopUnitAir);
				if (MainUnit is null)
					MainUnit = bopUnitAir;
				else if (!MainUnit.MainInGroup && bopUnitAir.MainInGroup)
					MainUnit = bopUnitAir;
			}
		}

		public override void ToMiz()
		{
			base.ToMiz();

			m_mizGroup.Task = Task;
			m_mizGroup.RadioFrequency = Radio?.Frequency;
			m_mizGroup.RadioModulation = Radio?.Modulation;
		}
		#endregion

		#region Methods
		public override string ToStringDisplayName()
		{
			string sDisplayName = base.ToStringDisplayName();

			if (!string.IsNullOrEmpty(Callsign) && (!Playable || !Miz.MizBopCustom.PreferencesMission.NoCallsignForPlayableFlights))
				return $"{Callsign} | {sDisplayName}";
			else
				return sDisplayName;
		}

		public override string ToStringAdditionnal()
		{
			string sDescription = base.ToStringDisplayName();
			if (!string.IsNullOrEmpty(Task))
				sDescription = $"{sDescription} Task:{Task}";
			if (Tacan is object)
				sDescription = $"{sDescription} TACAN:{Tacan}";

			return sDescription;
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