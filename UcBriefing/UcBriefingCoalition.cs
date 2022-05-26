using DcsBriefop.Data;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Zuby.ADGV;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingCoalition : UcBaseBriefing
	{
		#region Fields
		private List<GridManagerAsset> m_gridAssetManagers = new List<GridManagerAsset>();
		#endregion

		#region Properties
		public BriefingCoalition Coalition { get; private set; }
		#endregion

		#region CTOR
		public UcBriefingCoalition(UcMap ucMap, BriefingContainer briefingContainer, BriefingCoalition briefingCoalition) : base(ucMap, briefingContainer)
		{
			InitializeComponent();

			Coalition = briefingCoalition;
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			TbBullseyeCoordinates.Text = Coalition.GetBullseyeCoordinatesString();
			TbBullseyeDescription.Text = Coalition.BullseyeDescription;
			CkBullseyeWaypoint.Checked = Coalition.BullseyeWaypoint;
			TbTask.Text = Coalition.Task;

			DataToScreenTabs();
			SetComPresetButton();
		}

		private void DataToScreenTabs()
		{
			int iSelectedIndex = -1;
			if (TcAssets.TabPages.Count > 0)
			{
				iSelectedIndex = TcAssets.SelectedIndex;
				TcAssets.DisposeAndClearTabs();
			}

			m_gridAssetManagers.Clear();
			DataToScreenAddGridTab("Own assets", Coalition.OwnAssets, GridManagerAsset.ColumnsDisplayedOwn);
			DataToScreenAddGridTab("Opposing assets", Coalition.OpposingAssets, GridManagerAsset.ColumnsDisplayedOpposing);
			DataToScreenAddGridTab("Airdromes", Coalition.Airdromes.OfType<Asset>().ToList(), GridManagerAsset.ColumnsDisplayedAirdrome);

			if (iSelectedIndex >= 0 && iSelectedIndex < TcAssets.TabCount)
				TcAssets.SelectedIndex = iSelectedIndex;
		}

		private void DataToScreenAddGridTab(string sTabText, List<Asset> assets, List<string> columnsDisplayed)
		{
			TabPage tp = new TabPage(sTabText);
			TcAssets.TabPages.Add(tp);
			AdvancedDataGridView dgv = new AdvancedDataGridView();
			dgv.ReadOnly = false;
			dgv.Dock = DockStyle.Fill;
			tp.Controls.Add(dgv);

			GridManagerAsset gam = new GridManagerAsset(dgv, assets, null);
			gam.ColumnsDisplayed = columnsDisplayed;
			gam.Initialize();

			m_gridAssetManagers.Add(gam);
		}

		public override void ScreenToData()
		{
			Coalition.BullseyeDescription = TbBullseyeDescription.Text;
			Coalition.Task = TbTask.Text;
			Coalition.BullseyeWaypoint = CkBullseyeWaypoint.Checked;
		}

		private void SetComPresetButton()
		{

		}
		#endregion

		#region Events
		private void TbBullseyeDescription_Validated(object sender, System.EventArgs e)
		{
			Coalition.BullseyeDescription = TbBullseyeDescription.Text;
			Coalition.ResetBullseyeMarkerDescription();
		}

		private void CkBullseyeWaypoint_CheckedChanged(object sender, EventArgs e)
		{
			Coalition.BullseyeWaypoint = CkBullseyeWaypoint.Checked;
			Coalition.InitializeBullseyeWaypoints();
		}

		private void CkAssetFilter_CheckedChanged(object sender, EventArgs e)
		{
		}

		private void BtComPresets_Click(object sender, EventArgs e)
		{

			FrmComs f = new FrmComs(Coalition);
			if (f.ShowDialog() == DialogResult.OK)
			{
				DataToScreen();
			}
		}

		private void BtComPresets_Paint(object sender, PaintEventArgs e)
		{
			Control c = (sender as Control);
			if (c is null)
				return;

			if (Coalition.ComPresets is object && Coalition.ComPresets.Count > 0)
			{
				ControlPaint.DrawBorder(e.Graphics, c.ClientRectangle,
					Color.LawnGreen, 3, ButtonBorderStyle.Solid,
					Color.LawnGreen, 3, ButtonBorderStyle.Solid,
					Color.LawnGreen, 3, ButtonBorderStyle.Solid,
					Color.LawnGreen, 3, ButtonBorderStyle.Solid);
			}
		}
		#endregion
	}
}
