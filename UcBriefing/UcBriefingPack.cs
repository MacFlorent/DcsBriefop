﻿using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using System.Linq;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingPack : UcBaseBriefing
	{
		public UcBriefingPack(UcMap ucMap, BriefingContainer briefingContainer) : base (ucMap, briefingContainer)
		{
			InitializeComponent();

			TcMissionData.TabPages.Clear();
		}

		public override void DataToScreen()
		{
			TcMissionData.TabPages.Clear();

			UcBriefingSituation ucbs = new UcBriefingSituation(UcMap, BriefingContainer);
			TabPageBriefing tpb = new TabPageBriefing("situation", ucbs);
			TcMissionData.TabPages.Add(tpb);
			tpb.UcBriefing.DataToScreen();

			foreach(BriefingCoalition coalition in BriefingContainer.BriefingCoalitions.Where(_c => _c.Included))
			{
				DataToScreen_AddCoalitionTab(coalition);
			}

			DisplayCurrentTabMap();
		}

		private void DataToScreen_AddCoalitionTab(BriefingCoalition coalition)
		{
			UcBriefingCoalition ucbc = new UcBriefingCoalition(UcMap, BriefingContainer, coalition);
			TabPageBriefing tpb = new TabPageBriefing(coalition.CoalitionName, ucbc);
			TcMissionData.TabPages.Add(tpb);
			tpb.UcBriefing.DataToScreen();
		}

		public override void ScreenToData()
		{
			foreach (TabPageBriefing tpb in TcMissionData.TabPages.OfType<TabPageBriefing>())
				tpb.UcBriefing.ScreenToData();
		}

		private void DisplayCurrentTabMap()
		{
			if (TcMissionData.SelectedIndex >= 0 && TcMissionData.TabPages[TcMissionData.SelectedIndex] is TabPageBriefing tp)
			{
				string sTitle = null;
				BriefopCustomMap mapData = null;

				if (tp.UcBriefing is UcBriefingSituation ucBs)
				{
					sTitle = "General map data";
					mapData = ucBs.BriefingContainer.MapData;
				}
				else if (tp.UcBriefing is UcBriefingCoalition ucBc)
				{
					sTitle = $"{ucBc.Coalition.CoalitionName} coalition map data";
					mapData = ucBc.Coalition.MapData;
				}

				UcMap.SetMapData(mapData, BriefingContainer.Core.Theatre.Name, sTitle, false);
			}

		}

		private void TcMissionData_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DisplayCurrentTabMap();
		}
	}
}
