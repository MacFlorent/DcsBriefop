using DcsBriefop.Data;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace DcsBriefop.DataMiz
{
	internal class BriefopCustom : BaseBriefopCustomSerializable
	{
		#region Properties
		public BriefopCustomMap MapData { get; set; }

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

		public List<BriefopCustomCoalition> Coalitions { get; private set; } = new List<BriefopCustomCoalition>();
		public List<BriefopCustomGroup> AssetGroups { get; set; } = new List<BriefopCustomGroup>();
		public List<BriefopCustomUnit> AssetUnits { get; set; } = new List<BriefopCustomUnit>();
		public List<BriefopCustomAirdrome> AssetAirdromes { get; set; } = new List<BriefopCustomAirdrome>();
		public List<BriefopCustomMission> Missions { get; set; } = new List<BriefopCustomMission>();

		#endregion

		#region Methods
		public void InitializeDefault()
		{
			ExportFileTypes = Preferences.PreferencesManager.Preferences.Generation.ExportFileTypes;
		}

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

		public BriefopCustomUnit GetUnit(int iUnitId, string sCoalitionName)
		{
			return AssetUnits.Where(_u => _u.Id == iUnitId && _u.CoalitionName == sCoalitionName).FirstOrDefault();
		}

		public BriefopCustomAirdrome GetAirdrome(int iAssetId, string sCoalitionName)
		{
			return AssetAirdromes.Where(_a => _a.Id == iAssetId && _a.CoalitionName == sCoalitionName).FirstOrDefault();
		}

		public BriefopCustomMission GetMission(int iAssetId, string sCoalitionName)
		{
			return Missions?.Where(_m => _m.Id == iAssetId && _m.CoalitionName == sCoalitionName).FirstOrDefault();
		}

		public GMapProvider GetDefaultMapProvider()
		{
			string sMapProvider = DefaultMapProvider;
			if (string.IsNullOrEmpty(sMapProvider))
				sMapProvider = Preferences.PreferencesManager.Preferences.Map.DefaultProvider;

			return GMapProviders.TryGetProvider(sMapProvider);
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
		
		public GMapProvider GetMapProvider()
		{
			string sMapProvider = Provider;
			if (string.IsNullOrEmpty(sMapProvider))
				sMapProvider = Preferences.PreferencesManager.Preferences.Map.DefaultProvider;

			return GMapProviders.TryGetProvider(sMapProvider);
		}
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

