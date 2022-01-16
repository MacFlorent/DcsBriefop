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
	}

	public class RouteBriefopTemplate
	{
		#region Fields
		#endregion

		#region Properties
		public string Name { get; set; }
		public Bitmap Bitmap { get; set; }
		public DashStyle DashStyle { get; set; }
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

			if (Directory.Exists(sBaseDirectory))
			{
				foreach (string sFilePath in Directory.GetFiles(sBaseDirectory, "*.*", SearchOption.TopDirectoryOnly))
				{
					AddTemplate(sFilePath);
				}
			}

			foreach (string sDash in Enum.GetValues(typeof(RouteBriefopType)).Cast<RouteBriefopType>().Select(_e => _e.ToString()))
				AddTemplate(sDash);

		}

		private static void AddTemplate(string sTemplate)
		{
			try
			{
				string sName = null;
				DashStyle dashStyle = DashStyle.Solid;
				Bitmap bitmap = null;

				if (File.Exists(sTemplate))
				{
					sName = Path.GetFileNameWithoutExtension(sTemplate);
					bitmap = GetCachedBitmap(sTemplate);
				}
				else
				{
					sName = sTemplate;
					if (sTemplate == RouteBriefopType.dot.ToString())
						dashStyle = DashStyle.Dot;

				}

				if (string.IsNullOrEmpty(sName) || m_templatesList.ContainsKey(sName))
					return;

				RouteBriefopTemplate template = new RouteBriefopTemplate()
				{
					Name = sName,
					Bitmap = bitmap,
					DashStyle = dashStyle
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

		#endregion
	}
}
