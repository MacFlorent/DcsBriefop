
namespace DcsBriefop
{
	partial class FrmDatabaseDcsObject
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
			this.BtSave = new System.Windows.Forms.Button();
			this.BtClose = new System.Windows.Forms.Button();
			this.AdgvDatabase = new Zuby.ADGV.AdvancedDataGridView();
			((System.ComponentModel.ISupportInitialize)(this.AdgvDatabase)).BeginInit();
			this.SuspendLayout();
			// 
			// BtSave
			// 
			this.BtSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtSave.Location = new System.Drawing.Point(546, 509);
			this.BtSave.Name = "BtSave";
			this.BtSave.Size = new System.Drawing.Size(158, 23);
			this.BtSave.TabIndex = 42;
			this.BtSave.Text = "Save custom information";
			this.BtSave.UseVisualStyleBackColor = true;
			this.BtSave.Click += new System.EventHandler(this.BtSave_Click);
			// 
			// BtClose
			// 
			this.BtClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtClose.Location = new System.Drawing.Point(710, 509);
			this.BtClose.Name = "BtClose";
			this.BtClose.Size = new System.Drawing.Size(75, 23);
			this.BtClose.TabIndex = 43;
			this.BtClose.Text = "Close";
			this.BtClose.UseVisualStyleBackColor = true;
			this.BtClose.Click += new System.EventHandler(this.BtClose_Click);
			// 
			// AdgvDatabase
			// 
			this.AdgvDatabase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AdgvDatabase.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.AdgvDatabase.FilterAndSortEnabled = true;
			this.AdgvDatabase.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.AdgvDatabase.Location = new System.Drawing.Point(12, 12);
			this.AdgvDatabase.Name = "AdgvDatabase";
			this.AdgvDatabase.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.AdgvDatabase.Size = new System.Drawing.Size(773, 491);
			this.AdgvDatabase.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.AdgvDatabase.TabIndex = 44;
			this.AdgvDatabase.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.AdgvDatabase_CellValueChanged);
			// 
			// FrmDatabaseDcsObject
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(797, 535);
			this.Controls.Add(this.AdgvDatabase);
			this.Controls.Add(this.BtClose);
			this.Controls.Add(this.BtSave);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FrmDatabaseDcsObject";
			this.Text = "Database : DCS objects";
			((System.ComponentModel.ISupportInitialize)(this.AdgvDatabase)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button BtSave;
		private System.Windows.Forms.Button BtClose;
		private Zuby.ADGV.AdvancedDataGridView AdgvDatabase;
	}
}