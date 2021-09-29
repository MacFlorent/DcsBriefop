using DcsBriefop.Briefing;
using GMap.NET.WindowsForms;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBaseBriefing : UserControl
	{
		protected UcMap UcMap { get; private set; }
		public BriefingPack BriefingPack { get; set; }

		public UcBaseBriefing()
		{
			InitializeComponent();
		}

		public UcBaseBriefing(UcMap ucMap, BriefingPack bp)
		{
			BriefingPack = bp;
			UcMap = ucMap;
		}

		public virtual void DataToScreen() { }
		public virtual void ScreenToData() { }
	}
}
