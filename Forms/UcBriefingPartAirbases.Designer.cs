namespace DcsBriefop.Forms
{
	partial class UcBriefingPartAirbases
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
			LbAirbases = new Label();
			TbHeader = new TextBox();
			LbColumns = new Label();
			LstColumns = new CheckedListBox();
			TlMain = new TableLayoutPanel();
			panel1 = new Panel();
			BtAdd = new Button();
			BtDown = new Button();
			BtRemove = new Button();
			BtUp = new Button();
			BtAddAll = new Button();
			BtRemoveAll = new Button();
			DgvAvailable = new Zuby.ADGV.AdvancedDataGridView();
			UcMultiSelection = new UcMultiSelectionGrids();
			TlMain.SuspendLayout();
			panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DgvAvailable).BeginInit();
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
			// LbAirbases
			// 
			LbAirbases.AutoSize = true;
			LbAirbases.Location = new Point(3, 33);
			LbAirbases.Name = "LbAirbases";
			LbAirbases.Size = new Size(51, 15);
			LbAirbases.TabIndex = 1;
			LbAirbases.Text = "Airbases";
			// 
			// TbHeader
			// 
			TbHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbHeader.Location = new Point(74, 4);
			TbHeader.Name = "TbHeader";
			TbHeader.Size = new Size(805, 23);
			TbHeader.TabIndex = 2;
			// 
			// LbColumns
			// 
			LbColumns.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			LbColumns.AutoSize = true;
			LbColumns.Location = new Point(3, 550);
			LbColumns.Name = "LbColumns";
			LbColumns.Size = new Size(55, 15);
			LbColumns.TabIndex = 5;
			LbColumns.Text = "Columns";
			// 
			// LstColumns
			// 
			LstColumns.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			LstColumns.FormattingEnabled = true;
			LstColumns.Location = new Point(3, 568);
			LstColumns.Name = "LstColumns";
			LstColumns.Size = new Size(876, 94);
			LstColumns.TabIndex = 6;
			// 
			// TlMain
			// 
			TlMain.ColumnCount = 3;
			TlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			TlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
			TlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			TlMain.Controls.Add(panel1, 1, 0);
			TlMain.Location = new Point(0, 0);
			TlMain.Name = "TlMain";
			TlMain.RowCount = 1;
			TlMain.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			TlMain.Size = new Size(200, 100);
			TlMain.TabIndex = 0;
			// 
			// panel1
			// 
			panel1.Controls.Add(BtAdd);
			panel1.Controls.Add(BtDown);
			panel1.Controls.Add(BtRemove);
			panel1.Controls.Add(BtUp);
			panel1.Controls.Add(BtAddAll);
			panel1.Controls.Add(BtRemoveAll);
			panel1.Dock = DockStyle.Fill;
			panel1.Location = new Point(73, 3);
			panel1.Name = "panel1";
			panel1.Size = new Size(54, 94);
			panel1.TabIndex = 0;
			// 
			// BtAdd
			// 
			BtAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtAdd.Location = new Point(3, 3);
			BtAdd.Name = "BtAdd";
			BtAdd.Size = new Size(0, 23);
			BtAdd.TabIndex = 2;
			BtAdd.Text = ">";
			BtAdd.UseVisualStyleBackColor = true;
			// 
			// BtDown
			// 
			BtDown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtDown.Location = new Point(3, 148);
			BtDown.Name = "BtDown";
			BtDown.Size = new Size(0, 23);
			BtDown.TabIndex = 7;
			BtDown.Text = "v";
			BtDown.UseVisualStyleBackColor = true;
			// 
			// BtRemove
			// 
			BtRemove.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtRemove.Location = new Point(3, 32);
			BtRemove.Name = "BtRemove";
			BtRemove.Size = new Size(0, 23);
			BtRemove.TabIndex = 3;
			BtRemove.Text = "<";
			BtRemove.UseVisualStyleBackColor = true;
			// 
			// BtUp
			// 
			BtUp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtUp.Location = new Point(3, 119);
			BtUp.Name = "BtUp";
			BtUp.Size = new Size(0, 23);
			BtUp.TabIndex = 6;
			BtUp.Text = "^";
			BtUp.UseVisualStyleBackColor = true;
			// 
			// BtAddAll
			// 
			BtAddAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtAddAll.Location = new Point(3, 61);
			BtAddAll.Name = "BtAddAll";
			BtAddAll.Size = new Size(0, 23);
			BtAddAll.TabIndex = 4;
			BtAddAll.Text = ">>";
			BtAddAll.UseVisualStyleBackColor = true;
			// 
			// BtRemoveAll
			// 
			BtRemoveAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtRemoveAll.Location = new Point(3, 90);
			BtRemoveAll.Name = "BtRemoveAll";
			BtRemoveAll.Size = new Size(0, 23);
			BtRemoveAll.TabIndex = 5;
			BtRemoveAll.Text = "<<";
			BtRemoveAll.UseVisualStyleBackColor = true;
			// 
			// DgvAvailable
			// 
			DgvAvailable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvAvailable.Dock = DockStyle.Fill;
			DgvAvailable.FilterAndSortEnabled = true;
			DgvAvailable.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvAvailable.Location = new Point(3, 3);
			DgvAvailable.Name = "DgvAvailable";
			DgvAvailable.RightToLeft = RightToLeft.No;
			DgvAvailable.RowTemplate.Height = 25;
			DgvAvailable.Size = new Size(64, 239);
			DgvAvailable.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvAvailable.TabIndex = 0;
			// 
			// UcMultiAirbases
			// 
			UcMultiSelection.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			UcMultiSelection.Location = new Point(3, 51);
			UcMultiSelection.Name = "UcMultiAirbases";
			UcMultiSelection.Size = new Size(876, 496);
			UcMultiSelection.TabIndex = 7;
			// 
			// UcBriefingPartAirbases
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(UcMultiSelection);
			Controls.Add(LstColumns);
			Controls.Add(LbColumns);
			Controls.Add(TbHeader);
			Controls.Add(LbAirbases);
			Controls.Add(LbHeader);
			Name = "UcBriefingPartAirbases";
			Size = new Size(882, 665);
			TlMain.ResumeLayout(false);
			panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DgvAvailable).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label LbHeader;
		private Label LbAirbases;
		private TextBox TbHeader;
		private Label LbColumns;
		private CheckedListBox LstColumns;
		private TableLayoutPanel TlMain;
		private Panel panel1;
		private Button BtAdd;
		private Button BtDown;
		private Button BtRemove;
		private Button BtUp;
		private Button BtAddAll;
		private Button BtRemoveAll;
		private Zuby.ADGV.AdvancedDataGridView DgvAvailable;
		private UcMultiSelectionGrids UcMultiSelection;
	}
}
