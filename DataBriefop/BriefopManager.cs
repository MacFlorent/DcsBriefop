using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;
using System.IO;
using System.IO.Compression;

namespace DcsBriefop.DataBriefop
{
	internal class BriefopManager
	{
		#region Fields
		#endregion

		#region Properties
		public string MizFilePath { get; set; }
		public string MizFileDirectory { get { return Path.GetDirectoryName(MizFilePath); } }
		public string MizFileName { get { return Path.GetFileName(MizFilePath); } }

		public Miz Miz { get; private set; }
		public Theatre Theatre { get; private set; }
		public BriefopMain BriefopMain { get; private set; }


		#endregion

		#region CTOR
		public BriefopManager(string sMizFilePath)
		{
			MizFilePath = sMizFilePath;
			MizLoad();
		}
		#endregion

		#region Methods
		public void MizLoad()
		{
			//var stopWatch = System.Diagnostics.Stopwatch.StartNew();
			//Log.Debug("Initialize all briefing data start");
			//stopWatch.Stop();
			//Log.Debug($@"Initialize all briefing data end [{stopWatch.Elapsed:hh\:mm\:ss\.ff}]");
			Log.Debug($"Start for miz file : {MizFilePath}");
			string sMissionFilePath = null, sDictionaryFilePath = null, sCustomFilePath = null;

			if (!File.Exists(MizFilePath))
				throw new ExceptionBriefop($"Miz file not found : {MizFilePath}");

			Log.Debug("Opening zip archive");
			using (ZipArchive za = ZipFile.OpenRead(MizFilePath))
			{
				foreach (ZipArchiveEntry entry in za.Entries)
				{
					if (!string.IsNullOrEmpty(sMissionFilePath) && !string.IsNullOrEmpty(sDictionaryFilePath) && !string.IsNullOrEmpty(sCustomFilePath))
						break;

					if (entry.FullName.Equals(DataMiz.Miz.MissionFileName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{DataMiz.Miz.MissionFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
						Log.Debug($"Extracting {entry.FullName} to {sTempPath}");
						entry.ExtractToFile(sTempPath);
						sMissionFilePath = sTempPath;


					}
					if (entry.FullName.Equals(DataMiz.Miz.DictionaryZipEntryFullName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{DataMiz.Miz.DictionaryFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
						Log.Debug($"Extracting {entry.FullName} to {sTempPath}");
						entry.ExtractToFile(sTempPath);
						sDictionaryFilePath = sTempPath;
					}
					if (entry.FullName.Equals(DataMiz.Miz.BriefopCustomZipEntryFullName, StringComparison.OrdinalIgnoreCase))
					{
						string sTempPath = Path.Combine(Path.GetTempPath(), $"{DataMiz.Miz.BriefopCustomFileName}.{DateTime.Now:yyyyMMdd_HHmmss}");
						Log.Debug($"Extracting {entry.FullName} to {sTempPath}");
						entry.ExtractToFile(sTempPath);
						sCustomFilePath = sTempPath;
					}
				}
			}

			if (!File.Exists(sMissionFilePath))
			{
				throw new ExceptionBriefop($"Mission lua file not found : {sMissionFilePath}");
			}
			if (!File.Exists(sDictionaryFilePath))
			{
				throw new ExceptionBriefop($"Dictionary lua file not found : {sDictionaryFilePath}");
			}

			Log.Debug($"Reading lua data");
			string sLuaMission = ToolsLua.ReadLuaFileContent(sMissionFilePath);
			string sLuaDictionnary = ToolsLua.ReadLuaFileContent(sDictionaryFilePath);
			string sJsonBriefopCustom = "";
			if (File.Exists(sCustomFilePath))
			{
				Log.Debug($"Reading json data custom");
				sJsonBriefopCustom = File.ReadAllText(sCustomFilePath);
			}

			Log.Debug($"Building DataMiz objects");
			Miz = new Miz(sLuaMission, sLuaDictionnary, sJsonBriefopCustom);
			Theatre = new Theatre(Miz.RootMission.Theatre);
			BriefopMain = new BriefopMain(this);
		}

		public void MizSave(string sFilePath)
		{
			if (!File.Exists(MizFilePath))
				throw new ExceptionBriefop($"Original mission miz file not found : {MizFilePath}");

			if (string.IsNullOrEmpty(sFilePath))
				sFilePath = MizFilePath;

			if (MizFilePath.Equals(sFilePath))
			{
				// saving as the loaded file
				if (Preferences.PreferencesManager.Preferences.General.BackupBeforeOverwrite)
				{
					string sArchivePath = $"{sFilePath}.{DateTime.Now:yyyyMMdd_HHmmss}";
					Log.Info($"Archiving {sFilePath} as {sArchivePath}");
					File.Copy(sFilePath, sArchivePath);
				}
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
				ToolsZip.ReplaceZipEntry(za, DataMiz.Miz.DictionaryZipEntryFullName, ToolsLua.LsonRootToDcs(Miz.RootDictionary.RootLua));
				ToolsZip.ReplaceZipEntry(za, DataMiz.Miz.MissionFileName, ToolsLua.LsonRootToDcs(Miz.RootMission.RootLua));
				ToolsZip.ReplaceZipEntry(za, DataMiz.Miz.BriefopCustomZipEntryFullName, Miz.BriefopCustomData.SerializeToJson(Newtonsoft.Json.Formatting.Indented));
			}
		}

		public string MizBatchCommandFileName()
		{
			string sMizName = Path.GetFileNameWithoutExtension(MizFileName);
			return Path.Combine(MizFileDirectory, $"{sMizName}.cmd");
		}

		public void MizBatchCommand()
		{
			string sCommandFilePath = MizBatchCommandFileName();
			string sCommandFileContent = ToolsResources.GetTextResourceContent("DcsBriefopBatch", "cmd");
			sCommandFileContent = sCommandFileContent.Replace("%1", MizFileName);
			sCommandFileContent = sCommandFileContent.Replace("%2", System.Reflection.Assembly.GetExecutingAssembly().Location);
			StreamWriter sw = File.CreateText(sCommandFilePath);
			sw.Write(sCommandFileContent);
			sw.Close();
		}

		#endregion
	}
}
