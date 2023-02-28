using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartBase : UserControl
	{
		#region Fields
		protected BopBriefingPartBase m_bopBriefingPart;
		protected BopMission m_bopMission;
		#endregion

		#region Properties
		//public BopBriefingPart BopBriefingPart
		//{
		//	protected get { return m_bopBriefingPart; }
		//	set
		//	{
		//		m_bopBriefingPart = value;
		//		DataToScreen();
		//	}
		//}
		#endregion

		#region CTOR
		public UcBriefingPartBase() { InitializeComponent(); }
		public UcBriefingPartBase(BopBriefingPartBase bopBriefingPart, BopMission bopMission)
		{
			m_bopBriefingPart = bopBriefingPart;
			m_bopMission = bopMission;
		}
		#endregion

		#region Methods
		public virtual void DataToScreen() { }
		public virtual void ScreenToData() { }
		#endregion
	}
}
