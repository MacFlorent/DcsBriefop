namespace DcsBriefop.Forms
{
	partial class UcBriefingPartGroups
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
			LbHeader = new Label();
			LbGroups = new Label();
			TbHeader = new TextBox();
			DgvGroups = new Zuby.ADGV.AdvancedDataGridView();
			LbColumns = new Label();
			LstColumns = new CheckedListBox();
			((System.ComponentModel.ISupportInitialize)DgvGroups).BeginInit();
			SuspendLayout();
			// 
			// LbHeader
			// 
			LbHeader.AutoSize = true;
			LbHeader.Location = new Point(3, 7);
			LbHeader.Name = "LbHeader";
			LbHeader.Size = new Size(45, 15);
			LbHeader.TabIndex = 0;
			LbHeader.Text = "Header";
			// 
			// LbGroups
			// 
			LbGroups.AutoSize = true;
			LbGroups.Location = new Point(3, 33);
			LbGroups.Name = "LbGroups";
			LbGroups.Size = new Size(45, 15);
			LbGroups.TabIndex = 1;
			LbGroups.Text = "Groups";
			// 
			// TbHeader
			// 
			TbHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbHeader.Location = new Point(74, 4);
			TbHeader.Name = "TbHeader";
			TbHeader.Size = new Size(244, 23);
			TbHeader.TabIndex = 2;
			// 
			// DgvGroups
			// 
			DgvGroups.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			DgvGroups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvGroups.FilterAndSortEnabled = true;
			DgvGroups.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvGroups.Location = new Point(3, 51);
			DgvGroups.Name = "DgvGroups";
			DgvGroups.RightToLeft = RightToLeft.No;
			DgvGroups.RowTemplate.Height = 25;
			DgvGroups.Size = new Size(315, 118);
			DgvGroups.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvGroups.TabIndex = 3;
			// 
			// LbColumns
			// 
			LbColumns.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			LbColumns.AutoSize = true;
			LbColumns.Location = new Point(3, 172);
			LbColumns.Name = "LbColumns";
			LbColumns.Size = new Size(55, 15);
			LbColumns.TabIndex = 5;
			LbColumns.Text = "Columns";
			// 
			// LstColumns
			// 
			LstColumns.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			LstColumns.FormattingEnabled = true;
			LstColumns.Location = new Point(3, 190);
			LstColumns.Name = "LstColumns";
			LstColumns.Size = new Size(315, 94);
			LstColumns.TabIndex = 6;
			// 
			// UcBriefingPartGroups
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(LstColumns);
			Controls.Add(LbColumns);
			Controls.Add(DgvGroups);
			Controls.Add(TbHeader);
			Controls.Add(LbGroups);
			Controls.Add(LbHeader);
			Name = "UcBriefingPartGroups";
			Size = new Size(321, 287);
			((System.ComponentModel.ISupportInitialize)DgvGroups).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label LbHeader;
		private Label LbGroups;
		private TextBox TbHeader;
		private Zuby.ADGV.AdvancedDataGridView DgvGroups;
		private Label LbColumns;
		private CheckedListBox LstColumns;
	}
}
