
namespace DcsBriefop
{
	partial class FrmPreferencesMiz
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
			this.BtOk = new System.Windows.Forms.Button();
			this.BtCancel = new System.Windows.Forms.Button();
			this.GbPreferences = new System.Windows.Forms.GroupBox();
			this.CkNoCallsignForPlayableFlights = new System.Windows.Forms.CheckBox();
			this.LbMapProvider = new System.Windows.Forms.Label();
			this.BtMapProvider = new System.Windows.Forms.Button();
			this.CbMapProvider = new System.Windows.Forms.ComboBox();
			this.GbPreferences.SuspendLayout();
			this.SuspendLayout();
			// 
			// BtOk
			// 
			this.BtOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtOk.Location = new System.Drawing.Point(351, 95);
			this.BtOk.Name = "BtOk";
			this.BtOk.Size = new System.Drawing.Size(75, 23);
			this.BtOk.TabIndex = 42;
			this.BtOk.Text = "Ok";
			this.BtOk.UseVisualStyleBackColor = true;
			this.BtOk.Click += new System.EventHandler(this.BtOk_Click);
			// 
			// BtCancel
			// 
			this.BtCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtCancel.Location = new System.Drawing.Point(434, 95);
			this.BtCancel.Name = "BtCancel";
			this.BtCancel.Size = new System.Drawing.Size(75, 23);
			this.BtCancel.TabIndex = 43;
			this.BtCancel.Text = "Cancel";
			this.BtCancel.UseVisualStyleBackColor = true;
			this.BtCancel.Click += new System.EventHandler(this.BtCancel_Click);
			// 
			// GbPreferences
			// 
			this.GbPreferences.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GbPreferences.Controls.Add(this.CkNoCallsignForPlayableFlights);
			this.GbPreferences.Controls.Add(this.LbMapProvider);
			this.GbPreferences.Controls.Add(this.BtMapProvider);
			this.GbPreferences.Controls.Add(this.CbMapProvider);
			this.GbPreferences.Location = new System.Drawing.Point(12, 12);
			this.GbPreferences.Name = "GbPreferences";
			this.GbPreferences.Size = new System.Drawing.Size(497, 72);
			this.GbPreferences.TabIndex = 44;
			this.GbPreferences.TabStop = false;
			this.GbPreferences.Text = "Mission preferences";
			// 
			// CkNoCallsignForPlayableFlights
			// 
			this.CkNoCallsignForPlayableFlights.AutoSize = true;
			this.CkNoCallsignForPlayableFlights.Location = new System.Drawing.Point(24, 45);
			this.CkNoCallsignForPlayableFlights.Name = "CkNoCallsignForPlayableFlights";
			this.CkNoCallsignForPlayableFlights.Size = new System.Drawing.Size(165, 17);
			this.CkNoCallsignForPlayableFlights.TabIndex = 47;
			this.CkNoCallsignForPlayableFlights.Text = "No callsign for playable flights";
			this.CkNoCallsignForPlayableFlights.UseVisualStyleBackColor = true;
			this.CkNoCallsignForPlayableFlights.CheckedChanged += new System.EventHandler(this.CkNoCallsignForPlayableFlights_CheckedChanged);
			// 
			// LbMapProvider
			// 
			this.LbMapProvider.AutoSize = true;
			this.LbMapProvider.Location = new System.Drawing.Point(21, 21);
			this.LbMapProvider.Name = "LbMapProvider";
			this.LbMapProvider.Size = new System.Drawing.Size(105, 13);
			this.LbMapProvider.TabIndex = 24;
			this.LbMapProvider.Text = "Default map provider";
			// 
			// BtMapProvider
			// 
			this.BtMapProvider.Location = new System.Drawing.Point(294, 17);
			this.BtMapProvider.Name = "BtMapProvider";
			this.BtMapProvider.Size = new System.Drawing.Size(133, 23);
			this.BtMapProvider.TabIndex = 25;
			this.BtMapProvider.Text = "Update all existing maps";
			this.BtMapProvider.UseVisualStyleBackColor = true;
			this.BtMapProvider.Click += new System.EventHandler(this.BtMapProvider_Click);
			// 
			// CbMapProvider
			// 
			this.CbMapProvider.FormattingEnabled = true;
			this.CbMapProvider.Location = new System.Drawing.Point(132, 18);
			this.CbMapProvider.Name = "CbMapProvider";
			this.CbMapProvider.Size = new System.Drawing.Size(156, 21);
			this.CbMapProvider.TabIndex = 23;
			// 
			// FrmPreferencesMiz
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(521, 130);
			this.Controls.Add(this.GbPreferences);
			this.Controls.Add(this.BtCancel);
			this.Controls.Add(this.BtOk);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmPreferencesMiz";
			this.Text = "Mission preferences";
			this.GbPreferences.ResumeLayout(false);
			this.GbPreferences.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Button BtOk;
		private System.Windows.Forms.Button BtCancel;
		private System.Windows.Forms.GroupBox GbPreferences;
		private System.Windows.Forms.CheckBox CkNoCallsignForPlayableFlights;
		private System.Windows.Forms.Label LbMapProvider;
		private System.Windows.Forms.Button BtMapProvider;
		private System.Windows.Forms.ComboBox CbMapProvider;
	}
}