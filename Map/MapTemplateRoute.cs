using DcsBriefop.Tools;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;

namespace DcsBriefop.Map
{
	internal static class ElementMapTemplateRoute
	{
		public static string DashLine { get; set; } = "DashLine";
		public static string DashDot { get; set; } = "DashDot";
		public static string DashDash { get; set; } = "DashDash";
	}

	public class MapTemplateRoute
	{
		#region Fields
		private static string m_directory = @".\routes";
		#endregion

		#region Properties
		public string Name { get; set; }
		public string DcsMizStyle { get; set; }
		public string ImageName { get; set; }
		public DashStyle? DashOverride { get; set; }
		public double? ThicknessCorrection { get; set; }
		#endregion

		#region Methods
		public override string ToString()
		{
			return Name;
		}

		public Bitmap GetBitmap()
		{
			if (DashOverride is null)
				return new Bitmap(ToolsImage.GetCachedBitmap(ImageName));
			else
				return null;
		}
		#endregion

		#region Static
		private static readonly MapTemplateRoute m_default;
		private static Dictionary<string, MapTemplateRoute> m_templatesList = new();

		static MapTemplateRoute()
		{
			ConfigMapTemplateRoutes config;

			try
			{
				string sJsonStream = ToolsResources.GetJsonResourceContent("Routes");
				config = JsonConvert.DeserializeObject<ConfigMapTemplateRoutes>(sJsonStream);
			}
			catch (Exception ex)
			{
				Log.Error("Route template config file cannot be loaded");
				Log.Exception(ex);
				config = null;
			}

			if (config is object)
			{
				m_directory = config.Directory ?? m_directory;
			}

			string sBaseDirectory = ToolsMisc.GetDirectoryFullPath(m_directory);

			if (Directory.Exists(sBaseDirectory))
			{
				foreach (string sFilePath in Directory.GetFiles(sBaseDirectory, "*.*", SearchOption.TopDirectoryOnly))
				{
					AddTemplate(NewTemplateFromFile(sFilePath, config));
				}
			}

			AddTemplate(NewTemplate(ElementMapTemplateRoute.DashLine, DashStyle.Solid));
			AddTemplate(NewTemplate(ElementMapTemplateRoute.DashDot, DashStyle.Dot));
			AddTemplate(NewTemplate(ElementMapTemplateRoute.DashDash, DashStyle.Dash));

			m_default = GetTemplate(ElementMapTemplateRoute.DashLine);
		}

		private static MapTemplateRoute NewTemplateFromFile(string sTemplateString, ConfigMapTemplateRoutes config)
		{
			try
			{
				MapTemplateRoute template = new MapTemplateRoute();
				template.Name = Path.GetFileNameWithoutExtension(sTemplateString);
				template.ImageName = sTemplateString;

				if (config.Templates.Where(_c => string.Equals(_c.FileName, template.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() is ConfigMapTemplateRoute configTemplate)
				{
					template.DcsMizStyle = configTemplate.DcsMizStyle;
					template.ThicknessCorrection = configTemplate.ThicknessCorrection;
					template.DashOverride = configTemplate.DashOverride;
				}

				return template;
			}
			catch (ExceptionBop ex)
			{
				Log.Error($"Route template {sTemplateString} was not added");
				Log.Exception(ex);
				return null;
			}
		}

		private static MapTemplateRoute NewTemplate(string sTemplateString, DashStyle dashStyle)
		{
			MapTemplateRoute template = new MapTemplateRoute();
			template.Name = sTemplateString;
			template.DashOverride = dashStyle;

			return template;
		}

		private static void AddTemplate(MapTemplateRoute template)
		{
			if (string.IsNullOrEmpty(template.Name) || m_templatesList.ContainsKey(template.Name))
				return;

			m_templatesList.Add(template.Name, template);
		}

		public static MapTemplateRoute GetTemplate(string sTemplate)
		{
			if (!m_templatesList.TryGetValue(sTemplate, out MapTemplateRoute template))
			{
				template = m_default;
			}

			return template;
		}

		public static MapTemplateRoute GetTemplateFromDcsMizStyle(string sDcsMizStyle)
		{
			MapTemplateRoute template = m_templatesList.Values.Where(_t => string.Equals(_t.DcsMizStyle, sDcsMizStyle, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
			if (template is null)
				template = m_templatesList.Values.Where(_t => string.Equals(_t.Name, $"polyline_{sDcsMizStyle}", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
			if (template is null)
				template = m_default;

			return template;
		}

		#endregion
	}
}
