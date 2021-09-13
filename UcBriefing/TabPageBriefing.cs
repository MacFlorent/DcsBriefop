using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal class TabPageBriefing : TabPage
	{
		public UcBaseBriefing UcBriefing { get; private set; }

		public TabPageBriefing(string sText, UcBaseBriefing ucbb) : base(sText)
		{
			UcBriefing = ucbb;
			Controls.Clear();
			Controls.Add(ucbb);
			ucbb.Dock = DockStyle.Fill;
		}
	}
}
