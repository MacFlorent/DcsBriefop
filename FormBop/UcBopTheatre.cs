using DcsBriefop.DataBop;
using DcsBriefop.DataBopCustom;
using DcsBriefop.Tools;

namespace DcsBriefop.FormBop
{
	internal partial class UcBopTheatre : UcBaseBop, ICustomStylable
	{
		#region Fields
		private GridManagerAirdromes m_gridManagerAirdromes;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public UcBopTheatre(UcMap ucMap, BopManager bopManager) : base(ucMap, bopManager)
		{
			InitializeComponent();
		}
		#endregion

		#region ICustomStylable
		public void ApplyCustomStyle()
		{
			ToolsStyle.LabelTitle(LbTheatre);
			ToolsStyle.LabelHeader(LbAirdromes);
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			LbTheatre.Text = m_bopManager.Theatre.Name;
			m_gridManagerAirdromes = GridManagerAirdromes.NewManager(DgvAirdromes, null, m_bopManager.BopMain.Airdromes);
		}

		public override void ScreenToData()
		{
		}

		public override BopCustomMap GetMapData()
		{
			return m_bopManager.BopMain.BopGeneral.MapData;
		}
		#endregion

		#region Events
		#endregion
	}
}
