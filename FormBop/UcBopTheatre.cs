using DcsBriefop.Data;
using DcsBriefop.DataBop;
using DcsBriefop.FgControls;
using DcsBriefop.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Zuby.ADGV;

namespace DcsBriefop.FormBop
{
	internal partial class UcBopTheatre : UcBaseBop
	{
		#region Fields
		//private List<GridManagerAsset> m_gridAssetManagers = new List<GridManagerAsset>();
		#endregion

		#region Properties
		public BopCoalition Coalition { get; private set; }
		#endregion

		#region CTOR
		public UcBopTheatre(UcMap ucMap, BopManager briefopManager) : base(ucMap, briefopManager)
		{
			InitializeComponent();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			LbTheatre.Text = BopManager.Theatre.Name;

		}

		public override void ScreenToData()
		{
		}

		#endregion

		#region Events
		#endregion
	}
}
