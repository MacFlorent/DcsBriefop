//using DcsBriefop.Data;
//using DcsBriefop.DataMiz;
//using DcsBriefop.Tools;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace DcsBriefop.DataBop
//{
//	internal class BopAssetFlight : BopAssetGroup
//	{
//		#region Fields
//		private MizGroupFlight m_mizGroupFlight;
//		#endregion

//		#region Properties
//		public override ElementDcsObjectClass Class { get { return base.Class == ElementDcsObjectClass.None ? ElementDcsObjectClass.Air : base.Class; } }

//		public string Callsign { get; set; }
//		public Radio Radio { get; set; }
//		#endregion

//		#region CTOR
//		public BopAssetFlight(BopManager parentManager, string sCoalitionName, string sCountryName, MizGroupFlight groupFlight) : base(parentManager, sCoalitionName, sCountryName, groupFlight)
//		{
//			m_mizGroupFlight = m_mizGroup as MizGroupFlight;

//			MizUnitFlight mizUnitFlight = m_mizGroupFlight.Units.OfType<MizUnitFlight>().FirstOrDefault();
//			if (mizUnitFlight is object && mizUnitFlight.Callsign is MizCallsign callsign)
//				Callsign = callsign.Name.Substring(0, callsign.Name.Length - 1);
//			else if (mizUnitFlight is object && mizUnitFlight.CallsignNumber is object)
//				Callsign = mizUnitFlight.CallsignNumber.ToString();

//			Task = m_mizGroupFlight.Task;
//			Type = MainUnit.Type;

//			SetDisplayName();

//			Radio = new Radio() { Frequency = m_mizGroupFlight.RadioFrequency, Modulation = m_mizGroupFlight.RadioModulation };

//			//if (Task == ElementTask.Awacs || Task == ElementTask.Refueling)
//			//	Function = ElementAssetFunction.Support;
//			//if (Task == ElementTask.Refueling)
//			//	Information = GetTacanString();
//		}
//		#endregion

//		#region Methods
//		public override void Persist()
//		{
//			base.Persist();

//			m_mizGroupFlight.RadioFrequency = Radio.Frequency;
//			m_mizGroupFlight.RadioModulation = Radio.Modulation;
//		}

//		public void SetDisplayName()
//		{
//			DisplayName = $"{Callsign} | {m_mizGroup.Name}";
//			if (Playable && ParentManager.BopCustomMain.NoCallsignForPlayableFlights)
//				DisplayName = m_mizGroup.Name;
//		}

//		public string ToStringRadio()
//		{
//			if (Playable)
//				return "initial preset only";
//			else
//				return Radio.ToString();
//		}

//		private string ToStringBaseInformation()
//		{
//			StringBuilder sb = new StringBuilder();
//			foreach (Airdrome airdrome in GetAirdromeIds().Select(_i => ParentManager.Theatre.GetAirdrome(_i)))
//			{
//				if (airdrome is object)
//				{
//					if (sb.Length <= 0)
//					{
//						sb.Append($"Base={airdrome.Name}");
//					}
//					else
//					{
//						sb.AppendWithSeparator($"{airdrome.Name}", ",");
//					}
//				}
//			}
//			//foreach (AssetShip carrier in GetCarrierAssets())
//			//{
//			//	if (sb.Length <= 0)
//			//	{
//			//		sb.Append($"Base={carrier.Name}");
//			//	}
//			//	else
//			//	{
//			//		sb.AppendWithSeparator($"{carrier.Name}", ",");
//			//	}
//			//}

//			return sb.ToString();
//		}

//		public List<int> GetAirdromeIds()
//		{
//			IEnumerable<int> grouped = MapPoints.OfType<BopRoutePoint>()
//				.Where(_rp => _rp.AirdromeId is object
//					&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
//				.GroupBy(_rp => _rp.AirdromeId).Select(_g => _g.Key.Value);

//			return grouped.ToList();
//		}

//		//public List<AssetAirdrome> GetAirdromeAssets()
//		//{
//		//	List<int> flightAirdromes = GetAirdromeIds();
//		//	return Coalition.Airdromes.Where(_a => flightAirdromes.Contains(_a.Id)).ToList();
//		//}

//		//public List<AssetShip> GetCarrierAssets()
//		//{
//		//	IEnumerable<int> grouped = MapPoints.OfType<AssetRoutePoint>()
//		//		.Where(_rp => _rp.HelipadId is object
//		//			&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
//		//		.GroupBy(_rp => _rp.HelipadId).Select(_g => _g.Key.Value);

//		//	return Coalition.OwnAssets.OfType<AssetShip>().Where(_a => grouped.Contains(_a.MainUnit.Id)).ToList();
//		//}

//		//private void AddBullseyeWaypoint()
//		//{
//		//	AssetRoutePoint bullsPoint = MapPoints.OfType<AssetRoutePoint>().Where(_mp => _mp.Name == m_sBullsPointName).FirstOrDefault();

//		//	if (bullsPoint is null)
//		//	{
//		//		MizRoutePoint mizRoutePoint = MizRoutePoint.NewFromLuaTemplate();
//		//		mizRoutePoint.Y = Coalition.MizCoalition.BullseyeY;
//		//		mizRoutePoint.X = Coalition.MizCoalition.BullseyeX;

//		//		AssetRoutePoint routePoint = new AssetRoutePoint(Core, 0, this, mizRoutePoint);
//		//		routePoint.Name = m_sBullsPointName;

//		//		MapPoints.Insert(1, routePoint);
//		//		NumberMapPoints();
//		//	}
//		//	else
//		//	{
//		//		bullsPoint.SetYX(Coalition.MizCoalition.BullseyeY, Coalition.MizCoalition.BullseyeX);
//		//	}
//		//}

//		//private void RemoveBullseyeWaypoint()
//		//{
//		//	AssetRoutePoint bullsPoint = MapPoints.OfType<AssetRoutePoint>().Where(_mp => _mp.Name == m_sBullsPointName).FirstOrDefault();
//		//	if (bullsPoint is object)
//		//		MapPoints.Remove(bullsPoint);

//		//	NumberMapPoints();
//		//}

//		//public void InitializeBullseyeWaypoint(bool bWithWaypoint)
//		//{
//		//	//dataCartridge F18
//		//	// bullseye m2000
//		//	Log.Info("Updating BULLS waypoints status");
//		//	if (bWithWaypoint && Playable)
//		//		AddBullseyeWaypoint();
//		//	else
//		//		RemoveBullseyeWaypoint();
//		//}
//		#endregion
//	}

//}
