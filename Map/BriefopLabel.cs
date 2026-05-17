using DcsBriefop.Data;
using Mapsui.Layers;

namespace DcsBriefop.Map
{
	internal class BriefopLabel(GeoPoint position, string sText, Color foreColor, Color backColor, string sFontFamily, float fFontSize, int iAngle, int iBorderThickness)
	{
		#region Properties
		public GeoPoint Position { get; } = position;
		public string Text { get; } = sText;
		public Color ForeColor { get; } = foreColor;
		public Color BackColor { get; } = backColor;
		public string FontFamily { get; } = sFontFamily;
		public float FontSize { get; } = fFontSize;
		public int Angle { get; } = iAngle;
		public int BorderThickness { get; } = iBorderThickness;
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
