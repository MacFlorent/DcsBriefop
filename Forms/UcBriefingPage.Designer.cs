namespace DcsBriefop.Forms
{
	partial class UcBriefingPage
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
			this.TcPreview = new System.Windows.Forms.TabControl();
			this.TpHtml = new System.Windows.Forms.TabPage();
			this.BtRefresh = new System.Windows.Forms.Button();
			this.BtHtml = new System.Windows.Forms.Button();
			this.PbHtml = new System.Windows.Forms.PictureBox();
			this.TpMap = new System.Windows.Forms.TabPage();
			this.ScMain = new System.Windows.Forms.SplitContainer();
			this.CkDisplayTitle = new System.Windows.Forms.CheckBox();
			this.TbTitle = new System.Windows.Forms.TextBox();
			this.LbTitle = new System.Windows.Forms.Label();
			this.DgvParts = new Zuby.ADGV.AdvancedDataGridView();
			this.TcPreview.SuspendLayout();
			this.TpHtml.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.PbHtml)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).BeginInit();
			this.ScMain.Panel1.SuspendLayout();
			this.ScMain.Panel2.SuspendLayout();
			this.ScMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgvParts)).BeginInit();
			this.SuspendLayout();
			// 
			// TcPreview
			// 
			this.TcPreview.Controls.Add(this.TpHtml);
			this.TcPreview.Controls.Add(this.TpMap);
			this.TcPreview.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TcPreview.Location = new System.Drawing.Point(0, 0);
			this.TcPreview.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TcPreview.Name = "TcPreview";
			this.TcPreview.SelectedIndex = 0;
			this.TcPreview.Size = new System.Drawing.Size(1308, 828);
			this.TcPreview.TabIndex = 0;
			// 
			// TpHtml
			// 
			this.TpHtml.Controls.Add(this.BtRefresh);
			this.TpHtml.Controls.Add(this.BtHtml);
			this.TpHtml.Controls.Add(this.PbHtml);
			this.TpHtml.Location = new System.Drawing.Point(4, 24);
			this.TpHtml.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpHtml.Name = "TpHtml";
			this.TpHtml.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpHtml.Size = new System.Drawing.Size(1300, 800);
			this.TpHtml.TabIndex = 0;
			this.TpHtml.Text = "Html";
			this.TpHtml.UseVisualStyleBackColor = true;
			// 
			// BtRefresh
			// 
			this.BtRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtRefresh.Location = new System.Drawing.Point(546, 760);
			this.BtRefresh.Name = "BtRefresh";
			this.BtRefresh.Size = new System.Drawing.Size(75, 23);
			this.BtRefresh.TabIndex = 3;
			this.BtRefresh.Text = "Refresh";
			this.BtRefresh.UseVisualStyleBackColor = true;
			this.BtRefresh.Click += new System.EventHandler(this.BtRefresh_Click);
			// 
			// BtHtml
			// 
			this.BtHtml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtHtml.Location = new System.Drawing.Point(429, 760);
			this.BtHtml.Name = "BtHtml";
			this.BtHtml.Size = new System.Drawing.Size(111, 23);
			this.BtHtml.TabIndex = 2;
			this.BtHtml.Text = "Html source";
			this.BtHtml.UseVisualStyleBackColor = true;
			// 
			// PbHtml
			// 
			this.PbHtml.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PbHtml.Location = new System.Drawing.Point(11, 9);
			this.PbHtml.Name = "PbHtml";
			this.PbHtml.Size = new System.Drawing.Size(664, 680);
			this.PbHtml.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.PbHtml.TabIndex = 1;
			this.PbHtml.TabStop = false;
			// 
			// TpMap
			// 
			this.TpMap.Location = new System.Drawing.Point(4, 24);
			this.TpMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpMap.Name = "TpMap";
			this.TpMap.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpMap.Size = new System.Drawing.Size(1300, 800);
			this.TpMap.TabIndex = 1;
			this.TpMap.Text = "Map";
			this.TpMap.UseVisualStyleBackColor = true;
			// 
			// ScMain
			// 
			this.ScMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ScMain.Location = new System.Drawing.Point(0, 0);
			this.ScMain.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.ScMain.Name = "ScMain";
			// 
			// ScMain.Panel1
			// 
			this.ScMain.Panel1.Controls.Add(this.CkDisplayTitle);
			this.ScMain.Panel1.Controls.Add(this.TbTitle);
			this.ScMain.Panel1.Controls.Add(this.LbTitle);
			this.ScMain.Panel1.Controls.Add(this.DgvParts);
			// 
			// ScMain.Panel2
			// 
			this.ScMain.Panel2.AutoScroll = true;
			this.ScMain.Panel2.Controls.Add(this.TcPreview);
			this.ScMain.Size = new System.Drawing.Size(1657, 828);
			this.ScMain.SplitterDistance = 344;
			this.ScMain.SplitterWidth = 5;
			this.ScMain.TabIndex = 2;
			// 
			// CkDisplayTitle
			// 
			this.CkDisplayTitle.AutoSize = true;
			this.CkDisplayTitle.Location = new System.Drawing.Point(222, 20);
			this.CkDisplayTitle.Name = "CkDisplayTitle";
			this.CkDisplayTitle.Size = new System.Drawing.Size(87, 19);
			this.CkDisplayTitle.TabIndex = 3;
			this.CkDisplayTitle.Text = "Display title";
			this.CkDisplayTitle.UseVisualStyleBackColor = true;
			// 
			// TbTitle
			// 
			this.TbTitle.Location = new System.Drawing.Point(92, 15);
			this.TbTitle.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TbTitle.Name = "TbTitle";
			this.TbTitle.Size = new System.Drawing.Size(116, 23);
			this.TbTitle.TabIndex = 2;
			// 
			// LbTitle
			// 
			this.LbTitle.AutoSize = true;
			this.LbTitle.Location = new System.Drawing.Point(15, 15);
			this.LbTitle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbTitle.Name = "LbTitle";
			this.LbTitle.Size = new System.Drawing.Size(29, 15);
			this.LbTitle.TabIndex = 1;
			this.LbTitle.Text = "Title";
			// 
			// DgvParts
			// 
			this.DgvParts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvParts.FilterAndSortEnabled = true;
			this.DgvParts.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvParts.Location = new System.Drawing.Point(14, 55);
			this.DgvParts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DgvParts.Name = "DgvParts";
			this.DgvParts.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvParts.Size = new System.Drawing.Size(326, 735);
			this.DgvParts.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvParts.TabIndex = 0;
			// 
			// UcBriefingPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ScMain);
			this.Name = "UcBriefingPage";
			this.Size = new System.Drawing.Size(1657, 828);
			this.TcPreview.ResumeLayout(false);
			this.TpHtml.ResumeLayout(false);
			this.TpHtml.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.PbHtml)).EndInit();
			this.ScMain.Panel1.ResumeLayout(false);
			this.ScMain.Panel1.PerformLayout();
			this.ScMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).EndInit();
			this.ScMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DgvParts)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private TabControl TcPreview;
		private TabPage TpHtml;
		private Button BtRefresh;
		private Button BtHtml;
		private PictureBox PbHtml;
		private TabPage TpMap;
		private SplitContainer ScMain;
		private TextBox TbTitle;
		private Label LbTitle;
		private Zuby.ADGV.AdvancedDataGridView DgvParts;
		private CheckBox CkDisplayTitle;
	}
}
