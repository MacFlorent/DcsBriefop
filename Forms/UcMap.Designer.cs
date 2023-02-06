
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
			this.PnMapControls = new System.Windows.Forms.Panel();
			this.CbMapProvider = new System.Windows.Forms.ComboBox();
			this.LbMapDisplay = new System.Windows.Forms.Label();
			this.LbTitle = new System.Windows.Forms.Label();
			this.BtRefresh = new System.Windows.Forms.Button();
			this.PnSelectionDetail = new System.Windows.Forms.Panel();
			this.CkAddMarker = new System.Windows.Forms.CheckBox();
			this.BtAreaRecall = new System.Windows.Forms.Button();
			this.BtAreaSet = new System.Windows.Forms.Button();
			this.MapControl = new GMap.NET.WindowsForms.GMapControl();
			this.PnMapControls.SuspendLayout();
			this.SuspendLayout();
			// 
			// PnMapControls
			// 
			this.PnMapControls.Controls.Add(this.CbMapProvider);
			this.PnMapControls.Controls.Add(this.LbMapDisplay);
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
			// CbMapProvider
			// 
			this.CbMapProvider.FormattingEnabled = true;
			this.CbMapProvider.Location = new System.Drawing.Point(78, 66);
			this.CbMapProvider.Name = "CbMapProvider";
			this.CbMapProvider.Size = new System.Drawing.Size(143, 21);
			this.CbMapProvider.TabIndex = 21;
			this.CbMapProvider.SelectionChangeCommitted += new System.EventHandler(this.CbMapProvider_SelectionChangeCommitted);
			// 
			// LbMapDisplay
			// 
			this.LbMapDisplay.AutoSize = true;
			this.LbMapDisplay.Location = new System.Drawing.Point(3, 69);
			this.LbMapDisplay.Name = "LbMapDisplay";
			this.LbMapDisplay.Size = new System.Drawing.Size(69, 13);
			this.LbMapDisplay.TabIndex = 22;
			this.LbMapDisplay.Text = "Map provider";
			// 
			// LbTitle
			// 
			this.LbTitle.Location = new System.Drawing.Point(3, 0);
			this.LbTitle.Name = "LbTitle";
			this.LbTitle.Size = new System.Drawing.Size(226, 23);
			this.LbTitle.TabIndex = 7;
			this.LbTitle.Text = "title";
			this.LbTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// BtRefresh
			// 
			this.BtRefresh.Location = new System.Drawing.Point(48, 171);
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
			this.CkAddMarker.Location = new System.Drawing.Point(48, 238);
			this.CkAddMarker.Name = "CkAddMarker";
			this.CkAddMarker.Size = new System.Drawing.Size(80, 17);
			this.CkAddMarker.TabIndex = 2;
			this.CkAddMarker.Text = "Add marker";
			this.CkAddMarker.UseVisualStyleBackColor = true;
			// 
			// BtAreaRecall
			// 
			this.BtAreaRecall.Location = new System.Drawing.Point(48, 142);
			this.BtAreaRecall.Name = "BtAreaRecall";
			this.BtAreaRecall.Size = new System.Drawing.Size(134, 23);
			this.BtAreaRecall.TabIndex = 1;
			this.BtAreaRecall.Text = "Recall display area";
			this.BtAreaRecall.UseVisualStyleBackColor = true;
			this.BtAreaRecall.Click += new System.EventHandler(this.BtAreaRecall_Click);
			// 
			// BtAreaSet
			// 
			this.BtAreaSet.Location = new System.Drawing.Point(48, 113);
			this.BtAreaSet.Name = "BtAreaSet";
			this.BtAreaSet.Size = new System.Drawing.Size(134, 23);
			this.BtAreaSet.TabIndex = 0;
			this.BtAreaSet.Text = "Set display area";
			this.BtAreaSet.UseVisualStyleBackColor = true;
			this.BtAreaSet.Click += new System.EventHandler(this.BtAreaSet_Click);
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
			this.MapControl.Size = new System.Drawing.Size(1088, 929);
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
			this.Controls.Add(this.MapControl);
			this.Controls.Add(this.PnMapControls);
			this.Name = "UcMap";
			this.Size = new System.Drawing.Size(1323, 929);
			this.PnMapControls.ResumeLayout(false);
			this.PnMapControls.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Panel PnMapControls;
		private GMap.NET.WindowsForms.GMapControl MapControl;
		private System.Windows.Forms.Button BtAreaRecall;
		private System.Windows.Forms.Button BtAreaSet;
		private System.Windows.Forms.CheckBox CkAddMarker;
		private System.Windows.Forms.Panel PnSelectionDetail;
		private System.Windows.Forms.Button BtRefresh;
		private System.Windows.Forms.Label LbTitle;
		private System.Windows.Forms.ComboBox CbMapProvider;
		private System.Windows.Forms.Label LbMapDisplay;
	}
}
