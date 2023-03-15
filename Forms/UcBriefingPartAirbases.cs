using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartAirbases : UcBriefingPartBase
	{
		#region Fields
		private GridManagerAirbases m_gmMultiAvailable;
		private GridManagerAirbases m_gmMultiSelected;
		List<BopAirbase> m_selectedAirbases;
		#endregion

		#region CTOR
		public UcBriefingPartAirbases(BopBriefingPartBase bopBriefingPart, BopMission bopMission, UcBriefingPage ucBriefingPageParent) : base(bopBriefingPart, bopMission, ucBriefingPageParent)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			m_gmMultiAvailable = new GridManagerAirbases(DgvMultiAvailable, null);
			m_gmMultiSelected = new GridManagerAirbases(DgvMultiSelected, null);

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
			RefreshMultiGrids();

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

		private void RefreshMultiGrids()
		{
			m_gmMultiAvailable.Elements = m_bopMission.Airbases.Where(_ba => !m_selectedAirbases.Contains(_ba));
			m_gmMultiSelected.Elements = m_selectedAirbases;
			m_gmMultiAvailable.Initialize();
			m_gmMultiSelected.Initialize();
		}

		private void MultiAdd()
		{
			foreach (BopAirbase ba in m_gmMultiAvailable.GetSelectedElements().Where(_ba => !m_selectedAirbases.Contains(_ba)))
				m_selectedAirbases.Add(ba);

			RefreshMultiGrids();
		}

		private void MultiRemove()
		{
			foreach (BopAirbase ba in m_gmMultiSelected.GetSelectedElements())
				m_selectedAirbases.Remove(ba);

			RefreshMultiGrids();
		}

		private void MultiOrder(int iWay)
		{
			BopAirbase selectedElement = m_gmMultiSelected.GetSelectedElements().FirstOrDefault();
			if (selectedElement is not null)
			{
				MultiOrder(selectedElement, iWay);
				m_gmMultiSelected.Initialize();
				m_gmMultiSelected.SelectRow(selectedElement);
			}
		}

		private void MultiOrder(BopAirbase bopAirbase, int iWay)
		{
			if (iWay < 0)
				iWay = -1;
			else
				iWay = 1;

			int iCurrentIndex = m_selectedAirbases.IndexOf(bopAirbase);
			if (iCurrentIndex < 0)
				return;

			int iNewIndex = iCurrentIndex + iWay;
			if (iNewIndex >= 0 && iNewIndex < m_selectedAirbases.Count)
			{
				m_selectedAirbases.Remove(bopAirbase);
				m_selectedAirbases.Insert(iNewIndex, bopAirbase);
			}
		}
		#endregion

		#region Events
		private void BtMultiAdd_Click(object sender, EventArgs e)
		{
			MultiAdd();
		}

		private void BtMultiRemove_Click(object sender, EventArgs e)
		{
			MultiRemove();
		}

		private void BtMultiAddAll_Click(object sender, EventArgs e)
		{
			m_selectedAirbases.Clear();
			m_selectedAirbases.AddRange(m_bopMission.Airbases);
			RefreshMultiGrids();
		}

		private void BtMultiRemoveAll_Click(object sender, EventArgs e)
		{
			m_selectedAirbases.Clear();
			RefreshMultiGrids();
		}

		private void BtMultiUp_Click(object sender, EventArgs e)
		{
			MultiOrder(-1);
		}

		private void BtMultiDown_Click(object sender, EventArgs e)
		{
			MultiOrder(1);
		}

		private void DgvMultiAvailable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			MultiAdd();
		}

		private void DgvMultiSelected_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			MultiRemove();
		}
		#endregion
	}
}
