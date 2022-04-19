using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal class GridAsset : DataGridView
	{
		#region Columns
		private static class GridAssetColumn
		{
			public static readonly string Included = "Included";
			public static readonly string MapDisplay = "MapDisplay";
			public static readonly string Mission = "Mission";
			public static readonly string Side = "Side";
			public static readonly string Function = "Function";
			public static readonly string Id = "Id";
			public static readonly string Name = "Name";
			public static readonly string Type = "Type";
			public static readonly string Task = "Task";
			public static readonly string Radio = "Radio";
			public static readonly string Notes = "Notes";
			public static readonly string Data = "Data";
		}
		#endregion
	}
}
