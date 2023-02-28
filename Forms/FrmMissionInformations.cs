﻿using DcsBriefop.Data;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionInformations : FrmWithWaitDialog
	{
		#region Fields
		private BriefopManager m_briefopManager;

		private TextBox m_tbDescription;
		private Dictionary<string, UcMissionCoalition> m_ucCoalitionsControls;
		#endregion

		#region CTOR
		private FrmMissionInformations(BriefopManager briefopManager, WaitDialog waitDialog) : base(waitDialog)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();

			TcDetails.TabPages.Clear();
			AddTabDescription();

			m_ucCoalitionsControls = new Dictionary<string, UcMissionCoalition>();
			AddTabCoalition(ElementCoalition.Red);
			AddTabCoalition(ElementCoalition.Blue);
			AddTabCoalition(ElementCoalition.Neutral);

			ToolsStyle.ApplyStyle(this);
			ToolsStyle.ButtonCancel(BtClose);
		}

		public static void CreateModal(BriefopManager briefopManager, Form parentForm)
		{
			WaitDialog waitDialog = new WaitDialog(parentForm);
			FrmMissionInformations f = new FrmMissionInformations(briefopManager, waitDialog);
			f.ShowDialog();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			TbSortie.Text = m_briefopManager.BopMission.Sortie;
			DtpDate.Value = m_briefopManager.BopMission.Date;
			TbWeather.Text = $"{m_briefopManager.BopMission.Weather.ToString(ElementWeatherDisplay.Metar)}{Environment.NewLine}{Environment.NewLine}{m_briefopManager.BopMission.Weather.ToString(ElementWeatherDisplay.Plain)}";

			m_tbDescription.Text = m_briefopManager.BopMission.Description;

			foreach (UcMissionCoalition ucCoalition in m_ucCoalitionsControls.Values)
				ucCoalition.DataToScreen();

		}

		private void ScreenToData()
		{
			m_briefopManager.BopMission.Sortie = TbSortie.Text;
			m_briefopManager.BopMission.Date = DtpDate.Value;
			m_briefopManager.BopMission.Description = m_tbDescription.Text;
			foreach (UcMissionCoalition ucCoalition in m_ucCoalitionsControls.Values)
				ucCoalition.ScreenToData();
		}

		private void AddTabDescription()
		{
			m_tbDescription = new TextBox();
			m_tbDescription.Multiline = true;
			TcDetails.AddTab("Description", m_tbDescription);
		}

		private void AddTabCoalition(string sCoalitionName)
		{
			UcMissionCoalition uc = new UcMissionCoalition(m_briefopManager, sCoalitionName);
			m_ucCoalitionsControls.Add(sCoalitionName, uc);
			TcDetails.AddTab(sCoalitionName, uc);
		}

		//private ElementCoordinateDisplay GetCoordinateDisplayFromCheckboxes()
		//{
		//	ElementCoordinateDisplay coordinateDisplay = 0;
		//	if (CkCoordinateDisplayDms.Checked)
		//		coordinateDisplay |= ElementCoordinateDisplay.Dms;
		//	if (CkCoordinateDisplayDdm.Checked)
		//		coordinateDisplay |= ElementCoordinateDisplay.Ddm;
		//	if (CkCoordinateDisplayMgrs.Checked)
		//		coordinateDisplay |= ElementCoordinateDisplay.Mgrs;

		//	return coordinateDisplay;
		//}
		#endregion

		#region Events
		private void FrmMissionInformations_Shown(object sender, EventArgs e)
		{
			DataToScreen();
		}

		private void FrmMissionInformations_FormClosed(object sender, FormClosedEventArgs e)
		{
			ScreenToData();
		}

		private void BtClose_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
	}
}
