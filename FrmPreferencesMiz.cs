using DcsBriefop.Data;
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
		private BriefingContainer m_briefingContainer;
		private RefreshUcMapDelegate m_dlgtRefreshUcMap;
		#endregion

		#region CTOR
		public FrmPreferencesMiz(BriefingContainer briefingContainer, RefreshUcMapDelegate dlgtRefreshUcMap)
		{
			InitializeComponent();

			ToolsStyle.ApplyStyle(this);
			ToolsStyle.ButtonOk(BtOk);
			ToolsStyle.ButtonCancel(BtCancel);

			DialogResult = DialogResult.Cancel;
			m_briefingContainer = briefingContainer;
			m_dlgtRefreshUcMap = dlgtRefreshUcMap;

			CbMapProvider.DataSource = GMapProviders.List;

			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CkNoCallsignForPlayableFlights.CheckedChanged -= CkNoCallsignForPlayableFlights_CheckedChanged;

			CbMapProvider.SelectedItem = m_briefingContainer.Core.Miz.BriefopCustomData.GetDefaultMapProvider();
			CkNoCallsignForPlayableFlights.Checked = m_briefingContainer.Core.Miz.BriefopCustomData.NoCallsignForPlayableFlights;

			CkNoCallsignForPlayableFlights.CheckedChanged += CkNoCallsignForPlayableFlights_CheckedChanged;
		}

		private void ScreenToData()
		{
			m_briefingContainer.Core.Miz.BriefopCustomData.DefaultMapProvider = (CbMapProvider.SelectedItem as GMapProvider)?.Name;
			m_briefingContainer.Core.Miz.BriefopCustomData.NoCallsignForPlayableFlights = CkNoCallsignForPlayableFlights.Checked;
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
			m_briefingContainer.Core.Miz.BriefopCustomData.NoCallsignForPlayableFlights = CkNoCallsignForPlayableFlights.Checked;
			foreach (BriefingCoalition coalition in m_briefingContainer.BriefingCoalitions)
			{
				foreach (AssetFlight flight in coalition.OwnAssets.OfType<AssetFlight>())
					flight.SetDisplayName();
			}
		}

		private void BtMapProvider_Click(object sender, EventArgs e)
		{
			m_briefingContainer.SetMapProvider((CbMapProvider.SelectedItem as GMapProvider)?.Name);
			m_dlgtRefreshUcMap?.Invoke();
		}
	}
}
