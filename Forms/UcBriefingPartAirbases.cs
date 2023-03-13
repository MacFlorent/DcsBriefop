using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartAirbases : UcBriefingPartBase
	{
		#region Fields
		private GridManagerAirbases m_gmAvailable;
		private GridManagerAirbases m_gmSelected;
		List<BopAirbase> m_selectedAirbases;
		#endregion

		#region CTOR
		public UcBriefingPartAirbases(BopBriefingPartBase bopBriefingPart, BopMission bopMission, UcBriefingPage ucBriefingPageParent) : base(bopBriefingPart, bopMission, ucBriefingPageParent)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			BopBriefingPartAirbases briefingPart = m_bopBriefingPart as BopBriefingPartAirbases;

			m_gmAvailable = new GridManagerAirbases(UcMultiSelection.DgvAvailable, m_bopMission.Airbases);
			m_gmSelected = new GridManagerAirbases(UcMultiSelection.DgvSelected, m_bopMission.Airbases);

			UcMultiSelection.BtAdd.Click += BtAdd_Click;
			UcMultiSelection.BtRemove.Click += BtRemove_Click;
			UcMultiSelection.BtAddAll.Click += BtAddAll_Click;
			UcMultiSelection.BtRemoveAll.Click += BtRemoveAll_Click;
			UcMultiSelection.BtUp.Click += BtUp_Click;
			UcMultiSelection.BtDown.Click += BtDown_Click;

			LstColumns.DataSource = BopBriefingPartAirbases.AvailableColumns;

			DataToScreen();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			BopBriefingPartAirbases briefingPart = m_bopBriefingPart as BopBriefingPartAirbases;
			TbHeader.Text = briefingPart.Header;

			m_selectedAirbases = briefingPart.GetBopAirbases(m_bopMission);
			RefreshGrids();

			for (int i = 0; i < LstColumns.Items.Count; i++)
			{
				LstColumns.SetItemChecked(i, briefingPart.SelectedColumns.Contains(LstColumns.Items[i]));
			}
		}

		public override void ScreenToData()
		{
			BopBriefingPartAirbases briefingPart = m_bopBriefingPart as BopBriefingPartAirbases;
			briefingPart.Header = TbHeader.Text;

			briefingPart.SetBopAirbases(m_selectedAirbases);

			briefingPart.SelectedColumns.Clear();
			briefingPart.SelectedColumns.AddRange(LstColumns.CheckedItems.OfType<string>());
		}

		private void RefreshGrids()
		{
			m_gmAvailable.Elements = m_bopMission.Airbases.Where(_ba => !m_selectedAirbases.Contains(_ba));
			m_gmSelected.Elements = m_bopMission.Airbases.Where(_ba => m_selectedAirbases.Contains(_ba));
			m_gmAvailable.Initialize();
			m_gmSelected.Initialize();

		}
		#endregion

		#region Events
		private void CellEndEditEvent(object sender, EventArgs e)
		{
			m_ucBriefingPageParent?.DisplayCurrentMap();
		}

		private void BtAdd_Click(object sender, EventArgs e)
		{
			foreach (BopAirbase ba in m_gmAvailable.GetSelectedElements().Where(_ba => !m_selectedAirbases.Contains(_ba)))
				m_selectedAirbases.Add(ba);
		
			RefreshGrids();
		}

		private void BtRemove_Click(object sender, EventArgs e)
		{
			foreach (BopAirbase ba in m_gmSelected.GetSelectedElements())
				m_selectedAirbases.Remove(ba);

			RefreshGrids();
		}

		private void BtAddAll_Click(object sender, EventArgs e)
		{
			m_selectedAirbases.Clear();
			m_selectedAirbases.AddRange(m_bopMission.Airbases);
			RefreshGrids();
		}

		private void BtRemoveAll_Click(object sender, EventArgs e)
		{
			m_selectedAirbases.Clear();
			RefreshGrids();
		}

		private void BtUp_Click(object sender, EventArgs e)
		{

		}

		private void BtDown_Click(object sender, EventArgs e)
		{

		}
		#endregion
	}
}
