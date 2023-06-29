using DcsBriefop.DataBopBriefing;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartTableText : UcBriefingPartBase
	{
		#region CTOR
		public UcBriefingPartTableText(BaseBopBriefingPart bopBriefingPart, BriefopManager bopManager, UcBriefingPage ucBriefingPageParent) : base(bopBriefingPart, bopManager, ucBriefingPageParent)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			DataToScreen();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			BopBriefingPartTableText briefingPart = m_bopBriefingPart as BopBriefingPartTableText;
			CkBullseye.Checked = briefingPart.Bullseye;
			CkBullseyeWithDescription.Checked = briefingPart.BullseyeWithDescription;
			CkWeather.Checked = briefingPart.Weather;
		}

		public override void ScreenToData()
		{
			BopBriefingPartTableText briefingPart = m_bopBriefingPart as BopBriefingPartTableText;
			briefingPart.Bullseye = CkBullseye.Checked;
			briefingPart.BullseyeWithDescription = CkBullseyeWithDescription.Checked;
			briefingPart.Weather = CkWeather.Checked;
		}
		#endregion
	}
}
