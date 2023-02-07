
namespace DcsBriefop.Forms
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
			this.BtRefresh = new System.Windows.Forms.Button();
			this.PnSelectionDetail = new System.Windows.Forms.Panel();
			this.CkAddMarker = new System.Windows.Forms.CheckBox();
			this.BtAreaSet = new System.Windows.Forms.Button();
			this.BtAreaRecall = new System.Windows.Forms.Button();
			this.MapControl = new GMap.NET.WindowsForms.GMapControl();
			this.SuspendLayout();
			// 
			// BtRefresh
			// 
			this.BtRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtRefresh.Location = new System.Drawing.Point(1067, 905);
			this.BtRefresh.Name = "BtRefresh";
			this.BtRefresh.Size = new System.Drawing.Size(134, 23);
			this.BtRefresh.TabIndex = 6;
			this.BtRefresh.Text = "Refresh overlays";
			this.BtRefresh.UseVisualStyleBackColor = true;
			this.BtRefresh.Click += new System.EventHandler(this.BtRefresh_Click);
			// 
			// PnSelectionDetail
			// 
			this.PnSelectionDetail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.PnSelectionDetail.BackColor = System.Drawing.SystemColors.ControlDark;
			this.PnSelectionDetail.Location = new System.Drawing.Point(1036, 712);
			this.PnSelectionDetail.Name = "PnSelectionDetail";
			this.PnSelectionDetail.Size = new System.Drawing.Size(284, 191);
			this.PnSelectionDetail.TabIndex = 5;
			// 
			// CkAddMarker
			// 
			this.CkAddMarker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CkAddMarker.AutoSize = true;
			this.CkAddMarker.Location = new System.Drawing.Point(1221, 909);
			this.CkAddMarker.Name = "CkAddMarker";
			this.CkAddMarker.Size = new System.Drawing.Size(80, 17);
			this.CkAddMarker.TabIndex = 2;
			this.CkAddMarker.Text = "Add marker";
			this.CkAddMarker.UseVisualStyleBackColor = true;
			// 
			// BtAreaSet
			// 
			this.BtAreaSet.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtAreaSet.Location = new System.Drawing.Point(787, 905);
			this.BtAreaSet.Name = "BtAreaSet";
			this.BtAreaSet.Size = new System.Drawing.Size(134, 23);
			this.BtAreaSet.TabIndex = 0;
			this.BtAreaSet.Text = "Set display area";
			this.BtAreaSet.UseVisualStyleBackColor = true;
			this.BtAreaSet.Click += new System.EventHandler(this.BtAreaSet_Click);
			// 
			// BtAreaRecall
			// 
			this.BtAreaRecall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtAreaRecall.Location = new System.Drawing.Point(927, 905);
			this.BtAreaRecall.Name = "BtAreaRecall";
			this.BtAreaRecall.Size = new System.Drawing.Size(134, 23);
			this.BtAreaRecall.TabIndex = 1;
			this.BtAreaRecall.Text = "Recall display area";
			this.BtAreaRecall.UseVisualStyleBackColor = true;
			this.BtAreaRecall.Click += new System.EventHandler(this.BtAreaRecall_Click);
			// 
			// MapControl
			// 
			this.MapControl.Bearing = 0F;
			this.MapControl.CanDragMap = true;
			this.MapControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MapControl.EmptyTileColor = System.Drawing.Color.Navy;
			this.MapControl.GrayScaleMode = false;
			this.MapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.MapControl.LevelsKeepInMemory = 5;
			this.MapControl.Location = new System.Drawing.Point(0, 0);
			this.MapControl.MarkersEnabled = true;
			this.MapControl.MaxZoom = 18;
			this.MapControl.MinZoom = 2;
			this.MapControl.MouseWheelZoomEnabled = true;
			this.MapControl.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			this.MapControl.Name = "MapControl";
			this.MapControl.NegativeMode = false;
			this.MapControl.PolygonsEnabled = true;
			this.MapControl.RetryLoadTile = 0;
			this.MapControl.RoutesEnabled = true;
			this.MapControl.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
			this.MapControl.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
			this.MapControl.ShowTileGridLines = false;
			this.MapControl.Size = new System.Drawing.Size(1323, 929);
			this.MapControl.TabIndex = 5;
			this.MapControl.Zoom = 13D;
			this.MapControl.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.Map_OnMarkerClick);
			this.MapControl.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.Map_OnMarkerEnter);
			this.MapControl.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.Map_OnMarkerLeave);
			this.MapControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Map_KeyUp);
			this.MapControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Map_MouseClick);
			this.MapControl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map_MouseDown);
			this.MapControl.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Map_MouseMove);
			this.MapControl.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Map_MouseUp);
			// 
			// UcMap
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.BtRefresh);
			this.Controls.Add(this.PnSelectionDetail);
			this.Controls.Add(this.CkAddMarker);
			this.Controls.Add(this.BtAreaSet);
			this.Controls.Add(this.BtAreaRecall);
			this.Controls.Add(this.MapControl);
			this.Name = "UcMap";
			this.Size = new System.Drawing.Size(1323, 929);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private GMap.NET.WindowsForms.GMapControl MapControl;
		private System.Windows.Forms.Button BtAreaRecall;
		private System.Windows.Forms.Button BtAreaSet;
		private System.Windows.Forms.CheckBox CkAddMarker;
		private System.Windows.Forms.Panel PnSelectionDetail;
		private System.Windows.Forms.Button BtRefresh;
	}
}
