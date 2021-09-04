using DcsBriefop.Briefing;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcSituation : UserControl
	{
		public BriefingPageSituation PageSituation { get; set; }

		public UcSituation()
		{
			InitializeComponent();

			PageSituation = null;
		}

		public void DataToScreen()
		{
			DtpDate.Value = PageSituation.Date;
			TbSituation.Text = PageSituation.Description;
			TbTask.Text = PageSituation.Task;
			TbWeather.Text = PageSituation.Weather.ToString();
		}

		public void ScreenToData()
		{
			PageSituation.Date = DtpDate.Value;
			PageSituation.Description = TbSituation.Text;
			PageSituation.Task = TbTask.Text;
		}

		private void TbSituation_Validated(object sender, System.EventArgs e)
		{
			ScreenToData();
		}

		private void DtpDate_Validated(object sender, System.EventArgs e)
		{
			ScreenToData();
		}
	}
}
