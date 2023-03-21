using DcsBriefop.Tools;

namespace DcsBriefop.Data
{
	internal enum MasterDataType
	{
		AssetMapDisplay,
		RadioModulation,
		ComPresetMode,
		WeatherDisplay,
		MeasurementSystem,
		CoordinateDisplay,
		Coalition,
		BriefingPartType,
		BriefingPartGroupType
	}

	internal static class MasterDataColumn
	{
		public static readonly string Id = "Id";
		public static readonly string Label = "Label";
	}

	internal class MasterData
	{

		public int Id { get; set; }
		public string Label { get; set; }

		public override string ToString() { return Label; }
	}

	internal static class MasterDataRepository
	{
		#region Fields
		private static Dictionary<MasterDataType, List<MasterData>> m_repository = new Dictionary<MasterDataType, List<MasterData>>();
		#endregion

		#region CTOR
		static MasterDataRepository()
		{
			//m_repository.Add(MasterDataType.AssetMapDisplay, BuildListAssetMapDisplay());
			m_repository.Add(MasterDataType.RadioModulation, BuildListRadioModulation());
			m_repository.Add(MasterDataType.ComPresetMode, BuildListComPresetMode());
			m_repository.Add(MasterDataType.WeatherDisplay, BuildListWeatherDisplay());
			m_repository.Add(MasterDataType.CoordinateDisplay, BuildListCoordinateDisplay());
			m_repository.Add(MasterDataType.MeasurementSystem, BuildListMeasurementSystem());
			m_repository.Add(MasterDataType.Coalition, BuildListCoalition());
			m_repository.Add(MasterDataType.BriefingPartType, BuildListBriefingPartType());
			m_repository.Add(MasterDataType.BriefingPartGroupType, BuildListBriefingPartGroupType());
		}
		#endregion

		#region Build lists
		//private static List<MasterData> BuildListAssetMapDisplay()
		//{
		//	return new List<MasterData>()
		//	{
		//		new MasterData() { Id = (int)ElementAssetMapDisplay.None, Label = "None" },
		//		new MasterData() { Id = (int)ElementAssetMapDisplay.Point, Label = "Point" },
		//		new MasterData() { Id = (int)ElementAssetMapDisplay.Orbit, Label = "Orbit" },
		//		new MasterData() { Id = (int)ElementAssetMapDisplay.FullRoute, Label = "Full route" }
		//	};
		//}

		private static List<MasterData> BuildListRadioModulation()
		{
			return new List<MasterData>()
			{
				new MasterData() { Id = (int)ElementRadioModulation.AM, Label = "AM" },
				new MasterData() { Id = (int)ElementRadioModulation.FM, Label = "FM" }
			};
		}

		private static List<MasterData> BuildListComPresetMode()
		{
			return new List<MasterData>()
			{
				new MasterData() { Id = (int)ElementComPresetMode.Free, Label = "Free" },
				new MasterData() { Id = (int)ElementComPresetMode.Airdrome, Label = "Airdrome" },
				new MasterData() { Id = (int)ElementComPresetMode.Group, Label = "Group" }
			};
		}

		private static List<MasterData> BuildListWeatherDisplay()
		{
			return new List<MasterData>()
			{
				new MasterData() { Id = (int)ElementWeatherDisplay.Plain, Label = "Plain" },
				new MasterData() { Id = (int)ElementWeatherDisplay.Metar, Label = "METAR" },
			};
		}

		private static List<MasterData> BuildListCoordinateDisplay()
		{
			return new List<MasterData>()
			{
				new MasterData() { Id = (int)ElementCoordinateDisplay.Dms, Label = "DMS" },
				new MasterData() { Id = (int)ElementCoordinateDisplay.Ddm, Label = "DDM" },
				new MasterData() { Id = (int)ElementCoordinateDisplay.Mgrs, Label = "MGRS" },
			};
		}

		private static List<MasterData> BuildListMeasurementSystem()
		{
			return new List<MasterData>()
			{
				new MasterData() { Id = (int)ElementMeasurementSystem.Imperial, Label = "Imperial" },
				new MasterData() { Id = (int)ElementMeasurementSystem.Metric, Label = "Metric" },
			};
		}

		private static List<MasterData> BuildListCoalition()
		{
			return new List<MasterData>()
			{
				new MasterData() { Id = 1, Label = ElementCoalition.Red },
				new MasterData() { Id = 2, Label = ElementCoalition.Blue },
				new MasterData() { Id = 3, Label = ElementCoalition.Neutral }
			};
		}

		private static List<MasterData> BuildListBriefingPartType()
		{
			return new List<MasterData>()
			{
				new MasterData() { Id = (int)ElementBriefingPartType.Airbases, Label = "Airbases" },
				new MasterData() { Id = (int)ElementBriefingPartType.Groups, Label = "Groups" },
				new MasterData() { Id = (int)ElementBriefingPartType.Waypoints, Label = "Waypoints" },
				new MasterData() { Id = (int) ElementBriefingPartType.Sortie, Label =  "Sortie" },
				new MasterData() { Id = (int)ElementBriefingPartType.Bullseye, Label = "Bullseye" },
				new MasterData() { Id = (int)ElementBriefingPartType.Weather, Label = "Weather" },
				new MasterData() { Id = (int)ElementBriefingPartType.Description, Label = "Description" },
				new MasterData() { Id = (int)ElementBriefingPartType.Task, Label = "Task" },
				new MasterData() { Id = (int)ElementBriefingPartType.Paragraph, Label = "Paragraph" },
				new MasterData() { Id = (int)ElementBriefingPartType.Image, Label = "Image" }
			};
		}

		private static List<MasterData> BuildListBriefingPartGroupType()
		{
			return new List<MasterData>()
			{
				new MasterData() { Id = (int)ElementBriefingPartGroupType.GroupsAndUnits, Label = "Groups and units" },
				new MasterData() { Id = (int)ElementBriefingPartGroupType.GroupsOnly, Label = "Groups only" },
				new MasterData() { Id = (int)ElementBriefingPartGroupType.UnitsOnly, Label = "Units only" }
			};
		}
		#endregion

		#region Methods
		public static List<MasterData> GetMasterDataList(MasterDataType type)
		{
			if (m_repository.TryGetValue(type, out List<MasterData> list))
				return list;
			else
				return null;
		}

		public static MasterData GetById(MasterDataType type, int iId)
		{
			return GetMasterDataList(type)?.Where(_e => _e.Id == iId).FirstOrDefault();
		}

		public static void FillCombo(MasterDataType type, ComboBox cb, EventHandler selectedValueChanged)
		{
			ToolsControls.FillCombo(cb, GetMasterDataList(type), MasterDataColumn.Id, MasterDataColumn.Label, selectedValueChanged);
		}

		public static void FillCheckedListBox(MasterDataType type, CheckedListBox lst)
		{
			lst.DataSource = GetMasterDataList(type);
		}

		public static void SetFlagCheckedListbox(int iItemsFlag, CheckedListBox lst)
		{
			for (int i = 0; i < lst.Items.Count; i++)
			{
				MasterData masterData = lst.Items[i] as MasterData;
				lst.SetItemChecked(i, (masterData.Id & iItemsFlag) > 0);
			}
		}

		public static int GetFlagCheckedListbox(CheckedListBox lst)
		{
			int iFlag = 0;
			foreach (MasterData masterData in lst.CheckedItems)
			{
				iFlag = (iFlag | masterData.Id);
			}
			return iFlag;
		}

		#endregion
	}
}
