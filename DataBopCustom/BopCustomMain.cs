using DcsBriefop.Data;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.DataBopCustom
{
	internal class BopCustomMain : BaseBopCustomSerializable
	{
		#region Properties
		public string DefaultMapProvider { get; set; } = Preferences.PreferencesManager.Preferences.Map.DefaultProvider;
		public bool NoCallsignForPlayableFlights { get; set; } = Preferences.PreferencesManager.Preferences.General.NoCallsignForPlayableFlights;
		public bool ExportOnSave { get; set; } = Preferences.PreferencesManager.Preferences.Generation.ExportOnSave;
		public bool ExportMiz { get; set; } = Preferences.PreferencesManager.Preferences.Generation.ExportMiz;
		public bool ExportLocalDirectory { get; set; } = Preferences.PreferencesManager.Preferences.Generation.ExportLocalDirectory;
		public bool ExportLocalDirectoryHtml { get; set; } = Preferences.PreferencesManager.Preferences.Generation.ExportLocalDirectoryHtml;
		public string ExportLocalDirectoryPath { get; set; }
		public Size ExportImageSize { get; set; } = Preferences.PreferencesManager.Preferences.Generation.ExportImageSize;
		public string ExportImageBackgroundColor { get; set; } = Preferences.PreferencesManager.Preferences.Generation.ExportImageBackgroundColor;
		public List<ElementExportFileType> ExportFileTypes { get; set; } = new List<ElementExportFileType>();

		public BopCustomGeneral BopCustomGeneral { get; set; }

		public List<BopCustomCoalition> Coalitions { get; private set; } = new List<BopCustomCoalition>();
		//public List<BriefopCustomGroup> AssetGroups { get; set; } = new List<BriefopCustomGroup>();
		//public List<BriefopCustomUnit> AssetUnits { get; set; } = new List<BriefopCustomUnit>();
		//public List<BriefopCustomAirdrome> AssetAirdromes { get; set; } = new List<BriefopCustomAirdrome>();
		//public List<BriefopCustomAssetFlightMission> AssetFlightMissions { get; set; } = new List<BriefopCustomAssetFlightMission>();

		#endregion

		#region Methods
		public void InitializeDefault()
		{
			ExportFileTypes = Preferences.PreferencesManager.Preferences.Generation.ExportFileTypes;
		}

		public static BopCustomMain DeserializeJson(string sJson)
		{
			return JsonConvert.DeserializeObject<BopCustomMain>(sJson, m_deserializeConverters);
		}

		public string SerializeToJson(Formatting formatting)
		{
			return JsonConvert.SerializeObject(this, formatting, m_serializeConverters);
		}

		public BopCustomCoalition GetCoalition(string sCoalitionName)
		{
			return Coalitions.Where(c => c.CoalitionName == sCoalitionName).FirstOrDefault();
		}

		//public BriefopCustomGroup GetGroup(int iAssetId, string sCoalitionName)
		//{
		//	return AssetGroups.Where(_g => _g.Id == iAssetId && _g.CoalitionName == sCoalitionName).FirstOrDefault();
		//}

		//public BriefopCustomUnit GetUnit(int iUnitId, string sCoalitionName)
		//{
		//	return AssetUnits.Where(_u => _u.Id == iUnitId && _u.CoalitionName == sCoalitionName).FirstOrDefault();
		//}

		//public BriefopCustomAirdrome GetAirdrome(int iAssetId, string sCoalitionName)
		//{
		//	return AssetAirdromes.Where(_a => _a.Id == iAssetId && _a.CoalitionName == sCoalitionName).FirstOrDefault();
		//}

		//public BriefopCustomAssetFlightMission GetMission(int iAssetId, string sCoalitionName)
		//{
		//	return AssetFlightMissions?.Where(_m => _m.Id == iAssetId && _m.CoalitionName == sCoalitionName).FirstOrDefault();
		//}

		public GMapProvider GetDefaultMapProvider()
		{
			string sMapProvider = DefaultMapProvider;
			if (string.IsNullOrEmpty(sMapProvider))
				sMapProvider = Preferences.PreferencesManager.Preferences.Map.DefaultProvider;

			return GMapProviders.TryGetProvider(sMapProvider);
		}
		#endregion
	}

	internal class BopCustomGeneral
	{
		public BopCustomMap MapData { get; set; }
	}

	internal class BopCustomCoalition
	{
		public string CoalitionName { get; set; }
		public string BullseyeDescription { get; set; }
		public bool BullseyeWaypoint { get; set; }
		public BopCustomMap MapData { get; set; }
		//public ListComPreset ComPresets { get; set; }
	}
}

