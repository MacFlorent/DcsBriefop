
namespace DcsBriefop.UcBriefing
{
	partial class UcBriefingCoalition
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
			this.TbTask = new System.Windows.Forms.TextBox();
			this.LbTask = new System.Windows.Forms.Label();
			this.LbBullseye = new System.Windows.Forms.Label();
			this.TbBullseyeCoordinates = new System.Windows.Forms.TextBox();
			this.TbBullseyeDescription = new System.Windows.Forms.TextBox();
			this.BtComPresets = new System.Windows.Forms.Button();
			this.TcAssets = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.TcAssets.SuspendLayout();
			this.SuspendLayout();
			// 
			// TbTask
			// 
			this.TbTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbTask.Location = new System.Drawing.Point(6, 128);
			this.TbTask.Multiline = true;
			this.TbTask.Name = "TbTask";
			this.TbTask.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbTask.Size = new System.Drawing.Size(659, 83);
			this.TbTask.TabIndex = 3;
			// 
			// LbTask
			// 
			this.LbTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbTask.Location = new System.Drawing.Point(6, 106);
			this.LbTask.Name = "LbTask";
			this.LbTask.Size = new System.Drawing.Size(662, 20);
			this.LbTask.TabIndex = 2;
			this.LbTask.Text = "Ops";
			this.LbTask.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LbBullseye
			// 
			this.LbBullseye.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbBullseye.Location = new System.Drawing.Point(3, 0);
			this.LbBullseye.Name = "LbBullseye";
			this.LbBullseye.Size = new System.Drawing.Size(662, 24);
			this.LbBullseye.TabIndex = 5;
			this.LbBullseye.Text = "Bullseye";
			this.LbBullseye.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbBullseyeCoordinates
			// 
			this.TbBullseyeCoordinates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbBullseyeCoordinates.Location = new System.Drawing.Point(3, 27);
			this.TbBullseyeCoordinates.Multiline = true;
			this.TbBullseyeCoordinates.Name = "TbBullseyeCoordinates";
			this.TbBullseyeCoordinates.ReadOnly = true;
			this.TbBullseyeCoordinates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbBullseyeCoordinates.Size = new System.Drawing.Size(659, 56);
			this.TbBullseyeCoordinates.TabIndex = 7;
			this.TbBullseyeCoordinates.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// TbBullseyeDescription
			// 
			this.TbBullseyeDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbBullseyeDescription.Location = new System.Drawing.Point(3, 84);
			this.TbBullseyeDescription.Name = "TbBullseyeDescription";
			this.TbBullseyeDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbBullseyeDescription.Size = new System.Drawing.Size(659, 20);
			this.TbBullseyeDescription.TabIndex = 8;
			this.TbBullseyeDescription.Validated += new System.EventHandler(this.TbBullseyeDescription_Validated);
			// 
			// BtComPresets
			// 
			this.BtComPresets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BtComPresets.Location = new System.Drawing.Point(3, 751);
			this.BtComPresets.Name = "BtComPresets";
			this.BtComPresets.Size = new System.Drawing.Size(118, 23);
			this.BtComPresets.TabIndex = 9;
			this.BtComPresets.Text = "Communications";
			this.BtComPresets.UseVisualStyleBackColor = true;
			this.BtComPresets.Click += new System.EventHandler(this.BtComPresets_Click);
			// 
			// TcAssets
			// 
			this.TcAssets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TcAssets.Controls.Add(this.tabPage1);
			this.TcAssets.Location = new System.Drawing.Point(8, 217);
			this.TcAssets.Name = "TcAssets";
			this.TcAssets.SelectedIndex = 0;
			this.TcAssets.Size = new System.Drawing.Size(654, 522);
			this.TcAssets.TabIndex = 10;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(646, 496);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// UcBriefingCoalition
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TcAssets);
			this.Controls.Add(this.BtComPresets);
			this.Controls.Add(this.TbBullseyeDescription);
			this.Controls.Add(this.TbBullseyeCoordinates);
			this.Controls.Add(this.LbBullseye);
			this.Controls.Add(this.TbTask);
			this.Controls.Add(this.LbTask);
			this.Name = "UcBriefingCoalition";
			this.Size = new System.Drawing.Size(668, 777);
			this.TcAssets.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox TbTask;
		private System.Windows.Forms.Label LbTask;
		private System.Windows.Forms.Label LbBullseye;
		private System.Windows.Forms.TextBox TbBullseyeCoordinates;
		private System.Windows.Forms.TextBox TbBullseyeDescription;
		private System.Windows.Forms.Button BtComPresets;
		private System.Windows.Forms.TabControl TcAssets;
		private System.Windows.Forms.TabPage tabPage1;
	}
}
