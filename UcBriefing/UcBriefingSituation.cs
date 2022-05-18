using DcsBriefop.Data;
using GMap.NET.MapProviders;
using System.Linq;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingSituation : UcBaseBriefing
	{
		public UcBriefingSituation(UcMap ucMap, BriefingContainer briefingContainer) : base(ucMap, briefingContainer)
		{
			InitializeComponent();
		}

		public override void DataToScreen()
		{
			TbSortie.Text = BriefingContainer.Mission.Sortie;
			TbWeather.Text = BriefingContainer.Mission.Weather.ToString();
			TbDescription.Text = BriefingContainer.Mission.Description;
			DtpDate.Value = BriefingContainer.Mission.Date;
		}
		public override void ScreenToData()
		{
			BriefingContainer.Mission.Sortie = TbSortie.Text;
			BriefingContainer.Mission.Description = TbDescription.Text;
			BriefingContainer.Mission.Date = DtpDate.Value;
		}
	}
}
