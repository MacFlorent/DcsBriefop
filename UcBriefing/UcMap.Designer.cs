
namespace DcsBriefop.UcBriefing
{
	partial class UcMap
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.LbHeader = new System.Windows.Forms.Label();
			this.PnMapControls = new System.Windows.Forms.Panel();
			this.Map = new GMap.NET.WindowsForms.GMapControl();
			this.SuspendLayout();
			// 
			// LbHeader
			// 
			this.LbHeader.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LbHeader.Location = new System.Drawing.Point(3, 0);
			this.LbHeader.Name = "LbHeader";
			this.LbHeader.Size = new System.Drawing.Size(1317, 26);
			this.LbHeader.TabIndex = 7;
			this.LbHeader.Text = "Map";
			this.LbHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// PnMapControls
			// 
			this.PnMapControls.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnMapControls.Location = new System.Drawing.Point(1111, 29);
			this.PnMapControls.Name = "PnMapControls";
			this.PnMapControls.Size = new System.Drawing.Size(212, 900);
			this.PnMapControls.TabIndex = 6;
			// 
			// Map
			// 
			this.Map.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.Map.Bearing = 0F;
			this.Map.CanDragMap = true;
			this.Map.EmptyTileColor = System.Drawing.Color.Navy;
			this.Map.GrayScaleMode = false;
			this.Map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.Map.LevelsKeepInMemory = 5;
			this.Map.Location = new System.Drawing.Point(0, 29);
			this.Map.MarkersEnabled = true;
			this.Map.MaxZoom = 18;
			this.Map.MinZoom = 2;
			this.Map.MouseWheelZoomEnabled = true;
			this.Map.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			this.Map.Name = "Map";
			this.Map.NegativeMode = false;
			this.Map.PolygonsEnabled = true;
			this.Map.RetryLoadTile = 0;
			this.Map.RoutesEnabled = true;
			this.Map.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
			this.Map.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
			this.Map.ShowTileGridLines = false;
			this.Map.Size = new System.Drawing.Size(1105, 900);
			this.Map.TabIndex = 5;
			this.Map.Zoom = 13D;
			// 
			// UcMap
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.LbHeader);
			this.Controls.Add(this.PnMapControls);
			this.Controls.Add(this.Map);
			this.Name = "UcMap";
			this.Size = new System.Drawing.Size(1323, 929);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label LbHeader;
		private System.Windows.Forms.Panel PnMapControls;
		private GMap.NET.WindowsForms.GMapControl Map;
	}
}
