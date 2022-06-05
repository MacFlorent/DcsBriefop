using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace DcsBriefop.FgControls
{
	internal abstract class GridManager : IDisposable
	{
		#region Fields
		protected FgDataGridView m_dgv;
		protected DataTable m_dtSource;
		protected List<string> m_columnsDisplayed;

		protected bool m_disposedValue;
		#endregion

		#region CTOR
		public GridManager(FgDataGridView dgv, List<string> columnsDisplayed)
		{
			m_dgv = dgv;
			m_dgv.CellFormatting += CellFormattingEvent;
			m_dgv.CellValueChanged += CellValueChangedEvent;

			m_columnsDisplayed = columnsDisplayed;
		}
		#endregion

		#region Methods
		public void Initialize()
		{
			InitializeGridSource();
			InitializeContextMenu();
			InitializeGridColumns();
			FinalizeGridColumns();

			m_dgv.CellValueChanged -= CellValueChangedEvent;
			m_dgv.DtSource = m_dtSource;
			m_dgv.CellValueChanged += CellValueChangedEvent;
		}

		protected abstract void InitializeGridSource();
		protected abstract void InitializeGridColumns();

		protected void FinalizeGridColumns()
		{
			if (m_columnsDisplayed is object)
			{
				int iDisplayIndex = 0;
				foreach (string s in m_columnsDisplayed)
				{
					if (m_dgv.Columns.Contains(s))
						m_dgv.Columns[s].DisplayIndex = iDisplayIndex++;
				}

				foreach (DataGridViewColumn column in m_dgv.Columns)
				{
					if (!m_columnsDisplayed.Contains(column.Name))
					{
						column.Visible = false;
						column.DisplayIndex = iDisplayIndex++;
					}
				}
			}
		}

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

		protected virtual DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = dgvc.InheritedStyle;
			return cellStyle;
		}

		protected virtual void CellValueChanged(string sColumnName, DataRow dr) { }
		#endregion

		#region Menus
		private void InitializeContextMenu()
		{
			m_dgv.ContextMenuStrip = new ContextMenuStrip();
			m_dgv.ContextMenuStrip.Opening += (object sender, CancelEventArgs e) => { ContextMenuOpening(sender as ContextMenuStrip, m_dgv, e); };
		}

		protected virtual void ContextMenuOpening(ContextMenuStrip menu, DataGridView dgv, CancelEventArgs e) {}
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

		private void CellValueChangedEvent(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			FgDataGridView dgv = (sender as FgDataGridView);
			if (dgv is null)
				return;

			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			DataRow dr = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)?.Row;

			CellValueChanged(column.DataPropertyName, dr);
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
