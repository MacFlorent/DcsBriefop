using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionAirbases : FrmWithWaitDialog
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private GridManagerAirbases m_gridManagerAirbases;

		private UcAirbase m_ucAirbase;
		#endregion

		#region CTOR
		private FrmMissionAirbases(BriefopManager briefopManager, WaitDialog waitDialog) : base(waitDialog)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			m_gridManagerAirbases = new GridManagerAirbases(DgvAirbases, m_briefopManager.BopMission.Airbases);
			m_gridManagerAirbases.SelectionChanged += SelectionChangedEvent;
		}

		public static void CreateModal(BriefopManager briefopManager, Form parentForm)
		{
			WaitDialog waitDialog = new WaitDialog(parentForm);
			FrmMissionAirbases f = new FrmMissionAirbases(briefopManager, waitDialog);
			f.ShowDialog();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_gridManagerAirbases.SelectionChanged -= SelectionChangedEvent;

			m_gridManagerAirbases.Initialize();
			DataToScreenDetail();

			m_gridManagerAirbases.SelectionChanged += SelectionChangedEvent;
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopAirbase> selectedBopAirbases = m_gridManagerAirbases.GetSelectedElements();
			if (selectedBopAirbases.Count() == 1)
			{
				BopAirbase selectedBopAirbase = selectedBopAirbases.First();
				if (ScMain.Panel2.Controls.Count > 0 && !(ScMain.Panel2.Controls[0] is UcAirbase))
				{
					ScMain.Panel2.Controls.Clear();
				}
				if (m_ucAirbase is null)
				{
					m_ucAirbase = new UcAirbase(m_briefopManager, selectedBopAirbase);
				}
				else
				{
					m_ucAirbase.BopAirbase = selectedBopAirbase;
				}

				if (ScMain.Panel2.Controls.Count == 0)
				{
					ScMain.Panel2.Controls.Add(m_ucAirbase);
					m_ucAirbase.Dock = DockStyle.Fill;
				}
			}
			else
			{
				ScMain.Panel2.Controls.Clear();
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
			DataToScreen();
		}

		private void FrmMissionAirbases_FormClosed(object sender, FormClosedEventArgs e)
		{
			ScreenToDataDetail();
		}

		private void SelectionChangedEvent(object sender, EventArgs e)
		{
			ScreenToDataDetail();
			DataToScreenDetail();
		}
		#endregion
	}
}
