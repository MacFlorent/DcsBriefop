using DcsBriefop.DataBop;
using DcsBriefop.DataBopCustom;
using DcsBriefop.Tools;

namespace DcsBriefop.FormBop
{
	internal partial class UcBopGeneral : UcBaseBop, ICustomStylable
	{
		#region Properties
		#endregion


		#region CTOR
		public UcBopGeneral(UcMap ucMap, BopManager bopManager) : base(ucMap, bopManager)
		{
			InitializeComponent();
		}
		#endregion

		#region ICustomStylable
		public void ApplyCustomStyle()
		{
			ToolsStyle.LabelHeader(LbWeather);
			ToolsStyle.LabelHeader(LbOperations);
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			RbWeatherDisplayPlain.CheckedChanged -= RbWeatherDisplay_CheckedChanged;
			RbWeatherDisplayPlain.CheckedChanged -= RbWeatherDisplay_CheckedChanged;

			TbSortie.Text = m_bopManager.BopMain.BopGeneral.Sortie;
			TbDescription.Text = m_bopManager.BopMain.BopGeneral.Description;
			DtpDate.Value = m_bopManager.BopMain.BopGeneral.Date;

			RbWeatherDisplayPlain.Checked = m_bopManager.BopCustomMain.WeatherDisplay == Data.ElementWeatherDisplay.Plain;
			RbWeatherDisplayMetar.Checked = m_bopManager.BopCustomMain.WeatherDisplay == Data.ElementWeatherDisplay.Metar;
			TbWeather.Text = m_bopManager.BopMain.BopGeneral.ToStringWeather();

			RbWeatherDisplayPlain.CheckedChanged += RbWeatherDisplay_CheckedChanged;
			RbWeatherDisplayPlain.CheckedChanged += RbWeatherDisplay_CheckedChanged;
		}
		public override void ScreenToData()
		{
			m_bopManager.BopMain.BopGeneral.Sortie = TbSortie.Text;
			m_bopManager.BopMain.BopGeneral.Description = TbDescription.Text;
			m_bopManager.BopMain.BopGeneral.Date = DtpDate.Value;

			m_bopManager.BopCustomMain.WeatherDisplay = RbWeatherDisplayMetar.Checked ? Data.ElementWeatherDisplay.Metar : Data.ElementWeatherDisplay.Plain;
		}

		public override BopCustomMap GetMapData()
		{
			return m_bopManager.BopMain.BopGeneral.MapData;
		}
		#endregion

		#region Events
		private void RbWeatherDisplay_CheckedChanged(object sender, System.EventArgs e)
		{
			m_bopManager.BopCustomMain.WeatherDisplay = RbWeatherDisplayMetar.Checked ? Data.ElementWeatherDisplay.Metar : Data.ElementWeatherDisplay.Plain;
			TbWeather.Text = m_bopManager.BopMain.BopGeneral.ToStringWeather();
		}
		#endregion
	}
}
