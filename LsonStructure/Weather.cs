using LsonLib;

namespace DcsBriefop.LsonStructure
{
	internal class Weather : BaseLsonStructure
	{
		private class LuaNode
		{
			public static readonly string AtmosphereType = "atmosphere_type";
			public static readonly string TypeWeather = "type_weather";
			public static readonly string GroundTurbulence = "groundTurbulence";
			public static readonly string Visibility = "visibility";
			public static readonly string VisibilityDistance = "distance";
			public static readonly string Season = "season";
			public static readonly string Temperature = "temperature";
			public static readonly string Qnh = "qnh";
			public static readonly string DustEnable = "enable_dust";
			public static readonly string Wind = "wind";
			public static readonly string WindAtGround = "atGround";
			public static readonly string WindAt2000 = "at2000";
			public static readonly string WindAt8000 = "at8000";
			public static readonly string Clouds = "clouds";
			public static readonly string FogEnable = "enable_fog";
			public static readonly string Fog = "fog";
		}

		public int AtmosphereType { get; set; } // To check - seems to be 0=static, 1=dynamic
		public int TypeWeather { get; set; } // To check
		public decimal GroundTurbulence { get; set; }
		public int VisibilityDistance { get; set; } // Visibility distance in meters
		public decimal Temperature { get; set; } // Temperature in degrees celcius
		public decimal Qnh { get; set; } // Qnh in mmHg
		public bool Dust { get; set; }

		public WeatherWind WindAtGround { get; set; }
		public WeatherWind WindAt2000 { get; set; }
		public WeatherWind WindAt8000 { get; set; }
		
		public WeatherCloud Cloud { get; set; }
		public WeatherFog Fog { get; set; }

		public Weather(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			AtmosphereType = m_lsd[LuaNode.AtmosphereType].GetInt();
			TypeWeather = m_lsd[LuaNode.TypeWeather].GetInt();
			GroundTurbulence = m_lsd[LuaNode.GroundTurbulence].GetDecimal();
			VisibilityDistance = m_lsd[LuaNode.Visibility][LuaNode.VisibilityDistance].GetInt();
			Temperature = m_lsd[LuaNode.Season][LuaNode.Temperature].GetDecimal();
			Qnh = m_lsd[LuaNode.Qnh].GetDecimal();
			Dust = m_lsd[LuaNode.DustEnable].GetBool();

			LsonDict lsdWinds = m_lsd[LuaNode.Wind].GetDict();
			WindAtGround = new WeatherWind(lsdWinds[LuaNode.WindAtGround].GetDict());
			WindAt2000 = new WeatherWind(lsdWinds[LuaNode.WindAt2000].GetDict());
			WindAt8000 = new WeatherWind(lsdWinds[LuaNode.WindAt8000].GetDict());

			Cloud = new WeatherCloud(m_lsd[LuaNode.Clouds].GetDict());

			if (m_lsd[LuaNode.FogEnable].GetBool())
				Fog = new WeatherFog(m_lsd[LuaNode.Fog].GetDict());
			else
				Fog = null;
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.AtmosphereType] = AtmosphereType;
			m_lsd[LuaNode.TypeWeather] = TypeWeather;
			m_lsd[LuaNode.GroundTurbulence] = GroundTurbulence;
			m_lsd[LuaNode.Visibility][LuaNode.VisibilityDistance] = VisibilityDistance;
			m_lsd[LuaNode.Season][LuaNode.Temperature] = Temperature;
			m_lsd[LuaNode.Qnh] = Qnh;
			m_lsd[LuaNode.DustEnable] = Dust;

			WindAtGround.ToLua();
			WindAt2000.ToLua();
			WindAt8000.ToLua();

			Cloud.ToLua();

			if (Fog is object)
			{
				m_lsd[LuaNode.FogEnable] = true;
				Fog.ToLua();
			}
			else
			{
				m_lsd[LuaNode.FogEnable] = false;
			}
		}
	}

	internal class WeatherWind : BaseLsonStructure
	{
		private class LuaNode
		{
			public static readonly string Speed = "speed";
			public static readonly string Direction = "dir";
		}

		public decimal Speed { get; set; } // Speed in m/s
		public int Direction { get; set; } // Direction the wind is goind towards, in degrees true

		public WeatherWind(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Speed = m_lsd[LuaNode.Speed].GetDecimal();
			Direction = m_lsd[LuaNode.Direction].GetInt();
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Speed] = Speed;
			m_lsd[LuaNode.Direction] = Direction;
		}
	}

	internal class WeatherCloud : BaseLsonStructure
	{
		private class LuaNode
		{
			public static readonly string Preset = "preset";
			public static readonly string Density = "density";
			public static readonly string Thickness = "thickness";
			public static readonly string Base = "base";
			public static readonly string Precipitations = "iprecptns";
		}

		public string Preset { get; set; }
		public int Density { get; set; } // Density over a 1-10 scale (does not apply with presets)
		public int Thickness { get; set; } // Thickness in meters (does not apply with presets)
		public int Base { get; set; } // Base in meters
		public int Precipitations { get; set; } // 0=none, 1=rain (does not apply with presets)

		public WeatherCloud(LsonDict lsd) : base(lsd) { }
		
		public override void FromLua()
		{
			if (m_lsd.ContainsKey(LuaNode.Preset))
				Preset = m_lsd[LuaNode.Preset].GetString();
			Density = m_lsd[LuaNode.Density].GetInt();
			Thickness = m_lsd[LuaNode.Thickness].GetInt();
			Base = m_lsd[LuaNode.Base].GetInt();
			Precipitations = m_lsd[LuaNode.Precipitations].GetInt();
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Preset] = Preset;
			m_lsd[LuaNode.Density] = Density;
			m_lsd[LuaNode.Thickness] = Thickness;
			m_lsd[LuaNode.Base] = Base;
			m_lsd[LuaNode.Precipitations] = Precipitations;
		}
	}

	internal class WeatherFog : BaseLsonStructure
	{
		private class LuaNode
		{
			public static readonly string Thickness = "thickness";
			public static readonly string Visibility = "visibility";
		}

		public int Thickness { get; set; } // Thickness in meters
		public int Visibility { get; set; } // Visibility in meters

		public WeatherFog(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Thickness = m_lsd[LuaNode.Thickness].GetInt();
			Visibility = m_lsd[LuaNode.Visibility].GetInt();
		}

		public override void ToLua()
		{
			m_lsd[LuaNode.Thickness] = Thickness;
			m_lsd[LuaNode.Visibility] = Visibility;
		}
	}
}
