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
			advancedDataGridView1 = new Zuby.ADGV.AdvancedDataGridView();
			groupBox1 = new GroupBox();
			GbCoordinates = new GroupBox();
			CbMap = new ComboBox();
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
			((System.ComponentModel.ISupportInitialize)advancedDataGridView1).BeginInit();
			groupBox1.SuspendLayout();
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
			// advancedDataGridView1
			// 
			advancedDataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			advancedDataGridView1.FilterAndSortEnabled = true;
			advancedDataGridView1.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			advancedDataGridView1.Location = new Point(6, 22);
			advancedDataGridView1.Name = "advancedDataGridView1";
			advancedDataGridView1.RightToLeft = RightToLeft.No;
			advancedDataGridView1.RowTemplate.Height = 25;
			advancedDataGridView1.Size = new Size(764, 650);
			advancedDataGridView1.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			advancedDataGridView1.TabIndex = 1;
			// 
			// groupBox1
			// 
			groupBox1.Controls.Add(advancedDataGridView1);
			groupBox1.Location = new Point(12, 254);
			groupBox1.Name = "groupBox1";
			groupBox1.Size = new Size(776, 581);
			groupBox1.TabIndex = 2;
			groupBox1.TabStop = false;
			groupBox1.Text = "groupBox1";
			// 
			// GbCoordinates
			// 
			GbCoordinates.Controls.Add(CbMap);
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
			GbCoordinates.Size = new Size(776, 122);
			GbCoordinates.TabIndex = 3;
			GbCoordinates.TabStop = false;
			GbCoordinates.Text = "Coordinates conversion";
			// 
			// CbMap
			// 
			CbMap.FormattingEnabled = true;
			CbMap.Location = new Point(86, 38);
			CbMap.Name = "CbMap";
			CbMap.Size = new Size(121, 23);
			CbMap.TabIndex = 17;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(25, 39);
			label2.Name = "label2";
			label2.Size = new Size(31, 15);
			label2.TabIndex = 16;
			label2.Text = "Map";
			// 
			// BtConvetGeoToDcs
			// 
			BtConvetGeoToDcs.Location = new Point(438, 72);
			BtConvetGeoToDcs.Name = "BtConvetGeoToDcs";
			BtConvetGeoToDcs.Size = new Size(75, 23);
			BtConvetGeoToDcs.TabIndex = 15;
			BtConvetGeoToDcs.Text = "< Convert";
			BtConvetGeoToDcs.UseVisualStyleBackColor = true;
			// 
			// NudLong
			// 
			NudLong.DecimalPlaces = 8;
			NudLong.Location = new Point(624, 68);
			NudLong.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
			NudLong.Name = "NudLong";
			NudLong.Size = new Size(120, 23);
			NudLong.TabIndex = 14;
			NudLong.ThousandsSeparator = true;
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new Point(580, 70);
			label3.Name = "label3";
			label3.Size = new Size(34, 15);
			label3.TabIndex = 13;
			label3.Text = "Long";
			// 
			// NudLat
			// 
			NudLat.DecimalPlaces = 8;
			NudLat.Location = new Point(624, 39);
			NudLat.Minimum = new decimal(new int[] { 100, 0, 0, int.MinValue });
			NudLat.Name = "NudLat";
			NudLat.Size = new Size(120, 23);
			NudLat.TabIndex = 12;
			NudLat.ThousandsSeparator = true;
			// 
			// label4
			// 
			label4.AutoSize = true;
			label4.Location = new Point(580, 43);
			label4.Name = "label4";
			label4.Size = new Size(23, 15);
			label4.TabIndex = 11;
			label4.Text = "Lat";
			// 
			// NudY
			// 
			NudY.Location = new Point(297, 68);
			NudY.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
			NudY.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
			NudY.Name = "NudY";
			NudY.Size = new Size(120, 23);
			NudY.TabIndex = 10;
			NudY.ThousandsSeparator = true;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(262, 76);
			label1.Name = "label1";
			label1.Size = new Size(14, 15);
			label1.TabIndex = 9;
			label1.Text = "Y";
			// 
			// NudX
			// 
			NudX.Location = new Point(297, 39);
			NudX.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
			NudX.Minimum = new decimal(new int[] { 1000000, 0, 0, int.MinValue });
			NudX.Name = "NudX";
			NudX.Size = new Size(120, 23);
			NudX.TabIndex = 8;
			NudX.ThousandsSeparator = true;
			// 
			// LbX
			// 
			LbX.AutoSize = true;
			LbX.Location = new Point(262, 41);
			LbX.Name = "LbX";
			LbX.Size = new Size(14, 15);
			LbX.TabIndex = 7;
			LbX.Text = "X";
			// 
			// BtConvertDcsToGeo
			// 
			BtConvertDcsToGeo.Location = new Point(438, 43);
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
			ClientSize = new Size(800, 847);
			Controls.Add(GbCoordinates);
			Controls.Add(groupBox1);
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
			((System.ComponentModel.ISupportInitialize)advancedDataGridView1).EndInit();
			groupBox1.ResumeLayout(false);
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
		private Zuby.ADGV.AdvancedDataGridView advancedDataGridView1;
		private GroupBox groupBox1;
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
		private ComboBox CbMap;
		private Label label2;
	}
}