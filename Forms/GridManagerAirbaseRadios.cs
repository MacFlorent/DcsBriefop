using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using System.Data;
using Zuby.ADGV;

namespace DcsBriefop.Forms
{
	internal class GridManagerAirbaseRadios : GridManagerBase<BopAirbaseRadio>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Radio = "Radio";
			public static readonly string Label = "Label";
			public static readonly string Default = "Default";
			public static readonly string Used = "Used";
		}
		#endregion

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerAirbaseRadios(AdvancedDataGridView dgv, IEnumerable<BopAirbaseRadio> airbaseRadios) : base(dgv, airbaseRadios) { }
		#endregion

		#region Methods
		protected override void InitializeDataSourceColumns()
		{
			base.InitializeDataSourceColumns();

			m_dtSource.Columns.Add(GridColumn.Radio, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Label, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Default, typeof(bool));
			m_dtSource.Columns.Add(GridColumn.Used, typeof(bool));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, BopAirbaseRadio element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			dr.SetField(GridColumn.Radio, element.Radio?.ToString());
			dr.SetField(GridColumn.Label, element.Label);
			dr.SetField(GridColumn.Default, element.Default);
			dr.SetField(GridColumn.Used, element.Used);
		}

		private void RefreshGridRows()
		{
			foreach (DataGridViewRow dgvr in m_dgv.Rows)
				RefreshGridRow(dgvr);
		}

		private void RefreshGridRow(DataGridViewRow dgvr)
		{
			BopAirbaseRadio bopAirbaseRadio = GetBoundElement(dgvr);
			dgvr.Cells[GridColumn.Radio].ReadOnly = bopAirbaseRadio is null || bopAirbaseRadio.Default;
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			m_dgv.Columns[GridColumn.Label].ReadOnly = false;
			m_dgv.Columns[GridColumn.Used].ReadOnly = false;

			RefreshGridRows();
		}

		protected override void CellEndEditInternal(DataGridView dgv, DataGridViewCell dgvc)
		{
			DataRow dr = GetBoundDataRow(dgvc.OwningRow);
			BopAirbaseRadio bopAirbaseRadio = GetBoundElement(dgvc.OwningRow);

			if (bopAirbaseRadio is object)
			{
				if (dgvc.OwningColumn.Name == GridColumn.Used && (bool)dgvc.Value != bopAirbaseRadio.Used)
				{
					bopAirbaseRadio.Used = (bool)dgvc.Value;
				}
				else if (dgvc.OwningColumn.Name == GridColumn.Label && dgvc.Value as string != bopAirbaseRadio.Label)
				{
					bopAirbaseRadio.Label = dgvc.Value as string;
				}
				else if (dgvc.OwningColumn.Name == GridColumn.Radio)
				{
					Radio radio = Radio.NewFromString(dgvc.Value as string);
					if (radio is object && !radio.Equals(bopAirbaseRadio.Radio))
						bopAirbaseRadio.Radio = radio;

					RefreshDataSourceRowContent(dr, bopAirbaseRadio);
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
