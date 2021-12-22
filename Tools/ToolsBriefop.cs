using DcsBriefop.Data;
using System.Drawing;

namespace DcsBriefop.Tools
{
	internal static class ToolsBriefop
	{
		public static Color GetCoalitionColor(string sCoalitionName)
		{
			if (sCoalitionName == ElementCoalition.Red)
				return ElementCoalitionColor.Red;
			else if (sCoalitionName == ElementCoalition.Blue)
				return ElementCoalitionColor.Blue;
			else
				return ElementCoalitionColor.Neutral;
		}

		public static string GetOpposingCoalitionName(string sCoalitionName)
		{
			if (sCoalitionName == ElementCoalition.Blue)
				return ElementCoalition.Red;
			else if (sCoalitionName == ElementCoalition.Red)
				return ElementCoalition.Blue;
			else
				return null;
		}
	}
}
