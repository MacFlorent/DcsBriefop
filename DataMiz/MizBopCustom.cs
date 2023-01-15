using DcsBriefop.Data;
using DcsBriefop.Tools;
using GMap.NET.MapProviders;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

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
		//public List<BopCustomAirdrome> BopCustomAirdromes { get; set; } = new List<BopCustomAirdrome>();

		//public List<BriefopCustomGroup> AssetGroups { get; set; } = new List<BriefopCustomGroup>();
		//public List<BriefopCustomUnit> AssetUnits { get; set; } = new List<BriefopCustomUnit>();
		//public List<BriefopCustomAssetFlightMission> AssetFlightMissions { get; set; } = new List<BriefopCustomAssetFlightMission>();

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

		//public BopCustomCoalition GetCoalition(string sCoalitionName)
		//{
		//	return BopCoalitions.Where(_c => _c.CoalitionName == sCoalitionName).FirstOrDefault();
		//}

		//public BopCustomAirdrome GetAirdrome(int iAirdromeId)
		//{
		//	return BopCustomAirdromes.Where(_a => _a.Id == iAirdromeId).FirstOrDefault();
		//}

		//public BriefopCustomGroup GetGroup(int iAssetId, string sCoalitionName)
		//{
		//	return AssetGroups.Where(_g => _g.Id == iAssetId && _g.CoalitionName == sCoalitionName).FirstOrDefault();
		//}

		//public BriefopCustomUnit GetUnit(int iUnitId, string sCoalitionName)
		//{
		//	return AssetUnits.Where(_u => _u.Id == iUnitId && _u.CoalitionName == sCoalitionName).FirstOrDefault();
		//}



		//public BriefopCustomAssetFlightMission GetMission(int iAssetId, string sCoalitionName)
		//{
		//	return AssetFlightMissions?.Where(_m => _m.Id == iAssetId && _m.CoalitionName == sCoalitionName).FirstOrDefault();
		//}

		//public GMapProvider GetDefaultMapProvider()
		//{
		//	string sMapProvider = DefaultMapProvider;
		//	if (string.IsNullOrEmpty(sMapProvider))
		//		sMapProvider = Preferences.PreferencesManager.Preferences.Map.DefaultProvider;

		//	return GMapProviders.TryGetProvider(sMapProvider);
		//}
		#endregion
	}
}

