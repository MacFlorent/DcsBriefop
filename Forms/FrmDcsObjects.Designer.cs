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
			DgvDcsObjects = new BrightIdeasSoftware.FastObjectListView();
			SuspendLayout();
			//
			// DgvDcsObjects
			//
			DgvDcsObjects.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			DgvDcsObjects.Location = new Point(12, 12);
			DgvDcsObjects.Name = "DgvDcsObjects";
			DgvDcsObjects.Size = new Size(559, 596);
			DgvDcsObjects.TabIndex = 0;
			DgvDcsObjects.View = System.Windows.Forms.View.Details;
			// 
			// FrmDcsObjects
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(583, 620);
			Controls.Add(DgvDcsObjects);
			Name = "FrmDcsObjects";
			ShowIcon = false;
			Text = "DCS objects";
			Shown += FrmDcsObjects_Shown;
			((System.ComponentModel.ISupportInitialize)DgvDcsObjects).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private BrightIdeasSoftware.FastObjectListView DgvDcsObjects;
	}
}