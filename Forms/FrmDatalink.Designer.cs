namespace DcsBriefop.Forms
{
	partial class FrmDatalink
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
			DgvUnits = new Zuby.ADGV.AdvancedDataGridView();
			BtNormalize = new Button();
			((System.ComponentModel.ISupportInitialize)DgvUnits).BeginInit();
			SuspendLayout();
			// 
			// DgvUnits
			// 
			DgvUnits.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			DgvUnits.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvUnits.FilterAndSortEnabled = true;
			DgvUnits.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvUnits.Location = new Point(12, 12);
			DgvUnits.Name = "DgvUnits";
			DgvUnits.RightToLeft = RightToLeft.No;
			DgvUnits.RowTemplate.Height = 25;
			DgvUnits.Size = new Size(776, 404);
			DgvUnits.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvUnits.TabIndex = 1;
			// 
			// BtNormalize
			// 
			BtNormalize.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtNormalize.Location = new Point(713, 422);
			BtNormalize.Name = "BtNormalize";
			BtNormalize.Size = new Size(75, 23);
			BtNormalize.TabIndex = 43;
			BtNormalize.Text = "Normalize";
			BtNormalize.UseVisualStyleBackColor = true;
			BtNormalize.MouseDown += BtNormalize_MouseDown;
			// 
			// FrmDatalink
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(BtNormalize);
			Controls.Add(DgvUnits);
			Name = "FrmDatalink";
			ShowIcon = false;
			Text = "Datalink data";
			Shown += FrmDatalink_Shown;
			((System.ComponentModel.ISupportInitialize)DgvUnits).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Zuby.ADGV.AdvancedDataGridView DgvUnits;
		private Button BtNormalize;
	}
}