using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartBullseye : UcBriefingPartBase
	{
		#region CTOR
		public UcBriefingPartBullseye(BaseBopBriefingPart bopBriefingPart, BopMission bopMission, UcBriefingPage ucBriefingPageParent) : base(bopBriefingPart, bopMission, ucBriefingPageParent)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			DataToScreen();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			BopBriefingPartBullseye briefingPart = m_bopBriefingPart as BopBriefingPartBullseye;
			CkWithDescription.Checked = briefingPart.WithDescription;
		}

		public override void ScreenToData()
		{
			BopBriefingPartBullseye briefingPart = m_bopBriefingPart as BopBriefingPartBullseye;
			briefingPart.WithDescription = CkWithDescription.Checked;
		}
		#endregion
	}
}
