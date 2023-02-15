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
			this.LbSortie.Location = new System.Drawing.Point(14, 10);
			this.LbSortie.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbSortie.Name = "LbSortie";
			this.LbSortie.Size = new System.Drawing.Size(37, 15);
			this.LbSortie.TabIndex = 0;
			this.LbSortie.Text = "Sortie";
			// 
			// LbDate
			// 
			this.LbDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.LbDate.AutoSize = true;
			this.LbDate.Location = new System.Drawing.Point(760, 10);
			this.LbDate.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbDate.Name = "LbDate";
			this.LbDate.Size = new System.Drawing.Size(31, 15);
			this.LbDate.TabIndex = 1;
			this.LbDate.Text = "Date";
			// 
			// LbWeather
			// 
			this.LbWeather.AutoSize = true;
			this.LbWeather.Location = new System.Drawing.Point(15, 67);
			this.LbWeather.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbWeather.Name = "LbWeather";
			this.LbWeather.Size = new System.Drawing.Size(51, 15);
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
			this.TcDetails.Location = new System.Drawing.Point(14, 152);
			this.TcDetails.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TcDetails.Name = "TcDetails";
			this.TcDetails.SelectedIndex = 0;
			this.TcDetails.Size = new System.Drawing.Size(1032, 563);
			this.TcDetails.TabIndex = 3;
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 24);
			this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tabPage1.Size = new System.Drawing.Size(1024, 535);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 24);
			this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.tabPage2.Size = new System.Drawing.Size(1024, 535);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// TbSortie
			// 
			this.TbSortie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbSortie.Location = new System.Drawing.Point(122, 7);
			this.TbSortie.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TbSortie.Name = "TbSortie";
			this.TbSortie.Size = new System.Drawing.Size(615, 23);
			this.TbSortie.TabIndex = 4;
			// 
			// TbWeather
			// 
			this.TbWeather.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbWeather.Location = new System.Drawing.Point(122, 67);
			this.TbWeather.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.TbWeather.Multiline = true;
			this.TbWeather.Name = "TbWeather";
			this.TbWeather.ReadOnly = true;
			this.TbWeather.Size = new System.Drawing.Size(923, 78);
			this.TbWeather.TabIndex = 6;
			// 
			// DtpDate
			// 
			this.DtpDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DtpDate.Location = new System.Drawing.Point(813, 8);
			this.DtpDate.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.DtpDate.Name = "DtpDate";
			this.DtpDate.Size = new System.Drawing.Size(233, 23);
			this.DtpDate.TabIndex = 7;
			// 
			// BtClose
			// 
			this.BtClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtClose.Location = new System.Drawing.Point(951, 722);
			this.BtClose.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.BtClose.Name = "BtClose";
			this.BtClose.Size = new System.Drawing.Size(94, 27);
			this.BtClose.TabIndex = 9;
			this.BtClose.Text = "Close";
			this.BtClose.UseVisualStyleBackColor = true;
			this.BtClose.Click += new System.EventHandler(this.BtClose_Click);
			// 
			// CbWeatherDisplay
			// 
			this.CbWeatherDisplay.FormattingEnabled = true;
			this.CbWeatherDisplay.Location = new System.Drawing.Point(122, 36);
			this.CbWeatherDisplay.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CbWeatherDisplay.Name = "CbWeatherDisplay";
			this.CbWeatherDisplay.Size = new System.Drawing.Size(229, 23);
			this.CbWeatherDisplay.TabIndex = 10;
			// 
			// LbWeatherDisplay
			// 
			this.LbWeatherDisplay.AutoSize = true;
			this.LbWeatherDisplay.Location = new System.Drawing.Point(14, 39);
			this.LbWeatherDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbWeatherDisplay.Name = "LbWeatherDisplay";
			this.LbWeatherDisplay.Size = new System.Drawing.Size(91, 15);
			this.LbWeatherDisplay.TabIndex = 11;
			this.LbWeatherDisplay.Text = "Weather display";
			// 
			// LbCoodinateDisplay
			// 
			this.LbCoodinateDisplay.AutoSize = true;
			this.LbCoodinateDisplay.Location = new System.Drawing.Point(424, 39);
			this.LbCoodinateDisplay.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.LbCoodinateDisplay.Name = "LbCoodinateDisplay";
			this.LbCoodinateDisplay.Size = new System.Drawing.Size(106, 15);
			this.LbCoodinateDisplay.TabIndex = 25;
			this.LbCoodinateDisplay.Text = "Coordinate display";
			// 
			// CkCoordinateDisplayDms
			// 
			this.CkCoordinateDisplayDms.AutoSize = true;
			this.CkCoordinateDisplayDms.Location = new System.Drawing.Point(539, 38);
			this.CkCoordinateDisplayDms.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CkCoordinateDisplayDms.Name = "CkCoordinateDisplayDms";
			this.CkCoordinateDisplayDms.Size = new System.Drawing.Size(51, 19);
			this.CkCoordinateDisplayDms.TabIndex = 26;
			this.CkCoordinateDisplayDms.Text = "DMS";
			this.CkCoordinateDisplayDms.UseVisualStyleBackColor = true;
			// 
			// CkCoordinateDisplayDdm
			// 
			this.CkCoordinateDisplayDdm.AutoSize = true;
			this.CkCoordinateDisplayDdm.Location = new System.Drawing.Point(604, 38);
			this.CkCoordinateDisplayDdm.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CkCoordinateDisplayDdm.Name = "CkCoordinateDisplayDdm";
			this.CkCoordinateDisplayDdm.Size = new System.Drawing.Size(53, 19);
			this.CkCoordinateDisplayDdm.TabIndex = 27;
			this.CkCoordinateDisplayDdm.Text = "DDM";
			this.CkCoordinateDisplayDdm.UseVisualStyleBackColor = true;
			// 
			// CkCoordinateDisplayMgrs
			// 
			this.CkCoordinateDisplayMgrs.AutoSize = true;
			this.CkCoordinateDisplayMgrs.Location = new System.Drawing.Point(671, 38);
			this.CkCoordinateDisplayMgrs.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
			this.CkCoordinateDisplayMgrs.Name = "CkCoordinateDisplayMgrs";
			this.CkCoordinateDisplayMgrs.Size = new System.Drawing.Size(58, 19);
			this.CkCoordinateDisplayMgrs.TabIndex = 28;
			this.CkCoordinateDisplayMgrs.Text = "MGRS";
			this.CkCoordinateDisplayMgrs.UseVisualStyleBackColor = true;
			// 
			// FrmMissionInformations
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1060, 752);
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
			this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
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