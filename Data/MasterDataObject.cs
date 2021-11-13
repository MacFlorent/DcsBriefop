using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.Data
{
	internal enum MasterDataType
	{
		AssetUsage,
		AssetMapDisplay
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
			cb.ValueMember = "Id";
			cb.DisplayMember = "Label";
			cb.DataSource = GetMasterDataList(type);
		}
	}
}
