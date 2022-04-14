using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace DcsBriefop
{
	internal class MissionManager
	{
		#region Fields
		#endregion

		#region Properties
		public string MizFilePath { get; set; }
		public string MizFileDirectory { get { return Path.GetDirectoryName(MizFilePath); } }
		public string MizFileName { get { return Path.GetFileName(MizFilePath); } }

		public bool ExportMizActive { get; set; } = true;
		public bool ExportLocalDirectoryActive { get; set; } = true;
		public bool ExportLocalDirectoryBitmaps { get; set; } = false;
		public string ExportLocalDirectoryPath { get; set; }
		public List<ElementExportFileType> ExportFileTypes { get; set; } = new List<ElementExportFileType>() { ElementExportFileType.Operations, ElementExportFileType.Opposition, ElementExportFileType.Coms };

		public DataMiz.Miz Miz { get; private set; }
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

					if (entry.FullName.Equals(DataMiz.Miz.MissionFileName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{DataMiz.Miz.MissionFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
						entry.ExtractToFile(sTempPath);
						sMissionFilePath = sTempPath;
					}
					if (entry.FullName.Equals(DataMiz.Miz.DictionaryZipEntryFullName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{DataMiz.Miz.DictionaryFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
						entry.ExtractToFile(sTempPath);
						sDictionaryFilePath = sTempPath;
					}
					if (entry.FullName.Equals(DataMiz.Miz.BriefopCustomZipEntryFullName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{DataMiz.Miz.BriefopCustomFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
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

			string sLuaMission = ToolsLua.ReadLuaFileContent(sMissionFilePath);
			string sLuaDictionnary = ToolsLua.ReadLuaFileContent(sDictionaryFilePath);
			string sJsonBriefopCustom = "";
			if (File.Exists(sCustomFilePath))
				sJsonBriefopCustom = File.ReadAllText(sCustomFilePath);

			Miz = new DataMiz.Miz(sLuaMission, sLuaDictionnary, sJsonBriefopCustom);
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

			Miz.ToLua();

			using (ZipArchive za = ZipFile.Open(sFilePath, ZipArchiveMode.Update))
			{
				ToolsZip.ReplaceZipEntry(za, DataMiz.Miz.DictionaryZipEntryFullName, ToolsLua.LsonRootToCorrectedString(Miz.RootDictionary.RootLua));
				ToolsZip.ReplaceZipEntry(za, DataMiz.Miz.MissionFileName, ToolsLua.LsonRootToCorrectedString(Miz.RootMission.RootLua));
				ToolsZip.ReplaceZipEntry(za, DataMiz.Miz.BriefopCustomZipEntryFullName, Miz.BriefopCustomData.SerializeToJson(Newtonsoft.Json.Formatting.Indented));
			}
		}
		#endregion
	}
}
