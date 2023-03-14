using DcsBriefop.Data;
using DcsBriefop.Tools;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
			DataToScreen();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_gmDcsObjects.Initialize();
		}

		private void ScreenToData()
		{
		}
		#endregion

		#region Events
		#endregion
	}
}
