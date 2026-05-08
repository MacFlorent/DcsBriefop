using BrightIdeasSoftware;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

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
			public static readonly string Track = "Track";
			public static readonly string Speed = "Speed";
		}
		#endregion

		#region CTOR
		public GridManagerRoutePoints(FastObjectListView dgv, IEnumerable<BopRoutePoint> routePoints) : base(dgv, routePoints) { }
		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			string sAltitudeUnit = ToolsMeasurement.AltitudeUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem);
			string sDistanceUnit = ToolsMeasurement.DistanceUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem);
			string sSpeedUnit = ToolsMeasurement.SpeedUnit(PreferencesManager.Preferences.Briefing.MeasurementSystem);

			m_dgv.AllColumns.AddRange(new OLVColumn[]
			{
				new OLVColumn { Text = "Number", Name = GridColumn.Number, Width = GridWidth.Small, AspectGetter = obj => ((BopRoutePoint)obj).Number },
				new OLVColumn { Text = "Name", Name = GridColumn.Name, Width = GridWidth.Large, AspectGetter = obj => ((BopRoutePoint)obj).Name },
				new OLVColumn { Text = "Type", Name = GridColumn.Type, Width = GridWidth.Medium, AspectGetter = obj => ((BopRoutePoint)obj).Type },
				new OLVColumn { Text = "Action", Name = GridColumn.Action, Width = GridWidth.Medium, AspectGetter = obj => ((BopRoutePoint)obj).Action },
				new OLVColumn { Text = $"Altitude ({sAltitudeUnit})", Name = GridColumn.Altitude, Width = GridWidth.Medium, AspectGetter = obj =>
				{
					BopRoutePoint rp = (BopRoutePoint)obj;
					return $"{rp.GetAltitude(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}";
				}},
				new OLVColumn { Text = $"Distance ({sDistanceUnit})", Name = GridColumn.Distance, Width = GridWidth.Medium, AspectGetter = obj =>
				{
					BopRoutePoint rp = (BopRoutePoint)obj;
					return $"{rp.GetDistance(PreferencesManager.Preferences.Briefing.MeasurementSystem):0}";
				}},
				new OLVColumn { Text = "Track", Name = GridColumn.Track, Width = GridWidth.Medium, AspectGetter = obj =>
				{
					BopRoutePoint rp = (BopRoutePoint)obj;
					double? dTrackTrue = rp.GetTrack(false);
					double? dTrackMagnetic = rp.GetTrack(true);
					if (dTrackTrue is not null && dTrackMagnetic is not null)
						return $"{rp.GetTrack(true):000}°M / {rp.GetTrack(false):000}°T";
					return "";
				}},
				new OLVColumn { Text = $"Speed ({sSpeedUnit})", Name = GridColumn.Speed, Width = GridWidth.ExtraLarge, AspectGetter = obj =>
				{
					BopRoutePoint rp = (BopRoutePoint)obj;
					return $"{rp.GetSpeedTrue(PreferencesManager.Preferences.Briefing.MeasurementSystem):0} TAS / {rp.GetSpeedCalibrated(PreferencesManager.Preferences.Briefing.MeasurementSystem):0} CAS / {rp.GetSpeedMach():0.00} M";
				}},
			});
			m_dgv.RebuildColumns();
		}
		#endregion
	}
}
