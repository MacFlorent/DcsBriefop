using DcsBriefop.Data;
using DcsBriefop.DataBop;
using DcsBriefop.DataBopCustom;
using DcsBriefop.FormBop;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Zuby.ADGV;

namespace DcsBriefop.FormBop
{
	internal partial class UcBopCoalition : UcBaseBop
	{
		#region Fields
		//private List<GridManagerAsset> m_gridAssetManagers = new List<GridManagerAsset>();
		#endregion

		#region Properties
		public BopCoalition Coalition { get; private set; }
		#endregion

		#region CTOR
		public UcBopCoalition(UcMap ucMap, BopManager briefopManager, BopCoalition bopCoalition) : base(ucMap, briefopManager)
		{
			InitializeComponent();

			Coalition = bopCoalition;
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			TbBullseyeCoordinates.Text = Coalition.GetBullseyeCoordinatesString();
			TbBullseyeDescription.Text = Coalition.BullseyeDescription;
			CkBullseyeWaypoint.Checked = Coalition.BullseyeWaypoint;
			TbTask.Text = Coalition.Task;

			SetComPresetButton();
		}

		public override void ScreenToData()
		{
			Coalition.BullseyeDescription = TbBullseyeDescription.Text;
			Coalition.Task = TbTask.Text;
			Coalition.BullseyeWaypoint = CkBullseyeWaypoint.Checked;
		}

		public override BopCustomMap GetMapData()
		{
			return m_bopManager.BopMain.BopGeneral.MapData;
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

			//if (Coalition.ComPresets is object && Coalition.ComPresets.Count > 0)
			//{
			//	ControlPaint.DrawBorder(e.Graphics, c.ClientRectangle,
			//		Color.LawnGreen, 3, ButtonBorderStyle.Solid,
			//		Color.LawnGreen, 3, ButtonBorderStyle.Solid,
			//		Color.LawnGreen, 3, ButtonBorderStyle.Solid,
			//		Color.LawnGreen, 3, ButtonBorderStyle.Solid);
			//}
		}
		#endregion
	}
}
