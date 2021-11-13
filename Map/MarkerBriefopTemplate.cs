using DcsBriefop.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DcsBriefop.Map
{
	internal enum MarkerBriefopType
	{
		aircraft,
		airport,
		bullseye,
		dot,
		factory,
		helicopter,
		pin,
		ship,
		tank,
		triangle
	}

	public class MarkerBriefopTemplate
	{
		#region Fields
		private static readonly double m_dDefaultOffsetWidth = -0.5;
		private static readonly double m_dDefaultOffsetHeight = -0.5;
		#endregion

		#region Properties
		public string Name { get; set; }
		public Bitmap Bitmap { get; set; }
		public Size Size { get; set; }
		public double OffsetWidth { get; set; }
		public double OffsetHeight { get; set; }
		#endregion

		#region Methods
		public override string ToString()
		{
			return Name;
		}
		#endregion

		#region Static
		private static readonly string m_sDefaultMarkerType = MarkerBriefopType.pin.ToString();
		private static Dictionary<string, Bitmap> m_bitmapCache = new Dictionary<string, Bitmap>();
		private static Dictionary<string, MarkerBriefopTemplate> m_templatesList = new Dictionary<string, MarkerBriefopTemplate>();

		static MarkerBriefopTemplate()
		{
			BriefopMarkerSection configSection = BriefopMarkerSection.GetMarkerSection();

			string sBaseDirectory = configSection.BaseDirectory;
			if (sBaseDirectory.StartsWith(@".\"))
				sBaseDirectory = sBaseDirectory.Replace(@".\", $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\");

			if (Directory.Exists(sBaseDirectory))
			{
				foreach (string sFilePath in Directory.GetFiles(sBaseDirectory, "*.*", SearchOption.TopDirectoryOnly))
				{
					AddTemplate(sFilePath, configSection);
				}
			}

			foreach (string sResource in Enum.GetValues(typeof(MarkerBriefopType)).Cast<MarkerBriefopType>().Select(_e => _e.ToString()))
				AddTemplate(sResource, configSection);
		}

		private static void AddTemplate(string sTemplate, BriefopMarkerSection configSection)
		{
			try
			{
				Size size = new Size(configSection.DefaultWidth, configSection.DefaultHeight);
				double dOffsetWidth = m_dDefaultOffsetWidth, dOffsetHeight = m_dDefaultOffsetHeight;
				string sName = null;

				if (File.Exists(sTemplate))
				{
					sName = Path.GetFileNameWithoutExtension(sTemplate);
					if (configSection.MarkerConfigs[sName] is BriefopMarkerElement cfgElement)
					{
						size.Width = cfgElement.Width ?? size.Width;
						size.Height = cfgElement.Height ?? size.Height;
						dOffsetWidth = cfgElement.OffsetWidth ?? dOffsetWidth;
						dOffsetHeight = cfgElement.OffsetHeight ?? dOffsetHeight;
					}
				}
				else if (Properties.Resources.ResourceManager.GetObject(sTemplate, Properties.Resources.Culture) is object)
				{
					sName = sTemplate;
					if (sTemplate == MarkerBriefopType.pin.ToString())
					{
						dOffsetWidth = -0.5; dOffsetHeight = -0.95;
					}
					else if (sTemplate == MarkerBriefopType.dot.ToString())
					{
						size = new Size(16, 16);
					}
					else if (sTemplate == MarkerBriefopType.triangle.ToString())
					{
						size = new Size(16, 16);
					}
				}

				if (string.IsNullOrEmpty(sName) || m_templatesList.ContainsKey(sName))
					return;


				MarkerBriefopTemplate template = new MarkerBriefopTemplate()
				{
					Name = sName,
					Bitmap = GetCachedBitmap(sTemplate),
					Size = size,
					OffsetWidth = dOffsetWidth,
					OffsetHeight = dOffsetHeight
				};
				m_templatesList.Add(template.Name, template);
			}
			catch (ExceptionDcsBriefop ex)
			{
				Log.Error($"Marker template {sTemplate} was not added");
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
					throw new ExceptionDcsBriefop($"Failed to create bitmap template for marker file { sTemplate}");

				m_bitmapCache.Add(sTemplate, bmp);
			}

			return bmp;
		}

		public static void FillCombo(ComboBox cb)
		{
			cb.Items.Clear();
			cb.ValueMember = cb.DisplayMember = "Name";
			cb.DataSource = m_templatesList.Values.ToList();
		}

		public static MarkerBriefopTemplate GetTemplate(string sTemplate)
		{
			if (!m_templatesList.TryGetValue(sTemplate, out MarkerBriefopTemplate template))
			{
				template = m_templatesList[m_sDefaultMarkerType];
			}

			return template;
		}

		#endregion
	}
}
