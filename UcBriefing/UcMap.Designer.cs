
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
			this.PnMapControls = new System.Windows.Forms.Panel();
			this.BtZoomIn = new System.Windows.Forms.Button();
			this.BtZoomOut = new System.Windows.Forms.Button();
			this.LbTitle = new System.Windows.Forms.Label();
			this.BtRefresh = new System.Windows.Forms.Button();
			this.PnSelectionDetail = new System.Windows.Forms.Panel();
			this.CkAddMarker = new System.Windows.Forms.CheckBox();
			this.BtAreaRecall = new System.Windows.Forms.Button();
			this.BtAreaSet = new System.Windows.Forms.Button();
			this.Map = new GMap.NET.WindowsForms.GMapControl();
			this.LbTheater = new System.Windows.Forms.Label();
			this.PnMapControls.SuspendLayout();
			this.SuspendLayout();
			// 
			// PnMapControls
			// 
			this.PnMapControls.Controls.Add(this.LbTheater);
			this.PnMapControls.Controls.Add(this.BtZoomIn);
			this.PnMapControls.Controls.Add(this.BtZoomOut);
			this.PnMapControls.Controls.Add(this.LbTitle);
			this.PnMapControls.Controls.Add(this.BtRefresh);
			this.PnMapControls.Controls.Add(this.PnSelectionDetail);
			this.PnMapControls.Controls.Add(this.CkAddMarker);
			this.PnMapControls.Controls.Add(this.BtAreaRecall);
			this.PnMapControls.Controls.Add(this.BtAreaSet);
			this.PnMapControls.Dock = System.Windows.Forms.DockStyle.Right;
			this.PnMapControls.Location = new System.Drawing.Point(1088, 0);
			this.PnMapControls.Name = "PnMapControls";
			this.PnMapControls.Size = new System.Drawing.Size(235, 929);
			this.PnMapControls.TabIndex = 6;
			// 
			// BtZoomIn
			// 
			this.BtZoomIn.Location = new System.Drawing.Point(103, 148);
			this.BtZoomIn.Name = "BtZoomIn";
			this.BtZoomIn.Size = new System.Drawing.Size(78, 23);
			this.BtZoomIn.TabIndex = 9;
			this.BtZoomIn.Text = "Zoom+";
			this.BtZoomIn.UseVisualStyleBackColor = true;
			this.BtZoomIn.Click += new System.EventHandler(this.BtZoomIn_Click);
			// 
			// BtZoomOut
			// 
			this.BtZoomOut.Location = new System.Drawing.Point(19, 148);
			this.BtZoomOut.Name = "BtZoomOut";
			this.BtZoomOut.Size = new System.Drawing.Size(78, 23);
			this.BtZoomOut.TabIndex = 8;
			this.BtZoomOut.Text = "Zoom-";
			this.BtZoomOut.UseVisualStyleBackColor = true;
			this.BtZoomOut.Click += new System.EventHandler(this.BtZoomOut_Click);
			// 
			// LbTitle
			// 
			this.LbTitle.Location = new System.Drawing.Point(3, 23);
			this.LbTitle.Name = "LbTitle";
			this.LbTitle.Size = new System.Drawing.Size(226, 23);
			this.LbTitle.TabIndex = 7;
			this.LbTitle.Text = "title";
			this.LbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// BtRefresh
			// 
			this.BtRefresh.Location = new System.Drawing.Point(38, 119);
			this.BtRefresh.Name = "BtRefresh";
			this.BtRefresh.Size = new System.Drawing.Size(134, 23);
			this.BtRefresh.TabIndex = 6;
			this.BtRefresh.Text = "Refresh overlays";
			this.BtRefresh.UseVisualStyleBackColor = true;
			this.BtRefresh.Click += new System.EventHandler(this.BtRefresh_Click);
			// 
			// PnSelectionDetail
			// 
			this.PnSelectionDetail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnSelectionDetail.BackColor = System.Drawing.SystemColors.ControlDark;
			this.PnSelectionDetail.Location = new System.Drawing.Point(3, 735);
			this.PnSelectionDetail.Name = "PnSelectionDetail";
			this.PnSelectionDetail.Size = new System.Drawing.Size(229, 191);
			this.PnSelectionDetail.TabIndex = 5;
			// 
			// CkAddMarker
			// 
			this.CkAddMarker.AutoSize = true;
			this.CkAddMarker.Location = new System.Drawing.Point(38, 186);
			this.CkAddMarker.Name = "CkAddMarker";
			this.CkAddMarker.Size = new System.Drawing.Size(80, 17);
			this.CkAddMarker.TabIndex = 2;
			this.CkAddMarker.Text = "Add marker";
			this.CkAddMarker.UseVisualStyleBackColor = true;
			// 
			// BtAreaRecall
			// 
			this.BtAreaRecall.Location = new System.Drawing.Point(38, 90);
			this.BtAreaRecall.Name = "BtAreaRecall";
			this.BtAreaRecall.Size = new System.Drawing.Size(134, 23);
			this.BtAreaRecall.TabIndex = 1;
			this.BtAreaRecall.Text = "Recall display area";
			this.BtAreaRecall.UseVisualStyleBackColor = true;
			this.BtAreaRecall.Click += new System.EventHandler(this.BtAreaRecall_Click);
			// 
			// BtAreaSet
			// 
			this.BtAreaSet.Location = new System.Drawing.Point(38, 61);
			this.BtAreaSet.Name = "BtAreaSet";
			this.BtAreaSet.Size = new System.Drawing.Size(134, 23);
			this.BtAreaSet.TabIndex = 0;
			this.BtAreaSet.Text = "Set display area";
			this.BtAreaSet.UseVisualStyleBackColor = true;
			this.BtAreaSet.Click += new System.EventHandler(this.BtAreaSet_Click);
			// 
			// Map
			// 
			this.Map.Bearing = 0F;
			this.Map.CanDragMap = true;
			this.Map.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Map.EmptyTileColor = System.Drawing.Color.Navy;
			this.Map.GrayScaleMode = false;
			this.Map.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.Map.LevelsKeepInMemory = 5;
			this.Map.Location = new System.Drawing.Point(0, 0);
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
			this.Map.Size = new System.Drawing.Size(1088, 929);
			this.Map.TabIndex = 5;
			this.Map.Zoom = 13D;
			this.Map.OnMarkerClick += new GMap.NET.WindowsForms.MarkerClick(this.Map_OnMarkerClick);
			this.Map.OnMarkerEnter += new GMap.NET.WindowsForms.MarkerEnter(this.Map_OnMarkerEnter);
			this.Map.OnMarkerLeave += new GMap.NET.WindowsForms.MarkerLeave(this.Map_OnMarkerLeave);
			this.Map.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Map_KeyUp);
			this.Map.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Map_MouseClick);
			this.Map.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Map_MouseDown);
			this.Map.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Map_MouseMove);
			this.Map.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Map_MouseUp);
			// 
			// LbTheater
			// 
			this.LbTheater.Location = new System.Drawing.Point(3, 0);
			this.LbTheater.Name = "LbTheater";
			this.LbTheater.Size = new System.Drawing.Size(226, 23);
			this.LbTheater.TabIndex = 10;
			this.LbTheater.Text = "title";
			this.LbTheater.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UcMap
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.Map);
			this.Controls.Add(this.PnMapControls);
			this.Name = "UcMap";
			this.Size = new System.Drawing.Size(1323, 929);
			this.PnMapControls.ResumeLayout(false);
			this.PnMapControls.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel PnMapControls;
		private GMap.NET.WindowsForms.GMapControl Map;
		private System.Windows.Forms.Button BtAreaRecall;
		private System.Windows.Forms.Button BtAreaSet;
		private System.Windows.Forms.CheckBox CkAddMarker;
		private System.Windows.Forms.Panel PnSelectionDetail;
		private System.Windows.Forms.Button BtRefresh;
		private System.Windows.Forms.Label LbTitle;
		private System.Windows.Forms.Button BtZoomIn;
		private System.Windows.Forms.Button BtZoomOut;
		private System.Windows.Forms.Label LbTheater;
	}
}
