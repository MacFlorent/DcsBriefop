using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcGroup : UserControl
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private BopGroup m_bopGroup;

		private UcGroupInformation m_ucGroupInformation;
		private UcGroupUnits m_ucGroupUnits;
		private UcGroupRoutePoints m_ucGroupRoutePoints;
		#endregion

		#region Properties
		public BopGroup BopGroup
		{
			private get { return m_bopGroup; }
			set
			{
				m_bopGroup = value;
				m_ucGroupInformation.BopGroup = m_bopGroup;
				m_ucGroupUnits.BopGroup = m_bopGroup;
				m_ucGroupRoutePoints.BopGroup = m_bopGroup;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcGroup(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.LabelTitle(LbCoalition);
			ToolsStyle.LabelHeader(LbClass);
			ToolsStyle.LabelHeader(LbDisplayName);

			TcDetails.SelectedIndexChanged -= TcDetails_SelectedIndexChanged;
			m_ucGroupInformation = new UcGroupInformation(m_briefopManager, MapControl);
			m_ucGroupUnits = new UcGroupUnits(m_briefopManager, MapControl);
			m_ucGroupRoutePoints = new UcGroupRoutePoints(m_briefopManager, MapControl);
			TcDetails.TabPages.Clear();
			TcDetails.AddTab("Information", m_ucGroupInformation);
			TcDetails.AddTab("Units", m_ucGroupUnits);
			TcDetails.AddTab("Route", m_ucGroupRoutePoints);
			TcDetails.SelectedIndexChanged += TcDetails_SelectedIndexChanged;

			MapControl.InitializeMapControl(m_briefopManager.BopMission.Miz.MizBopCustom.PreferencesMap.ProviderName);
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_bopGroup.FinalizeFromMiz();

			LbCoalition.Text = $"{m_bopGroup.CoalitionName} | {m_bopGroup.CountryName}";
			LbCoalition.BackColor = ToolsBriefop.GetCoalitionColor(m_bopGroup.CoalitionName);
			LbClass.Text = $"{m_bopGroup.GroupClass} | {m_bopGroup.DcsGroupType}";
			LbDisplayName.Text = m_bopGroup.ToStringDisplayName();

			m_ucGroupInformation.DataToScreen();
			m_ucGroupUnits.DataToScreen();
			m_ucGroupRoutePoints.DataToScreen();

			DataToScreenMap();
		}

		private void DataToScreenMap()
		{
			UcGroupBase activeTabControl = null;
			if (TcDetails.SelectedTab is not null && TcDetails.SelectedTab.Controls.Count > 0)
				activeTabControl = TcDetails.SelectedTab.Controls[0] as UcGroupBase;

			activeTabControl?.DataToScreenMap();
		}

		public void ScreenToData()
		{
			m_ucGroupInformation.ScreenToData();
			m_ucGroupUnits.ScreenToData();
			m_ucGroupRoutePoints.ScreenToData();
		}
		#endregion

		#region Events
		private void TcDetails_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DataToScreenMap();
		}
		#endregion
	}
}
