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
			GbCoordinates = new GroupBox();
			BtProjBriefopReset = new Button();
			BtProjDcsReset = new Button();
			TbProjectionBriefop = new TextBox();
			label7 = new Label();
			label6 = new Label();
			label5 = new Label();
			TbProjectionDcs = new TextBox();
			LbProjection = new Label();
			LbControlCoord = new Label();
			CbTheatre = new ComboBox();
			label2 = new Label();
			BtConvetGeoToDcs = new Button();
			NudLong = new NumericUpDown();
			label3 = new Label();
			NudLat = new NumericUpDown();
			label4 = new Label();
			NudY = new NumericUpDown();
			label1 = new Label();
			NudX = new NumericUpDown();
			LbX = new Label();
			BtConvertDcsToGeo = new Button();
			GbSpeeds.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)NudAltitude).BeginInit();
			((System.ComponentModel.ISupportInitialize)NudKias).BeginInit();
			((System.ComponentModel.ISupportInitialize)NudKtas).BeginInit();
			GbCoordinates.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)NudLong).BeginInit();
			((System.ComponentModel.ISupportInitialize)NudLat).BeginInit();
			((System.ComponentModel.ISupportInitialize)NudY).BeginInit();
			((System.ComponentModel.ISupportInitialize)NudX).BeginInit();
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
			GbSpeeds.Size = new Size(776, 88);
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
			// GbCoordinates
			// 
			GbCoordinates.Controls.Add(BtProjBriefopReset);
			GbCoordinates.Controls.Add(BtProjDcsReset);
			GbCoordinates.Controls.Add(TbProjectionBriefop);
			GbCoordinates.Controls.Add(label7);
			GbCoordinates.Controls.Add(label6);
			GbCoordinates.Controls.Add(label5);
			GbCoordinates.Controls.Add(TbProjectionDcs);
			GbCoordinates.Controls.Add(LbProjection);
			GbCoordinates.Controls.Add(LbControlCoord);
			GbCoordinates.Controls.Add(CbTheatre);
			GbCoordinates.Controls.Add(label2);
			GbCoordinates.Controls.Add(BtConvetGeoToDcs);
			GbCoordinates.Controls.Add(NudLong);
			GbCoordinates.Controls.Add(label3);
			GbCoordinates.Controls.Add(NudLat);
			GbCoordinates.Controls.Add(label4);
			GbCoordinates.Controls.Add(NudY);
			GbCoordinates.Controls.Add(label1);
			GbCoordinates.Controls.Add(NudX);
			GbCoordinates.Controls.Add(LbX);
			GbCoordinates.Controls.Add(BtConvertDcsToGeo);
			GbCoordinates.Location = new Point(12, 106);
			GbCoordinates.Name = "GbCoordinates";
			GbCoordinates.Size = new Size(776, 196);
			GbCoordinates.TabIndex = 3;
			GbCoordinates.TabStop = false;
			GbCoordinates.Text = "Coordinates conversion";
			// 
			// BtProjBriefopReset
			// 
			BtProjBriefopReset.Location = new Point(750, 133);
			BtProjBriefopReset.Name = "BtProjBriefopReset";
			BtProjBriefopReset.Size = new Size(20, 23);
			BtProjBriefopReset.TabIndex = 26;
			BtProjBriefopReset.Text = "R";
			BtProjBriefopReset.UseVisualStyleBackColor = true;
			BtProjBriefopReset.Click += BtProjBriefopReset_Click;
			// 
			// BtProjDcsReset
			// 
			BtProjDcsReset.Location = new Point(750, 46);
			BtProjDcsReset.Name = "BtProjDcsReset";
			BtProjDcsReset.Size = new Size(20, 23);
			BtProjDcsReset.TabIndex = 25;
			BtProjDcsReset.Text = "R";
			BtProjDcsReset.UseVisualStyleBackColor = true;
			BtProjDcsReset.Click += BtProjDcsReset_Click;
			// 
			// TbProjectionBriefop
			// 
			TbProjectionBriefop.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbProjectionBriefop.Location = new Point(124, 133);
			TbProjectionBriefop.Name = "TbProjectionBriefop";
			TbProjectionBriefop.Size = new Size(620, 23);
			TbProjectionBriefop.TabIndex = 24;
			// 
			// label7
			// 
			label7.AutoSize = true;
			label7.Location = new Point(57, 136);
			label7.Name = "label7";
			label7.Size = new Size(61, 15);
			label7.TabIndex = 23;
			label7.Text = "Projection";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label6.Location = new Point(6, 107);
			label6.Name = "label6";
			label6.Size = new Size(61, 20);
			label6.TabIndex = 22;
			label6.Text = "Briefop";
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label5.Location = new Point(6, 19);
			label5.Name = "label5";
			label5.Size = new Size(37, 20);
			label5.TabIndex = 21;
			label5.Text = "DCS";
			// 
			// TbProjectionDcs
			// 
			TbProjectionDcs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbProjectionDcs.Location = new Point(124, 47);
			TbProjectionDcs.Name = "TbProjectionDcs";
			TbProjectionDcs.Size = new Size(620, 23);
			TbProjectionDcs.TabIndex = 20;
			// 
			// LbProjection
			// 
			LbProjection.AutoSize = true;
			LbProjection.Location = new Point(57, 50);
			LbProjection.Name = "LbProjection";
			LbProjection.Size = new Size(61, 15);
			LbProjection.TabIndex = 19;
			LbProjection.Text = "Projection";
			// 
			// LbControlCoord
			// 
			LbControlCoord.AutoSize = true;
			LbControlCoord.Location = new Point(529, 178);
			LbControlCoord.Name = "LbControlCoord";
			LbControlCoord.Size = new Size(38, 15);
			LbControlCoord.TabIndex = 18;
			LbControlCoord.Text = "label5";
			// 
			// CbTheatre
			// 
			CbTheatre.DropDownStyle = ComboBoxStyle.DropDownList;
			CbTheatre.FormattingEnabled = true;
			CbTheatre.Location = new Point(124, 18);
			CbTheatre.Name = "CbTheatre";
			CbTheatre.Size = new Size(121, 23);
			CbTheatre.TabIndex = 17;
			CbTheatre.SelectedIndexChanged += CbTheatre_SelectedIndexChanged;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(72, 21);
			label2.Name = "label2";
			label2.Size = new Size(46, 15);
			label2.TabIndex = 16;
			label2.Text = "Theatre";
			// 
			// BtConvetGeoToDcs
			// 
			BtConvetGeoToDcs.Location = new Point(573, 104);
			BtConvetGeoToDcs.Name = "BtConvetGeoToDcs";
			BtConvetGeoToDcs.Size = new Size(75, 23);
			BtConvetGeoToDcs.TabIndex = 15;
			BtConvetGeoToDcs.Text = "< Convert";
			BtConvetGeoToDcs.UseVisualStyleBackColor = true;
			BtConvetGeoToDcs.Click += BtConvetGeoToDcs_Click;
			// 
			// NudLong
			// 
			NudLong.DecimalPlaces = 10;
			NudLong.Location = new Point(447, 104);
			NudLong.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
			NudLong.Minimum = new decimal(new int[] { 999, 0, 0, int.MinValue });
			NudLong.Name = "NudLong";
			NudLong.Size = new Size(120, 23);
			NudLong.TabIndex = 14;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(403, 106);
			label3.Name = "label3";
			label3.Size = new Size(34, 15);
			label3.TabIndex = 13;
			label3.Text = "Long";
			// 
			// NudLat
			// 
			NudLat.DecimalPlaces = 10;
			NudLat.Location = new Point(284, 104);
			NudLat.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
			NudLat.Minimum = new decimal(new int[] { 999, 0, 0, int.MinValue });
			NudLat.Name = "NudLat";
			NudLat.Size = new Size(120, 23);
			NudLat.TabIndex = 12;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(240, 108);
			label4.Name = "label4";
			label4.Size = new Size(23, 15);
			label4.TabIndex = 11;
			label4.Text = "Lat";
			// 
			// NudY
			// 
			NudY.DecimalPlaces = 4;
			NudY.Location = new Point(447, 19);
			NudY.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
			NudY.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
			NudY.Name = "NudY";
			NudY.Size = new Size(120, 23);
			NudY.TabIndex = 10;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(412, 21);
			label1.Name = "label1";
			label1.Size = new Size(29, 15);
			label1.TabIndex = 9;
			label1.Text = "Y(Z)";
			// 
			// NudX
			// 
			NudX.DecimalPlaces = 4;
			NudX.Location = new Point(284, 19);
			NudX.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
			NudX.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
			NudX.Name = "NudX";
			NudX.Size = new Size(120, 23);
			NudX.TabIndex = 8;
			// 
			// LbX
			// 
			LbX.AutoSize = true;
			LbX.Location = new Point(249, 21);
			LbX.Name = "LbX";
			LbX.Size = new Size(14, 15);
			LbX.TabIndex = 7;
			LbX.Text = "X";
			// 
			// BtConvertDcsToGeo
			// 
			BtConvertDcsToGeo.Location = new Point(573, 19);
			BtConvertDcsToGeo.Name = "BtConvertDcsToGeo";
			BtConvertDcsToGeo.Size = new Size(75, 23);
			BtConvertDcsToGeo.TabIndex = 4;
			BtConvertDcsToGeo.Text = "Convert >";
			BtConvertDcsToGeo.UseVisualStyleBackColor = true;
			BtConvertDcsToGeo.Click += BtConvertDcsToGeo_Click;
			// 
			// FrmDebug
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 434);
			Controls.Add(GbCoordinates);
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
			GbCoordinates.ResumeLayout(false);
			GbCoordinates.PerformLayout();
			((System.ComponentModel.ISupportInitialize)NudLong).EndInit();
			((System.ComponentModel.ISupportInitialize)NudLat).EndInit();
			((System.ComponentModel.ISupportInitialize)NudY).EndInit();
			((System.ComponentModel.ISupportInitialize)NudX).EndInit();
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
		private GroupBox GbCoordinates;
		private Button BtConvertDcsToGeo;
		private NumericUpDown NudLong;
		private Label label3;
		private NumericUpDown NudLat;
		private Label label4;
		private NumericUpDown NudY;
		private Label label1;
		private NumericUpDown NudX;
		private Label LbX;
		private Button BtConvetGeoToDcs;
		private ComboBox CbTheatre;
		private Label label2;
		private Label LbControlCoord;
		private TextBox TbProjectionDcs;
		private Label LbProjection;
		private Label label5;
		private TextBox TbProjectionBriefop;
		private Label label7;
		private Label label6;
		private Button BtProjBriefopReset;
		private Button BtProjDcsReset;
	}
}