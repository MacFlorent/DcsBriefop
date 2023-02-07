using DcsBriefop.Forms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using static GMap.NET.Entity.OpenStreetMapGraphHopperRouteEntity;

namespace DcsBriefop.Tools
{
	internal static class ToolsControls
	{
		#region MessageBox
		public static void ShowMessageBoxInfo(string sMessage)
		{
			MessageBox.Show(sMessage, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public static bool ShowMessageBoxQuestion(string sMessage)
		{
			return MessageBox.Show(sMessage, "Question", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK;
		}

		public static void ShowMessageBoxError(string sMessage)
		{
			MessageBox.Show(sMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		public static void ShowMessageBoxAndLogException(string sMessage, Exception e, [CallerMemberName] string sMemberName = "", [CallerLineNumber] int iLineNumber = 0)
		{
			ShowMessageBoxError($"{sMessage}{Environment.NewLine}{Environment.NewLine}{e.Message}");
			Log.Error(sMessage, sMemberName, iLineNumber);
			Log.Exception(e, sMemberName, iLineNumber);
		}
		#endregion

		#region DataGridView
		//public static void SetDataGridViewProperties(this DataGridView dgv)
		//{
		//	dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
		//	dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
		//	dgv.AllowUserToResizeColumns = true;
		//	dgv.AllowUserToAddRows = false;
		//	dgv.RowHeadersVisible = false;

		//	dgv.MouseDown += (sender, e) => { MouseDownEvent(dgv, e); };
		//}
		//
		//public static DataGridViewColumn AddColumn<T>(this DataGridView dgv, string sColumnName, string sHeaderText)
		//{
		//	DataGridViewColumn dgvc;

		//	if (typeof(T) == typeof (bool))
		//	{
		//		dgvc = new DataGridViewCheckBoxColumn();
		//	}
		//	else
		//	{
		//		dgvc = new DataGridViewTextBoxColumn();
		//	}

		//	dgvc.Name = sColumnName;
		//	dgvc.DataPropertyName = sColumnName;
		//	dgvc.HeaderText = sHeaderText;

		//	dgv.Columns.Add(dgvc);
		//	return dgvc;
		//}

		//public static void SetDataSource(this DataGridView dgv, DataTable dtSource)
		//{
		//	try
		//	{
		//		dgv.SuspendDrawing();
		//		//ColumnHeadersHeight = 25; // not ideal, but if the header is to narrow, it will be widened by AdvancedDataGridView.OnColumnAdded, and sometimes it will cause problems that I don't understand
		//		if (dgv.DataSource is null)
		//			dgv.DataSource = new BindingSource();

		//		(dgv.DataSource as BindingSource).DataSource = dtSource?.DefaultView;

		//		dgv.ResumeDrawing();
		//	}
		//	catch (Exception ex) { ShowMessageBoxError(ex.Message); } // to check the problem addressed by "m_dgv.ColumnHeadersHeight = 25"
		//}

		//public static IEnumerable<DataGridViewRow> GetSelectedRows(this DataGridView dgv)
		//{
		//	if (dgv.SelectedRows is object && dgv.SelectedRows.Count > 0)
		//		return dgv.SelectedRows.OfType<DataGridViewRow>();
		//	else
		//		return dgv.SelectedCells.Cast<DataGridViewCell>().Select(_dgvc => _dgvc.OwningRow).Distinct();
		//}

		//public static IEnumerable<DataRow> GetSelectedDataRows(this DataGridView dgv)
		//{
		//	return dgv.GetSelectedRows().Select(_dgvr => (_dgvr.DataBoundItem as DataRowView)?.Row).Where(_r => _r is object);
		//}

		//internal static void MouseDownEvent(DataGridView dgv, MouseEventArgs e)
		//{
		//	if (e.Button == MouseButtons.Right)
		//	{
		//		var hti = dgv.HitTest(e.X, e.Y);
		//		DataGridViewCell dgvc = dgv.Rows[hti.RowIndex].Cells[hti.ColumnIndex];
		//		if (!dgvc.Selected)
		//		{
		//			dgv.ClearSelection();
		//			dgvc.Selected = true;
		//		}
		//	}
		//}
		#endregion

		#region Redraw Suspend/Resume
		[DllImport("user32.dll", EntryPoint = "SendMessageA", ExactSpelling = true, CharSet = CharSet.Ansi, SetLastError = true)]
		private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
		private const int WM_SETREDRAW = 0xB;

		public static void SuspendDrawing(this Control target)
		{
			SendMessage(target.Handle, WM_SETREDRAW, 0, 0);
		}

		public static void ResumeDrawing(this Control target) { ResumeDrawing(target, true); }
		public static void ResumeDrawing(this Control target, bool redraw)
		{
			SendMessage(target.Handle, WM_SETREDRAW, 1, 0);

			if (redraw)
			{
				target.Refresh();
			}
		}
		#endregion

		#region Controls
		public static void AddTab(this TabControl tc, string sText, Control control)
		{
			control.Dock = DockStyle.Fill;
			TabPage tp = new TabPage(sText);
			tp.Controls.Add(control);
			tc.TabPages.Add(tp);
		}

		public static void DisposeAndClearTabs(this TabControl tc)
		{
			while (tc.TabCount > 0)
				tc.TabPages[0].Dispose();
			tc.TabPages.Clear();
		}

		public static void CenterInParent(this Control c)
		{
			c.Location = new Point(c.Parent.ClientSize.Width / 2 - c.Size.Width / 2, c.Parent.ClientSize.Height / 2 - c.Size.Height / 2);
			c.Anchor = AnchorStyles.None;
		}

		public static void CenterInParentHorizontal(this Control c)
		{
			c.Location = new Point(c.Parent.ClientSize.Width / 2 - c.Size.Width / 2, c.Location.Y);
			c.Anchor = AnchorStyles.None;
		}

		public static void FillCombo(ComboBox cb, object dataSource, string sValueMember, string sDisplayMember, EventHandler selectedValueChanged)
		{
			if (selectedValueChanged is object)
				cb.SelectedValueChanged -= selectedValueChanged;

			cb.Items.Clear();

			cb.DisplayMember = sDisplayMember;
			if (!string.IsNullOrEmpty(sValueMember))
				cb.ValueMember = sValueMember;
						
			cb.DataSource = dataSource;

			if (selectedValueChanged is object)
				cb.SelectedValueChanged += selectedValueChanged;
		}
		#endregion
	}
}

