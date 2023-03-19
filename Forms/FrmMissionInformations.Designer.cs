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
			LbSortie = new Label();
			LbDate = new Label();
			LbWeather = new Label();
			TcDetails = new TabControl();
			tabPage1 = new TabPage();
			tabPage2 = new TabPage();
			TbSortie = new TextBox();
			TbWeather = new TextBox();
			DtpDate = new DateTimePicker();
			BtClose = new Button();
			TcDetails.SuspendLayout();
			SuspendLayout();
			// 
			// LbSortie
			// 
			LbSortie.AutoSize = true;
			LbSortie.Location = new Point(14, 10);
			LbSortie.Margin = new Padding(4, 0, 4, 0);
			LbSortie.Name = "LbSortie";
			LbSortie.Size = new Size(37, 15);
			LbSortie.TabIndex = 0;
			LbSortie.Text = "Sortie";
			// 
			// LbDate
			// 
			LbDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			LbDate.AutoSize = true;
			LbDate.Location = new Point(760, 10);
			LbDate.Margin = new Padding(4, 0, 4, 0);
			LbDate.Name = "LbDate";
			LbDate.Size = new Size(31, 15);
			LbDate.TabIndex = 1;
			LbDate.Text = "Date";
			// 
			// LbWeather
			// 
			LbWeather.AutoSize = true;
			LbWeather.Location = new Point(14, 40);
			LbWeather.Margin = new Padding(4, 0, 4, 0);
			LbWeather.Name = "LbWeather";
			LbWeather.Size = new Size(51, 15);
			LbWeather.TabIndex = 2;
			LbWeather.Text = "Weather";
			// 
			// TcDetails
			// 
			TcDetails.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			TcDetails.Controls.Add(tabPage1);
			TcDetails.Controls.Add(tabPage2);
			TcDetails.Location = new Point(14, 152);
			TcDetails.Margin = new Padding(4, 3, 4, 3);
			TcDetails.Name = "TcDetails";
			TcDetails.SelectedIndex = 0;
			TcDetails.Size = new Size(1032, 563);
			TcDetails.TabIndex = 3;
			// 
			// tabPage1
			// 
			tabPage1.Location = new Point(4, 24);
			tabPage1.Margin = new Padding(4, 3, 4, 3);
			tabPage1.Name = "tabPage1";
			tabPage1.Padding = new Padding(4, 3, 4, 3);
			tabPage1.Size = new Size(1024, 535);
			tabPage1.TabIndex = 0;
			tabPage1.Text = "tabPage1";
			tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			tabPage2.Location = new Point(4, 24);
			tabPage2.Margin = new Padding(4, 3, 4, 3);
			tabPage2.Name = "tabPage2";
			tabPage2.Padding = new Padding(4, 3, 4, 3);
			tabPage2.Size = new Size(1024, 535);
			tabPage2.TabIndex = 1;
			tabPage2.Text = "tabPage2";
			tabPage2.UseVisualStyleBackColor = true;
			// 
			// TbSortie
			// 
			TbSortie.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbSortie.Location = new Point(73, 7);
			TbSortie.Margin = new Padding(4, 3, 4, 3);
			TbSortie.Name = "TbSortie";
			TbSortie.Size = new Size(664, 23);
			TbSortie.TabIndex = 4;
			// 
			// TbWeather
			// 
			TbWeather.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbWeather.Location = new Point(73, 37);
			TbWeather.Margin = new Padding(4, 3, 4, 3);
			TbWeather.Multiline = true;
			TbWeather.Name = "TbWeather";
			TbWeather.ReadOnly = true;
			TbWeather.Size = new Size(972, 108);
			TbWeather.TabIndex = 6;
			// 
			// DtpDate
			// 
			DtpDate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			DtpDate.Location = new Point(812, 7);
			DtpDate.Margin = new Padding(4, 3, 4, 3);
			DtpDate.Name = "DtpDate";
			DtpDate.Size = new Size(233, 23);
			DtpDate.TabIndex = 7;
			// 
			// BtClose
			// 
			BtClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtClose.Location = new Point(951, 722);
			BtClose.Margin = new Padding(4, 3, 4, 3);
			BtClose.Name = "BtClose";
			BtClose.Size = new Size(94, 27);
			BtClose.TabIndex = 9;
			BtClose.Text = "Close";
			BtClose.UseVisualStyleBackColor = true;
			BtClose.Click += BtClose_Click;
			// 
			// FrmMissionInformations
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1060, 752);
			Controls.Add(TbWeather);
			Controls.Add(BtClose);
			Controls.Add(DtpDate);
			Controls.Add(TbSortie);
			Controls.Add(TcDetails);
			Controls.Add(LbWeather);
			Controls.Add(LbDate);
			Controls.Add(LbSortie);
			Margin = new Padding(4, 3, 4, 3);
			Name = "FrmMissionInformations";
			ShowIcon = false;
			ShowInTaskbar = false;
			StartPosition = FormStartPosition.CenterParent;
			Text = "Mission informations";
			FormClosed += FrmMissionInformations_FormClosed;
			Shown += FrmMissionInformations_Shown;
			TcDetails.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
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
	}
}