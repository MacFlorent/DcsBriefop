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
			this.CbMapMarker = new System.Windows.Forms.ComboBox();
			this.LbMapMarker = new System.Windows.Forms.Label();
			this.LbCoordinates = new System.Windows.Forms.Label();
			this.TbCoordinates = new System.Windows.Forms.TextBox();
			this.LbOther = new System.Windows.Forms.Label();
			this.LbDisplayName = new System.Windows.Forms.Label();
			this.TbDisplayName = new System.Windows.Forms.TextBox();
			this.TbOther = new System.Windows.Forms.TextBox();
			this.LbAttributes = new System.Windows.Forms.Label();
			this.TbAttributes = new System.Windows.Forms.TextBox();
			this.CkPlayable = new System.Windows.Forms.CheckBox();
			this.LbType = new System.Windows.Forms.Label();
			this.TbType = new System.Windows.Forms.TextBox();
			this.LbName = new System.Windows.Forms.Label();
			this.TbName = new System.Windows.Forms.TextBox();
			this.LbId = new System.Windows.Forms.Label();
			this.TbId = new System.Windows.Forms.TextBox();
			this.LbAltitude = new System.Windows.Forms.Label();
			this.TbAltitude = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// CbMapMarker
			// 
			this.CbMapMarker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.CbMapMarker.FormattingEnabled = true;
			this.CbMapMarker.Location = new System.Drawing.Point(82, 247);
			this.CbMapMarker.Name = "CbMapMarker";
			this.CbMapMarker.Size = new System.Drawing.Size(200, 21);
			this.CbMapMarker.TabIndex = 85;
			this.CbMapMarker.SelectedValueChanged += new System.EventHandler(this.CbMapMarker_SelectedValueChanged);
			// 
			// LbMapMarker
			// 
			this.LbMapMarker.AutoSize = true;
			this.LbMapMarker.Location = new System.Drawing.Point(6, 250);
			this.LbMapMarker.Name = "LbMapMarker";
			this.LbMapMarker.Size = new System.Drawing.Size(63, 13);
			this.LbMapMarker.TabIndex = 84;
			this.LbMapMarker.Text = "Map marker";
			// 
			// LbCoordinates
			// 
			this.LbCoordinates.AutoSize = true;
			this.LbCoordinates.Location = new System.Drawing.Point(6, 191);
			this.LbCoordinates.Name = "LbCoordinates";
			this.LbCoordinates.Size = new System.Drawing.Size(63, 13);
			this.LbCoordinates.TabIndex = 83;
			this.LbCoordinates.Text = "Coordinates";
			// 
			// TbCoordinates
			// 
			this.TbCoordinates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbCoordinates.Location = new System.Drawing.Point(82, 194);
			this.TbCoordinates.Multiline = true;
			this.TbCoordinates.Name = "TbCoordinates";
			this.TbCoordinates.ReadOnly = true;
			this.TbCoordinates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbCoordinates.Size = new System.Drawing.Size(200, 47);
			this.TbCoordinates.TabIndex = 82;
			// 
			// LbOther
			// 
			this.LbOther.AutoSize = true;
			this.LbOther.Location = new System.Drawing.Point(6, 145);
			this.LbOther.Name = "LbOther";
			this.LbOther.Size = new System.Drawing.Size(33, 13);
			this.LbOther.TabIndex = 81;
			this.LbOther.Text = "Other";
			// 
			// LbDisplayName
			// 
			this.LbDisplayName.AutoSize = true;
			this.LbDisplayName.Location = new System.Drawing.Point(6, 67);
			this.LbDisplayName.Name = "LbDisplayName";
			this.LbDisplayName.Size = new System.Drawing.Size(69, 13);
			this.LbDisplayName.TabIndex = 78;
			this.LbDisplayName.Text = "DisplayName";
			// 
			// TbDisplayName
			// 
			this.TbDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbDisplayName.Location = new System.Drawing.Point(82, 64);
			this.TbDisplayName.Name = "TbDisplayName";
			this.TbDisplayName.ReadOnly = true;
			this.TbDisplayName.Size = new System.Drawing.Size(200, 20);
			this.TbDisplayName.TabIndex = 77;
			// 
			// TbOther
			// 
			this.TbOther.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbOther.Location = new System.Drawing.Point(82, 142);
			this.TbOther.Name = "TbOther";
			this.TbOther.ReadOnly = true;
			this.TbOther.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbOther.Size = new System.Drawing.Size(200, 20);
			this.TbOther.TabIndex = 76;
			// 
			// LbAttributes
			// 
			this.LbAttributes.AutoSize = true;
			this.LbAttributes.Location = new System.Drawing.Point(7, 116);
			this.LbAttributes.Name = "LbAttributes";
			this.LbAttributes.Size = new System.Drawing.Size(51, 13);
			this.LbAttributes.TabIndex = 75;
			this.LbAttributes.Text = "Attributes";
			// 
			// TbAttributes
			// 
			this.TbAttributes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbAttributes.Location = new System.Drawing.Point(82, 116);
			this.TbAttributes.Name = "TbAttributes";
			this.TbAttributes.ReadOnly = true;
			this.TbAttributes.Size = new System.Drawing.Size(200, 20);
			this.TbAttributes.TabIndex = 74;
			// 
			// CkPlayable
			// 
			this.CkPlayable.AutoSize = true;
			this.CkPlayable.Enabled = false;
			this.CkPlayable.Location = new System.Drawing.Point(188, 15);
			this.CkPlayable.Name = "CkPlayable";
			this.CkPlayable.Size = new System.Drawing.Size(66, 17);
			this.CkPlayable.TabIndex = 72;
			this.CkPlayable.Text = "Playable";
			this.CkPlayable.UseVisualStyleBackColor = true;
			// 
			// LbType
			// 
			this.LbType.AutoSize = true;
			this.LbType.Location = new System.Drawing.Point(7, 93);
			this.LbType.Name = "LbType";
			this.LbType.Size = new System.Drawing.Size(31, 13);
			this.LbType.TabIndex = 71;
			this.LbType.Text = "Type";
			// 
			// TbType
			// 
			this.TbType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbType.Location = new System.Drawing.Point(82, 90);
			this.TbType.Name = "TbType";
			this.TbType.ReadOnly = true;
			this.TbType.Size = new System.Drawing.Size(200, 20);
			this.TbType.TabIndex = 70;
			// 
			// LbName
			// 
			this.LbName.AutoSize = true;
			this.LbName.Location = new System.Drawing.Point(7, 44);
			this.LbName.Name = "LbName";
			this.LbName.Size = new System.Drawing.Size(35, 13);
			this.LbName.TabIndex = 69;
			this.LbName.Text = "Name";
			// 
			// TbName
			// 
			this.TbName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbName.Location = new System.Drawing.Point(82, 38);
			this.TbName.Name = "TbName";
			this.TbName.ReadOnly = true;
			this.TbName.Size = new System.Drawing.Size(200, 20);
			this.TbName.TabIndex = 68;
			// 
			// LbId
			// 
			this.LbId.AutoSize = true;
			this.LbId.Location = new System.Drawing.Point(7, 15);
			this.LbId.Name = "LbId";
			this.LbId.Size = new System.Drawing.Size(18, 13);
			this.LbId.TabIndex = 67;
			this.LbId.Text = "ID";
			// 
			// TbId
			// 
			this.TbId.Location = new System.Drawing.Point(82, 12);
			this.TbId.Name = "TbId";
			this.TbId.ReadOnly = true;
			this.TbId.Size = new System.Drawing.Size(100, 20);
			this.TbId.TabIndex = 66;
			// 
			// LbAltitude
			// 
			this.LbAltitude.AutoSize = true;
			this.LbAltitude.Location = new System.Drawing.Point(6, 171);
			this.LbAltitude.Name = "LbAltitude";
			this.LbAltitude.Size = new System.Drawing.Size(57, 13);
			this.LbAltitude.TabIndex = 87;
			this.LbAltitude.Text = "Altitude (ft)";
			// 
			// TbAltitude
			// 
			this.TbAltitude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbAltitude.Location = new System.Drawing.Point(82, 168);
			this.TbAltitude.Name = "TbAltitude";
			this.TbAltitude.ReadOnly = true;
			this.TbAltitude.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbAltitude.Size = new System.Drawing.Size(200, 20);
			this.TbAltitude.TabIndex = 86;
			// 
			// UcUnit
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.LbAltitude);
			this.Controls.Add(this.TbAltitude);
			this.Controls.Add(this.CbMapMarker);
			this.Controls.Add(this.LbMapMarker);
			this.Controls.Add(this.LbCoordinates);
			this.Controls.Add(this.TbCoordinates);
			this.Controls.Add(this.LbOther);
			this.Controls.Add(this.LbDisplayName);
			this.Controls.Add(this.TbDisplayName);
			this.Controls.Add(this.TbOther);
			this.Controls.Add(this.LbAttributes);
			this.Controls.Add(this.TbAttributes);
			this.Controls.Add(this.CkPlayable);
			this.Controls.Add(this.LbType);
			this.Controls.Add(this.TbType);
			this.Controls.Add(this.LbName);
			this.Controls.Add(this.TbName);
			this.Controls.Add(this.LbId);
			this.Controls.Add(this.TbId);
			this.Name = "UcUnit";
			this.Size = new System.Drawing.Size(285, 270);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ComboBox CbMapMarker;
		private System.Windows.Forms.Label LbMapMarker;
		private System.Windows.Forms.Label LbCoordinates;
		private System.Windows.Forms.TextBox TbCoordinates;
		private System.Windows.Forms.Label LbOther;
		private System.Windows.Forms.Label LbDisplayName;
		private System.Windows.Forms.TextBox TbDisplayName;
		private System.Windows.Forms.TextBox TbOther;
		private System.Windows.Forms.Label LbAttributes;
		private System.Windows.Forms.TextBox TbAttributes;
		private System.Windows.Forms.CheckBox CkPlayable;
		private System.Windows.Forms.Label LbType;
		private System.Windows.Forms.TextBox TbType;
		private System.Windows.Forms.Label LbName;
		private System.Windows.Forms.TextBox TbName;
		private System.Windows.Forms.Label LbId;
		private System.Windows.Forms.TextBox TbId;
		private System.Windows.Forms.Label LbAltitude;
		private System.Windows.Forms.TextBox TbAltitude;
	}
}
