namespace DcsBriefop.Forms
{
	partial class UcBriefop
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			PnMission = new Panel();
			PnMissionTheatre = new Panel();
			LbTheatre = new Label();
			PnMissionActions = new Panel();
			BtMissionInformations = new Button();
			BtMissionMaps = new Button();
			BtMissionGroups = new Button();
			BtMissionAirbases = new Button();
			BtMissionComs = new Button();
			LbMissionDirectory = new LinkLabel();
			LbSortie = new Label();
			LbMissionTitle = new Label();
			BtBriefingPackages = new Button();
			PnBriefing = new Panel();
			DgvBriefingFolders = new Zuby.ADGV.AdvancedDataGridView();
			BtBriefingAto = new Button();
			BtBriefingFolderTest = new Button();
			LbBriefingTitle = new Label();
			PnBackground = new Panel();
			PnMission.SuspendLayout();
			PnMissionTheatre.SuspendLayout();
			PnMissionActions.SuspendLayout();
			PnBriefing.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DgvBriefingFolders).BeginInit();
			PnBackground.SuspendLayout();
			SuspendLayout();
			// 
			// PnMission
			// 
			PnMission.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			PnMission.BorderStyle = BorderStyle.FixedSingle;
			PnMission.Controls.Add(PnMissionTheatre);
			PnMission.Controls.Add(PnMissionActions);
			PnMission.Controls.Add(LbMissionDirectory);
			PnMission.Controls.Add(LbSortie);
			PnMission.Controls.Add(LbMissionTitle);
			PnMission.Location = new Point(0, 0);
			PnMission.Margin = new Padding(4, 3, 4, 3);
			PnMission.Name = "PnMission";
			PnMission.Size = new Size(1034, 236);
			PnMission.TabIndex = 0;
			// 
			// PnMissionTheatre
			// 
			PnMissionTheatre.Controls.Add(LbTheatre);
			PnMissionTheatre.Dock = DockStyle.Bottom;
			PnMissionTheatre.Location = new Point(0, 171);
			PnMissionTheatre.Margin = new Padding(4, 3, 4, 3);
			PnMissionTheatre.Name = "PnMissionTheatre";
			PnMissionTheatre.Size = new Size(1032, 63);
			PnMissionTheatre.TabIndex = 10;
			// 
			// LbTheatre
			// 
			LbTheatre.AutoSize = true;
			LbTheatre.Location = new Point(479, 23);
			LbTheatre.Margin = new Padding(4, 0, 4, 0);
			LbTheatre.Name = "LbTheatre";
			LbTheatre.Size = new Size(46, 15);
			LbTheatre.TabIndex = 1;
			LbTheatre.Text = "Theatre";
			LbTheatre.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// PnMissionActions
			// 
			PnMissionActions.AutoSize = true;
			PnMissionActions.Controls.Add(BtMissionInformations);
			PnMissionActions.Controls.Add(BtMissionMaps);
			PnMissionActions.Controls.Add(BtMissionGroups);
			PnMissionActions.Controls.Add(BtMissionAirbases);
			PnMissionActions.Controls.Add(BtMissionComs);
			PnMissionActions.Location = new Point(44, 120);
			PnMissionActions.Margin = new Padding(4, 3, 4, 3);
			PnMissionActions.Name = "PnMissionActions";
			PnMissionActions.Size = new Size(788, 44);
			PnMissionActions.TabIndex = 9;
			// 
			// BtMissionInformations
			// 
			BtMissionInformations.Location = new Point(4, 0);
			BtMissionInformations.Margin = new Padding(4, 3, 4, 3);
			BtMissionInformations.Name = "BtMissionInformations";
			BtMissionInformations.Size = new Size(150, 40);
			BtMissionInformations.TabIndex = 3;
			BtMissionInformations.Text = "Informations";
			BtMissionInformations.UseVisualStyleBackColor = true;
			BtMissionInformations.Click += BtMissionInformations_Click;
			// 
			// BtMissionMaps
			// 
			BtMissionMaps.Location = new Point(476, 0);
			BtMissionMaps.Margin = new Padding(4, 3, 4, 3);
			BtMissionMaps.Name = "BtMissionMaps";
			BtMissionMaps.Size = new Size(150, 40);
			BtMissionMaps.TabIndex = 4;
			BtMissionMaps.Text = "Maps";
			BtMissionMaps.UseVisualStyleBackColor = true;
			BtMissionMaps.Click += BtMissionMaps_Click;
			// 
			// BtMissionGroups
			// 
			BtMissionGroups.Location = new Point(318, 0);
			BtMissionGroups.Margin = new Padding(4, 3, 4, 3);
			BtMissionGroups.Name = "BtMissionGroups";
			BtMissionGroups.Size = new Size(150, 40);
			BtMissionGroups.TabIndex = 5;
			BtMissionGroups.Text = "Groups";
			BtMissionGroups.UseVisualStyleBackColor = true;
			BtMissionGroups.Click += BtMissionGroups_Click;
			// 
			// BtMissionAirbases
			// 
			BtMissionAirbases.Location = new Point(161, 0);
			BtMissionAirbases.Margin = new Padding(4, 3, 4, 3);
			BtMissionAirbases.Name = "BtMissionAirbases";
			BtMissionAirbases.Size = new Size(150, 40);
			BtMissionAirbases.TabIndex = 6;
			BtMissionAirbases.Text = "Airbases";
			BtMissionAirbases.UseVisualStyleBackColor = true;
			BtMissionAirbases.Click += BtMissionAirdromes_Click;
			// 
			// BtMissionComs
			// 
			BtMissionComs.Location = new Point(634, 0);
			BtMissionComs.Margin = new Padding(4, 3, 4, 3);
			BtMissionComs.Name = "BtMissionComs";
			BtMissionComs.Size = new Size(150, 40);
			BtMissionComs.TabIndex = 7;
			BtMissionComs.Text = "Coms";
			BtMissionComs.UseVisualStyleBackColor = true;
			// 
			// LbMissionDirectory
			// 
			LbMissionDirectory.Dock = DockStyle.Top;
			LbMissionDirectory.Location = new Point(0, 78);
			LbMissionDirectory.Margin = new Padding(4, 0, 4, 0);
			LbMissionDirectory.Name = "LbMissionDirectory";
			LbMissionDirectory.Size = new Size(1032, 23);
			LbMissionDirectory.TabIndex = 8;
			LbMissionDirectory.TabStop = true;
			LbMissionDirectory.Text = "linkLabel1";
			LbMissionDirectory.TextAlign = ContentAlignment.MiddleCenter;
			LbMissionDirectory.LinkClicked += LbMissionDirectory_LinkClicked;
			// 
			// LbSortie
			// 
			LbSortie.Dock = DockStyle.Top;
			LbSortie.Location = new Point(0, 39);
			LbSortie.Margin = new Padding(4, 0, 4, 0);
			LbSortie.Name = "LbSortie";
			LbSortie.Size = new Size(1032, 39);
			LbSortie.TabIndex = 0;
			LbSortie.Text = "sortie";
			LbSortie.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// LbMissionTitle
			// 
			LbMissionTitle.Dock = DockStyle.Top;
			LbMissionTitle.Location = new Point(0, 0);
			LbMissionTitle.Margin = new Padding(4, 0, 4, 0);
			LbMissionTitle.Name = "LbMissionTitle";
			LbMissionTitle.Size = new Size(1032, 39);
			LbMissionTitle.TabIndex = 2;
			LbMissionTitle.Text = "Mission";
			LbMissionTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// BtBriefingPackages
			// 
			BtBriefingPackages.Location = new Point(587, 229);
			BtBriefingPackages.Margin = new Padding(4, 3, 4, 3);
			BtBriefingPackages.Name = "BtBriefingPackages";
			BtBriefingPackages.Size = new Size(150, 40);
			BtBriefingPackages.TabIndex = 10;
			BtBriefingPackages.Text = "Packages";
			BtBriefingPackages.UseVisualStyleBackColor = true;
			// 
			// PnBriefing
			// 
			PnBriefing.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			PnBriefing.BorderStyle = BorderStyle.FixedSingle;
			PnBriefing.Controls.Add(DgvBriefingFolders);
			PnBriefing.Controls.Add(BtBriefingAto);
			PnBriefing.Controls.Add(BtBriefingPackages);
			PnBriefing.Controls.Add(BtBriefingFolderTest);
			PnBriefing.Controls.Add(LbBriefingTitle);
			PnBriefing.Location = new Point(0, 257);
			PnBriefing.Margin = new Padding(4, 3, 4, 3);
			PnBriefing.Name = "PnBriefing";
			PnBriefing.Size = new Size(1034, 274);
			PnBriefing.TabIndex = 1;
			// 
			// DgvBriefingFolders
			// 
			DgvBriefingFolders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvBriefingFolders.FilterAndSortEnabled = true;
			DgvBriefingFolders.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvBriefingFolders.Location = new Point(3, 42);
			DgvBriefingFolders.Name = "DgvBriefingFolders";
			DgvBriefingFolders.RightToLeft = RightToLeft.No;
			DgvBriefingFolders.RowTemplate.Height = 25;
			DgvBriefingFolders.Size = new Size(1026, 150);
			DgvBriefingFolders.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvBriefingFolders.TabIndex = 12;
			// 
			// BtBriefingAto
			// 
			BtBriefingAto.Location = new Point(745, 229);
			BtBriefingAto.Margin = new Padding(4, 3, 4, 3);
			BtBriefingAto.Name = "BtBriefingAto";
			BtBriefingAto.Size = new Size(150, 40);
			BtBriefingAto.TabIndex = 11;
			BtBriefingAto.Text = "ATO";
			BtBriefingAto.UseVisualStyleBackColor = true;
			// 
			// BtBriefingFolderTest
			// 
			BtBriefingFolderTest.Location = new Point(4, 198);
			BtBriefingFolderTest.Margin = new Padding(4, 3, 4, 3);
			BtBriefingFolderTest.Name = "BtBriefingFolderTest";
			BtBriefingFolderTest.Size = new Size(150, 40);
			BtBriefingFolderTest.TabIndex = 4;
			BtBriefingFolderTest.Text = "Test folder";
			BtBriefingFolderTest.UseVisualStyleBackColor = true;
			BtBriefingFolderTest.Click += BtBriefingPage_Click;
			// 
			// LbBriefingTitle
			// 
			LbBriefingTitle.Dock = DockStyle.Top;
			LbBriefingTitle.Location = new Point(0, 0);
			LbBriefingTitle.Margin = new Padding(4, 0, 4, 0);
			LbBriefingTitle.Name = "LbBriefingTitle";
			LbBriefingTitle.Size = new Size(1032, 39);
			LbBriefingTitle.TabIndex = 3;
			LbBriefingTitle.Text = "Briefing";
			LbBriefingTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// PnBackground
			// 
			PnBackground.BackColor = Color.Transparent;
			PnBackground.Controls.Add(PnMission);
			PnBackground.Controls.Add(PnBriefing);
			PnBackground.Location = new Point(4, 3);
			PnBackground.Margin = new Padding(4, 3, 4, 3);
			PnBackground.Name = "PnBackground";
			PnBackground.Size = new Size(1035, 532);
			PnBackground.TabIndex = 2;
			// 
			// UcBriefop
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(PnBackground);
			Margin = new Padding(4, 3, 4, 3);
			Name = "UcBriefop";
			Size = new Size(1042, 539);
			PnMission.ResumeLayout(false);
			PnMission.PerformLayout();
			PnMissionTheatre.ResumeLayout(false);
			PnMissionTheatre.PerformLayout();
			PnMissionActions.ResumeLayout(false);
			PnBriefing.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DgvBriefingFolders).EndInit();
			PnBackground.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel PnMission;
		private System.Windows.Forms.Label LbTheatre;
		private System.Windows.Forms.Label LbSortie;
		private System.Windows.Forms.Panel PnBriefing;
		private System.Windows.Forms.Panel PnBackground;
		private System.Windows.Forms.Button BtMissionGroups;
		private System.Windows.Forms.Button BtMissionMaps;
		private System.Windows.Forms.Button BtMissionInformations;
		private System.Windows.Forms.Label LbMissionTitle;
		private System.Windows.Forms.Label LbBriefingTitle;
		private System.Windows.Forms.Button BtMissionAirbases;
		private System.Windows.Forms.Button BtMissionComs;
		private System.Windows.Forms.LinkLabel LbMissionDirectory;
		private System.Windows.Forms.Panel PnMissionActions;
		private System.Windows.Forms.Button BtBriefingPackages;
		private System.Windows.Forms.Panel PnMissionTheatre;
		private System.Windows.Forms.Button BtBriefingFolderTest;
		private Button BtBriefingAto;
		private Zuby.ADGV.AdvancedDataGridView DgvBriefingFolders;
	}
}
