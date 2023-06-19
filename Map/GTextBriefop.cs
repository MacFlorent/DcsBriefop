using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing.Drawing2D;
using System.Runtime.Serialization;

namespace DcsBriefop.Map
{
	[Serializable]
	public class GTextBriefop : GMapMarker, ISerializable, IDeserializationCallback
	{
		#region Fields
		private Pen m_penBorder;
		private Brush m_brushFill;
		#endregion

		#region Properties
		public string Text { get; set; }
		public Color ForeColor { get; set; }
		public Color BackColor { get; set; }
		public Font Font { get; set; }
		public int Angle { get; set; }
		public int BorderThickness { get; set; }
		public bool IsHovered { get; set; } = false;
		public bool IsPressed { get; set; } = false;
		public bool IsSelected { get; set; } = false;
		#endregion

		#region CTOR
		public GTextBriefop(PointLatLng point, string sText, Color foreColor, Color backColor, Font font, int iAngle, int iBorderThickness) : base(point)
		{
			Text = sText;
			ForeColor = foreColor;
			BackColor = backColor;
			Font = font;
			Angle = iAngle;
			BorderThickness = iBorderThickness;

			m_penBorder = new Pen(ForeColor, iBorderThickness);
			m_brushFill = new SolidBrush(BackColor);
		}

		#endregion

		#region Methods
		#endregion

		#region Render
		public override void OnRender(Graphics g)
		{
			Render(g);
		}

		private void Render(Graphics g)
		{
			GraphicsState state = g.Save();
			g.TranslateTransform(LocalPosition.X, LocalPosition.Y);
			if (Angle != 0)
			{
				g.RotateTransform(Angle);
			}

			// in DCS texboxes are anchored on bottom left corner
			SizeF textSize = g.MeasureString(Text, Font);
			Rectangle targetRectangle = new Rectangle(0, -(int)textSize.Height, (int)textSize.Width, (int)textSize.Height);
			Point pointBaseString = new Point((int)textSize.Width / 2, -(int)textSize.Height);
			
			g.FillRectangle(m_brushFill, targetRectangle);
			ToolsImage.DrawStringAngledCentered(g, pointBaseString, Text, Font, textSize, ForeColor, false, Color.Empty, 0, 0);
			if (BorderThickness > 0)
				g.DrawRectangle(m_penBorder, targetRectangle);


			//if (IsSelected)
			//{
			//	g.DrawRectangle(m_penSelected, targetRectangle);
			//}
			//else if (IsHovered)
			//{
			//	g.DrawRectangle(m_penMouseOver, targetRectangle);
			//}

			g.Restore(state);
		}
		#endregion

		#region ISerializable Members
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			//info.AddValue("marker_type", MarkerTemplate);
			base.GetObjectData(info, context);
		}

		protected GTextBriefop(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			//string sMarkerType = Extensions.GetValue<string>(info, "marker_type", "");
		}
		#endregion

		#region IDeserializationCallback Members
		public void OnDeserialization(object sender)
		{
		}
		#endregion

		#region IDisposable
		public override void Dispose()
		{
			base.Dispose();
			m_penBorder?.Dispose();
			m_brushFill?.Dispose();
		}
		#endregion
	}
}
