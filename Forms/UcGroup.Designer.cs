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
			this.TbFullDescription = new System.Windows.Forms.TextBox();
			this.LbDisplayName = new System.Windows.Forms.Label();
			this.TbDisplayName = new System.Windows.Forms.TextBox();
			this.LbClass = new System.Windows.Forms.Label();
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
			this.TbAttributes.Size = new System.Drawing.Size(412, 20);
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
			this.DgvUnits.Location = new System.Drawing.Point(3, 187);
			this.DgvUnits.Name = "DgvUnits";
			this.DgvUnits.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.DgvUnits.Size = new System.Drawing.Size(935, 264);
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
			this.TbType.Size = new System.Drawing.Size(412, 20);
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
			this.TbName.Size = new System.Drawing.Size(412, 20);
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
			// TbFullDescription
			// 
			this.TbFullDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbFullDescription.Location = new System.Drawing.Point(497, 57);
			this.TbFullDescription.Multiline = true;
			this.TbFullDescription.Name = "TbFullDescription";
			this.TbFullDescription.ReadOnly = true;
			this.TbFullDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbFullDescription.Size = new System.Drawing.Size(441, 124);
			this.TbFullDescription.TabIndex = 36;
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
			this.TbDisplayName.Size = new System.Drawing.Size(412, 20);
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
			// UcGroup
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.LbClass);
			this.Controls.Add(this.LbDisplayName);
			this.Controls.Add(this.TbDisplayName);
			this.Controls.Add(this.TbFullDescription);
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
		private System.Windows.Forms.TextBox TbFullDescription;
		private System.Windows.Forms.Label LbDisplayName;
		private System.Windows.Forms.TextBox TbDisplayName;
		private System.Windows.Forms.Label LbClass;
	}
}
