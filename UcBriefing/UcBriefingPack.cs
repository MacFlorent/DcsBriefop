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

			CkDisplayRed.Checked = BriefingPack.BriefingPackRed.Displayed;
			CkDisplayBlue.Checked = BriefingPack.BriefingPackBlue.Displayed;
			CkDisplayNeutral.Checked = BriefingPack.BriefingPackNeutral.Displayed;

			TcMissionData.TabPages.Clear();
			DataToScreen_Coalition(BriefingPack.BriefingPackRed);
			DataToScreen_Coalition(BriefingPack.BriefingPackBlue);
			DataToScreen_Coalition(BriefingPack.BriefingPackNeutral);
		}

		private void DataToScreen_Coalition(BriefingPackCoalition bpc)
		{
			if (!bpc.Displayed)
				return;

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
			BriefingPack.BriefingPackRed.Displayed = CkDisplayRed.Checked;
			BriefingPack.BriefingPackBlue.Displayed = CkDisplayBlue.Checked;
			BriefingPack.BriefingPackNeutral.Displayed = CkDisplayNeutral.Checked;

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

				BriefingPackCoalition bpc = null;
				if (ck == CkDisplayRed)
					bpc = BriefingPack.BriefingPackRed;
				if (ck == CkDisplayBlue)
					bpc = BriefingPack.BriefingPackBlue;
				if (ck == CkDisplayNeutral)
					bpc = BriefingPack.BriefingPackNeutral;

				if (ck.Checked != bpc.Displayed)
				{
					bpc.Displayed = ck.Checked;
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
