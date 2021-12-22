
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
			this.LbTheatre = new System.Windows.Forms.Label();
			this.TcMissionData.SuspendLayout();
			this.PnHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// TcMissionData
			// 
			this.TcMissionData.Controls.Add(this.tabPage1);
			this.TcMissionData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TcMissionData.Location = new System.Drawing.Point(0, 33);
			this.TcMissionData.Name = "TcMissionData";
			this.TcMissionData.SelectedIndex = 0;
			this.TcMissionData.Size = new System.Drawing.Size(834, 805);
			this.TcMissionData.TabIndex = 1;
			this.TcMissionData.SelectedIndexChanged += new System.EventHandler(this.TcMissionData_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(826, 779);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// PnHeader
			// 
			this.PnHeader.Controls.Add(this.LbTheatre);
			this.PnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.PnHeader.Location = new System.Drawing.Point(0, 0);
			this.PnHeader.Name = "PnHeader";
			this.PnHeader.Size = new System.Drawing.Size(834, 33);
			this.PnHeader.TabIndex = 0;
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
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl TcMissionData;
		private System.Windows.Forms.Panel PnHeader;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.Label LbTheatre;
	}
}
