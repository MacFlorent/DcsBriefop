using DcsBriefop.DataBop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class FrmTest : Form
	{
		#region Fields
		private BopManager m_briefopManager;
		#endregion

		public FrmTest(BopManager manager)
		{
			InitializeComponent();

			m_briefopManager = manager;
		}
	}
}
