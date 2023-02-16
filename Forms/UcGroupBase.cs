using DcsBriefop.DataBopMission;
using GMap.NET.WindowsForms;

namespace DcsBriefop.Forms
{
	internal partial class UcGroupBase : UserControl
	{
		#region Fields
		protected BriefopManager m_briefopManager;
		protected BopGroup m_bopGroup;

		protected GMapControl m_mapControl;
		#endregion

		#region Properties
		public BopGroup BopGroup
		{
			protected get { return m_bopGroup; }
			set
			{
				m_bopGroup = value;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcGroupBase() { InitializeComponent(); }
		public UcGroupBase(BriefopManager briefopManager, BopGroup bopGroup, GMapControl mapControl)
		{
			m_briefopManager = briefopManager;
			m_bopGroup = bopGroup;
			m_mapControl = mapControl;
		}
		#endregion

		#region Methods
		public virtual void DataToScreen() { }
		public virtual void DataToScreenMap() { }
		public virtual void ScreenToData() { }
		#endregion
	}
}
