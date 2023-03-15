﻿using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using System.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerGroupOrUnits : GridManagerBase<BopGroupOrUnit>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string DisplayName = "DisplayName";
			public static readonly string Coalition = "Coalition";
			public static readonly string Country = "Country";
			public static readonly string Object = "Object";
			public static readonly string Group = "Group";
			public static readonly string Type = "Type";
			public static readonly string ObjectClass = "Class";
			public static readonly string Attributes = "Attributes";
			public static readonly string Radio = "Radio";
			public static readonly string Additional = "Additional";
		}
		#endregion

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerGroupOrUnits(DataGridView dgv, IEnumerable<BopGroupOrUnit> elements) : base(dgv, elements) { }
		#endregion

		#region Methods
		protected override void InitializeDataSourceColumns()
		{
			base.InitializeDataSourceColumns();

			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.DisplayName, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Coalition, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Country, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Object, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Group, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Type, typeof(string));
			m_dtSource.Columns.Add(GridColumn.ObjectClass, typeof(ElementGroupClass));
			m_dtSource.Columns.Add(GridColumn.Attributes, typeof(ElementDcsObjectAttribute));
			m_dtSource.Columns.Add(GridColumn.Radio, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Additional, typeof(string));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, BopGroupOrUnit element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			dr.SetField(GridColumn.Id, element.Id);
			dr.SetField(GridColumn.DisplayName, element.DisplayName);
			dr.SetField(GridColumn.Coalition, element.Coalition);
			dr.SetField(GridColumn.Country, element.Country);
			dr.SetField(GridColumn.Object, element.Object);
			dr.SetField(GridColumn.Group, element.Group);
			dr.SetField(GridColumn.Type, element.Type);
			dr.SetField(GridColumn.ObjectClass, element.GroupClass);
			dr.SetField(GridColumn.Attributes, element.Attributes);
			dr.SetField(GridColumn.Radio, element.Radio);
			dr.SetField(GridColumn.Additional, element.Additional);
		}
		#endregion

		#region Events
		#endregion
	}
}
