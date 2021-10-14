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

			BriefingInclusion.FillCombo(CbInclusion);

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

			CbInclusion.SelectedItem = BriefingInclusion.GetById(m_group.BriefingInclusion);

			m_ucMap.SetMapData(m_group.MapData);
		}

		private void ScreenToData()
		{
			m_group.BriefingInclusion = (CbInclusion.SelectedItem as BriefingInclusion)?.Id ?? ElementBriefingInclusionId.NotSet;
			CbInclusion.SelectedItem = BriefingInclusion.GetById(m_group.BriefingInclusion);
		}


		#endregion

		#region Events
		private void CbInclusion_Validated(object sender, System.EventArgs e)
		{
			ScreenToData();
		}
		#endregion
	}
}
