using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionAirbases : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private GridManagerAirbases m_gridManagerAirbases;

		private UcAirbase m_ucAirbase;
		#endregion

		#region CTOR
		private FrmMissionAirbases(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			m_gridManagerAirbases = new GridManagerAirbases(DgvAirbases, m_briefopManager.BopMission.Airbases);
			m_gridManagerAirbases.SelectionChanged += SelectionChangedEvent;
		}

		public static void CreateModal(BriefopManager briefopManager, Form parentForm)
		{
			using FrmMissionAirbases f = new(briefopManager);
			f.ShowDialog(parentForm);
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_gridManagerAirbases.SelectionChanged -= SelectionChangedEvent;

			m_gridManagerAirbases.Refresh();
			DataToScreenDetail();

			m_gridManagerAirbases.SelectionChanged += SelectionChangedEvent;
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopAirbase> selectedBopAirbases = m_gridManagerAirbases.GetSelectedElements();

			m_ucAirbase?.Dispose();
			m_ucAirbase = null;
			ScMain.Panel2.Controls.Clear();

			if (selectedBopAirbases.Count() == 1)
			{
				BopAirbase selectedBopAirbase = selectedBopAirbases.First();
				m_ucAirbase = new UcAirbase(m_briefopManager, selectedBopAirbase);
				ScMain.Panel2.Controls.Add(m_ucAirbase);
				m_ucAirbase.Dock = DockStyle.Fill;
			}
		}

		private void ScreenToDataDetail()
		{
			m_ucAirbase?.ScreenToData();
		}
		#endregion

		#region Events
		private void FrmMissionAirbases_Shown(object sender, EventArgs e)
		{
			using (new WaitDialog(this))
				DataToScreen();
		}

		private void FrmMissionAirbases_FormClosed(object sender, FormClosedEventArgs e)
		{
			ScreenToDataDetail();
		}

		private void SelectionChangedEvent(object sender, EventArgs e)
		{
			using (new WaitDialog(this))
			{
				ScreenToDataDetail();
				DataToScreenDetail();
			}
		}
		#endregion
	}
}
