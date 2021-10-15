using DcsBriefop.Briefing;
using DcsBriefop.MasterData;
using DcsBriefop.UcBriefing;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmGroupDetail : Form
	{
		#region Fields
		private BriefingGroup m_group;
		private UcMap m_ucMap;
		#endregion

		#region CTOR
		internal FrmGroupDetail(BriefingGroup group)
		{
			InitializeComponent();
			m_group = group;

			GroupStatus.FillCombo(CbGroupStatus);

			m_ucMap = new UcMap();
			m_ucMap.Dock = DockStyle.Fill;
			PnMap.Controls.Clear();
			PnMap.Controls.Add(m_ucMap);

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			TbId.Text = m_group.Id.ToString();
			TbName.Text = m_group.Name;
			TbCategory.Text = m_group.Category;
			TbType.Text = m_group.GetUnitTypes();
			CkPlayable.Checked = m_group.Playable;
			CkLateActivation.Checked = m_group.LateActivation;
			//TbTask.Text = m_group.;

			CbGroupStatus.SelectedItem = GroupStatus.GetById(m_group.BriefingStatus);

			m_ucMap.SetMapData(m_group.MapData);
		}

		private void ScreenToData()
		{
			int iStatus = CbGroupStatus.SelectedValue as int? ?? ElementGroupStatusId.Excluded;
			if (iStatus != m_group.BriefingStatus)
			{
				m_group.BriefingStatus = iStatus;
				m_group.InitializeMapData();
			}
		}


		#endregion

		#region Events
		private void CbGroupStatus_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			ScreenToData();
		}
		#endregion
	}
}
