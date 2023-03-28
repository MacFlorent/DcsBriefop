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
			TbBullseye = new TextBox();
			LbBullseye = new Label();
			TbBullseyeDescription = new TextBox();
			LbBullseyeDescription = new Label();
			LbTask = new Label();
			TbTask = new TextBox();
			LbBullseyeWaypoint = new Label();
			CbBullseyeWaypoint = new ComboBox();
			SuspendLayout();
			// 
			// TbBullseye
			// 
			TbBullseye.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbBullseye.Location = new Point(127, 3);
			TbBullseye.Margin = new Padding(4, 3, 4, 3);
			TbBullseye.Multiline = true;
			TbBullseye.Name = "TbBullseye";
			TbBullseye.ReadOnly = true;
			TbBullseye.Size = new Size(995, 57);
			TbBullseye.TabIndex = 6;
			// 
			// LbBullseye
			// 
			LbBullseye.AutoSize = true;
			LbBullseye.Location = new Point(4, 7);
			LbBullseye.Margin = new Padding(4, 0, 4, 0);
			LbBullseye.Name = "LbBullseye";
			LbBullseye.Size = new Size(50, 15);
			LbBullseye.TabIndex = 5;
			LbBullseye.Text = "Bullseye";
			// 
			// TbBullseyeDescription
			// 
			TbBullseyeDescription.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbBullseyeDescription.Location = new Point(127, 68);
			TbBullseyeDescription.Margin = new Padding(4, 3, 4, 3);
			TbBullseyeDescription.Multiline = true;
			TbBullseyeDescription.Name = "TbBullseyeDescription";
			TbBullseyeDescription.Size = new Size(995, 43);
			TbBullseyeDescription.TabIndex = 8;
			// 
			// LbBullseyeDescription
			// 
			LbBullseyeDescription.AutoSize = true;
			LbBullseyeDescription.Location = new Point(4, 68);
			LbBullseyeDescription.Margin = new Padding(4, 0, 4, 0);
			LbBullseyeDescription.Name = "LbBullseyeDescription";
			LbBullseyeDescription.Size = new Size(112, 15);
			LbBullseyeDescription.TabIndex = 7;
			LbBullseyeDescription.Text = "Bullseye description";
			// 
			// LbTask
			// 
			LbTask.AutoSize = true;
			LbTask.Location = new Point(4, 149);
			LbTask.Margin = new Padding(4, 0, 4, 0);
			LbTask.Name = "LbTask";
			LbTask.Size = new Size(29, 15);
			LbTask.TabIndex = 9;
			LbTask.Text = "Task";
			// 
			// TbTask
			// 
			TbTask.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			TbTask.Location = new Point(127, 145);
			TbTask.Margin = new Padding(4, 3, 4, 3);
			TbTask.Multiline = true;
			TbTask.Name = "TbTask";
			TbTask.Size = new Size(995, 382);
			TbTask.TabIndex = 10;
			// 
			// LbBullseyeWaypoint
			// 
			LbBullseyeWaypoint.AutoSize = true;
			LbBullseyeWaypoint.Location = new Point(1, 120);
			LbBullseyeWaypoint.Margin = new Padding(4, 0, 4, 0);
			LbBullseyeWaypoint.Name = "LbBullseyeWaypoint";
			LbBullseyeWaypoint.Size = new Size(102, 15);
			LbBullseyeWaypoint.TabIndex = 30;
			LbBullseyeWaypoint.Text = "Bullseye waypoint";
			// 
			// CbBullseyeWaypoint
			// 
			CbBullseyeWaypoint.FormattingEnabled = true;
			CbBullseyeWaypoint.Location = new Point(127, 117);
			CbBullseyeWaypoint.Margin = new Padding(4, 3, 4, 3);
			CbBullseyeWaypoint.Name = "CbBullseyeWaypoint";
			CbBullseyeWaypoint.Size = new Size(310, 23);
			CbBullseyeWaypoint.TabIndex = 29;
			CbBullseyeWaypoint.SelectedValueChanged += CbBullseyeWaypoint_SelectedValueChanged;
			// 
			// UcMissionCoalition
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(LbBullseyeWaypoint);
			Controls.Add(CbBullseyeWaypoint);
			Controls.Add(TbTask);
			Controls.Add(LbTask);
			Controls.Add(TbBullseyeDescription);
			Controls.Add(LbBullseyeDescription);
			Controls.Add(TbBullseye);
			Controls.Add(LbBullseye);
			Margin = new Padding(4, 3, 4, 3);
			Name = "UcMissionCoalition";
			Size = new Size(1127, 531);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TextBox TbBullseye;
		private System.Windows.Forms.Label LbBullseye;
		private System.Windows.Forms.TextBox TbBullseyeDescription;
		private System.Windows.Forms.Label LbBullseyeDescription;
		private System.Windows.Forms.Label LbTask;
		private System.Windows.Forms.TextBox TbTask;
		private Label LbBullseyeWaypoint;
		private ComboBox CbBullseyeWaypoint;
	}
}
