using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using System.Data;

namespace DcsBriefop.Forms
{
	internal class GridManagerAirbases : GridManager
	{
		#region Columns
		public static class GridColumn
		{
			public static readonly string Id = "Id";
			public static readonly string AirbaseType = "AirbaseType"; 
			public static readonly string Name = "Name";
			public static readonly string Additionnal = "Additionnal";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		private IEnumerable<BopAirbase> m_airbases;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerAirbases(DataGridView dgv, IEnumerable<BopAirbase> airbases) : base(dgv)
		{
			m_airbases = airbases;
		}
		#endregion

		#region Methods
		protected override void InitializeDataSource()
		{
			m_dtSource = new DataTable();
			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.AirbaseType, typeof(ElementAirbaseType));
			m_dtSource.Columns.Add(GridColumn.Name, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Additionnal, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Data, typeof(BopAirbase));

			foreach (BopAirbase bopAirbase in m_airbases)
				RefreshDataSourceRow(bopAirbase);
		}

		private void RefreshDataSourceRow(BopAirbase bopAirbase)
		{
			DataRow dr = m_dtSource.AsEnumerable().Where(_dr => _dr.Field<BopAirbase>(GridColumn.Data) == bopAirbase).FirstOrDefault();
			if (dr is null)
			{
				dr = m_dtSource.NewRow();
				dr.SetField(GridColumn.Data, bopAirbase);
				m_dtSource.Rows.Add(dr);
			}

			dr.SetField(GridColumn.Id, bopAirbase.Id);
			dr.SetField(GridColumn.AirbaseType, bopAirbase.AirbaseType);
			dr.SetField(GridColumn.Name, bopAirbase.Name);
			dr.SetField(GridColumn.Additionnal, bopAirbase.ToStringAdditionnal());
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				column.ReadOnly = true;
			}

			m_dgv.Columns[GridColumn.Data].Visible = false;
			m_dgv.Columns[GridColumn.AirbaseType].HeaderText = "Type";
		}

		public IEnumerable<BopAirbase> GetSelected()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopAirbase>(GridColumn.Data)).ToList();
		}

		protected override DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		{
			DataGridViewCellStyle cellStyle = base.CellFormatting(dgvc);
			return cellStyle;
		}

		protected override void SelectionChanged()
		{
			SelectionChangedTyped?.Invoke(this, new EventArgsBopAirbases() { BopAirbases = GetSelected() });
		}
		#endregion

		#region Events
		public class EventArgsBopAirbases : EventArgs
		{
			public IEnumerable<BopAirbase> BopAirbases { get; set; }
		}
		public event EventHandler<EventArgsBopAirbases> SelectionChangedTyped;
		#endregion
	}
}
