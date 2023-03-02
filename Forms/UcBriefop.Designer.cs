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
			LbMissionTheatre = new Label();
			PnMissionActions = new Panel();
			BtMissionInformations = new Button();
			BtMissionMaps = new Button();
			BtMissionGroups = new Button();
			BtMissionAirbases = new Button();
			BtMissionComs = new Button();
			LbMissionDirectory = new LinkLabel();
			LbMissionSortie = new Label();
			PnMissionPicture = new Panel();
			LbMissionTitle = new Label();
			BtBriefingPackages = new Button();
			PnBriefing = new Panel();
			BtBriefingFolderAdd = new Button();
			BtBriefingFolderDelete = new Button();
			BtBriefingFolderDetail = new Button();
			PnBriefingPicture = new Panel();
			LbBriefingTitle = new Label();
			LbBriefingFolders = new Label();
			PnBriefingActions = new Panel();
			BtBriefingGenerate = new Button();
			BtBriefingAto = new Button();
			DgvBriefingFolders = new Zuby.ADGV.AdvancedDataGridView();
			PnBackground = new Panel();
			PnMission.SuspendLayout();
			PnMissionTheatre.SuspendLayout();
			PnMissionActions.SuspendLayout();
			PnMissionPicture.SuspendLayout();
			PnBriefing.SuspendLayout();
			PnBriefingPicture.SuspendLayout();
			PnBriefingActions.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DgvBriefingFolders).BeginInit();
			PnBackground.SuspendLayout();
			SuspendLayout();
			// 
			// PnMission
			// 
			PnMission.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			PnMission.BorderStyle = BorderStyle.FixedSingle;
			PnMission.Controls.Add(PnMissionTheatre);
			PnMission.Controls.Add(PnMissionActions);
			PnMission.Controls.Add(LbMissionDirectory);
			PnMission.Controls.Add(LbMissionSortie);
			PnMission.Controls.Add(PnMissionPicture);
			PnMission.Location = new Point(0, 0);
			PnMission.Margin = new Padding(4, 3, 4, 3);
			PnMission.Name = "PnMission";
			PnMission.Size = new Size(1034, 236);
			PnMission.TabIndex = 0;
			// 
			// PnMissionTheatre
			// 
			PnMissionTheatre.Controls.Add(LbMissionTheatre);
			PnMissionTheatre.Dock = DockStyle.Bottom;
			PnMissionTheatre.Location = new Point(0, 171);
			PnMissionTheatre.Margin = new Padding(4, 3, 4, 3);
			PnMissionTheatre.Name = "PnMissionTheatre";
			PnMissionTheatre.Size = new Size(1032, 63);
			PnMissionTheatre.TabIndex = 10;
			// 
			// LbMissionTheatre
			// 
			LbMissionTheatre.AutoSize = true;
			LbMissionTheatre.Location = new Point(479, 23);
			LbMissionTheatre.Margin = new Padding(4, 0, 4, 0);
			LbMissionTheatre.Name = "LbMissionTheatre";
			LbMissionTheatre.Size = new Size(46, 15);
			LbMissionTheatre.TabIndex = 1;
			LbMissionTheatre.Text = "Theatre";
			LbMissionTheatre.TextAlign = ContentAlignment.MiddleCenter;
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
			LbMissionDirectory.Location = new Point(0, 91);
			LbMissionDirectory.Margin = new Padding(4, 0, 4, 0);
			LbMissionDirectory.Name = "LbMissionDirectory";
			LbMissionDirectory.Size = new Size(1032, 23);
			LbMissionDirectory.TabIndex = 8;
			LbMissionDirectory.TabStop = true;
			LbMissionDirectory.Text = "linkLabel1";
			LbMissionDirectory.TextAlign = ContentAlignment.MiddleCenter;
			LbMissionDirectory.LinkClicked += LbMissionDirectory_LinkClicked;
			// 
			// LbMissionSortie
			// 
			LbMissionSortie.Dock = DockStyle.Top;
			LbMissionSortie.Location = new Point(0, 52);
			LbMissionSortie.Margin = new Padding(4, 0, 4, 0);
			LbMissionSortie.Name = "LbMissionSortie";
			LbMissionSortie.Size = new Size(1032, 39);
			LbMissionSortie.TabIndex = 0;
			LbMissionSortie.Text = "sortie";
			LbMissionSortie.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// PnMissionPicture
			// 
			PnMissionPicture.Controls.Add(LbMissionTitle);
			PnMissionPicture.Dock = DockStyle.Top;
			PnMissionPicture.Location = new Point(0, 0);
			PnMissionPicture.Name = "PnMissionPicture";
			PnMissionPicture.Size = new Size(1032, 52);
			PnMissionPicture.TabIndex = 11;
			// 
			// LbMissionTitle
			// 
			LbMissionTitle.AutoSize = true;
			LbMissionTitle.Location = new Point(494, 11);
			LbMissionTitle.Margin = new Padding(4, 0, 4, 0);
			LbMissionTitle.Name = "LbMissionTitle";
			LbMissionTitle.Size = new Size(48, 15);
			LbMissionTitle.TabIndex = 3;
			LbMissionTitle.Text = "Mission";
			LbMissionTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// BtBriefingPackages
			// 
			BtBriefingPackages.Location = new Point(162, 3);
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
			PnBriefing.Controls.Add(BtBriefingFolderAdd);
			PnBriefing.Controls.Add(BtBriefingFolderDelete);
			PnBriefing.Controls.Add(BtBriefingFolderDetail);
			PnBriefing.Controls.Add(PnBriefingPicture);
			PnBriefing.Controls.Add(LbBriefingFolders);
			PnBriefing.Controls.Add(PnBriefingActions);
			PnBriefing.Controls.Add(DgvBriefingFolders);
			PnBriefing.Location = new Point(0, 257);
			PnBriefing.Margin = new Padding(4, 3, 4, 3);
			PnBriefing.Name = "PnBriefing";
			PnBriefing.Size = new Size(1034, 341);
			PnBriefing.TabIndex = 1;
			// 
			// BtBriefingFolderAdd
			// 
			BtBriefingFolderAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtBriefingFolderAdd.Location = new Point(842, 312);
			BtBriefingFolderAdd.Margin = new Padding(4, 3, 4, 3);
			BtBriefingFolderAdd.Name = "BtBriefingFolderAdd";
			BtBriefingFolderAdd.Size = new Size(89, 24);
			BtBriefingFolderAdd.TabIndex = 18;
			BtBriefingFolderAdd.Text = "Add";
			BtBriefingFolderAdd.UseVisualStyleBackColor = true;
			BtBriefingFolderAdd.Click += BtBriefingFolderAdd_Click;
			// 
			// BtBriefingFolderDelete
			// 
			BtBriefingFolderDelete.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtBriefingFolderDelete.Location = new Point(939, 312);
			BtBriefingFolderDelete.Margin = new Padding(4, 3, 4, 3);
			BtBriefingFolderDelete.Name = "BtBriefingFolderDelete";
			BtBriefingFolderDelete.Size = new Size(89, 24);
			BtBriefingFolderDelete.TabIndex = 17;
			BtBriefingFolderDelete.Text = "Delete";
			BtBriefingFolderDelete.UseVisualStyleBackColor = true;
			// 
			// BtBriefingFolderDetail
			// 
			BtBriefingFolderDetail.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			BtBriefingFolderDetail.Location = new Point(3, 312);
			BtBriefingFolderDetail.Margin = new Padding(4, 3, 4, 3);
			BtBriefingFolderDetail.Name = "BtBriefingFolderDetail";
			BtBriefingFolderDetail.Size = new Size(89, 24);
			BtBriefingFolderDetail.TabIndex = 16;
			BtBriefingFolderDetail.Text = "Detail";
			BtBriefingFolderDetail.UseVisualStyleBackColor = true;
			BtBriefingFolderDetail.Click += BtBriefingFolderDetail_Click;
			// 
			// PnBriefingPicture
			// 
			PnBriefingPicture.Controls.Add(LbBriefingTitle);
			PnBriefingPicture.Dock = DockStyle.Top;
			PnBriefingPicture.Location = new Point(0, 0);
			PnBriefingPicture.Name = "PnBriefingPicture";
			PnBriefingPicture.Size = new Size(1032, 52);
			PnBriefingPicture.TabIndex = 15;
			// 
			// LbBriefingTitle
			// 
			LbBriefingTitle.AutoSize = true;
			LbBriefingTitle.Location = new Point(479, 20);
			LbBriefingTitle.Margin = new Padding(4, 0, 4, 0);
			LbBriefingTitle.Name = "LbBriefingTitle";
			LbBriefingTitle.Size = new Size(48, 15);
			LbBriefingTitle.TabIndex = 3;
			LbBriefingTitle.Text = "Briefing";
			LbBriefingTitle.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// LbBriefingFolders
			// 
			LbBriefingFolders.Location = new Point(298, 128);
			LbBriefingFolders.Margin = new Padding(4, 0, 4, 0);
			LbBriefingFolders.Name = "LbBriefingFolders";
			LbBriefingFolders.Size = new Size(425, 24);
			LbBriefingFolders.TabIndex = 14;
			LbBriefingFolders.Text = "Folders";
			LbBriefingFolders.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// PnBriefingActions
			// 
			PnBriefingActions.Controls.Add(BtBriefingGenerate);
			PnBriefingActions.Controls.Add(BtBriefingAto);
			PnBriefingActions.Controls.Add(BtBriefingPackages);
			PnBriefingActions.Location = new Point(274, 58);
			PnBriefingActions.Name = "PnBriefingActions";
			PnBriefingActions.Size = new Size(473, 52);
			PnBriefingActions.TabIndex = 13;
			// 
			// BtBriefingGenerate
			// 
			BtBriefingGenerate.Location = new Point(4, 3);
			BtBriefingGenerate.Margin = new Padding(4, 3, 4, 3);
			BtBriefingGenerate.Name = "BtBriefingGenerate";
			BtBriefingGenerate.Size = new Size(150, 40);
			BtBriefingGenerate.TabIndex = 12;
			BtBriefingGenerate.Text = "Generate";
			BtBriefingGenerate.UseVisualStyleBackColor = true;
			// 
			// BtBriefingAto
			// 
			BtBriefingAto.Location = new Point(318, 3);
			BtBriefingAto.Margin = new Padding(4, 3, 4, 3);
			BtBriefingAto.Name = "BtBriefingAto";
			BtBriefingAto.Size = new Size(150, 40);
			BtBriefingAto.TabIndex = 11;
			BtBriefingAto.Text = "ATO";
			BtBriefingAto.UseVisualStyleBackColor = true;
			// 
			// DgvBriefingFolders
			// 
			DgvBriefingFolders.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			DgvBriefingFolders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvBriefingFolders.FilterAndSortEnabled = true;
			DgvBriefingFolders.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvBriefingFolders.Location = new Point(3, 155);
			DgvBriefingFolders.Name = "DgvBriefingFolders";
			DgvBriefingFolders.RightToLeft = RightToLeft.No;
			DgvBriefingFolders.RowTemplate.Height = 25;
			DgvBriefingFolders.Size = new Size(1026, 154);
			DgvBriefingFolders.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvBriefingFolders.TabIndex = 12;
			DgvBriefingFolders.CellDoubleClick += DgvBriefingFolders_CellDoubleClick;
			// 
			// PnBackground
			// 
			PnBackground.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			PnBackground.BackColor = Color.Transparent;
			PnBackground.Controls.Add(PnMission);
			PnBackground.Controls.Add(PnBriefing);
			PnBackground.Location = new Point(4, 3);
			PnBackground.Margin = new Padding(4, 3, 4, 3);
			PnBackground.Name = "PnBackground";
			PnBackground.Size = new Size(1035, 599);
			PnBackground.TabIndex = 2;
			// 
			// UcBriefop
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(PnBackground);
			Margin = new Padding(4, 3, 4, 3);
			Name = "UcBriefop";
			Size = new Size(1042, 606);
			PnMission.ResumeLayout(false);
			PnMission.PerformLayout();
			PnMissionTheatre.ResumeLayout(false);
			PnMissionTheatre.PerformLayout();
			PnMissionActions.ResumeLayout(false);
			PnMissionPicture.ResumeLayout(false);
			PnMissionPicture.PerformLayout();
			PnBriefing.ResumeLayout(false);
			PnBriefingPicture.ResumeLayout(false);
			PnBriefingPicture.PerformLayout();
			PnBriefingActions.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DgvBriefingFolders).EndInit();
			PnBackground.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel PnMission;
		private System.Windows.Forms.Label LbMissionTheatre;
		private System.Windows.Forms.Label LbMissionSortie;
		private System.Windows.Forms.Panel PnBriefing;
		private System.Windows.Forms.Panel PnBackground;
		private System.Windows.Forms.Button BtMissionGroups;
		private System.Windows.Forms.Button BtMissionMaps;
		private System.Windows.Forms.Button BtMissionInformations;
		private System.Windows.Forms.Label LbBriefingTitle;
		private System.Windows.Forms.Button BtMissionAirbases;
		private System.Windows.Forms.Button BtMissionComs;
		private System.Windows.Forms.LinkLabel LbMissionDirectory;
		private System.Windows.Forms.Panel PnMissionActions;
		private System.Windows.Forms.Button BtBriefingPackages;
		private System.Windows.Forms.Panel PnMissionTheatre;
		private Button BtBriefingAto;
		private Zuby.ADGV.AdvancedDataGridView DgvBriefingFolders;
		private Label LbBriefingFolders;
		private Panel PnBriefingActions;
		private Button BtBriefingGenerate;
		private Panel PnBriefingPicture;
		private Panel PnMissionPicture;
		private Label LbMissionTitle;
		private Button BtBriefingFolderAdd;
		private Button BtBriefingFolderDelete;
		private Button BtBriefingFolderDetail;
	}
}
