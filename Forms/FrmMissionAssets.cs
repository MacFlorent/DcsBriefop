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
	internal partial class FrmMissionAssets : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;
		#endregion

		#region CTOR
		public FrmMissionAssets(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();

			//GridManagerAsset gma = new GridManagerAsset(DgvAssets, m_briefopManager.BopMission.Assets, null);
			//gam.ColumnsDisplayed = GridManagerAsset.ColumnsDisplayedOpposing;
			//gam.Initialize();

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
		}

		private void ScreenToData()
		{
		}
		#endregion
	}
}
