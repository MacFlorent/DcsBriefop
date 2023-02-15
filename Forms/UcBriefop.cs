using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.Tools;
using System.Diagnostics;
using System.Drawing;
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

			ToolsStyle.ButtonLarge(BtMissionInformations);
			ToolsStyle.ButtonLarge(BtMissionAirbases);
			ToolsStyle.ButtonLarge(BtMissionGroups);
			ToolsStyle.ButtonLarge(BtMissionMaps);
			ToolsStyle.ButtonLarge(BtMissionComs);
			ToolsStyle.ButtonLarge(BtMissionPackages);

			PnMissionActions.CenterInParentHorizontal();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			LbSortie.Text = m_briefopManager.BopMission.Sortie;
			LbMissionDirectory.Text = m_briefopManager.MizFilePath;
			LbTheatre.Text = m_briefopManager.BopMission.Theatre.Name;
			LbTheatre.CenterInParent();

			Image theatreImage = ToolsResources.GetImageResource($"theatre{m_briefopManager.BopMission.Theatre.Name}", "jpg");
			if (theatreImage is object)
			{
				PnMissionTheatre.BackgroundImage = theatreImage;
				PnMissionTheatre.BackgroundImageLayout = ImageLayout.Stretch;
			}
			else
			{
				PnMissionTheatre.BackgroundImage = null;
			}
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

		private void BtMissionMaps_Click(object sender, System.EventArgs e)
		{
			FrmMissionMaps.CreateModal(m_briefopManager, ParentForm);
		}
		private void BtBriefingPage_Click(object sender, System.EventArgs e)
		{
			BopBriefingFolder bopBriefingFolder = new BopBriefingFolder(m_briefopManager.BopMission);
			bopBriefingFolder.CoalitionName = ElementCoalition.Blue;

			BopBriefingPage bopBriefingPage = new BopBriefingPage(m_briefopManager.BopMission, bopBriefingFolder);
			bopBriefingPage.Title = "TEST PAGE";
			bopBriefingPage.DisplayTitle = true;
			bopBriefingPage.Parts.Add(new BopBriefingPartParagraphSortie(m_briefopManager.BopMission, bopBriefingFolder));
			bopBriefingPage.Parts.Add(new BopBriefingPartParagraphDescription(m_briefopManager.BopMission, bopBriefingFolder));
			bopBriefingPage.Parts.Add(new BopBriefingPartParagraphTask(m_briefopManager.BopMission, bopBriefingFolder));
			bopBriefingPage.Parts.Add(new BopBriefingPartBullseye(m_briefopManager.BopMission, bopBriefingFolder));

			bopBriefingFolder.Pages.Add(bopBriefingPage);

			FrmBriefingFolder.CreateModal(bopBriefingFolder, ParentForm);
		}
		#endregion


	}
}
