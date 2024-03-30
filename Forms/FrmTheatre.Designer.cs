namespace DcsBriefop.Forms
{
	partial class FrmTheatre
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
			CbTheatre = new ComboBox();
			LbTheatre = new Label();
			LbProjection = new Label();
			TbProjection = new TextBox();
			MapControl = new GMap.NET.WindowsForms.GMapControl();
			LbMapDataDynamic = new Label();
			TbMapDataStatic = new TextBox();
			BtProjectionApply = new Button();
			BtProjectionReset = new Button();
			SuspendLayout();
			// 
			// CbTheatre
			// 
			CbTheatre.DropDownStyle = ComboBoxStyle.DropDownList;
			CbTheatre.FormattingEnabled = true;
			CbTheatre.Location = new Point(71, 10);
			CbTheatre.Name = "CbTheatre";
			CbTheatre.Size = new Size(157, 23);
			CbTheatre.TabIndex = 1;
			CbTheatre.SelectedIndexChanged += CbTheatre_SelectedIndexChanged;
			// 
			// LbTheatre
			// 
			LbTheatre.AutoSize = true;
			LbTheatre.Location = new Point(16, 13);
			LbTheatre.Name = "LbTheatre";
			LbTheatre.Size = new Size(46, 15);
			LbTheatre.TabIndex = 0;
			LbTheatre.Text = "Theatre";
			// 
			// LbProjection
			// 
			LbProjection.AutoSize = true;
			LbProjection.Location = new Point(4, 42);
			LbProjection.Name = "LbProjection";
			LbProjection.Size = new Size(61, 15);
			LbProjection.TabIndex = 2;
			LbProjection.Text = "Projection";
			// 
			// TbProjection
			// 
			TbProjection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbProjection.Location = new Point(71, 39);
			TbProjection.Name = "TbProjection";
			TbProjection.Size = new Size(881, 23);
			TbProjection.TabIndex = 3;
			// 
			// MapControl
			// 
			MapControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			MapControl.Bearing = 0F;
			MapControl.CanDragMap = true;
			MapControl.EmptyTileColor = Color.Navy;
			MapControl.GrayScaleMode = false;
			MapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			MapControl.LevelsKeepInMemory = 5;
			MapControl.Location = new Point(12, 97);
			MapControl.MarkersEnabled = true;
			MapControl.MaxZoom = 2;
			MapControl.MinZoom = 2;
			MapControl.MouseWheelZoomEnabled = true;
			MapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			MapControl.Name = "MapControl";
			MapControl.NegativeMode = false;
			MapControl.PolygonsEnabled = true;
			MapControl.RetryLoadTile = 0;
			MapControl.RoutesEnabled = true;
			MapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
			MapControl.SelectedAreaFillColor = Color.FromArgb(33, 65, 105, 225);
			MapControl.ShowTileGridLines = false;
			MapControl.Size = new Size(1054, 567);
			MapControl.TabIndex = 9;
			MapControl.Zoom = 0D;
			MapControl.OnMarkerClick += MapControl_OnMarkerClick;
			MapControl.MouseDoubleClick += MapControl_MouseDoubleClick;
			MapControl.MouseMove += MapControl_MouseMove;
			// 
			// LbMapDataDynamic
			// 
			LbMapDataDynamic.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			LbMapDataDynamic.AutoSize = true;
			LbMapDataDynamic.Location = new Point(12, 649);
			LbMapDataDynamic.Name = "LbMapDataDynamic";
			LbMapDataDynamic.Size = new Size(12, 15);
			LbMapDataDynamic.TabIndex = 8;
			LbMapDataDynamic.Text = "-";
			LbMapDataDynamic.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// TbMapDataStatic
			// 
			TbMapDataStatic.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbMapDataStatic.Location = new Point(16, 68);
			TbMapDataStatic.Name = "TbMapDataStatic";
			TbMapDataStatic.ReadOnly = true;
			TbMapDataStatic.Size = new Size(1050, 23);
			TbMapDataStatic.TabIndex = 7;
			// 
			// BtProjectionApply
			// 
			BtProjectionApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			BtProjectionApply.Location = new Point(958, 38);
			BtProjectionApply.Name = "BtProjectionApply";
			BtProjectionApply.Size = new Size(51, 23);
			BtProjectionApply.TabIndex = 4;
			BtProjectionApply.Text = "Apply";
			BtProjectionApply.UseVisualStyleBackColor = true;
			BtProjectionApply.Click += BtProjectionApply_Click;
			// 
			// BtProjectionReset
			// 
			BtProjectionReset.Anchor = AnchorStyles.Top | AnchorStyles.Right;
			BtProjectionReset.Location = new Point(1015, 38);
			BtProjectionReset.Name = "BtProjectionReset";
			BtProjectionReset.Size = new Size(51, 23);
			BtProjectionReset.TabIndex = 5;
			BtProjectionReset.Text = "Reset";
			BtProjectionReset.UseVisualStyleBackColor = true;
			BtProjectionReset.Click += BtProjectionReset_Click;
			// 
			// FrmTheatre
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1078, 676);
			Controls.Add(BtProjectionReset);
			Controls.Add(BtProjectionApply);
			Controls.Add(TbMapDataStatic);
			Controls.Add(LbMapDataDynamic);
			Controls.Add(MapControl);
			Controls.Add(TbProjection);
			Controls.Add(LbProjection);
			Controls.Add(CbTheatre);
			Controls.Add(LbTheatre);
			Name = "FrmTheatre";
			ShowIcon = false;
			Text = "Theatre data";
			Shown += FrmTheatre_Shown;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox CbTheatre;
		private Label LbTheatre;
		private Label LbProjection;
		private TextBox TbProjection;
		private GMap.NET.WindowsForms.GMapControl MapControl;
		private Label LbMapDataDynamic;
		private TextBox TbMapDataStatic;
		private Button BtProjectionApply;
		private Button BtProjectionReset;
	}
}