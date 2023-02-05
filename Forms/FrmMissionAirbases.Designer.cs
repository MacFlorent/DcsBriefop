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
			this.DgvAirbases = new Zuby.ADGV.AdvancedDataGridView();
			this.ScMain = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.DgvAirbases)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).BeginInit();
			this.ScMain.Panel1.SuspendLayout();
			this.ScMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// DgvAirbases
			// 
			this.DgvAirbases.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvAirbases.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DgvAirbases.FilterAndSortEnabled = true;
			this.DgvAirbases.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvAirbases.Location = new System.Drawing.Point(0, 0);
			this.DgvAirbases.Name = "DgvAirbases";
			this.DgvAirbases.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvAirbases.Size = new System.Drawing.Size(798, 153);
			this.DgvAirbases.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvAirbases.TabIndex = 0;
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
			this.ScMain.Panel1.Controls.Add(this.DgvAirbases);
			this.ScMain.Size = new System.Drawing.Size(800, 450);
			this.ScMain.SplitterDistance = 155;
			this.ScMain.TabIndex = 1;
			// 
			// FrmMissionAirbases
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.ScMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FrmMissionAirbases";
			this.ShowInTaskbar = false;
			this.Text = "Mission airbases";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMissionAirbases_FormClosed);
			((System.ComponentModel.ISupportInitialize)(this.DgvAirbases)).EndInit();
			this.ScMain.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).EndInit();
			this.ScMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private Zuby.ADGV.AdvancedDataGridView DgvAirbases;
		private System.Windows.Forms.SplitContainer ScMain;
	}
}