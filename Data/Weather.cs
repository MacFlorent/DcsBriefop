using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Text;

namespace DcsBriefop.Data
{
	internal class Weather
	{
		public WeatherPreset Preset { get; private set; }

		public WeatherWind WindGround { get; private set; }
		public WeatherWind Wind2000 { get; private set; }
		public WeatherWind Wind8000 { get; private set; }

		public int CloudDensityOkta { get; private set; }
		public int CloudBaseMeter { get; private set; }
		public int CloudBaseFoot
		{
			get { return Convert.ToInt32(UnitsNet.UnitConverter.Convert(CloudBaseMeter, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot)); }
		}

		public int VisibilityMeter { get; private set; }
		public int VisibilityStatuteMile
		{
			get { return Convert.ToInt32(UnitsNet.UnitConverter.Convert(VisibilityMeter, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Mile)); }
		}
		public bool Fog { get; private set; }
		public bool Dust { get; private set; }
		public bool Precipitation { get; private set; }

		public decimal QnhMmHg { get; private set; }
		public decimal QnhInHg
		{
			get { return Convert.ToDecimal(UnitsNet.UnitConverter.Convert(QnhMmHg, UnitsNet.Units.PressureUnit.MillimeterOfMercury, UnitsNet.Units.PressureUnit.InchOfMercury)); }
		}
		public decimal QnhHpa
		{
			get { return Convert.ToDecimal(UnitsNet.UnitConverter.Convert(QnhMmHg, UnitsNet.Units.PressureUnit.MillimeterOfMercury, UnitsNet.Units.PressureUnit.Hectopascal)); }
		}

		public decimal TemperatureCelcius { get; private set; }
		public int TemperatureFarenheit
		{
			get { return Convert.ToInt32(UnitsNet.UnitConverter.Convert(TemperatureCelcius, UnitsNet.Units.TemperatureUnit.DegreeCelsius, UnitsNet.Units.TemperatureUnit.DegreeFahrenheit)); }
		}

		public Weather(DataMiz.MizWeather mizWeather)
		{
			Preset = null;
			if (WeatherPreset.WeatherPresets.TryGetValue(mizWeather.Cloud.Preset, out WeatherPreset wp))
				Preset = wp;

			WindGround = new WeatherWind(mizWeather.WindAtGround);
			Wind2000= new WeatherWind(mizWeather.WindAt2000);
			Wind8000 = new WeatherWind(mizWeather.WindAt8000);

			VisibilityMeter = mizWeather.VisibilityDistance;
			if (mizWeather.Fog is object && mizWeather.Fog.Thickness > 100)
			{
				Fog = true;
				if (mizWeather.Fog.Visibility < VisibilityMeter)
					VisibilityMeter = mizWeather.Fog.Visibility;
			}

			if (Preset is object)
			{
				CloudDensityOkta = Preset.Density;
				CloudBaseMeter = mizWeather.Cloud.Base;
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

				CloudBaseMeter = mizWeather.Cloud.Base;
				Precipitation = mizWeather.Cloud.Precipitations > 0;
			}

			Dust = mizWeather.Dust;

			QnhMmHg = mizWeather.Qnh;
			TemperatureCelcius = mizWeather.Temperature;
		}

		public override string ToString()
		{
			return ToString(Environment.NewLine);
		}

		public string ToString(string sNewLine)
		{
			StringBuilder sb = new StringBuilder();
			// wind
			sb.AppendWithSeparator(ToString_Wind(0, WindGround), sNewLine);
			sb.AppendWithSeparator(ToString_Wind(2000, Wind2000), sNewLine);
			sb.AppendWithSeparator(ToString_Wind(8000, Wind8000), sNewLine);

			// visibility and clouds
			int iVisibilityKilometer = VisibilityMeter / 1000;
			if (iVisibilityKilometer > 10)
				iVisibilityKilometer = 10;

			sb.AppendWithSeparator($"Visibility {iVisibilityKilometer} kilometers", sNewLine);
			if (Precipitation)
				sb.Append(" precipitations");
			if (Fog)
				sb.Append(" fog");
			if (Dust)
				sb.Append(" dust");

			if (CloudDensityOkta <= 0)
				sb.AppendWithSeparator($"No clouds", sNewLine);
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

				int CloudBaseRoundedFoot = CloudBaseFoot / 1000 * 1000;
				sb.AppendWithSeparator($"{sDensity} clouds at {CloudBaseRoundedFoot} feet", sNewLine);
			}

			//T° and QNH
			sb.Append($"Temperature {TemperatureCelcius:0}°C / QNH {QnhHpa:0} hPa - {QnhInHg:00.00} inHg");

			return sb.ToString();
		}

		private string ToString_Wind(int iAltitudeFoot, WeatherWind ww)
		{
			return $"Wind {iAltitudeFoot} feet : {ww.DirectionTrue:000}° @ {ww.SpeedKnot:00} kt";
		}
	}

	internal class WeatherWind
	{
		public decimal SpeedMs { get; private set; }
		public decimal SpeedKnot
		{
			get { return Convert.ToDecimal(UnitsNet.UnitConverter.Convert(SpeedMs, UnitsNet.Units.SpeedUnit.MeterPerSecond, UnitsNet.Units.SpeedUnit.Knot)); }
		}
		public int DirectionTrue { get; private set; } // Direction the wind is coming from

		public WeatherWind(DataMiz.MizWeatherWind lson)
		{
			SpeedMs = lson.Speed;
			DirectionTrue = (lson.Direction + 180) % 360;
		}
	}
}
