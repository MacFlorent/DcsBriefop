
namespace DcsBriefop.Forms
{
	partial class UcMap
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
			BtRefresh = new Button();
			PnSelectionDetail = new Panel();
			CkAddMarker = new CheckBox();
			BtAreaSet = new Button();
			BtAreaRecall = new Button();
			MapControl = new Mapsui.UI.WindowsForms.MapControl();
			SuspendLayout();
			// 
			// BtRefresh
			// 
			BtRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtRefresh.Location = new Point(644, 469);
			BtRefresh.Margin = new Padding(4, 3, 4, 3);
			BtRefresh.Name = "BtRefresh";
			BtRefresh.Size = new Size(118, 27);
			BtRefresh.TabIndex = 6;
			BtRefresh.Text = "Refresh overlays";
			BtRefresh.UseVisualStyleBackColor = true;
			BtRefresh.Click += BtRefresh_Click;
			// 
			// PnSelectionDetail
			// 
			PnSelectionDetail.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			PnSelectionDetail.BackColor = SystemColors.ControlDark;
			PnSelectionDetail.Location = new Point(570, 247);
			PnSelectionDetail.Margin = new Padding(4, 3, 4, 3);
			PnSelectionDetail.Name = "PnSelectionDetail";
			PnSelectionDetail.Size = new Size(331, 220);
			PnSelectionDetail.TabIndex = 5;
			// 
			// CkAddMarker
			// 
			CkAddMarker.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			CkAddMarker.AutoSize = true;
			CkAddMarker.Location = new Point(790, 475);
			CkAddMarker.Margin = new Padding(4, 3, 4, 3);
			CkAddMarker.Name = "CkAddMarker";
			CkAddMarker.Size = new Size(88, 19);
			CkAddMarker.TabIndex = 2;
			CkAddMarker.Text = "Add marker";
			CkAddMarker.UseVisualStyleBackColor = true;
			// 
			// BtAreaSet
			// 
			BtAreaSet.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtAreaSet.Location = new Point(392, 469);
			BtAreaSet.Margin = new Padding(4, 3, 4, 3);
			BtAreaSet.Name = "BtAreaSet";
			BtAreaSet.Size = new Size(118, 27);
			BtAreaSet.TabIndex = 0;
			BtAreaSet.Text = "Set display area";
			BtAreaSet.UseVisualStyleBackColor = true;
			BtAreaSet.Click += BtAreaSet_Click;
			// 
			// BtAreaRecall
			// 
			BtAreaRecall.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			BtAreaRecall.Location = new Point(518, 469);
			BtAreaRecall.Margin = new Padding(4, 3, 4, 3);
			BtAreaRecall.Name = "BtAreaRecall";
			BtAreaRecall.Size = new Size(118, 27);
			BtAreaRecall.TabIndex = 1;
			BtAreaRecall.Text = "Recall display area";
			BtAreaRecall.UseVisualStyleBackColor = true;
			BtAreaRecall.Click += BtAreaRecall_Click;
			// 
			// MapControl
			// 
			MapControl.AutoSize = true;
			MapControl.BackColor = Color.White;
			MapControl.Dock = DockStyle.Fill;
			MapControl.Location = new Point(0, 0);
			MapControl.Margin = new Padding(4, 3, 4, 3);
			MapControl.Name = "MapControl";
			MapControl.Size = new Size(905, 497);
			MapControl.TabIndex = 5;
			MapControl.MapPointerPressed += MapControl_MapPointerPressed;
			MapControl.MapPointerMoved += MapControl_MapPointerMoved;
			MapControl.MapPointerReleased += MapControl_MapPointerReleased;
			MapControl.MapTapped += MapControl_MapTapped;
			// 
			// UcMap
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			Controls.Add(BtRefresh);
			Controls.Add(PnSelectionDetail);
			Controls.Add(CkAddMarker);
			Controls.Add(BtAreaSet);
			Controls.Add(BtAreaRecall);
			Controls.Add(MapControl);
			Margin = new Padding(4, 3, 4, 3);
			Name = "UcMap";
			Size = new Size(905, 497);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion
		private Mapsui.UI.WindowsForms.MapControl MapControl;
		private System.Windows.Forms.Button BtAreaRecall;
		private System.Windows.Forms.Button BtAreaSet;
		private System.Windows.Forms.CheckBox CkAddMarker;
		private System.Windows.Forms.Panel PnSelectionDetail;
		private System.Windows.Forms.Button BtRefresh;
	}
}
