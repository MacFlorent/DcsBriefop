using DcsBriefop.Briefing;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal abstract class UcBaseBriefingPage : UserControl
	{
		public BaseBriefingPage BriefingPage { get; set; }

		public UcBaseBriefingPage()
		{
			BriefingPage = null;
		}

		public abstract void DataToScreen();
		public abstract void ScreenToData();
	}
}
