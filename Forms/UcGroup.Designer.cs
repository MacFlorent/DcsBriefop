namespace DcsBriefop.Forms
{
	partial class UcGroup
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
			this.LbCoalition = new System.Windows.Forms.Label();
			this.LbClass = new System.Windows.Forms.Label();
			this.MapControl = new GMap.NET.WindowsForms.GMapControl();
			this.TcDetails = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.LbDisplayName = new System.Windows.Forms.Label();
			this.TcDetails.SuspendLayout();
			this.SuspendLayout();
			// 
			// LbCoalition
			// 
			this.LbCoalition.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbCoalition.Location = new System.Drawing.Point(0, 0);
			this.LbCoalition.Name = "LbCoalition";
			this.LbCoalition.Size = new System.Drawing.Size(970, 27);
			this.LbCoalition.TabIndex = 26;
			this.LbCoalition.Text = "Coalition";
			this.LbCoalition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LbClass
			// 
			this.LbClass.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbClass.Location = new System.Drawing.Point(0, 27);
			this.LbClass.Name = "LbClass";
			this.LbClass.Size = new System.Drawing.Size(970, 27);
			this.LbClass.TabIndex = 39;
			this.LbClass.Text = "Class";
			this.LbClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MapControl
			// 
			this.MapControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MapControl.Bearing = 0F;
			this.MapControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MapControl.CanDragMap = true;
			this.MapControl.EmptyTileColor = System.Drawing.Color.Navy;
			this.MapControl.GrayScaleMode = false;
			this.MapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.MapControl.LevelsKeepInMemory = 5;
			this.MapControl.Location = new System.Drawing.Point(462, 84);
			this.MapControl.MarkersEnabled = true;
			this.MapControl.MaxZoom = 2;
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
			this.MapControl.Size = new System.Drawing.Size(505, 666);
			this.MapControl.TabIndex = 43;
			this.MapControl.Zoom = 0D;
			// 
			// TcDetails
			// 
			this.TcDetails.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.TcDetails.Controls.Add(this.tabPage1);
			this.TcDetails.Controls.Add(this.tabPage2);
			this.TcDetails.Location = new System.Drawing.Point(3, 84);
			this.TcDetails.Name = "TcDetails";
			this.TcDetails.SelectedIndex = 0;
			this.TcDetails.Size = new System.Drawing.Size(453, 666);
			this.TcDetails.TabIndex = 48;
			this.TcDetails.SelectedIndexChanged += new System.EventHandler(this.TcDetails_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(445, 640);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// tabPage2
			// 
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(445, 640);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "tabPage2";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// LbDisplayName
			// 
			this.LbDisplayName.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbDisplayName.Location = new System.Drawing.Point(0, 54);
			this.LbDisplayName.Name = "LbDisplayName";
			this.LbDisplayName.Size = new System.Drawing.Size(970, 27);
			this.LbDisplayName.TabIndex = 49;
			this.LbDisplayName.Text = "DisplayName";
			this.LbDisplayName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// UcGroup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.LbDisplayName);
			this.Controls.Add(this.TcDetails);
			this.Controls.Add(this.MapControl);
			this.Controls.Add(this.LbClass);
			this.Controls.Add(this.LbCoalition);
			this.Name = "UcGroup";
			this.Size = new System.Drawing.Size(970, 753);
			this.TcDetails.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion
		private System.Windows.Forms.Label LbCoalition;
		private System.Windows.Forms.Label LbClass;
		private GMap.NET.WindowsForms.GMapControl MapControl;
		private System.Windows.Forms.TabControl TcDetails;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Label LbDisplayName;
	}
}
