namespace DcsBriefop.Forms
{
	partial class FrmBriefingPage
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
			this.TcPreview = new System.Windows.Forms.TabControl();
			this.TpHtml = new System.Windows.Forms.TabPage();
			this.button1 = new System.Windows.Forms.Button();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.TpMap = new System.Windows.Forms.TabPage();
			this.ScMain = new System.Windows.Forms.SplitContainer();
			this.TbName = new System.Windows.Forms.TextBox();
			this.LbName = new System.Windows.Forms.Label();
			this.DgvBriefingParts = new Zuby.ADGV.AdvancedDataGridView();
			this.TcPreview.SuspendLayout();
			this.TpHtml.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).BeginInit();
			this.ScMain.Panel1.SuspendLayout();
			this.ScMain.Panel2.SuspendLayout();
			this.ScMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgvBriefingParts)).BeginInit();
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
			this.TcPreview.Size = new System.Drawing.Size(687, 714);
			this.TcPreview.TabIndex = 0;
			// 
			// TpHtml
			// 
			this.TpHtml.Controls.Add(this.button1);
			this.TpHtml.Controls.Add(this.pictureBox1);
			this.TpHtml.Controls.Add(this.textBox1);
			this.TpHtml.Location = new System.Drawing.Point(4, 24);
			this.TpHtml.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpHtml.Name = "TpHtml";
			this.TpHtml.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpHtml.Size = new System.Drawing.Size(679, 686);
			this.TpHtml.TabIndex = 0;
			this.TpHtml.Text = "Html";
			this.TpHtml.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.Location = new System.Drawing.Point(596, 660);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// pictureBox1
			// 
			this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBox1.Location = new System.Drawing.Point(7, 303);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(664, 354);
			this.pictureBox1.TabIndex = 1;
			this.pictureBox1.TabStop = false;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(7, 6);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(664, 291);
			this.textBox1.TabIndex = 0;
			// 
			// TpMap
			// 
			this.TpMap.Location = new System.Drawing.Point(4, 24);
			this.TpMap.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpMap.Name = "TpMap";
			this.TpMap.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TpMap.Size = new System.Drawing.Size(679, 686);
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
			this.ScMain.Panel1.Controls.Add(this.TbName);
			this.ScMain.Panel1.Controls.Add(this.LbName);
			this.ScMain.Panel1.Controls.Add(this.DgvBriefingParts);
			// 
			// ScMain.Panel2
			// 
			this.ScMain.Panel2.Controls.Add(this.TcPreview);
			this.ScMain.Size = new System.Drawing.Size(1037, 714);
			this.ScMain.SplitterDistance = 345;
			this.ScMain.SplitterWidth = 5;
			this.ScMain.TabIndex = 1;
			// 
			// TbName
			// 
			this.TbName.Location = new System.Drawing.Point(92, 15);
			this.TbName.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TbName.Name = "TbName";
			this.TbName.Size = new System.Drawing.Size(116, 23);
			this.TbName.TabIndex = 2;
			// 
			// LbName
			// 
			this.LbName.AutoSize = true;
			this.LbName.Location = new System.Drawing.Point(15, 15);
			this.LbName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbName.Name = "LbName";
			this.LbName.Size = new System.Drawing.Size(39, 15);
			this.LbName.TabIndex = 1;
			this.LbName.Text = "Name";
			// 
			// DgvBriefingParts
			// 
			this.DgvBriefingParts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvBriefingParts.FilterAndSortEnabled = true;
			this.DgvBriefingParts.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvBriefingParts.Location = new System.Drawing.Point(14, 55);
			this.DgvBriefingParts.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DgvBriefingParts.Name = "DgvBriefingParts";
			this.DgvBriefingParts.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvBriefingParts.Size = new System.Drawing.Size(308, 323);
			this.DgvBriefingParts.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvBriefingParts.TabIndex = 0;
			// 
			// FrmBriefingPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1037, 714);
			this.Controls.Add(this.ScMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.Name = "FrmBriefingPage";
			this.ShowInTaskbar = false;
			this.Text = "FrmBriefingPage";
			this.TcPreview.ResumeLayout(false);
			this.TpHtml.ResumeLayout(false);
			this.TpHtml.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.ScMain.Panel1.ResumeLayout(false);
			this.ScMain.Panel1.PerformLayout();
			this.ScMain.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).EndInit();
			this.ScMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DgvBriefingParts)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl TcPreview;
		private System.Windows.Forms.TabPage TpHtml;
		private System.Windows.Forms.TabPage TpMap;
		private System.Windows.Forms.SplitContainer ScMain;
		private System.Windows.Forms.TextBox TbName;
		private System.Windows.Forms.Label LbName;
		private Zuby.ADGV.AdvancedDataGridView DgvBriefingParts;
		private GMap.NET.WindowsForms.GMapControl MapControl;
		private TextBox textBox1;
		private PictureBox pictureBox1;
		private Button button1;
	}
}