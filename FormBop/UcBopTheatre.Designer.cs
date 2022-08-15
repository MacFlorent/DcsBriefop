
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
			this.components = new System.ComponentModel.Container();
			this.LbAirdromes = new System.Windows.Forms.Label();
			this.LbTheatre = new System.Windows.Forms.Label();
			this.DgvAirdromes = new DcsBriefop.FormBop.BopDataGridView();
			((System.ComponentModel.ISupportInitialize)(this.DgvAirdromes)).BeginInit();
			this.SuspendLayout();
			// 
			// LbAirdromes
			// 
			this.LbAirdromes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbAirdromes.Location = new System.Drawing.Point(3, 29);
			this.LbAirdromes.Name = "LbAirdromes";
			this.LbAirdromes.Size = new System.Drawing.Size(659, 22);
			this.LbAirdromes.TabIndex = 2;
			this.LbAirdromes.Text = "Airdromes";
			this.LbAirdromes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LbTheatre
			// 
			this.LbTheatre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbTheatre.Location = new System.Drawing.Point(0, 4);
			this.LbTheatre.Name = "LbTheatre";
			this.LbTheatre.Size = new System.Drawing.Size(668, 25);
			this.LbTheatre.TabIndex = 3;
			this.LbTheatre.Text = "Theatre";
			this.LbTheatre.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// DgvAirdromes
			// 
			this.DgvAirdromes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvAirdromes.AutoGenerateColumns = false;
			this.DgvAirdromes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvAirdromes.DtSource = null;
			this.DgvAirdromes.FilterAndSortEnabled = true;
			this.DgvAirdromes.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvAirdromes.Location = new System.Drawing.Point(3, 54);
			this.DgvAirdromes.Name = "DgvAirdromes";
			this.DgvAirdromes.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvAirdromes.Size = new System.Drawing.Size(659, 533);
			this.DgvAirdromes.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvAirdromes.TabIndex = 4;
			// 
			// UcBopTheatre
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.DgvAirdromes);
			this.Controls.Add(this.LbTheatre);
			this.Controls.Add(this.LbAirdromes);
			this.Name = "UcBopTheatre";
			this.Size = new System.Drawing.Size(668, 590);
			((System.ComponentModel.ISupportInitialize)(this.DgvAirdromes)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label LbAirdromes;
		private System.Windows.Forms.Label LbTheatre;
		private FormBop.BopDataGridView DgvAirdromes;
	}
}
