using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartParagraph : UcBriefingPartBase
	{
		#region CTOR
		public UcBriefingPartParagraph(BopBriefingPartBase bopBriefingPart, BopMission bopMission) : base(bopBriefingPart, bopMission)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			DataToScreen();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			BopBriefingPartParagraph briefingPart = m_bopBriefingPart as BopBriefingPartParagraph;
			TbHeader.Text = briefingPart.Header;
			TbText.Text = briefingPart.Text;
		}

		public override void ScreenToData()
		{
			BopBriefingPartParagraph briefingPart = m_bopBriefingPart as BopBriefingPartParagraph;
			briefingPart.Header = TbHeader.Text;
			briefingPart.Text = TbText.Text;
		}
		#endregion
	}
}
