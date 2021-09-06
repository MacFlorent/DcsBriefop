using DcsBriefop.Briefing;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal class UcBaseBriefingPage : UserControl
	{
		public BaseBriefingPage BriefingPage { get; set; }

		public UcBaseBriefingPage()
		{
			BriefingPage = null;
		}

		public virtual void DataToScreen() { }
		public virtual void ScreenToData() { }
	}
}
