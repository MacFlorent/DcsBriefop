using System;
using System.Windows.Forms;

namespace DcsBriefop.Tools
{
	internal static class ToolsMenu
	{
		public static ToolStripMenuItem AddMenuItem(this ToolStripItemCollection tsic, string sLabel, EventHandler eventHandler)
		{
			int iIndex = tsic.Add(new ToolStripMenuItem(sLabel, null, eventHandler, null));
			return tsic[iIndex] as ToolStripMenuItem;
		}

		public static void AddMenuSeparator(this ToolStripItemCollection tsic)
		{
			if (tsic.Count > 0)
				tsic.Add(new ToolStripSeparator());
		}

	}
}
