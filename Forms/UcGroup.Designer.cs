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
			this.LbAttributes = new System.Windows.Forms.Label();
			this.TbAttributes = new System.Windows.Forms.TextBox();
			this.DgvUnits = new Zuby.ADGV.AdvancedDataGridView();
			this.LbCoalition = new System.Windows.Forms.Label();
			this.CkLateActivation = new System.Windows.Forms.CheckBox();
			this.CkPlayable = new System.Windows.Forms.CheckBox();
			this.LbType = new System.Windows.Forms.Label();
			this.TbType = new System.Windows.Forms.TextBox();
			this.LbName = new System.Windows.Forms.Label();
			this.TbName = new System.Windows.Forms.TextBox();
			this.LbId = new System.Windows.Forms.Label();
			this.TbId = new System.Windows.Forms.TextBox();
			this.PnUnitDetail = new System.Windows.Forms.Panel();
			this.TbOther = new System.Windows.Forms.TextBox();
			this.LbDisplayName = new System.Windows.Forms.Label();
			this.TbDisplayName = new System.Windows.Forms.TextBox();
			this.LbClass = new System.Windows.Forms.Label();
			this.LbRadio = new System.Windows.Forms.Label();
			this.TbRadio = new System.Windows.Forms.TextBox();
			this.LbOther = new System.Windows.Forms.Label();
			this.MapControl = new GMap.NET.WindowsForms.GMapControl();
			this.LbCoordinates = new System.Windows.Forms.Label();
			this.TbCoordinates = new System.Windows.Forms.TextBox();
			this.LbMapMarker = new System.Windows.Forms.Label();
			this.CbMapMarker = new System.Windows.Forms.ComboBox();
			((System.ComponentModel.ISupportInitialize)(this.DgvUnits)).BeginInit();
			this.SuspendLayout();
			// 
			// LbAttributes
			// 
			this.LbAttributes.AutoSize = true;
			this.LbAttributes.Location = new System.Drawing.Point(4, 161);
			this.LbAttributes.Name = "LbAttributes";
			this.LbAttributes.Size = new System.Drawing.Size(51, 13);
			this.LbAttributes.TabIndex = 34;
			this.LbAttributes.Text = "Attributes";
			// 
			// TbAttributes
			// 
			this.TbAttributes.Location = new System.Drawing.Point(79, 161);
			this.TbAttributes.Name = "TbAttributes";
			this.TbAttributes.ReadOnly = true;
			this.TbAttributes.Size = new System.Drawing.Size(377, 20);
			this.TbAttributes.TabIndex = 33;
			// 
			// DgvUnits
			// 
			this.DgvUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvUnits.FilterAndSortEnabled = true;
			this.DgvUnits.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvUnits.Location = new System.Drawing.Point(3, 292);
			this.DgvUnits.Name = "DgvUnits";
			this.DgvUnits.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvUnits.Size = new System.Drawing.Size(935, 159);
			this.DgvUnits.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvUnits.TabIndex = 31;
			// 
			// LbCoalition
			// 
			this.LbCoalition.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbCoalition.Location = new System.Drawing.Point(0, 0);
			this.LbCoalition.Name = "LbCoalition";
			this.LbCoalition.Size = new System.Drawing.Size(941, 27);
			this.LbCoalition.TabIndex = 26;
			this.LbCoalition.Text = "Coalition";
			this.LbCoalition.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// CkLateActivation
			// 
			this.CkLateActivation.AutoSize = true;
			this.CkLateActivation.Location = new System.Drawing.Point(185, 59);
			this.CkLateActivation.Name = "CkLateActivation";
			this.CkLateActivation.Size = new System.Drawing.Size(96, 17);
			this.CkLateActivation.TabIndex = 25;
			this.CkLateActivation.Text = "Late activation";
			this.CkLateActivation.UseVisualStyleBackColor = true;
			// 
			// CkPlayable
			// 
			this.CkPlayable.AutoSize = true;
			this.CkPlayable.Location = new System.Drawing.Point(287, 60);
			this.CkPlayable.Name = "CkPlayable";
			this.CkPlayable.Size = new System.Drawing.Size(66, 17);
			this.CkPlayable.TabIndex = 24;
			this.CkPlayable.Text = "Playable";
			this.CkPlayable.UseVisualStyleBackColor = true;
			// 
			// LbType
			// 
			this.LbType.AutoSize = true;
			this.LbType.Location = new System.Drawing.Point(4, 138);
			this.LbType.Name = "LbType";
			this.LbType.Size = new System.Drawing.Size(31, 13);
			this.LbType.TabIndex = 23;
			this.LbType.Text = "Type";
			// 
			// TbType
			// 
			this.TbType.Location = new System.Drawing.Point(79, 135);
			this.TbType.Name = "TbType";
			this.TbType.ReadOnly = true;
			this.TbType.Size = new System.Drawing.Size(377, 20);
			this.TbType.TabIndex = 22;
			// 
			// LbName
			// 
			this.LbName.AutoSize = true;
			this.LbName.Location = new System.Drawing.Point(4, 89);
			this.LbName.Name = "LbName";
			this.LbName.Size = new System.Drawing.Size(35, 13);
			this.LbName.TabIndex = 21;
			this.LbName.Text = "Name";
			// 
			// TbName
			// 
			this.TbName.Location = new System.Drawing.Point(79, 83);
			this.TbName.Name = "TbName";
			this.TbName.ReadOnly = true;
			this.TbName.Size = new System.Drawing.Size(377, 20);
			this.TbName.TabIndex = 20;
			// 
			// LbId
			// 
			this.LbId.AutoSize = true;
			this.LbId.Location = new System.Drawing.Point(4, 60);
			this.LbId.Name = "LbId";
			this.LbId.Size = new System.Drawing.Size(18, 13);
			this.LbId.TabIndex = 19;
			this.LbId.Text = "ID";
			// 
			// TbId
			// 
			this.TbId.Location = new System.Drawing.Point(79, 57);
			this.TbId.Name = "TbId";
			this.TbId.ReadOnly = true;
			this.TbId.Size = new System.Drawing.Size(100, 20);
			this.TbId.TabIndex = 18;
			// 
			// PnUnitDetail
			// 
			this.PnUnitDetail.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.PnUnitDetail.Location = new System.Drawing.Point(0, 457);
			this.PnUnitDetail.Name = "PnUnitDetail";
			this.PnUnitDetail.Size = new System.Drawing.Size(941, 165);
			this.PnUnitDetail.TabIndex = 35;
			// 
			// TbOther
			// 
			this.TbOther.Location = new System.Drawing.Point(79, 213);
			this.TbOther.Name = "TbOther";
			this.TbOther.ReadOnly = true;
			this.TbOther.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbOther.Size = new System.Drawing.Size(377, 20);
			this.TbOther.TabIndex = 36;
			// 
			// LbDisplayName
			// 
			this.LbDisplayName.AutoSize = true;
			this.LbDisplayName.Location = new System.Drawing.Point(3, 112);
			this.LbDisplayName.Name = "LbDisplayName";
			this.LbDisplayName.Size = new System.Drawing.Size(69, 13);
			this.LbDisplayName.TabIndex = 38;
			this.LbDisplayName.Text = "DisplayName";
			// 
			// TbDisplayName
			// 
			this.TbDisplayName.Location = new System.Drawing.Point(79, 109);
			this.TbDisplayName.Name = "TbDisplayName";
			this.TbDisplayName.ReadOnly = true;
			this.TbDisplayName.Size = new System.Drawing.Size(377, 20);
			this.TbDisplayName.TabIndex = 37;
			// 
			// LbClass
			// 
			this.LbClass.Dock = System.Windows.Forms.DockStyle.Top;
			this.LbClass.Location = new System.Drawing.Point(0, 27);
			this.LbClass.Name = "LbClass";
			this.LbClass.Size = new System.Drawing.Size(941, 27);
			this.LbClass.TabIndex = 39;
			this.LbClass.Text = "Class";
			this.LbClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// LbRadio
			// 
			this.LbRadio.AutoSize = true;
			this.LbRadio.Location = new System.Drawing.Point(4, 187);
			this.LbRadio.Name = "LbRadio";
			this.LbRadio.Size = new System.Drawing.Size(35, 13);
			this.LbRadio.TabIndex = 41;
			this.LbRadio.Text = "Radio";
			// 
			// TbRadio
			// 
			this.TbRadio.Location = new System.Drawing.Point(79, 187);
			this.TbRadio.Name = "TbRadio";
			this.TbRadio.ReadOnly = true;
			this.TbRadio.Size = new System.Drawing.Size(377, 20);
			this.TbRadio.TabIndex = 40;
			// 
			// LbOther
			// 
			this.LbOther.AutoSize = true;
			this.LbOther.Location = new System.Drawing.Point(3, 216);
			this.LbOther.Name = "LbOther";
			this.LbOther.Size = new System.Drawing.Size(33, 13);
			this.LbOther.TabIndex = 42;
			this.LbOther.Text = "Other";
			// 
			// MapControl
			// 
			this.MapControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.MapControl.Bearing = 0F;
			this.MapControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.MapControl.CanDragMap = true;
			this.MapControl.EmptyTileColor = System.Drawing.Color.Navy;
			this.MapControl.GrayScaleMode = false;
			this.MapControl.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
			this.MapControl.LevelsKeepInMemory = 5;
			this.MapControl.Location = new System.Drawing.Point(462, 83);
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
			this.MapControl.Size = new System.Drawing.Size(476, 203);
			this.MapControl.TabIndex = 43;
			this.MapControl.Zoom = 0D;
			// 
			// LbCoordinates
			// 
			this.LbCoordinates.AutoSize = true;
			this.LbCoordinates.Location = new System.Drawing.Point(3, 242);
			this.LbCoordinates.Name = "LbCoordinates";
			this.LbCoordinates.Size = new System.Drawing.Size(63, 13);
			this.LbCoordinates.TabIndex = 45;
			this.LbCoordinates.Text = "Coordinates";
			// 
			// TbCoordinates
			// 
			this.TbCoordinates.Location = new System.Drawing.Point(79, 239);
			this.TbCoordinates.Multiline = true;
			this.TbCoordinates.Name = "TbCoordinates";
			this.TbCoordinates.ReadOnly = true;
			this.TbCoordinates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbCoordinates.Size = new System.Drawing.Size(377, 47);
			this.TbCoordinates.TabIndex = 44;
			// 
			// LbMapMarker
			// 
			this.LbMapMarker.AutoSize = true;
			this.LbMapMarker.Location = new System.Drawing.Point(468, 61);
			this.LbMapMarker.Name = "LbMapMarker";
			this.LbMapMarker.Size = new System.Drawing.Size(63, 13);
			this.LbMapMarker.TabIndex = 46;
			this.LbMapMarker.Text = "Map marker";
			// 
			// CbMapMarker
			// 
			this.CbMapMarker.FormattingEnabled = true;
			this.CbMapMarker.Location = new System.Drawing.Point(538, 56);
			this.CbMapMarker.Name = "CbMapMarker";
			this.CbMapMarker.Size = new System.Drawing.Size(400, 21);
			this.CbMapMarker.TabIndex = 47;
			this.CbMapMarker.SelectionChangeCommitted += new System.EventHandler(this.CbMapMarker_SelectionChangeCommitted);
			// 
			// UcGroup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.CbMapMarker);
			this.Controls.Add(this.LbMapMarker);
			this.Controls.Add(this.LbCoordinates);
			this.Controls.Add(this.TbCoordinates);
			this.Controls.Add(this.MapControl);
			this.Controls.Add(this.LbOther);
			this.Controls.Add(this.LbRadio);
			this.Controls.Add(this.TbRadio);
			this.Controls.Add(this.LbClass);
			this.Controls.Add(this.LbDisplayName);
			this.Controls.Add(this.TbDisplayName);
			this.Controls.Add(this.TbOther);
			this.Controls.Add(this.PnUnitDetail);
			this.Controls.Add(this.LbAttributes);
			this.Controls.Add(this.TbAttributes);
			this.Controls.Add(this.DgvUnits);
			this.Controls.Add(this.LbCoalition);
			this.Controls.Add(this.CkLateActivation);
			this.Controls.Add(this.CkPlayable);
			this.Controls.Add(this.LbType);
			this.Controls.Add(this.TbType);
			this.Controls.Add(this.LbName);
			this.Controls.Add(this.TbName);
			this.Controls.Add(this.LbId);
			this.Controls.Add(this.TbId);
			this.Name = "UcGroup";
			this.Size = new System.Drawing.Size(941, 622);
			((System.ComponentModel.ISupportInitialize)(this.DgvUnits)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label LbAttributes;
		private System.Windows.Forms.TextBox TbAttributes;
		private Zuby.ADGV.AdvancedDataGridView DgvUnits;
		private System.Windows.Forms.Label LbCoalition;
		private System.Windows.Forms.CheckBox CkLateActivation;
		private System.Windows.Forms.CheckBox CkPlayable;
		private System.Windows.Forms.Label LbType;
		private System.Windows.Forms.TextBox TbType;
		private System.Windows.Forms.Label LbName;
		private System.Windows.Forms.TextBox TbName;
		private System.Windows.Forms.Label LbId;
		private System.Windows.Forms.TextBox TbId;
		private System.Windows.Forms.Panel PnUnitDetail;
		private System.Windows.Forms.TextBox TbOther;
		private System.Windows.Forms.Label LbDisplayName;
		private System.Windows.Forms.TextBox TbDisplayName;
		private System.Windows.Forms.Label LbClass;
		private System.Windows.Forms.Label LbRadio;
		private System.Windows.Forms.TextBox TbRadio;
		private System.Windows.Forms.Label LbOther;
		private GMap.NET.WindowsForms.GMapControl MapControl;
		private System.Windows.Forms.Label LbCoordinates;
		private System.Windows.Forms.TextBox TbCoordinates;
		private System.Windows.Forms.Label LbMapMarker;
		private System.Windows.Forms.ComboBox CbMapMarker;
	}
}
