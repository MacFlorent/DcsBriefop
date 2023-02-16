namespace DcsBriefop.Forms
{
	partial class UcBriefingPartBullseye
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
			this.CkWithDescription = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// CkWithDescription
			// 
			this.CkWithDescription.AutoSize = true;
			this.CkWithDescription.Location = new System.Drawing.Point(0, 0);
			this.CkWithDescription.Name = "CkWithDescription";
			this.CkWithDescription.Size = new System.Drawing.Size(113, 19);
			this.CkWithDescription.TabIndex = 0;
			this.CkWithDescription.Text = "With description";
			this.CkWithDescription.UseVisualStyleBackColor = true;
			// 
			// UcBriefingPartBullseye
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CkWithDescription);
			this.Name = "UcBriefingPartBullseye";
			this.Size = new System.Drawing.Size(148, 24);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private CheckBox CkWithDescription;
	}
}
