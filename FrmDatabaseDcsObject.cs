using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Data;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmDatabaseDcsObject : Form
	{
		#region Columns
		private static class GridColumn
		{
			public static readonly string Type = "Type";
			public static readonly string Description = "Description";
			public static readonly string Class = "Class";
			public static readonly string Attributes = "Attributes";
			public static readonly string MapMarker = "MapMarker";
			public static readonly string Information = "Information";
			public static readonly string MainInGroup = "MainInGroup";
			public static readonly string KneeboardFolder = "KneeboardFolder";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		DataTable m_dtGridSource;
		#endregion

		#region CTOR
		public FrmDatabaseDcsObject()
		{
			InitializeComponent();

			AdgvDatabase.SetDataGridViewProperties();
			AdgvDatabase.AutoGenerateColumns = false;

			ToolsStyle.ApplyStyle(this);
			ToolsStyle.ButtonOk(BtSave);
			ToolsStyle.ButtonCancel(BtClose);

			InitializeGridSource();
			DataToScreen();
		}
		#endregion

		#region Methods
		private void InitializeGridSource()
		{
			m_dtGridSource = new DataTable();
			m_dtGridSource.Columns.Add(GridColumn.Type, typeof(string));
			m_dtGridSource.Columns.Add(GridColumn.Description, typeof(string));
			m_dtGridSource.Columns.Add(GridColumn.Class, typeof(string));
			m_dtGridSource.Columns.Add(GridColumn.Attributes, typeof(string));
			m_dtGridSource.Columns.Add(GridColumn.MapMarker, typeof(string));
			m_dtGridSource.Columns.Add(GridColumn.Information, typeof(string));
			m_dtGridSource.Columns.Add(GridColumn.MainInGroup, typeof(bool));
			m_dtGridSource.Columns.Add(GridColumn.KneeboardFolder, typeof(string));
			m_dtGridSource.Columns.Add(GridColumn.Data, typeof(DcsObject));

			foreach (DcsObject dcsObject in DcsObjectManager.DcsObjects)
			{
				DataRow dr = m_dtGridSource.NewRow();
				dr.SetField(GridColumn.Data, dcsObject);
				ObjectToDataRow(dr);
				m_dtGridSource.Rows.Add(dr);
			}
		}

		private void ObjectToDataRow(DataRow dr)
		{
			DcsObject dcsObject = dr.Field<DcsObject>(GridColumn.Data);
			dr.SetField(GridColumn.Type, dcsObject.TypeName);
			dr.SetField(GridColumn.Description, dcsObject.Description);
			dr.SetField(GridColumn.Class, dcsObject.Class.ToString());
			dr.SetField(GridColumn.Attributes, dcsObject.Attributes.ToString());
			dr.SetField(GridColumn.MapMarker, dcsObject.CustomMapMarker);
			dr.SetField(GridColumn.Information, dcsObject.Information);
			dr.SetField(GridColumn.MainInGroup, dcsObject.MainInGroup);
			dr.SetField(GridColumn.KneeboardFolder, dcsObject.KneeboardFolder);
		}

		private void DataRowToObject(DataRow dr)
		{
			DcsObject dcsObject = dr.Field<DcsObject>(GridColumn.Data);
			dcsObject.Description = dr.Field<string>(GridColumn.Description);
			dcsObject.CustomMapMarker = dr.Field<string>(GridColumn.MapMarker);
			dcsObject.Information = dr.Field<string>(GridColumn.Information);
			dcsObject.MainInGroup = dr.Field<bool>(GridColumn.MainInGroup);
			dcsObject.KneeboardFolder = dr.Field<string>(GridColumn.KneeboardFolder);
		}

		private void InitializeGridColumns()
		{
			AdgvDatabase.Columns.Clear();

			AdgvDatabase.AddColumn<string>(GridColumn.Type, "Type").ReadOnly = true;
			AdgvDatabase.AddColumn<string>(GridColumn.Description, "Description");
			AdgvDatabase.AddColumn<string>(GridColumn.Class, "Class").ReadOnly = true;
			AdgvDatabase.AddColumn<string>(GridColumn.Attributes, "Attributes").ReadOnly = true;
			AdgvDatabase.AddColumn<string>(GridColumn.MapMarker, "Map marker");
			AdgvDatabase.AddColumn<string>(GridColumn.Information, "Information");
			AdgvDatabase.AddColumn<bool>(GridColumn.MainInGroup, "Main in group");
			AdgvDatabase.AddColumn<string>(GridColumn.KneeboardFolder, "KneeboardFolder");
		}

		private void DataToScreen()
		{
			AdgvDatabase.CellValueChanged -= AdgvDatabase_CellValueChanged;

			InitializeGridColumns();
			AdgvDatabase.SetDataSource(m_dtGridSource);

			AdgvDatabase.CellValueChanged += AdgvDatabase_CellValueChanged;
		}

		private void ScreenToData()
		{
			foreach(DataRow dr in m_dtGridSource.Rows)
				DataRowToObject(dr);
		}
		#endregion

		#region Events
		private void BtSave_Click(object sender, EventArgs e)
		{
			using (new WaitDialog(this))
			{
				ScreenToData();
				DcsObjectManager.SaveJsonCustom();
			}
		}

		private void BtClose_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		private void AdgvDatabase_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView dgv = (sender as DataGridView);
			if (dgv is null)
				return;

			DataRow dr = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)?.Row;
			if (dr is object)
			{
				DcsObject dcsObject = dr.Field<DcsObject>(GridColumn.Data);
				if (string.IsNullOrEmpty(dr.Field<string>(GridColumn.Description)))
					dr.SetField(GridColumn.Description, dcsObject.Description);
			}
		}
	}
}
