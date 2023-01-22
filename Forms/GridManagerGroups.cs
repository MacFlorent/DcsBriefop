﻿using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal class GridManagerGroups : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string Coalition = "Coalition";
			public static readonly string Country = "Country";
			public static readonly string Name = "Name";
			public static readonly string Class = "Class";
			public static readonly string Type = "Type";
			public static readonly string Attributes = "Attributes";
			public static readonly string Playable = "Playable";
			public static readonly string Data = "Data";
		}
		//public static List<string> ColumnsDisplayedOwn = new List<string>() { GridColumn.Included, GridColumn.Id, GridColumn.Description, GridColumn.Class, GridColumn.Type, GridColumn.Function, GridColumn.Task, GridColumn.Localisation, GridColumn.Radio, GridColumn.Notes, GridColumn.Playable, GridColumn.Mission, GridColumn.MapDisplay };
		//public static List<string> ColumnsDisplayedOpposing = new List<string>() { GridColumn.Included, GridColumn.Id, GridColumn.Description, GridColumn.Class, GridColumn.Type, GridColumn.Localisation, GridColumn.MapDisplay };
		//public static List<string> ColumnsDisplayedAirdrome = new List<string>() { GridColumn.Included, GridColumn.Id, GridColumn.Description, GridColumn.Localisation, GridColumn.Radio, GridColumn.Notes, GridColumn.MapDisplay };
		#endregion

		#region Fields
		private List<BopGroup> m_groups;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerGroups(DataGridView dgv, List<BopGroup> groups) : base(dgv)
		{
			m_groups = groups;

			m_dgv.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Coalition, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Country, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Name, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Class, typeof(ElementDcsObjectClass));
			m_dtSource.Columns.Add(GridColumn.Type, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Attributes, typeof(ElementDcsObjectAttribute));
			m_dtSource.Columns.Add(GridColumn.Playable, typeof(bool));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(BopGroup));

			foreach (BopGroup bopGroup in m_groups)
				RefreshDataSourceRow(bopGroup);
		}

		private void RefreshDataSourceRow(BopGroup bopGroup)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<BopGroup>(GridColumn.Data) == bopGroup).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, bopGroup);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.Id, bopGroup.Id);
			dr.SetField(GridColumn.Coalition, bopGroup.CoalitionName);
			dr.SetField(GridColumn.Country, bopGroup.CountryName);

			dr.SetField(GridColumn.Name, bopGroup.Name);
			dr.SetField(GridColumn.Class, bopGroup.Class);
			dr.SetField(GridColumn.Type, bopGroup.Type);
			dr.SetField(GridColumn.Attributes, bopGroup.Attributes);
			dr.SetField(GridColumn.Playable, bopGroup.Playable);
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				column.ReadOnly = true;
			}

			m_dgv.Columns[GridColumn.Data].Visible = false;
		}

		protected override DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = base.CellFormatting(dgvc);

			//DataGridViewColumn column = dgvc.DataGridView.Columns[dgvc.ColumnIndex];
			//Asset asset = dgvc.DataGridView.Rows[dgvc.RowIndex].Cells[GridColumn.Data].Value as Asset;

			//if (column.DataPropertyName == GridColumn.Included)
			//{
			//	if ((asset is object && AssetOrUnitIncluded(asset)))
			//	{
			//		cellStyle.BackColor = Color.LightGreen;
			//	}
			//}
			//else if (column.DataPropertyName == GridColumn.Mission)
			//{
			//	if (asset is AssetFlight flight && flight.MissionData is object)
			//	{
			//		cellStyle.BackColor = Color.LightGreen;
			//	}
			//}

			return cellStyle;
		}
		#endregion
	}
}
