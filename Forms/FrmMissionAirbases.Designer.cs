namespace DcsBriefop.Forms
{
	partial class FrmMissionAirbases
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			DgvAirbases = new BrightIdeasSoftware.FastObjectListView();
			ScMain = new SplitContainer();
			((System.ComponentModel.ISupportInitialize)ScMain).BeginInit();
			ScMain.Panel1.SuspendLayout();
			ScMain.SuspendLayout();
			SuspendLayout();
			// 
			// DgvAirbases
			// 
			DgvAirbases.Dock = DockStyle.Fill;
			DgvAirbases.Location = new Point(0, 0);
			DgvAirbases.Margin = new Padding(4, 3, 4, 3);
			DgvAirbases.Name = "DgvAirbases";
			DgvAirbases.Size = new Size(931, 176);
			DgvAirbases.TabIndex = 0;
			DgvAirbases.View = System.Windows.Forms.View.Details;
			// 
			// ScMain
			// 
			ScMain.BorderStyle = BorderStyle.FixedSingle;
			ScMain.Dock = DockStyle.Fill;
			ScMain.Location = new Point(0, 0);
			ScMain.Margin = new Padding(4, 3, 4, 3);
			ScMain.Name = "ScMain";
			ScMain.Orientation = Orientation.Horizontal;
			// 
			// ScMain.Panel1
			// 
			ScMain.Panel1.Controls.Add(DgvAirbases);
			ScMain.Size = new Size(933, 519);
			ScMain.SplitterDistance = 178;
			ScMain.SplitterWidth = 5;
			ScMain.TabIndex = 1;
			// 
			// FrmMissionAirbases
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(933, 519);
			Controls.Add(ScMain);
			Margin = new Padding(4, 3, 4, 3);
			Name = "FrmMissionAirbases";
			ShowIcon = false;
			ShowInTaskbar = false;
			Text = "Mission airbases";
			FormClosed += FrmMissionAirbases_FormClosed;
			Shown += FrmMissionAirbases_Shown;
			ScMain.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ScMain).EndInit();
			ScMain.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private BrightIdeasSoftware.FastObjectListView DgvAirbases;
		private System.Windows.Forms.SplitContainer ScMain;
	}
}