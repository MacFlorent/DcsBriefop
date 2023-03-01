using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartAirbases : UcBriefingPartBase
	{
		#region Fields
		private GridManagerAirbases m_gridManagerAirbases;
		#endregion

		#region CTOR
		public UcBriefingPartAirbases(BopBriefingPartBase bopBriefingPart, BopMission bopMission, UcBriefingPage ucBriefingPageParent) : base(bopBriefingPart, bopMission, ucBriefingPageParent)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			BopBriefingPartAirbases briefingPart = m_bopBriefingPart as BopBriefingPartAirbases;
			m_gridManagerAirbases = new GridManagerAirbases(DgvAirbases, m_bopMission.Airbases);
			m_gridManagerAirbases.CheckedElements = briefingPart.Airbases;
			m_gridManagerAirbases.CellEndEdit += CellEndEditEvent;

			DataToScreen();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			BopBriefingPartAirbases briefingPart = m_bopBriefingPart as BopBriefingPartAirbases;
			TbHeader.Text = briefingPart.Header;
			m_gridManagerAirbases.Initialize();
		}

		public override void ScreenToData()
		{
			BopBriefingPartAirbases briefingPart = m_bopBriefingPart as BopBriefingPartAirbases;
			briefingPart.Header = TbHeader.Text;
		}
		#endregion

		#region Events
		private void CellEndEditEvent(object sender, EventArgs e)
		{
			m_ucBriefingPageParent?.DisplayCurrentMap();
		}
		#endregion
	}
}
