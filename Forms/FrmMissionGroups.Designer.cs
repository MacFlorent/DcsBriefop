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
			this.ScMain = new System.Windows.Forms.SplitContainer();
			this.DgvAssets = new Zuby.ADGV.AdvancedDataGridView();
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).BeginInit();
			this.ScMain.Panel1.SuspendLayout();
			this.ScMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgvAssets)).BeginInit();
			this.SuspendLayout();
			// 
			// ScMain
			// 
			this.ScMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ScMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ScMain.Location = new System.Drawing.Point(0, 0);
			this.ScMain.Name = "ScMain";
			this.ScMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// ScMain.Panel1
			// 
			this.ScMain.Panel1.Controls.Add(this.DgvAssets);
			this.ScMain.Size = new System.Drawing.Size(1530, 792);
			this.ScMain.SplitterDistance = 274;
			this.ScMain.TabIndex = 0;
			// 
			// DgvAssets
			// 
			this.DgvAssets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvAssets.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DgvAssets.FilterAndSortEnabled = true;
			this.DgvAssets.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvAssets.Location = new System.Drawing.Point(0, 0);
			this.DgvAssets.Name = "DgvAssets";
			this.DgvAssets.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvAssets.Size = new System.Drawing.Size(1528, 272);
			this.DgvAssets.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvAssets.TabIndex = 0;
			// 
			// FrmMissionGroups
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1530, 792);
			this.Controls.Add(this.ScMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FrmMissionGroups";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FrmMissionAssets";
			this.ScMain.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).EndInit();
			this.ScMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DgvAssets)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer ScMain;
		private Zuby.ADGV.AdvancedDataGridView DgvAssets;
	}
}