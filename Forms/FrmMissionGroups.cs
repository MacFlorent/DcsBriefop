using DcsBriefop.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Data.Entity.Infrastructure.Design.Executor;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionGroups : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private GridManagerGroups m_gridManagerGroups;
		#endregion

		#region CTOR
		public FrmMissionGroups(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();

			m_gridManagerGroups = new GridManagerGroups(DgvAssets, m_briefopManager.BopMission.Groups);
			//gam.ColumnsDisplayed = GridManagerAsset.ColumnsDisplayedOpposing;

			DataToScreen();
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
	}
}
