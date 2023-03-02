using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartGroups : UcBriefingPartBase
	{
		#region Fields
		private GridManagerGroups m_gridManager;
		#endregion

		#region CTOR
		public UcBriefingPartGroups(BopBriefingPartBase bopBriefingPart, BopMission bopMission, UcBriefingPage ucBriefingPageParent) : base(bopBriefingPart, bopMission, ucBriefingPageParent)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			BopBriefingPartGroups briefingPart = m_bopBriefingPart as BopBriefingPartGroups;
			m_gridManager = new GridManagerGroups(DgvGroups, m_bopMission.Groups);
			m_gridManager.CheckedElements = briefingPart.Groups;
			m_gridManager.CellEndEdit += CellEndEditEvent;

			LstColumns.DataSource = BopBriefingPartGroups.AvailableColumns;

			DataToScreen();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			BopBriefingPartGroups briefingPart = m_bopBriefingPart as BopBriefingPartGroups;
			TbHeader.Text = briefingPart.Header;
			m_gridManager.Initialize();

			for (int i = 0; i < LstColumns.Items.Count; i++)
			{
				LstColumns.SetItemChecked(i, briefingPart.SelectedColumns.Contains(LstColumns.Items[i]));
			}
		}

		public override void ScreenToData()
		{
			BopBriefingPartGroups briefingPart = m_bopBriefingPart as BopBriefingPartGroups;
			briefingPart.Header = TbHeader.Text;

			briefingPart.SelectedColumns.Clear();
			briefingPart.SelectedColumns.AddRange(LstColumns.CheckedItems.OfType<string>());
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
