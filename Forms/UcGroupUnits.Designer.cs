namespace DcsBriefop.Forms
{
	partial class UcGroupUnits
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
			this.DgvUnits = new Zuby.ADGV.AdvancedDataGridView();
			this.PnUnitDetail = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.DgvUnits)).BeginInit();
			this.SuspendLayout();
			// 
			// DgvUnits
			// 
			this.DgvUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvUnits.FilterAndSortEnabled = true;
			this.DgvUnits.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvUnits.Location = new System.Drawing.Point(0, 0);
			this.DgvUnits.Name = "DgvUnits";
			this.DgvUnits.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvUnits.Size = new System.Drawing.Size(377, 120);
			this.DgvUnits.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvUnits.TabIndex = 36;
			// 
			// PnUnitDetail
			// 
			this.PnUnitDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnUnitDetail.Location = new System.Drawing.Point(0, 120);
			this.PnUnitDetail.Name = "PnUnitDetail";
			this.PnUnitDetail.Size = new System.Drawing.Size(377, 287);
			this.PnUnitDetail.TabIndex = 37;
			// 
			// UcGroupUnits
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.PnUnitDetail);
			this.Controls.Add(this.DgvUnits);
			this.Name = "UcGroupUnits";
			this.Size = new System.Drawing.Size(377, 408);
			((System.ComponentModel.ISupportInitialize)(this.DgvUnits)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private Zuby.ADGV.AdvancedDataGridView DgvUnits;
		private System.Windows.Forms.Panel PnUnitDetail;
	}
}
