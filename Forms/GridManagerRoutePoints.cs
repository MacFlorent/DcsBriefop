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
			public static readonly string Distance = "Distance";
			public static readonly string TrackM = "Track°M";
			public static readonly string TrackT = "Track°T";
			public static readonly string Speed = "Speed";

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
			m_dtSource.Columns.Add(GridColumn.Altitude, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Distance, typeof(string));
			m_dtSource.Columns.Add(GridColumn.TrackM, typeof(string));
			m_dtSource.Columns.Add(GridColumn.TrackT, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Speed, typeof(string));
		}

		protected override void RefreshDataSourceRowContent(DataRow dr, BopRoutePoint element)
		{
			base.RefreshDataSourceRowContent(dr, element);

			dr.SetField(GridColumn.Number, element.Number);
			dr.SetField(GridColumn.Name, element.Name);
			dr.SetField(GridColumn.Type, element.Type);
			dr.SetField(GridColumn.Action, element.Action);
			dr.SetField(GridColumn.Altitude, $"{element.GetAltitude(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}");
			dr.SetField(GridColumn.Distance, $"{element.GetDistance(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}");
			dr.SetField(GridColumn.TrackM, $"{element.GetTrack(true):000}");
			dr.SetField(GridColumn.TrackT, $"{element.GetTrack(false):000}");
			dr.SetField(GridColumn.Speed, $"{element.GetSpeed(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}");
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			m_dgv.Columns[GridColumn.Altitude].HeaderText = $"Altitude ({ToolsMeasurement.AltitudeUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem)})";
			m_dgv.Columns[GridColumn.Distance].HeaderText = $"Distance ({ToolsMeasurement.DistanceUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem)})";
			m_dgv.Columns[GridColumn.Distance].HeaderText = $"Distance ({ToolsMeasurement.DistanceUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem)})";
			m_dgv.Columns[GridColumn.Speed].HeaderText = $"Speed ({ToolsMeasurement.SpeedUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem)})";
		}
		#endregion

		#region Events
		#endregion
	}
}
