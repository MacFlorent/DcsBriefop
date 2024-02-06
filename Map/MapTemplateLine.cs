using DcsBriefop.Data;
using DcsBriefop.Tools;
using Newtonsoft.Json;
using System.Drawing.Drawing2D;

namespace DcsBriefop.Map
{
	internal static class ElementMapTemplateLine
	{
		public static string DashLine { get; set; } = "DashLine";
		public static string DashDot { get; set; } = "DashDot";
		public static string DashDash { get; set; } = "DashDash";
	}

	public class MapTemplateLine
	{
		#region Fields
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
		private static readonly MapTemplateLine m_default;
		private static Dictionary<string, MapTemplateLine> m_templatesList = new();

		static MapTemplateLine()
		{
			ConfigMapTemplateRoutes config;

			try
			{
				string sJsonStream = ToolsResources.GetJsonResourceContent(ElementBopResource.Routes, ElementBopResource.DirectoryRoutes);
				config = JsonConvert.DeserializeObject<ConfigMapTemplateRoutes>(sJsonStream);
			}
			catch (Exception ex)
			{
				Log.Error("Line template config file cannot be loaded");
				Log.Exception(ex);
				config = null;
			}

			string sBaseDirectory = ToolsResources.GetResourceDirectoryPath(ElementBopResource.DirectoryRoutes);

			if (Directory.Exists(sBaseDirectory))
			{
				foreach (string sFilePath in Directory.GetFiles(sBaseDirectory, "*.*", SearchOption.TopDirectoryOnly))
				{
					AddTemplate(NewTemplateFromFile(sFilePath, config));
				}
			}

			AddTemplate(NewTemplate(ElementMapTemplateLine.DashLine, DashStyle.Solid));
			AddTemplate(NewTemplate(ElementMapTemplateLine.DashDot, DashStyle.Dot));
			AddTemplate(NewTemplate(ElementMapTemplateLine.DashDash, DashStyle.Dash));

			m_default = GetTemplate(ElementMapTemplateLine.DashLine);
		}

		private static MapTemplateLine NewTemplateFromFile(string sTemplateString, ConfigMapTemplateRoutes config)
		{
			try
			{
				MapTemplateLine template = new MapTemplateLine();
				template.Name = Path.GetFileNameWithoutExtension(sTemplateString);
				template.ImageName = sTemplateString;

				if (config is not null && config.Templates.Where(_c => string.Equals(_c.FileName, template.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault() is ConfigMapTemplateRoute configTemplate)
				{
					template.DcsMizStyle = configTemplate.DcsMizStyle;
					template.ThicknessCorrection = configTemplate.ThicknessCorrection;
					template.DashOverride = configTemplate.DashOverride;
				}

				return template;
			}
			catch (ExceptionBop ex)
			{
				Log.Error($"Line template {sTemplateString} was not added");
				Log.Exception(ex);
				return null;
			}
		}

		private static MapTemplateLine NewTemplate(string sTemplateString, DashStyle dashStyle)
		{
			MapTemplateLine template = new MapTemplateLine();
			template.Name = sTemplateString;
			template.DashOverride = dashStyle;

			return template;
		}

		private static void AddTemplate(MapTemplateLine template)
		{
			if (string.IsNullOrEmpty(template.Name) || m_templatesList.ContainsKey(template.Name))
				return;

			m_templatesList.Add(template.Name, template);
		}

		public static MapTemplateLine GetTemplate(string sTemplate)
		{
			if (!m_templatesList.TryGetValue(sTemplate, out MapTemplateLine template))
			{
				template = m_default;
			}

			return template;
		}

		public static MapTemplateLine GetTemplateFromDcsMizStyle(string sDcsMizStyle)
		{
			MapTemplateLine template = m_templatesList.Values.Where(_t => string.Equals(_t.DcsMizStyle, sDcsMizStyle, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
			if (template is null)
				template = m_templatesList.Values.Where(_t => string.Equals(_t.Name, $"polyline_{sDcsMizStyle}", StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
			if (template is null)
				template = m_default;

			return template;
		}

		#endregion
	}
}
