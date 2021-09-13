using DcsBriefop.Briefing;
using GMap.NET.WindowsForms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingSituation : UcBaseBriefing
	{
		public UcBriefingSituation(GMapControl map, BriefingPack bp) : base(map, bp)
		{
			InitializeComponent();
		}

		public override void DataToScreen()
		{
			TbWeather.Text = BriefingPack.Weather.ToString();
			TbDescription.Text = BriefingPack.Description;
			DtpDate.Value = BriefingPack.Date;
		}
		public override void ScreenToData()
		{
			BriefingPack.Description = TbDescription.Text;
			BriefingPack.Date = DtpDate.Value;
		}
	}
}
