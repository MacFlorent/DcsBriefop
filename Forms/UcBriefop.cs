using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using DcsBriefop.Tools;
using System.Diagnostics;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefop : UserControl
	{
		#region Fields
		private BriefopManager m_briefopManager;

		private GridManagerBriefingFolders m_gridManagerBriefingFolders;
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
			SetImagePanel(PnMissionPicture, "backgroundMission", "jpg");
			ToolsStyle.LabelTitle(LbMissionTitle);
			LbMissionTitle.BackColor = Color.Transparent;
			LbMissionTitle.CenterInParent();
			ToolsStyle.LabelHeader(LbMissionTheatre);
			ToolsStyle.LabelHeader(LbMissionSortie);

			ToolsStyle.ButtonLarge(BtMissionInformations);
			ToolsStyle.ButtonLarge(BtMissionAirbases);
			ToolsStyle.ButtonLarge(BtMissionGroups);
			ToolsStyle.ButtonLarge(BtMissionMaps);
			ToolsStyle.ButtonLarge(BtMissionComs);

			PnMissionActions.CenterInParentHorizontal();

			PnBriefing.BackColor = ToolsStyle.ColorLightLight;
			SetImagePanel(PnBriefingPicture, "backgroundBriefing", "jpg");
			ToolsStyle.LabelTitle(LbBriefingTitle);
			LbBriefingTitle.BackColor = Color.Transparent;
			LbBriefingTitle.CenterInParent();

			ToolsStyle.LabelHeader(LbBriefingFolders);

			ToolsStyle.ButtonLarge(BtBriefingGenerate);
			ToolsStyle.ButtonLarge(BtBriefingPackages);
			ToolsStyle.ButtonLarge(BtBriefingAto);

			PnBriefingActions.CenterInParentHorizontal();
			LbBriefingFolders.CenterInParentHorizontal();
			ToolsStyle.ButtonOk(BtBriefingFolderAdd);
			ToolsStyle.ButtonCancel(BtBriefingFolderDelete);
			DgvBriefingFolders.MultiSelect = false;
		}

		private void SetImagePanel(Panel pn, string sImageName, string sImageExtension)
		{
			Image backgroundImage = ToolsResources.GetImageResource(sImageName, sImageExtension);
			if (backgroundImage is object)
			{
				backgroundImage = ToolsImage.SetImageOpacity(backgroundImage, 0.25f);
				pn.BackgroundImage = backgroundImage;
				pn.BackgroundImageLayout = ImageLayout.Stretch;
			}
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			LbMissionSortie.Text = m_briefopManager.BopMission.Sortie;
			LbMissionDirectory.Text = m_briefopManager.MizFilePath;
			LbMissionTheatre.Text = m_briefopManager.BopMission.Theatre.Name;
			LbMissionTheatre.CenterInParent();

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

			DataToScreenFolders();
		}

		private void DataToScreenFolders()
		{
			m_gridManagerBriefingFolders = new GridManagerBriefingFolders(DgvBriefingFolders, m_briefopManager.BopMission.BopBriefingFolders);
			m_gridManagerBriefingFolders.Initialize();
		}

		public void ScreenToData()
		{
		}

		private void BriefingFolderDetail()
		{
			BriefingFolderDetail(m_gridManagerBriefingFolders.GetSelectedElements().FirstOrDefault());
		}

		private void BriefingFolderDetail(BopBriefingFolder bopBriefingFolder)
		{
			if (bopBriefingFolder is not null)
			{
				FrmBriefingFolder.CreateModal(m_briefopManager.BopMission, bopBriefingFolder, ParentForm);
				DataToScreenFolders();
			}
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

		private void BtBriefingFolderDetail_Click(object sender, EventArgs e)
		{
			BriefingFolderDetail();
		}

		private void BtBriefingFolderAdd_Click(object sender, EventArgs e)
		{
			BopBriefingFolder bopBriefingFolder = new BopBriefingFolder();
			m_briefopManager.BopMission.BopBriefingFolders.Add(bopBriefingFolder);
			BriefingFolderDetail(bopBriefingFolder);
		}

		private void BtBriefingFolderDelete_Click(object sender, EventArgs e)
		{
			m_briefopManager.BopMission.BopBriefingFolders.Remove(m_gridManagerBriefingFolders.GetSelectedElements().FirstOrDefault());
			DataToScreenFolders();
		}

		private void DgvBriefingFolders_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			BriefingFolderDetail();
		}

		private async void GenerateBriefing(ElementBriefingGeneration briefingGeneration)
		{
			using (new WaitDialog(ParentForm))
			{
				await m_briefopManager.GenerateBriefingFiles(briefingGeneration);
			}

		}

		private void BtBriefingGenerate_MouseDown(object sender, MouseEventArgs e)
		{
			ContextMenuStrip menu = new ContextMenuStrip();
			menu.Items.Clear();

			menu.Items.AddMenuItem("In miz", (object _sender, EventArgs _e) => { GenerateBriefing(ElementBriefingGeneration.Miz); });
			menu.Items.AddMenuItem("In directory", (object _sender, EventArgs _e) => { GenerateBriefing(ElementBriefingGeneration.Directory); });

			if (menu.Items.Count > 0)
			{
				menu.Show(BtBriefingGenerate, new Point(0, BtBriefingGenerate.Height));
			}
		}
		#endregion


	}
}
