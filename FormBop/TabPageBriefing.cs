using System.Windows.Forms;

namespace DcsBriefop.FormBop
{
	internal class TabPageBriefop : TabPage
	{
		public UcBaseBop UcBriefop { get; private set; }

		public TabPageBriefop(string sText, UcBaseBop ucBriefop) : base(sText)
		{
			UcBriefop = ucBriefop;
			Controls.Clear();
			Controls.Add(UcBriefop);
			UcBriefop.Dock = DockStyle.Fill;
		}
	}
}
