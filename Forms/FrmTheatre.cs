using DcsBriefop.Data;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class FrmTheatre : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private Theatre m_theatre;
		#endregion


		#region CTOR
		public FrmTheatre(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			CbTheatre.ValueMember = "Value";
			CbTheatre.DisplayMember = "Key";
			CbTheatre.DataSource = new Dictionary<string, string>()
			{
				{ "Caucasus", ElementTheatreName.Caucasus},
				{ "Marianas", ElementTheatreName.Marianas},
				{ "Nevada", ElementTheatreName.Nevada},
				{ "Sinai", ElementTheatreName.Sinai},
				{ "Syria", ElementTheatreName.Syria}
			}.ToList();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CbTheatre.SelectedIndexChanged -= CbTheatre_SelectedIndexChanged;

			if (m_briefopManager is not null)
			{
				CbTheatre.Text = m_briefopManager.BopMission.Theatre.Name;
			}
			else
			{
				CbTheatre.Text = ElementTheatreName.Caucasus;
			}

			DisplayCurrentTheatre();

			CbTheatre.SelectedIndexChanged += CbTheatre_SelectedIndexChanged;
		}

		private void DisplayCurrentTheatre()
		{
			m_theatre = new Theatre(CbTheatre.SelectedValue as string);
			TbProjection.Text = m_theatre.ProjectionInfo.ToProj4String();
		}
		#endregion

		#region Events
		private void FrmTheatre_Shown(object sender, EventArgs e)
		{
			using (new WaitDialog(this))
				DataToScreen();
		}

		private void CbTheatre_SelectedIndexChanged(object sender, EventArgs e)
		{
			DisplayCurrentTheatre();
		}
		#endregion
	}
}
