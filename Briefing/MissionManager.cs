using LsonLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace DcsBriefop.Briefing
{
	internal class MissionManager
	{
		private readonly string m_dictionnaryLuaFileName = @"l10n\DEFAULT\dictionary";
		private readonly string m_missionLuaFileName = "mission";
		private string DictionaryZipEntryFullName { get { return m_dictionnaryLuaFileName.Replace(@"\", "/"); } }
		private string DictionaryFileName { get { return Path.GetFileName(m_dictionnaryLuaFileName); } }


		public string MizFilePath { get; set; }
		public string MizFileDirectory
		{
			get { return Path.GetDirectoryName(MizFilePath); }
		}
		public string MizFileName
		{
			get { return Path.GetFileName(MizFilePath); }
		}

		public LsonStructure.RootMission RootMission { get; private set; }
		public LsonStructure.RootDictionary RootDictionary { get; private set; }

		private BriefingPack m_briefingPack;
		public BriefingPack BriefingPack
		{
			get
			{
				if (m_briefingPack is null)
					m_briefingPack = new BriefingPack(this);
				return m_briefingPack;
			}
		}

		public MissionManager(string sMizFilePath)
		{
			MizFilePath = sMizFilePath;
			MizLoad();
		}

		//public void FromRoot()
		//{
		//	Sortie = RootDictionary.Sortie;
		//	Date = new DateTime(RootMission.Date.Year, RootMission.Date.Month, RootMission.Date.Day).AddSeconds(RootMission.StartTime);

		//	BriefingSituations = new Dictionary<string, BriefingPageSituation>();
		//	BriefingSituations.Add(MasterData.CoalitionName.Red, new BriefingPageSituation(this, MasterData.CoalitionName.Red));
		//	BriefingSituations.Add(MasterData.CoalitionName.Blue, new BriefingPageSituation(this, MasterData.CoalitionName.Blue));
		//	BriefingSituations.Add(MasterData.CoalitionName.Neutral, new BriefingPageSituation(this, MasterData.CoalitionName.Neutral));

		//	Weather = new BriefingWeather(this);
		//}

		//public void ToRoot()
		//{
		//	RootDictionary.Sortie = Sortie;
		//	RootMission.Date = new DateTime(Date.Year, Date.Month, Date.Day);
		//	RootMission.StartTime = Convert.ToInt32((Date - RootMission.Date).TotalSeconds);

		//	foreach(BriefingPageSituation bs in BriefingSituations.Values)
		//	{
		//		bs.ToManagerRoot();
		//	}

		//	Weather.ToManagerRoot();
		//}

		private void MizUnpack(string sFilePath)
		{

		}

		public void MizLoad()
		{
			string sMissionFilePath = null, sDictionaryFilePath = null;

			if (!File.Exists(MizFilePath))
				throw new ExceptionDcsBriefop($"Miz file not found : {MizFilePath}");

			using (ZipArchive mizArchive = ZipFile.OpenRead(MizFilePath))
			{
				foreach (ZipArchiveEntry entry in mizArchive.Entries)
				{
					if (!string.IsNullOrEmpty(sMissionFilePath) && !string.IsNullOrEmpty(sDictionaryFilePath))
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

			RootMission = new LsonStructure.RootMission(LsonVars.Parse(File.ReadAllText(sMissionFilePath)));
			RootDictionary = new LsonStructure.RootDictionary(LsonVars.Parse(File.ReadAllText(sDictionaryFilePath)));
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
				string sArchivePath = $"{sFilePath}.{DateTime.Now.ToString("yyyyMMdd_HHmmss")}";
				Log.Info($"Archiving {sFilePath} as {sArchivePath}");
				File.Copy(sFilePath, sArchivePath);
			}
			else
			{
				// saving as another file, initialize with a copy of the loaded file
				File.Copy(MizFilePath, sFilePath, true);
			}

			RootDictionary.ToLua();
			RootMission.ToLua();

			//File.WriteAllText(@"d:\test.lua", LsonVars.ToString(RootDictionary.RootLua));

			using (ZipArchive mizArchive = ZipFile.Open(sFilePath, ZipArchiveMode.Update))
			{
				ZipArchiveEntry zpeDictionary = mizArchive.GetEntry(DictionaryZipEntryFullName);
				zpeDictionary.Delete();
				zpeDictionary = mizArchive.CreateEntry(DictionaryZipEntryFullName);
				using (StreamWriter writer = new StreamWriter(zpeDictionary.Open()))
				{
					writer.Write(LsonVars.ToString(RootDictionary.RootLua));
				}
			}
		}
	}
}
