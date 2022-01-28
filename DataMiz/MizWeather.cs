using LsonLib;

namespace DcsBriefop.DataMiz
{
	internal class MizWeather : BaseMiz
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

		public MizWeatherWind WindAtGround { get; set; }
		public MizWeatherWind WindAt2000 { get; set; }
		public MizWeatherWind WindAt8000 { get; set; }
		
		public MizWeatherCloud Cloud { get; set; }
		public MizWeatherFog Fog { get; set; }

		public MizWeather(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			AtmosphereType = Lsd[LuaNode.AtmosphereType].GetInt();
			TypeWeather = Lsd[LuaNode.TypeWeather].GetInt();
			GroundTurbulence = Lsd[LuaNode.GroundTurbulence].GetDecimal();
			VisibilityDistance = Lsd[LuaNode.Visibility][LuaNode.VisibilityDistance].GetInt();
			Temperature = Lsd[LuaNode.Season][LuaNode.Temperature].GetDecimal();
			Qnh = Lsd[LuaNode.Qnh].GetDecimal();
			Dust = Lsd[LuaNode.DustEnable].GetBool();

			LsonDict lsdWinds = Lsd[LuaNode.Wind].GetDict();
			WindAtGround = new MizWeatherWind(lsdWinds[LuaNode.WindAtGround].GetDict());
			WindAt2000 = new MizWeatherWind(lsdWinds[LuaNode.WindAt2000].GetDict());
			WindAt8000 = new MizWeatherWind(lsdWinds[LuaNode.WindAt8000].GetDict());

			Cloud = new MizWeatherCloud(Lsd[LuaNode.Clouds].GetDict());

			if (Lsd[LuaNode.FogEnable].GetBool())
				Fog = new MizWeatherFog(Lsd[LuaNode.Fog].GetDict());
			else
				Fog = null;
		}

		public override void ToLua()
		{
			Lsd[LuaNode.AtmosphereType] = AtmosphereType;
			Lsd[LuaNode.TypeWeather] = TypeWeather;
			Lsd[LuaNode.GroundTurbulence] = GroundTurbulence;
			Lsd[LuaNode.Visibility][LuaNode.VisibilityDistance] = VisibilityDistance;
			Lsd[LuaNode.Season][LuaNode.Temperature] = Temperature;
			Lsd[LuaNode.Qnh] = Qnh;
			Lsd[LuaNode.DustEnable] = Dust;

			WindAtGround.ToLua();
			WindAt2000.ToLua();
			WindAt8000.ToLua();

			Cloud.ToLua();

			if (Fog is object)
			{
				Lsd[LuaNode.FogEnable] = true;
				Fog.ToLua();
			}
			else
			{
				Lsd[LuaNode.FogEnable] = false;
			}
		}
	}

	internal class MizWeatherWind : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Speed = "speed";
			public static readonly string Direction = "dir";
		}

		public decimal Speed { get; set; } // Speed in m/s
		public int Direction { get; set; } // Direction the wind is goind towards, in degrees true

		public MizWeatherWind(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Speed = Lsd[LuaNode.Speed].GetDecimal();
			Direction = Lsd[LuaNode.Direction].GetInt();
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Speed] = Speed;
			Lsd[LuaNode.Direction] = Direction;
		}
	}

	internal class MizWeatherCloud : BaseMiz
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

		public MizWeatherCloud(LsonDict lsd) : base(lsd) { }
		
		public override void FromLua()
		{
			if (Lsd.ContainsKey(LuaNode.Preset))
				Preset = Lsd[LuaNode.Preset].GetString();
			Density = Lsd[LuaNode.Density].GetInt();
			Thickness = Lsd[LuaNode.Thickness].GetInt();
			Base = Lsd[LuaNode.Base].GetInt();
			Precipitations = Lsd[LuaNode.Precipitations].GetInt();
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Preset] = Preset;
			Lsd[LuaNode.Density] = Density;
			Lsd[LuaNode.Thickness] = Thickness;
			Lsd[LuaNode.Base] = Base;
			Lsd[LuaNode.Precipitations] = Precipitations;
		}
	}

	internal class MizWeatherFog : BaseMiz
	{
		private class LuaNode
		{
			public static readonly string Thickness = "thickness";
			public static readonly string Visibility = "visibility";
		}

		public int Thickness { get; set; } // Thickness in meters
		public int Visibility { get; set; } // Visibility in meters

		public MizWeatherFog(LsonDict lsd) : base(lsd) { }

		public override void FromLua()
		{
			Thickness = Lsd[LuaNode.Thickness].GetInt();
			Visibility = Lsd[LuaNode.Visibility].GetInt();
		}

		public override void ToLua()
		{
			Lsd[LuaNode.Thickness] = Thickness;
			Lsd[LuaNode.Visibility] = Visibility;
		}
	}
}
