using DcsBriefop.Briefing;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	internal partial class UcBriefingPack : UserControl
	{
		public BriefingPack BriefingPack { get; set; }

		public UcBriefingPack()
		{
			InitializeComponent();

			BriefingPack = null;
			LbSortie.Text = "";
			TcMissionData.TabPages.Clear();
		}

		public void DataToScreen()
		{
			LbSortie.Text = BriefingPack.Sortie;

			CkDisplayRed.Checked = BriefingPack.DisplayRed;
			CkDisplayBlue.Checked = BriefingPack.DisplayBlue;
			CkDisplayNeutral.Checked = BriefingPack.DisplayNeutral;

			TcMissionData.TabPages.Clear();
			if (BriefingPack.DisplayRed)
				DataToScreen_Coalition(BriefingPack.BriefingPackRed);
			if (BriefingPack.DisplayBlue)
				DataToScreen_Coalition(BriefingPack.BriefingPackBlue);
			if (BriefingPack.DisplayNeutral)
				DataToScreen_Coalition(BriefingPack.BriefingPackNeutral);
		}

		private void DataToScreen_Coalition(BriefingPackCoalition bpc)
		{
			DataToScreen_Page<UcPageSituation>(bpc.BriefingPageSituation);
		}

		private void DataToScreen_Page<UcT>(BaseBriefingPage bbp) where UcT : UcBaseBriefingPage, new()
		{
			UcT uc = null;
			TabPageBriefing tpb = GetTabPageBriefing(bbp);

			if (tpb is null)
			{
				uc = new UcT();
				uc.BriefingPage = bbp;
				tpb = new TabPageBriefing(uc);
				TcMissionData.TabPages.Add(tpb);
			}
			else
				uc = tpb.UcBriefingPage as UcT;

			uc.DataToScreen();
		}

		public void ScreenToData()
		{
			BriefingPack.DisplayRed = CkDisplayRed.Checked;
			BriefingPack.DisplayBlue = CkDisplayBlue.Checked;
			BriefingPack.DisplayNeutral = CkDisplayNeutral.Checked;

			if (TcMissionData.TabPages[TcMissionData.SelectedIndex] is TabPageBriefing tp)
			{
				tp.UcBriefingPage.ScreenToData();
			}
		}

		private TabPageBriefing GetTabPageBriefing(BaseBriefingPage bbp)
		{
			foreach (TabPageBriefing tpb in TcMissionData.TabPages.OfType<TabPageBriefing>())
			{
				if (tpb.UcBriefingPage.BriefingPage.Label == bbp.Label)
					return tpb;
			}

			return null;
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

		private void TcMissionData_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (sender is TabControl tc && tc.SelectedIndex >= 0 && TcMissionData.TabPages[TcMissionData.SelectedIndex] is TabPageBriefing tp)
			{
				tp.UcBriefingPage.DataToScreen();
			}
		}
	}
}
