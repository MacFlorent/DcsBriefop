using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Zuby.ADGV;

namespace DcsBriefop.FgControls
{
	internal class FgDataGridView : AdvancedDataGridView
	{
		#region Default Properties override
		[DefaultValue(DataGridViewAutoSizeColumnsMode.AllCells)]
		public new DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
		{
			get { return base.AutoSizeColumnsMode; }
			set { base.AutoSizeColumnsMode = value; }
		}
		[DefaultValue(DataGridViewSelectionMode.CellSelect)]
		public new DataGridViewSelectionMode SelectionMode
		{
			get { return base.SelectionMode; }
			set { base.SelectionMode = value; }
		}
		[DefaultValue(false)]
		public new bool RowHeadersVisible
		{
			get { return base.RowHeadersVisible; }
			set { base.RowHeadersVisible = value; }
		}
		[DefaultValue(false)]
		public new bool AllowUserToAddRows
		{
			get { return base.AllowUserToAddRows; }
			set { base.AllowUserToAddRows = value; }
		}
		[DefaultValue(false)]
		public new bool AllowUserToDeleteRows
		{
			get { return base.AllowUserToDeleteRows; }
			set { base.AllowUserToDeleteRows = value; }
		}
		#endregion

		#region Properties
		private DataTable m_dtSource;
		public DataTable DtSource
		{
			get { return m_dtSource; }
			set
			{
				m_dtSource = value;
				ApplyDataSource();
			}
		}
		#endregion

		#region CTOR
		public FgDataGridView() : base()
		{
			AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			SelectionMode = DataGridViewSelectionMode.CellSelect;
			RowHeadersVisible = false;
			AutoGenerateColumns = false;
			AllowUserToAddRows = false;
			AllowUserToDeleteRows = false;
		}
		#endregion

		#region Methods
		private void ApplyDataSource()
		{
			try
			{
				//if (!DesignMode)
				//	this.SuspendDrawing();
				
				//ColumnHeadersHeight = 25; // not ideal, but if the header is to narrow, it will be widened by AdvancedDataGridView.OnColumnAdded, and sometimes it will cause problems that I don't understand
				if (DataSource is null)
					DataSource = new BindingSource();

				(DataSource as BindingSource).DataSource = m_dtSource?.DefaultView;

				//if (!DesignMode)
				//	this.ResumeDrawing();
			}
			catch (Exception ex) { ToolsControls.ShowMessageBoxError(ex.Message); } // to check the problem addressed by "m_dgv.ColumnHeadersHeight = 25"
		}

		public IEnumerable<DataGridViewRow> GetSelectedRows()
		{
			if (SelectedRows is object && SelectedRows.Count > 0)
				return SelectedRows.OfType<DataGridViewRow>();
			else
				return SelectedCells.Cast<DataGridViewCell>().Select(_dgvc => _dgvc.OwningRow).Distinct();
		}

		public IEnumerable<DataRow> GetSelectedDataRows()
		{
			return GetSelectedRows().Select(_dgvr => (_dgvr.DataBoundItem as DataRowView)?.Row).Where(_r => _r is object);
		}

		public DataGridViewColumn AddColumn<T>(string sColumnName, string sHeaderText)
		{
			DataGridViewColumn dgvc;

			if (typeof(T) == typeof(bool))
			{
				dgvc = new DataGridViewCheckBoxColumn();
			}
			else
			{
				dgvc = new DataGridViewTextBoxColumn();
			}

			dgvc.Name = sColumnName;
			dgvc.DataPropertyName = sColumnName;
			dgvc.HeaderText = sHeaderText;

			Columns.Add(dgvc);
			return dgvc;
		}
		#endregion

		#region Events
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			if (e.Button == MouseButtons.Right)
			{
				var hti = HitTest(e.X, e.Y);
				DataGridViewCell dgvc = Rows[hti.RowIndex].Cells[hti.ColumnIndex];
				if (!dgvc.Selected)
				{
					ClearSelection();
					dgvc.Selected = true;
				}
			}
		}
		#endregion
	}
}
