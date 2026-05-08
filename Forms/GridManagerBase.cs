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
		protected FastObjectListView m_dgv;
		private ToolStrip m_searchStrip;
		private ToolStripTextBox m_searchBox;
		#endregion

		#region Properties
		public List<string> ColumnsDisplayed { get; set; } = null;
		public IEnumerable<T> Elements { get; set; }
		public List<T> CheckedElements { get; set; }
		#endregion

		#region CTOR
		public GridManagerBase(FastObjectListView dgv, IEnumerable<T> elements)
		{
			m_dgv = dgv;
			Elements = elements;

			m_dgv.FullRowSelect = true;
			m_dgv.ShowGroups = false;
			m_dgv.MultiSelect = false;
			m_dgv.UseFiltering = true;
			m_dgv.FilterMenuBuildStrategy = new FilterMenuBuilder();
			m_dgv.CellEditActivation = ObjectListView.CellEditActivateMode.None;

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
			m_dgv.SuspendDrawing();
			RemoveEvents();

			m_dgv.ModelFilter = null;

			if (CheckedElements is not null)
			{
				m_dgv.CheckBoxes = true;
				m_dgv.CheckStateGetter = obj =>
					CheckedElements.Contains((T)obj) ? CheckState.Checked : CheckState.Unchecked;
				m_dgv.CheckStatePutter = (obj, value) =>
				{
					T element = (T)obj;
					if (value == CheckState.Checked)
					{
						if (!CheckedElements.Contains(element))
							CheckedElements.Add(element);
					}
					else
					{
						while (CheckedElements.Remove(element)) ;
					}
					return value;
				};
			}

			ApplyColumnsDisplayed();
			m_dgv.SetObjects(Elements);

			AssignEvents();
			m_dgv.ResumeDrawing();
		}

		private void ApplyColumnsDisplayed()
		{
			if (ColumnsDisplayed is null)
				return;

			foreach (OLVColumn col in m_dgv.AllColumns)
				col.IsVisible = false;

			int iDisplayIndex = 0;
			foreach (string sColumnName in ColumnsDisplayed)
			{
				OLVColumn col = m_dgv.AllColumns.FirstOrDefault(c => c.Name == sColumnName);
				if (col is not null)
				{
					col.IsVisible = true;
					col.DisplayIndex = iDisplayIndex++;
				}
			}
			m_dgv.RebuildColumns();
		}

		public void RefreshDataSourceRows()
		{
			if (Elements is not null)
				m_dgv.RefreshObjects(Elements.ToList());
		}

		public IEnumerable<T> GetSelectedElements()
		{
			return m_dgv.SelectedObjects.Cast<T>();
		}

		public void SelectRow(T element)
		{
			m_dgv.SelectObject(element, true);
		}

		private void SetupSearchBar()
		{
			m_dgv.KeyDown += (s, e) =>
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
			Control parent = m_dgv.Parent;
			if (parent is null)
				return;

			if (m_searchStrip is null)
			{
				m_searchStrip = new ToolStrip { GripStyle = ToolStripGripStyle.Hidden, AutoSize = false, Height = 27, Visible = false };
				ToolStripLabel lbl = new ToolStripLabel("Search:");
				m_searchBox = new ToolStripTextBox { AutoSize = false, Width = 200 };
				ToolStripButton btnClose = new ToolStripButton("✕") { DisplayStyle = ToolStripItemDisplayStyle.Text };

				m_searchStrip.Items.AddRange(new ToolStripItem[] { lbl, m_searchBox, btnClose });

				m_searchBox.TextChanged += (s, e) =>
				{
					m_dgv.ModelFilter = string.IsNullOrEmpty(m_searchBox.Text)
						? null
						: TextMatchFilter.Contains(m_dgv, m_searchBox.Text);
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
			m_dgv.ModelFilter = null;
			m_searchStrip.Visible = false;
			m_dgv.Focus();
		}
		#endregion

		#region Menus
		private void InitializeContextMenu()
		{
			m_dgv.ContextMenuStrip = new ContextMenuStrip();
			m_dgv.ContextMenuStrip.Opening += (object sender, CancelEventArgs e) =>
				ContextMenuOpening(sender as ContextMenuStrip, m_dgv, e);
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
			m_dgv.FormatRow += FormatRowEvent;
			m_dgv.FormatCell += FormatCellEvent;
			m_dgv.MouseDown += MouseDownEvent;
			m_dgv.SelectionChanged += SelectionChangedEvent;
			m_dgv.CellEditFinished += CellEditFinishedEvent;
		}

		protected virtual void RemoveEvents()
		{
			m_dgv.FormatRow -= FormatRowEvent;
			m_dgv.FormatCell -= FormatCellEvent;
			m_dgv.MouseDown -= MouseDownEvent;
			m_dgv.SelectionChanged -= SelectionChangedEvent;
			m_dgv.CellEditFinished -= CellEditFinishedEvent;
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
				OlvListViewHitTestInfo hti = m_dgv.OlvHitTest(e.X, e.Y);
				if (hti.Item is not null && !hti.Item.Selected)
				{
					m_dgv.DeselectAll();
					hti.Item.Selected = true;
				}
			}
		}
		#endregion

		#region IDisposable
		protected bool m_disposedValue;

		protected virtual void DisposeManaged()
		{
			m_dgv.ContextMenuStrip?.Dispose();
			m_dgv.ContextMenuStrip = null;

			m_searchStrip?.Dispose();
			m_searchStrip = null;

			m_dgv?.Dispose();
			m_dgv = null;
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
