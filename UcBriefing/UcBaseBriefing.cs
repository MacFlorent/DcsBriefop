using DcsBriefop.Data;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBaseBriefing : UserControl
	{
		protected UcMap UcMap { get; private set; }
		public BriefingContainer BriefingContainer { get; set; }

		public UcBaseBriefing()
		{
			InitializeComponent();
		}

		public UcBaseBriefing(UcMap ucMap, BriefingContainer briefingContainer)
		{
			BriefingContainer = briefingContainer;
			UcMap = ucMap;
		}

		public virtual void DataToScreen() { }
		public virtual void ScreenToData() { }
	}
}
