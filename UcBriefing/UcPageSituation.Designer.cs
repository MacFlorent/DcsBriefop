
namespace DcsBriefop.UcBriefing
{
	partial class UcPageSituation
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
			this.LbSituation = new System.Windows.Forms.Label();
			this.TbSituation = new System.Windows.Forms.TextBox();
			this.TbTask = new System.Windows.Forms.TextBox();
			this.LbTask = new System.Windows.Forms.Label();
			this.TbWeather = new System.Windows.Forms.TextBox();
			this.LbWeather = new System.Windows.Forms.Label();
			this.LbDate = new System.Windows.Forms.Label();
			this.DtpDate = new System.Windows.Forms.DateTimePicker();
			this.SuspendLayout();
			// 
			// LbSituation
			// 
			this.LbSituation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbSituation.Location = new System.Drawing.Point(6, 31);
			this.LbSituation.Name = "LbSituation";
			this.LbSituation.Size = new System.Drawing.Size(662, 24);
			this.LbSituation.TabIndex = 0;
			this.LbSituation.Text = "Situation";
			// 
			// TbSituation
			// 
			this.TbSituation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbSituation.Location = new System.Drawing.Point(9, 58);
			this.TbSituation.Multiline = true;
			this.TbSituation.Name = "TbSituation";
			this.TbSituation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbSituation.Size = new System.Drawing.Size(659, 128);
			this.TbSituation.TabIndex = 1;
			this.TbSituation.Validated += new System.EventHandler(this.TbSituation_Validated);
			// 
			// TbTask
			// 
			this.TbTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbTask.Location = new System.Drawing.Point(9, 222);
			this.TbTask.Multiline = true;
			this.TbTask.Name = "TbTask";
			this.TbTask.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbTask.Size = new System.Drawing.Size(659, 128);
			this.TbTask.TabIndex = 3;
			this.TbTask.Validated += new System.EventHandler(this.TbTask_Validated);
			// 
			// LbTask
			// 
			this.LbTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbTask.Location = new System.Drawing.Point(6, 195);
			this.LbTask.Name = "LbTask";
			this.LbTask.Size = new System.Drawing.Size(662, 24);
			this.LbTask.TabIndex = 2;
			this.LbTask.Text = "Ops";
			// 
			// TbWeather
			// 
			this.TbWeather.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbWeather.Location = new System.Drawing.Point(9, 379);
			this.TbWeather.Multiline = true;
			this.TbWeather.Name = "TbWeather";
			this.TbWeather.ReadOnly = true;
			this.TbWeather.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbWeather.Size = new System.Drawing.Size(659, 128);
			this.TbWeather.TabIndex = 5;
			// 
			// LbWeather
			// 
			this.LbWeather.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbWeather.Location = new System.Drawing.Point(6, 352);
			this.LbWeather.Name = "LbWeather";
			this.LbWeather.Size = new System.Drawing.Size(662, 24);
			this.LbWeather.TabIndex = 4;
			this.LbWeather.Text = "Weather";
			// 
			// LbDate
			// 
			this.LbDate.Location = new System.Drawing.Point(3, 4);
			this.LbDate.Name = "LbDate";
			this.LbDate.Size = new System.Drawing.Size(106, 24);
			this.LbDate.TabIndex = 6;
			this.LbDate.Text = "Date";
			// 
			// DtpDate
			// 
			this.DtpDate.CustomFormat = "dd/MM/yyyy HH:mm";
			this.DtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
			this.DtpDate.Location = new System.Drawing.Point(85, 3);
			this.DtpDate.Name = "DtpDate";
			this.DtpDate.Size = new System.Drawing.Size(200, 20);
			this.DtpDate.TabIndex = 8;
			this.DtpDate.Validated += new System.EventHandler(this.DtpDate_Validated);
			// 
			// UcSituation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.DtpDate);
			this.Controls.Add(this.LbDate);
			this.Controls.Add(this.TbWeather);
			this.Controls.Add(this.LbWeather);
			this.Controls.Add(this.TbTask);
			this.Controls.Add(this.LbTask);
			this.Controls.Add(this.TbSituation);
			this.Controls.Add(this.LbSituation);
			this.Name = "UcSituation";
			this.Size = new System.Drawing.Size(668, 663);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label LbSituation;
		private System.Windows.Forms.TextBox TbSituation;
		private System.Windows.Forms.TextBox TbTask;
		private System.Windows.Forms.Label LbTask;
		private System.Windows.Forms.TextBox TbWeather;
		private System.Windows.Forms.Label LbWeather;
		private System.Windows.Forms.Label LbDate;
		private System.Windows.Forms.DateTimePicker DtpDate;
	}
}
