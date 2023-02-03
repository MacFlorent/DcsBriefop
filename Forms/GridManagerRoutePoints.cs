using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal class GridManagerRoutePoints : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Number = "Number";
			public static readonly string Name = "Name";
			public static readonly string Type = "Type";
			public static readonly string Action = "Action";
			public static readonly string AltitudeFeet = "AltitudeFeet";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		private IEnumerable<BopRoutePoint> m_routePoints;
		#endregion

		#region Properties
		public IEnumerable<BopRoutePoint> RoutePoints
		{
			get { return m_routePoints; }
			set { m_routePoints = value; }
		}
		#endregion

		#region CTOR
		public GridManagerRoutePoints(DataGridView dgv, IEnumerable<BopRoutePoint> routePoints) : base(dgv)
		{
			m_routePoints = routePoints;
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.Number, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Name, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Type, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Action, typeof(string));
			m_dtSource.Columns.Add(GridColumn.AltitudeFeet, typeof(decimal));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(BopRoutePoint));

			foreach (BopRoutePoint bopRoutePoint in m_routePoints)
				RefreshDataSourceRow(bopRoutePoint);
		}

		private void RefreshDataSourceRow(BopRoutePoint bopRoutePoint)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<BopRoutePoint>(GridColumn.Data) == bopRoutePoint).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, bopRoutePoint);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.Number, bopRoutePoint.Number);
			dr.SetField(GridColumn.Name, bopRoutePoint.Name);
			dr.SetField(GridColumn.Type, bopRoutePoint.Type);
			dr.SetField(GridColumn.Action, bopRoutePoint.Action);
			dr.SetField(GridColumn.AltitudeFeet, $"{bopRoutePoint.AltitudeFeet:0}");
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				column.ReadOnly = true;
			}

			m_dgv.Columns[GridColumn.Data].Visible = false;
			m_dgv.Columns[GridColumn.AltitudeFeet].HeaderText = "Altitude (ft)";
		}

		public IEnumerable<BopRoutePoint> GetSelectedBopRoutePoints()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopRoutePoint>(GridColumn.Data)).ToList();
		}

		protected override DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = base.CellFormatting(dgvc);
			return cellStyle;
		}

		protected override void SelectionChanged()
		{
			SelectionChangedBopRoutePoints?.Invoke(this, new EventArgsBopRoutePoint() { BopRoutePoints = GetSelectedBopRoutePoints() });
		}
		#endregion

		#region Events
		public class EventArgsBopRoutePoint : EventArgs
		{
			public IEnumerable<BopRoutePoint> BopRoutePoints { get; set; }
		}
		public event EventHandler<EventArgsBopRoutePoint> SelectionChangedBopRoutePoints;
		#endregion
	}
}
