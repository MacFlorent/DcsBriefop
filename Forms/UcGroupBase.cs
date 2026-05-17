using DcsBriefop.DataBopMission;
using Mapsui.UI.WindowsForms;

namespace DcsBriefop.Forms
{
	internal partial class UcGroupBase : UserControl
	{
		#region Fields
		protected BriefopManager m_briefopManager;
		protected BopGroup m_bopGroup;
		protected MapControl m_mapControl;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public UcGroupBase() { InitializeComponent(); }
		public UcGroupBase(BriefopManager briefopManager, BopGroup bopGroup, MapControl mapControl)
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
