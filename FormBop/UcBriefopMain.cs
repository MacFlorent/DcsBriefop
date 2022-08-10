using DcsBriefop.Data;
using DcsBriefop.DataBop;
using DcsBriefop.DataBopCustom;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.FormBop
{
	internal partial class UcBriefopMain : UcBaseBop
	{
		public UcBriefopMain(UcMap ucMap, BopManager briefopManager) : base (ucMap, briefopManager)
		{
			InitializeComponent();

			TcMissionData.TabPages.Clear();
		}

		public override void DataToScreen()
		{
			int iSelectedIndex = -1;
			if (TcMissionData.TabPages.Count > 0)
			{
				iSelectedIndex = TcMissionData.SelectedIndex;
				TcMissionData.DisposeAndClearTabs();
			}

			UcBopGeneral ucBriefingGeneral = new UcBopGeneral(UcMap, BopManager);
			TabPageBriefop tpb = new TabPageBriefop("General", ucBriefingGeneral);
			TcMissionData.TabPages.Add(tpb);
			tpb.UcBriefop.DataToScreen();

			//foreach (BriefingCoalition coalition in BriefingContainer.BriefingCoalitions.Where(_c => _c.Included))
			//{
			//	DataToScreen_AddCoalitionTab(coalition);
			//}

			if (iSelectedIndex >= 0 && iSelectedIndex < TcMissionData.TabCount)
				TcMissionData.SelectedIndex = iSelectedIndex;

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

		public void DisplayCurrentTabMap()
		{
			if (TcMissionData.SelectedIndex >= 0 && TcMissionData.TabPages[TcMissionData.SelectedIndex] is TabPageBriefop tp)
			{
				string sTitle = null;
				BopCustomMap mapData = null;

				if (tp.UcBriefop is UcBopGeneral ucBriefopGeneral)
				{
					sTitle = "General map data";
					mapData = BopManager.BopMain.BopGeneral.MapData;
				}
				//else if (tp.UcBriefing is UcBriefingCoalition ucBc)
				//{
				//	sTitle = $"{ucBc.Coalition.CoalitionName} coalition map data";
				//	mapData = ucBc.Coalition.MapData;
				//}

				UcMap.SetMapData(mapData, BopManager.Theatre.Name, sTitle, false);
			}

		}

		private void TcMissionData_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DisplayCurrentTabMap();
		}
	}
}
