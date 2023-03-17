using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using System.Data;
using Zuby.ADGV;

namespace DcsBriefop.Forms
{
	internal class GridManagerRoutePoints : GridManagerBase<BopRoutePoint>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Number = "Number";
			public static readonly string Name = "Name";
			public static readonly string Type = "Type";
			public static readonly string Action = "Action";
			public static readonly string Altitude = "Altitude";
		}
		#endregion

		#region Fields
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerRoutePoints(AdvancedDataGridView dgv, IEnumerable<BopRoutePoint> routePoints) : base(dgv, routePoints) { }
		#endregion

		#region Methods
		protected override void InitializeDataSourceColumns()
		{
			base.InitializeDataSourceColumns();

			m_dtSource.Columns.Add(GridColumn.Number, typeof(int));
			m_dtSource.Columns.Add(GridColumn.Name, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Type, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Action, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Altitude, typeof(decimal));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, BopRoutePoint element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			dr.SetField(GridColumn.Number, element.Number);
			dr.SetField(GridColumn.Name, element.Name);
			dr.SetField(GridColumn.Type, element.Type);
			dr.SetField(GridColumn.Action, element.Action);
			dr.SetField(GridColumn.Altitude, $"{element.GetAltitude(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}");
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			m_dgv.Columns[GridColumn.Altitude].HeaderText = $"Altitude ({ToolsBriefop.GetUnitAltitude(PreferencesManager.Preferences.Briefing.MeasurementSystem)})";
		}
		#endregion

		#region Events
		#endregion
	}
}
