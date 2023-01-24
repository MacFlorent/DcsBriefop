using DcsBriefop.DataBopMission;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using static DcsBriefop.Forms.GridManagerGroups;

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

			m_ucGroup = new UcGroup(m_briefopManager);

			DataToScreen();
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopGroup> selectedBopGroups = m_gridManagerGroups.GetSelectedBopGroups();
			if (selectedBopGroups.Count() == 1)
			{
				if (ScMain.Panel2.Controls.Count > 0 && !(ScMain.Panel2.Controls[0] is UcGroup))
				{
					ScMain.Panel2.Controls.Clear();
				}
				if (ScMain.Panel2.Controls.Count == 0)
				{
					ScMain.Panel2.Controls.Add(m_ucGroup);
					m_ucGroup.Dock = DockStyle.Fill;
				}

				m_ucGroup.BopGroup = selectedBopGroups.First();
			}
			else
			{
				ScMain.Panel2.Controls.Clear();
			}
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			using (new WaitDialog(this))
			{
				m_gridManagerGroups.Initialize();
			}
		}

		private void ScreenToData()
		{
		}
		#endregion

		#region Events
		private void SelectionChangedBopGroupsEvent(object sender, EventArgsBopGroup e)
		{
			DataToScreenDetail();
		}
		#endregion
	}
}
