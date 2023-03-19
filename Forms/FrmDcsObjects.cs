using DcsBriefop.Data;
using DcsBriefop.Tools;

namespace DcsBriefop.Forms
{
	internal partial class FrmDcsObjects : Form
	{
		#region Fields
		private GridManagerDcsObjects m_gmDcsObjects;
		#endregion

		#region CTOR
		public FrmDcsObjects()
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			m_gmDcsObjects = new GridManagerDcsObjects(DgvDcsObjects, DcsObjectManager.DcsObjects);
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_gmDcsObjects.Refresh();
		}

		private void ScreenToData()
		{
		}
		#endregion

		#region Events
		private void FrmDcsObjects_Shown(object sender, EventArgs e)
		{
			using (new WaitDialog(this))
				DataToScreen();
		}
		#endregion


	}
}
