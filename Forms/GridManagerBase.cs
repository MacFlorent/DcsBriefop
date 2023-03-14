using DcsBriefop.Tools;
using System.ComponentModel;
using System.Data;

namespace DcsBriefop.Forms
{
	internal abstract class GridManagerBase<T> : IDisposable where T : class
	{
		#region Columns
		public static class GridColumnBase
		{
			public static readonly string Checked = "Checked";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		protected DataGridView m_dgv;
		protected DataTable m_dtSource;
		protected bool m_disposedValue;

		#endregion

		#region Properties
		public List<string> ColumnsDisplayed { get; set; } = null;
		public IEnumerable<T> Elements { get; set; }
		public List<T> CheckedElements { get; set; }
		#endregion

		#region CTOR
		public GridManagerBase(DataGridView dgv, IEnumerable<T> elements)
		{
			m_dgv = dgv;
			Elements = elements;

			m_dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			m_dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
			m_dgv.AllowUserToResizeColumns = true;
			m_dgv.AllowUserToAddRows = false;
			m_dgv.RowHeadersVisible = false;

			m_dgv.AutoGenerateColumns = true;
			m_dgv.DataSource = new BindingSource();

			AssignEvents();
		}
		#endregion

		#region Methods
		public void Initialize()
		{
			InitializeDataSource();
			SetDataSource();
			PostInitializeColumns();
			InitializeContextMenu();
		}

		private void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			InitializeDataSourceColumns();
			RefreshDataSourceRows();
		}

		protected virtual void InitializeDataSourceColumns()
		{
			m_dtSource.Columns.Add(GridColumnBase.Data, typeof(T));
			m_dtSource.Columns.Add(GridColumnBase.Checked, typeof(bool));
		}

		public void RefreshDataSourceRows()
		{
			foreach (T element in Elements)
				RefreshDataSourceRow(element);
		}

		protected void RefreshDataSourceRow(T element)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<T>(GridColumnBase.Data).Equals(element)).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumnBase.Data, element);
				m_dtSource.Rows.Add(dr);
			}

			RefreshDataSourceRowContent(dr, element);
		}

		protected virtual void RefreshDataSourceRowContent(DataRow dr, T element)
		{
			dr.SetField(GridColumnBase.Checked, CheckedElements is not null && CheckedElements.Contains(element));
		}

		protected void SetDataSource()
		{
			try
			{
				RemoveEvents();
				m_dgv.SuspendDrawing();
				m_dgv.ColumnHeadersHeight = 25; // not ideal, but if the header is to narrow, it will be widened by AdvancedDataGridView.OnColumnAdded, and sometimes it will cause problems that I don't understand
				(m_dgv.DataSource as BindingSource).DataSource = m_dtSource.DefaultView;
				m_dgv.ResumeDrawing();
				AssignEvents();
			}
			catch (Exception ex) { ToolsControls.ShowMessageBoxError(ex.Message); } // to check the problem addressed by "m_dgv.ColumnHeadersHeight = 25"
		}

		protected virtual void PostInitializeColumns()
		{
			if (ColumnsDisplayed is not null)
			{
				foreach (DataGridViewColumn column in m_dgv.Columns)
				{
					if (!ColumnsDisplayed.Contains(column.DataPropertyName))
						column.Visible = false;
					else
						column.DisplayIndex = ColumnsDisplayed.IndexOf(column.DataPropertyName);
				}
			}

			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				column.ReadOnly = true;
			}

			m_dgv.Columns[GridColumnBase.Data].Visible = false;
			m_dgv.Columns[GridColumnBase.Checked].ReadOnly = false;
			m_dgv.Columns[GridColumnBase.Checked].Visible = CheckedElements is not null;
		}

		//public void RefreshGridRows()
		//{
		//	foreach (DataGridViewRow dgvr in m_dgv.Rows)
		//		RefreshGridRow(dgvr);
		//}

		//public virtual void RefreshGridRow(DataGridViewRow dgvr) { }

		//protected void ReplaceColumnWithComboBox(string sColumnName, string sHeaderText, object dataSource, string sValueMember, string sDisplayMember)
		//{
		//	if (!m_dgv.Columns.Contains(sColumnName))
		//		return;

		//	DataGridViewColumn dgvc = m_dgv.Columns[sColumnName];
		//	int iDisplayIndex = dgvc.DisplayIndex;
		//	m_dgv.Columns.Remove(dgvc);

		//	DataGridViewComboBoxColumn dgvcComboBox = new DataGridViewComboBoxColumn() { Name = sColumnName, DataPropertyName = sColumnName, HeaderText = sHeaderText };
		//	if (dataSource is object)
		//	{
		//		dgvcComboBox.DataSource = dataSource;
		//		dgvcComboBox.ValueMember = sValueMember;
		//		dgvcComboBox.DisplayMember = sDisplayMember;
		//	}

		//	dgvcComboBox.DisplayIndex = iDisplayIndex;
		//	m_dgv.Columns.Add(dgvcComboBox);
		//}

		private DataRow GetBoundDataRow(DataGridViewRow dgvr)
		{
			return (dgvr?.DataBoundItem as DataRowView)?.Row;
		}

		protected T GetBoundElement(DataGridViewRow dgvr)
		{
			return GetBoundDataRow(dgvr)?.Field<T>(GridColumnBase.Data);
		}

		private void SelectCell(DataGridViewCell dgvc)
		{
			if (dgvc is not null)
			{
				dgvc.Selected = true;
				m_dgv.CurrentCell = dgvc;
			}
		}

		private void SelectRow(DataGridViewRow dgvr)
		{
			if (dgvr is not null)
			{
				dgvr.Selected = true;
				DataGridViewCell dgvc = dgvr.Cells.OfType<DataGridViewCell>().Where(_c => _c.Visible).FirstOrDefault();
				SelectCell(dgvc);
			}
		}

		public void SelectRow(T element)
		{
			DataGridViewRow dgvr = m_dgv.Rows.Cast<DataGridViewRow>()
				.Where(_dgvr => (GetBoundElement(_dgvr)?.Equals(element)).GetValueOrDefault(false))
				.FirstOrDefault();

			SelectRow(dgvr);
		}

		private IEnumerable<DataGridViewRow> GetSelectedRows()
		{
			return m_dgv.SelectedCells.Cast<DataGridViewCell>().Select(_dgvc => _dgvc.OwningRow).Distinct();
		}

		private IEnumerable<DataRow> GetSelectedDataRows()
		{
			return GetSelectedRows().Select(_dgvr => GetBoundDataRow(_dgvr)).Where(_dr => _dr is object);
		}

		public IEnumerable<T> GetSelectedElements()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<T>(GridColumnBase.Data));
		}

		protected virtual DataGridViewCellStyle CellFormattingInternal(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = dgvc.InheritedStyle;
			return cellStyle;
		}

		protected virtual void SelectionChangedInternal() { }

		protected virtual void CellEndEditInternal(DataGridView dgv, DataGridViewCell dgvc)
		{
			T element = GetBoundElement(dgvc.OwningRow);
			if (element is not null && CheckedElements is not null && dgvc.OwningColumn.Name == GridColumnBase.Checked)
			{
				if ((bool)dgvc.Value)
				{
					if (!CheckedElements.Contains(element))
						CheckedElements.Add(element);
				}
				else
				{
					while (CheckedElements.Remove(element)) ;
				}
			} 
		}

		protected virtual void CellMouseUpInternal(DataGridView dgv, DataGridViewCell dgvc)
		{
			if (dgvc.OwningColumn.Name == GridColumnBase.Checked)
				dgv.EndEdit();
		}
		#endregion

		#region Menus
		private void InitializeContextMenu()
		{
			m_dgv.ContextMenuStrip = new ContextMenuStrip();
			m_dgv.ContextMenuStrip.Opening += (object sender, CancelEventArgs e) => { ContextMenuOpening(sender as ContextMenuStrip, m_dgv, e); };
		}

		protected virtual void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e) { }
		#endregion

		#region Events
		public class EventArgsCell : EventArgs
		{
			public DataGridViewCell Cell { get; set; }
		}

		public event EventHandler SelectionChanged;
		public event EventHandler<EventArgsCell> CellEndEdit;

		protected virtual void AssignEvents()
		{
			m_dgv.CellFormatting += CellFormattingEvent;
			m_dgv.MouseDown += MouseDownEvent;
			m_dgv.SelectionChanged += SelectionChangedEvent;
			m_dgv.CellMouseUp += CellMouseUpEvent;
			m_dgv.CellEndEdit += CellEndEditEvent;
		}

		protected virtual void RemoveEvents()
		{
			m_dgv.CellFormatting -= CellFormattingEvent;
			m_dgv.MouseDown -= MouseDownEvent;
			m_dgv.SelectionChanged -= SelectionChangedEvent;
			m_dgv.CellMouseUp -= CellMouseUpEvent;
			m_dgv.CellEndEdit -= CellEndEditEvent;
		}

		private void SelectionChangedEvent(object sender, EventArgs e)
		{
			SelectionChangedInternal();
			SelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		private void CellFormattingEvent(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0)
				return;
			DataGridView dgv = sender as DataGridView;
			if (dgv == null)
				return;

			DataGridViewCell dgvc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
			e.CellStyle = CellFormattingInternal(dgvc);
			e.CellStyle.SelectionBackColor = ToolsImage.Lerp(dgv.DefaultCellStyle.SelectionBackColor, e.CellStyle.BackColor, 0.5f);
		}

		private void CellEndEditEvent(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView dgv = (sender as DataGridView);
			if (dgv is null)
				return;

			DataGridViewCell dgvc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
			CellEndEditInternal(dgv, dgvc);
			CellEndEdit?.Invoke(this, new EventArgsCell() { Cell = dgvc });
		}

		private void MouseDownEvent(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				var hti = m_dgv.HitTest(e.X, e.Y);
				if (hti.RowIndex >= 0 && hti.ColumnIndex >= 0)
				{
					DataGridViewCell dgvc = m_dgv.Rows[hti.RowIndex].Cells[hti.ColumnIndex];
					if (!dgvc.Selected)
					{
						SelectCell(dgvc);
					}
				}
			}
		}

		private void CellMouseUpEvent(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView dgv = (sender as DataGridView);
			if (dgv is null)
				return;

			DataGridViewCell dgvc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
			CellMouseUpInternal(dgv, dgvc);
		}
		#endregion

		#region IDisposable
		protected virtual void DisposeManaged()
		{
			m_dgv.ContextMenuStrip?.Dispose();
			m_dgv.ContextMenuStrip = null;

			m_dgv?.Dispose();
			m_dgv = null;
		}

		private void Dispose(bool disposing)
		{
			if (!m_disposedValue)
			{
				if (disposing)
				{
					DisposeManaged();
				}
				m_disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(disposing: true);
			GC.SuppressFinalize(this);
		}
		#endregion
	}
}
