using DcsBriefop.MasterData;
using DcsBriefop.Tools;
using System;
using System.Text;

namespace DcsBriefop.Briefing
{
	internal class BriefingWeather : BaseBriefing
	{
		public WeatherPreset Preset { get; private set; }

		public BriefingWeatherWind WindGround { get; private set; }
		public BriefingWeatherWind Wind2000 { get; private set; }
		public BriefingWeatherWind Wind8000 { get; private set; }

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

		public BriefingWeather(BriefingPack bp) : base(bp)
		{
			Initialize();
		}

		private void Initialize()
		{
			LsonStructure.Weather lson = RootMission.Weather;

			Preset = null;
			if (WeatherPreset.WeatherPresets.TryGetValue(lson.Cloud.Preset, out WeatherPreset wp))
				Preset = wp;

			WindGround = new BriefingWeatherWind(lson.WindAtGround);
			Wind2000= new BriefingWeatherWind(lson.WindAt2000);
			Wind8000 = new BriefingWeatherWind(lson.WindAt8000);

			VisibilityMeter = lson.VisibilityDistance;
			if (lson.Fog is object && lson.Fog.Thickness > 100)
			{
				Fog = true;
				if (lson.Fog.Visibility < VisibilityMeter)
					VisibilityMeter = lson.Fog.Visibility;
			}

			if (Preset is object)
			{
				CloudDensityOkta = Preset.Density;
				CloudBaseMeter = lson.Cloud.Base;
				if (Preset.Visibility is object && Preset.Visibility < VisibilityMeter)
					VisibilityMeter = Preset.Visibility.Value;

				Precipitation = Preset.Precipitation;
			}
			else
			{
				if (lson.Cloud.Density < 10)
					CloudDensityOkta = 0;
				else
					CloudDensityOkta = lson.Cloud.Density * 8 / 10; // density in mission editor is on a scale of 10

				CloudBaseMeter = lson.Cloud.Base;
				Precipitation = lson.Cloud.Precipitations > 0;
			}

			Dust = lson.Dust;

			QnhMmHg = lson.Qnh;
			TemperatureCelcius = lson.Temperature;
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

				sb.AppendWithSeparator($"{sDensity} clouds at {CloudBaseFoot} feet", sNewLine);
			}

			//T° and QNH
			sb.Append($"Temperature {TemperatureCelcius}°C / QNH {QnhHpa} hPa - {QnhInHg} inHg");

			return sb.ToString();
		}

		private string ToString_Wind(int iAltitudeFoot, BriefingWeatherWind ww)
		{
			return $"Wind {iAltitudeFoot} feet : {ww.DirectionTrue}° @ {ww.SpeedKnot} kt";
		}
	}

	internal class BriefingWeatherWind
	{
		public decimal SpeedMs { get; private set; }
		public decimal SpeedKnot
		{
			get { return Convert.ToDecimal(UnitsNet.UnitConverter.Convert(SpeedMs, UnitsNet.Units.SpeedUnit.MeterPerSecond, UnitsNet.Units.SpeedUnit.Knot)); }
		}
		public int DirectionTrue { get; private set; } // Direction the wind is coming from

		public BriefingWeatherWind(LsonStructure.WeatherWind lson)
		{
			SpeedMs = lson.Speed;
			DirectionTrue = (lson.Direction + 180) % 360;
		}
	}
}
