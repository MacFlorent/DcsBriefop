using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System.Text;
using UnitsNet;

namespace DcsBriefop.DataBopMission
{
	internal class BopWeather : BaseBop
	{
		#region Fields
		#endregion

		#region Properties
		public DateTime Date { get; set; }
		public WeatherPreset Preset { get; private set; }

		public BopWeatherWind WindGround { get; private set; }
		public BopWeatherWind Wind2000 { get; private set; }
		public BopWeatherWind Wind8000 { get; private set; }

		public int CloudDensityOkta { get; private set; }
		public int CloudBaseMeter { get; private set; }
		public int VisibilityMeter { get; private set; }
		public bool Fog { get; private set; }
		public bool Dust { get; private set; }
		public bool Precipitation { get; private set; }

		public decimal QnhMmHg { get; private set; }
		public decimal QnhInHg
		{
			get { return Convert.ToDecimal(UnitConverter.Convert(QnhMmHg, UnitsNet.Units.PressureUnit.MillimeterOfMercury, UnitsNet.Units.PressureUnit.InchOfMercury)); }
		}
		public decimal QnhHpa
		{
			get { return Convert.ToDecimal(UnitConverter.Convert(QnhMmHg, UnitsNet.Units.PressureUnit.MillimeterOfMercury, UnitsNet.Units.PressureUnit.Hectopascal)); }
		}

		public decimal TemperatureCelcius { get; private set; }
		public int TemperatureFarenheit
		{
			get { return Convert.ToInt32(UnitConverter.Convert(TemperatureCelcius, UnitsNet.Units.TemperatureUnit.DegreeCelsius, UnitsNet.Units.TemperatureUnit.DegreeFahrenheit)); }
		}
		#endregion

		#region CTOR
		public BopWeather(Miz miz, Theatre theatre, DateTime date) : base(miz, theatre)
		{
			Date = date;
			MizWeather mizWeather = Miz.RootMission.Weather;

			Preset = null;
			if (!string.IsNullOrEmpty(mizWeather.Cloud.Preset) && WeatherPreset.WeatherPresets.TryGetValue(mizWeather.Cloud.Preset, out WeatherPreset wp))
				Preset = wp;

			WindGround = new BopWeatherWind(mizWeather.WindAtGround);
			Wind2000 = new BopWeatherWind(mizWeather.WindAt2000);
			Wind8000 = new BopWeatherWind(mizWeather.WindAt8000);

			VisibilityMeter = mizWeather.VisibilityDistance;
			if (mizWeather.Fog is object && mizWeather.Fog.Thickness > 100)
			{
				Fog = true;
				if (mizWeather.Fog.Visibility < VisibilityMeter)
					VisibilityMeter = (int)mizWeather.Fog.Visibility;
			}

			if (Preset is object)
			{
				CloudDensityOkta = Preset.Density;
				CloudBaseMeter = (int)mizWeather.Cloud.Base;
				if (Preset.Visibility is object && Preset.Visibility < VisibilityMeter)
					VisibilityMeter = Preset.Visibility.Value;

				Precipitation = Preset.Precipitation;
			}
			else
			{
				if (mizWeather.Cloud.Density < 10)
					CloudDensityOkta = 0;
				else
					CloudDensityOkta = mizWeather.Cloud.Density * 8 / 10; // density in mission editor is on a scale of 10

				CloudBaseMeter = (int)mizWeather.Cloud.Base;
				Precipitation = mizWeather.Cloud.Precipitations > 0;
			}

			Dust = mizWeather.Dust;

			QnhMmHg = mizWeather.Qnh;
			TemperatureCelcius = mizWeather.Temperature;
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();
		}
		#endregion

		#region Methods
		public string ToString(ElementWeatherDisplay weatherDisplay, ElementMeasurementSystem measurementSystem)
		{
			if (weatherDisplay == ElementWeatherDisplay.Plain)
				return ToStringPlain(measurementSystem);
			else if (weatherDisplay == ElementWeatherDisplay.Metar)
				return ToStringMetar(measurementSystem);
			else
				return "";
		}

		public string ToStringPlain(ElementMeasurementSystem measurementSystem)
		{
			return ToStringPlain(measurementSystem, Environment.NewLine);
		}

		public string ToStringPlain(ElementMeasurementSystem measurementSystem, string sNewLine)
		{
			StringBuilder sb = new StringBuilder();
			// wind
			sb.AppendWithSeparator($"Wind: {ToStringPlain_Wind(measurementSystem, WindGround)} - {ToStringPlain_Wind(measurementSystem, Wind2000)} - {ToStringPlain_Wind(measurementSystem, Wind8000)}", sNewLine);

			// visibility and clouds
			int iVisibility;
			if (measurementSystem == ElementMeasurementSystem.Imperial)
			{
				iVisibility = Convert.ToInt32(UnitConverter.Convert(VisibilityMeter, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Mile));
				if (iVisibility > 6)
					iVisibility = 6;
			}
			else
			{
				iVisibility = Convert.ToInt32(UnitConverter.Convert(VisibilityMeter, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Kilometer));
				if (iVisibility > 10)
					iVisibility = 10;
			}

			sb.AppendWithSeparator($"Visibility {iVisibility} {ToolsBriefop.GetUnitVisibility(measurementSystem)}", sNewLine);
			if (Precipitation)
				sb.Append(" precipitations");
			if (Fog)
				sb.Append(" fog");
			if (Dust)
				sb.Append(" dust");

			if (CloudDensityOkta <= 0)
				sb.Append(" - No clouds");
			else
			{
				string sDensity;
				if (CloudDensityOkta <= 2)
					sDensity = "Few";
				else if (CloudDensityOkta <= 4)
					sDensity = "Scattered";
				else if (CloudDensityOkta <= 6)
					sDensity = "Broken";
				else
					sDensity = "Overcast";

				int iCloudBaseRounded;
				if (measurementSystem == ElementMeasurementSystem.Imperial)
					iCloudBaseRounded = Convert.ToInt32(UnitConverter.Convert(CloudBaseMeter, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot));
				else
					iCloudBaseRounded = CloudBaseMeter;

				iCloudBaseRounded = iCloudBaseRounded / 1000 * 1000;
				sb.Append($" - {sDensity} clouds at {iCloudBaseRounded} {ToolsBriefop.GetUnitAltitude(measurementSystem)}");
			}

			//T° and QNH
			int iTemperature;
			if (measurementSystem == ElementMeasurementSystem.Imperial)
				iTemperature = Convert.ToInt32(UnitConverter.Convert(TemperatureCelcius, UnitsNet.Units.TemperatureUnit.DegreeCelsius, UnitsNet.Units.TemperatureUnit.DegreeFahrenheit));
			else
				iTemperature = Convert.ToInt32(TemperatureCelcius);

			sb.AppendWithSeparator($"{iTemperature:0}{ToolsBriefop.GetUnitTemperature(measurementSystem)} - QNH {QnhHpa:0} hPa - {QnhInHg:00.00} inHg", sNewLine);

			return sb.ToString();
		}

		private string ToStringPlain_Wind(ElementMeasurementSystem measurementSystem, BopWeatherWind ww)
		{
			int iAltitude;
			double dSpeed;
			if (measurementSystem == ElementMeasurementSystem.Imperial)
			{
				iAltitude = Convert.ToInt32(UnitConverter.Convert(ww.Altitude, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot));
				dSpeed = UnitConverter.Convert(ww.SpeedMs, UnitsNet.Units.SpeedUnit.MeterPerSecond, UnitsNet.Units.SpeedUnit.Knot);
			}
			else
			{
				iAltitude = ww.Altitude;
				dSpeed = UnitConverter.Convert(ww.SpeedMs, UnitsNet.Units.SpeedUnit.MeterPerSecond, UnitsNet.Units.SpeedUnit.KilometerPerHour);

			}

			return $"{iAltitude} {ToolsBriefop.GetUnitAltitude(measurementSystem)}: {ww.DirectionTrue:000}° @ {dSpeed:00} {ToolsBriefop.GetUnitSpeed(measurementSystem)}";
		}

		public string ToStringMetar(ElementMeasurementSystem measurementsystem)
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendWithSeparator($"{Date.Day:00}{Date.Hour:00}{Date.Minute:00}L", " ");

			double dSpeedKnot = UnitConverter.Convert(WindGround.SpeedMs, UnitsNet.Units.SpeedUnit.MeterPerSecond, UnitsNet.Units.SpeedUnit.Knot);
			if (dSpeedKnot < 1)
				sb.AppendWithSeparator("00000KT", " ");
			else
				sb.AppendWithSeparator($"{WindGround.DirectionTrue:000}{dSpeedKnot:0}KT", " ");

			int iCloudBaseFoot = Convert.ToInt32(UnitConverter.Convert(CloudBaseMeter, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot));
			if (VisibilityMeter >= 10000 && iCloudBaseFoot >= 5000 && !Precipitation && !Fog && !Dust)
				sb.Append(" CAVOK");
			else
			{
				int iVisibilityDistance = VisibilityMeter;
				if (iVisibilityDistance > 9999)
					iVisibilityDistance = 9999;

				sb.AppendWithSeparator($"{iVisibilityDistance}", " ");
				if (Precipitation)
					sb.Append(" RA");
				if (Fog)
					sb.Append(" FG");
				if (Dust)
					sb.Append(" DU");

				if (CloudDensityOkta <= 0)
					sb.Append(" SKC");
				else
				{
					int iCloudBaseRounded = iCloudBaseFoot / 100;

					string sDensity;
					if (CloudDensityOkta <= 2)
						sDensity = "FEW";
					else if (CloudDensityOkta <= 4)
						sDensity = "SCT";
					else if (CloudDensityOkta <= 6)
						sDensity = "BKN";
					else
						sDensity = "OVC";

					sb.AppendWithSeparator($"{sDensity}{iCloudBaseRounded:000}", " ");
				}
			}

			sb.Append($" {TemperatureCelcius:0} Q{QnhHpa:0}/{QnhInHg:00.00}");
			
			return sb.ToString();
		}
		#endregion
	}

	internal class BopWeatherWind
	{
		public decimal SpeedMs { get; private set; }
		public int Altitude { get; private set; }
		public int DirectionTrue { get; private set; } // Direction the wind is coming from

		public BopWeatherWind(MizWeatherWind lson)
		{
			SpeedMs = lson.Speed;
			Altitude = lson.Altitude;
			DirectionTrue = ((int)Math.Round(lson.Direction) + 180) % 360;
		}
	}
}
