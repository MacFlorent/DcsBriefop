namespace DcsBriefop.Forms
{
	partial class UcBriefingPartWaypoints
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
			CkDisplayGroupName = new CheckBox();
			CbGroup = new ComboBox();
			LbGroup = new Label();
			LbHeader = new Label();
			TbHeader = new TextBox();
			LstColumns = new CheckedListBox();
			SuspendLayout();
			// 
			// CkDisplayGroupName
			// 
			CkDisplayGroupName.AutoSize = true;
			CkDisplayGroupName.Location = new Point(64, 29);
			CkDisplayGroupName.Name = "CkDisplayGroupName";
			CkDisplayGroupName.Size = new Size(132, 19);
			CkDisplayGroupName.TabIndex = 1;
			CkDisplayGroupName.Text = "Display group name";
			CkDisplayGroupName.UseVisualStyleBackColor = true;
			// 
			// CbGroup
			// 
			CbGroup.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			CbGroup.FormattingEnabled = true;
			CbGroup.Location = new Point(64, 51);
			CbGroup.Name = "CbGroup";
			CbGroup.Size = new Size(513, 23);
			CbGroup.TabIndex = 2;
			CbGroup.SelectedValueChanged += CbGroup_SelectedValueChanged;
			// 
			// LbGroup
			// 
			LbGroup.AutoSize = true;
			LbGroup.Location = new Point(3, 54);
			LbGroup.Name = "LbGroup";
			LbGroup.Size = new Size(40, 15);
			LbGroup.TabIndex = 4;
			LbGroup.Text = "Group";
			// 
			// LbHeader
			// 
			LbHeader.AutoSize = true;
			LbHeader.Location = new Point(3, 9);
			LbHeader.Name = "LbHeader";
			LbHeader.Size = new Size(45, 15);
			LbHeader.TabIndex = 5;
			LbHeader.Text = "Header";
			// 
			// TbHeader
			// 
			TbHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbHeader.Location = new Point(64, 3);
			TbHeader.Name = "TbHeader";
			TbHeader.Size = new Size(513, 23);
			TbHeader.TabIndex = 6;
			// 
			// LstColumns
			// 
			LstColumns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			LstColumns.FormattingEnabled = true;
			LstColumns.Location = new Point(3, 82);
			LstColumns.Name = "LstColumns";
			LstColumns.Size = new Size(574, 130);
			LstColumns.TabIndex = 7;
			// 
			// UcBriefingPartWaypoints
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(LstColumns);
			Controls.Add(TbHeader);
			Controls.Add(LbHeader);
			Controls.Add(LbGroup);
			Controls.Add(CbGroup);
			Controls.Add(CkDisplayGroupName);
			Name = "UcBriefingPartWaypoints";
			Size = new Size(580, 223);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private CheckBox CkDisplayGroupName;
		private ComboBox CbGroup;
		private Label LbGroup;
		private Label LbHeader;
		private TextBox TbHeader;
		private CheckedListBox LstColumns;
	}
}
