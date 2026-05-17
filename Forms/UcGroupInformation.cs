using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using Mapsui;
using Mapsui.Layers;

namespace DcsBriefop.Forms
{
	internal partial class UcGroupInformation : UcGroupBase
	{
		#region Fields
		#endregion

		#region Properties
		#endregion
		
		#region CTOR
		public UcGroupInformation(BriefopManager briefopManager, BopGroup bopGroup, Mapsui.UI.WindowsForms.MapControl mapControl) : base(briefopManager, bopGroup, mapControl)
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

			CbMapMarker.SelectedValueChanged += CbMapMarker_SelectedValueChanged;
		}

		public override void DataToScreenMap()
		{
			if (!Visible)
				return;
				
			foreach (MemoryLayer mapLayer in m_mapControl.Map.Layers.OfType<MemoryLayer>().ToList())
				m_mapControl.Map.Layers.Remove(mapLayer);

			m_mapControl.Map.Layers.Add(m_bopGroup.GetPositionMapLayer());
			m_mapControl.Map.Layers.Add(m_bopGroup.GetRouteMapLayer(null, ElementMapOverlayRouteDisplay.NoMarkerFirstPoint, PreferencesManager.Preferences.Briefing.MeasurementSystem));

			MPoint center = MapProjection.ToMPoint(m_bopGroup.Coordinate.Latitude.DecimalDegree, m_bopGroup.Coordinate.Longitude.DecimalDegree);
			m_mapControl.Map.Navigator.CenterOnAndZoomTo(center, MapProjection.ZoomToResolution((int)PreferencesManager.Preferences.Map.Zoom), 0, null);
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
