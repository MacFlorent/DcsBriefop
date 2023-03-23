using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionGroups : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private GridManagerGroups m_gridManagerGroups;

		private UcGroup m_ucGroup;
		#endregion

		#region CTOR
		private FrmMissionGroups(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			m_gridManagerGroups = new GridManagerGroups(DgvGroups, m_briefopManager.BopMission.Groups);
			m_gridManagerGroups.SelectionChanged += SelectionChangedEvent;
		}

		public static void CreateModal(BriefopManager briefopManager, Form parentForm)
		{
			FrmMissionGroups f = new FrmMissionGroups(briefopManager);
			f.ShowDialog(parentForm);
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_gridManagerGroups.SelectionChanged -= SelectionChangedEvent;

			m_gridManagerGroups.Refresh();
			DataToScreenDetail();

			m_gridManagerGroups.SelectionChanged += SelectionChangedEvent;
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopGroup> selectedBopGroups = m_gridManagerGroups.GetSelectedElements();
			if (selectedBopGroups.Count() == 1)
			{
				BopGroup selectedBopGroup = selectedBopGroups.First();
				if (ScMain.Panel2.Controls.Count > 0 && ScMain.Panel2.Controls[0] is not UcGroup)
				{
					ScMain.Panel2.Controls.Clear();
				}
				if (m_ucGroup is null)
				{
					m_ucGroup = new UcGroup(m_briefopManager);
				}
				if (ScMain.Panel2.Controls.Count == 0)
				{
					ScMain.Panel2.Controls.Add(m_ucGroup);
					m_ucGroup.Dock = DockStyle.Fill;
				}

				m_ucGroup.BopGroup = selectedBopGroup;
			}
			else
			{
				ScMain.Panel2.Controls.Clear();
			}
		}

		private void ScreenToDataDetail()
		{
			m_ucGroup?.ScreenToData();
		}
		#endregion

		#region Events
		private void FrmMissionGroups_Shown(object sender, EventArgs e)
		{
			using (new WaitDialog(this))
				DataToScreen();
		}

		private void FrmMissionGroups_FormClosed(object sender, FormClosedEventArgs e)
		{
			ScreenToDataDetail();
		}

		private void SelectionChangedEvent(object sender, EventArgs e)
		{
			ScreenToDataDetail();
			DataToScreenDetail();
		}
		#endregion
	}
}
