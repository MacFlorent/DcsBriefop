using DcsBriefop.Configuration;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal static class MarkerBriefopType
	{
		public static readonly string Aircraft = "aircraft";
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
		public string Name { get; set; }
		public string FileName { get; set; }
		public Bitmap Bitmap { get; set; }
		public Size Size { get; set; }
		public double OffsetWidth { get; set; }
		public double OffsetHeight { get; set; }

		public override string ToString()
		{
			return Name;
		}

		#region Static data
		private static readonly string m_sDefaultMarkerType = GMarkerBriefopType.pin.ToString();
		private static Dictionary<string, Bitmap> m_bitmapCache = new Dictionary<string, Bitmap>();
		private static Dictionary<string, MarkerBriefopTemplate> m_templatesList = new Dictionary<string, MarkerBriefopTemplate>();

		static MarkerBriefopTemplate()
		{
			BriefopMarkerSection configSection = BriefopMarkerSection.GetMarkerSection();
			
			string sBaseDirectory = configSection.BaseDirectory;
			if (sBaseDirectory.StartsWith(@".\"))
				sBaseDirectory = sBaseDirectory.Replace(@".\", $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\");

			foreach (string sType in Enum.GetValues(typeof(GMarkerBriefopType)).Cast<GMarkerBriefopType>().Select(_e => _e.ToString()))
				AddTemplate(sType);
		}

		private static void AddTemplate(string sMarkerType)
		{
			BriefopMarkerSection configSection = BriefopMarkerSection.GetMarkerSection();
			Size size = new Size(configSection.DefaultWidth, configSection.DefaultHeight);
			double dOffsetWidth = -0.5, dOffsetHeight = -0.5;

			if (sMarkerType == GMarkerBriefopType.pin.ToString())
			{
				dOffsetWidth = -0.5; dOffsetHeight = -0.95;
			}
			else if (sMarkerType == GMarkerBriefopType.dot.ToString())
			{
				size = new Size(16, 16);
			}
			else if (sMarkerType == GMarkerBriefopType.triangle.ToString())
			{
				size = new Size(16, 16);
			}

			GMarkerBriefopTemplate template = new GMarkerBriefopTemplate()
			{
				MarkerType = sMarkerType,
				Bitmap = GetCachedBitmapResource(sMarkerType),
				Size = size,
				OffsetWidth = dOffsetWidth,
				OffsetHeight = dOffsetHeight
			};
			m_templatesList.Add(sMarkerType, template);

		}

		public static void FillCombo(ComboBox cb)
		{
			cb.Items.Clear();
			foreach (GMarkerBriefopTemplate template in m_templatesList.Values)
			{
				cb.Items.Add(template);
			}
		}

		public static GMarkerBriefopTemplate GetTemplate(string sMarkerType)
		{
			if (!m_templatesList.TryGetValue(sMarkerType, out GMarkerBriefopTemplate template))
			{
				template = m_templatesList[m_sDefaultMarkerType];
			}

			return template;
		}
		private static Bitmap GetCachedBitmapResource(string sResourceName)
		{
			if (!m_bitmapCache.TryGetValue(sResourceName, out Bitmap bmp))
			{
				bmp = Properties.Resources.ResourceManager.GetObject(sResourceName, Properties.Resources.Culture) as Bitmap;
				m_bitmapCache.Add(sResourceName, bmp);
			}

			return bmp;
		}
		#endregion
	}

	}
}
