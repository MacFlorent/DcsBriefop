using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.MapProviders;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal partial class UcAirbase : UserControl
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private BopAirbase m_bopAirbase;
		private GridManagerAirbaseRadios m_gridManagerAirbaseRadios;
		#endregion

		#region Properties
		public BopAirbase BopAirbase
		{
			protected get { return m_bopAirbase; }
			set
			{
				m_bopAirbase = value;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcAirbase(BriefopManager briefopManager, BopAirbase bopAirbase)
		{
			m_briefopManager = briefopManager;
			m_bopAirbase = bopAirbase;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			m_gridManagerAirbaseRadios = new GridManagerAirbaseRadios(DgvRadios, m_bopAirbase.Radios);
			//m_gridManagerAirbases.SelectionChangedTyped += SelectionChangedTypedEvent;

			MapControl.InitializeMapControl();
			MapControl.MapProvider = GMapProviders.TryGetProvider(m_briefopManager.BopMission.Miz.MizBopCustom.PreferencesMap.DefaultProviderName);

			MapTemplateMarker.FillCombo(CbMapMarker, CbMapMarker_SelectedValueChanged);

			DataToScreen();
		}
		#endregion

		#region Methods
		public void DataToScreen()
		{
			CbMapMarker.SelectedValueChanged -= CbMapMarker_SelectedValueChanged;
			TbTacan.Validated -= TbTacan_Validated;

			m_bopAirbase.FinalizeFromMiz();

			TbId.Text = m_bopAirbase.Id.ToString();
			TbType.Text = m_bopAirbase.AirbaseType.ToString();
			TbTacan.Text = m_bopAirbase.Tacan?.ToString();
			TbName.Text = m_bopAirbase.Name;
			TbCoordinates.Text = m_bopAirbase.ToStringCoordinate();
			CbMapMarker.Text = m_bopAirbase.MapMarker;

			m_gridManagerAirbaseRadios.AirbaseRadios = m_bopAirbase.Radios;
			m_gridManagerAirbaseRadios.Initialize();
			
			DataToScreenMap();

			TbTacan.ReadOnly = (m_bopAirbase.AirbaseType == ElementAirbaseType.Ship);

			CbMapMarker.SelectedValueChanged += CbMapMarker_SelectedValueChanged;
			TbTacan.Validated += TbTacan_Validated;
		}

		private void DataToScreenMap()
		{
			MapControl.Overlays.Clear();
			MapControl.Overlays.Add(m_bopAirbase.GetMapOverlayPosition());
			MapControl.Position = new PointLatLng(m_bopAirbase.Coordinate.Latitude.DecimalDegree, m_bopAirbase.Coordinate.Longitude.DecimalDegree);
			MapControl.ForceRefresh();
		}

		public void ScreenToData()
		{
			m_bopAirbase.MapMarker = CbMapMarker.Text;
			m_bopAirbase.Tacan = Tacan.NewFromString(TbTacan.Text);
		}
		#endregion

		#region Events
		private void CbMapMarker_SelectedValueChanged(object sender, EventArgs e)
		{
			ScreenToData();
			DataToScreenMap();
		}

		private void TbTacan_Validated(object sender, EventArgs e)
		{
			Tacan tacan = Tacan.NewFromString(TbTacan.Text);
			TbTacan.Text = tacan?.ToString();
		}

		private void BtRadioAdd_Click(object sender, EventArgs e)
		{
			Radio radio = new Radio();
			radio.Normalize();
			m_bopAirbase.Radios.Add(new BopAirbaseRadio() { Radio = radio, Default = false, Used = true });
			m_gridManagerAirbaseRadios.Initialize();
		}

		private void BtRadioRemove_Click(object sender, EventArgs e)
		{
			BopAirbaseRadio airbaseRadio = m_gridManagerAirbaseRadios.GetSelected().FirstOrDefault();
			if (airbaseRadio is object && !airbaseRadio.Default)
				m_bopAirbase.Radios.Remove(airbaseRadio);

			m_gridManagerAirbaseRadios.Initialize();
		}

		#endregion
	}
}
