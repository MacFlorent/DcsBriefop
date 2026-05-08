using DcsBriefop.Data;
using Mapsui.Layers;

namespace DcsBriefop.Map
{
	internal class BriefopLabel
	{
		#region Properties
		public GeoPoint Position { get; }
		public string Text { get; }
		public Color ForeColor { get; }
		public Color BackColor { get; }
		public string FontFamily { get; }
		public float FontSize { get; }
		public int Angle { get; }
		public int BorderThickness { get; }
		#endregion

		#region CTOR
		public BriefopLabel(GeoPoint position, string sText, Color foreColor, Color backColor, string sFontFamily, float fFontSize, int iAngle, int iBorderThickness)
		{
			Position = position;
			Text = sText;
			ForeColor = foreColor;
			BackColor = backColor;
			FontFamily = sFontFamily;
			FontSize = fFontSize;
			Angle = iAngle;
			BorderThickness = iBorderThickness;
		}
		#endregion

		#region Methods
		public PointFeature ToMapFeature()
		{
			PointFeature mapFeature = new(MapProjection.ToMPoint(Position));
			mapFeature.Styles.Add(new BriefopLabelStyle(this));
			return mapFeature;
		}
		#endregion
	}
}
