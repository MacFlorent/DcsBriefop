using DcsBriefop.Briefing;
using DcsBriefop.UcBriefing;
using System;
using System.Windows.Forms;

namespace DcsBriefop
{
	//http://www.independent-software.com/gmap-net-beginners-tutorial-maps-markers-polygons-routes-updated-for-vs2015-and-gmap1-7.html
	public partial class FrmMain : Form
	{
		private MissionManager m_missionManager;
		private UcBriefingPack m_ucBriefingContainer;

		private string m_sDcsFileFilter = "DCS mission files (*.miz)|*.miz|All files (*.*)|*.*";
		private string m_sExcelFileFilter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

		public FrmMain()
		{
			InitializeComponent();

			BuildMenu();

			m_ucBriefingContainer = new UcBriefingPack();
			m_ucBriefingContainer.Dock = DockStyle.Fill;
			SplitContainer.Panel1.Controls.Clear();
			SplitContainer.Panel1.Controls.Add(m_ucBriefingContainer);
		}

		private void MizOpen()
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.InitialDirectory = @"d:\projects";
				ofd.Filter = m_sDcsFileFilter;
				ofd.RestoreDirectory = true;

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					m_missionManager = new MissionManager(ofd.FileName);
					DataToScreen();

				}
			}
		}

		private void MizReload()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			m_missionManager.MizLoad();
			DataToScreen();
		}

		private void MizSave(string sMizFilePath)
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			ScreenToData();

			m_missionManager.MizSave(sMizFilePath);
			MizReload();
		}

		private void MizSaveAs()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			using (SaveFileDialog sfd = new SaveFileDialog())
			{
				sfd.InitialDirectory = m_missionManager.MizFileDirectory;
				sfd.FileName = m_missionManager.MizFileName;
				sfd.Filter = m_sDcsFileFilter;

				if (sfd.ShowDialog() == DialogResult.OK)
				{
					MizSave(sfd.FileName);
				}
			}
		}

		private void DataToScreen()
		{
			StatusStrip.Items.Clear();
			StatusStrip.Items.Add(m_missionManager.MizFilePath);

			m_ucBriefingContainer.BriefingPack = m_missionManager.BriefingPack;
			m_ucBriefingContainer.DataToScreen();
		}

		private void ScreenToData()
		{
			m_ucBriefingContainer.ScreenToData();
		}

		#region Menus
		private class MenuName
		{
			public static readonly string Open = "Open";
			public static readonly string Reload = "Reload";
			public static readonly string Save = "Save";
			public static readonly string SaveAs = "SaveAs";
			public static readonly string Exit = "Exit";
		}

		private void BuildMenu()
		{
			MainMenu.Items.Clear();

			ToolStripMenuItem tsmiFile = MenuItemRoot("Miz", "Miz");
			tsmiFile.DropDownItems.Add(MenuItem("Open", MenuName.Open));
			tsmiFile.DropDownItems.Add(MenuItem("Reload", MenuName.Reload));
			tsmiFile.DropDownItems.Add(MenuItem("Save", MenuName.Save));
			tsmiFile.DropDownItems.Add(MenuItem("Save as", MenuName.SaveAs));
			tsmiFile.DropDownItems.Add(new ToolStripSeparator());
			tsmiFile.DropDownItems.Add(MenuItem("Exit", MenuName.Exit));
			MainMenu.Items.Add(tsmiFile);

			ToolStripMenuItem tsmiBriefing = MenuItemRoot("Briefing", "Briefing");
			MainMenu.Items.Add(tsmiBriefing);
		}

		private ToolStripMenuItem MenuItemRoot(string sLabel, string sName)
		{
			return new ToolStripMenuItem(sLabel, null, null, sName);
		}

		private ToolStripMenuItem MenuItem(string sLabel, string sName)
		{
			return new ToolStripMenuItem(sLabel, null, new EventHandler(ToolStripItemClicked), sName);
		}

		private void ToolStripItemClicked(object sender, EventArgs e)
		{
			ToolStripItem tsi = sender as ToolStripItem;
			if (tsi == null)
				return;

			if (tsi.Name == MenuName.Open)
			{
				MizOpen();
			}
			else if (tsi.Name == MenuName.Reload)
			{
				MizReload();
			}
			else if (tsi.Name == MenuName.Save)
			{
				MizSave(null);
			}
			else if (tsi.Name == MenuName.SaveAs)
			{
				MizSaveAs();
			}
			else if (tsi.Name == MenuName.Exit)
			{
				Application.Exit();
			}

		}
		#endregion

		#region Events
		private void FrmMain_Load(object sender, EventArgs e)
		{
			Map.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
			GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
			Map.SetPositionByKeywords("Paris, France");
		}
		#endregion
	}
}
