using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartWaypoints : UcBriefingPartBase
	{
		#region Fields
		#endregion

		#region CTOR
		public UcBriefingPartWaypoints(BaseBopBriefingPart bopBriefingPart, BopMission bopMission, UcBriefingPage ucBriefingPageParent) : base(bopBriefingPart, bopMission, ucBriefingPageParent)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			ToolsControls.FillCombo(CbGroup, m_bopMission.Groups, "Id", "Name", CbGroup_SelectedValueChanged);
			LstColumns.DataSource = BopBriefingPartWaypoints.AvailableTableColumns;

			DataToScreen();

		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			CbGroup.SelectedValueChanged -= CbGroup_SelectedValueChanged;

			BopBriefingPartWaypoints briefingPart = m_bopBriefingPart as BopBriefingPartWaypoints;
			TbHeader.Text = briefingPart.Header;
			CkDisplayGroupName.Checked = briefingPart.DisplayGroupName;
			CkIncludeBullseye.Checked = briefingPart.IncludeBullseye;
			CkDisplayGraph.Checked = briefingPart.DisplayGraph;
			CbGroup.SelectedValue = briefingPart.GroupId;

			for (int i = 0; i < LstColumns.Items.Count; i++)
			{
				LstColumns.SetItemChecked(i, briefingPart.SelectedTableColumns.Contains(LstColumns.Items[i]));
			}

			CbGroup.SelectedValueChanged += CbGroup_SelectedValueChanged;
		}

		public override void ScreenToData()
		{
			BopBriefingPartWaypoints briefingPart = m_bopBriefingPart as BopBriefingPartWaypoints;
			briefingPart.Header = TbHeader.Text;
			briefingPart.DisplayGroupName = CkDisplayGroupName.Checked;
			briefingPart.IncludeBullseye = CkIncludeBullseye.Checked;
			briefingPart.DisplayGraph = CkDisplayGraph.Checked;
			briefingPart.GroupId = (CbGroup.SelectedValue as int?).GetValueOrDefault(-1);

			briefingPart.SelectedTableColumns.Clear();
			briefingPart.SelectedTableColumns.AddRange(LstColumns.CheckedItems.OfType<string>());
		}

		private void RefreshMap()
		{
			ScreenToData();
			m_ucBriefingPageParent.DisplayCurrentMap();
		}
		#endregion

		#region Events
		private void CbGroup_SelectedValueChanged(object sender, EventArgs e)
		{
			RefreshMap();
		}
		#endregion
	}
}
