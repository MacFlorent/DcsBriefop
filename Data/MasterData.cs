using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.Data
{
	internal enum MasterDataType
	{
		AssetMapDisplay,
		RadioModulation,
		ComPresetMode,
		WeatherDisplay,
		CoordinateDisplay
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
		private static Dictionary<MasterDataType, List<MasterData>> m_repository = new Dictionary<MasterDataType, List<MasterData>>();

		static MasterDataRepository()
		{
			//m_repository.Add(MasterDataType.AssetMapDisplay, BuildListAssetMapDisplay());
			m_repository.Add(MasterDataType.RadioModulation, BuildListRadioModulation());
			m_repository.Add(MasterDataType.ComPresetMode, BuildListComPresetMode());
			m_repository.Add(MasterDataType.WeatherDisplay, BuildListWeatherDisplay());
			m_repository.Add(MasterDataType.CoordinateDisplay, BuildListCoordinateDisplay());
		}

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

		public static void FillCombo(MasterDataType type, ComboBox cb)
		{
			cb.Items.Clear();
			cb.ValueMember = MasterDataColumn.Id;
			cb.DisplayMember = MasterDataColumn.Label;
			cb.DataSource = GetMasterDataList(type);
		}

		public static void FillListBox(MasterDataType type, ListBox lst)
		{
			lst.Items.Clear();
			lst.ValueMember = MasterDataColumn.Id;
			lst.DisplayMember = MasterDataColumn.Label;
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
			foreach(MasterData masterData in lst.CheckedItems)
			{
				iFlag = (iFlag | masterData.Id);
			}
			return iFlag;
		}
	}
}
