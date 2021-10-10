using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;
using System.Windows.Forms;

namespace DcsBriefop
{
	public static class GMarkerBriefopType
	{
		public static readonly string Bullseye = "bullseye";
		public static readonly string Pin = "pin";
		public static readonly string Factory = "factory";
	}

	public class GMarkerBriefopTemplate
	{
		public string MarkerType { get; set; }
		public Bitmap Bitmap { get; set; }
		public Size Size { get; set; }
		public double OffsetWidth { get; set; }
		public double OffsetHeight { get; set; }

		public override string ToString()
		{
			return MarkerType;
		}

		#region Static data
		private static readonly string m_sDefaultMarkerType = GMarkerBriefopType.Pin;
		private static Dictionary<string, Bitmap> m_bitmapCache = new Dictionary<string, Bitmap>();
		private static Dictionary<string, GMarkerBriefopTemplate> m_templatesList = new Dictionary<string, GMarkerBriefopTemplate>();

		static GMarkerBriefopTemplate()
		{
			Size defaultSize = new Size(32, 32);

			AddTemplate(GMarkerBriefopType.Bullseye, defaultSize);
			AddTemplate(GMarkerBriefopType.Pin, defaultSize, -0.5, -1);
			AddTemplate(GMarkerBriefopType.Factory, defaultSize);
		}

		public static void FillCombo(ComboBox cb)
		{
			cb.Items.Clear();
			foreach(GMarkerBriefopTemplate template in m_templatesList.Values)
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

		private static void AddTemplate(string sMarkerType, Size size)
		{
			AddTemplate(sMarkerType, size, -0.5, -0.5);
		}
		private static void AddTemplate(string sMarkerType, Size size, double dOffsetWidth, double dOffsetHeight)
		{
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

	[Serializable]
	public class GMarkerBriefop : GMapMarker, ISerializable, IDeserializationCallback
	{
		private Font m_font = new Font("Arial", 10);
		private Pen m_penSelected = new Pen(Color.Blue, 1);
		private Pen m_penMouseOver = new Pen(Color.CadetBlue, 1);

		private GMarkerBriefopTemplate m_template;
		private Bitmap m_bitmap;

		public string MarkerType
		{
			get { return m_template?.MarkerType; }
		}

		public Color Color { get; set; }

		public string Label { get; set; }

		public bool IsHovered { get; set; } = false;
		public bool IsPressed { get; set; } = false;
		public bool IsSelected { get; set; } = false;

		public GMarkerBriefop(PointLatLng p, string sMarkerType, Color color, string sLabel) : base(p)
		{
			LoadTemplate(sMarkerType);
			Color = color;
			Label = sLabel;
			LoadBitmap();
		}

		public void LoadTemplate(string sMarkerType)
		{
			m_template = GMarkerBriefopTemplate.GetTemplate(sMarkerType);
			Size = m_template.Size;
			Offset = new Point((int)(Size.Width * m_template.OffsetWidth), (int)(Size.Height * m_template.OffsetHeight));
		}

		public void LoadBitmap()
		{
			if (m_bitmap != null)
			{
				m_bitmap.Dispose();
				m_bitmap = null;
			}

			m_bitmap = m_template.Bitmap.ColorTint(Color);
		}

		public override void OnRender(Graphics g)
		{
			Rectangle rect = new Rectangle(LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);

			lock (m_bitmap) { g.DrawImage(m_bitmap, rect); }

			if (!string.IsNullOrEmpty(Label))
				g.DrawString(Label, m_font, Brushes.Black, rect.Left, rect.Bottom);

			if (IsSelected)
			{
				g.DrawRectangle(m_penSelected, rect);
			}
			else if (IsHovered)
			{
				g.DrawRectangle(m_penMouseOver, rect);
			}
		}

		public override void Dispose()
		{
			if (m_bitmap != null)
			{
				m_bitmap.Dispose();
				m_bitmap = null;
			}

			base.Dispose();
		}

		#region ISerializable Members
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("marker_type", MarkerType);
			base.GetObjectData(info, context);
		}

		protected GMarkerBriefop(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			string sMarkerType = Extensions.GetValue<string>(info, "marker_type", "");
			LoadTemplate(sMarkerType);
		}
		#endregion

		#region IDeserializationCallback Members
		public void OnDeserialization(object sender)
		{
			LoadBitmap();
		}
		#endregion
	}
}
