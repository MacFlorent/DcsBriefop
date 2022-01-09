
namespace DcsBriefop
{
	partial class FrmComs
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
			this.BtOk = new System.Windows.Forms.Button();
			this.DgvRadio1 = new System.Windows.Forms.DataGridView();
			this.DgvRadio2 = new System.Windows.Forms.DataGridView();
			this.BtCancel = new System.Windows.Forms.Button();
			this.BtClear = new System.Windows.Forms.Button();
			this.BtAuto = new System.Windows.Forms.Button();
			this.TlRadios = new System.Windows.Forms.TableLayoutPanel();
			this.PnRadio1 = new System.Windows.Forms.Panel();
			this.LbRadio1 = new System.Windows.Forms.Label();
			this.PnRadio2 = new System.Windows.Forms.Panel();
			this.LbRadio2 = new System.Windows.Forms.Label();
			this.TbLegend = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio2)).BeginInit();
			this.TlRadios.SuspendLayout();
			this.PnRadio1.SuspendLayout();
			this.PnRadio2.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtOk
			// 
			this.BtOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtOk.Location = new System.Drawing.Point(855, 542);
			this.BtOk.Name = "BtOk";
			this.BtOk.Size = new System.Drawing.Size(108, 23);
			this.BtOk.TabIndex = 3;
			this.BtOk.Text = "OK";
			this.BtOk.UseVisualStyleBackColor = true;
			this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
			// 
			// DgvRadio1
			// 
			this.DgvRadio1.AllowUserToAddRows = false;
			this.DgvRadio1.AllowUserToDeleteRows = false;
			this.DgvRadio1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.DgvRadio1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvRadio1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DgvRadio1.Location = new System.Drawing.Point(0, 23);
			this.DgvRadio1.Name = "DgvRadio1";
			this.DgvRadio1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvRadio1.Size = new System.Drawing.Size(524, 365);
			this.DgvRadio1.TabIndex = 25;
			// 
			// DgvRadio2
			// 
			this.DgvRadio2.AllowUserToAddRows = false;
			this.DgvRadio2.AllowUserToDeleteRows = false;
			this.DgvRadio2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.DgvRadio2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvRadio2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DgvRadio2.Location = new System.Drawing.Point(0, 23);
			this.DgvRadio2.Name = "DgvRadio2";
			this.DgvRadio2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvRadio2.Size = new System.Drawing.Size(524, 365);
			this.DgvRadio2.TabIndex = 26;
			// 
			// BtCancel
			// 
			this.BtCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtCancel.Location = new System.Drawing.Point(969, 542);
			this.BtCancel.Name = "BtCancel";
			this.BtCancel.Size = new System.Drawing.Size(108, 23);
			this.BtCancel.TabIndex = 27;
			this.BtCancel.Text = "Cancel";
			this.BtCancel.UseVisualStyleBackColor = true;
			this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
			// 
			// BtClear
			// 
			this.BtClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BtClear.Location = new System.Drawing.Point(12, 542);
			this.BtClear.Name = "BtClear";
			this.BtClear.Size = new System.Drawing.Size(108, 23);
			this.BtClear.TabIndex = 28;
			this.BtClear.Text = "Clear";
			this.BtClear.UseVisualStyleBackColor = true;
			this.BtClear.Click += new System.EventHandler(this.BtClear_Click);
			// 
			// BtAuto
			// 
			this.BtAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BtAuto.Location = new System.Drawing.Point(126, 542);
			this.BtAuto.Name = "BtAuto";
			this.BtAuto.Size = new System.Drawing.Size(108, 23);
			this.BtAuto.TabIndex = 29;
			this.BtAuto.Text = "Auto";
			this.BtAuto.UseVisualStyleBackColor = true;
			this.BtAuto.Click += new System.EventHandler(this.BtAuto_Click);
			// 
			// TlRadios
			// 
			this.TlRadios.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TlRadios.ColumnCount = 2;
			this.TlRadios.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TlRadios.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TlRadios.Controls.Add(this.PnRadio1, 0, 0);
			this.TlRadios.Controls.Add(this.PnRadio2, 1, 0);
			this.TlRadios.Location = new System.Drawing.Point(12, 12);
			this.TlRadios.Name = "TlRadios";
			this.TlRadios.RowCount = 1;
			this.TlRadios.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TlRadios.Size = new System.Drawing.Size(1060, 394);
			this.TlRadios.TabIndex = 30;
			// 
			// PnRadio1
			// 
			this.PnRadio1.Controls.Add(this.DgvRadio1);
			this.PnRadio1.Controls.Add(this.LbRadio1);
			this.PnRadio1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PnRadio1.Location = new System.Drawing.Point(3, 3);
			this.PnRadio1.Name = "PnRadio1";
			this.PnRadio1.Size = new System.Drawing.Size(524, 388);
			this.PnRadio1.TabIndex = 27;
			// 
			// LbRadio1
			// 
			this.LbRadio1.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbRadio1.Location = new System.Drawing.Point(0, 0);
			this.LbRadio1.Name = "LbRadio1";
			this.LbRadio1.Size = new System.Drawing.Size(524, 23);
			this.LbRadio1.TabIndex = 26;
			this.LbRadio1.Text = "Radio 1";
			this.LbRadio1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PnRadio2
			// 
			this.PnRadio2.Controls.Add(this.DgvRadio2);
			this.PnRadio2.Controls.Add(this.LbRadio2);
			this.PnRadio2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PnRadio2.Location = new System.Drawing.Point(533, 3);
			this.PnRadio2.Name = "PnRadio2";
			this.PnRadio2.Size = new System.Drawing.Size(524, 388);
			this.PnRadio2.TabIndex = 28;
			// 
			// LbRadio2
			// 
			this.LbRadio2.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbRadio2.Location = new System.Drawing.Point(0, 0);
			this.LbRadio2.Name = "LbRadio2";
			this.LbRadio2.Size = new System.Drawing.Size(524, 23);
			this.LbRadio2.TabIndex = 27;
			this.LbRadio2.Text = "Radio 2";
			this.LbRadio2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// TbLegend
			// 
			this.TbLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbLegend.Location = new System.Drawing.Point(12, 412);
			this.TbLegend.Multiline = true;
			this.TbLegend.Name = "TbLegend";
			this.TbLegend.ReadOnly = true;
			this.TbLegend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbLegend.Size = new System.Drawing.Size(1065, 124);
			this.TbLegend.TabIndex = 32;
			// 
			// FrmComs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1084, 572);
			this.Controls.Add(this.TbLegend);
			this.Controls.Add(this.TlRadios);
			this.Controls.Add(this.BtAuto);
			this.Controls.Add(this.BtClear);
			this.Controls.Add(this.BtCancel);
			this.Controls.Add(this.BtOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FrmComs";
			this.ShowInTaskbar = false;
			this.Text = "Communications";
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio2)).EndInit();
			this.TlRadios.ResumeLayout(false);
			this.PnRadio1.ResumeLayout(false);
			this.PnRadio2.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button BtOk;
		private System.Windows.Forms.DataGridView DgvRadio1;
		private System.Windows.Forms.DataGridView DgvRadio2;
		private System.Windows.Forms.Button BtCancel;
		private System.Windows.Forms.Button BtClear;
		private System.Windows.Forms.Button BtAuto;
		private System.Windows.Forms.TableLayoutPanel TlRadios;
		private System.Windows.Forms.Panel PnRadio1;
		private System.Windows.Forms.Label LbRadio1;
		private System.Windows.Forms.Panel PnRadio2;
		private System.Windows.Forms.Label LbRadio2;
		private System.Windows.Forms.TextBox TbLegend;
	}
}