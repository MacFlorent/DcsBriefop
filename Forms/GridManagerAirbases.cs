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
			public static readonly string Checked = "Checked";
			public static readonly string Id = "Id";
			public static readonly string AirbaseType = "AirbaseType"; 
			public static readonly string Name = "Name";
			public static readonly string Additional = "Additional";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		private IEnumerable<BopAirbase> m_airbases;
		#endregion

		#region Properties
		public List<Tuple<int, ElementAirbaseType>> CheckedElements = null;
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
			m_dtSource.Columns.Add(GridColumn.Checked, typeof(bool));
			m_dtSource.Columns.Add(GridColumn.Id, typeof(int));
			m_dtSource.Columns.Add(GridColumn.AirbaseType, typeof(ElementAirbaseType));
			m_dtSource.Columns.Add(GridColumn.Name, typeof(string));
			m_dtSource.Columns.Add(GridColumn.Additional, typeof(string));
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

			dr.SetField(GridColumn.Checked, CheckedElements is not null && CheckedElements.Contains(new Tuple<int, ElementAirbaseType>(bopAirbase.Id, bopAirbase.AirbaseType)));
			dr.SetField(GridColumn.Id, bopAirbase.Id);
			dr.SetField(GridColumn.AirbaseType, bopAirbase.AirbaseType);
			dr.SetField(GridColumn.Name, bopAirbase.Name);
			dr.SetField(GridColumn.Additional, bopAirbase.ToStringAdditional());
		}

		protected override void PostInitializeColumns()
		{
			base.PostInitializeColumns();

			foreach (DataGridViewColumn column in m_dgv.Columns)
			{
				column.ReadOnly = true;
			}

			m_dgv.Columns[GridColumn.Checked].ReadOnly = false;
			m_dgv.Columns[GridColumn.Checked].Visible = CheckedElements is not null;
			m_dgv.Columns[GridColumn.Data].Visible = false;
			m_dgv.Columns[GridColumn.AirbaseType].HeaderText = "Type";
		}

		public IEnumerable<BopAirbase> GetSelectedElements()
		{
			return GetSelectedDataRows().Select(_dr => _dr.Field<BopAirbase>(GridColumn.Data)).ToList();
		}

		//protected override void SelectionChanged()
		//{
		//	SelectionChangedTyped?.Invoke(this, new EventArgsBopAirbases() { BopAirbases = GetSelected() });
		//}

		protected override void CellEndEditInternal(DataGridView dgv, DataGridViewCell dgvc)
		{
			BopAirbase bopAirbase = dgvc.OwningRow.Cells[GridColumn.Data].Value as BopAirbase;
			if (bopAirbase is not null && CheckedElements is not null && dgvc.OwningColumn.Name == GridColumn.Checked)
			{
				Tuple<int, ElementAirbaseType> airbase = new(bopAirbase.Id, bopAirbase.AirbaseType);
				if ((bool)dgvc.Value)
				{
					if (!CheckedElements.Contains(airbase))
						CheckedElements.Add(airbase);
				}
				else
				{
					while (CheckedElements.Remove(airbase));
				}
			}
		}

		protected override void CellMouseUpInternal(DataGridView dgv, DataGridViewCell dgvc)
		{
			if (dgvc.OwningColumn.Name == GridColumn.Checked)
				dgv.EndEdit();
		}
		#endregion

		#region Events
		//public class EventArgsBopAirbases : EventArgs
		//{
		//	public IEnumerable<BopAirbase> BopAirbases { get; set; }
		//}
		//public event EventHandler<EventArgsBopAirbases> SelectionChangedTyped;
		#endregion
	}
}
