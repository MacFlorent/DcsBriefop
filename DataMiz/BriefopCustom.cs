using DcsBriefop.Data;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.DataMiz
{
	internal class BriefopCustom : BaseBriefopCustomSerializable
	{
		#region Properties
		public BriefopCustomMap MapData { get; set; }

		public List<BriefopCustomCoalition> Coalitions { get; private set; } = new List<BriefopCustomCoalition>();
		public List<BriefopCustomGroup> AssetGroups { get; set; } = new List<BriefopCustomGroup>();
		public List<BriefopCustomAirdrome> AssetAirdromes { get; set; } = new List<BriefopCustomAirdrome>();
		public List<BriefopCustomMission> Missions { get; set; } = new List<BriefopCustomMission>();

		#endregion

		#region Methods
		public static BriefopCustom DeserializeJson(string sJson)
		{
			return JsonConvert.DeserializeObject<BriefopCustom>(sJson, m_deserializeConverters);
		}

		public string SerializeToJson(Formatting formatting)
		{
			return JsonConvert.SerializeObject(this, formatting, m_serializeConverters);
		}

		public BriefopCustomCoalition GetCoalition(string sCoalitionName)
		{
			return Coalitions.Where(c => c.CoalitionName == sCoalitionName).FirstOrDefault();
		}

		public BriefopCustomGroup GetGroup(int iAssetId, string sCoalitionName)
		{
			return AssetGroups.Where(_g => _g.Id == iAssetId && _g.CoalitionName == sCoalitionName).FirstOrDefault();
		}

		public BriefopCustomAirdrome GetAirdrome(int iAssetId, string sCoalitionName)
		{
			return AssetAirdromes.Where(_a => _a.Id == iAssetId && _a.CoalitionName == sCoalitionName).FirstOrDefault();
		}

		public BriefopCustomMission GetMission(int iAssetId, string sCoalitionName)
		{
			return Missions?.Where(_m => _m.Id == iAssetId && _m.CoalitionName == sCoalitionName).FirstOrDefault();
		}
		#endregion
	}

	internal class BriefopCustomMap
	{
		public string Provider { get; set; }
		public double CenterLatitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Zoom { get; set; }

		public GMapOverlay MapOverlayCustom { get; set; }
		[JsonIgnore]
		public List<GMapOverlay> AdditionalMapOverlays { get; private set; } = new List<GMapOverlay>();
	}

	internal class BriefopCustomCoalition
	{
		public string CoalitionName { get; set; }
		public bool Included { get; set; }
		public string BullseyeDescription { get; set; }
		public bool BullseyeWaypoint { get; set; }
		public BriefopCustomMap MapData { get; set; }
		public ListComPreset ComPresets { get; set; }
	}
}

