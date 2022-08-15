
namespace DcsBriefop.FormBop
{
	partial class UcBopGeneral
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
			this.LbTitle = new System.Windows.Forms.Label();
			this.LbWeather = new System.Windows.Forms.Label();
			this.LbOperations = new System.Windows.Forms.Label();
			this.RbWeatherDisplayPlain = new System.Windows.Forms.RadioButton();
			this.RbWeatherDisplayMetar = new System.Windows.Forms.RadioButton();
			this.PnWeatherDisplay = new System.Windows.Forms.Panel();
			this.PnWeatherDisplay.SuspendLayout();
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
			this.TbDescription.Location = new System.Drawing.Point(3, 150);
			this.TbDescription.Multiline = true;
			this.TbDescription.Name = "TbDescription";
			this.TbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbDescription.Size = new System.Drawing.Size(1036, 532);
			this.TbDescription.TabIndex = 3;
			// 
			// TbWeather
			// 
			this.TbWeather.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbWeather.Location = new System.Drawing.Point(3, 76);
			this.TbWeather.Multiline = true;
			this.TbWeather.Name = "TbWeather";
			this.TbWeather.ReadOnly = true;
			this.TbWeather.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbWeather.Size = new System.Drawing.Size(1036, 48);
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
			this.TbSortie.Location = new System.Drawing.Point(63, 27);
			this.TbSortie.Name = "TbSortie";
			this.TbSortie.Size = new System.Drawing.Size(976, 20);
			this.TbSortie.TabIndex = 4;
			// 
			// LbTitle
			// 
			this.LbTitle.AutoSize = true;
			this.LbTitle.Location = new System.Drawing.Point(27, 30);
			this.LbTitle.Name = "LbTitle";
			this.LbTitle.Size = new System.Drawing.Size(27, 13);
			this.LbTitle.TabIndex = 5;
			this.LbTitle.Text = "Title";
			this.LbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LbWeather
			// 
			this.LbWeather.AutoSize = true;
			this.LbWeather.Location = new System.Drawing.Point(3, 60);
			this.LbWeather.Name = "LbWeather";
			this.LbWeather.Size = new System.Drawing.Size(48, 13);
			this.LbWeather.TabIndex = 6;
			this.LbWeather.Text = "Weather";
			this.LbWeather.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// LbOperations
			// 
			this.LbOperations.AutoSize = true;
			this.LbOperations.Location = new System.Drawing.Point(3, 134);
			this.LbOperations.Name = "LbOperations";
			this.LbOperations.Size = new System.Drawing.Size(58, 13);
			this.LbOperations.TabIndex = 7;
			this.LbOperations.Text = "Operations";
			this.LbOperations.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// RbWeatherDisplayText
			// 
			this.RbWeatherDisplayPlain.AutoSize = true;
			this.RbWeatherDisplayPlain.Location = new System.Drawing.Point(3, 3);
			this.RbWeatherDisplayPlain.Name = "RbWeatherDisplayText";
			this.RbWeatherDisplayPlain.Size = new System.Drawing.Size(46, 17);
			this.RbWeatherDisplayPlain.TabIndex = 8;
			this.RbWeatherDisplayPlain.TabStop = true;
			this.RbWeatherDisplayPlain.Text = "Text";
			this.RbWeatherDisplayPlain.UseVisualStyleBackColor = true;
			this.RbWeatherDisplayPlain.CheckedChanged += new System.EventHandler(this.RbWeatherDisplay_CheckedChanged);
			// 
			// RbWeatherDisplayMetar
			// 
			this.RbWeatherDisplayMetar.AutoSize = true;
			this.RbWeatherDisplayMetar.Location = new System.Drawing.Point(55, 3);
			this.RbWeatherDisplayMetar.Name = "RbWeatherDisplayMetar";
			this.RbWeatherDisplayMetar.Size = new System.Drawing.Size(63, 17);
			this.RbWeatherDisplayMetar.TabIndex = 9;
			this.RbWeatherDisplayMetar.TabStop = true;
			this.RbWeatherDisplayMetar.Text = "METAR";
			this.RbWeatherDisplayMetar.UseVisualStyleBackColor = true;
			this.RbWeatherDisplayMetar.CheckedChanged += new System.EventHandler(this.RbWeatherDisplay_CheckedChanged);
			// 
			// PnWeatherDisplay
			// 
			this.PnWeatherDisplay.Controls.Add(this.RbWeatherDisplayPlain);
			this.PnWeatherDisplay.Controls.Add(this.RbWeatherDisplayMetar);
			this.PnWeatherDisplay.Location = new System.Drawing.Point(91, 55);
			this.PnWeatherDisplay.Name = "PnWeatherDisplay";
			this.PnWeatherDisplay.Size = new System.Drawing.Size(200, 21);
			this.PnWeatherDisplay.TabIndex = 10;
			// 
			// UcBopGeneral
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TbWeather);
			this.Controls.Add(this.PnWeatherDisplay);
			this.Controls.Add(this.LbOperations);
			this.Controls.Add(this.LbWeather);
			this.Controls.Add(this.LbTitle);
			this.Controls.Add(this.TbSortie);
			this.Controls.Add(this.LbDate);
			this.Controls.Add(this.DtpDate);
			this.Controls.Add(this.TbDescription);
			this.Name = "UcBopGeneral";
			this.Size = new System.Drawing.Size(1042, 685);
			this.PnWeatherDisplay.ResumeLayout(false);
			this.PnWeatherDisplay.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DateTimePicker DtpDate;
		private System.Windows.Forms.TextBox TbDescription;
		private System.Windows.Forms.TextBox TbWeather;
		private System.Windows.Forms.Label LbDate;
		private System.Windows.Forms.TextBox TbSortie;
		private System.Windows.Forms.Label LbTitle;
		private System.Windows.Forms.Label LbWeather;
		private System.Windows.Forms.Label LbOperations;
		private System.Windows.Forms.RadioButton RbWeatherDisplayPlain;
		private System.Windows.Forms.RadioButton RbWeatherDisplayMetar;
		private System.Windows.Forms.Panel PnWeatherDisplay;
	}
}
