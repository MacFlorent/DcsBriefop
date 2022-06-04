using DcsBriefop.Data;
using DcsBriefop.Tools;
using DcsBriefop.UcBriefing;
using System;
using System.IO;
using System.Windows.Forms;

namespace DcsBriefop
{
	public partial class FrmMain : Form
	{
		#region Fields
		private MissionManager m_missionManager;
		private BriefingContainer m_briefingContainer;
		private UcBriefingPack m_ucBriefingPack;
		private UcMap m_ucMap;

		private string m_sDcsFileFilter = "DCS mission files (*.miz)|*.miz|All files (*.*)|*.*";
		#endregion

		#region CTOR
		public FrmMain()
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			this.Icon = ToolsResources.GetIconResource("icon256");
			SetStatusStrip();
			BuildMenu();
		}
		#endregion

		#region Methods
		private void MizOpen()
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.InitialDirectory = Preferences.PreferencesManager.Preferences.General.WorkingDirectory;
				ofd.Filter = m_sDcsFileFilter;
				ofd.RestoreDirectory = true;

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					MizOpen(ofd.FileName);
				}
			}
		}

		private void MizOpen(string sMizFilePath)
		{
			using (new WaitDialog(this))
			{
				m_missionManager = new MissionManager(sMizFilePath);
				m_briefingContainer = new BriefingContainer(m_missionManager.Miz);
				BuildMenu();
				DataToScreen();
			}
		}


		private void MizReload()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			using (new WaitDialog(this))
			{
				m_missionManager.MizLoad();
				m_briefingContainer = new BriefingContainer(m_missionManager.Miz);
				BuildMenu();
				DataToScreen();
			}
		}

		private void MizSave(string sMizFilePath)
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			using (new WaitDialog(this))
			{
				ScreenToData();

				m_briefingContainer.Persist();
				m_missionManager.MizSave(sMizFilePath);
				if (m_missionManager.Miz.BriefopCustomData.ExportOnSave)
				{
					using (BriefingFilesBuilder builder = new BriefingFilesBuilder(m_briefingContainer, m_missionManager))
						builder.Generate();
				}
				if (Preferences.PreferencesManager.Preferences.General.GenerateBatchCommandOnSave)
				{
					m_missionManager.MizBatchCommand();
				}
			}

			MizReload();
		}

		private void MizSaveAs()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				sfd.InitialDirectory = m_missionManager.MizFileDirectory;
				sfd.FileName = m_missionManager.MizFileName;
				sfd.Filter = m_sDcsFileFilter;

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					MizSave(sfd.FileName);
				}
			}
		}

		private void OpenDatabaseUnits()
		{
			FrmDatabaseDcsObject f = new FrmDatabaseDcsObject();
			f.ShowDialog();
		}

		private void OpenPreferences()
		{
			FrmPreferences f = new FrmPreferences();
			f.ShowDialog();
		}

		private void OpenPreferencesMiz()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			ScreenToData();

			FrmPreferencesMiz f = new FrmPreferencesMiz(m_briefingContainer, delegate () { m_ucMap.RefreshMapData(); });
			if (f.ShowDialog() == DialogResult.OK)
			{
				DataToScreen();
			}
		}

		private void OpenPreferencesMizGenerate()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			ScreenToData();

			FrmPreferencesMizGenerate f = new FrmPreferencesMizGenerate(m_briefingContainer, m_missionManager);
			f.ShowDialog();
		}

		private void GenerateFiles()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			using (new WaitDialog(this))
			{
				ScreenToData();

				using (BriefingFilesBuilder builder = new BriefingFilesBuilder(m_briefingContainer, m_missionManager))
					builder.Generate();
			}
		}

		private void GenerateBatchCommand()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			string sCommandFilePath = m_missionManager.MizBatchCommandFileName();
			if (ToolsControls.ShowMessageBoxQuestion($"The following command file will be generated in the mission directory :{Environment.NewLine}  {sCommandFilePath}{Environment.NewLine}{Environment.NewLine}You can execute this command file to generate the kneeboard contents for the mission. This file will also be generated each time the mission is saved in DcsBriefop if the relevant preference is active."))
			{
				using (new WaitDialog(this))
				{
					m_missionManager.MizBatchCommand();
				}
			}
		}

		private void DataToScreen()
		{
			if (m_ucMap is null)
			{
				m_ucMap = new UcMap();
				m_ucMap.Dock = DockStyle.Fill;
				SplitContainer.Panel2.Controls.Clear();
				SplitContainer.Panel2.Controls.Add(m_ucMap);
			}

			if (m_ucBriefingPack is null)
			{
				m_ucBriefingPack = new UcBriefingPack(m_ucMap, m_briefingContainer);
				m_ucBriefingPack.Dock = DockStyle.Fill;
				SplitContainer.Panel1.Controls.Clear();
				SplitContainer.Panel1.Controls.Add(m_ucBriefingPack);
			}
			else
				m_ucBriefingPack.BriefingContainer = m_briefingContainer;

			SetStatusStrip();

			m_ucBriefingPack.DataToScreen();

			ToolsStyle.ApplyStyle(this);
		}

		private void ScreenToData()
		{
			m_ucBriefingPack.ScreenToData();
		}

		private void SetStatusStrip()
		{
			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
			System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

			StatusStrip.Items.Clear();
			StatusStrip.Items.Add(fvi.FileVersion);
			if (m_missionManager is object)
				StatusStrip.Items.Add(m_missionManager.MizFilePath);
		}

		private void Test()
		{
			ToolsTests.LsonText(SplitContainer);
		}
		#endregion

		#region Menus
		private void BuildMenu()
		{
			MainMenu.Items.Clear();

			ToolStripMenuItem tsmiMiz = MainMenu.Items.AddMenuItem("Miz", null);
			MainMenu.Items.Add(tsmiMiz);
			tsmiMiz.DropDownItems.AddMenuItem("Open", (object _sender, EventArgs _e) => { MizOpen(); });
			tsmiMiz.DropDownItems.AddMenuItem("Reload", (object _sender, EventArgs _e) => { MizReload(); });
			tsmiMiz.DropDownItems.AddMenuItem("Save", (object _sender, EventArgs _e) => { MizSave(null); });
			tsmiMiz.DropDownItems.AddMenuItem("Save as", (object _sender, EventArgs _e) => { MizSaveAs(); });
			tsmiMiz.DropDownItems.AddMenuSeparator();
			tsmiMiz.DropDownItems.AddMenuItem("Test (do not use)", (object _sender, EventArgs _e) => { Test(); });

			if (Preferences.PreferencesManager.Preferences.General.RecentMiz.Count > 0)
			{
				tsmiMiz.DropDownItems.AddMenuSeparator();
				foreach (string sRecentMizFilePath in Preferences.PreferencesManager.Preferences.General.RecentMiz)
					tsmiMiz.DropDownItems.AddMenuItem(sRecentMizFilePath, (object _sender, EventArgs _e) => { MizOpen(sRecentMizFilePath); });
			}
			tsmiMiz.DropDownItems.AddMenuSeparator();
			tsmiMiz.DropDownItems.AddMenuItem("Exit", (object _sender, EventArgs _e) => { Application.Exit(); });

			ToolStripMenuItem tsmiBriefing = MainMenu.Items.AddMenuItem("Briefing", null);
			MainMenu.Items.Add(tsmiBriefing);
			ToolStripMenuItem tsmiBriefingCoalitions = tsmiBriefing.DropDownItems.AddMenuItem("Coalitions", null);
			tsmiBriefing.DropDownItems.Add(tsmiBriefingCoalitions);
			AddMenuCoalition(tsmiBriefingCoalitions, ElementCoalition.Red);
			AddMenuCoalition(tsmiBriefingCoalitions, ElementCoalition.Blue);
			//AddMenuCoalition(tsmiBriefingCoalitions, ElementCoalition.Neutral);
			tsmiBriefing.DropDownItems.AddMenuItem("Generate kneeboard", (object _sender, EventArgs _e) => { GenerateFiles(); });
			tsmiBriefing.DropDownItems.AddMenuItem("Generate batch command", (object _sender, EventArgs _e) => { GenerateBatchCommand(); });
			tsmiBriefing.DropDownItems.AddMenuSeparator();
			tsmiBriefing.DropDownItems.AddMenuItem("Kneeboard generation preferences", (object _sender, EventArgs _e) => { OpenPreferencesMizGenerate(); });
			tsmiBriefing.DropDownItems.AddMenuItem("Mission preferences", (object _sender, EventArgs _e) => { OpenPreferencesMiz(); });

			ToolStripMenuItem tsmiTools = MainMenu.Items.AddMenuItem("Tools", null);
			MainMenu.Items.Add(tsmiTools);
			tsmiTools.DropDownItems.AddMenuItem("Preferences", (object _sender, EventArgs _e) => { OpenPreferences(); });
			tsmiTools.DropDownItems.AddMenuSeparator();
			tsmiTools.DropDownItems.AddMenuItem("Unit database", (object _sender, EventArgs _e) => { OpenDatabaseUnits(); });

		}

		private void AddMenuCoalition(ToolStripMenuItem tsmiBriefingCoalitions, string sCoalitionName)
		{
			if (m_briefingContainer is null)
				return;

			ToolStripMenuItem tsmiCoalition = tsmiBriefingCoalitions.DropDownItems.AddMenuItem(sCoalitionName, (object _sender, EventArgs _e) => { CoalitionMenuClick(_sender as ToolStripMenuItem, sCoalitionName); });
			tsmiCoalition.Checked = (m_briefingContainer.GetCoalition(sCoalitionName) is BriefingCoalition coalition && coalition.Included);
		}

		private void CoalitionMenuClick(ToolStripMenuItem tsmiBriefingCoalition, string sCoalitionName)
		{
			using (new WaitDialog(this))
			{
				BriefingCoalition coalition = m_briefingContainer.GetCoalition(sCoalitionName);
				if (coalition is null)
				{
					m_briefingContainer.AddCoalition(sCoalitionName);
					coalition = m_briefingContainer.GetCoalition(sCoalitionName);
					coalition.Included = false;
				}

				coalition.Included = !coalition.Included;
				tsmiBriefingCoalition.Checked = coalition.Included;

				ScreenToData();
				DataToScreen();
			}
		}

		#endregion

		#region Events
		#endregion
	}
}
