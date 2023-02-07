namespace DcsBriefop.Forms
{
	partial class FrmMissionInformations
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
			this.LbSortie = new System.Windows.Forms.Label();
			this.LbDate = new System.Windows.Forms.Label();
			this.LbWeather = new System.Windows.Forms.Label();
			this.TcDetails = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.TbSortie = new System.Windows.Forms.TextBox();
			this.TbWeather = new System.Windows.Forms.TextBox();
			this.DtpDate = new System.Windows.Forms.DateTimePicker();
			this.BtClose = new System.Windows.Forms.Button();
			this.CbWeatherDisplay = new System.Windows.Forms.ComboBox();
			this.LbWeatherDisplay = new System.Windows.Forms.Label();
			this.LbCoodinateDisplay = new System.Windows.Forms.Label();
			this.CkCoordinateDisplayDms = new System.Windows.Forms.CheckBox();
			this.CkCoordinateDisplayDdm = new System.Windows.Forms.CheckBox();
			this.CkCoordinateDisplayMgrs = new System.Windows.Forms.CheckBox();
			this.TcDetails.SuspendLayout();
			this.SuspendLayout();
			// 
			// LbSortie
			// 
			this.LbSortie.AutoSize = true;
			this.LbSortie.Location = new System.Drawing.Point(12, 9);
			this.LbSortie.Name = "LbSortie";
			this.LbSortie.Size = new System.Drawing.Size(34, 13);
			this.LbSortie.TabIndex = 0;
			this.LbSortie.Text = "Sortie";
			// 
			// LbDate
			// 
			this.LbDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LbDate.AutoSize = true;
			this.LbDate.Location = new System.Drawing.Point(651, 9);
			this.LbDate.Name = "LbDate";
			this.LbDate.Size = new System.Drawing.Size(30, 13);
			this.LbDate.TabIndex = 1;
			this.LbDate.Text = "Date";
			// 
			// LbWeather
			// 
			this.LbWeather.AutoSize = true;
			this.LbWeather.Location = new System.Drawing.Point(13, 58);
			this.LbWeather.Name = "LbWeather";
			this.LbWeather.Size = new System.Drawing.Size(48, 13);
			this.LbWeather.TabIndex = 2;
			this.LbWeather.Text = "Weather";
			// 
			// TcDetails
			// 
			this.TcDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TcDetails.Controls.Add(this.tabPage1);
			this.TcDetails.Controls.Add(this.tabPage2);
			this.TcDetails.Location = new System.Drawing.Point(12, 132);
			this.TcDetails.Name = "TcDetails";
			this.TcDetails.SelectedIndex = 0;
			this.TcDetails.Size = new System.Drawing.Size(885, 488);
			this.TcDetails.TabIndex = 3;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(877, 462);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(877, 462);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// TbSortie
			// 
			this.TbSortie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbSortie.Location = new System.Drawing.Point(105, 6);
			this.TbSortie.Name = "TbSortie";
			this.TbSortie.Size = new System.Drawing.Size(528, 20);
			this.TbSortie.TabIndex = 4;
			// 
			// TbWeather
			// 
			this.TbWeather.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbWeather.Location = new System.Drawing.Point(105, 58);
			this.TbWeather.Multiline = true;
			this.TbWeather.Name = "TbWeather";
			this.TbWeather.ReadOnly = true;
			this.TbWeather.Size = new System.Drawing.Size(792, 68);
			this.TbWeather.TabIndex = 6;
			// 
			// DtpDate
			// 
			this.DtpDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DtpDate.Location = new System.Drawing.Point(697, 7);
			this.DtpDate.Name = "DtpDate";
			this.DtpDate.Size = new System.Drawing.Size(200, 20);
			this.DtpDate.TabIndex = 7;
			// 
			// BtClose
			// 
			this.BtClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtClose.Location = new System.Drawing.Point(815, 626);
			this.BtClose.Name = "BtClose";
			this.BtClose.Size = new System.Drawing.Size(75, 23);
			this.BtClose.TabIndex = 9;
			this.BtClose.Text = "Close";
			this.BtClose.UseVisualStyleBackColor = true;
			this.BtClose.Click += new System.EventHandler(this.BtClose_Click);
			// 
			// CbWeatherDisplay
			// 
			this.CbWeatherDisplay.FormattingEnabled = true;
			this.CbWeatherDisplay.Location = new System.Drawing.Point(105, 31);
			this.CbWeatherDisplay.Name = "CbWeatherDisplay";
			this.CbWeatherDisplay.Size = new System.Drawing.Size(197, 21);
			this.CbWeatherDisplay.TabIndex = 10;
			this.CbWeatherDisplay.SelectedValueChanged += new System.EventHandler(this.CbWeatherDisplay_SelectedValueChanged);
			// 
			// LbWeatherDisplay
			// 
			this.LbWeatherDisplay.AutoSize = true;
			this.LbWeatherDisplay.Location = new System.Drawing.Point(12, 34);
			this.LbWeatherDisplay.Name = "LbWeatherDisplay";
			this.LbWeatherDisplay.Size = new System.Drawing.Size(83, 13);
			this.LbWeatherDisplay.TabIndex = 11;
			this.LbWeatherDisplay.Text = "Weather display";
			// 
			// LbCoodinateDisplay
			// 
			this.LbCoodinateDisplay.AutoSize = true;
			this.LbCoodinateDisplay.Location = new System.Drawing.Point(363, 34);
			this.LbCoodinateDisplay.Name = "LbCoodinateDisplay";
			this.LbCoodinateDisplay.Size = new System.Drawing.Size(93, 13);
			this.LbCoodinateDisplay.TabIndex = 25;
			this.LbCoodinateDisplay.Text = "Coordinate display";
			// 
			// CkCoordinateDisplayDms
			// 
			this.CkCoordinateDisplayDms.AutoSize = true;
			this.CkCoordinateDisplayDms.Location = new System.Drawing.Point(462, 33);
			this.CkCoordinateDisplayDms.Name = "CkCoordinateDisplayDms";
			this.CkCoordinateDisplayDms.Size = new System.Drawing.Size(50, 17);
			this.CkCoordinateDisplayDms.TabIndex = 26;
			this.CkCoordinateDisplayDms.Text = "DMS";
			this.CkCoordinateDisplayDms.UseVisualStyleBackColor = true;
			this.CkCoordinateDisplayDms.CheckedChanged += new System.EventHandler(this.CkCoordinateDisplay_CheckedChanged);
			// 
			// CkCoordinateDisplayDdm
			// 
			this.CkCoordinateDisplayDdm.AutoSize = true;
			this.CkCoordinateDisplayDdm.Location = new System.Drawing.Point(518, 33);
			this.CkCoordinateDisplayDdm.Name = "CkCoordinateDisplayDdm";
			this.CkCoordinateDisplayDdm.Size = new System.Drawing.Size(51, 17);
			this.CkCoordinateDisplayDdm.TabIndex = 27;
			this.CkCoordinateDisplayDdm.Text = "DDM";
			this.CkCoordinateDisplayDdm.UseVisualStyleBackColor = true;
			this.CkCoordinateDisplayDdm.CheckedChanged += new System.EventHandler(this.CkCoordinateDisplay_CheckedChanged);
			// 
			// CkCoordinateDisplayMgrs
			// 
			this.CkCoordinateDisplayMgrs.AutoSize = true;
			this.CkCoordinateDisplayMgrs.Location = new System.Drawing.Point(575, 33);
			this.CkCoordinateDisplayMgrs.Name = "CkCoordinateDisplayMgrs";
			this.CkCoordinateDisplayMgrs.Size = new System.Drawing.Size(58, 17);
			this.CkCoordinateDisplayMgrs.TabIndex = 28;
			this.CkCoordinateDisplayMgrs.Text = "MGRS";
			this.CkCoordinateDisplayMgrs.UseVisualStyleBackColor = true;
			this.CkCoordinateDisplayMgrs.CheckedChanged += new System.EventHandler(this.CkCoordinateDisplay_CheckedChanged);
			// 
			// FrmMissionInformations
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(909, 652);
			this.Controls.Add(this.CkCoordinateDisplayMgrs);
			this.Controls.Add(this.CkCoordinateDisplayDdm);
			this.Controls.Add(this.CkCoordinateDisplayDms);
			this.Controls.Add(this.TbWeather);
			this.Controls.Add(this.LbCoodinateDisplay);
			this.Controls.Add(this.LbWeatherDisplay);
			this.Controls.Add(this.CbWeatherDisplay);
			this.Controls.Add(this.BtClose);
			this.Controls.Add(this.DtpDate);
			this.Controls.Add(this.TbSortie);
			this.Controls.Add(this.TcDetails);
			this.Controls.Add(this.LbWeather);
			this.Controls.Add(this.LbDate);
			this.Controls.Add(this.LbSortie);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FrmMissionInformations";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Mission informations";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmMissionInformations_FormClosed);
			this.TcDetails.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label LbSortie;
		private System.Windows.Forms.Label LbDate;
		private System.Windows.Forms.Label LbWeather;
		private System.Windows.Forms.TabControl TcDetails;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TextBox TbSortie;
		private System.Windows.Forms.TextBox TbWeather;
		private System.Windows.Forms.DateTimePicker DtpDate;
		private System.Windows.Forms.Button BtClose;
		private System.Windows.Forms.ComboBox CbWeatherDisplay;
		private System.Windows.Forms.Label LbWeatherDisplay;
		private System.Windows.Forms.Label LbCoodinateDisplay;
		private System.Windows.Forms.CheckBox CkCoordinateDisplayDms;
		private System.Windows.Forms.CheckBox CkCoordinateDisplayDdm;
		private System.Windows.Forms.CheckBox CkCoordinateDisplayMgrs;
	}
}