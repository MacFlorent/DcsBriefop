//using DcsBriefop.Tools;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Linq;
//using System.Windows.Forms;
//using Zuby.ADGV;

//namespace DcsBriefop.UcBriefing
//{
//	internal class FgDataGridView : AdvancedDataGridView
//	{
//		#region Fields
//		#endregion

//		#region Properties
//		private DataTable m_dtSource;
//		public DataTable DtSource
//		{
//			get
//			{
//				return m_dtSource;
//			}
//			set
//			{
//				m_dtSource = value;
//				ApplyDataSource();
//			}
//		}

//		public override DataGridViewAutoSizeColumnsMode AutoSizeColumnsMode
//		{
//			get { return base.AutoSizeColumnsMode; }
//		}
//		#endregion

//		#region CTOR
//		public FgDataGridView()
//		{
//			//AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
//			//SelectionMode = DataGridViewSelectionMode.CellSelect;
//			//AllowUserToResizeColumns = true;
//			//AllowUserToAddRows = false;
//			//RowHeadersVisible = false;
//		}
//		#endregion

//		#region Methods
//		private void ApplyDataSource()
//		{
//			try
//			{
//				this.SuspendDrawing();
//				//ColumnHeadersHeight = 25; // not ideal, but if the header is to narrow, it will be widened by AdvancedDataGridView.OnColumnAdded, and sometimes it will cause problems that I don't understand
//				if (DataSource is null)
//					DataSource = new BindingSource();

//				(DataSource as BindingSource).DataSource = m_dtSource?.DefaultView;

//				this.ResumeDrawing();
//			}
//			catch (Exception ex) { ToolsControls.ShowMessageBoxError(ex.Message); } // to check the problem addressed by "m_dgv.ColumnHeadersHeight = 25"
//		}

//		public DataGridViewColumn AddColumn(string sColumnName, string sHeaderText)
//		{
//			DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
//			column.Name = sColumnName;
//			column.DataPropertyName = sColumnName;
//			column.HeaderText = sHeaderText;

//			Columns.Add(column);
//			return column;
//		}

//		public IEnumerable<DataGridViewRow> GetSelectedRows()
//		{
//			return SelectedCells.Cast<DataGridViewCell>().Select(_dgvc => _dgvc.OwningRow).Distinct();
//		}

//		public IEnumerable<DataRow> GetSelectedDataRows()
//		{
//			return GetSelectedRows().Select(_dgvr => (_dgvr.DataBoundItem as DataRowView)?.Row).Where(_r => _r is object);
//		}
//		#endregion

//		#region Events
//		protected override void OnMouseDown(MouseEventArgs e)
//		{
//			base.OnMouseDown(e);
//			if (e.Button == MouseButtons.Right)
//			{
//				var hti = HitTest(e.X, e.Y);
//				DataGridViewCell dgvc = Rows[hti.RowIndex].Cells[hti.ColumnIndex];
//				if (!dgvc.Selected)
//				{
//					ClearSelection();
//					dgvc.Selected = true;
//				}
//			}
//		}
//		#endregion
//	}
//}
