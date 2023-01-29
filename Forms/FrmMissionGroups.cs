using DcsBriefop.DataBopMission;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

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
		public FrmMissionGroups(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();

			m_gridManagerGroups = new GridManagerGroups(DgvAssets, m_briefopManager.BopMission.Groups);
			m_gridManagerGroups.SelectionChangedBopGroups += SelectionChangedBopGroupsEvent;

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			using (new WaitDialog(this))
			{
				DgvAssets.RowLeave -= DgvAssets_RowLeave;
				m_gridManagerGroups.SelectionChangedBopGroups -= SelectionChangedBopGroupsEvent;

				m_gridManagerGroups.Initialize();

				DgvAssets.RowLeave += DgvAssets_RowLeave;
				m_gridManagerGroups.SelectionChangedBopGroups += SelectionChangedBopGroupsEvent;
			}
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopGroup> selectedBopGroups = m_gridManagerGroups.GetSelectedBopGroups();
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
		private void FrmMissionGroups_FormClosed(object sender, FormClosedEventArgs e)
		{
			ScreenToDataDetail();
		}

		private void SelectionChangedBopGroupsEvent(object sender, GridManagerGroups.EventArgsBopGroup e)
		{
			DataToScreenDetail();
		}

		private void DgvAssets_RowLeave(object sender, DataGridViewCellEventArgs e)
		{
			ScreenToDataDetail();
		}
		#endregion
	}
}
