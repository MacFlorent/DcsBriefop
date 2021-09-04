
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
			this.CkDisplayNeutral = new System.Windows.Forms.CheckBox();
			this.CkDisplayBlue = new System.Windows.Forms.CheckBox();
			this.CkDisplayRed = new System.Windows.Forms.CheckBox();
			this.LbSortie = new System.Windows.Forms.Label();
			this.TcMissionData.SuspendLayout();
			this.PnHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// TcMissionData
			// 
			this.TcMissionData.Controls.Add(this.tabPage1);
			this.TcMissionData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TcMissionData.Location = new System.Drawing.Point(0, 52);
			this.TcMissionData.Name = "TcMissionData";
			this.TcMissionData.SelectedIndex = 0;
			this.TcMissionData.Size = new System.Drawing.Size(884, 786);
			this.TcMissionData.TabIndex = 1;
			this.TcMissionData.SelectedIndexChanged += new System.EventHandler(this.TcMissionData_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(876, 760);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// PnHeader
			// 
			this.PnHeader.Controls.Add(this.CkDisplayNeutral);
			this.PnHeader.Controls.Add(this.CkDisplayBlue);
			this.PnHeader.Controls.Add(this.CkDisplayRed);
			this.PnHeader.Controls.Add(this.LbSortie);
			this.PnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.PnHeader.Location = new System.Drawing.Point(0, 0);
			this.PnHeader.Name = "PnHeader";
			this.PnHeader.Size = new System.Drawing.Size(884, 52);
			this.PnHeader.TabIndex = 2;
			// 
			// CkDisplayNeutral
			// 
			this.CkDisplayNeutral.AutoSize = true;
			this.CkDisplayNeutral.Location = new System.Drawing.Point(182, 30);
			this.CkDisplayNeutral.Name = "CkDisplayNeutral";
			this.CkDisplayNeutral.Size = new System.Drawing.Size(60, 17);
			this.CkDisplayNeutral.TabIndex = 3;
			this.CkDisplayNeutral.Text = "Neutral";
			this.CkDisplayNeutral.UseVisualStyleBackColor = true;
			this.CkDisplayNeutral.CheckedChanged += new System.EventHandler(this.CkDisplayCoalition_CheckedChanged);
			// 
			// CkDisplayBlue
			// 
			this.CkDisplayBlue.AutoSize = true;
			this.CkDisplayBlue.Location = new System.Drawing.Point(130, 30);
			this.CkDisplayBlue.Name = "CkDisplayBlue";
			this.CkDisplayBlue.Size = new System.Drawing.Size(47, 17);
			this.CkDisplayBlue.TabIndex = 2;
			this.CkDisplayBlue.Text = "Blue";
			this.CkDisplayBlue.UseVisualStyleBackColor = true;
			this.CkDisplayBlue.CheckedChanged += new System.EventHandler(this.CkDisplayCoalition_CheckedChanged);
			// 
			// CkDisplayRed
			// 
			this.CkDisplayRed.AutoSize = true;
			this.CkDisplayRed.Location = new System.Drawing.Point(78, 31);
			this.CkDisplayRed.Name = "CkDisplayRed";
			this.CkDisplayRed.Size = new System.Drawing.Size(46, 17);
			this.CkDisplayRed.TabIndex = 1;
			this.CkDisplayRed.Text = "Red";
			this.CkDisplayRed.UseVisualStyleBackColor = true;
			this.CkDisplayRed.CheckedChanged += new System.EventHandler(this.CkDisplayCoalition_CheckedChanged);
			// 
			// LbSortie
			// 
			this.LbSortie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbSortie.Location = new System.Drawing.Point(4, 4);
			this.LbSortie.Name = "LbSortie";
			this.LbSortie.Size = new System.Drawing.Size(876, 23);
			this.LbSortie.TabIndex = 0;
			this.LbSortie.Text = "Sortie";
			this.LbSortie.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UcBriefingPack
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TcMissionData);
			this.Controls.Add(this.PnHeader);
			this.Name = "UcBriefingPack";
			this.Size = new System.Drawing.Size(884, 838);
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
	}
}
