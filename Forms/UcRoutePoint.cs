using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcRoutePoint : UserControl
	{
		#region Fields
		protected BriefopManager m_briefopManager;
		protected BopRoutePoint m_bopRoutePoint;
		#endregion

		#region Properties
		public BopRoutePoint BopRoutePoint
		{
			protected get { return m_bopRoutePoint; }
			set
			{
				m_bopRoutePoint = value;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcRoutePoint(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
		}
		#endregion

		#region Methods
		public void DataToScreen()
		{
			m_bopRoutePoint.FinalizeFromMiz();

			TbNumber.Text = m_bopRoutePoint.Number.ToString();
			TbName.Text = m_bopRoutePoint.Name;
			TbType.Text = m_bopRoutePoint.Type;
			TbAction.Text = m_bopRoutePoint.Action;
			LbAltitude.Text = $"Altitude ({ToolsMeasurement.AltitudeUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem)})";
			TbAltitude.Text = $"{ToolsMeasurement.AltitudeDisplay(m_bopRoutePoint.AltitudeMeters, PreferencesManager.Preferences.Briefing.MeasurementSystem):0}";
			LbAltitudeCustom.Text = $"Custom altitude ({ToolsMeasurement.AltitudeUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem)})";
			TbAltitudeCustom.Text = m_bopRoutePoint.AltitudeCustomMeters is null ? null : ToolsMeasurement.AltitudeDisplay(m_bopRoutePoint.AltitudeCustomMeters.Value, PreferencesManager.Preferences.Briefing.MeasurementSystem).ToString();
			TbCoordinates.Text = m_bopRoutePoint.Coordinate.ToString(ElementCoordinateDisplay.All);
			TbNotes.Text = m_bopRoutePoint.Notes;
			TbAdditional.Text = m_bopRoutePoint.ToStringAdditional(PreferencesManager.Preferences.Briefing.MeasurementSystem);
		}

		public void ScreenToData()
		{
			if (int.TryParse(TbAltitudeCustom.Text, out int iAltitude))
			{
				m_bopRoutePoint.AltitudeCustomMeters = ToolsMeasurement.AltitudeData(iAltitude, PreferencesManager.Preferences.Briefing.MeasurementSystem);
			}
			m_bopRoutePoint.Notes = TbNotes.Text;
		}
		#endregion

		private void TbAltitudeCustom_Validated(object sender, EventArgs e)
		{
			if (!int.TryParse(TbAltitudeCustom.Text, out int iAltitude))
			{
				TbAltitudeCustom.Text = null;
			}
		}
	}
}
