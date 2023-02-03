using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal class FrmWithWaitDialog : Form
	{
		#region Fields
		protected WaitDialog m_waitDialog;
		#endregion

		#region CTOR
		public FrmWithWaitDialog() { }
		protected FrmWithWaitDialog(WaitDialog waitDialog)
		{
			m_waitDialog = waitDialog;
			Shown += ShownEvent;
		}
		#endregion

		#region Methods
		
		#endregion

		#region Events
		internal virtual void ShownEvent(object sender, System.EventArgs e)
		{
			if (m_waitDialog is object)
				m_waitDialog.Dispose();
		}
		#endregion


	}
}
