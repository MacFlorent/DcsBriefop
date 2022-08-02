using DcsBriefop.DataBriefop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DcsBriefop
{
	internal partial class FrmTest : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;
		#endregion

		public FrmTest(BriefopManager manager)
		{
			InitializeComponent();

			m_briefopManager = manager;
		}
	}
}
