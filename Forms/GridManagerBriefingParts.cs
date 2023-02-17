using DcsBriefop.Data;
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
			m_dtSource.Columns.Add(GridColumn.Data, typeof(BopBriefingPartBase));

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

		public IEnumerable<BopBriefingPartBase> GetSelected()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopBriefingPartBase>(GridColumn.Data)).ToList();
		}

		protected override DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = base.CellFormatting(dgvc);
			return cellStyle;
		}

		protected override void SelectionChanged()
		{
			SelectionChangedTyped?.Invoke(this, new EventArgsBopBriefingParts() { BopBriefingParts = GetSelected() });
		}
		#endregion

		#region Events
		public class EventArgsBopBriefingParts : EventArgs
		{
			public IEnumerable<BopBriefingPartBase> BopBriefingParts { get; set; }
		}
		public event EventHandler<EventArgsBopBriefingParts> SelectionChangedTyped;
		#endregion
	}
}
