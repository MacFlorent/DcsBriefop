
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
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio2)).BeginInit();
			this.SuspendLayout();
			// 
			// BtOk
			// 
			this.BtOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtOk.Location = new System.Drawing.Point(332, 373);
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
			this.DgvRadio1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvRadio1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.DgvRadio1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvRadio1.Location = new System.Drawing.Point(12, 12);
			this.DgvRadio1.Name = "DgvRadio1";
			this.DgvRadio1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvRadio1.Size = new System.Drawing.Size(268, 355);
			this.DgvRadio1.TabIndex = 25;
			// 
			// DgvRadio2
			// 
			this.DgvRadio2.AllowUserToAddRows = false;
			this.DgvRadio2.AllowUserToDeleteRows = false;
			this.DgvRadio2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvRadio2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.DgvRadio2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvRadio2.Location = new System.Drawing.Point(286, 12);
			this.DgvRadio2.Name = "DgvRadio2";
			this.DgvRadio2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvRadio2.Size = new System.Drawing.Size(268, 355);
			this.DgvRadio2.TabIndex = 26;
			// 
			// BtCancel
			// 
			this.BtCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtCancel.Location = new System.Drawing.Point(446, 373);
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
			this.BtClear.Location = new System.Drawing.Point(12, 373);
			this.BtClear.Name = "BtClear";
			this.BtClear.Size = new System.Drawing.Size(108, 23);
			this.BtClear.TabIndex = 28;
			this.BtClear.Text = "Clear";
			this.BtClear.UseVisualStyleBackColor = true;
			// 
			// BtAuto
			// 
			this.BtAuto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.BtAuto.Location = new System.Drawing.Point(126, 373);
			this.BtAuto.Name = "BtAuto";
			this.BtAuto.Size = new System.Drawing.Size(108, 23);
			this.BtAuto.TabIndex = 29;
			this.BtAuto.Text = "Auto";
			this.BtAuto.UseVisualStyleBackColor = true;
			// 
			// FrmComs
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(561, 403);
			this.Controls.Add(this.BtAuto);
			this.Controls.Add(this.BtClear);
			this.Controls.Add(this.BtCancel);
			this.Controls.Add(this.DgvRadio2);
			this.Controls.Add(this.DgvRadio1);
			this.Controls.Add(this.BtOk);
			this.Name = "FrmComs";
			this.ShowInTaskbar = false;
			this.Text = "FrmComs";
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DgvRadio2)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button BtOk;
		private System.Windows.Forms.DataGridView DgvRadio1;
		private System.Windows.Forms.DataGridView DgvRadio2;
		private System.Windows.Forms.Button BtCancel;
		private System.Windows.Forms.Button BtClear;
		private System.Windows.Forms.Button BtAuto;
	}
}