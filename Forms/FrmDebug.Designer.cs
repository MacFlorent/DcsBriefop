namespace DcsBriefop.Forms
{
	partial class FrmDebug
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
			GbSpeeds = new GroupBox();
			NudAltitude = new NumericUpDown();
			LbAltitude = new Label();
			NudKias = new NumericUpDown();
			BtConvertSpeed = new Button();
			LbKias = new Label();
			NudKtas = new NumericUpDown();
			LbKtas = new Label();
			GbSpeeds.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)NudAltitude).BeginInit();
			((System.ComponentModel.ISupportInitialize)NudKias).BeginInit();
			((System.ComponentModel.ISupportInitialize)NudKtas).BeginInit();
			SuspendLayout();
			// 
			// GbSpeeds
			// 
			GbSpeeds.Controls.Add(NudAltitude);
			GbSpeeds.Controls.Add(LbAltitude);
			GbSpeeds.Controls.Add(NudKias);
			GbSpeeds.Controls.Add(BtConvertSpeed);
			GbSpeeds.Controls.Add(LbKias);
			GbSpeeds.Controls.Add(NudKtas);
			GbSpeeds.Controls.Add(LbKtas);
			GbSpeeds.Location = new Point(12, 12);
			GbSpeeds.Name = "GbSpeeds";
			GbSpeeds.Size = new Size(776, 139);
			GbSpeeds.TabIndex = 0;
			GbSpeeds.TabStop = false;
			GbSpeeds.Text = "Speed conversion";
			// 
			// NudAltitude
			// 
			NudAltitude.Location = new Point(85, 54);
			NudAltitude.Maximum = new decimal(new int[] { 100000, 0, 0, 0 });
			NudAltitude.Name = "NudAltitude";
			NudAltitude.Size = new Size(120, 23);
			NudAltitude.TabIndex = 6;
			// 
			// LbAltitude
			// 
			LbAltitude.AutoSize = true;
			LbAltitude.Location = new Point(6, 58);
			LbAltitude.Name = "LbAltitude";
			LbAltitude.Size = new Size(71, 15);
			LbAltitude.TabIndex = 5;
			LbAltitude.Text = "Altitude (m)";
			// 
			// NudKias
			// 
			NudKias.Location = new Point(352, 39);
			NudKias.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
			NudKias.Name = "NudKias";
			NudKias.ReadOnly = true;
			NudKias.Size = new Size(120, 23);
			NudKias.TabIndex = 4;
			// 
			// BtConvertSpeed
			// 
			BtConvertSpeed.Location = new Point(227, 39);
			BtConvertSpeed.Name = "BtConvertSpeed";
			BtConvertSpeed.Size = new Size(75, 23);
			BtConvertSpeed.TabIndex = 3;
			BtConvertSpeed.Text = "Convert";
			BtConvertSpeed.UseVisualStyleBackColor = true;
			BtConvertSpeed.Click += BtConvertSpeed_Click;
			// 
			// LbKias
			// 
			LbKias.AutoSize = true;
			LbKias.Location = new Point(315, 41);
			LbKias.Name = "LbKias";
			LbKias.Size = new Size(31, 15);
			LbKias.TabIndex = 2;
			LbKias.Text = "KIAS";
			// 
			// NudKtas
			// 
			NudKtas.Location = new Point(85, 25);
			NudKtas.Maximum = new decimal(new int[] { 10000, 0, 0, 0 });
			NudKtas.Name = "NudKtas";
			NudKtas.Size = new Size(120, 23);
			NudKtas.TabIndex = 1;
			// 
			// LbKtas
			// 
			LbKtas.AutoSize = true;
			LbKtas.Location = new Point(6, 29);
			LbKtas.Name = "LbKtas";
			LbKtas.Size = new Size(33, 15);
			LbKtas.TabIndex = 0;
			LbKtas.Text = "KTAS";
			// 
			// FrmDebug
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(GbSpeeds);
			Name = "FrmDebug";
			ShowIcon = false;
			Text = "DEBUG";
			Shown += FrmDebug_Shown;
			GbSpeeds.ResumeLayout(false);
			GbSpeeds.PerformLayout();
			((System.ComponentModel.ISupportInitialize)NudAltitude).EndInit();
			((System.ComponentModel.ISupportInitialize)NudKias).EndInit();
			((System.ComponentModel.ISupportInitialize)NudKtas).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private GroupBox GbSpeeds;
		private NumericUpDown NudKias;
		private Button BtConvertSpeed;
		private Label LbKias;
		private NumericUpDown NudKtas;
		private Label LbKtas;
		private NumericUpDown NudAltitude;
		private Label LbAltitude;
	}
}