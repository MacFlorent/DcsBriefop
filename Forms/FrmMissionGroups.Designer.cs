namespace DcsBriefop.Forms
{
	partial class FrmMissionGroups
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
			ScMain = new SplitContainer();
			DgvGroups = new Zuby.ADGV.AdvancedDataGridView();
			((System.ComponentModel.ISupportInitialize)ScMain).BeginInit();
			ScMain.Panel1.SuspendLayout();
			ScMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DgvGroups).BeginInit();
			SuspendLayout();
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
			ScMain.Panel1.Controls.Add(DgvGroups);
			ScMain.Size = new Size(1139, 914);
			ScMain.SplitterDistance = 316;
			ScMain.SplitterWidth = 5;
			ScMain.TabIndex = 0;
			// 
			// DgvGroups
			// 
			DgvGroups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvGroups.Dock = DockStyle.Fill;
			DgvGroups.FilterAndSortEnabled = true;
			DgvGroups.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvGroups.Location = new Point(0, 0);
			DgvGroups.Margin = new Padding(4, 3, 4, 3);
			DgvGroups.Name = "DgvGroups";
			DgvGroups.RightToLeft = RightToLeft.No;
			DgvGroups.Size = new Size(1137, 314);
			DgvGroups.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvGroups.TabIndex = 0;
			// 
			// FrmMissionGroups
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1139, 914);
			Controls.Add(ScMain);
			Margin = new Padding(4, 3, 4, 3);
			Name = "FrmMissionGroups";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Mission groups";
			FormClosed += FrmMissionGroups_FormClosed;
			Shown += FrmMissionGroups_Shown;
			ScMain.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)ScMain).EndInit();
			ScMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DgvGroups).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.SplitContainer ScMain;
		private Zuby.ADGV.AdvancedDataGridView DgvGroups;
	}
}