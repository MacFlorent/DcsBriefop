using DcsBriefop.Data;
using UnitsNet;

namespace DcsBriefop.Tools
{
	internal static class ToolsMeasurement
	{
		#region Altitude
		public static decimal AltitudeDisplay(decimal dAltitudeMeters, ElementMeasurementSystem measurementSystem)
		{
			if (measurementSystem == ElementMeasurementSystem.Metric)
				return dAltitudeMeters;
			else
				return Convert.ToDecimal(UnitConverter.Convert(dAltitudeMeters, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot));
		}

		public static decimal AltitudeData(decimal dAltitude, ElementMeasurementSystem measurementSystem)
		{
			if (measurementSystem == ElementMeasurementSystem.Metric)
				return dAltitude;
			else
				return Convert.ToDecimal(UnitConverter.Convert(dAltitude, UnitsNet.Units.LengthUnit.Foot, UnitsNet.Units.LengthUnit.Meter));
		}

		public static string AltitudeUnit(ElementMeasurementSystem measurementSystem)
		{
			return measurementSystem == ElementMeasurementSystem.Metric ? "m" : "ft";
		}
		#endregion

		#region Distance
		public static int DistanceDisplay(int iDistanceMeters, ElementMeasurementSystem measurementSystem)
		{
			if (measurementSystem == ElementMeasurementSystem.Metric)
				return Convert.ToInt32(UnitConverter.Convert(iDistanceMeters, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Kilometer));
			else
				return Convert.ToInt32(UnitConverter.Convert(iDistanceMeters, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.NauticalMile));
		}

		public static string DistanceUnit(ElementMeasurementSystem measurementSystem)
		{
			return measurementSystem == ElementMeasurementSystem.Metric ? "km" : "nm";
		}
		#endregion

		#region Visiblity
		public static int VisibilityDisplay(int iVisibilityMeters, ElementMeasurementSystem measurementSystem)
		{
			if (measurementSystem == ElementMeasurementSystem.Imperial)
				return Convert.ToInt32(UnitConverter.Convert(iVisibilityMeters, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Mile));
			else
				return Convert.ToInt32(UnitConverter.Convert(iVisibilityMeters, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Kilometer));
		}

		public static string VisibilityUnit(ElementMeasurementSystem measurementSystem)
		{
			return measurementSystem == ElementMeasurementSystem.Imperial ? "SM" : "km";
		}
		#endregion

		#region Speed
		public static decimal SpeedDisplay(decimal dSpeedMs, ElementMeasurementSystem measurementSystem)
		{
			if (measurementSystem == ElementMeasurementSystem.Metric)
				return Convert.ToDecimal(UnitConverter.Convert(dSpeedMs, UnitsNet.Units.SpeedUnit.MeterPerSecond, UnitsNet.Units.SpeedUnit.KilometerPerHour));
			else
				return Convert.ToDecimal(UnitConverter.Convert(dSpeedMs, UnitsNet.Units.SpeedUnit.MeterPerSecond, UnitsNet.Units.SpeedUnit.Knot));
		}

		public static string SpeedUnit(ElementMeasurementSystem measurementSystem)
		{
			return measurementSystem == ElementMeasurementSystem.Metric ? "km/h" : "kts";
		}
		#endregion

		#region Temperature
		public static decimal TemperatureDisplay(decimal dTemperatureCelcius, ElementMeasurementSystem measurementSystem)
		{
			if (measurementSystem == ElementMeasurementSystem.Imperial)
				return Convert.ToDecimal(UnitConverter.Convert(dTemperatureCelcius, UnitsNet.Units.TemperatureUnit.DegreeCelsius, UnitsNet.Units.TemperatureUnit.DegreeFahrenheit));
			else
				return dTemperatureCelcius;
		}

		public static string TemperatureUnit(ElementMeasurementSystem measurementSystem)
		{
			return measurementSystem == ElementMeasurementSystem.Imperial ? "°F" : "°C";
		}
		#endregion
	}
}
