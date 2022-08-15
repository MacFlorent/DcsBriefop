using DcsBriefop.DataBop;
using DcsBriefop.Tools;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.FormBop
{
	internal partial class UcBopMain : UserControl
	{
		#region Fields
		private UcMap m_ucMap;
		private BopManager m_bopManager;
		#endregion

		#region CTOR
		public UcBopMain(UcMap ucMap, BopManager bopManager)
		{
			InitializeComponent();

			m_ucMap = ucMap;
			m_bopManager = bopManager;


			TcMain.TabPages.Clear();
		}
		#endregion

		#region Methods
		public void DataToScreen()
		{
			int iSelectedIndex = -1;
			if (TcMain.TabPages.Count > 0)
			{
				iSelectedIndex = TcMain.SelectedIndex;
				TcMain.DisposeAndClearTabs();
			}

			TcMain.TabPages.Add(new BopTabPage("General", new UcBopGeneral(m_ucMap, m_bopManager)));
			TcMain.TabPages.Add(new BopTabPage("Theatre", new UcBopTheatre(m_ucMap, m_bopManager)));

			//foreach (BriefingCoalition coalition in BriefingContainer.BriefingCoalitions.Where(_c => _c.Included))
			//{
			//	DataToScreen_AddCoalitionTab(coalition);
			//}

			foreach (UcBaseBop ucBop in TcMain.TabPages.OfType<BopTabPage>().Select(_tp => _tp.UcBop))
			{
				ucBop.DataToScreen();
			}

			if (iSelectedIndex >= 0 && iSelectedIndex < TcMain.TabCount)
				TcMain.SelectedIndex = iSelectedIndex;

			DisplayCurrentTabMap();
		}

		//private void DataToScreen_AddCoalitionTab(BriefingCoalition coalition)
		//{
		//	UcBriefingCoalition ucbc = new UcBriefingCoalition(UcMap, BriefingContainer, coalition);
		//	TabPageBriefing tpb = new TabPageBriefing(coalition.CoalitionName, ucbc);
		//	TcMissionData.TabPages.Add(tpb);
		//	tpb.UcBriefing.DataToScreen();
		//}

		//public override void ScreenToData()
		//{
		//	foreach (TabPageBriefing tpb in TcMissionData.TabPages.OfType<TabPageBriefing>())
		//		tpb.UcBriefing.ScreenToData();
		//}

		public void SetUcMap(UcMap ucMap)
		{
			m_ucMap = ucMap;
			foreach(UcBaseBop ucBop in GetUcBops())
				ucBop.SetUcMap(ucMap);
		}

		public void SetBopManager(BopManager bopManager)
		{
			m_bopManager = bopManager;
			foreach (UcBaseBop ucBop in GetUcBops())
				ucBop.SetBopManager(bopManager);
		}

		public void DisplayCurrentTabMap()
		{
			if (TcMain.SelectedIndex >= 0 && TcMain.TabPages[TcMain.SelectedIndex] is BopTabPage tp)
			{
				m_ucMap.SetMapData(tp.UcBop.GetMapData(), m_bopManager.Theatre.Name, false);
			}
		}

		private IEnumerable<UcBaseBop> GetUcBops()
		{
			return TcMain.TabPages.OfType<BopTabPage>().Select(_tp => _tp.UcBop);
		}
	#endregion


	private void TcMissionData_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DisplayCurrentTabMap();
		}
	}
}
