using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Drawing;
using System.Runtime.Serialization;

namespace DcsBriefop
{
	[Serializable]
	public class GMarkerBriefop : GMapMarker, ISerializable, IDeserializationCallback
	{
		private Font m_font = new Font("Arial", 10);
		private Pen m_penSelected = new Pen(Color.Blue, 1);
		private Pen m_penMouseOver = new Pen(Color.CadetBlue, 1);

		private MarkerBriefopTemplate m_template;
		private Bitmap m_bitmap;

		public string MarkerTemplate
		{
			get { return m_template?.Name; }
		}

		public Color? TintColor { get; set; }

		public string Label { get; set; }

		public bool IsHovered { get; set; } = false;
		public bool IsPressed { get; set; } = false;
		public bool IsSelected { get; set; } = false;

		public GMarkerBriefop(PointLatLng p, string sMarkerType, Color? tintColor, string sLabel) : base(p)
		{
			LoadTemplate(sMarkerType);
			TintColor = tintColor;
			Label = sLabel;
			LoadBitmap();
		}

		public void LoadTemplate(string sTemplate)
		{
			m_template = MarkerBriefopTemplate.GetTemplate(sTemplate);
			Size = m_template.Size;
			Offset = new Point((int)(Size.Width * m_template.OffsetWidth), (int)(Size.Height * m_template.OffsetHeight));
		}

		public void LoadBitmap()
		{
			if (m_bitmap != null && m_bitmap != m_template.Bitmap)
			{
				m_bitmap.Dispose();
				m_bitmap = null;
			}

			if (TintColor is object)
				m_bitmap = m_template.Bitmap.ColorTint(TintColor.Value);
			else
				m_bitmap = m_template.Bitmap;
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
			info.AddValue("marker_type", MarkerTemplate);
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
