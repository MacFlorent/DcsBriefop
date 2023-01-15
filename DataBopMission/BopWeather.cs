using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System;
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
		public int CloudBaseFoot
		{
			get { return Convert.ToInt32(UnitConverter.Convert(CloudBaseMeter, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot)); }
		}

		public int VisibilityMeter { get; private set; }
		public int VisibilityStatuteMile
		{
			get { return Convert.ToInt32(UnitConverter.Convert(VisibilityMeter, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Mile)); }
		}
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
		}
		#endregion

		#region Miz
		public override void FromMiz()
		{
			base.FromMiz();

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

		public override void ToMiz()
		{
			base.ToMiz();
		}
		#endregion

		#region Methods
		public string ToString(ElementWeatherDisplay weatherDisplay)
		{
			if (weatherDisplay == ElementWeatherDisplay.Plain)
				return ToStringPlain();
			else if (weatherDisplay == ElementWeatherDisplay.Metar)
				return ToStringMetar();
			else
				return "";
		}

		public string ToStringPlain()
		{
			return ToStringPlain(Environment.NewLine);
		}

		public string ToStringPlain(string sNewLine)
		{
			StringBuilder sb = new StringBuilder();
			// wind
			sb.AppendWithSeparator($"Wind: {ToStringPlain_Wind(0, WindGround)} - {ToStringPlain_Wind(2000, Wind2000)} - {ToStringPlain_Wind(8000, Wind8000)}", sNewLine);

			// visibility and clouds
			int iVisibilityKilometer = VisibilityMeter / 1000;
			if (iVisibilityKilometer > 10)
				iVisibilityKilometer = 10;

			sb.AppendWithSeparator($"Visibility {iVisibilityKilometer} km", sNewLine);
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

				int CloudBaseRoundedFoot = CloudBaseFoot / 1000 * 1000;
				sb.Append($" - {sDensity} clouds at {CloudBaseRoundedFoot} ft");
			}

			//T° and QNH
			sb.AppendWithSeparator($"{TemperatureCelcius:0}°C - QNH {QnhHpa:0} hPa - {QnhInHg:00.00} inHg", sNewLine);

			return sb.ToString();
		}

		private string ToStringPlain_Wind(int iAltitudeFoot, BopWeatherWind ww)
		{
			return $"{iAltitudeFoot} ft: {ww.DirectionTrue:000}° @ {ww.SpeedKnot:00} kt";
		}

		public string ToStringMetar()
		{
			StringBuilder sb = new StringBuilder();

			sb.AppendWithSeparator($"{Date.Day:00}{Date.Hour:00}{Date.Minute:00}L", " ");

			if (WindGround.SpeedKnot < 1)
				sb.AppendWithSeparator("00000KT", " ");
			else
				sb.AppendWithSeparator($"{WindGround.DirectionTrue:000}{WindGround.SpeedKnot:0}KT", " ");

			if (VisibilityMeter >= 10000 && CloudBaseFoot >= 5000 && !Precipitation && !Fog && !Dust)
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
					int iCloudBaseRounded = CloudBaseFoot / 100;

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
		public decimal SpeedKnot
		{
			get { return Convert.ToDecimal(UnitConverter.Convert(SpeedMs, UnitsNet.Units.SpeedUnit.MeterPerSecond, UnitsNet.Units.SpeedUnit.Knot)); }
		}
		public int DirectionTrue { get; private set; } // Direction the wind is coming from

		public BopWeatherWind(DataMiz.MizWeatherWind lson)
		{
			SpeedMs = lson.Speed;
			DirectionTrue = ((int)Math.Round(lson.Direction) + 180) % 360;
		}
	}
}
