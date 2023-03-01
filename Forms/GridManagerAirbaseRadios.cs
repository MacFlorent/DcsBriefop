using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using System.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerAirbaseRadios : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Radio = "Radio";
			public static readonly string Label = "Label";
			public static readonly string Default = "Default";
			public static readonly string Used = "Used";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		private IEnumerable<BopAirbaseRadio> m_airbaseRadios;
		#endregion

		#region Properties
		public IEnumerable<BopAirbaseRadio> AirbaseRadios
		{
			get { return m_airbaseRadios; }
			set { m_airbaseRadios = value; }
		}
		#endregion

		#region CTOR
		public GridManagerAirbaseRadios(DataGridView dgv, IEnumerable<BopAirbaseRadio> airbaseRadios) : base(dgv)
		{
			m_airbaseRadios = airbaseRadios;
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.Radio, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Label, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Default, typeof(bool));
			m_dtSource.Columns.Add(GridColumn.Used, typeof(bool));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(BopAirbaseRadio));

			foreach (BopAirbaseRadio bopAirbaseRadio in m_airbaseRadios)
				RefreshDataSourceRow(bopAirbaseRadio);
		}

		private void RefreshDataSourceRow(BopAirbaseRadio bopAirbaseRadio)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<BopAirbaseRadio>(GridColumn.Data) == bopAirbaseRadio).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, bopAirbaseRadio);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.Radio, bopAirbaseRadio.Radio?.ToString());
			dr.SetField(GridColumn.Label, bopAirbaseRadio.Label);
			dr.SetField(GridColumn.Default, bopAirbaseRadio.Default);
			dr.SetField(GridColumn.Used, bopAirbaseRadio.Used);
		}

		protected override void RefreshGridRow(DataGridViewRow dgvr)
		{
			BopAirbaseRadio bopAirbaseRadio = dgvr.Cells[GridColumn.Data].Value as BopAirbaseRadio;
			dgvr.Cells[GridColumn.Radio].ReadOnly = bopAirbaseRadio is null || bopAirbaseRadio.Default;
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				column.ReadOnly = true;
			}

			m_dgv.Columns[GridColumn.Data].Visible = false;
			m_dgv.Columns[GridColumn.Radio].ReadOnly = false;
			m_dgv.Columns[GridColumn.Label].ReadOnly = false;
			m_dgv.Columns[GridColumn.Used].ReadOnly = false;
		}

		public IEnumerable<BopAirbaseRadio> GetSelectedElements()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopAirbaseRadio>(GridColumn.Data)).ToList();
		}

		//protected override void SelectionChanged()
		//{
		//	SelectionChangedTyped?.Invoke(this, new EventArgsBopAirbaseRadios() { BopAirbaseRadios = GetSelectedElements() });
		//}

		protected override void CellEndEditInternal(DataGridView dgv, DataGridViewCell dgvc)
		{
			BopAirbaseRadio airbaseRadio = dgvc.OwningRow.Cells[GridColumn.Data].Value as BopAirbaseRadio;

			if (airbaseRadio is object)
			{
				if (dgvc.OwningColumn.Name == GridColumn.Used && (bool)dgvc.Value != airbaseRadio.Used)
				{
					airbaseRadio.Used = (bool)dgvc.Value;
				}
				else if (dgvc.OwningColumn.Name == GridColumn.Label && dgvc.Value as string != airbaseRadio.Label)
				{
					airbaseRadio.Label = dgvc.Value as string;
				}
				else if (dgvc.OwningColumn.Name == GridColumn.Radio)
				{
					Radio radio = Radio.NewFromString(dgvc.Value as string);
					if (radio is object && !radio.Equals(airbaseRadio.Radio))
						airbaseRadio.Radio = radio;

					RefreshDataSourceRow(airbaseRadio);
				}
			}
		}

		protected override void CellMouseUpInternal(DataGridView dgv, DataGridViewCell dgvc)
		{
			if (dgvc.OwningColumn.Name == GridColumn.Used)
				dgv.EndEdit();
		}
		#endregion

		#region Events
		//public class EventArgsBopAirbaseRadios : EventArgs
		//{
		//	public IEnumerable<BopAirbaseRadio> BopAirbaseRadios { get; set; }
		//}
		//public event EventHandler<EventArgsBopAirbaseRadios> SelectionChangedTyped;
		//public event EventHandler<EventArgsBopAirbaseRadios> ElementsModified;

		//protected override void AssignEvents()
		//{
		//	base.AssignEvents();
		//	m_dgv.CellEndEdit += CellEndEditEvent;
		//	m_dgv.CellMouseUp += CellMouseUpEvent;
		//}

		//protected override void RemoveEvents()
		//{
		//	base.RemoveEvents();
		//	m_dgv.CellEndEdit -= CellEndEditEvent;
		//	m_dgv.CellMouseUp -= CellMouseUpEvent;
		//}

		//private void CellEndEditEvent(object sender, DataGridViewCellEventArgs e)
		//{
		//	if (e.RowIndex < 0)
		//		return;

		//	DataGridView dgv = (sender as DataGridView);
		//	if (dgv is null)
		//		return;

		//	DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
		//	DataGridViewCell cell = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
		//	BopAirbaseRadio airbaseRadio = dgv.Rows[e.RowIndex].Cells[GridColumn.Data].Value as BopAirbaseRadio;

		//	if (airbaseRadio is object)
		//	{
		//		bool bFireEvent = false;

		//		if (column.Name == GridColumn.Used && (bool)cell.Value != airbaseRadio.Used)
		//		{
		//			airbaseRadio.Used = (bool)cell.Value;
		//			bFireEvent = true;
		//		}
		//		else if (column.Name == GridColumn.Label && cell.Value as string != airbaseRadio.Label)
		//		{
		//			airbaseRadio.Label = cell.Value as string;
		//			bFireEvent = true;
		//		}
		//		else if (column.Name == GridColumn.Radio)
		//		{
		//			Radio radio = Radio.NewFromString(cell.Value as string);
		//			if (radio is object && !radio.Equals(airbaseRadio.Radio))
		//				airbaseRadio.Radio = radio;

		//			RefreshDataSourceRow(airbaseRadio);
		//			bFireEvent = true;
		//		}

		//		if (bFireEvent)
		//			ElementsModified?.Invoke(this, new EventArgsBopAirbaseRadios() { BopAirbaseRadios = new List<BopAirbaseRadio>() { airbaseRadio } });
		//	}

		//}

		//private void CellMouseUpEvent(object sender, DataGridViewCellMouseEventArgs e)
		//{
		//	if (e.RowIndex < 0)
		//		return;

		//	DataGridViewColumn column = (sender as DataGridView).Columns[e.ColumnIndex];
		//	if (column.Name == GridColumn.Used)
		//	{
		//		(sender as DataGridView).EndEdit();
		//	}
		//}
		#endregion
	}
}
