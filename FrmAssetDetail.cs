using DcsBriefop.Data;
using DcsBriefop.Tools;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmAssetDetail : Form
	{
		private static class GridColumn
		{
			public static readonly string Included = "Included";
			public static readonly string Id = "Id";
			public static readonly string Description = "Description";
			public static readonly string Type = "Type";
			public static readonly string Localisation = "Localisation";
			public static readonly string Information = "Information";
			public static readonly string Data = "Data";
		}

		#region Fields
		private Asset m_asset;
		#endregion

		#region CTOR
		internal FrmAssetDetail(Asset asset)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			m_asset = asset;

			MasterDataRepository.FillCombo(MasterDataType.AssetMapDisplay, CbMapDisplay);
			ToolsMisc.SetDataGridViewProperties(DgvUnits);

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			Text = $"Asset detail : {m_asset.Description}";

			TbDescription.Text = m_asset.Description;
			TbSide.Text = m_asset.Side.ToString();

			TbAssetClass.Text = m_asset.GetType().ToString();
			TbFunction.Text = m_asset.Function.ToString();
			CkIncluded.Checked = m_asset.Included;
			CbMapDisplay.SelectedValue = (int)m_asset.MapDisplay;
			TbInformation.Text = m_asset.Information;

			TbId.Text = m_asset.Id.ToString();
			TbName.Text = m_asset.Name;
			TbTask.Text = m_asset.Task;
			TbType.Text = m_asset.Type;

			CkPlayable.Visible = CkLateActivation.Visible = (m_asset is AssetGroup);
			AssetGroup group = m_asset as AssetGroup;
			if (group is object)
			{
				CkPlayable.Checked = group.Playable;
				CkLateActivation.Checked = group.LateActivation;
				TbRadio.Text = group.GetRadioString();
			}
			else if (m_asset is AssetAirdrome airdrome)
			{
				TbRadio.Text = airdrome.GetRadioString();
			}

			DataToScreenUnits();
		}

		private void DataToScreenUnits()
		{
			InitializeGridUnits();
			if (m_asset is AssetGroup group)
				foreach (AssetUnit unit in group.Units)
				{
					RefreshGridRowUnit(unit);
				}
		}

		private void InitializeGridUnits()
		{
			DgvUnits.Columns.Clear();
			DataGridViewCheckBoxColumn col = new DataGridViewCheckBoxColumn() { Name = GridColumn.Included, HeaderText = "Included" };
			DgvUnits.Columns.Add(col);
			DgvUnits.Columns.Add(GridColumn.Id, "ID");
			DgvUnits.Columns.Add(GridColumn.Type, "Type");
			DgvUnits.Columns.Add(GridColumn.Description, "Description");
			DgvUnits.Columns.Add(GridColumn.Localisation, "Localisation");
			DgvUnits.Columns.Add(GridColumn.Information, "Information");
			DgvUnits.Columns.Add(GridColumn.Data, "Data");

			DgvUnits.Columns[GridColumn.Included].ValueType = typeof(bool);
			DgvUnits.Columns[GridColumn.Type].ReadOnly = true;
			DgvUnits.Columns[GridColumn.Description].ReadOnly = true;
			DgvUnits.Columns[GridColumn.Localisation].ReadOnly = true;
			DgvUnits.Columns[GridColumn.Information].ReadOnly = true;
			DgvUnits.Columns[GridColumn.Data].Visible = false;
		}

		private void RefreshGridRowUnit(AssetUnit unit)
		{
			DataGridViewRow dgvr = null;
			foreach (DataGridViewRow existingRow in DgvUnits.Rows)
			{
				if (existingRow.Cells[GridColumn.Data].Value == unit)
				{
					dgvr = existingRow;
					break;
				}
			}
			if (dgvr is null)
			{
				int iNewRowIndex = DgvUnits.Rows.Add();
				dgvr = DgvUnits.Rows[iNewRowIndex];
				dgvr.Cells[GridColumn.Data].Value = unit;
			}

			dgvr.Cells[GridColumn.Included].Value = unit.Included;
			dgvr.Cells[GridColumn.Id].Value = unit.Id;
			dgvr.Cells[GridColumn.Type].Value = unit.Type;
			dgvr.Cells[GridColumn.Description].Value = unit.Description;
			dgvr.Cells[GridColumn.Localisation].Value = unit.GetLocalisation();
			dgvr.Cells[GridColumn.Information].Value = unit.Information;
		}

		private void ScreenToData()
		{
			m_asset.Included = CkIncluded.Checked;

			ElementAssetMapDisplay mapDisplay = (ElementAssetMapDisplay)CbMapDisplay.SelectedValue;
			if (mapDisplay != m_asset.MapDisplay)
			{
				m_asset.MapDisplay = mapDisplay;
				m_asset.InitializeMapOverlay();
			}

			m_asset.Information = TbInformation.Text;
		}
		#endregion

		#region Events
		private void BtInformationReset_Click(object sender, System.EventArgs e)
		{
			m_asset.Information = null;
			TbInformation.Text = m_asset.Information;
		}

		private void DgvUnits_CellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView grid = (sender as DataGridView);
			if (grid is null)
				return;

			DataGridViewColumn column = grid.Columns[e.ColumnIndex];
			if (column.Name == GridColumn.Included)
			{
				DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
				AssetUnit unit = grid.Rows[e.RowIndex].Cells[GridColumn.Data].Value as AssetUnit;
				unit.Included = (bool)cell.Value;
			}
		}

		private void DgvUnits_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridViewColumn column = (sender as DataGridView).Columns[e.ColumnIndex];
			if (column.Name == GridColumn.Included)
			{
				(sender as DataGridView).EndEdit();
			}
		}

		private void FrmAssetDetail_FormClosing(object sender, FormClosingEventArgs e)
		{
			ScreenToData();
		}
		#endregion
	}
}
