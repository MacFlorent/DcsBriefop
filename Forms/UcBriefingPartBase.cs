using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartBase : UserControl
	{
		#region Fields
		protected BaseBopBriefingPart m_bopBriefingPart;
		protected BopMission m_bopMission;
		protected UcBriefingPage m_ucBriefingPageParent;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public UcBriefingPartBase() { InitializeComponent(); }
		public UcBriefingPartBase(BaseBopBriefingPart bopBriefingPart, BopMission bopMission, UcBriefingPage ucBriefingPageParent)
		{
			m_bopBriefingPart = bopBriefingPart;
			m_bopMission = bopMission;
			m_ucBriefingPageParent = ucBriefingPageParent;
		}
		#endregion

		#region Methods
		public virtual void DataToScreen() { }
		public virtual void ScreenToData() { }
		#endregion
	}
}
