using DcsBriefop.DataBop;
using System.Windows.Forms;

namespace DcsBriefop.FormBop
{
	internal partial class UcBaseBop : UserControl
	{
		protected UcMap UcMap { get; private set; }
		public BopManager BopManager { get; set; }

		public UcBaseBop()
		{
			InitializeComponent();
		}

		public UcBaseBop(UcMap ucMap, BopManager briefopManager)
		{
			BopManager = briefopManager;
			UcMap = ucMap;
		}

		public virtual void DataToScreen() { }
		public virtual void ScreenToData() { }
	}
}
