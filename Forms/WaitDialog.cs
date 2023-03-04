namespace DcsBriefop.Forms
{
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
			m_callerForm.Enabled = false;
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
			m_callerForm.Enabled = true;

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
}