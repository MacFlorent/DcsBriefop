namespace DcsBriefop.Forms
{
	partial class UcGroupRoutePoints
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
			this.PnRoutePointDetail = new System.Windows.Forms.Panel();
			this.DgvRoutePoints = new Zuby.ADGV.AdvancedDataGridView();
			((System.ComponentModel.ISupportInitialize)(this.DgvRoutePoints)).BeginInit();
			this.SuspendLayout();
			// 
			// PnRoutePointDetail
			// 
			this.PnRoutePointDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnRoutePointDetail.Location = new System.Drawing.Point(0, 250);
			this.PnRoutePointDetail.Name = "PnRoutePointDetail";
			this.PnRoutePointDetail.Size = new System.Drawing.Size(505, 274);
			this.PnRoutePointDetail.TabIndex = 39;
			// 
			// DgvRoutePoints
			// 
			this.DgvRoutePoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvRoutePoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvRoutePoints.FilterAndSortEnabled = true;
			this.DgvRoutePoints.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvRoutePoints.Location = new System.Drawing.Point(0, 0);
			this.DgvRoutePoints.Name = "DgvRoutePoints";
			this.DgvRoutePoints.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvRoutePoints.Size = new System.Drawing.Size(505, 249);
			this.DgvRoutePoints.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvRoutePoints.TabIndex = 38;
			// 
			// UcGroupRoutePoints
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.PnRoutePointDetail);
			this.Controls.Add(this.DgvRoutePoints);
			this.Name = "UcGroupRoutePoints";
			this.Size = new System.Drawing.Size(505, 525);
			((System.ComponentModel.ISupportInitialize)(this.DgvRoutePoints)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel PnRoutePointDetail;
		private Zuby.ADGV.AdvancedDataGridView DgvRoutePoints;
	}
}
