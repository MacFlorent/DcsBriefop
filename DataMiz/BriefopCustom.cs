﻿using DcsBriefop.Data;
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

		public bool ExportOnSave { get; set; } = true;
		public bool ExportMiz { get; set; } = true;
		public bool ExportLocalDirectory { get; set; } = true;
		public bool ExportLocalDirectoryHtml { get; set; } = false;
		public string ExportLocalDirectoryPath { get; set; }
		public Size ExportImageSize { get; set; } = new Size(ElementImageSize.Width, ElementImageSize.Height);
		public string ExportImageBackgroundColor { get; set; }
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
			ExportFileTypes.Clear();
			ExportFileTypes.Add(ElementExportFileType.Operations);
			ExportFileTypes.Add(ElementExportFileType.Opposition);
			ExportFileTypes.Add(ElementExportFileType.Missions);
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

