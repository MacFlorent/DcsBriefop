using DcsBriefop.Data;
using DcsBriefop.Tools;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DcsBriefop.DataMiz
{
	internal class MizBopCustom : BaseMizBopSerializable
	{
		#region Properties
		public PreferencesMission PreferencesMission { get; set; } = new PreferencesMission();
		public PreferencesMap PreferencesMap { get; set; } = new PreferencesMap();
		//public Preferences.PreferencesBriefing PreferencesBriefing { get; set; }
		public MizBopMap MapData { get; set; } = new MizBopMap();	
		public List<MizBopCoalition> MizBopCoalitions { get; private set; } = new List<MizBopCoalition>();
		public List<MizBopAirbase> MizBopAirbases { get; set; } = new List<MizBopAirbase>();
		public List<MizBopGroup> MizBopGroups { get; set; } = new List<MizBopGroup>();
		public List<MizBopUnit> MizBopUnits { get; set; } = new List<MizBopUnit>();
		#endregion

		#region Methods
		public void InitializeDefault()
		{
			PreferencesMission = PreferencesManager.Preferences.Mission.CloneJson();
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

