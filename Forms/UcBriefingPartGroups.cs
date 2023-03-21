using DcsBriefop.Data;
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
		#endregion

		#region CTOR
		public UcBriefingPartGroups(BopBriefingPartBase bopBriefingPart, BopMission bopMission, UcBriefingPage ucBriefingPageParent) : base(bopBriefingPart, bopMission, ucBriefingPageParent)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			MasterDataRepository.FillCombo(MasterDataType.BriefingPartGroupType, CbPartGroupType, CbPartGroupType_SelectedValueChanged);

			m_gmMultiAvailable = new GridManagerGroupOrUnits(DgvMultiAvailable, null);
			m_gmMultiSelected = new GridManagerGroupOrUnits(DgvMultiSelected, null);

			LstColumns.DataSource = BopBriefingPartGroups.AvailableHtmlColumns;

			DataToScreen();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			CbPartGroupType.SelectedValueChanged -= CbPartGroupType_SelectedValueChanged;

			BopBriefingPartGroups briefingPart = m_bopBriefingPart as BopBriefingPartGroups;
			TbHeader.Text = briefingPart.Header;
			CbPartGroupType.SelectedValue = (int)briefingPart.PartGroupType;
			DisplayCurrentPartGroupTypeGrid();

			m_selectedGroupOrUnits = briefingPart.GetBopGroupOrUnits(m_bopMission);
			m_gmMultiSelected.ColumnsDisplayed = new List<string>() { GridManagerGroupOrUnits.GridColumn.Id, GridManagerGroupOrUnits.GridColumn.DisplayName, GridManagerGroupOrUnits.GridColumn.Group, GridManagerGroupOrUnits.GridColumn.Type, GridManagerGroupOrUnits.GridColumn.ObjectClass };
			RefreshSelectedGrid();

			for (int i = 0; i < LstColumns.Items.Count; i++)
			{
				LstColumns.SetItemChecked(i, briefingPart.SelectedHtmlColumns.Contains(LstColumns.Items[i]));
			}

			CbPartGroupType.SelectedValueChanged += CbPartGroupType_SelectedValueChanged;
		}

		public override void ScreenToData()
		{
			BopBriefingPartGroups briefingPart = m_bopBriefingPart as BopBriefingPartGroups;
			briefingPart.Header = TbHeader.Text;
			briefingPart.PartGroupType = (ElementBriefingPartGroupType)CbPartGroupType.SelectedValue;

			briefingPart.SetBopGroupOrUnits(m_selectedGroupOrUnits);

			briefingPart.SelectedHtmlColumns.Clear();
			briefingPart.SelectedHtmlColumns.AddRange(LstColumns.CheckedItems.OfType<string>());
		}

		private void DisplayCurrentPartGroupTypeGrid()
		{
			ElementBriefingPartGroupType partGroupType = (ElementBriefingPartGroupType)CbPartGroupType.SelectedValue;

			IEnumerable<string> gridColumns;
			IEnumerable<BopGroupOrUnit> availableElements = m_bopMission.GetGroupOrUnits();

			if (partGroupType == ElementBriefingPartGroupType.GroupsOnly)
			{
				gridColumns = new List<string>() { GridManagerGroupOrUnits.GridColumn.Coalition, GridManagerGroupOrUnits.GridColumn.Id, GridManagerGroupOrUnits.GridColumn.DisplayName, GridManagerGroupOrUnits.GridColumn.Type, GridManagerGroupOrUnits.GridColumn.Attributes };
				availableElements = availableElements.Where(_gou => _gou.GroupOrUnit == ElementGroupOrUnit.Group).ToList();
			}
			else if (partGroupType == ElementBriefingPartGroupType.UnitsOnly)
			{
				gridColumns = new List<string>() { GridManagerGroupOrUnits.GridColumn.Coalition, GridManagerGroupOrUnits.GridColumn.Id, GridManagerGroupOrUnits.GridColumn.DisplayName, GridManagerGroupOrUnits.GridColumn.Group, GridManagerGroupOrUnits.GridColumn.Type, GridManagerGroupOrUnits.GridColumn.Attributes };
				availableElements = availableElements.Where(_gou => _gou.GroupOrUnit == ElementGroupOrUnit.Unit).ToList();
			}
			else
			{
				gridColumns = new List<string>() { GridManagerGroupOrUnits.GridColumn.Coalition, GridManagerGroupOrUnits.GridColumn.GroupOrUnit, GridManagerGroupOrUnits.GridColumn.Id, GridManagerGroupOrUnits.GridColumn.DisplayName, GridManagerGroupOrUnits.GridColumn.Group, GridManagerGroupOrUnits.GridColumn.Type, GridManagerGroupOrUnits.GridColumn.Attributes };
			}

			m_gmMultiAvailable.Elements = availableElements;
			m_gmMultiAvailable.ColumnsDisplayed = gridColumns.ToList();
			m_gmMultiAvailable.Refresh();
		}

		private void RefreshSelectedGrid()
		{
			m_gmMultiSelected.Elements = m_selectedGroupOrUnits;
			m_gmMultiSelected.Refresh();
		}

		private void MultiAdd()
		{
			foreach (BopGroupOrUnit ba in m_gmMultiAvailable.GetSelectedElements().Where(_ba => !m_selectedGroupOrUnits.Contains(_ba)))
				m_selectedGroupOrUnits.Add(ba);

			RefreshSelectedGrid();
			RefreshMap();
		}

		private void MultiRemove()
		{
			foreach (BopGroupOrUnit ba in m_gmMultiSelected.GetSelectedElements())
				m_selectedGroupOrUnits.Remove(ba);

			RefreshSelectedGrid();
			RefreshMap();
		}

		private void MultiOrder(int iWay)
		{
			BopGroupOrUnit selectedElement = m_gmMultiSelected.GetSelectedElements().FirstOrDefault();
			if (selectedElement is not null)
			{
				MultiOrder(selectedElement, iWay);
				m_gmMultiSelected.Refresh();
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

		private void RefreshMap()
		{
			ScreenToData();
			m_ucBriefingPageParent.DisplayCurrentMap();
		}
		#endregion

		#region Events
		private void CbPartGroupType_SelectedValueChanged(object sender, EventArgs e)
		{
			DisplayCurrentPartGroupTypeGrid();
		}

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
			//m_selectedGroupOrUnits.Clear();
			//m_selectedGroupOrUnits.AddRange(m_missionGroupOrUnits);
			//RefreshMultiGrids();
			//RefreshMap();
		}

		private void BtMultiRemoveAll_Click(object sender, EventArgs e)
		{
			//m_selectedGroupOrUnits.Clear();
			//RefreshMultiGrids();
			//RefreshMap();
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
