using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Serialization;

namespace DcsBriefop
{
	public static class GMarkerBriefopType
	{
		public static readonly string Bullseye = "bullseye";
	}

	[Serializable]
	public class GMarkerBriefop : GMapMarker, ISerializable, IDeserializationCallback
	{
		static readonly Dictionary<string, Bitmap> m_bitmapCache = new Dictionary<string, Bitmap>();
		static readonly string m_sDefaultMarkerType = GMarkerBriefopType.Bullseye;

		private Font m_font = new Font("Arial", 10);
		private Bitmap m_bitmap;
		public Bitmap Bitmap
		{
			get { return m_bitmap; }
			set { m_bitmap = value; }
		}

		public string MarkerType { get; private set; }
		public string Label { get; set; }

		public GMarkerBriefop(PointLatLng p, string sType, string sLabel) : base(p)
		{
			MarkerType = sType;
			Label = sLabel;
			LoadBitmap();
		}

		private Bitmap GetBitmapResource()
		{
			if (!m_bitmapCache.TryGetValue(MarkerType, out Bitmap bmp))
			{
				bmp = Properties.Resources.ResourceManager.GetObject(MarkerType, Properties.Resources.Culture) as Bitmap;
				if (bmp is null && MarkerType != m_sDefaultMarkerType)
				{
					MarkerType = m_sDefaultMarkerType;
					bmp = GetBitmapResource();
				}
				else
					m_bitmapCache.Add(MarkerType, bmp);
			}

			return bmp;
		}

		private void LoadBitmap()
		{
			Bitmap = GetBitmapResource();

			if (MarkerType == GMarkerBriefopType.Bullseye)
			{
				Size = new Size(32, 32);
				Offset = new Point(-Size.Width / 2, -Size.Height / 2);
			}
		}



		public override void OnRender(Graphics g)
		{
			Rectangle rect = new Rectangle(LocalPosition.X, LocalPosition.Y, Size.Width, Size.Height);

			//ColorMap[] colorMap = new ColorMap[1];
			//colorMap[0] = new ColorMap();
			//colorMap[0].OldColor = Color.Black;
			//colorMap[0].NewColor = Color.Blue;
			//ImageAttributes attr = new ImageAttributes();
			//attr.SetRemapTable(colorMap);
			//g.DrawImage(Bitmap, rect, 0, 0, rect.Width, rect.Height, GraphicsUnit.Pixel, attr);

			lock (Bitmap) { g.DrawImage(Bitmap, rect); }

			if (!string.IsNullOrEmpty(Label))
				g.DrawString(Label, m_font, Brushes.Black, rect.Left, rect.Bottom);
		}

		public override void Dispose()
		{
			if (Bitmap != null)
			{
				if (!m_bitmapCache.ContainsValue(Bitmap))
				{
					Bitmap.Dispose();
					Bitmap = null;
				}
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
			MarkerType = Extensions.GetValue<string>(info, "marker_type", m_sDefaultMarkerType);
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
