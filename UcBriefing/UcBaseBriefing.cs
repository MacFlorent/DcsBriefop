using DcsBriefop.Briefing;
using GMap.NET.WindowsForms;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBaseBriefing : UserControl
	{
		protected GMapControl Map { get; private set; }
		public BriefingPack BriefingPack { get; set; }

		public UcBaseBriefing()
		{
			InitializeComponent();
		}

		public UcBaseBriefing(GMapControl map, BriefingPack bp)
		{
			BriefingPack = bp;
			Map = map;
		}

		public virtual void DataToScreen() { }
		public virtual void ScreenToData() { }
	}
}
