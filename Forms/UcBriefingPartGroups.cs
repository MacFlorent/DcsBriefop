using DcsBriefop.DataBopBriefing;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class UcBriefingPartGroups : UcBriefingPartBase
	{
		#region Fields
		private GridManagerGroupOrUnits m_gmMultiAvailable;
		private GridManagerGroupOrUnits m_gmMultiSelected;
		List<BopGroupOrUnit> m_selectedGroupOrUnits;
		List<BopGroupOrUnit> m_missionGroupOrUnits;
		#endregion

		#region CTOR
		public UcBriefingPartGroups(BopBriefingPartBase bopBriefingPart, BopMission bopMission, UcBriefingPage ucBriefingPageParent) : base(bopBriefingPart, bopMission, ucBriefingPageParent)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			m_missionGroupOrUnits = m_bopMission.GetGroupOrUnits();

			m_gmMultiAvailable = new GridManagerGroupOrUnits(DgvMultiAvailable, null);
			m_gmMultiSelected = new GridManagerGroupOrUnits(DgvMultiSelected, null);

			LstColumns.DataSource = BopBriefingPartGroups.AvailableColumns;

			DataToScreen();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			BopBriefingPartGroups briefingPart = m_bopBriefingPart as BopBriefingPartGroups;
			TbHeader.Text = briefingPart.Header;

			m_selectedGroupOrUnits = briefingPart.GetBopGroupOrUnits(m_bopMission);
			RefreshMultiGrids();

			for (int i = 0; i < LstColumns.Items.Count; i++)
			{
				LstColumns.SetItemChecked(i, briefingPart.SelectedColumns.Contains(LstColumns.Items[i]));
			}
		}

		public override void ScreenToData()
		{
			BopBriefingPartGroups briefingPart = m_bopBriefingPart as BopBriefingPartGroups;
			briefingPart.Header = TbHeader.Text;

			briefingPart.SetBopGroupOrUnits(m_selectedGroupOrUnits);

			briefingPart.SelectedColumns.Clear();
			briefingPart.SelectedColumns.AddRange(LstColumns.CheckedItems.OfType<string>());
		}

		private void RefreshMultiGrids()
		{
			m_gmMultiAvailable.Elements = m_missionGroupOrUnits.Where(_gou => !m_selectedGroupOrUnits.Contains(_gou));
			m_gmMultiSelected.Elements = m_selectedGroupOrUnits;
			m_gmMultiAvailable.Initialize();
			m_gmMultiSelected.Initialize();
		}

		private void MultiAdd()
		{
			foreach (BopGroupOrUnit ba in m_gmMultiAvailable.GetSelectedElements().Where(_ba => !m_selectedGroupOrUnits.Contains(_ba)))
				m_selectedGroupOrUnits.Add(ba);

			RefreshMultiGrids();
		}

		private void MultiRemove()
		{
			foreach (BopGroupOrUnit ba in m_gmMultiSelected.GetSelectedElements())
				m_selectedGroupOrUnits.Remove(ba);

			RefreshMultiGrids();
		}

		private void MultiOrder(int iWay)
		{
			BopGroupOrUnit selectedElement = m_gmMultiSelected.GetSelectedElements().FirstOrDefault();
			if (selectedElement is not null)
			{
				MultiOrder(selectedElement, iWay);
				m_gmMultiSelected.Initialize();
				m_gmMultiSelected.SelectRow(selectedElement);
			}
		}

		private void MultiOrder(BopGroupOrUnit bopAirbase, int iWay)
		{
			if (iWay < 0)
				iWay = -1;
			else
				iWay = 1;

			int iCurrentIndex = m_selectedGroupOrUnits.IndexOf(bopAirbase);
			if (iCurrentIndex < 0)
				return;

			int iNewIndex = iCurrentIndex + iWay;
			if (iNewIndex >= 0 && iNewIndex < m_selectedGroupOrUnits.Count)
			{
				m_selectedGroupOrUnits.Remove(bopAirbase);
				m_selectedGroupOrUnits.Insert(iNewIndex, bopAirbase);
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
			m_selectedGroupOrUnits.Clear();
			m_selectedGroupOrUnits.AddRange(m_missionGroupOrUnits);
			RefreshMultiGrids();
		}

		private void BtMultiRemoveAll_Click(object sender, EventArgs e)
		{
			m_selectedGroupOrUnits.Clear();
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
