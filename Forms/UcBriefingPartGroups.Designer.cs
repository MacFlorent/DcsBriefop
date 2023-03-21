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
			LbColumns = new Label();
			LstColumns = new CheckedListBox();
			TlpMulti = new TableLayoutPanel();
			DgvMultiAvailable = new Zuby.ADGV.AdvancedDataGridView();
			PnMultiButtons = new Panel();
			BtMultiAdd = new Button();
			BtMultiDown = new Button();
			BtMultiRemove = new Button();
			BtMultiUp = new Button();
			BtMultiAddAll = new Button();
			BtMultiRemoveAll = new Button();
			LbMultiAvailable = new Label();
			LbMultiSelected = new Label();
			DgvMultiSelected = new Zuby.ADGV.AdvancedDataGridView();
			CbPartGroupType = new ComboBox();
			TlpMulti.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DgvMultiAvailable).BeginInit();
			PnMultiButtons.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)DgvMultiSelected).BeginInit();
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
			LbGroups.Location = new Point(3, 37);
			LbGroups.Name = "LbGroups";
			LbGroups.Size = new Size(55, 15);
			LbGroups.TabIndex = 1;
			LbGroups.Text = "Selection";
			// 
			// TbHeader
			// 
			TbHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbHeader.Location = new Point(74, 4);
			TbHeader.Name = "TbHeader";
			TbHeader.Size = new Size(1064, 23);
			TbHeader.TabIndex = 2;
			// 
			// LbColumns
			// 
			LbColumns.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
			LbColumns.AutoSize = true;
			LbColumns.Location = new Point(3, 595);
			LbColumns.Name = "LbColumns";
			LbColumns.Size = new Size(55, 15);
			LbColumns.TabIndex = 5;
			LbColumns.Text = "Columns";
			// 
			// LstColumns
			// 
			LstColumns.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			LstColumns.FormattingEnabled = true;
			LstColumns.Location = new Point(3, 613);
			LstColumns.Name = "LstColumns";
			LstColumns.Size = new Size(1135, 94);
			LstColumns.TabIndex = 6;
			// 
			// TlpMulti
			// 
			TlpMulti.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			TlpMulti.ColumnCount = 3;
			TlpMulti.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
			TlpMulti.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 40F));
			TlpMulti.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
			TlpMulti.Controls.Add(DgvMultiAvailable, 0, 1);
			TlpMulti.Controls.Add(PnMultiButtons, 1, 1);
			TlpMulti.Controls.Add(LbMultiAvailable, 0, 0);
			TlpMulti.Controls.Add(LbMultiSelected, 2, 0);
			TlpMulti.Controls.Add(DgvMultiSelected, 2, 1);
			TlpMulti.Location = new Point(3, 58);
			TlpMulti.Name = "TlpMulti";
			TlpMulti.RowCount = 2;
			TlpMulti.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			TlpMulti.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			TlpMulti.Size = new Size(1135, 556);
			TlpMulti.TabIndex = 8;
			// 
			// DgvMultiAvailable
			// 
			DgvMultiAvailable.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvMultiAvailable.Dock = DockStyle.Fill;
			DgvMultiAvailable.FilterAndSortEnabled = true;
			DgvMultiAvailable.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvMultiAvailable.Location = new Point(3, 23);
			DgvMultiAvailable.Name = "DgvMultiAvailable";
			DgvMultiAvailable.RightToLeft = RightToLeft.No;
			DgvMultiAvailable.RowTemplate.Height = 25;
			DgvMultiAvailable.Size = new Size(651, 530);
			DgvMultiAvailable.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvMultiAvailable.TabIndex = 1;
			DgvMultiAvailable.CellDoubleClick += DgvMultiAvailable_CellDoubleClick;
			// 
			// PnMultiButtons
			// 
			PnMultiButtons.Controls.Add(BtMultiAdd);
			PnMultiButtons.Controls.Add(BtMultiDown);
			PnMultiButtons.Controls.Add(BtMultiRemove);
			PnMultiButtons.Controls.Add(BtMultiUp);
			PnMultiButtons.Controls.Add(BtMultiAddAll);
			PnMultiButtons.Controls.Add(BtMultiRemoveAll);
			PnMultiButtons.Dock = DockStyle.Fill;
			PnMultiButtons.Location = new Point(660, 23);
			PnMultiButtons.Name = "PnMultiButtons";
			PnMultiButtons.Size = new Size(34, 530);
			PnMultiButtons.TabIndex = 0;
			// 
			// BtMultiAdd
			// 
			BtMultiAdd.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtMultiAdd.Location = new Point(0, 3);
			BtMultiAdd.Name = "BtMultiAdd";
			BtMultiAdd.Size = new Size(34, 23);
			BtMultiAdd.TabIndex = 8;
			BtMultiAdd.Text = ">";
			BtMultiAdd.UseVisualStyleBackColor = true;
			BtMultiAdd.Click += BtMultiAdd_Click;
			// 
			// BtMultiDown
			// 
			BtMultiDown.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtMultiDown.Location = new Point(0, 148);
			BtMultiDown.Name = "BtMultiDown";
			BtMultiDown.Size = new Size(34, 23);
			BtMultiDown.TabIndex = 13;
			BtMultiDown.Text = "v";
			BtMultiDown.UseVisualStyleBackColor = true;
			BtMultiDown.Click += BtMultiDown_Click;
			// 
			// BtMultiRemove
			// 
			BtMultiRemove.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtMultiRemove.Location = new Point(0, 32);
			BtMultiRemove.Name = "BtMultiRemove";
			BtMultiRemove.Size = new Size(34, 23);
			BtMultiRemove.TabIndex = 9;
			BtMultiRemove.Text = "<";
			BtMultiRemove.UseVisualStyleBackColor = true;
			BtMultiRemove.Click += BtMultiRemove_Click;
			// 
			// BtMultiUp
			// 
			BtMultiUp.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtMultiUp.Location = new Point(0, 119);
			BtMultiUp.Name = "BtMultiUp";
			BtMultiUp.Size = new Size(34, 23);
			BtMultiUp.TabIndex = 12;
			BtMultiUp.Text = "^";
			BtMultiUp.UseVisualStyleBackColor = true;
			BtMultiUp.Click += BtMultiUp_Click;
			// 
			// BtMultiAddAll
			// 
			BtMultiAddAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtMultiAddAll.Location = new Point(0, 61);
			BtMultiAddAll.Name = "BtMultiAddAll";
			BtMultiAddAll.Size = new Size(34, 23);
			BtMultiAddAll.TabIndex = 10;
			BtMultiAddAll.Text = ">>";
			BtMultiAddAll.UseVisualStyleBackColor = true;
			BtMultiAddAll.Visible = false;
			BtMultiAddAll.Click += BtMultiAddAll_Click;
			// 
			// BtMultiRemoveAll
			// 
			BtMultiRemoveAll.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			BtMultiRemoveAll.Location = new Point(0, 90);
			BtMultiRemoveAll.Name = "BtMultiRemoveAll";
			BtMultiRemoveAll.Size = new Size(34, 23);
			BtMultiRemoveAll.TabIndex = 11;
			BtMultiRemoveAll.Text = "<<";
			BtMultiRemoveAll.UseVisualStyleBackColor = true;
			BtMultiRemoveAll.Visible = false;
			BtMultiRemoveAll.Click += BtMultiRemoveAll_Click;
			// 
			// LbMultiAvailable
			// 
			LbMultiAvailable.Dock = DockStyle.Fill;
			LbMultiAvailable.Location = new Point(3, 0);
			LbMultiAvailable.Name = "LbMultiAvailable";
			LbMultiAvailable.Size = new Size(651, 20);
			LbMultiAvailable.TabIndex = 2;
			LbMultiAvailable.Text = "Available";
			LbMultiAvailable.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// LbMultiSelected
			// 
			LbMultiSelected.Dock = DockStyle.Fill;
			LbMultiSelected.Location = new Point(700, 0);
			LbMultiSelected.Name = "LbMultiSelected";
			LbMultiSelected.Size = new Size(432, 20);
			LbMultiSelected.TabIndex = 4;
			LbMultiSelected.Text = "Selected";
			LbMultiSelected.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// DgvMultiSelected
			// 
			DgvMultiSelected.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvMultiSelected.Dock = DockStyle.Fill;
			DgvMultiSelected.FilterAndSortEnabled = false;
			DgvMultiSelected.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvMultiSelected.Location = new Point(700, 23);
			DgvMultiSelected.Name = "DgvMultiSelected";
			DgvMultiSelected.RightToLeft = RightToLeft.No;
			DgvMultiSelected.RowTemplate.Height = 25;
			DgvMultiSelected.Size = new Size(432, 530);
			DgvMultiSelected.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvMultiSelected.TabIndex = 5;
			DgvMultiSelected.CellDoubleClick += DgvMultiSelected_CellDoubleClick;
			// 
			// CbPartGroupType
			// 
			CbPartGroupType.FormattingEnabled = true;
			CbPartGroupType.Location = new Point(74, 33);
			CbPartGroupType.Name = "CbPartGroupType";
			CbPartGroupType.Size = new Size(159, 23);
			CbPartGroupType.TabIndex = 9;
			CbPartGroupType.SelectedValueChanged += CbPartGroupType_SelectedValueChanged;
			// 
			// UcBriefingPartGroups
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(CbPartGroupType);
			Controls.Add(TlpMulti);
			Controls.Add(LstColumns);
			Controls.Add(LbColumns);
			Controls.Add(TbHeader);
			Controls.Add(LbGroups);
			Controls.Add(LbHeader);
			Name = "UcBriefingPartGroups";
			Size = new Size(1141, 710);
			TlpMulti.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DgvMultiAvailable).EndInit();
			PnMultiButtons.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)DgvMultiSelected).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label LbHeader;
		private Label LbGroups;
		private TextBox TbHeader;
		private Label LbColumns;
		private CheckedListBox LstColumns;
		private TableLayoutPanel TlpMulti;
		private Zuby.ADGV.AdvancedDataGridView DgvMultiAvailable;
		private Panel PnMultiButtons;
		public Button BtMultiAdd;
		public Button BtMultiDown;
		public Button BtMultiRemove;
		public Button BtMultiUp;
		public Button BtMultiAddAll;
		public Button BtMultiRemoveAll;
		private Label LbMultiAvailable;
		private Label LbMultiSelected;
		private Zuby.ADGV.AdvancedDataGridView DgvMultiSelected;
		private ComboBox CbPartGroupType;
	}
}
