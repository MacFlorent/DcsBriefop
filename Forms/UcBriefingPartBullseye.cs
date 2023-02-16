using DcsBriefop.DataBopBriefing;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartBullseye : UcBriefingPartBase
	{
		#region CTOR
		public UcBriefingPartBullseye(BopBriefingPartBullseye bopBriefingPart) : base(bopBriefingPart)
		{
			InitializeComponent();

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
