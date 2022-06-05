
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
			this.TbType = new System.Windows.Forms.TextBox();
			this.LbType = new System.Windows.Forms.Label();
			this.PnData = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.LbClass = new System.Windows.Forms.Label();
			this.TbAssetClass = new System.Windows.Forms.TextBox();
			this.TbSide = new System.Windows.Forms.TextBox();
			this.LbSide = new System.Windows.Forms.Label();
			this.TbFunction = new System.Windows.Forms.TextBox();
			this.LbFunction = new System.Windows.Forms.Label();
			this.LbDescription = new System.Windows.Forms.Label();
			this.CkIncluded = new System.Windows.Forms.CheckBox();
			this.CbMapDisplay = new System.Windows.Forms.ComboBox();
			this.LbMapDisplay = new System.Windows.Forms.Label();
			this.BtInformationReset = new System.Windows.Forms.Button();
			this.TbDescription = new System.Windows.Forms.TextBox();
			this.TbInformation = new System.Windows.Forms.TextBox();
			this.LbInformation = new System.Windows.Forms.Label();
			this.PnDcsData = new System.Windows.Forms.Panel();
			this.TbId = new System.Windows.Forms.TextBox();
			this.LbId = new System.Windows.Forms.Label();
			this.TbRadio = new System.Windows.Forms.TextBox();
			this.LbRadio = new System.Windows.Forms.Label();
			this.CkPlayable = new System.Windows.Forms.CheckBox();
			this.CkLateActivation = new System.Windows.Forms.CheckBox();
			this.LbTask = new System.Windows.Forms.Label();
			this.TbTask = new System.Windows.Forms.TextBox();
			this.LbUnits = new System.Windows.Forms.Label();
			this.DgvUnits = new DcsBriefop.FgControls.FgDataGridView();
			this.PnData.SuspendLayout();
			this.panel1.SuspendLayout();
			this.PnDcsData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgvUnits)).BeginInit();
			this.SuspendLayout();
			// 
			// LbName
			// 
			this.LbName.AutoSize = true;
			this.LbName.Location = new System.Drawing.Point(139, 8);
			this.LbName.Name = "LbName";
			this.LbName.Size = new System.Drawing.Size(35, 13);
			this.LbName.TabIndex = 1;
			this.LbName.Text = "Name";
			// 
			// TbName
			// 
			this.TbName.Location = new System.Drawing.Point(180, 5);
			this.TbName.Name = "TbName";
			this.TbName.ReadOnly = true;
			this.TbName.Size = new System.Drawing.Size(100, 20);
			this.TbName.TabIndex = 2;
			// 
			// TbType
			// 
			this.TbType.Location = new System.Drawing.Point(84, 28);
			this.TbType.Name = "TbType";
			this.TbType.ReadOnly = true;
			this.TbType.Size = new System.Drawing.Size(100, 20);
			this.TbType.TabIndex = 6;
			// 
			// LbType
			// 
			this.LbType.AutoSize = true;
			this.LbType.Location = new System.Drawing.Point(47, 31);
			this.LbType.Name = "LbType";
			this.LbType.Size = new System.Drawing.Size(31, 13);
			this.LbType.TabIndex = 5;
			this.LbType.Text = "Type";
			// 
			// PnData
			// 
			this.PnData.Controls.Add(this.DgvUnits);
			this.PnData.Controls.Add(this.panel1);
			this.PnData.Controls.Add(this.PnDcsData);
			this.PnData.Controls.Add(this.LbUnits);
			this.PnData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PnData.Location = new System.Drawing.Point(0, 0);
			this.PnData.Name = "PnData";
			this.PnData.Size = new System.Drawing.Size(531, 516);
			this.PnData.TabIndex = 10;
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.LbClass);
			this.panel1.Controls.Add(this.TbAssetClass);
			this.panel1.Controls.Add(this.TbSide);
			this.panel1.Controls.Add(this.LbSide);
			this.panel1.Controls.Add(this.TbFunction);
			this.panel1.Controls.Add(this.LbFunction);
			this.panel1.Controls.Add(this.LbDescription);
			this.panel1.Controls.Add(this.CkIncluded);
			this.panel1.Controls.Add(this.CbMapDisplay);
			this.panel1.Controls.Add(this.LbMapDisplay);
			this.panel1.Controls.Add(this.BtInformationReset);
			this.panel1.Controls.Add(this.TbDescription);
			this.panel1.Controls.Add(this.TbInformation);
			this.panel1.Controls.Add(this.LbInformation);
			this.panel1.Location = new System.Drawing.Point(3, 3);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(525, 158);
			this.panel1.TabIndex = 32;
			// 
			// LbClass
			// 
			this.LbClass.AutoSize = true;
			this.LbClass.Location = new System.Drawing.Point(209, 32);
			this.LbClass.Name = "LbClass";
			this.LbClass.Size = new System.Drawing.Size(32, 13);
			this.LbClass.TabIndex = 37;
			this.LbClass.Text = "Class";
			// 
			// TbAssetClass
			// 
			this.TbAssetClass.Location = new System.Drawing.Point(246, 29);
			this.TbAssetClass.Name = "TbAssetClass";
			this.TbAssetClass.ReadOnly = true;
			this.TbAssetClass.Size = new System.Drawing.Size(100, 20);
			this.TbAssetClass.TabIndex = 36;
			// 
			// TbSide
			// 
			this.TbSide.Location = new System.Drawing.Point(84, 29);
			this.TbSide.Name = "TbSide";
			this.TbSide.ReadOnly = true;
			this.TbSide.Size = new System.Drawing.Size(100, 20);
			this.TbSide.TabIndex = 35;
			// 
			// LbSide
			// 
			this.LbSide.AutoSize = true;
			this.LbSide.Location = new System.Drawing.Point(50, 32);
			this.LbSide.Name = "LbSide";
			this.LbSide.Size = new System.Drawing.Size(28, 13);
			this.LbSide.TabIndex = 34;
			this.LbSide.Text = "Side";
			// 
			// TbFunction
			// 
			this.TbFunction.Location = new System.Drawing.Point(420, 29);
			this.TbFunction.Name = "TbFunction";
			this.TbFunction.ReadOnly = true;
			this.TbFunction.Size = new System.Drawing.Size(100, 20);
			this.TbFunction.TabIndex = 31;
			// 
			// LbFunction
			// 
			this.LbFunction.AutoSize = true;
			this.LbFunction.Location = new System.Drawing.Point(366, 32);
			this.LbFunction.Name = "LbFunction";
			this.LbFunction.Size = new System.Drawing.Size(48, 13);
			this.LbFunction.TabIndex = 30;
			this.LbFunction.Text = "Function";
			// 
			// LbDescription
			// 
			this.LbDescription.AutoSize = true;
			this.LbDescription.Location = new System.Drawing.Point(18, 6);
			this.LbDescription.Name = "LbDescription";
			this.LbDescription.Size = new System.Drawing.Size(60, 13);
			this.LbDescription.TabIndex = 29;
			this.LbDescription.Text = "Description";
			// 
			// CkIncluded
			// 
			this.CkIncluded.AutoSize = true;
			this.CkIncluded.Location = new System.Drawing.Point(373, 59);
			this.CkIncluded.Name = "CkIncluded";
			this.CkIncluded.Size = new System.Drawing.Size(115, 17);
			this.CkIncluded.TabIndex = 28;
			this.CkIncluded.Text = "Included in briefing";
			this.CkIncluded.UseVisualStyleBackColor = true;
			// 
			// CbMapDisplay
			// 
			this.CbMapDisplay.FormattingEnabled = true;
			this.CbMapDisplay.Location = new System.Drawing.Point(84, 55);
			this.CbMapDisplay.Name = "CbMapDisplay";
			this.CbMapDisplay.Size = new System.Drawing.Size(244, 21);
			this.CbMapDisplay.TabIndex = 17;
			// 
			// LbMapDisplay
			// 
			this.LbMapDisplay.AutoSize = true;
			this.LbMapDisplay.Location = new System.Drawing.Point(15, 58);
			this.LbMapDisplay.Name = "LbMapDisplay";
			this.LbMapDisplay.Size = new System.Drawing.Size(63, 13);
			this.LbMapDisplay.TabIndex = 18;
			this.LbMapDisplay.Text = "Map display";
			// 
			// BtInformationReset
			// 
			this.BtInformationReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.BtInformationReset.Location = new System.Drawing.Point(389, 87);
			this.BtInformationReset.Name = "BtInformationReset";
			this.BtInformationReset.Size = new System.Drawing.Size(131, 21);
			this.BtInformationReset.TabIndex = 23;
			this.BtInformationReset.Text = "Reset information";
			this.BtInformationReset.UseVisualStyleBackColor = true;
			this.BtInformationReset.Click += new System.EventHandler(this.BtInformationReset_Click);
			// 
			// TbDescription
			// 
			this.TbDescription.Location = new System.Drawing.Point(84, 3);
			this.TbDescription.Name = "TbDescription";
			this.TbDescription.ReadOnly = true;
			this.TbDescription.Size = new System.Drawing.Size(436, 20);
			this.TbDescription.TabIndex = 25;
			// 
			// TbInformation
			// 
			this.TbInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbInformation.Location = new System.Drawing.Point(3, 110);
			this.TbInformation.Multiline = true;
			this.TbInformation.Name = "TbInformation";
			this.TbInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbInformation.Size = new System.Drawing.Size(517, 43);
			this.TbInformation.TabIndex = 22;
			// 
			// LbInformation
			// 
			this.LbInformation.AutoSize = true;
			this.LbInformation.Location = new System.Drawing.Point(3, 95);
			this.LbInformation.Name = "LbInformation";
			this.LbInformation.Size = new System.Drawing.Size(59, 13);
			this.LbInformation.TabIndex = 21;
			this.LbInformation.Text = "Information";
			// 
			// PnDcsData
			// 
			this.PnDcsData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnDcsData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnDcsData.Controls.Add(this.TbName);
			this.PnDcsData.Controls.Add(this.LbName);
			this.PnDcsData.Controls.Add(this.TbId);
			this.PnDcsData.Controls.Add(this.LbId);
			this.PnDcsData.Controls.Add(this.TbType);
			this.PnDcsData.Controls.Add(this.LbType);
			this.PnDcsData.Controls.Add(this.TbRadio);
			this.PnDcsData.Controls.Add(this.LbRadio);
			this.PnDcsData.Controls.Add(this.CkPlayable);
			this.PnDcsData.Controls.Add(this.CkLateActivation);
			this.PnDcsData.Controls.Add(this.LbTask);
			this.PnDcsData.Controls.Add(this.TbTask);
			this.PnDcsData.Location = new System.Drawing.Point(3, 167);
			this.PnDcsData.Name = "PnDcsData";
			this.PnDcsData.Size = new System.Drawing.Size(525, 55);
			this.PnDcsData.TabIndex = 31;
			// 
			// TbId
			// 
			this.TbId.Location = new System.Drawing.Point(85, 3);
			this.TbId.Name = "TbId";
			this.TbId.ReadOnly = true;
			this.TbId.Size = new System.Drawing.Size(41, 20);
			this.TbId.TabIndex = 10;
			// 
			// LbId
			// 
			this.LbId.AutoSize = true;
			this.LbId.Location = new System.Drawing.Point(61, 6);
			this.LbId.Name = "LbId";
			this.LbId.Size = new System.Drawing.Size(18, 13);
			this.LbId.TabIndex = 9;
			this.LbId.Text = "ID";
			// 
			// TbRadio
			// 
			this.TbRadio.Location = new System.Drawing.Point(420, 28);
			this.TbRadio.Name = "TbRadio";
			this.TbRadio.ReadOnly = true;
			this.TbRadio.Size = new System.Drawing.Size(100, 20);
			this.TbRadio.TabIndex = 20;
			// 
			// LbRadio
			// 
			this.LbRadio.AutoSize = true;
			this.LbRadio.Location = new System.Drawing.Point(379, 31);
			this.LbRadio.Name = "LbRadio";
			this.LbRadio.Size = new System.Drawing.Size(35, 13);
			this.LbRadio.TabIndex = 19;
			this.LbRadio.Text = "Radio";
			// 
			// CkPlayable
			// 
			this.CkPlayable.AutoSize = true;
			this.CkPlayable.Enabled = false;
			this.CkPlayable.Location = new System.Drawing.Point(347, 5);
			this.CkPlayable.Name = "CkPlayable";
			this.CkPlayable.Size = new System.Drawing.Size(66, 17);
			this.CkPlayable.TabIndex = 11;
			this.CkPlayable.Text = "Playable";
			this.CkPlayable.UseVisualStyleBackColor = true;
			// 
			// CkLateActivation
			// 
			this.CkLateActivation.AutoSize = true;
			this.CkLateActivation.Enabled = false;
			this.CkLateActivation.Location = new System.Drawing.Point(419, 5);
			this.CkLateActivation.Name = "CkLateActivation";
			this.CkLateActivation.Size = new System.Drawing.Size(96, 17);
			this.CkLateActivation.TabIndex = 12;
			this.CkLateActivation.Text = "Late activation";
			this.CkLateActivation.UseVisualStyleBackColor = true;
			// 
			// LbTask
			// 
			this.LbTask.AutoSize = true;
			this.LbTask.Location = new System.Drawing.Point(209, 31);
			this.LbTask.Name = "LbTask";
			this.LbTask.Size = new System.Drawing.Size(31, 13);
			this.LbTask.TabIndex = 13;
			this.LbTask.Text = "Task";
			// 
			// TbTask
			// 
			this.TbTask.Location = new System.Drawing.Point(246, 28);
			this.TbTask.Name = "TbTask";
			this.TbTask.ReadOnly = true;
			this.TbTask.Size = new System.Drawing.Size(100, 20);
			this.TbTask.TabIndex = 14;
			// 
			// LbUnits
			// 
			this.LbUnits.AutoSize = true;
			this.LbUnits.Location = new System.Drawing.Point(4, 225);
			this.LbUnits.Name = "LbUnits";
			this.LbUnits.Size = new System.Drawing.Size(31, 13);
			this.LbUnits.TabIndex = 29;
			this.LbUnits.Text = "Units";
			// 
			// DgvUnits
			// 
			this.DgvUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvUnits.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvUnits.DtSource = null;
			this.DgvUnits.FilterAndSortEnabled = true;
			this.DgvUnits.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvUnits.Location = new System.Drawing.Point(5, 241);
			this.DgvUnits.Name = "DgvUnits";
			this.DgvUnits.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvUnits.Size = new System.Drawing.Size(523, 272);
			this.DgvUnits.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.DgvUnits.TabIndex = 33;
			// 
			// FrmAssetDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(531, 516);
			this.Controls.Add(this.PnData);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			this.Name = "FrmAssetDetail";
			this.ShowInTaskbar = false;
			this.Text = "Asset detail";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmAssetDetail_FormClosing);
			this.PnData.ResumeLayout(false);
			this.PnData.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.PnDcsData.ResumeLayout(false);
			this.PnDcsData.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgvUnits)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label LbName;
		private System.Windows.Forms.TextBox TbName;
		private System.Windows.Forms.TextBox TbType;
		private System.Windows.Forms.Label LbType;
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
		private System.Windows.Forms.TextBox TbDescription;
		private System.Windows.Forms.CheckBox CkIncluded;
		private System.Windows.Forms.Label LbUnits;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label LbDescription;
		private System.Windows.Forms.Panel PnDcsData;
		private System.Windows.Forms.TextBox TbAssetClass;
		private System.Windows.Forms.TextBox TbSide;
		private System.Windows.Forms.Label LbSide;
		private System.Windows.Forms.TextBox TbFunction;
		private System.Windows.Forms.Label LbFunction;
		private System.Windows.Forms.Label LbClass;
		private FgControls.FgDataGridView DgvUnits;
	}
}