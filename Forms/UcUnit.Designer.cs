namespace DcsBriefop.Forms
{
	partial class UcUnit
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
			CbMapMarker = new ComboBox();
			LbMapMarker = new Label();
			LbCoordinates = new Label();
			TbCoordinates = new TextBox();
			LbOther = new Label();
			LbDisplayName = new Label();
			TbDisplayName = new TextBox();
			TbOther = new TextBox();
			LbAttributes = new Label();
			TbAttributes = new TextBox();
			CkPlayable = new CheckBox();
			LbType = new Label();
			TbType = new TextBox();
			LbName = new Label();
			TbName = new TextBox();
			LbId = new Label();
			TbId = new TextBox();
			LbAltitude = new Label();
			TbAltitude = new TextBox();
			LbLink16Stn = new Label();
			TbLink16Stn = new TextBox();
			LbLink16Callsign = new Label();
			TbLink16Callsign = new TextBox();
			SuspendLayout();
			// 
			// CbMapMarker
			// 
			CbMapMarker.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			CbMapMarker.FormattingEnabled = true;
			CbMapMarker.Location = new Point(100, 285);
			CbMapMarker.Margin = new Padding(4, 3, 4, 3);
			CbMapMarker.Name = "CbMapMarker";
			CbMapMarker.Size = new Size(229, 23);
			CbMapMarker.TabIndex = 85;
			CbMapMarker.SelectedValueChanged += CbMapMarker_SelectedValueChanged;
			// 
			// LbMapMarker
			// 
			LbMapMarker.AutoSize = true;
			LbMapMarker.Location = new Point(7, 288);
			LbMapMarker.Margin = new Padding(4, 0, 4, 0);
			LbMapMarker.Name = "LbMapMarker";
			LbMapMarker.Size = new Size(71, 15);
			LbMapMarker.TabIndex = 84;
			LbMapMarker.Text = "Map marker";
			// 
			// LbCoordinates
			// 
			LbCoordinates.AutoSize = true;
			LbCoordinates.Location = new Point(7, 220);
			LbCoordinates.Margin = new Padding(4, 0, 4, 0);
			LbCoordinates.Name = "LbCoordinates";
			LbCoordinates.Size = new Size(71, 15);
			LbCoordinates.TabIndex = 83;
			LbCoordinates.Text = "Coordinates";
			// 
			// TbCoordinates
			// 
			TbCoordinates.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbCoordinates.Location = new Point(100, 224);
			TbCoordinates.Margin = new Padding(4, 3, 4, 3);
			TbCoordinates.Multiline = true;
			TbCoordinates.Name = "TbCoordinates";
			TbCoordinates.ReadOnly = true;
			TbCoordinates.ScrollBars = ScrollBars.Vertical;
			TbCoordinates.Size = new Size(229, 54);
			TbCoordinates.TabIndex = 82;
			// 
			// LbOther
			// 
			LbOther.AutoSize = true;
			LbOther.Location = new Point(7, 167);
			LbOther.Margin = new Padding(4, 0, 4, 0);
			LbOther.Name = "LbOther";
			LbOther.Size = new Size(37, 15);
			LbOther.TabIndex = 81;
			LbOther.Text = "Other";
			// 
			// LbDisplayName
			// 
			LbDisplayName.AutoSize = true;
			LbDisplayName.Location = new Point(7, 77);
			LbDisplayName.Margin = new Padding(4, 0, 4, 0);
			LbDisplayName.Name = "LbDisplayName";
			LbDisplayName.Size = new Size(77, 15);
			LbDisplayName.TabIndex = 78;
			LbDisplayName.Text = "DisplayName";
			// 
			// TbDisplayName
			// 
			TbDisplayName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbDisplayName.Location = new Point(100, 74);
			TbDisplayName.Margin = new Padding(4, 3, 4, 3);
			TbDisplayName.Name = "TbDisplayName";
			TbDisplayName.ReadOnly = true;
			TbDisplayName.Size = new Size(229, 23);
			TbDisplayName.TabIndex = 77;
			// 
			// TbOther
			// 
			TbOther.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbOther.Location = new Point(100, 164);
			TbOther.Margin = new Padding(4, 3, 4, 3);
			TbOther.Name = "TbOther";
			TbOther.ReadOnly = true;
			TbOther.ScrollBars = ScrollBars.Vertical;
			TbOther.Size = new Size(229, 23);
			TbOther.TabIndex = 76;
			// 
			// LbAttributes
			// 
			LbAttributes.AutoSize = true;
			LbAttributes.Location = new Point(8, 134);
			LbAttributes.Margin = new Padding(4, 0, 4, 0);
			LbAttributes.Name = "LbAttributes";
			LbAttributes.Size = new Size(59, 15);
			LbAttributes.TabIndex = 75;
			LbAttributes.Text = "Attributes";
			// 
			// TbAttributes
			// 
			TbAttributes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbAttributes.Location = new Point(100, 134);
			TbAttributes.Margin = new Padding(4, 3, 4, 3);
			TbAttributes.Name = "TbAttributes";
			TbAttributes.ReadOnly = true;
			TbAttributes.Size = new Size(229, 23);
			TbAttributes.TabIndex = 74;
			// 
			// CkPlayable
			// 
			CkPlayable.AutoSize = true;
			CkPlayable.Enabled = false;
			CkPlayable.Location = new Point(219, 17);
			CkPlayable.Margin = new Padding(4, 3, 4, 3);
			CkPlayable.Name = "CkPlayable";
			CkPlayable.Size = new Size(70, 19);
			CkPlayable.TabIndex = 72;
			CkPlayable.Text = "Playable";
			CkPlayable.UseVisualStyleBackColor = true;
			// 
			// LbType
			// 
			LbType.AutoSize = true;
			LbType.Location = new Point(8, 107);
			LbType.Margin = new Padding(4, 0, 4, 0);
			LbType.Name = "LbType";
			LbType.Size = new Size(31, 15);
			LbType.TabIndex = 71;
			LbType.Text = "Type";
			// 
			// TbType
			// 
			TbType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbType.Location = new Point(100, 104);
			TbType.Margin = new Padding(4, 3, 4, 3);
			TbType.Name = "TbType";
			TbType.ReadOnly = true;
			TbType.Size = new Size(229, 23);
			TbType.TabIndex = 70;
			// 
			// LbName
			// 
			LbName.AutoSize = true;
			LbName.Location = new Point(8, 51);
			LbName.Margin = new Padding(4, 0, 4, 0);
			LbName.Name = "LbName";
			LbName.Size = new Size(39, 15);
			LbName.TabIndex = 69;
			LbName.Text = "Name";
			// 
			// TbName
			// 
			TbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbName.Location = new Point(100, 44);
			TbName.Margin = new Padding(4, 3, 4, 3);
			TbName.Name = "TbName";
			TbName.ReadOnly = true;
			TbName.Size = new Size(229, 23);
			TbName.TabIndex = 68;
			// 
			// LbId
			// 
			LbId.AutoSize = true;
			LbId.Location = new Point(8, 17);
			LbId.Margin = new Padding(4, 0, 4, 0);
			LbId.Name = "LbId";
			LbId.Size = new Size(18, 15);
			LbId.TabIndex = 67;
			LbId.Text = "ID";
			// 
			// TbId
			// 
			TbId.Location = new Point(100, 14);
			TbId.Margin = new Padding(4, 3, 4, 3);
			TbId.Name = "TbId";
			TbId.ReadOnly = true;
			TbId.Size = new Size(112, 23);
			TbId.TabIndex = 66;
			// 
			// LbAltitude
			// 
			LbAltitude.AutoSize = true;
			LbAltitude.Location = new Point(7, 197);
			LbAltitude.Margin = new Padding(4, 0, 4, 0);
			LbAltitude.Name = "LbAltitude";
			LbAltitude.Size = new Size(68, 15);
			LbAltitude.TabIndex = 87;
			LbAltitude.Text = "Altitude (ft)";
			// 
			// TbAltitude
			// 
			TbAltitude.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbAltitude.Location = new Point(100, 194);
			TbAltitude.Margin = new Padding(4, 3, 4, 3);
			TbAltitude.Name = "TbAltitude";
			TbAltitude.ReadOnly = true;
			TbAltitude.ScrollBars = ScrollBars.Vertical;
			TbAltitude.Size = new Size(229, 23);
			TbAltitude.TabIndex = 86;
			// 
			// LbLink16Stn
			// 
			LbLink16Stn.AutoSize = true;
			LbLink16Stn.Location = new Point(8, 344);
			LbLink16Stn.Margin = new Padding(4, 0, 4, 0);
			LbLink16Stn.Name = "LbLink16Stn";
			LbLink16Stn.Size = new Size(65, 15);
			LbLink16Stn.TabIndex = 89;
			LbLink16Stn.Text = "Link16 STN";
			// 
			// TbLink16Stn
			// 
			TbLink16Stn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbLink16Stn.Location = new Point(100, 341);
			TbLink16Stn.Margin = new Padding(4, 3, 4, 3);
			TbLink16Stn.Name = "TbLink16Stn";
			TbLink16Stn.ScrollBars = ScrollBars.Vertical;
			TbLink16Stn.Size = new Size(229, 23);
			TbLink16Stn.TabIndex = 88;
			// 
			// LbLink16Callsign
			// 
			LbLink16Callsign.AutoSize = true;
			LbLink16Callsign.Location = new Point(8, 315);
			LbLink16Callsign.Margin = new Padding(4, 0, 4, 0);
			LbLink16Callsign.Name = "LbLink16Callsign";
			LbLink16Callsign.Size = new Size(84, 15);
			LbLink16Callsign.TabIndex = 91;
			LbLink16Callsign.Text = "Link16 callsign";
			// 
			// TbLink16Callsign
			// 
			TbLink16Callsign.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbLink16Callsign.Location = new Point(100, 312);
			TbLink16Callsign.Margin = new Padding(4, 3, 4, 3);
			TbLink16Callsign.Name = "TbLink16Callsign";
			TbLink16Callsign.ReadOnly = true;
			TbLink16Callsign.ScrollBars = ScrollBars.Vertical;
			TbLink16Callsign.Size = new Size(229, 23);
			TbLink16Callsign.TabIndex = 90;
			// 
			// UcUnit
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(LbLink16Callsign);
			Controls.Add(TbLink16Callsign);
			Controls.Add(LbLink16Stn);
			Controls.Add(TbLink16Stn);
			Controls.Add(LbAltitude);
			Controls.Add(TbAltitude);
			Controls.Add(CbMapMarker);
			Controls.Add(LbMapMarker);
			Controls.Add(LbCoordinates);
			Controls.Add(TbCoordinates);
			Controls.Add(LbOther);
			Controls.Add(LbDisplayName);
			Controls.Add(TbDisplayName);
			Controls.Add(TbOther);
			Controls.Add(LbAttributes);
			Controls.Add(TbAttributes);
			Controls.Add(CkPlayable);
			Controls.Add(LbType);
			Controls.Add(TbType);
			Controls.Add(LbName);
			Controls.Add(TbName);
			Controls.Add(LbId);
			Controls.Add(TbId);
			Margin = new Padding(4, 3, 4, 3);
			Name = "UcUnit";
			Size = new Size(332, 367);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox CbMapMarker;
		private Label LbMapMarker;
		private Label LbCoordinates;
		private TextBox TbCoordinates;
		private Label LbOther;
		private Label LbDisplayName;
		private TextBox TbDisplayName;
		private TextBox TbOther;
		private Label LbAttributes;
		private TextBox TbAttributes;
		private CheckBox CkPlayable;
		private Label LbType;
		private TextBox TbType;
		private Label LbName;
		private TextBox TbName;
		private Label LbId;
		private TextBox TbId;
		private Label LbAltitude;
		private TextBox TbAltitude;
		private Label LbLink16Stn;
		private TextBox TbLink16Stn;
		private Label LbLink16Callsign;
		private TextBox TbLink16Callsign;
	}
}
