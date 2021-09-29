using DcsBriefop.Briefing;
using DcsBriefop.UcBriefing;
using System;
using System.Windows.Forms;

namespace DcsBriefop
{
	//http://www.independent-software.com/gmap-net-beginners-tutorial-maps-markers-polygons-routes-updated-for-vs2015-and-gmap1-7.html
	public partial class FrmMain : Form
	{
		#region Fields
		private MissionManager m_missionManager;
		private BriefingPack m_briefingPack;
		private UcBriefingPack m_ucBriefingPack;
		private UcMap m_ucMap;

		private string m_sDcsFileFilter = "DCS mission files (*.miz)|*.miz|All files (*.*)|*.*";
		//private string m_sExcelFileFilter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
		#endregion

		#region CTOR
		public FrmMain()
		{
			InitializeComponent();
			BuildMenu();
		}
		#endregion

		#region Methods
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
					m_briefingPack = new BriefingPack(m_missionManager);
					DataToScreen();

				}
			}
		}

		private void MizReload()
		{
			if (m_missionManager is null)
				throw new ExceptionDcsBriefop("No mission is currently loaded");

			m_missionManager.MizLoad();
			m_briefingPack = new BriefingPack(m_missionManager);
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
			if (m_ucMap is null)
			{
				m_ucMap = new UcMap();
				m_ucMap.Dock = DockStyle.Fill;
				SplitContainer.Panel2.Controls.Clear();
				SplitContainer.Panel2.Controls.Add(m_ucMap);
			}

			if (m_ucBriefingPack is null)
			{
				m_ucBriefingPack = new UcBriefingPack(m_ucMap, m_briefingPack);
				m_ucBriefingPack.Dock = DockStyle.Fill;
				SplitContainer.Panel1.Controls.Clear();
				SplitContainer.Panel1.Controls.Add(m_ucBriefingPack);
			}
			else
				m_ucBriefingPack.BriefingPack = m_briefingPack;

			StatusStrip.Items.Clear();
			StatusStrip.Items.Add(m_missionManager.MizFilePath);

			m_ucBriefingPack.DataToScreen();
		}

		private void ScreenToData()
		{
			m_ucBriefingPack.ScreenToData();
		}

		private void Test()
		{
			//SplitContainer.Panel1.Controls.Clear();
			//FlowLayoutPanel f = new FlowLayoutPanel();
			//f.FlowDirection = FlowDirection.LeftToRight;
			//f.Dock = DockStyle.Fill;
			//SplitContainer.Panel1.Controls.Add(f);

			//TextBox tb1 = new TextBox();
			//tb1.Multiline = true;
			//tb1.Height = 500;
			//tb1.Width = f.Width;
			//TextBox tb2 = new TextBox();
			//tb2.Multiline = true;
			//tb2.Height = 500;
			//tb2.Width = f.Width;
			//f.Controls.Add(tb1);
			//f.Controls.Add(tb2);

			//string sFilePath = @"D:\Projects\dictionary";
			//string s = MissionManager.ReadLuaFileContent(sFilePath);
			//var v = LsonLib.LsonVars.Parse(s);

			//LsonLib.LsonDict root = v["dictionary"].GetDict();
			//s = root["DictKey_descriptionText_1"].GetString();

			//tb1.Text = s;

			//s = MissionManager.LsonRootToCorrectedString(v);

			//tb2.Text = s;
			//System.IO.File.WriteAllText(sFilePath + "_mod", s);

		}
		#endregion

		#region Menus
		private class MenuName
		{
			public static readonly string Open = "Open";
			public static readonly string Reload = "Reload";
			public static readonly string Save = "Save";
			public static readonly string SaveAs = "SaveAs";
			public static readonly string Test = "Test";
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
			tsmiFile.DropDownItems.Add(MenuItem("Test", MenuName.Test));
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
			else if (tsi.Name == MenuName.Test)
			{
				Test();
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
		}
		#endregion
	}
}
