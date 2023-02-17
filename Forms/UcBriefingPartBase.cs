using DcsBriefop.DataBopBriefing;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartBase : UserControl
	{
		#region Fields
		protected BopBriefingPartBase m_bopBriefingPart;
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
		public UcBriefingPartBase(BopBriefingPartBase bopBriefingPart)
		{
			m_bopBriefingPart = bopBriefingPart;
		}
		#endregion

		#region Methods
		public virtual void DataToScreen() { }
		public virtual void ScreenToData() { }
		#endregion
	}
}
