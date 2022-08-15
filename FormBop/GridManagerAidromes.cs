using DcsBriefop.Data;
using DcsBriefop.DataBop;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.FormBop
{
	internal class GridManagerAirdromes : GridManager
	{
		#region Columns
		public static class Column
		{
			public static readonly string Id = "Id";
			public static readonly string Name = "Name";
			public static readonly string Localisation = "Localisation";
			public static readonly string Radio = "Radio";
			public static readonly string Information = "Information";
			public static readonly string Data = "Data";
		}
		#endregion

		#region Fields
		private List<BopAirdrome> m_airdomes;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public GridManagerAirdromes(BopDataGridView dgv, List<string> columnsDisplayed, List<BopAirdrome> airdomes) : base(dgv, columnsDisplayed)
		{
			m_airdomes = airdomes;

			m_dgv.CellDoubleClick += CellDoubleClickEvent;
		}

		public static GridManagerAirdromes NewManager(BopDataGridView dgv, List<string> columnsDisplayed, List<BopAirdrome> airdomes)
		{
			GridManagerAirdromes gm = new GridManagerAirdromes(dgv, columnsDisplayed, airdomes);
			gm.Initialize();
			return gm;
		}
		#endregion

		#region Methods
		protected override void InitializeGridSource()
		{
			m_dtSource = new DataTable();

			m_dtSource.Columns.Add(Column.Id, typeof(int));
			m_dtSource.Columns.Add(Column.Name, typeof(string));
			m_dtSource.Columns.Add(Column.Localisation, typeof(string));
			m_dtSource.Columns.Add(Column.Radio, typeof(string));
			m_dtSource.Columns.Add(Column.Information, typeof(string));
			m_dtSource.Columns.Add(Column.Data, typeof(BopAirdrome));

			foreach (BopAirdrome airdrome in m_airdomes)
			{
				DataRow dr = m_dtSource.NewRow();
				dr.SetField(Column.Data, airdrome);
				ObjectToDataRow(dr);
				m_dtSource.Rows.Add(dr);
			}
		}

		public void ObjectToDataRow(DataRow dr)
		{
			BopAirdrome airdrome = dr.Field<BopAirdrome>(Column.Data);

			dr.SetField(Column.Id, airdrome.Id);
			dr.SetField(Column.Name, airdrome.Name);
			dr.SetField(Column.Localisation, airdrome.ToStringLocalisation());
			//dr.SetField(Column.Radio, airdrome.GetRadioString());
			dr.SetField(Column.Information, airdrome.Information);
		}

		protected override void InitializeGridColumns()
		{
			m_dgv.Columns.Clear();

			m_dgv.AddColumn<int>(Column.Id, "Id").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Name, "Name").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Localisation, "Localisation").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Radio, "Radio").ReadOnly = true;
			m_dgv.AddColumn<string>(Column.Information, "Information").ReadOnly = true;
		}

		private void ShowDetail(DataRow dr)
		{
			BopAirdrome airdrome = dr.Field<BopAirdrome>(Column.Data);

			//FrmAssetDetail f = new FrmAssetDetail(asset);
			//f.ShowDialog();
			AirdromeModified?.Invoke(this, new EventArgsAirdrome() { BopAirdrome = airdrome });
			ObjectToDataRow(dr);
			(m_dgv.DataSource as BindingSource).EndEdit();
		}

		//protected override DataGridViewCellStyle CellFormatting(DataGridViewCell dgvc)
		//{
		//	DataGridViewCellStyle cellStyle = base.CellFormatting(dgvc);

		//	DataGridViewColumn column = dgvc.DataGridView.Columns[dgvc.ColumnIndex];
		//	DataRow dr = (dgvc.OwningRow.DataBoundItem as DataRowView)?.Row;

		//	return cellStyle;
		//}
		#endregion

		#region Menus

		#endregion

		#region Events
		public class EventArgsAirdrome : EventArgs
		{
			public BopAirdrome BopAirdrome { get; set; }
		}
		public event EventHandler<EventArgsAirdrome> AirdromeModified;

		private void CellDoubleClickEvent(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0)
				return;

			DataGridView dgv = (sender as DataGridView);
			if (dgv is null)
				return;

			DataGridViewColumn column = dgv.Columns[e.ColumnIndex];
			DataRow dr = (dgv.Rows[e.RowIndex].DataBoundItem as DataRowView)?.Row;

			if (column.ReadOnly)
			{
				if (dr is object)
					ShowDetail(dr);
			}
		}
		#endregion
	}
}
