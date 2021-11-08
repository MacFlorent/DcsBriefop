using DcsBriefop.Data;
using DcsBriefop.Map;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class CustomData
	{
		public bool? DisplayRed { get; set; }
		public bool? DisplayBlue { get; set; }
		public bool? DisplayNeutral { get; set; }

		public CustomDataMap MapData { get; set; }

		public CustomDataCoalition Red { get; set; } = new CustomDataCoalition();
		public CustomDataCoalition Blue { get; set; } = new CustomDataCoalition();
		public CustomDataCoalition Neutral { get; set; } = new CustomDataCoalition();

		public List<CustomDataAssetGroup> AssetGroups = new List<CustomDataAssetGroup>();
		public List<CustomDataAssetAirdrome> AssetAirdromes = new List<CustomDataAssetAirdrome>();

		public CustomData() { }

		public static CustomData DeserializeJson(string sJson)
		{
			return JsonConvert.DeserializeObject<CustomData>(sJson, new GMapOverlayJsonConverter(), new GMarkerBriefopJsonConverter());
		}

		public string SerializeToJson()
		{
			NormalizeData();
			return JsonConvert.SerializeObject(this, Formatting.Indented, new GMapOverlayJsonConverter(), new GMarkerBriefopJsonConverter());
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
			return AssetGroups?.Where(_g => _g.Id == iAssetId && _g.Coalition == sCoalition).FirstOrDefault();
		}

		public CustomDataAssetAirdrome GetAssetAirdrome(int iAssetId, string sCoalition)
		{
			return AssetAirdromes?.Where(_a => _a.Id == iAssetId && _a.Coalition == sCoalition).FirstOrDefault();
		}


		private void NormalizeData()
		{
			foreach (CustomDataAssetGroup asset in AssetGroups)
			{
				if (asset.Category != (int)ElementAssetCategory.Mission)
				{
					asset.MapDataMission = null;
					asset.AssetMissionPoints = null;
				}
			}

			List<CustomDataAssetAirdrome> toRemove = AssetAirdromes.Where(_a => _a.Category == (int)ElementAssetCategory.Excluded && _a.MapDisplay == (int)ElementAssetMapDisplay.None).ToList();
			foreach (CustomDataAssetAirdrome asset in toRemove)
			{
				AssetAirdromes.Remove(asset); // TODO do not save excluded airfields
			}
		}
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

	internal class CustomDataAssetAirdrome
	{
		public int Id { get; set; }
		public string Coalition { get; set; }
		public int Category { get; set; }
		public int MapDisplay { get; set; }
		public string Information { get; set; }

		public CustomDataAssetAirdrome(int iId, string sCoalition)
		{
			Id = iId;
			Coalition = sCoalition;
		}
	}

	internal class CustomDataAssetGroup
	{
		public int Id { get; set; }
		public string Coalition { get; set; }
		public int Category { get; set; }
		public int MapDisplay { get; set; }
		public string Information { get; set; }
		public string MissionInformation { get; set; }

		public CustomDataMap MapDataMission { get; set; }
		public List<CustomDataAssetMissionPoint> AssetMissionPoints { get; set; }

		public CustomDataAssetGroup(int iId)
		{
			Id = iId;
		}
	}

	internal class CustomDataAssetMissionPoint
	{
		public int Id { get; set; }
		public string Notes { get; set; }
	}
}

