using BrightIdeasSoftware;
using DcsBriefop.Data;
using DcsBriefop.DataBopMission;

namespace DcsBriefop.Forms
{
	internal class GridManagerAirbaseRadios : GridManagerBase<BopAirbaseRadio>
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Radio = "Radio";
			public static readonly string Label = "Label";
			public static readonly string Default = "Default";
			public static readonly string Used = "Used";
		}
		#endregion

		#region CTOR
		public GridManagerAirbaseRadios(FastObjectListView dgv, IEnumerable<BopAirbaseRadio> airbaseRadios) : base(dgv, airbaseRadios) { }
		#endregion

		#region Methods
		protected override void InitializeColumns()
		{
			m_grid.AllColumns.AddRange(new OLVColumn[]
			{
				new OLVColumn { Text = "Radio", Name = GridColumn.Radio, Width = GridWidth.Medium, IsEditable = true, AspectGetter = obj => ((BopAirbaseRadio)obj).Radio?.ToString() },
				new OLVColumn { Text = "Label", Name = GridColumn.Label, Width = GridWidth.Medium, IsEditable = true, AspectGetter = obj => ((BopAirbaseRadio)obj).Label },
				new OLVColumn { Text = "Default", Name = GridColumn.Default, Width = GridWidth.Small, CheckBoxes = true, AspectGetter = obj => ((BopAirbaseRadio)obj).Default },
				new OLVColumn { Text = "Used", Name = GridColumn.Used, Width = GridWidth.Small, CheckBoxes = true, IsEditable = true, AspectGetter = obj => ((BopAirbaseRadio)obj).Used },
			});
			m_grid.RebuildColumns();
			m_grid.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;
		}

		protected override void CellEditFinishedInternal(CellEditEventArgs e)
		{
			BopAirbaseRadio bopAirbaseRadio = e.RowObject as BopAirbaseRadio;
			if (bopAirbaseRadio is null)
				return;

			if (e.Column.Name == GridColumn.Used)
			{
				bopAirbaseRadio.Used = (bool)e.NewValue;
			}
			else if (e.Column.Name == GridColumn.Label)
			{
				bopAirbaseRadio.Label = e.NewValue as string;
			}
			else if (e.Column.Name == GridColumn.Radio)
			{
				Radio radio = Radio.NewFromString(e.NewValue as string);
				if (radio is not null && !radio.Equals(bopAirbaseRadio.Radio))
					bopAirbaseRadio.Radio = radio;
				m_grid.RefreshObject(bopAirbaseRadio);
			}
		}

		protected override void AssignEvents()
		{
			base.AssignEvents();
			m_grid.CellEditStarting += CellEditStartingEvent;
		}

		protected override void RemoveEvents()
		{
			base.RemoveEvents();
			m_grid.CellEditStarting -= CellEditStartingEvent;
		}

		private void CellEditStartingEvent(object sender, CellEditEventArgs e)
		{
			if (e.Column.Name == GridColumn.Radio)
			{
				BopAirbaseRadio bopAirbaseRadio = e.RowObject as BopAirbaseRadio;
				if (bopAirbaseRadio is null || bopAirbaseRadio.Default)
					e.Cancel = true;
			}
		}
		#endregion
	}
}
