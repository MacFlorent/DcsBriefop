using DcsBriefop.DataBop;

namespace DcsBriefop.FormBop
{
	internal partial class UcBopGeneral : UcBaseBop
	{
		public UcBopGeneral(UcMap ucMap, BopManager briefopManager) : base(ucMap, briefopManager)
		{
			InitializeComponent();
		}

		public override void DataToScreen()
		{
			TbSortie.Text = BopManager.BopMain.BopGeneral.Sortie;
			TbWeather.Text = BopManager.BopMain.BopGeneral.Weather.ToString();
			TbDescription.Text = BopManager.BopMain.BopGeneral.Description;
			DtpDate.Value = BopManager.BopMain.BopGeneral.Date;
		}
		public override void ScreenToData()
		{
			BopManager.BopMain.BopGeneral.Sortie = TbSortie.Text;
			BopManager.BopMain.BopGeneral.Description = TbDescription.Text;
			BopManager.BopMain.BopGeneral.Date = DtpDate.Value;
		}
	}
}
