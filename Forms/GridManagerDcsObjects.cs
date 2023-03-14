using DcsBriefop.Data;
using System.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerDcsObjects : GridManagerBase<DcsObject>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string TypeName = "TypeName";
			public static readonly string DisplayName = "DisplayName";
			public static readonly string GroupClass = "GroupClass";
			public static readonly string Attributes = "Attributes";
			public static readonly string MapMarker = "MapMarker";
			public static readonly string Information = "Information";
			public static readonly string MainInGroup = "MainInGroup";
		}
		#endregion

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerDcsObjects(DataGridView dgv, IEnumerable<DcsObject> elements) : base(dgv, elements) { }
		#endregion

		#region Methods
		protected override void InitializeDataSourceColumns()
		{
			base.InitializeDataSourceColumns();

			m_dtSource.Columns.Add(GridColumn.TypeName, typeof(string));
			m_dtSource.Columns.Add(GridColumn.DisplayName, typeof(string));
			m_dtSource.Columns.Add(GridColumn.GroupClass, typeof(ElementGroupClass));
			m_dtSource.Columns.Add(GridColumn.Attributes, typeof(ElementDcsObjectAttribute));
			m_dtSource.Columns.Add(GridColumn.MapMarker, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Information, typeof(string));
			m_dtSource.Columns.Add(GridColumn.MainInGroup, typeof(bool));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, DcsObject element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			dr.SetField(GridColumn.TypeName, element.TypeName);
			dr.SetField(GridColumn.DisplayName, element.DisplayName);
			dr.SetField(GridColumn.GroupClass, element.GroupClass);
			dr.SetField(GridColumn.Attributes, element.Attributes);
			dr.SetField(GridColumn.MapMarker, element.MapMarker);
			dr.SetField(GridColumn.Information, element.Information);
			dr.SetField(GridColumn.MainInGroup, element.MainInGroup);
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			m_dgv.Columns[GridColumn.TypeName].HeaderText = "Type";
			m_dgv.Columns[GridColumn.DisplayName].HeaderText = "Name";
			m_dgv.Columns[GridColumn.GroupClass].HeaderText = "Class";
			m_dgv.Columns[GridColumn.MapMarker].HeaderText = "Marker";
			m_dgv.Columns[GridColumn.MainInGroup].HeaderText = "Main in group";
		}
		#endregion

		#region Events
		#endregion
	}
}
