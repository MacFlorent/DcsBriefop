namespace DcsBriefop.Forms
{
	partial class FrmDcsObjects
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
			DgvDcsObjects = new Zuby.ADGV.AdvancedDataGridView();
			((System.ComponentModel.ISupportInitialize)DgvDcsObjects).BeginInit();
			SuspendLayout();
			// 
			// DgvDcsObjects
			// 
			DgvDcsObjects.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			DgvDcsObjects.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			DgvDcsObjects.FilterAndSortEnabled = true;
			DgvDcsObjects.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvDcsObjects.Location = new Point(12, 12);
			DgvDcsObjects.Name = "DgvDcsObjects";
			DgvDcsObjects.RightToLeft = RightToLeft.No;
			DgvDcsObjects.RowTemplate.Height = 25;
			DgvDcsObjects.Size = new Size(559, 596);
			DgvDcsObjects.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			DgvDcsObjects.TabIndex = 0;
			// 
			// FrmDcsObjects
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(583, 620);
			Controls.Add(DgvDcsObjects);
			FormBorderStyle = FormBorderStyle.SizableToolWindow;
			Name = "FrmDcsObjects";
			Text = "DCS objects";
			((System.ComponentModel.ISupportInitialize)DgvDcsObjects).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private Zuby.ADGV.AdvancedDataGridView DgvDcsObjects;
	}
}