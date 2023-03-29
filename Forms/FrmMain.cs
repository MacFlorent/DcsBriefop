﻿using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	public partial class FrmMain : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private UcNoFile m_ucNoFile;
		private UcBriefop m_ucBriefop;
		#endregion

		#region CTOR
		public FrmMain()
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			Icon = ToolsResources.GetIconResource("icon256", null);

			m_ucNoFile = new UcNoFile(this);
			m_ucBriefop = new UcBriefop();

			BuildMenu();
			DataToScreen();
		}
		#endregion

		#region Methods
		public void MizOpen()
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.InitialDirectory = PreferencesManager.Preferences.Application.WorkingDirectory;
				ofd.Filter = ElementGlobalData.DcsFileFilter;
				ofd.RestoreDirectory = true;

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					MizOpen(ofd.FileName);
				}
			}
		}

		public void MizOpen(string sMizFilePath)
		{
			using (new WaitDialog(this))
			{
				m_briefopManager = new BriefopManager(sMizFilePath);
				BuildMenu();
				DataToScreen();
			}
		}


		private void MizReload()
		{
			if (m_briefopManager is null)
				throw new ExceptionBop("No mission is currently loaded");

			using (new WaitDialog(this))
			{
				m_briefopManager.MizLoad();
				BuildMenu();
				DataToScreen();
			}
		}

		private void MizSave(string sMizFilePath)
		{
			if (m_briefopManager is null)
				throw new ExceptionBop("No mission is currently loaded");

			using (new WaitDialog(this))
			{
				m_briefopManager.MizSave(sMizFilePath);

				if (PreferencesManager.Preferences.Briefing.GenerateOnSave)
					m_briefopManager.GenerateBriefing(ElementBriefingOutput.Miz);
				if (PreferencesManager.Preferences.Application.GenerateBatchCommandOnSave)
					m_briefopManager.MizBatchCommand();
			}

			MizReload();
		}

		private void MizSaveAs()
		{
			if (m_briefopManager is null)
				throw new ExceptionBop("No mission is currently loaded");

			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				sfd.InitialDirectory = m_briefopManager.MizFileDirectory;
				sfd.FileName = m_briefopManager.MizFileName;
				sfd.Filter = ElementGlobalData.DcsFileFilter;

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					MizSave(sfd.FileName);
				}
			}
		}

		private void OpenPreferences()
		{
			FrmPreferences f = new FrmPreferences();
			if (f.ShowDialog() == DialogResult.OK)
			{
				BuildMenu();
				DataToScreen();
			}
		}

		private void OpenDcsObjects()
		{
			FrmDcsObjects f = new FrmDcsObjects();
			f.ShowDialog();
		}

		private void GenerateBatchCommand()
		{
			if (m_briefopManager is null)
				throw new ExceptionBop("No mission is currently loaded");

			string sCommandFilePath = m_briefopManager.MizBatchCommandFileName();
			if (ToolsControls.ShowMessageBoxQuestion(
				$"""
				The following command file will be generated in the mission directory :
				    {sCommandFilePath}
				
				You can execute this command file to generate the kneeboard contents for the mission. This file will also be generated each time the mission is saved in DcsBriefop if the relevant preference is active.
				"""))
			{
				using (new WaitDialog(this))
				{
					m_briefopManager.MizBatchCommand();
				}
			}
		}

		private void DataToScreen()
		{
			PnMain.Controls.Clear();

			if (m_briefopManager is null)
			{
				PnMain.Controls.Add(m_ucNoFile);
				m_ucNoFile.Dock = DockStyle.Fill;
				m_ucNoFile.DataToScreen();
			}
			else
			{
				PnMain.Controls.Add(m_ucBriefop);
				m_ucBriefop.Dock = DockStyle.Fill;
				m_ucBriefop.BriefopManager = m_briefopManager;
			}

			SetStatusStrip();
		}

		private void SetStatusStrip()
		{
			System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
			System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);

			StatusStrip.Items.Clear();
			StatusStrip.Items.Add(fvi.FileDescription);
			StatusStrip.Items.Add(fvi.FileVersion);
			if (m_briefopManager is object)
				StatusStrip.Items.Add(m_briefopManager.MizFilePath);
		}
		#endregion

		#region Menus
		private void BuildMenu()
		{
			MainMenu.Items.Clear();

			ToolStripMenuItem tsmiMiz = MainMenu.Items.AddMenuItem("Miz", null);
			MainMenu.Items.Add(tsmiMiz);
			tsmiMiz.DropDownItems.AddMenuItem("Open", (object _sender, EventArgs _e) => { MizOpen(); });
			
			if (m_briefopManager is object)
			{
				tsmiMiz.DropDownItems.AddMenuSeparator();
				tsmiMiz.DropDownItems.AddMenuItem("Reload", (object _sender, EventArgs _e) => { MizReload(); });
				tsmiMiz.DropDownItems.AddMenuItem("Save", (object _sender, EventArgs _e) => { MizSave(null); });
				tsmiMiz.DropDownItems.AddMenuItem("Save as", (object _sender, EventArgs _e) => { MizSaveAs(); });
				tsmiMiz.DropDownItems.AddMenuItem("Generate batch command", (object _sender, EventArgs _e) => { GenerateBatchCommand(); });
			}

			if (PreferencesManager.Preferences.Application.RecentMiz.Count > 0)
			{
				tsmiMiz.DropDownItems.AddMenuSeparator();
				foreach(string sRecentMizFilePath in PreferencesManager.Preferences.Application.RecentMiz)
					tsmiMiz.DropDownItems.AddMenuItem(sRecentMizFilePath, (object _sender, EventArgs _e) => { MizOpen(sRecentMizFilePath); });
			}
			tsmiMiz.DropDownItems.AddMenuSeparator();
			tsmiMiz.DropDownItems.AddMenuItem("Exit", (object _sender, EventArgs _e) => { Application.Exit(); });

			ToolStripMenuItem tsmiTools = MainMenu.Items.AddMenuItem("Tools", null);
			MainMenu.Items.Add(tsmiTools);
			tsmiTools.DropDownItems.AddMenuItem("Preferences", (object _sender, EventArgs _e) => { OpenPreferences(); });
			tsmiTools.DropDownItems.AddMenuItem("DCS objects", (object _sender, EventArgs _e) => { OpenDcsObjects(); });
		}
		#endregion

		#region Events
		#endregion
	}
}