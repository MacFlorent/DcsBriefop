using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionGroups : FrmWithWaitDialog
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private GridManagerGroups m_gridManagerGroups;

		private UcGroup m_ucGroup;
		#endregion

		#region CTOR
		private FrmMissionGroups(BriefopManager briefopManager, WaitDialog waitDialog) : base(waitDialog)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			m_gridManagerGroups = new GridManagerGroups(DgvGroups, m_briefopManager.BopMission.Groups);
			m_gridManagerGroups.SelectionChangedTyped += SelectionChangedTypedEvent;
		}

		public static void CreateModal(BriefopManager briefopManager, Form parentForm)
		{
			WaitDialog waitDialog = new WaitDialog(parentForm);
			FrmMissionGroups f = new FrmMissionGroups(briefopManager, waitDialog);
			f.ShowDialog();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_gridManagerGroups.SelectionChangedTyped -= SelectionChangedTypedEvent;

			m_gridManagerGroups.Initialize();
			DataToScreenDetail();

			m_gridManagerGroups.SelectionChangedTyped += SelectionChangedTypedEvent;
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopGroup> selectedBopGroups = m_gridManagerGroups.GetSelected();
			if (selectedBopGroups.Count() == 1)
			{
				BopGroup selectedBopGroup = selectedBopGroups.First();
				if (ScMain.Panel2.Controls.Count > 0 && !(ScMain.Panel2.Controls[0] is UcGroup))
				{
					ScMain.Panel2.Controls.Clear();
				}
				if (m_ucGroup is null)
				{
					m_ucGroup = new UcGroup(m_briefopManager, selectedBopGroup);
				}
				else
				{
					m_ucGroup.BopGroup = selectedBopGroup;
				}

				if (ScMain.Panel2.Controls.Count == 0)
				{
					ScMain.Panel2.Controls.Add(m_ucGroup);
					m_ucGroup.Dock = DockStyle.Fill;
				}
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
			DataToScreen();
		}

		private void FrmMissionGroups_FormClosed(object sender, FormClosedEventArgs e)
		{
			ScreenToDataDetail();
		}

		private void SelectionChangedTypedEvent(object sender, GridManagerGroups.EventArgsBopGroups e)
		{
			ScreenToDataDetail();
			DataToScreenDetail();
		}
		#endregion
	}
}
