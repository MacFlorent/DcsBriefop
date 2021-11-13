
namespace DcsBriefop
{
	partial class FrmMissionDetail
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
			this.PnMissionMap = new System.Windows.Forms.Panel();
			this.PnData = new System.Windows.Forms.Panel();
			this.TbAssetInformation = new System.Windows.Forms.TextBox();
			this.LbAssetInformation = new System.Windows.Forms.Label();
			this.DgvRoutePoints = new System.Windows.Forms.DataGridView();
			this.LbWaypoints = new System.Windows.Forms.Label();
			this.DgvTargets = new System.Windows.Forms.DataGridView();
			this.LbTargets = new System.Windows.Forms.Label();
			this.TbInformation = new System.Windows.Forms.TextBox();
			this.LbInformation = new System.Windows.Forms.Label();
			this.TbTask = new System.Windows.Forms.TextBox();
			this.LbTask = new System.Windows.Forms.Label();
			this.LbId = new System.Windows.Forms.Label();
			this.TbId = new System.Windows.Forms.TextBox();
			this.PnData.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgvRoutePoints)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DgvTargets)).BeginInit();
			this.SuspendLayout();
			// 
			// LbName
			// 
			this.LbName.AutoSize = true;
			this.LbName.Location = new System.Drawing.Point(183, 9);
			this.LbName.Name = "LbName";
			this.LbName.Size = new System.Drawing.Size(35, 13);
			this.LbName.TabIndex = 1;
			this.LbName.Text = "Name";
			// 
			// TbName
			// 
			this.TbName.Location = new System.Drawing.Point(224, 3);
			this.TbName.Name = "TbName";
			this.TbName.ReadOnly = true;
			this.TbName.Size = new System.Drawing.Size(100, 20);
			this.TbName.TabIndex = 2;
			// 
			// TbType
			// 
			this.TbType.Location = new System.Drawing.Point(224, 29);
			this.TbType.Name = "TbType";
			this.TbType.ReadOnly = true;
			this.TbType.Size = new System.Drawing.Size(100, 20);
			this.TbType.TabIndex = 6;
			// 
			// LbType
			// 
			this.LbType.AutoSize = true;
			this.LbType.Location = new System.Drawing.Point(187, 32);
			this.LbType.Name = "LbType";
			this.LbType.Size = new System.Drawing.Size(31, 13);
			this.LbType.TabIndex = 5;
			this.LbType.Text = "Type";
			// 
			// PnMissionMap
			// 
			this.PnMissionMap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PnMissionMap.Location = new System.Drawing.Point(400, 0);
			this.PnMissionMap.Name = "PnMissionMap";
			this.PnMissionMap.Size = new System.Drawing.Size(859, 679);
			this.PnMissionMap.TabIndex = 9;
			// 
			// PnData
			// 
			this.PnData.Controls.Add(this.TbAssetInformation);
			this.PnData.Controls.Add(this.LbAssetInformation);
			this.PnData.Controls.Add(this.DgvRoutePoints);
			this.PnData.Controls.Add(this.LbWaypoints);
			this.PnData.Controls.Add(this.DgvTargets);
			this.PnData.Controls.Add(this.LbTargets);
			this.PnData.Controls.Add(this.TbInformation);
			this.PnData.Controls.Add(this.LbInformation);
			this.PnData.Controls.Add(this.TbTask);
			this.PnData.Controls.Add(this.LbTask);
			this.PnData.Controls.Add(this.LbId);
			this.PnData.Controls.Add(this.TbId);
			this.PnData.Controls.Add(this.TbType);
			this.PnData.Controls.Add(this.LbName);
			this.PnData.Controls.Add(this.TbName);
			this.PnData.Controls.Add(this.LbType);
			this.PnData.Dock = System.Windows.Forms.DockStyle.Left;
			this.PnData.Location = new System.Drawing.Point(0, 0);
			this.PnData.Name = "PnData";
			this.PnData.Size = new System.Drawing.Size(400, 679);
			this.PnData.TabIndex = 10;
			// 
			// TbAssetInformation
			// 
			this.TbAssetInformation.Location = new System.Drawing.Point(134, 55);
			this.TbAssetInformation.Name = "TbAssetInformation";
			this.TbAssetInformation.ReadOnly = true;
			this.TbAssetInformation.Size = new System.Drawing.Size(190, 20);
			this.TbAssetInformation.TabIndex = 28;
			// 
			// LbAssetInformation
			// 
			this.LbAssetInformation.AutoSize = true;
			this.LbAssetInformation.Location = new System.Drawing.Point(41, 58);
			this.LbAssetInformation.Name = "LbAssetInformation";
			this.LbAssetInformation.Size = new System.Drawing.Size(87, 13);
			this.LbAssetInformation.TabIndex = 27;
			this.LbAssetInformation.Text = "Asset information";
			// 
			// DgvRoutePoints
			// 
			this.DgvRoutePoints.AllowUserToAddRows = false;
			this.DgvRoutePoints.AllowUserToDeleteRows = false;
			this.DgvRoutePoints.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvRoutePoints.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.DgvRoutePoints.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvRoutePoints.Location = new System.Drawing.Point(2, 215);
			this.DgvRoutePoints.Name = "DgvRoutePoints";
			this.DgvRoutePoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvRoutePoints.Size = new System.Drawing.Size(394, 150);
			this.DgvRoutePoints.TabIndex = 26;
			// 
			// LbWaypoints
			// 
			this.LbWaypoints.AutoSize = true;
			this.LbWaypoints.Location = new System.Drawing.Point(2, 199);
			this.LbWaypoints.Name = "LbWaypoints";
			this.LbWaypoints.Size = new System.Drawing.Size(57, 13);
			this.LbWaypoints.TabIndex = 25;
			this.LbWaypoints.Text = "Waypoints";
			// 
			// DgvTargets
			// 
			this.DgvTargets.AllowUserToAddRows = false;
			this.DgvTargets.AllowUserToDeleteRows = false;
			this.DgvTargets.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvTargets.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
			this.DgvTargets.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvTargets.Location = new System.Drawing.Point(5, 387);
			this.DgvTargets.Name = "DgvTargets";
			this.DgvTargets.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvTargets.Size = new System.Drawing.Size(394, 100);
			this.DgvTargets.TabIndex = 24;
			// 
			// LbTargets
			// 
			this.LbTargets.AutoSize = true;
			this.LbTargets.Location = new System.Drawing.Point(5, 371);
			this.LbTargets.Name = "LbTargets";
			this.LbTargets.Size = new System.Drawing.Size(43, 13);
			this.LbTargets.TabIndex = 23;
			this.LbTargets.Text = "Targets";
			// 
			// TbInformation
			// 
			this.TbInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbInformation.Location = new System.Drawing.Point(0, 102);
			this.TbInformation.Multiline = true;
			this.TbInformation.Name = "TbInformation";
			this.TbInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbInformation.Size = new System.Drawing.Size(396, 84);
			this.TbInformation.TabIndex = 22;
			this.TbInformation.TextChanged += new System.EventHandler(this.TbInformation_TextChanged);
			// 
			// LbInformation
			// 
			this.LbInformation.AutoSize = true;
			this.LbInformation.Location = new System.Drawing.Point(5, 86);
			this.LbInformation.Name = "LbInformation";
			this.LbInformation.Size = new System.Drawing.Size(96, 13);
			this.LbInformation.TabIndex = 21;
			this.LbInformation.Text = "Mission information";
			// 
			// TbTask
			// 
			this.TbTask.Location = new System.Drawing.Point(78, 29);
			this.TbTask.Name = "TbTask";
			this.TbTask.ReadOnly = true;
			this.TbTask.Size = new System.Drawing.Size(100, 20);
			this.TbTask.TabIndex = 14;
			// 
			// LbTask
			// 
			this.LbTask.AutoSize = true;
			this.LbTask.Location = new System.Drawing.Point(41, 32);
			this.LbTask.Name = "LbTask";
			this.LbTask.Size = new System.Drawing.Size(31, 13);
			this.LbTask.TabIndex = 13;
			this.LbTask.Text = "Task";
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
			// FrmMissionDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1259, 679);
			this.Controls.Add(this.PnMissionMap);
			this.Controls.Add(this.PnData);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmMissionDetail";
			this.ShowInTaskbar = false;
			this.Text = "FrmGroupDetail";
			this.PnData.ResumeLayout(false);
			this.PnData.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.DgvRoutePoints)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DgvTargets)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label LbName;
		private System.Windows.Forms.TextBox TbName;
		private System.Windows.Forms.TextBox TbType;
		private System.Windows.Forms.Label LbType;
		private System.Windows.Forms.Panel PnMissionMap;
		private System.Windows.Forms.Panel PnData;
		private System.Windows.Forms.Label LbId;
		private System.Windows.Forms.TextBox TbId;
		private System.Windows.Forms.TextBox TbTask;
		private System.Windows.Forms.Label LbTask;
		private System.Windows.Forms.Label LbInformation;
		private System.Windows.Forms.TextBox TbInformation;
		private System.Windows.Forms.Label LbTargets;
		private System.Windows.Forms.DataGridView DgvRoutePoints;
		private System.Windows.Forms.Label LbWaypoints;
		private System.Windows.Forms.DataGridView DgvTargets;
		private System.Windows.Forms.TextBox TbAssetInformation;
		private System.Windows.Forms.Label LbAssetInformation;
	}
}