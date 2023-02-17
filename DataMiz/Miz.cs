using DcsBriefop.Tools;
using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal class Miz
	{
		#region Fields
		private static readonly string m_dictionnaryLuaPath = @"l10n\DEFAULT\dictionary";
		private static readonly string m_bopCustomLuaPath = @"kneeboard\mizBopCustom";
		private static readonly string m_missionLuaPath = "mission";
		#endregion

		#region Properties
		public static string MissionFileName { get { return Path.GetFileName(m_missionLuaPath); } }
		public static string MissionZipEntryFullName { get { return ToolsZip.PathToZip(m_missionLuaPath); } }
		public static string DictionaryFileName { get { return Path.GetFileName(m_dictionnaryLuaPath); } }
		public static string DictionaryZipEntryFullName { get { return ToolsZip.PathToZip(m_dictionnaryLuaPath); } }
		public static string BopCustomFileName { get { return Path.GetFileName(m_bopCustomLuaPath); } }
		public static string BopCustomZipEntryFullName { get { return ToolsZip.PathToZip(m_bopCustomLuaPath); } }

		public MizRootMission RootMission { get; private set; }
		public MizRootDictionary RootDictionary { get; private set; }
		public MizBopCustom MizBopCustom { get; private set; }
		#endregion

		#region CTOR
		public Miz(string sLuaMission, string sLuaDictionary, string sJsonBopCustom)
		{
			RootMission = new MizRootMission(LsonVars.Parse(sLuaMission));
			RootDictionary = new MizRootDictionary(LsonVars.Parse(sLuaDictionary));

			if (string.IsNullOrEmpty(sJsonBopCustom))
			{
				MizBopCustom = new MizBopCustom();
				MizBopCustom.InitializeEmpty();
			}
			else
				MizBopCustom = MizBopCustom.DeserializeJson(sJsonBopCustom);			
		}
		#endregion

		#region Methods
		public void FromLua()
		{
			RootMission.FromLua();
			RootDictionary.FromLua();
		}

		public void ToLua()
		{
			RootMission.ToLua();
			RootDictionary.ToLua();
		}
		#endregion
	}
}
