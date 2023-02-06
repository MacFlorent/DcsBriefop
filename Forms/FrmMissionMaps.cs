using DcsBriefop.DataMiz;
using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Windows.Forms;
using System.Linq;
using DcsBriefop.DataBopMission;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionMaps : FrmWithWaitDialog
	{
		#region Fields
		private BriefopManager m_briefopManager;

		private UcMap m_ucMap;
		#endregion

		#region CTOR
		public FrmMissionMaps(BriefopManager briefopManager, WaitDialog waitDialog) : base(waitDialog)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			RbMapSelectionRed.Tag = ElementCoalition.Red;
			RbMapSelectionBlue.Tag = ElementCoalition.Blue;
			RbMapSelectionNeutral.Tag = ElementCoalition.Neutral;

			DataToScreen();
		}

		public static void CreateModal(BriefopManager briefopManager, Form parentForm)
		{
			WaitDialog waitDialog = new WaitDialog(parentForm);
			FrmMissionMaps f = new FrmMissionMaps(briefopManager, waitDialog);
			f.ShowDialog();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_ucMap = new UcMap();
			m_ucMap.Dock = DockStyle.Fill;
			PnMap.Controls.Clear();
			PnMap.Controls.Add(m_ucMap);

			DataToScreenDetail();
		}

		private void DataToScreenDetail()
		{
			string sCoalition = PnMapSelection.Controls.OfType<RadioButton>().Where(_rb => _rb.Checked).FirstOrDefault()?.Tag as string ?? "global";

			MizBopMap mapData = m_briefopManager.BopMission.MapData;
			if (sCoalition is object && m_briefopManager.BopMission.Coalitions.ContainsKey(sCoalition))
			{
				BopCoalition bopCoalition = m_briefopManager.BopMission.Coalitions[sCoalition];
				mapData = bopCoalition.MapData;
			}

			m_ucMap.SetMapData(mapData, sCoalition, false);
		}
		#endregion

		#region Events
		private void RbMapSelection_CheckedChanged(object sender, EventArgs e)
		{
			DataToScreenDetail();
		}
		#endregion
	}
}
