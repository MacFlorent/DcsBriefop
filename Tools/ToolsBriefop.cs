using DcsBriefop.Data;
using System.Drawing;
using System.Linq;

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

		//public static bool AssetOrUnitIncluded(Asset asset)
		//{
		//	return asset is object &&
		//		(asset.Included || ((asset as AssetGroup)?.Units.Where(_u => _u.Included).Any()).GetValueOrDefault(false));
		//}
	}
}
