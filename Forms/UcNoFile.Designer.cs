namespace DcsBriefop.Forms
{
	partial class UcNoFile
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
			this.linkLabel1 = new System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new System.Windows.Forms.LinkLabel();
			this.TlRecentMiz = new System.Windows.Forms.TableLayoutPanel();
			this.TlRecentMiz.SuspendLayout();
			this.SuspendLayout();
			// 
			// linkLabel1
			// 
			this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.linkLabel1.Location = new System.Drawing.Point(3, 0);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new System.Drawing.Size(601, 23);
			this.linkLabel1.TabIndex = 0;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "linkLabel1";
			this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// linkLabel2
			// 
			this.linkLabel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.linkLabel2.Location = new System.Drawing.Point(3, 51);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new System.Drawing.Size(601, 52);
			this.linkLabel2.TabIndex = 1;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "linkLabel2";
			this.linkLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
			// 
			// TlRecentMiz
			// 
			this.TlRecentMiz.AutoSize = true;
			this.TlRecentMiz.ColumnCount = 1;
			this.TlRecentMiz.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TlRecentMiz.Controls.Add(this.linkLabel2, 0, 1);
			this.TlRecentMiz.Controls.Add(this.linkLabel1, 0, 0);
			this.TlRecentMiz.Location = new System.Drawing.Point(37, 33);
			this.TlRecentMiz.Name = "TlRecentMiz";
			this.TlRecentMiz.RowCount = 2;
			this.TlRecentMiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TlRecentMiz.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.TlRecentMiz.Size = new System.Drawing.Size(607, 103);
			this.TlRecentMiz.TabIndex = 2;
			// 
			// UcNoFile
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TlRecentMiz);
			this.Name = "UcNoFile";
			this.Size = new System.Drawing.Size(666, 477);
			this.TlRecentMiz.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.LinkLabel linkLabel1;
		private System.Windows.Forms.LinkLabel linkLabel2;
		private System.Windows.Forms.TableLayoutPanel TlRecentMiz;
	}
}
