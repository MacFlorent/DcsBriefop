using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcMissionCoalition : UserControl
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private BopCoalition m_bopCoalition;
		#endregion

		#region CTOR
		public UcMissionCoalition(BriefopManager briefopManager, string sCoalitionName)
		{
			m_briefopManager = briefopManager;
			m_bopCoalition = m_briefopManager.BopMission.Coalitions[sCoalitionName];
			m_bopCoalition.FinalizeFromMiz();

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			MasterDataRepository.FillCombo(MasterDataType.BullseyeWaypoint, CbBullseyeWaypoint, CbBullseyeWaypoint_SelectedValueChanged);
		}
		#endregion

		#region Methods
		public void DataToScreen()
		{
			CbBullseyeWaypoint.SelectedValueChanged -= CbBullseyeWaypoint_SelectedValueChanged;

			TbBullseye.Text = m_bopCoalition.Bullseye.ToString(ElementCoordinateDisplay.All);
			TbBullseyeDescription.Text = m_bopCoalition.BullseyeDescription;
			CbBullseyeWaypoint.SelectedValue = (int)m_bopCoalition.BullseyeWaypoint;
			TbTask.Text = m_bopCoalition.Task;

			CbBullseyeWaypoint.SelectedValueChanged += CbBullseyeWaypoint_SelectedValueChanged;
		}

		public void ScreenToData()
		{
			m_bopCoalition.BullseyeDescription = TbBullseyeDescription.Text;
			m_bopCoalition.BullseyeWaypoint = (ElementBullseyeWaypoint)CbBullseyeWaypoint.SelectedValue;
			m_bopCoalition.Task = TbTask.Text;
		}
		#endregion

		#region Events
		private void CbBullseyeWaypoint_SelectedValueChanged(object sender, EventArgs e)
		{
			ScreenToData();
			m_briefopManager.BopMission.SetBullseyeRoutePoint();
		}
		#endregion
	}
}
