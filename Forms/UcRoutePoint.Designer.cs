namespace DcsBriefop.Forms
{
	partial class UcRoutePoint
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
			LbCoordinates = new Label();
			TbCoordinates = new TextBox();
			LbAdditional = new Label();
			TbAdditional = new TextBox();
			LbAction = new Label();
			TbAction = new TextBox();
			LbType = new Label();
			TbType = new TextBox();
			LbName = new Label();
			TbName = new TextBox();
			LbNumber = new Label();
			TbNumber = new TextBox();
			LbAltitude = new Label();
			TbAltitude = new TextBox();
			LbNotes = new Label();
			TbNotes = new TextBox();
			SuspendLayout();
			// 
			// LbCoordinates
			// 
			LbCoordinates.AutoSize = true;
			LbCoordinates.Location = new Point(6, 157);
			LbCoordinates.Margin = new Padding(4, 0, 4, 0);
			LbCoordinates.Name = "LbCoordinates";
			LbCoordinates.Size = new Size(71, 15);
			LbCoordinates.TabIndex = 100;
			LbCoordinates.Text = "Coordinates";
			// 
			// TbCoordinates
			// 
			TbCoordinates.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbCoordinates.Location = new Point(94, 153);
			TbCoordinates.Margin = new Padding(4, 3, 4, 3);
			TbCoordinates.Multiline = true;
			TbCoordinates.Name = "TbCoordinates";
			TbCoordinates.ReadOnly = true;
			TbCoordinates.ScrollBars = ScrollBars.Vertical;
			TbCoordinates.Size = new Size(339, 54);
			TbCoordinates.TabIndex = 99;
			// 
			// LbAdditional
			// 
			LbAdditional.AutoSize = true;
			LbAdditional.Location = new Point(6, 245);
			LbAdditional.Margin = new Padding(4, 0, 4, 0);
			LbAdditional.Name = "LbAdditional";
			LbAdditional.Size = new Size(37, 15);
			LbAdditional.TabIndex = 98;
			LbAdditional.Text = "Other";
			// 
			// TbAdditional
			// 
			TbAdditional.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
			TbAdditional.Location = new Point(94, 242);
			TbAdditional.Margin = new Padding(4, 3, 4, 3);
			TbAdditional.Multiline = true;
			TbAdditional.Name = "TbAdditional";
			TbAdditional.ReadOnly = true;
			TbAdditional.ScrollBars = ScrollBars.Vertical;
			TbAdditional.Size = new Size(339, 111);
			TbAdditional.TabIndex = 95;
			// 
			// LbAction
			// 
			LbAction.AutoSize = true;
			LbAction.Location = new Point(7, 93);
			LbAction.Margin = new Padding(4, 0, 4, 0);
			LbAction.Name = "LbAction";
			LbAction.Size = new Size(42, 15);
			LbAction.TabIndex = 94;
			LbAction.Text = "Action";
			// 
			// TbAction
			// 
			TbAction.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbAction.Location = new Point(94, 93);
			TbAction.Margin = new Padding(4, 3, 4, 3);
			TbAction.Name = "TbAction";
			TbAction.ReadOnly = true;
			TbAction.Size = new Size(339, 23);
			TbAction.TabIndex = 93;
			// 
			// LbType
			// 
			LbType.AutoSize = true;
			LbType.Location = new Point(7, 67);
			LbType.Margin = new Padding(4, 0, 4, 0);
			LbType.Name = "LbType";
			LbType.Size = new Size(31, 15);
			LbType.TabIndex = 91;
			LbType.Text = "Type";
			// 
			// TbType
			// 
			TbType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbType.Location = new Point(94, 63);
			TbType.Margin = new Padding(4, 3, 4, 3);
			TbType.Name = "TbType";
			TbType.ReadOnly = true;
			TbType.Size = new Size(339, 23);
			TbType.TabIndex = 90;
			// 
			// LbName
			// 
			LbName.AutoSize = true;
			LbName.Location = new Point(7, 40);
			LbName.Margin = new Padding(4, 0, 4, 0);
			LbName.Name = "LbName";
			LbName.Size = new Size(39, 15);
			LbName.TabIndex = 89;
			LbName.Text = "Name";
			// 
			// TbName
			// 
			TbName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbName.Location = new Point(94, 33);
			TbName.Margin = new Padding(4, 3, 4, 3);
			TbName.Name = "TbName";
			TbName.ReadOnly = true;
			TbName.Size = new Size(339, 23);
			TbName.TabIndex = 88;
			// 
			// LbNumber
			// 
			LbNumber.AutoSize = true;
			LbNumber.Location = new Point(7, 7);
			LbNumber.Margin = new Padding(4, 0, 4, 0);
			LbNumber.Name = "LbNumber";
			LbNumber.Size = new Size(51, 15);
			LbNumber.TabIndex = 87;
			LbNumber.Text = "Number";
			// 
			// TbNumber
			// 
			TbNumber.Location = new Point(94, 3);
			TbNumber.Margin = new Padding(4, 3, 4, 3);
			TbNumber.Name = "TbNumber";
			TbNumber.ReadOnly = true;
			TbNumber.Size = new Size(356, 23);
			TbNumber.TabIndex = 86;
			// 
			// LbAltitude
			// 
			LbAltitude.AutoSize = true;
			LbAltitude.Location = new Point(7, 127);
			LbAltitude.Margin = new Padding(4, 0, 4, 0);
			LbAltitude.Name = "LbAltitude";
			LbAltitude.Size = new Size(49, 15);
			LbAltitude.TabIndex = 102;
			LbAltitude.Text = "Altitude";
			// 
			// TbAltitude
			// 
			TbAltitude.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbAltitude.Location = new Point(94, 123);
			TbAltitude.Margin = new Padding(4, 3, 4, 3);
			TbAltitude.Name = "TbAltitude";
			TbAltitude.ReadOnly = true;
			TbAltitude.Size = new Size(339, 23);
			TbAltitude.TabIndex = 101;
			// 
			// LbNotes
			// 
			LbNotes.AutoSize = true;
			LbNotes.Location = new Point(7, 217);
			LbNotes.Margin = new Padding(4, 0, 4, 0);
			LbNotes.Name = "LbNotes";
			LbNotes.Size = new Size(38, 15);
			LbNotes.TabIndex = 104;
			LbNotes.Text = "Notes";
			// 
			// TbNotes
			// 
			TbNotes.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			TbNotes.Location = new Point(94, 213);
			TbNotes.Margin = new Padding(4, 3, 4, 3);
			TbNotes.Name = "TbNotes";
			TbNotes.Size = new Size(339, 23);
			TbNotes.TabIndex = 103;
			// 
			// UcRoutePoint
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(LbNotes);
			Controls.Add(TbNotes);
			Controls.Add(LbAltitude);
			Controls.Add(TbAltitude);
			Controls.Add(LbCoordinates);
			Controls.Add(TbCoordinates);
			Controls.Add(LbAdditional);
			Controls.Add(TbAdditional);
			Controls.Add(LbAction);
			Controls.Add(TbAction);
			Controls.Add(LbType);
			Controls.Add(TbType);
			Controls.Add(LbName);
			Controls.Add(TbName);
			Controls.Add(LbNumber);
			Controls.Add(TbNumber);
			Margin = new Padding(4, 3, 4, 3);
			Name = "UcRoutePoint";
			Size = new Size(438, 357);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private System.Windows.Forms.Label LbCoordinates;
		private System.Windows.Forms.TextBox TbCoordinates;
		private System.Windows.Forms.Label LbAdditional;
		private System.Windows.Forms.TextBox TbAdditional;
		private System.Windows.Forms.Label LbAction;
		private System.Windows.Forms.TextBox TbAction;
		private System.Windows.Forms.Label LbType;
		private System.Windows.Forms.TextBox TbType;
		private System.Windows.Forms.Label LbName;
		private System.Windows.Forms.TextBox TbName;
		private System.Windows.Forms.Label LbNumber;
		private System.Windows.Forms.TextBox TbNumber;
		private System.Windows.Forms.Label LbAltitude;
		private System.Windows.Forms.TextBox TbAltitude;
		private Label LbNotes;
		private TextBox TbNotes;
	}
}
