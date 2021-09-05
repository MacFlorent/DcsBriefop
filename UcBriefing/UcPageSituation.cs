using DcsBriefop.Briefing;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcPageSituation : UcBaseBriefingPage
	{
		public UcPageSituation() : base()
		{
			InitializeComponent();
		}

		public override void DataToScreen()
		{
			if (BriefingPage is BriefingPageSituation bps)
			{
				DtpDate.Value = bps.Date;
				TbSituation.Text = bps.Description;
				TbTask.Text = bps.Task;
				TbWeather.Text = bps.Weather.ToString();
			}
		}

		public override void ScreenToData()
		{
			if (BriefingPage is BriefingPageSituation bps)
			{
				bps.Date = DtpDate.Value;
				bps.Description = TbSituation.Text;
				bps.Task = TbTask.Text;
			}
		}

		private void DtpDate_Validated(object sender, System.EventArgs e)
		{
			ScreenToData();
		}

		private void TbSituation_Validated(object sender, System.EventArgs e)
		{
			ScreenToData();
		}

		private void TbTask_Validated(object sender, System.EventArgs e)
		{
			ScreenToData();
		}
	}
}
