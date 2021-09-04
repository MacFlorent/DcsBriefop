
namespace DcsBriefop
{
	partial class FrmMain
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
			this.StatusStrip = new System.Windows.Forms.StatusStrip();
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.Map = new GMap.NET.WindowsForms.GMapControl();
			this.SplitContainer = new System.Windows.Forms.SplitContainer();
			((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
			this.SplitContainer.Panel2.SuspendLayout();
			this.SplitContainer.SuspendLayout();
			this.SuspendLayout();
			// 
			// StatusStrip
			// 
			this.StatusStrip.Location = new System.Drawing.Point(0, 757);
			this.StatusStrip.Name = "StatusStrip";
			this.StatusStrip.Size = new System.Drawing.Size(1475, 22);
			this.StatusStrip.TabIndex = 0;
			this.StatusStrip.Text = "statusStrip1";
			// 
			// MainMenu
			// 
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(1475, 24);
			this.MainMenu.TabIndex = 1;
			this.MainMenu.Text = "menuStrip1";
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
			this.Map.Size = new System.Drawing.Size(980, 733);
			this.Map.TabIndex = 2;
			this.Map.Zoom = 13D;
			// 
			// SplitContainer
			// 
			this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainer.Location = new System.Drawing.Point(0, 24);
			this.SplitContainer.Name = "SplitContainer";
			// 
			// SplitContainer.Panel1
			// 
			this.SplitContainer.Panel1.BackColor = System.Drawing.SystemColors.Control;
			// 
			// SplitContainer.Panel2
			// 
			this.SplitContainer.Panel2.Controls.Add(this.Map);
			this.SplitContainer.Size = new System.Drawing.Size(1475, 733);
			this.SplitContainer.SplitterDistance = 491;
			this.SplitContainer.TabIndex = 3;
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1475, 779);
			this.Controls.Add(this.SplitContainer);
			this.Controls.Add(this.StatusStrip);
			this.Controls.Add(this.MainMenu);
			this.MainMenuStrip = this.MainMenu;
			this.Name = "FrmMain";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.FrmMain_Load);
			this.SplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
			this.SplitContainer.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip StatusStrip;
		private System.Windows.Forms.MenuStrip MainMenu;
		private GMap.NET.WindowsForms.GMapControl Map;
		private System.Windows.Forms.SplitContainer SplitContainer;
	}
}

