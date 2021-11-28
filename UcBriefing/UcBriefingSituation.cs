using DcsBriefop.Briefing;
using GMap.NET.WindowsForms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingSituation : UcBaseBriefing
	{
		public UcBriefingSituation(UcMap ucMap, BriefingPack bp) : base(ucMap, bp)
		{
			InitializeComponent();
		}

		public override void DataToScreen()
		{
			TbSortie.Text = BriefingPack.Sortie;
			TbWeather.Text = BriefingPack.Weather.ToString();
			TbDescription.Text = BriefingPack.Description;
			DtpDate.Value = BriefingPack.Date;
		}
		public override void ScreenToData()
		{
			BriefingPack.Sortie = TbSortie.Text;
			BriefingPack.Description = TbDescription.Text;
			BriefingPack.Date = DtpDate.Value;
		}
	}
}
