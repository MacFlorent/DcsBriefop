﻿using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using System.Windows.Forms;

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
		}
		#endregion

		#region Methods
		public void DataToScreen()
		{
			CkBullseyeWaypoint.CheckedChanged -= CkBullseyeWaypoint_CheckedChanged;

			DisplayCurrentBullseyeLocalisation(m_briefopManager.BopMission.PreferencesMission.CoordinateDisplay);
			TbBullseyeDescription.Text = m_bopCoalition.BullseyeDescription;
			CkBullseyeWaypoint.Checked = m_bopCoalition.BullseyeWaypoint;
			TbTask.Text = m_bopCoalition.Task;
			
			CkBullseyeWaypoint.CheckedChanged += CkBullseyeWaypoint_CheckedChanged;
		}

		public void ScreenToData()
		{
			m_bopCoalition.BullseyeDescription = TbBullseyeDescription.Text;
			m_bopCoalition.BullseyeWaypoint = CkBullseyeWaypoint.Checked;
			m_bopCoalition.Task = TbTask.Text;
		}

		public void DisplayCurrentBullseyeLocalisation(ElementCoordinateDisplay coordinateDisplay)
		{
			TbBullseye.Text = m_bopCoalition.Bullseye.ToString(coordinateDisplay);
		}
		#endregion

		#region Events
		private void CkBullseyeWaypoint_CheckedChanged(object sender, System.EventArgs e)
		{
			ScreenToData();
			m_briefopManager.BopMission.SetBullseyeRoutePoint();
		}
		#endregion
	}
}
