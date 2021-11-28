using DcsBriefop.Tools;
using LsonLib;
using System;
using System.IO;
using System.IO.Compression;

namespace DcsBriefop.DataMiz
{
	internal class Miz
	{
		#region Fields
		private readonly string m_dictionnaryLuaPath = @"l10n\DEFAULT\dictionary";
		private readonly string m_missionLuaPath = "mission";
		#endregion

		#region Properties
		public string MissionFileName { get { return Path.GetFileName(m_missionLuaPath); } }
		public string MissionZipEntryFullName { get { return ToolsZip.PathToZip(m_missionLuaPath); } }
		public string DictionaryFileName { get { return Path.GetFileName(m_dictionnaryLuaPath); } }
		public string DictionaryZipEntryFullName { get { return ToolsZip.PathToZip(m_dictionnaryLuaPath); } }

		public MizRootMission RootMission { get; private set; }
		public MizRootDictionary RootDictionary { get; private set; }
		#endregion

		#region CTOR
		public Miz(string sLuaMission, string sLuaDictionary)
		{
			RootMission = new MizRootMission(LsonVars.Parse(sLuaMission));
			RootDictionary = new MizRootDictionary(LsonVars.Parse(sLuaDictionary));
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
