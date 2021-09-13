
namespace DcsBriefop.UcBriefing
{
	partial class UcBriefingPack
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
			this.TcMissionData = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.PnHeader = new System.Windows.Forms.Panel();
			this.LbSortie = new System.Windows.Forms.Label();
			this.LbTheatre = new System.Windows.Forms.Label();
			this.LbDisplayCoalition = new System.Windows.Forms.Label();
			this.CkDisplayNeutral = new System.Windows.Forms.CheckBox();
			this.CkDisplayBlue = new System.Windows.Forms.CheckBox();
			this.CkDisplayRed = new System.Windows.Forms.CheckBox();
			this.TcMissionData.SuspendLayout();
			this.PnHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// TcMissionData
			// 
			this.TcMissionData.Controls.Add(this.tabPage1);
			this.TcMissionData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TcMissionData.Location = new System.Drawing.Point(0, 107);
			this.TcMissionData.Name = "TcMissionData";
			this.TcMissionData.SelectedIndex = 0;
			this.TcMissionData.Size = new System.Drawing.Size(834, 731);
			this.TcMissionData.TabIndex = 1;
			this.TcMissionData.SelectedIndexChanged += new System.EventHandler(this.TcMissionData_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(826, 705);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// PnHeader
			// 
			this.PnHeader.Controls.Add(this.LbSortie);
			this.PnHeader.Controls.Add(this.LbTheatre);
			this.PnHeader.Controls.Add(this.LbDisplayCoalition);
			this.PnHeader.Controls.Add(this.CkDisplayNeutral);
			this.PnHeader.Controls.Add(this.CkDisplayBlue);
			this.PnHeader.Controls.Add(this.CkDisplayRed);
			this.PnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.PnHeader.Location = new System.Drawing.Point(0, 0);
			this.PnHeader.Name = "PnHeader";
			this.PnHeader.Size = new System.Drawing.Size(834, 107);
			this.PnHeader.TabIndex = 0;
			// 
			// LbSortie
			// 
			this.LbSortie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbSortie.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LbSortie.Location = new System.Drawing.Point(4, 29);
			this.LbSortie.Name = "LbSortie";
			this.LbSortie.Size = new System.Drawing.Size(826, 32);
			this.LbSortie.TabIndex = 1;
			this.LbSortie.Text = "Sortie";
			this.LbSortie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LbTheatre
			// 
			this.LbTheatre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbTheatre.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LbTheatre.Location = new System.Drawing.Point(4, 0);
			this.LbTheatre.Name = "LbTheatre";
			this.LbTheatre.Size = new System.Drawing.Size(826, 26);
			this.LbTheatre.TabIndex = 0;
			this.LbTheatre.Text = "Theater";
			this.LbTheatre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LbDisplayCoalition
			// 
			this.LbDisplayCoalition.AutoSize = true;
			this.LbDisplayCoalition.Location = new System.Drawing.Point(19, 78);
			this.LbDisplayCoalition.Name = "LbDisplayCoalition";
			this.LbDisplayCoalition.Size = new System.Drawing.Size(95, 13);
			this.LbDisplayCoalition.TabIndex = 2;
			this.LbDisplayCoalition.Text = "Coalitions included";
			// 
			// CkDisplayNeutral
			// 
			this.CkDisplayNeutral.AutoSize = true;
			this.CkDisplayNeutral.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CkDisplayNeutral.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.CkDisplayNeutral.Location = new System.Drawing.Point(232, 77);
			this.CkDisplayNeutral.Name = "CkDisplayNeutral";
			this.CkDisplayNeutral.Size = new System.Drawing.Size(67, 17);
			this.CkDisplayNeutral.TabIndex = 5;
			this.CkDisplayNeutral.Text = "Neutral";
			this.CkDisplayNeutral.UseVisualStyleBackColor = true;
			this.CkDisplayNeutral.CheckedChanged += new System.EventHandler(this.CkDisplayCoalition_CheckedChanged);
			// 
			// CkDisplayBlue
			// 
			this.CkDisplayBlue.AutoSize = true;
			this.CkDisplayBlue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CkDisplayBlue.ForeColor = System.Drawing.Color.Blue;
			this.CkDisplayBlue.Location = new System.Drawing.Point(175, 77);
			this.CkDisplayBlue.Name = "CkDisplayBlue";
			this.CkDisplayBlue.Size = new System.Drawing.Size(51, 17);
			this.CkDisplayBlue.TabIndex = 4;
			this.CkDisplayBlue.Text = "Blue";
			this.CkDisplayBlue.UseVisualStyleBackColor = true;
			this.CkDisplayBlue.CheckedChanged += new System.EventHandler(this.CkDisplayCoalition_CheckedChanged);
			// 
			// CkDisplayRed
			// 
			this.CkDisplayRed.AutoSize = true;
			this.CkDisplayRed.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.CkDisplayRed.ForeColor = System.Drawing.Color.Red;
			this.CkDisplayRed.Location = new System.Drawing.Point(120, 77);
			this.CkDisplayRed.Name = "CkDisplayRed";
			this.CkDisplayRed.Size = new System.Drawing.Size(49, 17);
			this.CkDisplayRed.TabIndex = 3;
			this.CkDisplayRed.Text = "Red";
			this.CkDisplayRed.UseVisualStyleBackColor = true;
			this.CkDisplayRed.CheckedChanged += new System.EventHandler(this.CkDisplayCoalition_CheckedChanged);
			// 
			// UcBriefingPack
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TcMissionData);
			this.Controls.Add(this.PnHeader);
			this.Name = "UcBriefingPack";
			this.Size = new System.Drawing.Size(834, 838);
			this.TcMissionData.ResumeLayout(false);
			this.PnHeader.ResumeLayout(false);
			this.PnHeader.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl TcMissionData;
		private System.Windows.Forms.Panel PnHeader;
		private System.Windows.Forms.CheckBox CkDisplayNeutral;
		private System.Windows.Forms.CheckBox CkDisplayBlue;
		private System.Windows.Forms.CheckBox CkDisplayRed;
		private System.Windows.Forms.Label LbSortie;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label LbDisplayCoalition;
		private System.Windows.Forms.Label LbTheatre;
	}
}
