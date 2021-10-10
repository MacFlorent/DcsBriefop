using DcsBriefop.Briefing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingPack : UcBaseBriefing
	{
		public UcBriefingPack(UcMap ucMap, BriefingPack bp) : base (ucMap, bp)
		{
			InitializeComponent();

			LbSortie.Text = "";
			TcMissionData.TabPages.Clear();
		}

		public override void DataToScreen()
		{
			LbTheatre.Text = BriefingPack.Theatre.Name;
			LbSortie.Text = BriefingPack.Sortie;

			CkDisplayRed.Checked = BriefingPack.DisplayRed;
			CkDisplayBlue.Checked = BriefingPack.DisplayBlue;
			CkDisplayNeutral.Checked = BriefingPack.DisplayNeutral;

			TcMissionData.TabPages.Clear();

			UcBriefingSituation ucbs = new UcBriefingSituation(UcMap, BriefingPack);
			TabPageBriefing tpb = new TabPageBriefing("SITUATION", ucbs);
			TcMissionData.TabPages.Add(tpb);
			tpb.UcBriefing.DataToScreen();

			if (BriefingPack.DisplayRed)
				DataToScreen_AddCoalitionTab(BriefingPack.BriefingRed);
			if (BriefingPack.DisplayBlue)
				DataToScreen_AddCoalitionTab(BriefingPack.BriefingBlue);
			if (BriefingPack.DisplayNeutral)
				DataToScreen_AddCoalitionTab(BriefingPack.BriefingNeutral);

			DisplayCurrentTabMap();
		}

		private void DataToScreen_AddCoalitionTab(BriefingCoalition bc)
		{
			UcBriefingCoalition ucbc = new UcBriefingCoalition(UcMap, BriefingPack, bc);
			TabPageBriefing tpb = new TabPageBriefing(bc.Name, ucbc);
			TcMissionData.TabPages.Add(tpb);
			tpb.UcBriefing.DataToScreen();
		}

		public override void ScreenToData()
		{
			BriefingPack.DisplayRed = CkDisplayRed.Checked;
			BriefingPack.DisplayBlue = CkDisplayBlue.Checked;
			BriefingPack.DisplayNeutral = CkDisplayNeutral.Checked;

			foreach (TabPageBriefing tpb in TcMissionData.TabPages.OfType<TabPageBriefing>())
				tpb.UcBriefing.ScreenToData();

		}

		private void CkDisplayCoalition_CheckedChanged(object sender, System.EventArgs e)
		{
			if (sender is CheckBox ck && BriefingPack is object)
			{

				bool bDisplay = ck.Checked;
				if (ck == CkDisplayRed && ck.Checked != BriefingPack.DisplayRed)
				{
					BriefingPack.DisplayRed = ck.Checked;
					DataToScreen();
				}
				else if (ck == CkDisplayBlue && ck.Checked != BriefingPack.DisplayBlue)
				{
					BriefingPack.DisplayBlue = ck.Checked;
					DataToScreen();
				}
				if (ck == CkDisplayNeutral && ck.Checked != BriefingPack.DisplayNeutral)
				{
					BriefingPack.DisplayNeutral = ck.Checked;
					DataToScreen();
				}
			}
		}

		private void DisplayCurrentTabMap()
		{
			if (TcMissionData.SelectedIndex >= 0 && TcMissionData.TabPages[TcMissionData.SelectedIndex] is TabPageBriefing tp)
			{
				CustomDataMap cdm = null;
				if (tp.UcBriefing is UcBriefingSituation ucBs)
				{
					cdm = ucBs.BriefingPack.MapData;
				}
				else if (tp.UcBriefing is UcBriefingCoalition ucBc)
				{
					cdm = ucBc.BriefingCoalition.MapData;
				}

				UcMap.SetMapData(cdm);
			}

		}

		private void TcMissionData_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			DisplayCurrentTabMap();
		}
	}
}
