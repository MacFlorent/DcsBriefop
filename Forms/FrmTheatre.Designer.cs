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
			SuspendLayout();
			// 
			// CbTheatre
			// 
			CbTheatre.DropDownStyle = ComboBoxStyle.DropDownList;
			CbTheatre.FormattingEnabled = true;
			CbTheatre.Location = new Point(71, 10);
			CbTheatre.Name = "CbTheatre";
			CbTheatre.Size = new Size(121, 23);
			CbTheatre.TabIndex = 19;
			CbTheatre.SelectedIndexChanged += CbTheatre_SelectedIndexChanged;
			// 
			// LbTheatre
			// 
			LbTheatre.AutoSize = true;
			LbTheatre.Location = new Point(16, 13);
			LbTheatre.Name = "LbTheatre";
			LbTheatre.Size = new Size(46, 15);
			LbTheatre.TabIndex = 18;
			LbTheatre.Text = "Theatre";
			// 
			// LbProjection
			// 
			LbProjection.AutoSize = true;
			LbProjection.Location = new Point(219, 13);
			LbProjection.Name = "LbProjection";
			LbProjection.Size = new Size(61, 15);
			LbProjection.TabIndex = 20;
			LbProjection.Text = "Projection";
			// 
			// TbProjection
			// 
			TbProjection.Location = new Point(286, 10);
			TbProjection.Name = "TbProjection";
			TbProjection.Size = new Size(502, 23);
			TbProjection.TabIndex = 21;
			// 
			// MapControl
			// 
			MapControl.Bearing = 0F;
			MapControl.CanDragMap = true;
			MapControl.EmptyTileColor = Color.Navy;
			MapControl.GrayScaleMode = false;
			MapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			MapControl.LevelsKeepInMemory = 5;
			MapControl.Location = new Point(12, 72);
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
			MapControl.Size = new Size(776, 366);
			MapControl.TabIndex = 22;
			MapControl.Zoom = 0D;
			// 
			// FrmTheatre
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
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
	}
}