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
			DgvUnits = new Zuby.ADGV.AdvancedDataGridView();
			PnUnitDetail = new Panel();
			((System.ComponentModel.ISupportInitialize)DgvUnits).BeginInit();
			SuspendLayout();
			// 
			// DgvUnits
			// 
			DgvUnits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvUnits.Dock = DockStyle.Fill;
			DgvUnits.FilterAndSortEnabled = true;
			DgvUnits.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvUnits.Location = new Point(0, 0);
			DgvUnits.Margin = new Padding(4, 3, 4, 3);
			DgvUnits.Name = "DgvUnits";
			DgvUnits.RightToLeft = RightToLeft.No;
			DgvUnits.Size = new Size(801, 330);
			DgvUnits.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvUnits.TabIndex = 36;
			// 
			// PnUnitDetail
			// 
			PnUnitDetail.Dock = DockStyle.Bottom;
			PnUnitDetail.Location = new Point(0, 330);
			PnUnitDetail.Margin = new Padding(4, 3, 4, 3);
			PnUnitDetail.Name = "PnUnitDetail";
			PnUnitDetail.Size = new Size(801, 280);
			PnUnitDetail.TabIndex = 37;
			// 
			// UcGroupUnits
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(DgvUnits);
			Controls.Add(PnUnitDetail);
			Margin = new Padding(4, 3, 4, 3);
			Name = "UcGroupUnits";
			Size = new Size(801, 610);
			((System.ComponentModel.ISupportInitialize)DgvUnits).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Zuby.ADGV.AdvancedDataGridView DgvUnits;
		private System.Windows.Forms.Panel PnUnitDetail;
	}
}
