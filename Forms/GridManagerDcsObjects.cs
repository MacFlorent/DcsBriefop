using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using System.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerDcsObjects : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Name = "Name";
			public static readonly string Kneeboard = "Kneeboard";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		private IEnumerable<DcsObject> m_dcsObjects;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerDcsObjects(DataGridView dgv, IEnumerable<DcsObject> elements) : base(dgv)
		{
			m_dcsObjects = elements;
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.Name, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Kneeboard, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(DcsObject));

			RefreshDataSourceRows();
		}

		public void RefreshDataSourceRows()
		{
			foreach (DcsObject element in m_dcsObjects)
				RefreshDataSourceRow(element);
		}

		private void RefreshDataSourceRow(DcsObject element)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<DcsObject>(GridColumn.Data) == element).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, element);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.Name, element.DisplayName);
			dr.SetField(GridColumn.Kneeboard, element.KneeboardFolder);
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

		public IEnumerable<BopBriefingPartBase> GetSelectedElements()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopBriefingPartBase>(GridColumn.Data)).ToList();
		}
		#endregion

		#region Events
		#endregion
	}
}
