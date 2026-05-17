using BrightIdeasSoftware;
using DcsBriefop.Data;
using DcsBriefop.DataBopMission;

namespace DcsBriefop.Forms
{
	internal class GridManagerAirbases : GridManagerBase<BopAirbase>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string AirbaseType = "AirbaseType";
			public static readonly string Name = "Name";
			public static readonly string Additional = "Additional";
		}
		#endregion

		#region CTOR
		public GridManagerAirbases(FastObjectListView dgv, IEnumerable<BopAirbase> airbases) : base(dgv, airbases) { }
		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			m_grid.AllColumns.AddRange(new OLVColumn[]
			{
				new OLVColumn { Text = "Id", Name = GridColumn.Id, Width = GridWidth.Small, AspectGetter = obj => ((BopAirbase)obj).Id },
				new OLVColumn { Text = "Type", Name = GridColumn.AirbaseType, Width = GridWidth.Medium, AspectGetter = obj => ((BopAirbase)obj).AirbaseType },
				new OLVColumn { Text = "Name", Name = GridColumn.Name, Width = GridWidth.Large, AspectGetter = obj => ((BopAirbase)obj).Name },
				new OLVColumn { Text = "Additional", Name = GridColumn.Additional, Width = GridWidth.ExtraLarge, AspectGetter = obj => ((BopAirbase)obj).ToStringAdditional() },
			});
			m_grid.RebuildColumns();
		}

		protected override void FormatCellInternal(FormatCellEventArgs e)
		{
			if (e.Column.Name != GridColumn.AirbaseType)
				return;

			BopAirbase element = (BopAirbase)e.Model;
			if (element.AirbaseType == ElementAirbaseType.Ship)
				e.SubItem.BackColor = Color.LightBlue;
			else if (element.AirbaseType == ElementAirbaseType.Farp)
				e.SubItem.BackColor = Color.Salmon;
		}
		#endregion
	}
}
