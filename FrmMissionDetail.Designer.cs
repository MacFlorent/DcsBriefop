
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
			this.LbDescription = new System.Windows.Forms.Label();
			this.TbDescription = new System.Windows.Forms.TextBox();
			this.TbType = new System.Windows.Forms.TextBox();
			this.LbType = new System.Windows.Forms.Label();
			this.TbAssetInformation = new System.Windows.Forms.TextBox();
			this.LbAssetInformation = new System.Windows.Forms.Label();
			this.DgvRoutePoints = new System.Windows.Forms.DataGridView();
			this.LbWaypoints = new System.Windows.Forms.Label();
			this.LbThreats = new System.Windows.Forms.Label();
			this.TbInformation = new System.Windows.Forms.TextBox();
			this.LbInformation = new System.Windows.Forms.Label();
			this.TbTask = new System.Windows.Forms.TextBox();
			this.LbTask = new System.Windows.Forms.Label();
			this.LbId = new System.Windows.Forms.Label();
			this.TbId = new System.Windows.Forms.TextBox();
			this.SplitContainer = new System.Windows.Forms.SplitContainer();
			this.PnFlight = new System.Windows.Forms.Panel();
			this.AdgvThreats = new Zuby.ADGV.AdvancedDataGridView();
			((System.ComponentModel.ISupportInitialize)(this.DgvRoutePoints)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
			this.SplitContainer.Panel1.SuspendLayout();
			this.SplitContainer.SuspendLayout();
			this.PnFlight.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AdgvThreats)).BeginInit();
			this.SuspendLayout();
			// 
			// LbDescription
			// 
			this.LbDescription.AutoSize = true;
			this.LbDescription.Location = new System.Drawing.Point(5, 32);
			this.LbDescription.Name = "LbDescription";
			this.LbDescription.Size = new System.Drawing.Size(35, 13);
			this.LbDescription.TabIndex = 1;
			this.LbDescription.Text = "Desc.";
			// 
			// TbDescription
			// 
			this.TbDescription.Location = new System.Drawing.Point(46, 29);
			this.TbDescription.Name = "TbDescription";
			this.TbDescription.ReadOnly = true;
			this.TbDescription.Size = new System.Drawing.Size(100, 20);
			this.TbDescription.TabIndex = 2;
			// 
			// TbType
			// 
			this.TbType.Location = new System.Drawing.Point(189, 3);
			this.TbType.Name = "TbType";
			this.TbType.ReadOnly = true;
			this.TbType.Size = new System.Drawing.Size(100, 20);
			this.TbType.TabIndex = 6;
			// 
			// LbType
			// 
			this.LbType.AutoSize = true;
			this.LbType.Location = new System.Drawing.Point(152, 6);
			this.LbType.Name = "LbType";
			this.LbType.Size = new System.Drawing.Size(31, 13);
			this.LbType.TabIndex = 5;
			this.LbType.Text = "Type";
			// 
			// TbAssetInformation
			// 
			this.TbAssetInformation.Location = new System.Drawing.Point(189, 29);
			this.TbAssetInformation.Name = "TbAssetInformation";
			this.TbAssetInformation.ReadOnly = true;
			this.TbAssetInformation.Size = new System.Drawing.Size(243, 20);
			this.TbAssetInformation.TabIndex = 28;
			// 
			// LbAssetInformation
			// 
			this.LbAssetInformation.AutoSize = true;
			this.LbAssetInformation.Location = new System.Drawing.Point(152, 32);
			this.LbAssetInformation.Name = "LbAssetInformation";
			this.LbAssetInformation.Size = new System.Drawing.Size(28, 13);
			this.LbAssetInformation.TabIndex = 27;
			this.LbAssetInformation.Text = "Info.";
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
			this.DgvRoutePoints.Location = new System.Drawing.Point(6, 148);
			this.DgvRoutePoints.Name = "DgvRoutePoints";
			this.DgvRoutePoints.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.DgvRoutePoints.Size = new System.Drawing.Size(445, 153);
			this.DgvRoutePoints.TabIndex = 26;
			// 
			// LbWaypoints
			// 
			this.LbWaypoints.AutoSize = true;
			this.LbWaypoints.Location = new System.Drawing.Point(8, 132);
			this.LbWaypoints.Name = "LbWaypoints";
			this.LbWaypoints.Size = new System.Drawing.Size(57, 13);
			this.LbWaypoints.TabIndex = 25;
			this.LbWaypoints.Text = "Waypoints";
			// 
			// LbThreats
			// 
			this.LbThreats.AutoSize = true;
			this.LbThreats.Location = new System.Drawing.Point(8, 304);
			this.LbThreats.Name = "LbThreats";
			this.LbThreats.Size = new System.Drawing.Size(43, 13);
			this.LbThreats.TabIndex = 23;
			this.LbThreats.Text = "Threats";
			// 
			// TbInformation
			// 
			this.TbInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbInformation.Location = new System.Drawing.Point(6, 82);
			this.TbInformation.Multiline = true;
			this.TbInformation.Name = "TbInformation";
			this.TbInformation.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbInformation.Size = new System.Drawing.Size(445, 47);
			this.TbInformation.TabIndex = 22;
			// 
			// LbInformation
			// 
			this.LbInformation.AutoSize = true;
			this.LbInformation.Location = new System.Drawing.Point(3, 66);
			this.LbInformation.Name = "LbInformation";
			this.LbInformation.Size = new System.Drawing.Size(96, 13);
			this.LbInformation.TabIndex = 21;
			this.LbInformation.Text = "Mission information";
			// 
			// TbTask
			// 
			this.TbTask.Location = new System.Drawing.Point(332, 3);
			this.TbTask.Name = "TbTask";
			this.TbTask.ReadOnly = true;
			this.TbTask.Size = new System.Drawing.Size(100, 20);
			this.TbTask.TabIndex = 14;
			// 
			// LbTask
			// 
			this.LbTask.AutoSize = true;
			this.LbTask.Location = new System.Drawing.Point(295, 6);
			this.LbTask.Name = "LbTask";
			this.LbTask.Size = new System.Drawing.Size(31, 13);
			this.LbTask.TabIndex = 13;
			this.LbTask.Text = "Task";
			// 
			// LbId
			// 
			this.LbId.AutoSize = true;
			this.LbId.Location = new System.Drawing.Point(22, 6);
			this.LbId.Name = "LbId";
			this.LbId.Size = new System.Drawing.Size(18, 13);
			this.LbId.TabIndex = 9;
			this.LbId.Text = "ID";
			// 
			// TbId
			// 
			this.TbId.Location = new System.Drawing.Point(46, 3);
			this.TbId.Name = "TbId";
			this.TbId.ReadOnly = true;
			this.TbId.Size = new System.Drawing.Size(100, 20);
			this.TbId.TabIndex = 10;
			// 
			// SplitContainer
			// 
			this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SplitContainer.Location = new System.Drawing.Point(0, 0);
			this.SplitContainer.Name = "SplitContainer";
			// 
			// SplitContainer.Panel1
			// 
			this.SplitContainer.Panel1.Controls.Add(this.AdgvThreats);
			this.SplitContainer.Panel1.Controls.Add(this.PnFlight);
			this.SplitContainer.Panel1.Controls.Add(this.DgvRoutePoints);
			this.SplitContainer.Panel1.Controls.Add(this.LbWaypoints);
			this.SplitContainer.Panel1.Controls.Add(this.LbThreats);
			this.SplitContainer.Panel1.Controls.Add(this.TbInformation);
			this.SplitContainer.Panel1.Controls.Add(this.LbInformation);
			this.SplitContainer.Size = new System.Drawing.Size(1259, 679);
			this.SplitContainer.SplitterDistance = 454;
			this.SplitContainer.TabIndex = 11;
			// 
			// PnFlight
			// 
			this.PnFlight.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PnFlight.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.PnFlight.Controls.Add(this.TbId);
			this.PnFlight.Controls.Add(this.TbAssetInformation);
			this.PnFlight.Controls.Add(this.LbAssetInformation);
			this.PnFlight.Controls.Add(this.LbId);
			this.PnFlight.Controls.Add(this.TbDescription);
			this.PnFlight.Controls.Add(this.TbType);
			this.PnFlight.Controls.Add(this.LbType);
			this.PnFlight.Controls.Add(this.LbDescription);
			this.PnFlight.Controls.Add(this.TbTask);
			this.PnFlight.Controls.Add(this.LbTask);
			this.PnFlight.Location = new System.Drawing.Point(6, 3);
			this.PnFlight.Name = "PnFlight";
			this.PnFlight.Size = new System.Drawing.Size(445, 56);
			this.PnFlight.TabIndex = 29;
			// 
			// AdgvThreats
			// 
			this.AdgvThreats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.AdgvThreats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.AdgvThreats.FilterAndSortEnabled = true;
			this.AdgvThreats.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
			this.AdgvThreats.Location = new System.Drawing.Point(6, 320);
			this.AdgvThreats.Name = "AdgvThreats";
			this.AdgvThreats.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.AdgvThreats.Size = new System.Drawing.Size(445, 356);
			this.AdgvThreats.SortStringChangedInvokeBeforeDatasourceUpdate = true;
			this.AdgvThreats.TabIndex = 34;
			// 
			// FrmMissionDetail
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1259, 679);
			this.Controls.Add(this.SplitContainer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "FrmMissionDetail";
			this.ShowInTaskbar = false;
			this.Text = "FrmMissionDetail";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMissionDetail_FormClosing);
			((System.ComponentModel.ISupportInitialize)(this.DgvRoutePoints)).EndInit();
			this.SplitContainer.Panel1.ResumeLayout(false);
			this.SplitContainer.Panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
			this.SplitContainer.ResumeLayout(false);
			this.PnFlight.ResumeLayout(false);
			this.PnFlight.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.AdgvThreats)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label LbDescription;
		private System.Windows.Forms.TextBox TbDescription;
		private System.Windows.Forms.TextBox TbType;
		private System.Windows.Forms.Label LbType;
		private System.Windows.Forms.Label LbId;
		private System.Windows.Forms.TextBox TbId;
		private System.Windows.Forms.TextBox TbTask;
		private System.Windows.Forms.Label LbTask;
		private System.Windows.Forms.Label LbInformation;
		private System.Windows.Forms.TextBox TbInformation;
		private System.Windows.Forms.Label LbThreats;
		private System.Windows.Forms.DataGridView DgvRoutePoints;
		private System.Windows.Forms.Label LbWaypoints;
		private System.Windows.Forms.TextBox TbAssetInformation;
		private System.Windows.Forms.Label LbAssetInformation;
		private System.Windows.Forms.SplitContainer SplitContainer;
		private System.Windows.Forms.Panel PnFlight;
		private Zuby.ADGV.AdvancedDataGridView AdgvThreats;
	}
}