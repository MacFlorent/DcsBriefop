namespace DcsBriefop.Forms
{
	partial class FrmMissionGroups
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
			this.ScMain = new System.Windows.Forms.SplitContainer();
			this.DgvAssets = new Zuby.ADGV.AdvancedDataGridView();
			this.TbId = new System.Windows.Forms.TextBox();
			this.LbId = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textBox3 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.DgvUnits = new Zuby.ADGV.AdvancedDataGridView();
			this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).BeginInit();
			this.ScMain.Panel1.SuspendLayout();
			this.ScMain.Panel2.SuspendLayout();
			this.ScMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgvAssets)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DgvUnits)).BeginInit();
			this.SuspendLayout();
			// 
			// ScMain
			// 
			this.ScMain.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ScMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ScMain.Location = new System.Drawing.Point(0, 0);
			this.ScMain.Name = "ScMain";
			// 
			// ScMain.Panel1
			// 
			this.ScMain.Panel1.Controls.Add(this.DgvAssets);
			// 
			// ScMain.Panel2
			// 
			this.ScMain.Panel2.Controls.Add(this.gMapControl1);
			this.ScMain.Panel2.Controls.Add(this.DgvUnits);
			this.ScMain.Panel2.Controls.Add(this.label6);
			this.ScMain.Panel2.Controls.Add(this.label5);
			this.ScMain.Panel2.Controls.Add(this.label4);
			this.ScMain.Panel2.Controls.Add(this.textBox3);
			this.ScMain.Panel2.Controls.Add(this.label3);
			this.ScMain.Panel2.Controls.Add(this.checkBox2);
			this.ScMain.Panel2.Controls.Add(this.checkBox1);
			this.ScMain.Panel2.Controls.Add(this.label2);
			this.ScMain.Panel2.Controls.Add(this.textBox2);
			this.ScMain.Panel2.Controls.Add(this.label1);
			this.ScMain.Panel2.Controls.Add(this.textBox1);
			this.ScMain.Panel2.Controls.Add(this.LbId);
			this.ScMain.Panel2.Controls.Add(this.TbId);
			this.ScMain.Size = new System.Drawing.Size(1530, 792);
			this.ScMain.SplitterDistance = 807;
			this.ScMain.TabIndex = 0;
			// 
			// DgvAssets
			// 
			this.DgvAssets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvAssets.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DgvAssets.FilterAndSortEnabled = true;
			this.DgvAssets.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvAssets.Location = new System.Drawing.Point(0, 0);
			this.DgvAssets.Name = "DgvAssets";
			this.DgvAssets.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvAssets.Size = new System.Drawing.Size(805, 790);
			this.DgvAssets.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvAssets.TabIndex = 0;
			// 
			// TbId
			// 
			this.TbId.Location = new System.Drawing.Point(159, 61);
			this.TbId.Name = "TbId";
			this.TbId.Size = new System.Drawing.Size(100, 20);
			this.TbId.TabIndex = 0;
			// 
			// LbId
			// 
			this.LbId.AutoSize = true;
			this.LbId.Location = new System.Drawing.Point(110, 67);
			this.LbId.Name = "LbId";
			this.LbId.Size = new System.Drawing.Size(18, 13);
			this.LbId.TabIndex = 1;
			this.LbId.Text = "ID";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(110, 93);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(35, 13);
			this.label1.TabIndex = 3;
			this.label1.Text = "Name";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(159, 87);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 2;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(110, 119);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(31, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Type";
			// 
			// textBox2
			// 
			this.textBox2.Location = new System.Drawing.Point(159, 113);
			this.textBox2.Name = "textBox2";
			this.textBox2.Size = new System.Drawing.Size(100, 20);
			this.textBox2.TabIndex = 4;
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Location = new System.Drawing.Point(367, 63);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(66, 17);
			this.checkBox1.TabIndex = 6;
			this.checkBox1.Text = "Playable";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Location = new System.Drawing.Point(265, 63);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(96, 17);
			this.checkBox2.TabIndex = 7;
			this.checkBox2.Text = "Late activation";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.Dock = System.Windows.Forms.DockStyle.Top;
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(717, 27);
			this.label3.TabIndex = 8;
			this.label3.Text = "Coalition";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(110, 36);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 13);
			this.label4.TabIndex = 10;
			this.label4.Text = "Class";
			// 
			// textBox3
			// 
			this.textBox3.Location = new System.Drawing.Point(159, 30);
			this.textBox3.Name = "textBox3";
			this.textBox3.Size = new System.Drawing.Size(100, 20);
			this.textBox3.TabIndex = 9;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(110, 190);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(31, 13);
			this.label5.TabIndex = 11;
			this.label5.Text = "Task";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(110, 144);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(35, 13);
			this.label6.TabIndex = 12;
			this.label6.Text = "Radio";
			// 
			// DgvUnits
			// 
			this.DgvUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvUnits.FilterAndSortEnabled = true;
			this.DgvUnits.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvUnits.Location = new System.Drawing.Point(3, 598);
			this.DgvUnits.Name = "DgvUnits";
			this.DgvUnits.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvUnits.Size = new System.Drawing.Size(711, 189);
			this.DgvUnits.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvUnits.TabIndex = 13;
			// 
			// gMapControl1
			// 
			this.gMapControl1.Bearing = 0F;
			this.gMapControl1.CanDragMap = true;
			this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
			this.gMapControl1.GrayScaleMode = false;
			this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.gMapControl1.LevelsKeepInMemory = 5;
			this.gMapControl1.Location = new System.Drawing.Point(3, 307);
			this.gMapControl1.MarkersEnabled = true;
			this.gMapControl1.MaxZoom = 2;
			this.gMapControl1.MinZoom = 2;
			this.gMapControl1.MouseWheelZoomEnabled = true;
			this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			this.gMapControl1.Name = "gMapControl1";
			this.gMapControl1.NegativeMode = false;
			this.gMapControl1.PolygonsEnabled = true;
			this.gMapControl1.RetryLoadTile = 0;
			this.gMapControl1.RoutesEnabled = true;
			this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
			this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
			this.gMapControl1.ShowTileGridLines = false;
			this.gMapControl1.Size = new System.Drawing.Size(711, 285);
			this.gMapControl1.TabIndex = 14;
			this.gMapControl1.Zoom = 0D;
			// 
			// FrmMissionGroups
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1530, 792);
			this.Controls.Add(this.ScMain);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FrmMissionGroups";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "FrmMissionAssets";
			this.ScMain.Panel1.ResumeLayout(false);
			this.ScMain.Panel2.ResumeLayout(false);
			this.ScMain.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.ScMain)).EndInit();
			this.ScMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DgvAssets)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DgvUnits)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.SplitContainer ScMain;
		private Zuby.ADGV.AdvancedDataGridView DgvAssets;
		private System.Windows.Forms.TextBox TbId;
		private System.Windows.Forms.Label LbId;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.CheckBox checkBox2;
		private GMap.NET.WindowsForms.GMapControl gMapControl1;
		private Zuby.ADGV.AdvancedDataGridView DgvUnits;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox textBox3;
		private System.Windows.Forms.Label label3;
	}
}