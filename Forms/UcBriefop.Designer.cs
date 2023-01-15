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
			this.BtComs = new System.Windows.Forms.Button();
			this.BtMissionAirdromes = new System.Windows.Forms.Button();
			this.BtMissionAssets = new System.Windows.Forms.Button();
			this.BtMissionMaps = new System.Windows.Forms.Button();
			this.BtMissionInformations = new System.Windows.Forms.Button();
			this.LbMissionTitle = new System.Windows.Forms.Label();
			this.LbTheatre = new System.Windows.Forms.Label();
			this.LbSortie = new System.Windows.Forms.Label();
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
			this.PnMission.Controls.Add(this.BtComs);
			this.PnMission.Controls.Add(this.BtMissionAirdromes);
			this.PnMission.Controls.Add(this.BtMissionAssets);
			this.PnMission.Controls.Add(this.BtMissionMaps);
			this.PnMission.Controls.Add(this.BtMissionInformations);
			this.PnMission.Controls.Add(this.LbMissionTitle);
			this.PnMission.Controls.Add(this.LbTheatre);
			this.PnMission.Controls.Add(this.LbSortie);
			this.PnMission.Location = new System.Drawing.Point(0, 0);
			this.PnMission.Name = "PnMission";
			this.PnMission.Size = new System.Drawing.Size(887, 205);
			this.PnMission.TabIndex = 0;
			// 
			// BtComs
			// 
			this.BtComs.Location = new System.Drawing.Point(556, 125);
			this.BtComs.Name = "BtComs";
			this.BtComs.Size = new System.Drawing.Size(75, 23);
			this.BtComs.TabIndex = 7;
			this.BtComs.Text = "Coms";
			this.BtComs.UseVisualStyleBackColor = true;
			// 
			// BtMissionAirdromes
			// 
			this.BtMissionAirdromes.Location = new System.Drawing.Point(304, 125);
			this.BtMissionAirdromes.Name = "BtMissionAirdromes";
			this.BtMissionAirdromes.Size = new System.Drawing.Size(75, 23);
			this.BtMissionAirdromes.TabIndex = 6;
			this.BtMissionAirdromes.Text = "Airdromes";
			this.BtMissionAirdromes.UseVisualStyleBackColor = true;
			// 
			// BtMissionAssets
			// 
			this.BtMissionAssets.Location = new System.Drawing.Point(385, 125);
			this.BtMissionAssets.Name = "BtMissionAssets";
			this.BtMissionAssets.Size = new System.Drawing.Size(75, 23);
			this.BtMissionAssets.TabIndex = 5;
			this.BtMissionAssets.Text = "Assets";
			this.BtMissionAssets.UseVisualStyleBackColor = true;
			this.BtMissionAssets.Click += new System.EventHandler(this.BtMissionAssets_Click);
			// 
			// BtMissionMaps
			// 
			this.BtMissionMaps.Location = new System.Drawing.Point(475, 125);
			this.BtMissionMaps.Name = "BtMissionMaps";
			this.BtMissionMaps.Size = new System.Drawing.Size(75, 23);
			this.BtMissionMaps.TabIndex = 4;
			this.BtMissionMaps.Text = "Maps";
			this.BtMissionMaps.UseVisualStyleBackColor = true;
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
			this.LbMissionTitle.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.LbMissionTitle.Location = new System.Drawing.Point(0, 169);
			this.LbMissionTitle.Name = "LbMissionTitle";
			this.LbMissionTitle.Size = new System.Drawing.Size(885, 34);
			this.LbMissionTitle.TabIndex = 2;
			this.LbMissionTitle.Text = "Mission";
			this.LbMissionTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LbTheatre
			// 
			this.LbTheatre.Location = new System.Drawing.Point(73, 34);
			this.LbTheatre.Name = "LbTheatre";
			this.LbTheatre.Size = new System.Drawing.Size(707, 34);
			this.LbTheatre.TabIndex = 1;
			this.LbTheatre.Text = "Theatre";
			this.LbTheatre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LbSortie
			// 
			this.LbSortie.Location = new System.Drawing.Point(73, 0);
			this.LbSortie.Name = "LbSortie";
			this.LbSortie.Size = new System.Drawing.Size(707, 34);
			this.LbSortie.TabIndex = 0;
			this.LbSortie.Text = "sortie";
			this.LbSortie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
		private System.Windows.Forms.Button BtMissionAssets;
		private System.Windows.Forms.Button BtMissionMaps;
		private System.Windows.Forms.Button BtMissionInformations;
		private System.Windows.Forms.Label LbMissionTitle;
		private System.Windows.Forms.Label LbBriefingTitle;
		private System.Windows.Forms.Button BtMissionAirdromes;
		private System.Windows.Forms.Button BtComs;
	}
}
