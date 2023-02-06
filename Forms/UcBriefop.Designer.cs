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
			this.LbMissionDirectory = new System.Windows.Forms.LinkLabel();
			this.LbSortie = new System.Windows.Forms.Label();
			this.LbTheatre = new System.Windows.Forms.Label();
			this.BtComs = new System.Windows.Forms.Button();
			this.BtMissionAirbases = new System.Windows.Forms.Button();
			this.BtMissionGroups = new System.Windows.Forms.Button();
			this.BtMissionMaps = new System.Windows.Forms.Button();
			this.BtMissionInformations = new System.Windows.Forms.Button();
			this.LbMissionTitle = new System.Windows.Forms.Label();
			this.PnBriefing = new System.Windows.Forms.Panel();
			this.LbBriefingTitle = new System.Windows.Forms.Label();
			this.PnBackground = new System.Windows.Forms.Panel();
			this.PnMission.SuspendLayout();
			this.PnBriefing.SuspendLayout();
			this.PnBackground.SuspendLayout();
			this.SuspendLayout();
			// 
			// PnMission
			// 
			this.PnMission.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnMission.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnMission.Controls.Add(this.LbMissionDirectory);
			this.PnMission.Controls.Add(this.LbSortie);
			this.PnMission.Controls.Add(this.LbTheatre);
			this.PnMission.Controls.Add(this.BtComs);
			this.PnMission.Controls.Add(this.BtMissionAirbases);
			this.PnMission.Controls.Add(this.BtMissionGroups);
			this.PnMission.Controls.Add(this.BtMissionMaps);
			this.PnMission.Controls.Add(this.BtMissionInformations);
			this.PnMission.Controls.Add(this.LbMissionTitle);
			this.PnMission.Location = new System.Drawing.Point(0, 0);
			this.PnMission.Name = "PnMission";
			this.PnMission.Size = new System.Drawing.Size(887, 205);
			this.PnMission.TabIndex = 0;
			// 
			// LbMissionDirectory
			// 
			this.LbMissionDirectory.AutoSize = true;
			this.LbMissionDirectory.Location = new System.Drawing.Point(414, 102);
			this.LbMissionDirectory.Name = "LbMissionDirectory";
			this.LbMissionDirectory.Size = new System.Drawing.Size(55, 13);
			this.LbMissionDirectory.TabIndex = 8;
			this.LbMissionDirectory.TabStop = true;
			this.LbMissionDirectory.Text = "linkLabel1";
			this.LbMissionDirectory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.LbMissionDirectory.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LbMissionDirectory_LinkClicked);
			// 
			// LbSortie
			// 
			this.LbSortie.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbSortie.Location = new System.Drawing.Point(0, 68);
			this.LbSortie.Name = "LbSortie";
			this.LbSortie.Size = new System.Drawing.Size(885, 34);
			this.LbSortie.TabIndex = 0;
			this.LbSortie.Text = "sortie";
			this.LbSortie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LbTheatre
			// 
			this.LbTheatre.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbTheatre.Location = new System.Drawing.Point(0, 34);
			this.LbTheatre.Name = "LbTheatre";
			this.LbTheatre.Size = new System.Drawing.Size(885, 34);
			this.LbTheatre.TabIndex = 1;
			this.LbTheatre.Text = "Theatre";
			this.LbTheatre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// BtComs
			// 
			this.BtComs.Location = new System.Drawing.Point(547, 125);
			this.BtComs.Name = "BtComs";
			this.BtComs.Size = new System.Drawing.Size(75, 23);
			this.BtComs.TabIndex = 7;
			this.BtComs.Text = "Coms";
			this.BtComs.UseVisualStyleBackColor = true;
			// 
			// BtMissionAirbases
			// 
			this.BtMissionAirbases.Location = new System.Drawing.Point(304, 125);
			this.BtMissionAirbases.Name = "BtMissionAirbases";
			this.BtMissionAirbases.Size = new System.Drawing.Size(75, 23);
			this.BtMissionAirbases.TabIndex = 6;
			this.BtMissionAirbases.Text = "Airbases";
			this.BtMissionAirbases.UseVisualStyleBackColor = true;
			this.BtMissionAirbases.Click += new System.EventHandler(this.BtMissionAirdromes_Click);
			// 
			// BtMissionGroups
			// 
			this.BtMissionGroups.Location = new System.Drawing.Point(385, 125);
			this.BtMissionGroups.Name = "BtMissionGroups";
			this.BtMissionGroups.Size = new System.Drawing.Size(75, 23);
			this.BtMissionGroups.TabIndex = 5;
			this.BtMissionGroups.Text = "Groups";
			this.BtMissionGroups.UseVisualStyleBackColor = true;
			this.BtMissionGroups.Click += new System.EventHandler(this.BtMissionGroups_Click);
			// 
			// BtMissionMaps
			// 
			this.BtMissionMaps.Location = new System.Drawing.Point(466, 125);
			this.BtMissionMaps.Name = "BtMissionMaps";
			this.BtMissionMaps.Size = new System.Drawing.Size(75, 23);
			this.BtMissionMaps.TabIndex = 4;
			this.BtMissionMaps.Text = "Maps";
			this.BtMissionMaps.UseVisualStyleBackColor = true;
			this.BtMissionMaps.Click += new System.EventHandler(this.BtMissionMaps_Click);
			// 
			// BtMissionInformations
			// 
			this.BtMissionInformations.Location = new System.Drawing.Point(223, 125);
			this.BtMissionInformations.Name = "BtMissionInformations";
			this.BtMissionInformations.Size = new System.Drawing.Size(75, 23);
			this.BtMissionInformations.TabIndex = 3;
			this.BtMissionInformations.Text = "Informations";
			this.BtMissionInformations.UseVisualStyleBackColor = true;
			this.BtMissionInformations.Click += new System.EventHandler(this.BtMissionInformations_Click);
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
			// UcBriefop
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.PnBackground);
			this.Name = "UcBriefop";
			this.Size = new System.Drawing.Size(893, 467);
			this.PnMission.ResumeLayout(false);
			this.PnMission.PerformLayout();
			this.PnBriefing.ResumeLayout(false);
			this.PnBackground.ResumeLayout(false);
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
		private System.Windows.Forms.Button BtComs;
		private System.Windows.Forms.LinkLabel LbMissionDirectory;
	}
}
