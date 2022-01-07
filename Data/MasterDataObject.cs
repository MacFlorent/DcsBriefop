using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.Data
{
	internal enum MasterDataType
	{
		AssetUsage,
		AssetMapDisplay,
		ExportFileType,
		RadioModulation,
		ComPresetMode
	}

	internal static class MasterDataColumn
	{
		public static readonly string Id = "Id";
		public static readonly string Label = "Label";
	}

	internal class MasterDataObject
	{

		public int Id { get; set; }
		public string Label { get; set; }

		public override string ToString() { return Label; }
	}

	internal static class MasterDataRepository
	{ 
		private static Dictionary<MasterDataType, List<MasterDataObject>> m_repository = new Dictionary<MasterDataType, List<MasterDataObject>>();

		static MasterDataRepository()
		{
			m_repository.Add(MasterDataType.AssetUsage, BuildListAssetUsage());
			m_repository.Add(MasterDataType.AssetMapDisplay, BuildListAssetMapDisplay());
			m_repository.Add(MasterDataType.ExportFileType, BuildListExportFileTypes());
			m_repository.Add(MasterDataType.RadioModulation, BuildListRadioModulation());
			m_repository.Add(MasterDataType.ComPresetMode, BuildListComPresetMode());
		}

		private static List<MasterDataObject> BuildListAssetUsage()
		{
			return new List<MasterDataObject>()
			{
				new MasterDataObject() { Id = (int)ElementAssetUsage.Excluded, Label = "Excluded" },
				new MasterDataObject() { Id = (int)ElementAssetUsage.MissionWithDetail, Label = "Mission with detail" },
				new MasterDataObject() { Id = (int)ElementAssetUsage.Mission, Label = "Mission" },
				new MasterDataObject() { Id = (int)ElementAssetUsage.Support, Label = "Support" },
				new MasterDataObject() { Id = (int)ElementAssetUsage.Base, Label = "Base" }
			};
		}

		private static List<MasterDataObject> BuildListAssetMapDisplay()
		{
			return new List<MasterDataObject>()
			{
				new MasterDataObject() { Id = (int)ElementAssetMapDisplay.None, Label = "None" },
				new MasterDataObject() { Id = (int)ElementAssetMapDisplay.Point, Label = "Point" },
				new MasterDataObject() { Id = (int)ElementAssetMapDisplay.Orbit, Label = "Orbit" },
				new MasterDataObject() { Id = (int)ElementAssetMapDisplay.FullRoute, Label = "Full route" }
			};
		}

		private static List<MasterDataObject> BuildListExportFileTypes()
		{
			return new List<MasterDataObject>()
			{
				new MasterDataObject() { Id = (int)ElementExportFileType.Situation, Label = "Situation" },
				new MasterDataObject() { Id = (int)ElementExportFileType.SituationMap, Label = "SituationMap" },
				new MasterDataObject() { Id = (int)ElementExportFileType.Operations, Label = "Operations" },
				new MasterDataObject() { Id = (int)ElementExportFileType.Coms, Label = "Coms" },
				new MasterDataObject() { Id = (int)ElementExportFileType.Missions, Label = "Missions" },
				new MasterDataObject() { Id = (int)ElementExportFileType.MissionMaps, Label = "MissionMaps" }
			};
		}

		private static List<MasterDataObject> BuildListRadioModulation()
		{
			return new List<MasterDataObject>()
			{
				new MasterDataObject() { Id = (int)ElementRadioModulation.AM, Label = "AM" },
				new MasterDataObject() { Id = (int)ElementRadioModulation.FM, Label = "FM" }
			};
		}

		private static List<MasterDataObject> BuildListComPresetMode()
		{
			return new List<MasterDataObject>()
			{
				new MasterDataObject() { Id = (int)ElementComPresetMode.Free, Label = "Free" },
				new MasterDataObject() { Id = (int)ElementComPresetMode.Airdrome, Label = "Airdrome" },
				new MasterDataObject() { Id = (int)ElementComPresetMode.Group, Label = "Group" }
			};
		}

		public static List<MasterDataObject> GetMasterDataList(MasterDataType type)
		{
			if (m_repository.TryGetValue(type, out List<MasterDataObject> list))
				return list;
			else
				return null;
		}

		public static MasterDataObject GetById(MasterDataType type, int iId)
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
	}
}
