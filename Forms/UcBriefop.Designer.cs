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
			this.PnMission = new System.Windows.Forms.Panel();
			this.PnMissionActions = new System.Windows.Forms.Panel();
			this.BtMissionPackages = new System.Windows.Forms.Button();
			this.BtMissionInformations = new System.Windows.Forms.Button();
			this.BtMissionMaps = new System.Windows.Forms.Button();
			this.BtMissionGroups = new System.Windows.Forms.Button();
			this.BtMissionAirbases = new System.Windows.Forms.Button();
			this.BtMissionComs = new System.Windows.Forms.Button();
			this.LbMissionDirectory = new System.Windows.Forms.LinkLabel();
			this.LbSortie = new System.Windows.Forms.Label();
			this.LbTheatre = new System.Windows.Forms.Label();
			this.LbMissionTitle = new System.Windows.Forms.Label();
			this.PnBriefing = new System.Windows.Forms.Panel();
			this.LbBriefingTitle = new System.Windows.Forms.Label();
			this.PnBackground = new System.Windows.Forms.Panel();
			this.PnMissionTheatre = new System.Windows.Forms.Panel();
			this.PnMission.SuspendLayout();
			this.PnMissionActions.SuspendLayout();
			this.PnBriefing.SuspendLayout();
			this.PnBackground.SuspendLayout();
			this.PnMissionTheatre.SuspendLayout();
			this.SuspendLayout();
			// 
			// PnMission
			// 
			this.PnMission.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnMission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnMission.Controls.Add(this.PnMissionTheatre);
			this.PnMission.Controls.Add(this.PnMissionActions);
			this.PnMission.Controls.Add(this.LbMissionDirectory);
			this.PnMission.Controls.Add(this.LbSortie);
			this.PnMission.Controls.Add(this.LbMissionTitle);
			this.PnMission.Location = new System.Drawing.Point(0, 0);
			this.PnMission.Name = "PnMission";
			this.PnMission.Size = new System.Drawing.Size(887, 205);
			this.PnMission.TabIndex = 0;
			// 
			// PnMissionActions
			// 
			this.PnMissionActions.AutoSize = true;
			this.PnMissionActions.Controls.Add(this.BtMissionPackages);
			this.PnMissionActions.Controls.Add(this.BtMissionInformations);
			this.PnMissionActions.Controls.Add(this.BtMissionMaps);
			this.PnMissionActions.Controls.Add(this.BtMissionGroups);
			this.PnMissionActions.Controls.Add(this.BtMissionAirbases);
			this.PnMissionActions.Controls.Add(this.BtMissionComs);
			this.PnMissionActions.Location = new System.Drawing.Point(38, 104);
			this.PnMissionActions.Name = "PnMissionActions";
			this.PnMissionActions.Size = new System.Drawing.Size(810, 38);
			this.PnMissionActions.TabIndex = 9;
			// 
			// BtMissionPackages
			// 
			this.BtMissionPackages.Location = new System.Drawing.Point(678, 0);
			this.BtMissionPackages.Name = "BtMissionPackages";
			this.BtMissionPackages.Size = new System.Drawing.Size(129, 35);
			this.BtMissionPackages.TabIndex = 10;
			this.BtMissionPackages.Text = "Packages";
			this.BtMissionPackages.UseVisualStyleBackColor = true;
			// 
			// BtMissionInformations
			// 
			this.BtMissionInformations.Location = new System.Drawing.Point(3, 0);
			this.BtMissionInformations.Name = "BtMissionInformations";
			this.BtMissionInformations.Size = new System.Drawing.Size(129, 35);
			this.BtMissionInformations.TabIndex = 3;
			this.BtMissionInformations.Text = "Informations";
			this.BtMissionInformations.UseVisualStyleBackColor = true;
			this.BtMissionInformations.Click += new System.EventHandler(this.BtMissionInformations_Click);
			// 
			// BtMissionMaps
			// 
			this.BtMissionMaps.Location = new System.Drawing.Point(408, 0);
			this.BtMissionMaps.Name = "BtMissionMaps";
			this.BtMissionMaps.Size = new System.Drawing.Size(129, 35);
			this.BtMissionMaps.TabIndex = 4;
			this.BtMissionMaps.Text = "Maps";
			this.BtMissionMaps.UseVisualStyleBackColor = true;
			this.BtMissionMaps.Click += new System.EventHandler(this.BtMissionMaps_Click);
			// 
			// BtMissionGroups
			// 
			this.BtMissionGroups.Location = new System.Drawing.Point(273, 0);
			this.BtMissionGroups.Name = "BtMissionGroups";
			this.BtMissionGroups.Size = new System.Drawing.Size(129, 35);
			this.BtMissionGroups.TabIndex = 5;
			this.BtMissionGroups.Text = "Groups";
			this.BtMissionGroups.UseVisualStyleBackColor = true;
			this.BtMissionGroups.Click += new System.EventHandler(this.BtMissionGroups_Click);
			// 
			// BtMissionAirbases
			// 
			this.BtMissionAirbases.Location = new System.Drawing.Point(138, 0);
			this.BtMissionAirbases.Name = "BtMissionAirbases";
			this.BtMissionAirbases.Size = new System.Drawing.Size(129, 35);
			this.BtMissionAirbases.TabIndex = 6;
			this.BtMissionAirbases.Text = "Airbases";
			this.BtMissionAirbases.UseVisualStyleBackColor = true;
			this.BtMissionAirbases.Click += new System.EventHandler(this.BtMissionAirdromes_Click);
			// 
			// BtMissionComs
			// 
			this.BtMissionComs.Location = new System.Drawing.Point(543, 0);
			this.BtMissionComs.Name = "BtMissionComs";
			this.BtMissionComs.Size = new System.Drawing.Size(129, 35);
			this.BtMissionComs.TabIndex = 7;
			this.BtMissionComs.Text = "Coms";
			this.BtMissionComs.UseVisualStyleBackColor = true;
			// 
			// LbMissionDirectory
			// 
			this.LbMissionDirectory.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbMissionDirectory.Location = new System.Drawing.Point(0, 68);
			this.LbMissionDirectory.Name = "LbMissionDirectory";
			this.LbMissionDirectory.Size = new System.Drawing.Size(885, 20);
			this.LbMissionDirectory.TabIndex = 8;
			this.LbMissionDirectory.TabStop = true;
			this.LbMissionDirectory.Text = "linkLabel1";
			this.LbMissionDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.LbMissionDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LbMissionDirectory_LinkClicked);
			// 
			// LbSortie
			// 
			this.LbSortie.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbSortie.Location = new System.Drawing.Point(0, 34);
			this.LbSortie.Name = "LbSortie";
			this.LbSortie.Size = new System.Drawing.Size(885, 34);
			this.LbSortie.TabIndex = 0;
			this.LbSortie.Text = "sortie";
			this.LbSortie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LbTheatre
			// 
			this.LbTheatre.AutoSize = true;
			this.LbTheatre.Location = new System.Drawing.Point(411, 20);
			this.LbTheatre.Name = "LbTheatre";
			this.LbTheatre.Size = new System.Drawing.Size(44, 13);
			this.LbTheatre.TabIndex = 1;
			this.LbTheatre.Text = "Theatre";
			this.LbTheatre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LbMissionTitle
			// 
			this.LbMissionTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbMissionTitle.Location = new System.Drawing.Point(0, 0);
			this.LbMissionTitle.Name = "LbMissionTitle";
			this.LbMissionTitle.Size = new System.Drawing.Size(885, 34);
			this.LbMissionTitle.TabIndex = 2;
			this.LbMissionTitle.Text = "Mission";
			this.LbMissionTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PnBriefing
			// 
			this.PnBriefing.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnBriefing.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnBriefing.Controls.Add(this.LbBriefingTitle);
			this.PnBriefing.Location = new System.Drawing.Point(0, 223);
			this.PnBriefing.Name = "PnBriefing";
			this.PnBriefing.Size = new System.Drawing.Size(887, 238);
			this.PnBriefing.TabIndex = 1;
			// 
			// LbBriefingTitle
			// 
			this.LbBriefingTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbBriefingTitle.Location = new System.Drawing.Point(0, 0);
			this.LbBriefingTitle.Name = "LbBriefingTitle";
			this.LbBriefingTitle.Size = new System.Drawing.Size(885, 34);
			this.LbBriefingTitle.TabIndex = 3;
			this.LbBriefingTitle.Text = "Briefing";
			this.LbBriefingTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PnBackground
			// 
			this.PnBackground.BackColor = System.Drawing.Color.Transparent;
			this.PnBackground.Controls.Add(this.PnMission);
			this.PnBackground.Controls.Add(this.PnBriefing);
			this.PnBackground.Location = new System.Drawing.Point(3, 3);
			this.PnBackground.Name = "PnBackground";
			this.PnBackground.Size = new System.Drawing.Size(887, 461);
			this.PnBackground.TabIndex = 2;
			// 
			// PnMissionTheatre
			// 
			this.PnMissionTheatre.Controls.Add(this.LbTheatre);
			this.PnMissionTheatre.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PnMissionTheatre.Location = new System.Drawing.Point(0, 148);
			this.PnMissionTheatre.Name = "PnMissionTheatre";
			this.PnMissionTheatre.Size = new System.Drawing.Size(885, 55);
			this.PnMissionTheatre.TabIndex = 10;
			// 
			// UcBriefop
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.PnBackground);
			this.Name = "UcBriefop";
			this.Size = new System.Drawing.Size(893, 467);
			this.PnMission.ResumeLayout(false);
			this.PnMission.PerformLayout();
			this.PnMissionActions.ResumeLayout(false);
			this.PnBriefing.ResumeLayout(false);
			this.PnBackground.ResumeLayout(false);
			this.PnMissionTheatre.ResumeLayout(false);
			this.PnMissionTheatre.PerformLayout();
			this.ResumeLayout(false);

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
		private System.Windows.Forms.Button BtMissionPackages;
		private System.Windows.Forms.Panel PnMissionTheatre;
	}
}
