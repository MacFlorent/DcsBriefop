namespace DcsBriefop.Forms
{
	partial class FrmNormalize
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
			BtNormalizeDatalink = new Button();
			BtNormalizeCallsign = new Button();
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
			// BtNormalizeDatalink
			// 
			BtNormalizeDatalink.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtNormalizeDatalink.Location = new Point(650, 422);
			BtNormalizeDatalink.Name = "BtNormalizeDatalink";
			BtNormalizeDatalink.Size = new Size(138, 23);
			BtNormalizeDatalink.TabIndex = 43;
			BtNormalizeDatalink.Text = "Normalize datalink";
			BtNormalizeDatalink.UseVisualStyleBackColor = true;
			BtNormalizeDatalink.MouseDown += BtNormalize_MouseDown;
			// 
			// BtNormalizeCallsign
			// 
			BtNormalizeCallsign.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtNormalizeCallsign.Location = new Point(506, 422);
			BtNormalizeCallsign.Name = "BtNormalizeCallsign";
			BtNormalizeCallsign.Size = new Size(138, 23);
			BtNormalizeCallsign.TabIndex = 44;
			BtNormalizeCallsign.Text = "Normalize callsigns";
			BtNormalizeCallsign.UseVisualStyleBackColor = true;
			BtNormalizeCallsign.MouseDown += BtNormalizeCallsign_MouseDown;
			// 
			// FrmNormalize
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(BtNormalizeCallsign);
			Controls.Add(BtNormalizeDatalink);
			Controls.Add(DgvUnits);
			Name = "FrmNormalize";
			ShowIcon = false;
			Text = "Check and normalize mission data";
			Shown += FrmDatalink_Shown;
			((System.ComponentModel.ISupportInitialize)DgvUnits).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Zuby.ADGV.AdvancedDataGridView DgvUnits;
		private Button BtNormalizeDatalink;
		private Button BtNormalizeCallsign;
	}
}