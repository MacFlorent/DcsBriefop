
namespace DcsBriefop
{
	partial class FrmAssetDetail
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
			this.LbName = new System.Windows.Forms.Label();
			this.TbName = new System.Windows.Forms.TextBox();
			this.CbUsage = new System.Windows.Forms.ComboBox();
			this.LbUsage = new System.Windows.Forms.Label();
			this.TbType = new System.Windows.Forms.TextBox();
			this.LbType = new System.Windows.Forms.Label();
			this.PnData = new System.Windows.Forms.Panel();
			this.TbLegend = new System.Windows.Forms.TextBox();
			this.LbDescription = new System.Windows.Forms.Label();
			this.TbDescription = new System.Windows.Forms.TextBox();
			this.BtInformationReset = new System.Windows.Forms.Button();
			this.TbInformation = new System.Windows.Forms.TextBox();
			this.LbInformation = new System.Windows.Forms.Label();
			this.TbRadio = new System.Windows.Forms.TextBox();
			this.LbRadio = new System.Windows.Forms.Label();
			this.CbMapDisplay = new System.Windows.Forms.ComboBox();
			this.LbMapDisplay = new System.Windows.Forms.Label();
			this.TbTask = new System.Windows.Forms.TextBox();
			this.LbTask = new System.Windows.Forms.Label();
			this.CkLateActivation = new System.Windows.Forms.CheckBox();
			this.CkPlayable = new System.Windows.Forms.CheckBox();
			this.LbId = new System.Windows.Forms.Label();
			this.TbId = new System.Windows.Forms.TextBox();
			this.PnData.SuspendLayout();
			this.SuspendLayout();
			// 
			// LbName
			// 
			this.LbName.AutoSize = true;
			this.LbName.Location = new System.Drawing.Point(143, 6);
			this.LbName.Name = "LbName";
			this.LbName.Size = new System.Drawing.Size(35, 13);
			this.LbName.TabIndex = 1;
			this.LbName.Text = "Name";
			// 
			// TbName
			// 
			this.TbName.Location = new System.Drawing.Point(184, 3);
			this.TbName.Name = "TbName";
			this.TbName.ReadOnly = true;
			this.TbName.Size = new System.Drawing.Size(174, 20);
			this.TbName.TabIndex = 2;
			// 
			// CbUsage
			// 
			this.CbUsage.FormattingEnabled = true;
			this.CbUsage.Location = new System.Drawing.Point(96, 156);
			this.CbUsage.Name = "CbUsage";
			this.CbUsage.Size = new System.Drawing.Size(262, 21);
			this.CbUsage.TabIndex = 3;
			this.CbUsage.SelectionChangeCommitted += new System.EventHandler(this.CbUsage_SelectionChangeCommitted);
			// 
			// LbUsage
			// 
			this.LbUsage.AutoSize = true;
			this.LbUsage.Location = new System.Drawing.Point(52, 159);
			this.LbUsage.Name = "LbUsage";
			this.LbUsage.Size = new System.Drawing.Size(38, 13);
			this.LbUsage.TabIndex = 4;
			this.LbUsage.Text = "Usage";
			// 
			// TbType
			// 
			this.TbType.Location = new System.Drawing.Point(96, 52);
			this.TbType.Name = "TbType";
			this.TbType.ReadOnly = true;
			this.TbType.Size = new System.Drawing.Size(100, 20);
			this.TbType.TabIndex = 6;
			// 
			// LbType
			// 
			this.LbType.AutoSize = true;
			this.LbType.Location = new System.Drawing.Point(59, 55);
			this.LbType.Name = "LbType";
			this.LbType.Size = new System.Drawing.Size(31, 13);
			this.LbType.TabIndex = 5;
			this.LbType.Text = "Type";
			// 
			// PnData
			// 
			this.PnData.Controls.Add(this.TbLegend);
			this.PnData.Controls.Add(this.LbDescription);
			this.PnData.Controls.Add(this.TbDescription);
			this.PnData.Controls.Add(this.BtInformationReset);
			this.PnData.Controls.Add(this.TbInformation);
			this.PnData.Controls.Add(this.LbInformation);
			this.PnData.Controls.Add(this.TbRadio);
			this.PnData.Controls.Add(this.LbRadio);
			this.PnData.Controls.Add(this.CbMapDisplay);
			this.PnData.Controls.Add(this.LbMapDisplay);
			this.PnData.Controls.Add(this.TbTask);
			this.PnData.Controls.Add(this.LbTask);
			this.PnData.Controls.Add(this.CkLateActivation);
			this.PnData.Controls.Add(this.CkPlayable);
			this.PnData.Controls.Add(this.LbId);
			this.PnData.Controls.Add(this.TbId);
			this.PnData.Controls.Add(this.TbType);
			this.PnData.Controls.Add(this.LbName);
			this.PnData.Controls.Add(this.TbName);
			this.PnData.Controls.Add(this.CbUsage);
			this.PnData.Controls.Add(this.LbUsage);
			this.PnData.Controls.Add(this.LbType);
			this.PnData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PnData.Location = new System.Drawing.Point(0, 0);
			this.PnData.Name = "PnData";
			this.PnData.Size = new System.Drawing.Size(364, 434);
			this.PnData.TabIndex = 10;
			// 
			// TbLegend
			// 
			this.TbLegend.Location = new System.Drawing.Point(6, 329);
			this.TbLegend.Multiline = true;
			this.TbLegend.Name = "TbLegend";
			this.TbLegend.ReadOnly = true;
			this.TbLegend.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbLegend.Size = new System.Drawing.Size(352, 101);
			this.TbLegend.TabIndex = 27;
			// 
			// LbDescription
			// 
			this.LbDescription.AutoSize = true;
			this.LbDescription.Location = new System.Drawing.Point(55, 81);
			this.LbDescription.Name = "LbDescription";
			this.LbDescription.Size = new System.Drawing.Size(35, 13);
			this.LbDescription.TabIndex = 24;
			this.LbDescription.Text = "Desc.";
			// 
			// TbDescription
			// 
			this.TbDescription.Location = new System.Drawing.Point(96, 78);
			this.TbDescription.Name = "TbDescription";
			this.TbDescription.ReadOnly = true;
			this.TbDescription.Size = new System.Drawing.Size(262, 20);
			this.TbDescription.TabIndex = 25;
			// 
			// BtInformationReset
			// 
			this.BtInformationReset.Location = new System.Drawing.Point(227, 181);
			this.BtInformationReset.Name = "BtInformationReset";
			this.BtInformationReset.Size = new System.Drawing.Size(131, 22);
			this.BtInformationReset.TabIndex = 23;
			this.BtInformationReset.Text = "Reset information";
			this.BtInformationReset.UseVisualStyleBackColor = true;
			this.BtInformationReset.Click += new System.EventHandler(this.BtInformationReset_Click);
			// 
			// TbInformation
			// 
			this.TbInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbInformation.Location = new System.Drawing.Point(6, 205);
			this.TbInformation.Multiline = true;
			this.TbInformation.Name = "TbInformation";
			this.TbInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbInformation.Size = new System.Drawing.Size(352, 118);
			this.TbInformation.TabIndex = 22;
			this.TbInformation.TextChanged += new System.EventHandler(this.TbInformation_TextChanged);
			// 
			// LbInformation
			// 
			this.LbInformation.AutoSize = true;
			this.LbInformation.Location = new System.Drawing.Point(3, 186);
			this.LbInformation.Name = "LbInformation";
			this.LbInformation.Size = new System.Drawing.Size(59, 13);
			this.LbInformation.TabIndex = 21;
			this.LbInformation.Text = "Information";
			// 
			// TbRadio
			// 
			this.TbRadio.Location = new System.Drawing.Point(96, 104);
			this.TbRadio.Name = "TbRadio";
			this.TbRadio.ReadOnly = true;
			this.TbRadio.Size = new System.Drawing.Size(100, 20);
			this.TbRadio.TabIndex = 20;
			// 
			// LbRadio
			// 
			this.LbRadio.AutoSize = true;
			this.LbRadio.Location = new System.Drawing.Point(51, 107);
			this.LbRadio.Name = "LbRadio";
			this.LbRadio.Size = new System.Drawing.Size(35, 13);
			this.LbRadio.TabIndex = 19;
			this.LbRadio.Text = "Radio";
			// 
			// CbMapDisplay
			// 
			this.CbMapDisplay.FormattingEnabled = true;
			this.CbMapDisplay.Location = new System.Drawing.Point(96, 129);
			this.CbMapDisplay.Name = "CbMapDisplay";
			this.CbMapDisplay.Size = new System.Drawing.Size(262, 21);
			this.CbMapDisplay.TabIndex = 17;
			this.CbMapDisplay.SelectionChangeCommitted += new System.EventHandler(this.CbMapDisplay_SelectionChangeCommitted);
			// 
			// LbMapDisplay
			// 
			this.LbMapDisplay.AutoSize = true;
			this.LbMapDisplay.Location = new System.Drawing.Point(27, 132);
			this.LbMapDisplay.Name = "LbMapDisplay";
			this.LbMapDisplay.Size = new System.Drawing.Size(63, 13);
			this.LbMapDisplay.TabIndex = 18;
			this.LbMapDisplay.Text = "Map display";
			// 
			// TbTask
			// 
			this.TbTask.Location = new System.Drawing.Point(258, 52);
			this.TbTask.Name = "TbTask";
			this.TbTask.ReadOnly = true;
			this.TbTask.Size = new System.Drawing.Size(100, 20);
			this.TbTask.TabIndex = 14;
			// 
			// LbTask
			// 
			this.LbTask.AutoSize = true;
			this.LbTask.Location = new System.Drawing.Point(221, 55);
			this.LbTask.Name = "LbTask";
			this.LbTask.Size = new System.Drawing.Size(31, 13);
			this.LbTask.TabIndex = 13;
			this.LbTask.Text = "Task";
			// 
			// CkLateActivation
			// 
			this.CkLateActivation.AutoSize = true;
			this.CkLateActivation.Enabled = false;
			this.CkLateActivation.Location = new System.Drawing.Point(168, 29);
			this.CkLateActivation.Name = "CkLateActivation";
			this.CkLateActivation.Size = new System.Drawing.Size(96, 17);
			this.CkLateActivation.TabIndex = 12;
			this.CkLateActivation.Text = "Late activation";
			this.CkLateActivation.UseVisualStyleBackColor = true;
			// 
			// CkPlayable
			// 
			this.CkPlayable.AutoSize = true;
			this.CkPlayable.Enabled = false;
			this.CkPlayable.Location = new System.Drawing.Point(96, 29);
			this.CkPlayable.Name = "CkPlayable";
			this.CkPlayable.Size = new System.Drawing.Size(66, 17);
			this.CkPlayable.TabIndex = 11;
			this.CkPlayable.Text = "Playable";
			this.CkPlayable.UseVisualStyleBackColor = true;
			// 
			// LbId
			// 
			this.LbId.AutoSize = true;
			this.LbId.Location = new System.Drawing.Point(72, 6);
			this.LbId.Name = "LbId";
			this.LbId.Size = new System.Drawing.Size(18, 13);
			this.LbId.TabIndex = 9;
			this.LbId.Text = "ID";
			// 
			// TbId
			// 
			this.TbId.Location = new System.Drawing.Point(96, 3);
			this.TbId.Name = "TbId";
			this.TbId.ReadOnly = true;
			this.TbId.Size = new System.Drawing.Size(41, 20);
			this.TbId.TabIndex = 10;
			// 
			// FrmAssetDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 434);
			this.Controls.Add(this.PnData);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmAssetDetail";
			this.Text = "FrmGroupDetail";
			this.PnData.ResumeLayout(false);
			this.PnData.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label LbName;
		private System.Windows.Forms.TextBox TbName;
		private System.Windows.Forms.ComboBox CbUsage;
		private System.Windows.Forms.TextBox TbType;
		private System.Windows.Forms.Label LbType;
		private System.Windows.Forms.Label LbUsage;
		private System.Windows.Forms.Panel PnData;
		private System.Windows.Forms.Label LbId;
		private System.Windows.Forms.TextBox TbId;
		private System.Windows.Forms.CheckBox CkPlayable;
		private System.Windows.Forms.CheckBox CkLateActivation;
		private System.Windows.Forms.TextBox TbTask;
		private System.Windows.Forms.Label LbTask;
		private System.Windows.Forms.ComboBox CbMapDisplay;
		private System.Windows.Forms.Label LbMapDisplay;
		private System.Windows.Forms.Label LbInformation;
		private System.Windows.Forms.TextBox TbRadio;
		private System.Windows.Forms.Label LbRadio;
		private System.Windows.Forms.Button BtInformationReset;
		private System.Windows.Forms.TextBox TbInformation;
		private System.Windows.Forms.Label LbDescription;
		private System.Windows.Forms.TextBox TbDescription;
		private System.Windows.Forms.TextBox TbLegend;
	}
}