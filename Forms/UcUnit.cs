using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Map;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcUnit : UserControl
	{
		#region Fields
		protected BriefopManager m_briefopManager;
		protected BopUnit m_bopUnit;

		private UcGroupUnits m_ucParentGroupUnits;
		#endregion

		#region Properties
		public BopUnit BopUnit
		{
			protected get { return m_bopUnit; }
			set
			{
				m_bopUnit = value;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcUnit(BriefopManager briefopManager, UcGroupUnits ucParentGroupUnits)
		{
			m_briefopManager = briefopManager;
			m_ucParentGroupUnits = ucParentGroupUnits;

			InitializeComponent();

			MapTemplateMarker.FillCombo(CbMapMarker, CbMapMarker_SelectedValueChanged);
		}
		#endregion

		#region Methods
		public void DataToScreen()
		{
			CbMapMarker.SelectedValueChanged -= CbMapMarker_SelectedValueChanged;

			m_bopUnit.FinalizeFromMiz();

			TbId.Text = m_bopUnit.Id.ToString();
			CkPlayable.Checked = m_bopUnit.Playable;
			TbName.Text = m_bopUnit.Name;
			TbDisplayName.Text = m_bopUnit.ToStringDisplayName();
			TbType.Text = m_bopUnit.Type;
			TbAttributes.Text = m_bopUnit.Attributes.ToString();
			TbOther.Text = m_bopUnit.ToStringAdditional();
			LbAltitude.Text = $"Altitude ({ToolsMeasurement.AltitudeUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem)})";
			TbAltitude.Text = $"{m_bopUnit.GetAltitude(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}";
			TbCoordinates.Text = m_bopUnit.Coordinate.ToString(ElementCoordinateDisplay.All);
			CbMapMarker.Text = m_bopUnit.MapMarker;

			if (m_bopUnit.Link16 is null)
			{
				TbLink16Callsign.Visible = TbLink16Stn.Visible = false;
			}
			else
			{
				TbLink16Callsign.Visible = TbLink16Stn.Visible = true;
				TbLink16Callsign.Text = m_bopUnit.Link16?.ToStringCallsign();
				TbLink16Stn.Text = m_bopUnit.Link16?.StnL16;
			}

			CbMapMarker.SelectedValueChanged += CbMapMarker_SelectedValueChanged;
		}

		public void ScreenToData()
		{
			m_bopUnit.MapMarker = CbMapMarker.Text;
			if (m_bopUnit.Link16 is not null)
				m_bopUnit.Link16.StnL16 = TbLink16Stn.Text;
		}
		#endregion

		#region Events
		private void CbMapMarker_SelectedValueChanged(object sender, EventArgs e)
		{
			ScreenToData();
			m_ucParentGroupUnits?.DataToScreenMap();
		}
		#endregion

	}
}
