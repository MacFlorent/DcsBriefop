namespace DcsBriefop.Forms
{
	partial class UcAirbase
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
			this.CbMapMarker = new System.Windows.Forms.ComboBox();
			this.LbMapMarker = new System.Windows.Forms.Label();
			this.LbCoordinates = new System.Windows.Forms.Label();
			this.TbCoordinates = new System.Windows.Forms.TextBox();
			this.LbRadios = new System.Windows.Forms.Label();
			this.LbTacan = new System.Windows.Forms.Label();
			this.TbTacan = new System.Windows.Forms.TextBox();
			this.LbType = new System.Windows.Forms.Label();
			this.TbType = new System.Windows.Forms.TextBox();
			this.LbName = new System.Windows.Forms.Label();
			this.TbName = new System.Windows.Forms.TextBox();
			this.LbId = new System.Windows.Forms.Label();
			this.TbId = new System.Windows.Forms.TextBox();
			this.DgvRadios = new Zuby.ADGV.AdvancedDataGridView();
			this.MapControl = new GMap.NET.WindowsForms.GMapControl();
			this.BtRadioAdd = new System.Windows.Forms.Button();
			this.BtRadioRemove = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.DgvRadios)).BeginInit();
			this.SuspendLayout();
			// 
			// CbMapMarker
			// 
			this.CbMapMarker.FormattingEnabled = true;
			this.CbMapMarker.Location = new System.Drawing.Point(84, 134);
			this.CbMapMarker.Name = "CbMapMarker";
			this.CbMapMarker.Size = new System.Drawing.Size(394, 21);
			this.CbMapMarker.TabIndex = 104;
			// 
			// LbMapMarker
			// 
			this.LbMapMarker.AutoSize = true;
			this.LbMapMarker.Location = new System.Drawing.Point(8, 137);
			this.LbMapMarker.Name = "LbMapMarker";
			this.LbMapMarker.Size = new System.Drawing.Size(63, 13);
			this.LbMapMarker.TabIndex = 103;
			this.LbMapMarker.Text = "Map marker";
			// 
			// LbCoordinates
			// 
			this.LbCoordinates.AutoSize = true;
			this.LbCoordinates.Location = new System.Drawing.Point(8, 78);
			this.LbCoordinates.Name = "LbCoordinates";
			this.LbCoordinates.Size = new System.Drawing.Size(63, 13);
			this.LbCoordinates.TabIndex = 102;
			this.LbCoordinates.Text = "Coordinates";
			// 
			// TbCoordinates
			// 
			this.TbCoordinates.Location = new System.Drawing.Point(84, 81);
			this.TbCoordinates.Multiline = true;
			this.TbCoordinates.Name = "TbCoordinates";
			this.TbCoordinates.ReadOnly = true;
			this.TbCoordinates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbCoordinates.Size = new System.Drawing.Size(394, 47);
			this.TbCoordinates.TabIndex = 101;
			// 
			// LbRadios
			// 
			this.LbRadios.AutoSize = true;
			this.LbRadios.Location = new System.Drawing.Point(12, 161);
			this.LbRadios.Name = "LbRadios";
			this.LbRadios.Size = new System.Drawing.Size(40, 13);
			this.LbRadios.TabIndex = 100;
			this.LbRadios.Text = "Radios";
			// 
			// LbTacan
			// 
			this.LbTacan.AutoSize = true;
			this.LbTacan.Location = new System.Drawing.Point(9, 57);
			this.LbTacan.Name = "LbTacan";
			this.LbTacan.Size = new System.Drawing.Size(43, 13);
			this.LbTacan.TabIndex = 96;
			this.LbTacan.Text = "TACAN";
			// 
			// TbTacan
			// 
			this.TbTacan.Location = new System.Drawing.Point(84, 55);
			this.TbTacan.Name = "TbTacan";
			this.TbTacan.Size = new System.Drawing.Size(394, 20);
			this.TbTacan.TabIndex = 95;
			this.TbTacan.Validated += new System.EventHandler(this.TbTacan_Validated);
			// 
			// LbType
			// 
			this.LbType.AutoSize = true;
			this.LbType.Location = new System.Drawing.Point(190, 6);
			this.LbType.Name = "LbType";
			this.LbType.Size = new System.Drawing.Size(31, 13);
			this.LbType.TabIndex = 93;
			this.LbType.Text = "Type";
			// 
			// TbType
			// 
			this.TbType.Location = new System.Drawing.Point(227, 3);
			this.TbType.Name = "TbType";
			this.TbType.ReadOnly = true;
			this.TbType.Size = new System.Drawing.Size(124, 20);
			this.TbType.TabIndex = 92;
			// 
			// LbName
			// 
			this.LbName.AutoSize = true;
			this.LbName.Location = new System.Drawing.Point(9, 35);
			this.LbName.Name = "LbName";
			this.LbName.Size = new System.Drawing.Size(35, 13);
			this.LbName.TabIndex = 91;
			this.LbName.Text = "Name";
			// 
			// TbName
			// 
			this.TbName.Location = new System.Drawing.Point(84, 29);
			this.TbName.Name = "TbName";
			this.TbName.ReadOnly = true;
			this.TbName.Size = new System.Drawing.Size(394, 20);
			this.TbName.TabIndex = 90;
			// 
			// LbId
			// 
			this.LbId.AutoSize = true;
			this.LbId.Location = new System.Drawing.Point(9, 6);
			this.LbId.Name = "LbId";
			this.LbId.Size = new System.Drawing.Size(18, 13);
			this.LbId.TabIndex = 89;
			this.LbId.Text = "ID";
			// 
			// TbId
			// 
			this.TbId.Location = new System.Drawing.Point(84, 3);
			this.TbId.Name = "TbId";
			this.TbId.ReadOnly = true;
			this.TbId.Size = new System.Drawing.Size(100, 20);
			this.TbId.TabIndex = 88;
			// 
			// DgvRadios
			// 
			this.DgvRadios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.DgvRadios.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvRadios.FilterAndSortEnabled = true;
			this.DgvRadios.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvRadios.Location = new System.Drawing.Point(84, 161);
			this.DgvRadios.Name = "DgvRadios";
			this.DgvRadios.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvRadios.Size = new System.Drawing.Size(394, 324);
			this.DgvRadios.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvRadios.TabIndex = 105;
			// 
			// MapControl
			// 
			this.MapControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MapControl.Bearing = 0F;
			this.MapControl.CanDragMap = true;
			this.MapControl.EmptyTileColor = System.Drawing.Color.Navy;
			this.MapControl.GrayScaleMode = false;
			this.MapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.MapControl.LevelsKeepInMemory = 5;
			this.MapControl.Location = new System.Drawing.Point(484, 0);
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
			this.MapControl.Size = new System.Drawing.Size(648, 482);
			this.MapControl.TabIndex = 106;
			this.MapControl.Zoom = 0D;
			// 
			// BtRadioAdd
			// 
			this.BtRadioAdd.Location = new System.Drawing.Point(45, 189);
			this.BtRadioAdd.Name = "BtRadioAdd";
			this.BtRadioAdd.Size = new System.Drawing.Size(33, 23);
			this.BtRadioAdd.TabIndex = 107;
			this.BtRadioAdd.Text = "+";
			this.BtRadioAdd.UseVisualStyleBackColor = true;
			this.BtRadioAdd.Click += new System.EventHandler(this.BtRadioAdd_Click);
			// 
			// BtRadioRemove
			// 
			this.BtRadioRemove.Location = new System.Drawing.Point(45, 218);
			this.BtRadioRemove.Name = "BtRadioRemove";
			this.BtRadioRemove.Size = new System.Drawing.Size(33, 23);
			this.BtRadioRemove.TabIndex = 108;
			this.BtRadioRemove.Text = "-";
			this.BtRadioRemove.UseVisualStyleBackColor = true;
			this.BtRadioRemove.Click += new System.EventHandler(this.BtRadioRemove_Click);
			// 
			// UcAirbase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.BtRadioRemove);
			this.Controls.Add(this.BtRadioAdd);
			this.Controls.Add(this.MapControl);
			this.Controls.Add(this.DgvRadios);
			this.Controls.Add(this.CbMapMarker);
			this.Controls.Add(this.LbMapMarker);
			this.Controls.Add(this.LbCoordinates);
			this.Controls.Add(this.TbCoordinates);
			this.Controls.Add(this.LbRadios);
			this.Controls.Add(this.LbTacan);
			this.Controls.Add(this.TbTacan);
			this.Controls.Add(this.LbType);
			this.Controls.Add(this.TbType);
			this.Controls.Add(this.LbName);
			this.Controls.Add(this.TbName);
			this.Controls.Add(this.LbId);
			this.Controls.Add(this.TbId);
			this.Name = "UcAirbase";
			this.Size = new System.Drawing.Size(1132, 488);
			((System.ComponentModel.ISupportInitialize)(this.DgvRadios)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ComboBox CbMapMarker;
		private System.Windows.Forms.Label LbMapMarker;
		private System.Windows.Forms.Label LbCoordinates;
		private System.Windows.Forms.TextBox TbCoordinates;
		private System.Windows.Forms.Label LbRadios;
		private System.Windows.Forms.Label LbTacan;
		private System.Windows.Forms.TextBox TbTacan;
		private System.Windows.Forms.Label LbType;
		private System.Windows.Forms.TextBox TbType;
		private System.Windows.Forms.Label LbName;
		private System.Windows.Forms.TextBox TbName;
		private System.Windows.Forms.Label LbId;
		private System.Windows.Forms.TextBox TbId;
		private Zuby.ADGV.AdvancedDataGridView DgvRadios;
		private GMap.NET.WindowsForms.GMapControl MapControl;
		private System.Windows.Forms.Button BtRadioAdd;
		private System.Windows.Forms.Button BtRadioRemove;
	}
}
