using DcsBriefop.Data;
using DcsBriefop.DataBop;
using DcsBriefop.Tools;
using DcsBriefop.UcBriefing;
using GMap.NET.MapProviders;
using System;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmPreferencesMiz : Form
	{
		public delegate void RefreshUcMapDelegate();

		#region Fields
		private BopManager m_bopManager;
		private RefreshUcMapDelegate m_dlgtRefreshUcMap;
		#endregion

		#region CTOR
		public FrmPreferencesMiz(BopManager bopManager, RefreshUcMapDelegate dlgtRefreshUcMap)
		{
			InitializeComponent();

			ToolsStyle.ApplyStyle(this);
			ToolsStyle.ButtonOk(BtOk);
			ToolsStyle.ButtonCancel(BtCancel);

			DialogResult = DialogResult.Cancel;
			m_bopManager = bopManager;
			m_dlgtRefreshUcMap = dlgtRefreshUcMap;

			CbMapProvider.DataSource = GMapProviders.List;

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CkNoCallsignForPlayableFlights.CheckedChanged -= CkNoCallsignForPlayableFlights_CheckedChanged;

			CbMapProvider.SelectedItem = m_bopManager.BopCustomMain.GetDefaultMapProvider();
			CkNoCallsignForPlayableFlights.Checked = m_bopManager.BopCustomMain.NoCallsignForPlayableFlights;

			CkNoCallsignForPlayableFlights.CheckedChanged += CkNoCallsignForPlayableFlights_CheckedChanged;
		}

		private void ScreenToData()
		{
			m_bopManager.BopCustomMain.DefaultMapProvider = (CbMapProvider.SelectedItem as GMapProvider)?.Name;
			m_bopManager.BopCustomMain.NoCallsignForPlayableFlights = CkNoCallsignForPlayableFlights.Checked;
		}
		#endregion

		#region Events
		private void BtOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			ScreenToData();
			Close();
		}

		private void BtCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
		#endregion

		private void CkNoCallsignForPlayableFlights_CheckedChanged(object sender, EventArgs e)
		{
			//m_bopManager.BopCustomMain.NoCallsignForPlayableFlights = CkNoCallsignForPlayableFlights.Checked;
			//foreach (BopCoalition coalition in m_briefingContainer.BriefingCoalitions)
			//{
			//	foreach (AssetFlight flight in coalition.OwnAssets.OfType<AssetFlight>())
			//		flight.SetDisplayName();
			//}
		}

		private void BtMapProvider_Click(object sender, EventArgs e)
		{
			m_bopManager.BopMain.SetMapProvider((CbMapProvider.SelectedItem as GMapProvider)?.Name);
			m_dlgtRefreshUcMap?.Invoke();
		}
	}
}
