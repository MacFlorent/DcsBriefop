using DcsBriefop.Tools;

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

			PbWait.Image = ToolsResources.GetImageResource("wait2", null, null);

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
}
