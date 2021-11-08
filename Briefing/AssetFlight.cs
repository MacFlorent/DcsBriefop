using DcsBriefop.LsonStructure;
using DcsBriefop.Data;
using DcsBriefop.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DcsBriefop.Briefing
{
	internal class AssetFlight : AssetGroup
	{
		#region Fields
		private GroupFlight GroupFlight { get { return m_group as GroupFlight; } }
		#endregion

		#region Properties
		protected override string DefaultMarker { get; set; } = MarkerBriefopType.aircraft.ToString();
		public override string Task { get { return GroupFlight.Task; } }
		public override string Type { get { return GroupFlight.Units.FirstOrDefault()?.Type; } }
		public override string RadioString { get { return Radio.ToString(); } }


		private Radio m_radio;
		public Radio Radio
		{
			get
			{
				if (m_radio is null)
					m_radio = new Radio() { Frequency = GroupFlight.RadioFrequency, Modulation = GroupFlight.RadioModulation };
				return m_radio;
			}
			set
			{
				GroupFlight.RadioFrequency = value.Frequency;
				GroupFlight.RadioModulation = value.Modulation;
			}
		}
		#endregion

		#region CTOR
		public AssetFlight(BriefingPack briefingPack, BriefingCoalition briefingCoalition, GroupFlight group) : base(briefingPack, briefingCoalition, group) { }
		#endregion

		#region Methods
		protected override string GetDefaultInformation()
		{
			StringBuilder sbInformation = new StringBuilder();

			if (Task == ElementTask.Refueling)
			{
				sbInformation.AppendWithSeparator($"TCN={GetTacanString()}", " ");
			}

			StringBuilder sbBases = new StringBuilder();
			foreach (Airdrome airdrome in GetAirdromeIds().Select(_i => Theatre.GetAirdrome(_i)))
			{
				if (airdrome is object)
				{
					if (sbBases.Length <= 0)
					{
						sbBases.Append($"Base={airdrome.Name}");
					}
					else
					{
						sbBases.AppendWithSeparator($"{airdrome.Name}", ",");
					}
				}
			}
			foreach (AssetShip carrier in GetCarrierAssets())
			{
				if (sbBases.Length <= 0)
				{
					sbBases.Append($"Base={carrier.Name}");
				}
				else
				{
					sbBases.AppendWithSeparator($"{carrier.Name}", ",");
				}

			}

			sbInformation.AppendWithSeparator(sbBases.ToString(), " ");
			return sbInformation.ToString();
		}

		protected override void InitializeCustomData()
		{
			CustomData = RootCustom.GetAssetGroup(Id, BriefingCoalition.Name);
			if (CustomData is object)
				return;

			CustomData = new CustomDataAssetGroup(Id);
			RootCustom.AssetGroups.Add(CustomData);

			if (Playable)
			{
				Category = ElementAssetCategory.Mission;
				MapDisplay = ElementAssetMapDisplay.None;
			}
			else if (Task == ElementTask.Awacs || Task == ElementTask.Refueling)
			{
				Category = ElementAssetCategory.Support;
				MapDisplay = ElementAssetMapDisplay.Orbit;
			}
			else
			{
				Category = ElementAssetCategory.Mission;
				MapDisplay = ElementAssetMapDisplay.None;
			}
		}

		public string GetCallsign()
		{
			string sCallsign = m_group.Units.OfType<UnitFlight>().FirstOrDefault()?.Callsign;
			if (!string.IsNullOrEmpty(sCallsign))
				return sCallsign.Substring(0, sCallsign.Length - 1);
			else
				return null;
		}

		public List<int> GetAirdromeIds()
		{
			IEnumerable<int> grouped = m_group.RoutePoints
				.Where(_rp => _rp.AirdromeId is object
					&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
				.GroupBy(_rp => _rp.AirdromeId).Select(_g => _g.Key.Value);

			return grouped.ToList();
		}

		public List<AssetShip> GetCarrierAssets()
		{
			IEnumerable<int> grouped = m_group.RoutePoints
				.Where(_rp => _rp.HelipadId is object
					&& (_rp.Type == ElementRoutePointType.TakeOff || _rp.Type == ElementRoutePointType.TakeOffParking || _rp.Type == ElementRoutePointType.TakeOffParkingHot || _rp.Type == ElementRoutePointType.Land))
				.GroupBy(_rp => _rp.HelipadId).Select(_g => _g.Key.Value);


			return BriefingCoalition.Assets.OfType<AssetShip>().Where(_a => grouped.Contains(_a.MainUnit.Id)).ToList();
		}
		#endregion
	}
}
