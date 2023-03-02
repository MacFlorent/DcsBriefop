using DcsBriefop.DataBopBriefing;
using System.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerBriefingFolders : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Name = "Name";
			public static readonly string Coalition = "Coalition";
			public static readonly string AircraftTypes = "AircraftTypes";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		private IEnumerable<BopBriefingFolder> m_briefingFolders;
		#endregion

		#region Properties
		public IEnumerable<BopBriefingFolder> BriefingFolders
		{
			get { return m_briefingFolders; }
			set { m_briefingFolders = value; }
		}
		#endregion

		#region CTOR
		public GridManagerBriefingFolders(DataGridView dgv, IEnumerable<BopBriefingFolder> briefingFolders) : base(dgv)
		{
			m_briefingFolders = briefingFolders;
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.Name, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Coalition, typeof(string));
			m_dtSource.Columns.Add(GridColumn.AircraftTypes, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(BopBriefingFolder));

			foreach (BopBriefingFolder element in m_briefingFolders)
				RefreshDataSourceRow(element);
		}

		private void RefreshDataSourceRow(BopBriefingFolder element)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<BopBriefingFolder>(GridColumn.Data) == element).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, element);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.Name, element.Name);
			dr.SetField(GridColumn.Coalition, element.CoalitionName);
			//dr.SetField(GridColumn.AircraftTypes, string.Join(",", element.AircraftTypes));
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				column.ReadOnly = true;
			}

			m_dgv.Columns[GridColumn.Data].Visible = false;
		}

		public IEnumerable<BopBriefingFolder> GetSelectedElements()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopBriefingFolder>(GridColumn.Data)).ToList();
		}

		public void SelectElement(BopBriefingFolder element)
		{
			DataGridViewRow rowToSelect = m_dgv.Rows.Cast<DataGridViewRow>().Where(_dgvr => (_dgvr.DataBoundItem as DataRowView)?.Row?.Field<BopBriefingFolder>(GridColumn.Data) == element).FirstOrDefault();
			SelectRow(rowToSelect);
		}
		#endregion

		#region Events
		#endregion
	}
}
