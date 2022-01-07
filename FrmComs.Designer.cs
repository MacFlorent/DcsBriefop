
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio2)).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtOk
			// 
			this.BtOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtOk.Location = new System.Drawing.Point(742, 448);
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
			this.DgvRadio1.Location = new System.Drawing.Point(3, 3);
			this.DgvRadio1.Name = "DgvRadio1";
			this.DgvRadio1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvRadio1.Size = new System.Drawing.Size(467, 424);
			this.DgvRadio1.TabIndex = 25;
			// 
			// DgvRadio2
			// 
			this.DgvRadio2.AllowUserToAddRows = false;
			this.DgvRadio2.AllowUserToDeleteRows = false;
			this.DgvRadio2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.DgvRadio2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvRadio2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DgvRadio2.Location = new System.Drawing.Point(476, 3);
			this.DgvRadio2.Name = "DgvRadio2";
			this.DgvRadio2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvRadio2.Size = new System.Drawing.Size(468, 424);
			this.DgvRadio2.TabIndex = 26;
			// 
			// BtCancel
			// 
			this.BtCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtCancel.Location = new System.Drawing.Point(856, 448);
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
			this.BtClear.Location = new System.Drawing.Point(12, 448);
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
			this.BtAuto.Location = new System.Drawing.Point(126, 448);
			this.BtAuto.Name = "BtAuto";
			this.BtAuto.Size = new System.Drawing.Size(108, 23);
			this.BtAuto.TabIndex = 29;
			this.BtAuto.Text = "Auto";
			this.BtAuto.UseVisualStyleBackColor = true;
			this.BtAuto.Click += new System.EventHandler(this.BtAuto_Click);
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.DgvRadio1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.DgvRadio2, 1, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(947, 430);
			this.tableLayoutPanel1.TabIndex = 30;
			// 
			// FrmComs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(971, 478);
			this.Controls.Add(this.tableLayoutPanel1);
			this.Controls.Add(this.BtAuto);
			this.Controls.Add(this.BtClear);
			this.Controls.Add(this.BtCancel);
			this.Controls.Add(this.BtOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FrmComs";
			this.ShowInTaskbar = false;
			this.Text = "Coms";
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio2)).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button BtOk;
		private System.Windows.Forms.DataGridView DgvRadio1;
		private System.Windows.Forms.DataGridView DgvRadio2;
		private System.Windows.Forms.Button BtCancel;
		private System.Windows.Forms.Button BtClear;
		private System.Windows.Forms.Button BtAuto;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
	}
}