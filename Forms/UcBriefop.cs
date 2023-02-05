﻿using DcsBriefop.Tools;
using System.Diagnostics;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefop : UserControl
	{
		#region Fields
		private BriefopManager m_briefopManager;
		#endregion

		#region Properties
		public BriefopManager BriefopManager
		{
			private get { return m_briefopManager; }
			set
			{
				m_briefopManager = value;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcBriefop()
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.SetBackgroundImage(this);
			PnBackground.CenterInParent();
			PnMission.BackColor = ToolsStyle.ColorLightLight;
			PnBriefing.BackColor = ToolsStyle.ColorLightLight;

			ToolsStyle.LabelTitle(LbMissionTitle);
			ToolsStyle.LabelHeader(LbTheatre);
			ToolsStyle.LabelHeader(LbSortie);

			ToolsStyle.LabelTitle(LbBriefingTitle);
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			LbSortie.Text = m_briefopManager.BopMission.Sortie;
			LbTheatre.Text = m_briefopManager.BopMission.Theatre.Name;
			LbMissionDirectory.Text = m_briefopManager.MizFilePath;
		}

		public void ScreenToData()
		{
		}
		#endregion

		#region Events
		private void LbMissionDirectory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(m_briefopManager.MizFileDirectory);
		}

		private void BtMissionInformations_Click(object sender, System.EventArgs e)
		{
			FrmMissionInformations.CreateModal(m_briefopManager, ParentForm);
			DataToScreen();
		}

		private void BtMissionGroups_Click(object sender, System.EventArgs e)
		{
			FrmMissionGroups.CreateModal(m_briefopManager, ParentForm);
		}

		private void BtMissionAirdromes_Click(object sender, System.EventArgs e)
		{
			FrmMissionAirbases.CreateModal(m_briefopManager, ParentForm);
		}
		#endregion
	}
}
