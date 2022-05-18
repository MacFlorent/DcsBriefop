
namespace DcsBriefop.UcBriefing
{
	partial class UcBriefingSituation
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
			this.DtpDate = new System.Windows.Forms.DateTimePicker();
			this.TbDescription = new System.Windows.Forms.TextBox();
			this.TbWeather = new System.Windows.Forms.TextBox();
			this.LbDate = new System.Windows.Forms.Label();
			this.TbSortie = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// DtpDate
			// 
			this.DtpDate.CustomFormat = "dd/MM/yyyy HH:mm";
			this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DtpDate.Location = new System.Drawing.Point(63, 4);
			this.DtpDate.Name = "DtpDate";
			this.DtpDate.Size = new System.Drawing.Size(143, 20);
			this.DtpDate.TabIndex = 1;
			// 
			// TbDescription
			// 
			this.TbDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbDescription.Location = new System.Drawing.Point(3, 118);
			this.TbDescription.Multiline = true;
			this.TbDescription.Name = "TbDescription";
			this.TbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbDescription.Size = new System.Drawing.Size(1036, 564);
			this.TbDescription.TabIndex = 3;
			// 
			// TbWeather
			// 
			this.TbWeather.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbWeather.Location = new System.Drawing.Point(3, 28);
			this.TbWeather.Multiline = true;
			this.TbWeather.Name = "TbWeather";
			this.TbWeather.ReadOnly = true;
			this.TbWeather.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbWeather.Size = new System.Drawing.Size(1036, 84);
			this.TbWeather.TabIndex = 2;
			this.TbWeather.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// LbDate
			// 
			this.LbDate.AutoSize = true;
			this.LbDate.Location = new System.Drawing.Point(27, 7);
			this.LbDate.Name = "LbDate";
			this.LbDate.Size = new System.Drawing.Size(30, 13);
			this.LbDate.TabIndex = 0;
			this.LbDate.Text = "Date";
			this.LbDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TbSortie
			// 
			this.TbSortie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbSortie.Location = new System.Drawing.Point(212, 4);
			this.TbSortie.Name = "TbSortie";
			this.TbSortie.Size = new System.Drawing.Size(827, 20);
			this.TbSortie.TabIndex = 4;
			// 
			// UcBriefingSituation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TbSortie);
			this.Controls.Add(this.LbDate);
			this.Controls.Add(this.DtpDate);
			this.Controls.Add(this.TbDescription);
			this.Controls.Add(this.TbWeather);
			this.Name = "UcBriefingSituation";
			this.Size = new System.Drawing.Size(1042, 685);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker DtpDate;
		private System.Windows.Forms.TextBox TbDescription;
		private System.Windows.Forms.TextBox TbWeather;
		private System.Windows.Forms.Label LbDate;
		private System.Windows.Forms.TextBox TbSortie;
	}
}
