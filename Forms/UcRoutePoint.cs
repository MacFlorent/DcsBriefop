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
			LbAltitude.Text = $"Altitude ({ToolsBriefop.GetUnitAltitude(PreferencesManager.Preferences.Briefing.MeasurementSystem)})";
			TbAltitude.Text = $"{m_bopRoutePoint.GetAltitude(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}";
			TbCoordinates.Text = m_bopRoutePoint.Coordinate.ToString(ElementCoordinateDisplay.All);
			TbNotes.Text = m_bopRoutePoint.Notes;
			TbAdditional.Text = m_bopRoutePoint.ToStringAdditional(PreferencesManager.Preferences.Briefing.MeasurementSystem);
		}

		public void ScreenToData()
		{
			m_bopRoutePoint.Notes = TbNotes.Text;
		}
		#endregion
	}
}
