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
			string sFilePath = null;
			using (OpenFileDialog openFileDialog = new OpenFileDialog())
			{
				openFileDialog.InitialDirectory = @"d:\projects";
				openFileDialog.Filter = m_sDcsFileFilter;
				openFileDialog.RestoreDirectory = true;

				if (openFileDialog.ShowDialog() == DialogResult.OK)
				{
					sFilePath = openFileDialog.FileName;
				}
			}
			m_missionManager = new MissionManager(sFilePath);
			MizToScreen();
		}

		private void MizReload()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			m_missionManager.MizLoad();
			MizToScreen();
		}

		private void MizSave(string sMizFilePath)
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			m_missionManager.MizSave(sMizFilePath);
			MizReload();
		}

		private void MizSaveAs()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			using (SaveFileDialog saveFileDialog = new SaveFileDialog())
			{
				saveFileDialog.InitialDirectory = m_missionManager.MizFileDirectory;
				saveFileDialog.FileName = m_missionManager.MizFileName;
				saveFileDialog.Filter = m_sDcsFileFilter;

				if (saveFileDialog.ShowDialog() == DialogResult.OK)
				{
					MizSave(saveFileDialog.FileName);
				}
			}
		}

		private void MizToScreen()
		{
			m_ucBriefingContainer.BriefingPack = m_missionManager.BriefingPack;
			m_ucBriefingContainer.DataToScreen();
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
			tsmiFile.DropDownItems.Add(MenuItem("Save as", MenuName.Save));
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
