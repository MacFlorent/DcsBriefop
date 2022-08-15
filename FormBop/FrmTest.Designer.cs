namespace DcsBriefop.FormBop
{
	partial class FrmTest
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
			this.components = new System.ComponentModel.Container();
			this.fgDataGridView1 = new DcsBriefop.FormBop.BopDataGridView();
			((System.ComponentModel.ISupportInitialize)(this.fgDataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// fgDataGridView1
			// 
			this.fgDataGridView1.AutoGenerateColumns = false;
			this.fgDataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.fgDataGridView1.DtSource = null;
			this.fgDataGridView1.FilterAndSortEnabled = true;
			this.fgDataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.fgDataGridView1.Location = new System.Drawing.Point(346, 255);
			this.fgDataGridView1.Name = "fgDataGridView1";
			this.fgDataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.fgDataGridView1.Size = new System.Drawing.Size(442, 183);
			this.fgDataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.fgDataGridView1.TabIndex = 0;
			// 
			// FrmTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 450);
			this.Controls.Add(this.fgDataGridView1);
			this.Name = "FrmTest";
			this.Text = "Form1";
			((System.ComponentModel.ISupportInitialize)(this.fgDataGridView1)).EndInit();
			this.ResumeLayout(false);

	}

	#endregion

	private FormBop.BopDataGridView fgDataGridView1;
}
}