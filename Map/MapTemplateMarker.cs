using DcsBriefop.Tools;
using Newtonsoft.Json;

namespace DcsBriefop.Map
{
	internal static class ElementMapTemplateMarker
	{
		public static string DefaultMark { get; set; } = "defaultMark";
		public static string Mark { get; set; } = "trigger_zone";
		public static string Airdrome { get; set; } = "airdrome_class_2";
		public static string Aircraft { get; set; } = "P91000025";
		public static string Helicopter { get; set; } = "P91000020";
		public static string Ground { get; set; } = "ground";
		public static string Ship { get; set; } = "P91000057";
		public static string Waypoint { get; set; } = "navigation_point";
		public static string Bullseye { get; set; } = "P0091000347";
		public static string Circle { get; set; } = "P91000042";
	}

	public class MapTemplateMarker
	{
		#region Fields
		private static readonly string m_directory = @".\markers";
		private static readonly double m_dDefaultOffsetWidth = -0.5;
		private static readonly double m_dDefaultOffsetHeight = -0.5;
		private static readonly int m_iDefaultWidth = 24;
		private static readonly int m_iDefaultHeight = 24;
		#endregion

		#region Properties
		public string Name { get; set; }
		public string ImageName { get; set; }
		public string DcsMizFileName { get; set; }
		public int SizeWidth { get; set; }
		public int SizeHeight { get; set; }
		public double OffsetWidth { get; set; }
		public double OffsetHeight { get; set; }
		#endregion

		#region Methods
		public override string ToString()
		{
			return Name;
		}

		public Bitmap GetBitmap()
		{
			return new Bitmap(ToolsImage.GetCachedBitmap(ImageName));
		}
		#endregion

		#region Static
		private static readonly MapTemplateMarker m_default;
		private static Dictionary<string, MapTemplateMarker> m_templatesList = new();

		static MapTemplateMarker()
		{
			ConfigMapTemplateMarkers config;
			try
			{
				string sJsonStream = ToolsResources.GetJsonResourceContent("Markers");
				config = JsonConvert.DeserializeObject<ConfigMapTemplateMarkers>(sJsonStream);
			}
			catch (Exception ex)
			{
				Log.Error("Marker template config file cannot be loaded");
				Log.Exception(ex);
				config = null;
			}

			if (config is not null)
			{
				m_directory = config.Directory ?? m_directory;
				m_iDefaultWidth = config.DefaultWidth.GetValueOrDefault(m_iDefaultWidth);
				m_iDefaultHeight = config.DefaultHeight.GetValueOrDefault(m_iDefaultHeight);

				ElementMapTemplateMarker.Airdrome = config.DefaultAirdrome ?? ElementMapTemplateMarker.Airdrome;
				ElementMapTemplateMarker.Aircraft = config.DefaultAircraft ?? ElementMapTemplateMarker.Aircraft;
				ElementMapTemplateMarker.Helicopter = config.DefaultHelicopter ?? ElementMapTemplateMarker.Helicopter;
				ElementMapTemplateMarker.Ground = config.DefaultGround ?? ElementMapTemplateMarker.Ground;
				ElementMapTemplateMarker.Ship = config.DefaultShip ?? ElementMapTemplateMarker.Ship;
				ElementMapTemplateMarker.Waypoint = config.DefaultWaypoint ?? ElementMapTemplateMarker.Waypoint;
				ElementMapTemplateMarker.Bullseye = config.DefaultBullseye ?? ElementMapTemplateMarker.Bullseye;
			}

			string sBaseDirectory = ToolsMisc.GetDirectoryFullPath(m_directory);

			if (Directory.Exists(sBaseDirectory))
			{
				foreach (string sFilePath in Directory.GetFiles(sBaseDirectory, "*.*", SearchOption.TopDirectoryOnly))
				{
					AddTemplate(NewTemplateFromFile(sFilePath, config));
				}
			}

			AddTemplate(NewTemplateFromFile(ElementMapTemplateMarker.DefaultMark, config));
			m_default = GetTemplate(ElementMapTemplateMarker.DefaultMark);
		}

		private static MapTemplateMarker NewTemplateFromFile(string sTemplateString, ConfigMapTemplateMarkers config)
		{
			try
			{
				MapTemplateMarker template = new MapTemplateMarker();
				template.Name = Path.GetFileNameWithoutExtension(sTemplateString);
				template.ImageName = sTemplateString;

				if (config is not null && config.Templates.Where(_c => string.Equals(_c.FileName, template.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() is ConfigMapTemplateMarker configTemplate)
				{
					template.DcsMizFileName = configTemplate.DcsMizFileName ?? Path.GetFileName(sTemplateString);
					template.SizeWidth = configTemplate.Width.GetValueOrDefault(config.DefaultWidth.Value);
					template.SizeHeight = configTemplate.Height.GetValueOrDefault(config.DefaultHeight.Value);
					template.OffsetWidth = configTemplate.OffsetWidth.GetValueOrDefault(m_dDefaultOffsetWidth);
					template.OffsetHeight = configTemplate.OffsetWidth.GetValueOrDefault(m_dDefaultOffsetHeight);
				}
				else
				{
					template.DcsMizFileName = Path.GetFileName(sTemplateString);
					template.SizeWidth = m_iDefaultWidth;
					template.SizeHeight = m_iDefaultHeight;
					template.OffsetWidth = m_dDefaultOffsetWidth;
					template.OffsetHeight = m_dDefaultOffsetHeight;
				}

				return template;
			}
			catch (ExceptionBop ex)
			{
				Log.Error($"Marker template {sTemplateString} was not added");
				Log.Exception(ex);
				return null;
			}
		}

		private static void AddTemplate(MapTemplateMarker template)
		{
			if (string.IsNullOrEmpty(template.Name) || m_templatesList.ContainsKey(template.Name))
				return;

			m_templatesList.Add(template.Name, template);
		}

		public static MapTemplateMarker GetTemplate(string sTemplate)
		{
			if (!m_templatesList.TryGetValue(sTemplate, out MapTemplateMarker template))
			{
				template = m_default;
			}

			return template;
		}

		public static MapTemplateMarker GetTemplateFromDcsMizFile(string sDcsMizFile)
		{
			MapTemplateMarker template = m_templatesList.Values.Where(_t => string.Equals(_t.DcsMizFileName, sDcsMizFile, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
			template ??= m_default;

			return template;
		}

		public static void FillCombo(ComboBox cb, EventHandler selectedValueChanged)
		{
			ToolsControls.FillCombo(cb, m_templatesList.Values.ToList(), "Name", "Name", selectedValueChanged);
		}
		#endregion
	}
}
