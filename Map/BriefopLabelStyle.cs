using Mapsui.Styles;

namespace DcsBriefop.Map
{
	internal class BriefopLabelStyle : BaseStyle
	{
		public BriefopLabel Label { get; }

		public BriefopLabelStyle(BriefopLabel label)
		{
			Label = label;
		}
	}
}
