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
		public UcBriefingPartAirbases(BopBriefingPartBase bopBriefingPart, BopMission bopMission) : base(bopBriefingPart, bopMission)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			BopBriefingPartAirbases briefingPart = m_bopBriefingPart as BopBriefingPartAirbases;
			m_gridManagerAirbases = new GridManagerAirbases(DgvAirbases, m_bopMission.Airbases);
			m_gridManagerAirbases.CheckedIds = briefingPart.AirbasesIds;

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
	}
}
