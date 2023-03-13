namespace DcsBriefop.Forms
{
	partial class UcMultiSelectionGrids
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
			DgvAvailable = new Zuby.ADGV.AdvancedDataGridView();
			DgvSelected = new Zuby.ADGV.AdvancedDataGridView();
			BtAdd = new Button();
			BtRemove = new Button();
			BtRemoveAll = new Button();
			BtAddAll = new Button();
			BtDown = new Button();
			BtUp = new Button();
			TlMain = new TableLayoutPanel();
			panel1 = new Panel();
			((System.ComponentModel.ISupportInitialize)DgvAvailable).BeginInit();
			((System.ComponentModel.ISupportInitialize)DgvSelected).BeginInit();
			TlMain.SuspendLayout();
			panel1.SuspendLayout();
			SuspendLayout();
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
			DgvAvailable.Size = new Size(288, 282);
			DgvAvailable.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvAvailable.TabIndex = 0;
			// 
			// DgvSelected
			// 
			DgvSelected.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvSelected.Dock = DockStyle.Fill;
			DgvSelected.FilterAndSortEnabled = false;
			DgvSelected.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvSelected.Location = new Point(357, 3);
			DgvSelected.Name = "DgvSelected";
			DgvSelected.RightToLeft = RightToLeft.No;
			DgvSelected.RowTemplate.Height = 25;
			DgvSelected.Size = new Size(289, 282);
			DgvSelected.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvSelected.TabIndex = 1;
			// 
			// BtAdd
			// 
			BtAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtAdd.Location = new Point(3, 3);
			BtAdd.Name = "BtAdd";
			BtAdd.Size = new Size(48, 23);
			BtAdd.TabIndex = 2;
			BtAdd.Text = ">";
			BtAdd.UseVisualStyleBackColor = true;
			// 
			// BtRemove
			// 
			BtRemove.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtRemove.Location = new Point(3, 32);
			BtRemove.Name = "BtRemove";
			BtRemove.Size = new Size(48, 23);
			BtRemove.TabIndex = 3;
			BtRemove.Text = "<";
			BtRemove.UseVisualStyleBackColor = true;
			// 
			// BtRemoveAll
			// 
			BtRemoveAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtRemoveAll.Location = new Point(3, 90);
			BtRemoveAll.Name = "BtRemoveAll";
			BtRemoveAll.Size = new Size(48, 23);
			BtRemoveAll.TabIndex = 5;
			BtRemoveAll.Text = "<<";
			BtRemoveAll.UseVisualStyleBackColor = true;
			// 
			// BtAddAll
			// 
			BtAddAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtAddAll.Location = new Point(3, 61);
			BtAddAll.Name = "BtAddAll";
			BtAddAll.Size = new Size(48, 23);
			BtAddAll.TabIndex = 4;
			BtAddAll.Text = ">>";
			BtAddAll.UseVisualStyleBackColor = true;
			// 
			// BtDown
			// 
			BtDown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtDown.Location = new Point(3, 148);
			BtDown.Name = "BtDown";
			BtDown.Size = new Size(48, 23);
			BtDown.TabIndex = 7;
			BtDown.Text = "v";
			BtDown.UseVisualStyleBackColor = true;
			// 
			// BtUp
			// 
			BtUp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtUp.Location = new Point(3, 119);
			BtUp.Name = "BtUp";
			BtUp.Size = new Size(48, 23);
			BtUp.TabIndex = 6;
			BtUp.Text = "^";
			BtUp.UseVisualStyleBackColor = true;
			// 
			// TlMain
			// 
			TlMain.ColumnCount = 3;
			TlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			TlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 60F));
			TlMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
			TlMain.Controls.Add(panel1, 1, 0);
			TlMain.Controls.Add(DgvAvailable, 0, 0);
			TlMain.Controls.Add(DgvSelected, 2, 0);
			TlMain.Dock = DockStyle.Fill;
			TlMain.Location = new Point(0, 0);
			TlMain.Name = "TlMain";
			TlMain.RowCount = 1;
			TlMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			TlMain.Size = new Size(649, 288);
			TlMain.TabIndex = 8;
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
			panel1.Location = new Point(297, 3);
			panel1.Name = "panel1";
			panel1.Size = new Size(54, 282);
			panel1.TabIndex = 0;
			// 
			// UcMultiSelectionGrids
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(TlMain);
			Name = "UcMultiSelectionGrids";
			Size = new Size(649, 288);
			((System.ComponentModel.ISupportInitialize)DgvAvailable).EndInit();
			((System.ComponentModel.ISupportInitialize)DgvSelected).EndInit();
			TlMain.ResumeLayout(false);
			panel1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		public Zuby.ADGV.AdvancedDataGridView DgvAvailable;
		public Zuby.ADGV.AdvancedDataGridView DgvSelected;
		public Button BtAdd;
		public Button BtRemove;
		public Button BtRemoveAll;
		public Button BtAddAll;
		public Button BtDown;
		public Button BtUp;
		private TableLayoutPanel TlMain;
		private Panel panel1;
	}
}
