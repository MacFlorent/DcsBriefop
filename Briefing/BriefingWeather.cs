using DcsBriefop.MasterData;
using System;

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

		public int TemperatureCelcius { get; private set; }
		public int TemperatureFarenheit
		{
			get { return Convert.ToInt32(UnitsNet.UnitConverter.Convert(TemperatureCelcius, UnitsNet.Units.TemperatureUnit.DegreeCelsius, UnitsNet.Units.TemperatureUnit.DegreeFahrenheit)); }
		}

		public BriefingWeather(MissionManager manager) : base(manager)
		{
			Initialize();
		}

		private void Initialize()
		{
			LsonStructure.Weather lson = m_manager.RootMission.Weather;

			Preset = null;
			if (MasterData.WeatherPreset.WeatherPresets.TryGetValue(lson.Cloud.Preset, out MasterData.WeatherPreset wp))
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
		}

		//public override string ToString()
		//{
		//	StringBuilder sb = new StringBuilder();
		//	// wind
		//	foreach (WeatherWind ww in Winds)
		//	{
		//		int iDirectionFromTrue = (ww.DirectionToTrue + 180) % 360;
		//		int iSpeedKnots = (int)Math.Round(ww.SpeedMs * ToolsMisc.MsToKnots, 0);
		//		int iAltitudeFeet = (int)Math.Round(ww.AltitudeMeters * ToolsMisc.MetersToFeet, 0);
		//		sb.AppendWithSeparator($"Wind {iAltitudeFeet} feet : {iDirectionFromTrue}° @ {iSpeedKnots} kt", "\n");
		//	}
		//	// visibility and clouds
		//	int iVisibilityMeters = VisibilityMeters;
		//	bool bFog = false; bool bPrecipitations = false;
		//	if (Fog is object && Fog.ThicknessMeters > 100)
		//	{
		//		bFog = true;
		//		if (Fog.VisibilityMeters < iVisibilityMeters)
		//			iVisibilityMeters = Fog.VisibilityMeters;
		//	}

		//	int iCloudDensityOctas, iCloudBaseMeters;
		//	if (WeatherCloudPreset.WeatherCloudPresets.TryGetValue(Cloud.PresetName, out WeatherCloudPreset cp))
		//	{
		//		iCloudDensityOctas = cp.Density;
		//		iCloudBaseMeters = Cloud.BaseMeters;
		//		if (cp.VisibilityMeters is object && cp.VisibilityMeters < iVisibilityMeters)
		//			iVisibilityMeters = cp.VisibilityMeters.Value;

		//		bPrecipitations = cp.Precipitation;
		//	}
		//	else
		//	{
		//		iCloudDensityOctas = Cloud.Density * 8 / 10; // density in mission editor is on a scale of 10
		//		iCloudBaseMeters = Cloud.BaseMeters;
		//		bPrecipitations = Cloud.Precipitations > 0;
		//	}

		//	int iVisibilityKilometers = iVisibilityMeters / 1000;
		//	if (iVisibilityKilometers > 10)
		//		iVisibilityKilometers = 10;
		//	int iCloudBaseFeet = (int)Math.Round(iCloudBaseMeters * ToolsMisc.MetersToFeet);
		//	iCloudBaseFeet = (iCloudBaseFeet / 1000) * 1000;

		//	sb.AppendWithSeparator($"Visibility {iVisibilityKilometers} kilometers", "\n");
		//	if (bPrecipitations)
		//		sb.Append(" precipitations");
		//	if (bFog)
		//		sb.Append(" fog");
		//	if (Dust)
		//		sb.Append(" dust");

		//	if (iCloudDensityOctas <= 0 || Cloud.ThicknessMeters < 10)
		//		sb.AppendWithSeparator($"No clouds", "\n");
		//	else
		//	{
		//		string sDensity;
		//		if (iCloudDensityOctas <= 2)
		//			sDensity = "Few";
		//		else if (iCloudDensityOctas <= 4)
		//			sDensity = "Scattered";
		//		else if (iCloudDensityOctas <= 6)
		//			sDensity = "Broken";
		//		else
		//			sDensity = "Overcast";

		//		sb.AppendWithSeparator($"{sDensity} clouds at {iCloudBaseFeet} feet", "\n");
		//	}

		//	//T° and QNH
		//	int iQnhHpa = (int)Math.Round(QnhMmHg * ToolsMisc.MmHgToHpa);
		//	decimal dQnhInHg = Math.Round(QnhMmHg * ToolsMisc.MmHgToInH, 2);
		//	sb.AppendWithSeparator($"Temperature {TemperatureCelcius}°C / QNH {iQnhHpa} hPa - {dQnhInHg} inHg", "\n");

		//	return sb.ToString();
		//}
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
