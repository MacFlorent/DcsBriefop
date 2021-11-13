using DcsBriefop.Data;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal abstract class CustomDataBase
	{
		#region Fields
		protected static JsonConverter m_converterGMapOverlays = new GMapOverlayJsonConverter();
		protected static JsonConverter m_converterGMarkerBriefop = new GMarkerBriefopJsonConverter();
		protected static JsonConverter m_converterListCustomDataAssetGroup = new ListCustomDataAssetGroupJsonConverter();
		protected static JsonConverter m_converterListCustomDataAssetAirdrome = new ListCustomDataAssetAirdromeJsonConverter();

		protected static JsonConverter[] m_serializeConverters = new JsonConverter[] { m_converterGMapOverlays, m_converterGMarkerBriefop, m_converterListCustomDataAssetGroup, m_converterListCustomDataAssetAirdrome };
		protected static JsonConverter[] m_deserializeConverters = new JsonConverter[] { m_converterGMapOverlays, m_converterGMarkerBriefop };
		#endregion
	}

	internal abstract class CustomDataWithDefault : CustomDataBase
	{
		protected string m_defaultDataJson;

		public void SetDefaultData()
		{
			m_defaultDataJson = JsonConvert.SerializeObject(this, Formatting.None, m_serializeConverters);
		}

		public bool IsDefaultData()
		{
			string sJson = JsonConvert.SerializeObject(this, Formatting.None, m_serializeConverters);
			return sJson == m_defaultDataJson;
		}
	}

	internal class CustomData : CustomDataBase
	{
		#region Properties
		public bool? DisplayRed { get; set; }
		public bool? DisplayBlue { get; set; }
		public bool? DisplayNeutral { get; set; }

		public CustomDataMap MapData { get; set; }

		public CustomDataCoalition Red { get; set; } = new CustomDataCoalition();
		public CustomDataCoalition Blue { get; set; } = new CustomDataCoalition();
		public CustomDataCoalition Neutral { get; set; } = new CustomDataCoalition();

		public List<CustomDataAssetGroup> AssetGroups { get; set; } = new List<CustomDataAssetGroup>();
		public List<CustomDataAssetAirdrome> AssetAirdromes { get; set; } = new List<CustomDataAssetAirdrome>();
		public List<CustomDataMission> Missions { get; set; } = new List<CustomDataMission>();

		#endregion

		#region Methods
		public static CustomData DeserializeJson(string sJson)
		{
			return JsonConvert.DeserializeObject<CustomData>(sJson, m_deserializeConverters);
		}

		public string SerializeToJson(Formatting formatting)
		{
			return JsonConvert.SerializeObject(this, formatting, m_serializeConverters);
		}

		public CustomDataCoalition GetCoalition(string sCoalitionCode)
		{
			if (sCoalitionCode == ElementCoalition.Red)
				return Red;
			else if (sCoalitionCode == ElementCoalition.Blue)
				return Blue;
			else if (sCoalitionCode == ElementCoalition.Neutral)
				return Neutral;
			else
				return null;
		}

		public CustomDataAssetGroup GetAssetGroup(int iAssetId, string sCoalition)
		{
			return AssetGroups.OfType<CustomDataAssetGroup>().Where(_g => _g.Id == iAssetId && _g.Coalition == sCoalition).FirstOrDefault();
		}

		public CustomDataAssetAirdrome GetAssetAirdrome(int iAssetId, string sCoalition)
		{
			return AssetAirdromes.OfType<CustomDataAssetAirdrome>().Where(_a => _a.Id == iAssetId && _a.Coalition == sCoalition).FirstOrDefault();
		}

		public CustomDataMission GetMission(int iAssetId, string sCoalition)
		{
			return Missions?.Where(_m => _m.Id == iAssetId && _m.Coalition == sCoalition).FirstOrDefault();
		}
		#endregion
	}

	internal class CustomDataMap
	{
		public double CenterLatitude { get; set; }
		public double CenterLongitude { get; set; }
		public double Zoom { get; set; }

		public GMapOverlay MapOverlayCustom { get; set; }
		[JsonIgnore]
		public List<GMapOverlay> AdditionalMapOverlays { get; private set; } = new List<GMapOverlay>();
	}

	internal class CustomDataCoalition
	{
		public string BullseyeDescription { get; set; }
		public CustomDataMap MapData { get; set; }
	}
}

