﻿using DcsBriefop.Data;
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

			this.Icon = ToolsResources.GetIconResource("icon16");
			BuildMenu();
		}
		#endregion

		#region Methods
		private void MizOpen()
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.InitialDirectory = ToolsSettings.GetWorkingDirectory();
				ofd.Filter = m_sDcsFileFilter;
				ofd.RestoreDirectory = true;

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					ToolsSettings.SetWorkingDirectory(Path.GetDirectoryName(ofd.FileName));

					using (new WaitDialog(this))
					{
						m_missionManager = new MissionManager(ofd.FileName);
						m_briefingContainer = new BriefingContainer(m_missionManager.Miz);
						BuildMenu();
						DataToScreen();
					}
				}
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

		private void OpenGenerateFilesForm()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			ScreenToData();

			FrmGenerateFiles f = new FrmGenerateFiles(m_briefingContainer, m_missionManager);
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

			StatusStrip.Items.Clear();
			StatusStrip.Items.Add(m_missionManager.MizFilePath);

			m_ucBriefingPack.DataToScreen();

			ToolsStyle.ApplyStyle(this);
		}

		private void ScreenToData()
		{
			m_ucBriefingPack.ScreenToData();
		}

		private void Test()
		{
			//string sJsonStream = ToolsResources.GetJsonResourceContent("Routes");
			//Map.ConfigMapTemplateRoutes test = Newtonsoft.Json.JsonConvert.DeserializeObject<Map.ConfigMapTemplateRoutes>(sJsonStream);

			//SplitContainer.Panel1.Controls.Clear();
			//FlowLayoutPanel f = new FlowLayoutPanel();
			//f.FlowDirection = FlowDirection.LeftToRight;
			//f.Dock = DockStyle.Fill;
			//SplitContainer.Panel1.Controls.Add(f);

			//TextBox tb1 = new TextBox();
			//tb1.Multiline = true;
			//tb1.Height = 500;
			//tb1.Width = f.Width;
			//TextBox tb2 = new TextBox();
			//tb2.Multiline = true;
			//tb2.Height = 500;
			//tb2.Width = f.Width;
			//f.Controls.Add(tb1);
			//f.Controls.Add(tb2);

			//string sFilePath = @"D:\Projects\dictionary";
			//string s = MissionManager.ReadLuaFileContent(sFilePath);
			//var v = LsonLib.LsonVars.Parse(s);

			//LsonLib.LsonDict root = v["dictionary"].GetDict();
			//s = root["DictKey_descriptionText_1"].GetString();

			//tb1.Text = s;

			//s = MissionManager.LsonRootToCorrectedString(v);

			//tb2.Text = s;
			//System.IO.File.WriteAllText(sFilePath + "_mod", s);

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
			tsmiMiz.DropDownItems.AddMenuItem("Test", (object _sender, EventArgs _e) => { Test(); });
			tsmiMiz.DropDownItems.AddMenuSeparator();
			tsmiMiz.DropDownItems.AddMenuItem("Exit", (object _sender, EventArgs _e) => { Application.Exit(); });
			
			ToolStripMenuItem tsmiBriefing = MainMenu.Items.AddMenuItem("Briefing", null);
			MainMenu.Items.Add(tsmiBriefing);
			ToolStripMenuItem tsmiBriefingCoalitions = tsmiBriefing.DropDownItems.AddMenuItem("Coalitions", null);
			tsmiBriefing.DropDownItems.Add(tsmiBriefingCoalitions);
			AddMenuCoalition(tsmiBriefingCoalitions, ElementCoalition.Red);
			AddMenuCoalition(tsmiBriefingCoalitions, ElementCoalition.Blue);
			//AddMenuCoalition(tsmiBriefingCoalitions, ElementCoalition.Neutral);
			tsmiBriefing.DropDownItems.AddMenuItem("Open generate files config", (object _sender, EventArgs _e) => { OpenGenerateFilesForm(); });
			tsmiBriefing.DropDownItems.AddMenuItem("Generate files", (object _sender, EventArgs _e) => { GenerateFiles(); });
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
