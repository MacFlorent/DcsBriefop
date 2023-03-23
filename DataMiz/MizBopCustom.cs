using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.Tools;
using Newtonsoft.Json;

namespace DcsBriefop.DataMiz
{
	internal class MizBopCustom : BaseMizBopSerializable
	{
		#region Properties
		public PreferencesMap PreferencesMap { get; set; } = new PreferencesMap();
		public MizBopMap MapData { get; set; }
		public List<MizBopCoalition> MizBopCoalitions { get; private set; } = new();
		public List<MizBopAirbase> MizBopAirbases { get; set; } = new();
		public List<MizBopGroup> MizBopGroups { get; set; } = new();
		public List<MizBopUnit> MizBopUnits { get; set; } = new();
		public List<MizBopRoutePoint> MizBopRoutePoints { get; set; } = new();
		public List<BopBriefingFolder> BopBriefingFolders { get; set; } = new();
		#endregion

		#region Methods
		public void InitializeEmpty()
		{
			PreferencesMap = PreferencesManager.Preferences.Map.CloneJson();
		}

		public static MizBopCustom DeserializeJson(string sJson)
		{
			return JsonConvert.DeserializeObject<MizBopCustom>(sJson, m_deserializeConverters);
		}

		public string SerializeToJson(Formatting formatting)
		{
			return JsonConvert.SerializeObject(this, formatting, m_serializeConverters);
		}
		#endregion
	}
}

