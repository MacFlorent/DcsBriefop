using DcsBriefop.DataBopBriefing;
using System.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerBriefingParts : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string PartName = "PartName";
			public static readonly string Information = "Information";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		private IEnumerable<BopBriefingPartBase> m_briefingParts;
		#endregion

		#region Properties
		public IEnumerable<BopBriefingPartBase> BriefingParts
		{
			get { return m_briefingParts; }
			set { m_briefingParts = value; }
		}
		#endregion

		#region CTOR
		public GridManagerBriefingParts(DataGridView dgv, IEnumerable<BopBriefingPartBase> briefingParts) : base(dgv)
		{
			m_briefingParts = briefingParts;
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.PartName, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Information, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(BopBriefingPartBase));

			RefreshDataSourceRows();
		}

		public void RefreshDataSourceRows()
		{
			foreach (BopBriefingPartBase element in m_briefingParts)
				RefreshDataSourceRow(element);
		}

		private void RefreshDataSourceRow(BopBriefingPartBase element)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<BopBriefingPartBase>(GridColumn.Data) == element).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, element);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.PartName, element.PartType);
			dr.SetField(GridColumn.Information, element.ToStringAdditional());
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				column.ReadOnly = true;
			}

			m_dgv.Columns[GridColumn.PartName].HeaderText = "Part name";
			m_dgv.Columns[GridColumn.Data].Visible = false;
		}

		public IEnumerable<BopBriefingPartBase> GetSelectedElements()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopBriefingPartBase>(GridColumn.Data)).ToList();
		}

		public void SelectElement(BopBriefingPartBase element)
		{
			DataGridViewRow rowToSelect = m_dgv.Rows.Cast<DataGridViewRow>().Where(_dgvr => (_dgvr.DataBoundItem as DataRowView)?.Row?.Field<BopBriefingPartBase>(GridColumn.Data) == element).FirstOrDefault();
			SelectRow(rowToSelect);
		}

		//protected override void SelectionChanged()
		//{
		//	SelectionChangedTyped?.Invoke(this, new EventArgsBopBriefingParts() { BopBriefingParts = GetSelectedElements() });
		//}
		#endregion

		#region Events
		//public class EventArgsBopBriefingParts : EventArgs
		//{
		//	public IEnumerable<BopBriefingPartBase> BopBriefingParts { get; set; }
		//}
		//public event EventHandler<EventArgsBopBriefingParts> SelectionChangedTyped;
		#endregion
	}
}
