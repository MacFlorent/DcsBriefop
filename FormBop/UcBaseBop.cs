using DcsBriefop.DataBop;
using DcsBriefop.DataBopCustom;
using System.Windows.Forms;

namespace DcsBriefop.FormBop
{
	internal partial class UcBaseBop : UserControl
	{
		#region Fields
		protected UcMap m_ucMap;
		protected BopManager m_bopManager;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public UcBaseBop() { } // required for the designer only

		public UcBaseBop(UcMap ucMap, BopManager bopManager)
		{
			// no InitializeComponents in the base UserControl
			// each child must do its own

			m_ucMap = ucMap;
			m_bopManager = bopManager;
		}
		#endregion

		#region Methods
		public virtual void DataToScreen() { }
		public virtual void ScreenToData() { }

		public virtual BopCustomMap GetMapData() { return null; }

		public void SetUcMap(UcMap ucMap)
		{
			m_ucMap = ucMap;
		}

		public void SetBopManager(BopManager bopManager)
		{
			m_bopManager = bopManager;
		}
		#endregion
	}
}
