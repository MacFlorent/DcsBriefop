using DcsBriefop.Data;
using DcsBriefop.DataBopBriefing;
using System.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerBriefingPages : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Title = "Title";
			public static readonly string Render = "Render";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		private IEnumerable<BopBriefingPage> m_briefingPages;
		#endregion

		#region Properties
		public IEnumerable<BopBriefingPage> BriefingPages
		{
			get { return m_briefingPages; }
			set { m_briefingPages = value; }
		}
		#endregion

		#region CTOR
		public GridManagerBriefingPages(DataGridView dgv, IEnumerable<BopBriefingPage> briefingPages) : base(dgv)
		{
			m_briefingPages = briefingPages;
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.Title, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Render, typeof(ElementBriefingPageRender));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(BopBriefingPage));

			RefreshDataSourceRows();
		}

		public void RefreshDataSourceRows()
		{
			foreach (BopBriefingPage element in m_briefingPages)
				RefreshDataSourceRow(element);
		}

		private void RefreshDataSourceRow(BopBriefingPage element)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<BopBriefingPage>(GridColumn.Data) == element).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, element);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.Title, element.Title);
			dr.SetField(GridColumn.Render, element.Render);
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

		public IEnumerable<BopBriefingPage> GetSelectedElements()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopBriefingPage>(GridColumn.Data)).ToList();
		}

		public void SelectElement(BopBriefingPage element)
		{
			DataGridViewRow rowToSelect = m_dgv.Rows.Cast<DataGridViewRow>().Where(_dgvr => (_dgvr.DataBoundItem as DataRowView)?.Row?.Field<BopBriefingPage>(GridColumn.Data) == element).FirstOrDefault();
			SelectRow(rowToSelect);
		}
		#endregion

		#region Events
		#endregion
	}
}
