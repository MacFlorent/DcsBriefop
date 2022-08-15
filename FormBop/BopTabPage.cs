using System.Windows.Forms;

namespace DcsBriefop.FormBop
{
	internal class BopTabPage : TabPage
	{
		public UcBaseBop UcBop { get; private set; }

		public BopTabPage(string sText, UcBaseBop ucBaseBop) : base(sText)
		{
			UcBop = ucBaseBop;
			Controls.Clear();
			Controls.Add(UcBop);
			UcBop.Dock = DockStyle.Fill;
		}
	}
}
