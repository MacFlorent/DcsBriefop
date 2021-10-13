using DcsBriefop.Briefing;
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

			CbInclusion.Items.Add("Excluded");
			CbInclusion.Items.Add("FullRoute");
			CbInclusion.Items.Add("Orbit");
			CbInclusion.Items.Add("Point");

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
			//TbTask.Text = m_group.;

			//if (m_group.BriefingInclusion == ElementBriefingInclusion.Excluded)
			m_ucMap.SetMapData(m_group.MapData);
		}

		private void ScreenToData()
		{

		}
		#endregion

		#region Events
		#endregion


	}
}
