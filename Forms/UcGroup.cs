using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsPresentation;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal partial class UcGroup : UserControl
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private BopGroup m_bopGroup;
		private GridManagerUnits m_gridManagerUnits;
		#endregion

		#region Properties
		public BopGroup BopGroup
		{
			private get { return m_bopGroup; }
			set
			{
				m_bopGroup = value;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcGroup(BriefopManager briefopManager, BopGroup bopGroup)
		{
			m_briefopManager = briefopManager;
			m_bopGroup = bopGroup;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.LabelTitle(LbCoalition);
			ToolsStyle.LabelHeader(LbClass);

			MapTemplateMarker.FillCombo(CbMapMarker);

			MapControl.MapProvider = GMapProviders.TryGetProvider(m_briefopManager.BopMission.Miz.MizBopCustom.PreferencesMap.DefaultProvider);
			GMaps.Instance.Mode = AccessMode.ServerOnly;
			//Map.ShowTileGridLines = true;
			MapControl.Position = new PointLatLng(26.1702778, 56.24);
			MapControl.MinZoom = ElementMapValue.MinZoom;
			MapControl.MaxZoom = ElementMapValue.MaxZoom;
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_bopGroup.FinalizeFromMiz();

			LbCoalition.Text = $"{m_bopGroup.CoalitionName} | {m_bopGroup.CountryName}";
			LbCoalition.BackColor = ToolsBriefop.GetCoalitionColor(m_bopGroup.CoalitionName);
			LbClass.Text = $"{m_bopGroup.ObjectClass} | {m_bopGroup.DcsGroupType}";
			TbId.Text = m_bopGroup.Id.ToString();
			CkLateActivation.Checked = m_bopGroup.LateActivation;
			CkPlayable.Checked = m_bopGroup.Playable;
			TbName.Text = m_bopGroup.Name;
			TbDisplayName.Text = m_bopGroup.ToStringDisplayName();
			TbType.Text = m_bopGroup.Type;
			TbAttributes.Text = m_bopGroup.Attributes.ToString();
			TbRadio.Text = m_bopGroup.Radio?.ToString();
			TbOther.Text = m_bopGroup.ToStringAdditionnal();
			TbCoordinates.Text = m_bopGroup.ToStringLocalisation();

			CbMapMarker.Text = m_bopGroup.MapMarker;
			DataToScreenMap();

			m_gridManagerUnits = new GridManagerUnits(DgvUnits, m_bopGroup.Units);
			m_gridManagerUnits.SelectionChangedBopUnits += SelectionChangedBopUnitsEvent;
			m_gridManagerUnits.Initialize();
		}

		private void DataToScreenMap()
		{
			MapControl.Position = new PointLatLng(m_bopGroup.Coordinate.Latitude.DecimalDegree, m_bopGroup.Coordinate.Longitude.DecimalDegree);
			MapControl.Zoom = 8;

			MapControl.Overlays.Clear();
			MapControl.Overlays.Add(m_bopGroup.GetMapOverlayPosition());
			MapControl.Overlays.Add(m_bopGroup.GetMapOverlayRouteFull(false, false));
			MapControl.Refresh();
			MapControl.Zoom += 1; MapControl.Zoom -= 1;
		}

		public void ScreenToData()
		{
			m_bopGroup.MapMarker = CbMapMarker.Text;
		}
		#endregion

		#region Events
		private void SelectionChangedBopUnitsEvent(object sender, GridManagerUnits.EventArgsBopUnit e)
		{
			//DataToScreenUnit();
		}

		private void CbMapMarker_SelectionChangeCommitted(object sender, System.EventArgs e)
		{
			ScreenToData();
			DataToScreenMap();
		}
		#endregion
	}
}
