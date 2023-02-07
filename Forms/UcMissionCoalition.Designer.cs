namespace DcsBriefop.Forms
{
	partial class UcMissionCoalition
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
			this.TbBullseye = new System.Windows.Forms.TextBox();
			this.LbBullseye = new System.Windows.Forms.Label();
			this.TbBullseyeDescription = new System.Windows.Forms.TextBox();
			this.LbBullseyeDescription = new System.Windows.Forms.Label();
			this.LbTask = new System.Windows.Forms.Label();
			this.TbTask = new System.Windows.Forms.TextBox();
			this.CkBullseyeWaypoint = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// TbBullseye
			// 
			this.TbBullseye.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbBullseye.Location = new System.Drawing.Point(109, 3);
			this.TbBullseye.Multiline = true;
			this.TbBullseye.Name = "TbBullseye";
			this.TbBullseye.ReadOnly = true;
			this.TbBullseye.Size = new System.Drawing.Size(960, 50);
			this.TbBullseye.TabIndex = 6;
			// 
			// LbBullseye
			// 
			this.LbBullseye.AutoSize = true;
			this.LbBullseye.Location = new System.Drawing.Point(3, 6);
			this.LbBullseye.Name = "LbBullseye";
			this.LbBullseye.Size = new System.Drawing.Size(46, 13);
			this.LbBullseye.TabIndex = 5;
			this.LbBullseye.Text = "Bullseye";
			// 
			// TbBullseyeDescription
			// 
			this.TbBullseyeDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbBullseyeDescription.Location = new System.Drawing.Point(109, 59);
			this.TbBullseyeDescription.Multiline = true;
			this.TbBullseyeDescription.Name = "TbBullseyeDescription";
			this.TbBullseyeDescription.Size = new System.Drawing.Size(960, 38);
			this.TbBullseyeDescription.TabIndex = 8;
			// 
			// LbBullseyeDescription
			// 
			this.LbBullseyeDescription.AutoSize = true;
			this.LbBullseyeDescription.Location = new System.Drawing.Point(3, 59);
			this.LbBullseyeDescription.Name = "LbBullseyeDescription";
			this.LbBullseyeDescription.Size = new System.Drawing.Size(100, 13);
			this.LbBullseyeDescription.TabIndex = 7;
			this.LbBullseyeDescription.Text = "Bullseye description";
			// 
			// LbTask
			// 
			this.LbTask.AutoSize = true;
			this.LbTask.Location = new System.Drawing.Point(3, 129);
			this.LbTask.Name = "LbTask";
			this.LbTask.Size = new System.Drawing.Size(31, 13);
			this.LbTask.TabIndex = 9;
			this.LbTask.Text = "Task";
			// 
			// TbTask
			// 
			this.TbTask.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbTask.Location = new System.Drawing.Point(109, 126);
			this.TbTask.Multiline = true;
			this.TbTask.Name = "TbTask";
			this.TbTask.Size = new System.Drawing.Size(960, 355);
			this.TbTask.TabIndex = 10;
			// 
			// CkBullseyeWaypoint
			// 
			this.CkBullseyeWaypoint.AutoSize = true;
			this.CkBullseyeWaypoint.Location = new System.Drawing.Point(109, 103);
			this.CkBullseyeWaypoint.Name = "CkBullseyeWaypoint";
			this.CkBullseyeWaypoint.Size = new System.Drawing.Size(248, 17);
			this.CkBullseyeWaypoint.TabIndex = 11;
			this.CkBullseyeWaypoint.Text = "Add Bulls as first waypoint for all playable flights";
			this.CkBullseyeWaypoint.UseVisualStyleBackColor = true;
			this.CkBullseyeWaypoint.CheckedChanged += new System.EventHandler(this.CkBullseyeWaypoint_CheckedChanged);
			// 
			// UcMissionCoalition
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CkBullseyeWaypoint);
			this.Controls.Add(this.TbTask);
			this.Controls.Add(this.LbTask);
			this.Controls.Add(this.TbBullseyeDescription);
			this.Controls.Add(this.LbBullseyeDescription);
			this.Controls.Add(this.TbBullseye);
			this.Controls.Add(this.LbBullseye);
			this.Name = "UcMissionCoalition";
			this.Size = new System.Drawing.Size(1072, 484);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox TbBullseye;
		private System.Windows.Forms.Label LbBullseye;
		private System.Windows.Forms.TextBox TbBullseyeDescription;
		private System.Windows.Forms.Label LbBullseyeDescription;
		private System.Windows.Forms.Label LbTask;
		private System.Windows.Forms.TextBox TbTask;
		private System.Windows.Forms.CheckBox CkBullseyeWaypoint;
	}
}
