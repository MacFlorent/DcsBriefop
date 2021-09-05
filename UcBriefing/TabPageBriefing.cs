using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal class TabPageBriefing : TabPage
	{
		public UcBaseBriefingPage UcBriefingPage { get; private set; }

		public TabPageBriefing(UcBaseBriefingPage uc)
		{
			UcBriefingPage = uc;
			Text = UcBriefingPage.BriefingPage.Label;

			Controls.Clear();
			Controls.Add(UcBriefingPage);
			UcBriefingPage.Dock = DockStyle.Fill;
		}
	}
}
