using DcsBriefop.Briefing;
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

			TcMissionData.TabPages.Clear();
			DataToScreen_Coalition(BriefingPack.BriefingPackRed, CkDisplayRed);
			DataToScreen_Coalition(BriefingPack.BriefingPackBlue, CkDisplayBlue);
			DataToScreen_Coalition(BriefingPack.BriefingPackNeutral, CkDisplayNeutral);
		}

		private void DataToScreen_Coalition(BriefingPackCoalition bpc, CheckBox ckDisplayed)
		{
			if (bpc.Displayed)
			{
				DataToScreen_Situation(bpc);
				ckDisplayed.Checked = true;
			}
			else
				ckDisplayed.Checked = false;
		}

		private void DataToScreen_Situation(BriefingPackCoalition bpc)
		{
			UcSituation ucs = new UcSituation();
			ucs.PageSituation = bpc.BriefingPageSituation;
			ucs.DataToScreen();

			TabPage tpSituation = new TabPage(bpc.BriefingPageSituation.Label);
			tpSituation.Controls.Clear();
			tpSituation.Controls.Add(ucs);
			ucs.Dock = DockStyle.Fill;
			TcMissionData.TabPages.Add(tpSituation);
		}


		public void SaveMissionManager(MissionManager manager) { }

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
			if (sender is TabControl tc && tc.SelectedIndex >= 0)
			{
				TabPage tp = TcMissionData.TabPages[TcMissionData.SelectedIndex];
				if (tp.Controls.Count == 1 && tp.Controls[0] is UcSituation ucs)
				{
					ucs.DataToScreen();
				}
			}
		}
	}
}
