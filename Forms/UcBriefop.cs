using DcsBriefop.Tools;
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
		}
		#endregion

		#region Methods
		public void DataToScreen()
		{
			LbSortie.Text = m_briefopManager.BopMission.Sortie;
			LbTheatre.Text = m_briefopManager.BopMission.Theatre.Name;
		}

		public void ScreenToData()
		{
		}
		#endregion

		#region Events
		private void BtMissionInformations_Click(object sender, System.EventArgs e)
		{
			FrmMissionInformations f = new FrmMissionInformations(m_briefopManager);
			f.ShowDialog();
		}

		private void BtMissionAssets_Click(object sender, System.EventArgs e)
		{
			FrmMissionAssets f = new FrmMissionAssets(m_briefopManager);
			f.ShowDialog();
		}
		#endregion


	}
}
