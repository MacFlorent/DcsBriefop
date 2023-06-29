namespace DcsBriefop.Forms
{
	partial class UcBriefingPartTableText
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
			CkBullseyeWithDescription = new CheckBox();
			CkBullseye = new CheckBox();
			CkWeather = new CheckBox();
			SuspendLayout();
			// 
			// CkBullseyeWithDescription
			// 
			CkBullseyeWithDescription.AutoSize = true;
			CkBullseyeWithDescription.Location = new Point(78, 3);
			CkBullseyeWithDescription.Name = "CkBullseyeWithDescription";
			CkBullseyeWithDescription.Size = new Size(113, 19);
			CkBullseyeWithDescription.TabIndex = 0;
			CkBullseyeWithDescription.Text = "With description";
			CkBullseyeWithDescription.UseVisualStyleBackColor = true;
			// 
			// CkBullseye
			// 
			CkBullseye.AutoSize = true;
			CkBullseye.Location = new Point(3, 3);
			CkBullseye.Name = "CkBullseye";
			CkBullseye.Size = new Size(69, 19);
			CkBullseye.TabIndex = 1;
			CkBullseye.Text = "Bullseye";
			CkBullseye.UseVisualStyleBackColor = true;
			// 
			// CkWeather
			// 
			CkWeather.AutoSize = true;
			CkWeather.Location = new Point(3, 28);
			CkWeather.Name = "CkWeather";
			CkWeather.Size = new Size(70, 19);
			CkWeather.TabIndex = 2;
			CkWeather.Text = "Weather";
			CkWeather.UseVisualStyleBackColor = true;
			// 
			// UcBriefingPartTableText
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(CkWeather);
			Controls.Add(CkBullseye);
			Controls.Add(CkBullseyeWithDescription);
			Name = "UcBriefingPartTableText";
			Size = new Size(279, 61);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private CheckBox CkBullseyeWithDescription;
		private CheckBox CkBullseye;
		private CheckBox CkWeather;
	}
}
