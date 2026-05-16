using BrightIdeasSoftware;
using DcsBriefop.Tools;
using System.ComponentModel;

namespace DcsBriefop.Forms
{
	internal abstract class GridManagerBase<T> : IDisposable where T : class
	{
		#region Columns
		protected static class GridWidth
		{
			public static readonly int Tiny = 25;
			public static readonly int Small = 50;
			public static readonly int Medium = 100;
			public static readonly int Large = 200;
			public static readonly int ExtraLarge = 300;
		}
		#endregion

		#region Fields
		protected FastObjectListView m_grid;
		private ToolStrip m_searchStrip;
		private ToolStripTextBox m_searchBox;
		#endregion

		#region Properties
		public List<string> ColumnsDisplayed { get; set; } = null;
		public IEnumerable<T> Elements { get; set; }
		#endregion

		#region CTOR
		public GridManagerBase(FastObjectListView grid, IEnumerable<T> elements)
		{
			m_grid = grid;
			Elements = elements;

			m_grid.FullRowSelect = true;
			m_grid.ShowGroups = false;
			m_grid.MultiSelect = false;
			m_grid.UseFiltering = true;
			m_grid.FilterMenuBuildStrategy = new FilterMenuBuilder();
			m_grid.CellEditActivation = ObjectListView.CellEditActivateMode.None;

			InitializeColumns();
			InitializeContextMenu();
			SetupSearchBar();
			AssignEvents();
		}
		#endregion

		#region Abstract / Virtual methods
		protected abstract void InitializeColumns();
		protected virtual void FormatRowInternal(FormatRowEventArgs e) { }
		protected virtual void FormatCellInternal(FormatCellEventArgs e) { }
		protected virtual void CellEditFinishedInternal(CellEditEventArgs e) { }
		protected virtual void SelectionChangedInternal() { }
		#endregion

		#region Methods
		public void Refresh()
		{
			m_grid.SuspendDrawing();
			RemoveEvents();

			m_grid.ModelFilter = null;

			ApplyColumnsDisplayed();
			m_grid.SetObjects(Elements);

			AssignEvents();
			if (m_grid.GetItemCount() > 0 && m_grid.SelectedIndex < 0)
				m_grid.SelectedIndex = 0;

			m_grid.ResumeDrawing();
		}

		private void ApplyColumnsDisplayed()
		{
			if (ColumnsDisplayed is null)
				return;

			foreach (OLVColumn col in m_grid.AllColumns)
				col.IsVisible = false;

			int iInsertAt = 0;
			foreach (string sColumnName in ColumnsDisplayed)
			{
				OLVColumn col = m_grid.AllColumns.FirstOrDefault(_c => _c.Name == sColumnName);
				if (col is not null)
				{
					col.IsVisible = true;
					m_grid.AllColumns.Remove(col);
					m_grid.AllColumns.Insert(iInsertAt++, col);
				}
			}

			m_grid.RebuildColumns();
		}

		public void RefreshObjects()
		{
			if (Elements is not null)
				m_grid.RefreshObjects(Elements.ToList());
		}

		public IEnumerable<T> GetSelectedElements()
		{
			return m_grid.SelectedObjects.Cast<T>();
		}

		public void SelectRow(T element)
		{
			m_grid.SelectObject(element, true);
		}
		#endregion

		#region Search bar
		private void SetupSearchBar()
		{
			m_grid.KeyDown += (s, e) =>
			{
				if (e.Control && e.KeyCode == Keys.F)
				{
					ShowSearchBar();
					e.Handled = true;
				}
			};
		}

		private void ShowSearchBar()
		{
			Control parent = m_grid.Parent;
			if (parent is null)
				return;

			if (m_searchStrip is null)
			{
				m_searchStrip = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden, AutoSize = false, Height = 27, Visible = false };
				ToolStripLabel lbl = new("Search:");
				m_searchBox = new ToolStripTextBox { AutoSize = false, Width = 200 };
				ToolStripButton btnClose = new("✕") { DisplayStyle = ToolStripItemDisplayStyle.Text };

				m_searchStrip.Items.AddRange([lbl, m_searchBox, btnClose]);

				m_searchBox.TextChanged += (s, e) =>
				{
					m_grid.ModelFilter = string.IsNullOrEmpty(m_searchBox.Text)
						? null
						: TextMatchFilter.Contains(m_grid, m_searchBox.Text);
				};

				m_searchBox.KeyDown += (s, e) =>
				{
					if (e.KeyCode == Keys.Escape)
						HideSearchBar();
				};

				btnClose.Click += (s, e) => HideSearchBar();

				m_searchStrip.Dock = DockStyle.Top;
				parent.Controls.Add(m_searchStrip);
				parent.Controls.SetChildIndex(m_searchStrip, parent.Controls.Count - 1);
			}

			m_searchStrip.Visible = true;
			m_searchBox.Focus();
		}

		private void HideSearchBar()
		{
			if (m_searchStrip is null)
				return;

			m_searchBox.Text = "";
			m_grid.ModelFilter = null;
			m_searchStrip.Visible = false;
			m_grid.Focus();
		}
		#endregion

		#region Menus
		private void InitializeContextMenu()
		{
			m_grid.ContextMenuStrip = new ContextMenuStrip();
			m_grid.ContextMenuStrip.Opening += (object sender, CancelEventArgs e) =>
				ContextMenuOpening(sender as ContextMenuStrip, m_grid, e);
		}

		protected virtual void ContextMenuOpening(ContextMenuStrip menu, FastObjectListView dgv, CancelEventArgs e) { }
		#endregion

		#region Events
		public class EventArgsCell : EventArgs
		{
			public T Element { get; set; }
			public string ColumnName { get; set; }
		}

		public event EventHandler SelectionChanged;
		public event EventHandler<EventArgsCell> CellEndEdit;

		protected virtual void AssignEvents()
		{
			m_grid.FormatRow += FormatRowEvent;
			m_grid.FormatCell += FormatCellEvent;
			m_grid.MouseDown += MouseDownEvent;
			m_grid.SelectionChanged += SelectionChangedEvent;
			m_grid.CellEditFinished += CellEditFinishedEvent;
		}

		protected virtual void RemoveEvents()
		{
			m_grid.FormatRow -= FormatRowEvent;
			m_grid.FormatCell -= FormatCellEvent;
			m_grid.MouseDown -= MouseDownEvent;
			m_grid.SelectionChanged -= SelectionChangedEvent;
			m_grid.CellEditFinished -= CellEditFinishedEvent;
		}

		private void SelectionChangedEvent(object sender, EventArgs e)
		{
			SelectionChangedInternal();
			SelectionChanged?.Invoke(this, EventArgs.Empty);
		}

		private void FormatRowEvent(object sender, FormatRowEventArgs e)
		{
			FormatRowInternal(e);
		}

		private void FormatCellEvent(object sender, FormatCellEventArgs e)
		{
			FormatCellInternal(e);
		}

		private void CellEditFinishedEvent(object sender, CellEditEventArgs e)
		{
			CellEditFinishedInternal(e);
			CellEndEdit?.Invoke(this, new EventArgsCell { Element = e.RowObject as T, ColumnName = e.Column?.Name });
		}

		private void MouseDownEvent(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				OlvListViewHitTestInfo hti = m_grid.OlvHitTest(e.X, e.Y);
				if (hti.Item is not null && !hti.Item.Selected)
				{
					m_grid.DeselectAll();
					hti.Item.Selected = true;
				}
			}
		}
		#endregion

		#region IDisposable
		protected bool m_disposedValue;

		protected virtual void DisposeManaged()
		{
			m_grid.ContextMenuStrip?.Dispose();
			m_grid.ContextMenuStrip = null;

			m_searchStrip?.Dispose();
			m_searchStrip = null;

			m_grid?.Dispose();
			m_grid = null;
		}

		private void Dispose(bool disposing)
		{
			if (!m_disposedValue)
			{
				if (disposing)
					DisposeManaged();
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
