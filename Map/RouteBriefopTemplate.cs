using DcsBriefop.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DcsBriefop.Map
{
	internal enum RouteBriefopType
	{
		line,
		dot,
		dash
	}

	public class RouteBriefopTemplate
	{
		#region Fields
		#endregion

		#region Properties
		public string Name { get; set; }
		public DashStyle DashStyle { get; set; }
		public Bitmap Bitmap { get; set; }
		public decimal? ThicknessCorrection { get; set; }
		public string DcsMizStyle { get; set; }
		#endregion

		#region Methods
		public override string ToString()
		{
			return Name;
		}
		#endregion

		#region Static
		private static readonly string m_sDefaultRouteType = RouteBriefopType.line.ToString();
		private static Dictionary<string, Bitmap> m_bitmapCache = new Dictionary<string, Bitmap>();
		private static Dictionary<string, RouteBriefopTemplate> m_templatesList = new Dictionary<string, RouteBriefopTemplate>();

		static RouteBriefopTemplate()
		{
			BriefopConfigSection configSection = BriefopConfigSection.GetConfigSection();

			string sBaseDirectory = configSection.BaseDirectoryRoutes;
			if (sBaseDirectory.StartsWith(@".\"))
				sBaseDirectory = sBaseDirectory.Replace(@".\", $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\");

			BriefopRoutesElement configElement = configSection.BriefopRoutes;

			if (Directory.Exists(sBaseDirectory))
			{
				foreach (string sFilePath in Directory.GetFiles(sBaseDirectory, "*.*", SearchOption.TopDirectoryOnly))
				{
					AddTemplate(sFilePath, configElement);
				}
			}

			foreach (string sDash in Enum.GetValues(typeof(RouteBriefopType)).Cast<RouteBriefopType>().Select(_e => _e.ToString()))
				AddTemplate(sDash, configElement);

		}

		private static void AddTemplate(string sTemplate, BriefopRoutesElement configsElement)
		{
			try
			{
				string sName = null;
				DashStyle dashStyle = DashStyle.Solid;
				Bitmap bitmap = null;
				decimal? dThicknessCorrection = null;
				string sDcsMizStyle = null;

				if (File.Exists(sTemplate))
				{
					sName = Path.GetFileNameWithoutExtension(sTemplate);
					bitmap = GetCachedBitmap(sTemplate);

					if (configsElement.MarkerConfigsList[sName] is BriefopRouteElement cfgElement)
					{
						dThicknessCorrection = cfgElement.ThicknessCorrection ?? configsElement.DefaultThicknessCorrection;
						sDcsMizStyle = cfgElement.DcsMizStyle ?? sName.Replace("polyline_", "");
					}
				}
				else
				{
					sName = sTemplate;
					if (sTemplate == RouteBriefopType.dot.ToString())
						dashStyle = DashStyle.Dot;
					else if (sTemplate == RouteBriefopType.dash.ToString())
						dashStyle = DashStyle.Dash;
				}

				if (string.IsNullOrEmpty(sName) || m_templatesList.ContainsKey(sName))
					return;

				RouteBriefopTemplate template = new RouteBriefopTemplate()
				{
					Name = sName,
					DashStyle = dashStyle,
					Bitmap = bitmap,
					ThicknessCorrection = dThicknessCorrection,
					DcsMizStyle = sDcsMizStyle
				};
				m_templatesList.Add(template.Name, template);
			}
			catch (ExceptionDcsBriefop ex)
			{
				Log.Error($"Route template {sTemplate} was not added");
				Log.Exception(ex);
			}
		}

		private static Bitmap GetCachedBitmap(string sTemplate)
		{
			if (!m_bitmapCache.TryGetValue(sTemplate, out Bitmap bmp))
			{
				if (File.Exists(sTemplate))
				{
					try { bmp = new Bitmap(sTemplate); }
					catch (Exception ex) { Log.Exception(ex); }
				}
				else
				{
					bmp = Properties.Resources.ResourceManager.GetObject(sTemplate, Properties.Resources.Culture) as Bitmap;
				}

				if (bmp is null)
					throw new ExceptionDcsBriefop($"Failed to create bitmap template for route file {sTemplate}");

				m_bitmapCache.Add(sTemplate, bmp);
			}

			return bmp;
		}

		public static RouteBriefopTemplate GetTemplate(string sTemplate)
		{
			if (!m_templatesList.TryGetValue(sTemplate, out RouteBriefopTemplate template))
			{
				template = m_templatesList[m_sDefaultRouteType];
			}

			return template;
		}

		public static RouteBriefopTemplate GetTemplateFromDcsMizStyle(string sDcsMizStyle)
		{
			RouteBriefopTemplate template = m_templatesList.Values.Where(_t => _t.DcsMizStyle == sDcsMizStyle).FirstOrDefault();
			if (template is null)
				template = m_templatesList[m_sDefaultRouteType];

			return template;
		}

		#endregion
	}
}
