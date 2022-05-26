using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal abstract class GridManager : IDisposable
	{
		#region Fields
		protected DataGridView m_dgv;
		protected DataTable m_dtSource;
		protected bool m_disposedValue;
		#endregion

		#region Properties
		public List<string> ColumnsDisplayed { get; set; } = null;
		#endregion

		#region CTOR
		public GridManager(DataGridView dgv)
		{
			m_dgv = dgv;

			ToolsControls.SetDataGridViewProperties(dgv);
			m_dgv.AutoGenerateColumns = true;
			m_dgv.DataSource = new BindingSource();

			m_dgv.CellFormatting += CellFormattingEvent;
			m_dgv.MouseDown += MouseDownEvent;
		}
		#endregion

		#region Methods
		public virtual void Initialize()
		{
			InitializeDataSource();
			SetDataSource();
			PostInitializeColumns();
		}

		protected abstract void InitializeDataSource();

		protected virtual void PostInitializeColumns()
		{
			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				if (ColumnsDisplayed is object && !ColumnsDisplayed.Contains(column.DataPropertyName))
					column.Visible = false;
			}
		}

		protected void SetDataSource()
		{
			try
			{
				m_dgv.SuspendDrawing();
				//m_dgv.ColumnHeadersHeight = 25; // not ideal, but if the header is to narrow, it will be widened by AdvancedDataGridView.OnColumnAdded, and sometimes it will cause problems that I don't understand
				(m_dgv.DataSource as BindingSource).DataSource = m_dtSource.DefaultView;
				m_dgv.ResumeDrawing();
			}
			catch (Exception ex) { ToolsControls.ShowMessageBoxError(ex.Message); } // to check the problem addressed by "m_dgv.ColumnHeadersHeight = 25"
		}

		protected IEnumerable<DataGridViewRow> GetSelectedRows()
		{
			return m_dgv.SelectedCells.Cast<DataGridViewCell>().Select(_dgvc => _dgvc.OwningRow).Distinct();
		}

		protected virtual DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = dgvc.InheritedStyle;
			return cellStyle;
		}
		#endregion

		#region Events
		private void CellFormattingEvent(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0)
				return;
			DataGridView dgv = sender as DataGridView;
			if (dgv == null)
				return;

			DataGridViewCell dgvc = dgv.Rows[e.RowIndex].Cells[e.ColumnIndex];
			e.CellStyle = CellFormatting(dgvc);
			e.CellStyle.SelectionBackColor = ToolsImage.Lerp(dgv.DefaultCellStyle.SelectionBackColor, e.CellStyle.BackColor, 0.5f);
		}

		private void MouseDownEvent(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				var hti = m_dgv.HitTest(e.X, e.Y);
				DataGridViewCell dgvc = m_dgv.Rows[hti.RowIndex].Cells[hti.ColumnIndex];
				if (!dgvc.Selected)
				{
					m_dgv.ClearSelection();
					dgvc.Selected = true;
				}
			}
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
