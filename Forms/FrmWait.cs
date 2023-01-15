using DcsBriefop.Tools;
using System;
using System.Threading;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	#region FrmWait
	public partial class FrmWait : Form
	{
		#region Fields
		private delegate void DlgtThreadSafeClose();
		private delegate void DlgtThreadSafeShowModal();

		private Form m_parent = null;
		private TimeSpan m_elapsed = new TimeSpan();
		private System.Windows.Forms.Timer m_tmTimer = new System.Windows.Forms.Timer();
		private AutoResetEvent m_areIsReady = null;
		#endregion

		#region CTOR
		public FrmWait(Form parent, AutoResetEvent areIsReady)
		{
			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			PbWait.Image = ToolsResources.GetImageResource("wait2");

			m_parent = parent;
			m_areIsReady = areIsReady;
			LbElapsed.Text = "";
			m_tmTimer.Tick += m_tmTimer_Tick;
		}
		#endregion

		#region Methods
		private void DisplayElapsedTime()
		{
			LbElapsed.Text = $"{m_elapsed:hh\\:mm\\:ss}";
		}

		public void CloseThreadSafe()
		{
			if (InvokeRequired)
			{
				DlgtThreadSafeClose d = new DlgtThreadSafeClose(CloseThreadSafe);
				Invoke(d, null);
			}
			else
			{
				m_tmTimer.Stop();
				DialogResult = DialogResult.None;

				if (!IsDisposed)
					Close();
			}
		}

		public void ShowDialogInThread()
		{
			ShowDialog(); // windows position relative to he parent will be done in the FrmWait_Load function
		}

		protected override CreateParams CreateParams
		{
			get
			{
				// Turn on WS_EX_TOOLWINDOW style bit
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x80;
				return cp;
			}
		}
		#endregion

		#region Events
		private void FrmWait_Load(object sender, EventArgs e)
		{
			if (m_parent != null)
			{
				Location = m_parent.Location;
				Left += m_parent.ClientSize.Width / 2 - this.Width / 2;
				Top += m_parent.ClientSize.Height / 2 - this.Height / 2;
			}
			m_tmTimer.Interval = 1000;
			DisplayElapsedTime();
			m_tmTimer.Start();

			m_areIsReady.Set();
		}

		private void m_tmTimer_Tick(object sender, EventArgs e)
		{
			m_elapsed = m_elapsed.Add(new TimeSpan(0, 0, 0, 0, m_tmTimer.Interval));
			DisplayElapsedTime();
		}
		#endregion
	}
	#endregion

	#region WaitDialog
	public class WaitDialog : IDisposable
	{
		private Thread m_waiting = null;
		private FrmWait m_frmWait = null;
		private Form m_callerForm = null;
		private WaitCursor m_waitCursor = null;

		public WaitDialog(Form parent)
		{
			m_waitCursor = new WaitCursor();

			AutoResetEvent areFormReady = new AutoResetEvent(false); // to ensure the window will not be disposed before it is ready
			m_callerForm = parent;
			m_frmWait = new FrmWait(parent, areFormReady);
			m_waiting = new Thread(new ThreadStart(m_frmWait.ShowDialogInThread));
			m_waiting.Start();
			areFormReady.WaitOne();
		}

		public void Dispose()
		{
			m_frmWait.CloseThreadSafe();
			GC.SuppressFinalize(this);

			// it can happen that the application is sent to back if the WaitDialog is displayed in another form's Load event
			// in this case better move the call in the Shown event
			// CallerBringToFront will not always work as it seems sometimes it is done too soon and the caller is sent to back afterwards
			CallerActivate();

			m_waitCursor.Dispose();
		}

		private void CallerBringToFront()
		{
			if (m_callerForm != null)
			{
				if (m_callerForm.InvokeRequired)
					m_callerForm.BeginInvoke(new MethodInvoker(delegate () { CallerBringToFront(); }));
				else
					m_callerForm.BringToFront();
			}
		}
		private void CallerActivate()
		{
			if (m_callerForm != null)
			{
				if (m_callerForm.InvokeRequired)
					m_callerForm.BeginInvoke(new MethodInvoker(delegate () { CallerActivate(); }));
				else
					m_callerForm.Activate();
			}
		}
	}
	#endregion

	#region WaitCursor
	public class WaitCursor : IDisposable
	{
		private static int m_iCountWait = 0;
		public WaitCursor()
		{
			Cursor.Current = Cursors.WaitCursor;
			m_iCountWait++;
		}

		//~WaitCursor()
		//{
		//	Dispose(false);
		//}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				m_iCountWait--;
				if (m_iCountWait == 0)
					Cursor.Current = Cursors.Default;
			}
		}
	}
	#endregion
}
