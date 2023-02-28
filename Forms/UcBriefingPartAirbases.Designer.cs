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
			DgvAirbases = new Zuby.ADGV.AdvancedDataGridView();
			((System.ComponentModel.ISupportInitialize)DgvAirbases).BeginInit();
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
			LbAirbases.Size = new Size(28, 15);
			LbAirbases.TabIndex = 1;
			LbAirbases.Text = "Text";
			// 
			// TbHeader
			// 
			TbHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbHeader.Location = new Point(74, 4);
			TbHeader.Name = "TbHeader";
			TbHeader.Size = new Size(244, 23);
			TbHeader.TabIndex = 2;
			// 
			// DgvAirbases
			// 
			DgvAirbases.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			DgvAirbases.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvAirbases.FilterAndSortEnabled = true;
			DgvAirbases.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvAirbases.Location = new Point(3, 51);
			DgvAirbases.Name = "DgvAirbases";
			DgvAirbases.RightToLeft = RightToLeft.No;
			DgvAirbases.RowTemplate.Height = 25;
			DgvAirbases.Size = new Size(315, 88);
			DgvAirbases.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvAirbases.TabIndex = 3;
			// 
			// UcBriefingPartAirbases
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(DgvAirbases);
			Controls.Add(TbHeader);
			Controls.Add(LbAirbases);
			Controls.Add(LbHeader);
			Name = "UcBriefingPartAirbases";
			Size = new Size(321, 142);
			((System.ComponentModel.ISupportInitialize)DgvAirbases).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Label LbHeader;
		private Label LbAirbases;
		private TextBox TbHeader;
		private Zuby.ADGV.AdvancedDataGridView DgvAirbases;
	}
}
