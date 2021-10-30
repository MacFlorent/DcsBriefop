
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
			this.CbCategory = new System.Windows.Forms.ComboBox();
			this.LbCategory = new System.Windows.Forms.Label();
			this.TbType = new System.Windows.Forms.TextBox();
			this.LbType = new System.Windows.Forms.Label();
			this.PnMissionMap = new System.Windows.Forms.Panel();
			this.PnData = new System.Windows.Forms.Panel();
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
			this.LbName.Location = new System.Drawing.Point(37, 35);
			this.LbName.Name = "LbName";
			this.LbName.Size = new System.Drawing.Size(35, 13);
			this.LbName.TabIndex = 1;
			this.LbName.Text = "Name";
			// 
			// TbName
			// 
			this.TbName.Location = new System.Drawing.Point(78, 29);
			this.TbName.Name = "TbName";
			this.TbName.ReadOnly = true;
			this.TbName.Size = new System.Drawing.Size(100, 20);
			this.TbName.TabIndex = 2;
			// 
			// CbCategory
			// 
			this.CbCategory.FormattingEnabled = true;
			this.CbCategory.Location = new System.Drawing.Point(78, 183);
			this.CbCategory.Name = "CbCategory";
			this.CbCategory.Size = new System.Drawing.Size(100, 21);
			this.CbCategory.TabIndex = 3;
			this.CbCategory.SelectionChangeCommitted += new System.EventHandler(this.CbCategory_SelectionChangeCommitted);
			// 
			// LbCategory
			// 
			this.LbCategory.AutoSize = true;
			this.LbCategory.Location = new System.Drawing.Point(23, 186);
			this.LbCategory.Name = "LbCategory";
			this.LbCategory.Size = new System.Drawing.Size(49, 13);
			this.LbCategory.TabIndex = 4;
			this.LbCategory.Text = "Category";
			// 
			// TbType
			// 
			this.TbType.Location = new System.Drawing.Point(78, 81);
			this.TbType.Name = "TbType";
			this.TbType.ReadOnly = true;
			this.TbType.Size = new System.Drawing.Size(100, 20);
			this.TbType.TabIndex = 6;
			// 
			// LbType
			// 
			this.LbType.AutoSize = true;
			this.LbType.Location = new System.Drawing.Point(41, 84);
			this.LbType.Name = "LbType";
			this.LbType.Size = new System.Drawing.Size(31, 13);
			this.LbType.TabIndex = 5;
			this.LbType.Text = "Type";
			// 
			// PnMissionMap
			// 
			this.PnMissionMap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PnMissionMap.Location = new System.Drawing.Point(189, 0);
			this.PnMissionMap.Name = "PnMissionMap";
			this.PnMissionMap.Size = new System.Drawing.Size(1070, 679);
			this.PnMissionMap.TabIndex = 9;
			// 
			// PnData
			// 
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
			this.PnData.Controls.Add(this.CbCategory);
			this.PnData.Controls.Add(this.LbCategory);
			this.PnData.Controls.Add(this.LbType);
			this.PnData.Dock = System.Windows.Forms.DockStyle.Left;
			this.PnData.Location = new System.Drawing.Point(0, 0);
			this.PnData.Name = "PnData";
			this.PnData.Size = new System.Drawing.Size(189, 679);
			this.PnData.TabIndex = 10;
			// 
			// BtInformationReset
			// 
			this.BtInformationReset.Location = new System.Drawing.Point(153, 213);
			this.BtInformationReset.Name = "BtInformationReset";
			this.BtInformationReset.Size = new System.Drawing.Size(25, 22);
			this.BtInformationReset.TabIndex = 23;
			this.BtInformationReset.Text = "R";
			this.BtInformationReset.UseVisualStyleBackColor = true;
			this.BtInformationReset.Click += new System.EventHandler(this.BtInformationReset_Click);
			// 
			// TbInformation
			// 
			this.TbInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbInformation.Location = new System.Drawing.Point(6, 234);
			this.TbInformation.Multiline = true;
			this.TbInformation.Name = "TbInformation";
			this.TbInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbInformation.Size = new System.Drawing.Size(177, 114);
			this.TbInformation.TabIndex = 22;
			this.TbInformation.TextChanged += new System.EventHandler(this.TbInformation_TextChanged);
			// 
			// LbInformation
			// 
			this.LbInformation.AutoSize = true;
			this.LbInformation.Location = new System.Drawing.Point(3, 218);
			this.LbInformation.Name = "LbInformation";
			this.LbInformation.Size = new System.Drawing.Size(59, 13);
			this.LbInformation.TabIndex = 21;
			this.LbInformation.Text = "Information";
			// 
			// TbRadio
			// 
			this.TbRadio.Location = new System.Drawing.Point(78, 130);
			this.TbRadio.Name = "TbRadio";
			this.TbRadio.ReadOnly = true;
			this.TbRadio.Size = new System.Drawing.Size(100, 20);
			this.TbRadio.TabIndex = 20;
			// 
			// LbRadio
			// 
			this.LbRadio.AutoSize = true;
			this.LbRadio.Location = new System.Drawing.Point(41, 133);
			this.LbRadio.Name = "LbRadio";
			this.LbRadio.Size = new System.Drawing.Size(35, 13);
			this.LbRadio.TabIndex = 19;
			this.LbRadio.Text = "Radio";
			// 
			// CbMapDisplay
			// 
			this.CbMapDisplay.FormattingEnabled = true;
			this.CbMapDisplay.Location = new System.Drawing.Point(78, 156);
			this.CbMapDisplay.Name = "CbMapDisplay";
			this.CbMapDisplay.Size = new System.Drawing.Size(100, 21);
			this.CbMapDisplay.TabIndex = 17;
			this.CbMapDisplay.SelectionChangeCommitted += new System.EventHandler(this.CbMapDisplay_SelectionChangeCommitted);
			// 
			// LbMapDisplay
			// 
			this.LbMapDisplay.AutoSize = true;
			this.LbMapDisplay.Location = new System.Drawing.Point(9, 159);
			this.LbMapDisplay.Name = "LbMapDisplay";
			this.LbMapDisplay.Size = new System.Drawing.Size(63, 13);
			this.LbMapDisplay.TabIndex = 18;
			this.LbMapDisplay.Text = "Map display";
			// 
			// TbTask
			// 
			this.TbTask.Location = new System.Drawing.Point(78, 55);
			this.TbTask.Name = "TbTask";
			this.TbTask.ReadOnly = true;
			this.TbTask.Size = new System.Drawing.Size(100, 20);
			this.TbTask.TabIndex = 14;
			// 
			// LbTask
			// 
			this.LbTask.AutoSize = true;
			this.LbTask.Location = new System.Drawing.Point(41, 58);
			this.LbTask.Name = "LbTask";
			this.LbTask.Size = new System.Drawing.Size(31, 13);
			this.LbTask.TabIndex = 13;
			this.LbTask.Text = "Task";
			// 
			// CkLateActivation
			// 
			this.CkLateActivation.AutoSize = true;
			this.CkLateActivation.Enabled = false;
			this.CkLateActivation.Location = new System.Drawing.Point(82, 107);
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
			this.CkPlayable.Location = new System.Drawing.Point(10, 107);
			this.CkPlayable.Name = "CkPlayable";
			this.CkPlayable.Size = new System.Drawing.Size(66, 17);
			this.CkPlayable.TabIndex = 11;
			this.CkPlayable.Text = "Playable";
			this.CkPlayable.UseVisualStyleBackColor = true;
			// 
			// LbId
			// 
			this.LbId.AutoSize = true;
			this.LbId.Location = new System.Drawing.Point(54, 6);
			this.LbId.Name = "LbId";
			this.LbId.Size = new System.Drawing.Size(18, 13);
			this.LbId.TabIndex = 9;
			this.LbId.Text = "ID";
			// 
			// TbId
			// 
			this.TbId.Location = new System.Drawing.Point(78, 3);
			this.TbId.Name = "TbId";
			this.TbId.ReadOnly = true;
			this.TbId.Size = new System.Drawing.Size(100, 20);
			this.TbId.TabIndex = 10;
			// 
			// FrmAssetDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1259, 679);
			this.Controls.Add(this.PnMissionMap);
			this.Controls.Add(this.PnData);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmAssetDetail";
			this.Text = "FrmGroupDetail";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAssetDetail_FormClosed);
			this.PnData.ResumeLayout(false);
			this.PnData.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label LbName;
		private System.Windows.Forms.TextBox TbName;
		private System.Windows.Forms.ComboBox CbCategory;
		private System.Windows.Forms.TextBox TbType;
		private System.Windows.Forms.Label LbType;
		private System.Windows.Forms.Label LbCategory;
		private System.Windows.Forms.Panel PnMissionMap;
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
	}
}