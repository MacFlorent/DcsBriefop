using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionInformations : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;

		private TextBox m_tbDescription;
		private Dictionary <string, UcMissionCoalition> m_ucCoalitionsControls;
		#endregion

		#region CTOR
		public FrmMissionInformations(BriefopManager briefopManager)
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
			ToolsStyle.ButtonOk(BtOk);
			ToolsStyle.ButtonCancel(BtCancel);

			MasterDataRepository.FillCombo(MasterDataType.WeatherDisplay, CbWeatherDisplay);

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CbWeatherDisplay.Validated -= CbWeatherDisplay_Validated;
			CkCoordinateDisplayDms.CheckedChanged -= CkCoordinateDisplay_CheckedChanged;
			CkCoordinateDisplayDdm.CheckedChanged -= CkCoordinateDisplay_CheckedChanged;
			CkCoordinateDisplayMgrs.CheckedChanged -= CkCoordinateDisplay_CheckedChanged;

			TbSortie.Text = m_briefopManager.BopMission.Sortie;
			DtpDate.Value = m_briefopManager.BopMission.Date;
			CbWeatherDisplay.SelectedValue = (int)m_briefopManager.BopMission.PreferencesMission.WeatherDisplay;
			CkCoordinateDisplayDms.Checked = (m_briefopManager.BopMission.PreferencesMission.CoordinateDisplay & ElementCoordinateDisplay.Dms) > 0;
			CkCoordinateDisplayDdm.Checked = (m_briefopManager.BopMission.PreferencesMission.CoordinateDisplay & ElementCoordinateDisplay.Ddm) > 0;
			CkCoordinateDisplayMgrs.Checked = (m_briefopManager.BopMission.PreferencesMission.CoordinateDisplay & ElementCoordinateDisplay.Mgrs) > 0;
			DisplayCurrentWeather();

			m_tbDescription.Text = m_briefopManager.BopMission.Description;

			foreach(UcMissionCoalition ucCoalition in m_ucCoalitionsControls.Values)
				ucCoalition.DataToScreen();

			CbWeatherDisplay.Validated += CbWeatherDisplay_Validated;
			CkCoordinateDisplayDms.CheckedChanged += CkCoordinateDisplay_CheckedChanged;
			CkCoordinateDisplayDdm.CheckedChanged += CkCoordinateDisplay_CheckedChanged;
			CkCoordinateDisplayMgrs.CheckedChanged += CkCoordinateDisplay_CheckedChanged;
		}

		private void ScreenToData()
		{
			m_briefopManager.BopMission.Sortie = TbSortie.Text;
			m_briefopManager.BopMission.Date = DtpDate.Value;
			m_briefopManager.BopMission.PreferencesMission.WeatherDisplay = (ElementWeatherDisplay)CbWeatherDisplay.SelectedValue;

			m_briefopManager.BopMission.PreferencesMission.CoordinateDisplay = GetCoordinateDisplayFromCheckboxes();
			m_briefopManager.BopMission.Description = m_tbDescription.Text;
			foreach (UcMissionCoalition ucCoalition in m_ucCoalitionsControls.Values)
				ucCoalition.ScreenToData();
		}

		private void AddTabDescription()
		{
			m_tbDescription = new TextBox();
			m_tbDescription.Multiline= true;
			AddTab("Description", m_tbDescription);
		}

		private void AddTabCoalition(string sCoalitionName)
		{
			UcMissionCoalition uc = new UcMissionCoalition(m_briefopManager, sCoalitionName);
			m_ucCoalitionsControls.Add(sCoalitionName, uc);
			AddTab(sCoalitionName, uc);
		}

		private void AddTab(string sText, Control control)
		{
			control.Dock = DockStyle.Fill;
			TabPage tp = new TabPage(sText);
			tp.Controls.Add(control);
			TcDetails.TabPages.Add(tp);
		}

		private void DisplayCurrentWeather()
		{
			TbWeather.Text = m_briefopManager.BopMission.Weather.ToString((ElementWeatherDisplay)CbWeatherDisplay.SelectedValue);
		}

		private ElementCoordinateDisplay GetCoordinateDisplayFromCheckboxes()
		{
			ElementCoordinateDisplay coordinateDisplay = 0;
			if (CkCoordinateDisplayDms.Checked)
				coordinateDisplay |= ElementCoordinateDisplay.Dms;
			if (CkCoordinateDisplayDdm.Checked)
				coordinateDisplay |= ElementCoordinateDisplay.Ddm;
			if (CkCoordinateDisplayMgrs.Checked)
				coordinateDisplay |= ElementCoordinateDisplay.Mgrs;

			return coordinateDisplay;
		}
		#endregion

		#region Events
		private void BtOk_Click(object sender, EventArgs e)
		{
			ScreenToData();
			Close();
		}

		private void BtCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void CbWeatherDisplay_Validated(object sender, EventArgs e)
		{
			DisplayCurrentWeather();
		}

		private void CkCoordinateDisplay_CheckedChanged(object sender, EventArgs e)
		{
			foreach (UcMissionCoalition ucCoalition in m_ucCoalitionsControls.Values)
				ucCoalition.DisplayCurrentBullseyeLocalisation(GetCoordinateDisplayFromCheckboxes());
		}
		#endregion
	}
}
