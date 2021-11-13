using DcsBriefop.Tools;
using LsonLib;
using System;
using System.IO;
using System.IO.Compression;

namespace DcsBriefop.Briefing
{
	internal class MissionManager
	{
		#region Fields
		private readonly string m_dictionnaryLuaFileName = @"l10n\DEFAULT\dictionary";
		private readonly string m_missionLuaFileName = "mission";
		private readonly string m_customLuaFileName = "customBriefOp";
		#endregion

		#region Properties
		private string DictionaryZipEntryFullName { get { return m_dictionnaryLuaFileName.Replace(@"\", "/"); } }
		private string DictionaryFileName { get { return Path.GetFileName(m_dictionnaryLuaFileName); } }
		public string MizFilePath { get; set; }
		public string MizFileDirectory { get { return Path.GetDirectoryName(MizFilePath); } }
		public string MizFileName { get { return Path.GetFileName(MizFilePath); } }

		public bool ExportMizActive { get; set; } = true;
		public bool ExportLocalDirectoryActive { get; set; } = true;
		public bool ExportLocalDirectoryBitmaps { get; set; } = false;
		public string ExportLocalDirectoryPath { get; set; }

		public LsonStructure.RootMission RootMission { get; private set; }
		public LsonStructure.RootDictionary RootDictionary { get; private set; }
		public CustomData RootCustom { get; private set; }
		#endregion

		#region CTOR
		public MissionManager(string sMizFilePath)
		{
			MizFilePath = sMizFilePath;
			MizLoad();
		}
		#endregion

		#region Methods
		public void MizLoad()
		{
			string sMissionFilePath = null, sDictionaryFilePath = null, sCustomFilePath = null;

			if (!File.Exists(MizFilePath))
				throw new ExceptionDcsBriefop($"Miz file not found : {MizFilePath}");

			using (ZipArchive za = ZipFile.OpenRead(MizFilePath))
			{
				foreach (ZipArchiveEntry entry in za.Entries)
				{
					if (!string.IsNullOrEmpty(sMissionFilePath) && !string.IsNullOrEmpty(sDictionaryFilePath) && !string.IsNullOrEmpty(sCustomFilePath))
						break;

					if (entry.FullName.Equals(m_missionLuaFileName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{m_missionLuaFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
						entry.ExtractToFile(sTempPath);
						sMissionFilePath = sTempPath;
					}
					if (entry.FullName.Equals(DictionaryZipEntryFullName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{DictionaryFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
						entry.ExtractToFile(sTempPath);
						sDictionaryFilePath = sTempPath;
					}
					if (entry.FullName.Equals(m_customLuaFileName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{m_customLuaFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
						entry.ExtractToFile(sTempPath);
						sCustomFilePath = sTempPath;
					}
				}
			}

			if (!File.Exists(sMissionFilePath))
			{
				throw new ExceptionDcsBriefop($"Mission lua file not found : {sMissionFilePath}");
			}
			if (!File.Exists(sDictionaryFilePath))
			{
				throw new ExceptionDcsBriefop($"Dictionary lua file not found : {sDictionaryFilePath}");
			}

			RootMission = new LsonStructure.RootMission(LsonVars.Parse(ToolsLua.ReadLuaFileContent(sMissionFilePath)));
			RootDictionary = new LsonStructure.RootDictionary(LsonVars.Parse(ToolsLua.ReadLuaFileContent(sDictionaryFilePath)));

			if (File.Exists(sCustomFilePath))
			{
				RootCustom = CustomData.DeserializeJson(File.ReadAllText(sCustomFilePath));
			}
			else
			{
				RootCustom = new CustomData();
			}

			if (RootCustom.DisplayRed is null)
				RootCustom.DisplayRed = !string.IsNullOrEmpty(RootDictionary.RedTask);
			if (RootCustom.DisplayBlue is null)
				RootCustom.DisplayBlue = !string.IsNullOrEmpty(RootDictionary.BlueTask);
			if (RootCustom.DisplayNeutral is null)
				RootCustom.DisplayNeutral = !string.IsNullOrEmpty(RootDictionary.NeutralTask);
		}

		public void MizSave(string sFilePath)
		{
			if (!File.Exists(MizFilePath))
				throw new ExceptionDcsBriefop($"Original mission miz file not found : {MizFilePath}");

			if (string.IsNullOrEmpty(sFilePath))
				sFilePath = MizFilePath;

			if (MizFilePath.Equals(sFilePath))
			{
				// saving as the loaded file
				string sArchivePath = $"{sFilePath}.{DateTime.Now:yyyyMMdd_HHmmss}";
				Log.Info($"Archiving {sFilePath} as {sArchivePath}");
				File.Copy(sFilePath, sArchivePath);
			}
			else
			{
				// saving as another file, initialize with a copy of the loaded file
				File.Copy(MizFilePath, sFilePath, true);
				MizFilePath = sFilePath;
			}

			RootDictionary.ToLua();
			RootMission.ToLua();

			using (ZipArchive za = ZipFile.Open(sFilePath, ZipArchiveMode.Update))
			{
				ToolsZip.ReplaceZipEntry(za, DictionaryZipEntryFullName, ToolsLua.LsonRootToCorrectedString(RootDictionary.RootLua));
				ToolsZip.ReplaceZipEntry(za, m_missionLuaFileName, ToolsLua.LsonRootToCorrectedString(RootMission.RootLua));
				ToolsZip.ReplaceZipEntry(za, m_customLuaFileName, RootCustom.SerializeToJson(Newtonsoft.Json.Formatting.Indented));
			}

			File.WriteAllText(Path.Combine(MizFileDirectory, "testcustom.json"), RootCustom.SerializeToJson(Newtonsoft.Json.Formatting.Indented));
		}
		#endregion
	}
}
