using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace DcsBriefop.Forms
{
	internal partial class UcGroupInformation : UcGroupBase
	{
		#region Fields
		#endregion

		#region Properties
		#endregion
		
		#region CTOR
		public UcGroupInformation(BriefopManager briefopManager, GMapControl mapControl) : base (briefopManager, mapControl)
		{
			InitializeComponent();

			MapTemplateMarker.FillCombo(CbMapMarker, CbMapMarker_SelectedValueChanged);
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			CbMapMarker.SelectedValueChanged -= CbMapMarker_SelectedValueChanged;

			m_bopGroup.FinalizeFromMiz();

			TbId.Text = m_bopGroup.Id.ToString();
			CkLateActivation.Checked = m_bopGroup.LateActivation;
			CkPlayable.Checked = m_bopGroup.Playable;
			TbName.Text = m_bopGroup.Name;
			TbDisplayName.Text = m_bopGroup.ToStringDisplayName();
			TbType.Text = m_bopGroup.Type;
			TbAttributes.Text = m_bopGroup.Attributes.ToString();
			TbRadio.Text = m_bopGroup.Radio?.ToString();
			TbOther.Text = m_bopGroup.ToStringAdditional();
			LbAltitude.Text = $"Altitude ({ToolsMeasurement.AltitudeUnit (PreferencesManager.Preferences.Briefing.MeasurementSystem)})";
			TbAltitude.Text = $"{m_bopGroup.GetAltitude(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}";
			TbCoordinates.Text = m_bopGroup.Coordinate.ToString(ElementCoordinateDisplay.All);
			CbMapMarker.Text = m_bopGroup.MapMarker;

			DataToScreenMap();

			CbMapMarker.SelectedValueChanged += CbMapMarker_SelectedValueChanged;
		}

		public override void DataToScreenMap()
		{
			m_mapControl.Overlays.Clear();
			m_mapControl.Overlays.Add(m_bopGroup.GetMapOverlayPosition());
			m_mapControl.Overlays.Add(m_bopGroup.GetMapOverlayRoute(null, ElementMapOverlayRouteDisplay.NoMarkerFirstPoint));

			m_mapControl.Position = new PointLatLng(m_bopGroup.Coordinate.Latitude.DecimalDegree, m_bopGroup.Coordinate.Longitude.DecimalDegree);
			m_mapControl.ForceRefresh();
		}

		public override void ScreenToData()
		{
			m_bopGroup.MapMarker = CbMapMarker.Text;
		}
		#endregion

		#region Events
		private void CbMapMarker_SelectedValueChanged(object sender, System.EventArgs e)
		{
			ScreenToData();
			DataToScreenMap();
		}

		#endregion
	}
}
