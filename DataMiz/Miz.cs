using DcsBriefop.Tools;
using LsonLib;
using System.IO;

namespace DcsBriefop.DataMiz
{
	internal class Miz
	{
		#region Fields
		private static readonly string m_dictionnaryLuaPath = @"l10n\DEFAULT\dictionary";
		private static readonly string m_briefopCustomLuaPath = @"kneeboard\briefopCustom";
		private static readonly string m_missionLuaPath = "mission";
		#endregion

		#region Properties
		public static string MissionFileName { get { return Path.GetFileName(m_missionLuaPath); } }
		public static string MissionZipEntryFullName { get { return ToolsZip.PathToZip(m_missionLuaPath); } }
		public static string DictionaryFileName { get { return Path.GetFileName(m_dictionnaryLuaPath); } }
		public static string DictionaryZipEntryFullName { get { return ToolsZip.PathToZip(m_dictionnaryLuaPath); } }
		public static string BriefopCustomFileName { get { return Path.GetFileName(m_briefopCustomLuaPath); } }
		public static string BriefopCustomZipEntryFullName { get { return ToolsZip.PathToZip(m_briefopCustomLuaPath); } }

		public MizRootMission RootMission { get; private set; }
		public MizRootDictionary RootDictionary { get; private set; }
		public BriefopCustom BriefopCustomData { get; private set; }
		#endregion

		#region CTOR
		public Miz(string sLuaMission, string sLuaDictionary, string sJsonBriefopCustom)
		{
			RootMission = new MizRootMission(LsonVars.Parse(sLuaMission));
			RootDictionary = new MizRootDictionary(LsonVars.Parse(sLuaDictionary));

			if (!string.IsNullOrEmpty(sJsonBriefopCustom))
				BriefopCustomData = BriefopCustom.DeserializeJson(sJsonBriefopCustom);
			else
				BriefopCustomData = new BriefopCustom();
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
