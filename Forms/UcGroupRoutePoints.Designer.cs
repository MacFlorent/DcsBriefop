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
			PnRoutePointDetail = new Panel();
			DgvRoutePoints = new Zuby.ADGV.AdvancedDataGridView();
			((System.ComponentModel.ISupportInitialize)DgvRoutePoints).BeginInit();
			SuspendLayout();
			// 
			// PnRoutePointDetail
			// 
			PnRoutePointDetail.Dock = DockStyle.Bottom;
			PnRoutePointDetail.Location = new Point(0, 317);
			PnRoutePointDetail.Margin = new Padding(4, 3, 4, 3);
			PnRoutePointDetail.Name = "PnRoutePointDetail";
			PnRoutePointDetail.Size = new Size(589, 289);
			PnRoutePointDetail.TabIndex = 39;
			// 
			// DgvRoutePoints
			// 
			DgvRoutePoints.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvRoutePoints.Dock = DockStyle.Fill;
			DgvRoutePoints.FilterAndSortEnabled = true;
			DgvRoutePoints.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvRoutePoints.Location = new Point(0, 0);
			DgvRoutePoints.Margin = new Padding(4, 3, 4, 3);
			DgvRoutePoints.Name = "DgvRoutePoints";
			DgvRoutePoints.RightToLeft = RightToLeft.No;
			DgvRoutePoints.Size = new Size(589, 317);
			DgvRoutePoints.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvRoutePoints.TabIndex = 38;
			// 
			// UcGroupRoutePoints
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(DgvRoutePoints);
			Controls.Add(PnRoutePointDetail);
			Margin = new Padding(4, 3, 4, 3);
			Name = "UcGroupRoutePoints";
			Size = new Size(589, 606);
			((System.ComponentModel.ISupportInitialize)DgvRoutePoints).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel PnRoutePointDetail;
		private Zuby.ADGV.AdvancedDataGridView DgvRoutePoints;
	}
}
