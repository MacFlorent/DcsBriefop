
namespace DcsBriefop.FormBop
{
	partial class UcBopTheatre
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
			this.LbAirdromes = new System.Windows.Forms.Label();
			this.LbTheatre = new System.Windows.Forms.Label();
			this.DgvAirdromes = new DcsBriefop.FgControls.FgDataGridView();
			((System.ComponentModel.ISupportInitialize)(this.DgvAirdromes)).BeginInit();
			this.SuspendLayout();
			// 
			// LbAirdromes
			// 
			this.LbAirdromes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbAirdromes.Location = new System.Drawing.Point(3, 98);
			this.LbAirdromes.Name = "LbAirdromes";
			this.LbAirdromes.Size = new System.Drawing.Size(659, 22);
			this.LbAirdromes.TabIndex = 2;
			this.LbAirdromes.Text = "Airdromes";
			this.LbAirdromes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LbTheatre
			// 
			this.LbTheatre.AutoSize = true;
			this.LbTheatre.Location = new System.Drawing.Point(26, 4);
			this.LbTheatre.Name = "LbTheatre";
			this.LbTheatre.Size = new System.Drawing.Size(56, 13);
			this.LbTheatre.TabIndex = 3;
			this.LbTheatre.Text = "LbTheatre";
			// 
			// DgvAirdromes
			// 
			this.DgvAirdromes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvAirdromes.DtSource = null;
			this.DgvAirdromes.FilterAndSortEnabled = true;
			this.DgvAirdromes.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvAirdromes.Location = new System.Drawing.Point(102, 167);
			this.DgvAirdromes.Name = "DgvAirdromes";
			this.DgvAirdromes.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvAirdromes.Size = new System.Drawing.Size(240, 150);
			this.DgvAirdromes.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvAirdromes.TabIndex = 4;
			// 
			// UcBriefopTheatre
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.DgvAirdromes);
			this.Controls.Add(this.LbTheatre);
			this.Controls.Add(this.LbAirdromes);
			this.Name = "UcBriefopTheatre";
			this.Size = new System.Drawing.Size(668, 590);
			((System.ComponentModel.ISupportInitialize)(this.DgvAirdromes)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label LbAirdromes;
		private System.Windows.Forms.Label LbTheatre;
		private FgControls.FgDataGridView DgvAirdromes;
	}
}
