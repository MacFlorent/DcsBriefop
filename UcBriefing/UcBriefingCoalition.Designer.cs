
namespace DcsBriefop.UcBriefing
{
	partial class UcBriefingCoalition
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
			this.TbTask = new System.Windows.Forms.TextBox();
			this.LbTask = new System.Windows.Forms.Label();
			this.DgvFlights = new System.Windows.Forms.DataGridView();
			this.LbBullseye = new System.Windows.Forms.Label();
			this.LbFlights = new System.Windows.Forms.Label();
			this.TbBullseyeCoordinates = new System.Windows.Forms.TextBox();
			this.TbBullseyeDescription = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.DgvFlights)).BeginInit();
			this.SuspendLayout();
			// 
			// TbTask
			// 
			this.TbTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbTask.Location = new System.Drawing.Point(6, 176);
			this.TbTask.Multiline = true;
			this.TbTask.Name = "TbTask";
			this.TbTask.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbTask.Size = new System.Drawing.Size(659, 128);
			this.TbTask.TabIndex = 3;
			this.TbTask.Validated += new System.EventHandler(this.TbTask_Validated);
			// 
			// LbTask
			// 
			this.LbTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbTask.Location = new System.Drawing.Point(6, 154);
			this.LbTask.Name = "LbTask";
			this.LbTask.Size = new System.Drawing.Size(662, 20);
			this.LbTask.TabIndex = 2;
			this.LbTask.Text = "Ops";
			// 
			// DgvFlights
			// 
			this.DgvFlights.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.DgvFlights.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DgvFlights.Location = new System.Drawing.Point(3, 340);
			this.DgvFlights.Name = "DgvFlights";
			this.DgvFlights.Size = new System.Drawing.Size(659, 296);
			this.DgvFlights.TabIndex = 4;
			this.DgvFlights.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgvFlights_CellDoubleClick);
			// 
			// LbBullseye
			// 
			this.LbBullseye.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbBullseye.Location = new System.Drawing.Point(3, 0);
			this.LbBullseye.Name = "LbBullseye";
			this.LbBullseye.Size = new System.Drawing.Size(662, 24);
			this.LbBullseye.TabIndex = 5;
			this.LbBullseye.Text = "Bullseye";
			// 
			// LbFlights
			// 
			this.LbFlights.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.LbFlights.Location = new System.Drawing.Point(3, 307);
			this.LbFlights.Name = "LbFlights";
			this.LbFlights.Size = new System.Drawing.Size(662, 24);
			this.LbFlights.TabIndex = 6;
			this.LbFlights.Text = "Flights";
			// 
			// TbBullseyeCoordinates
			// 
			this.TbBullseyeCoordinates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbBullseyeCoordinates.Location = new System.Drawing.Point(3, 14);
			this.TbBullseyeCoordinates.Multiline = true;
			this.TbBullseyeCoordinates.Name = "TbBullseyeCoordinates";
			this.TbBullseyeCoordinates.ReadOnly = true;
			this.TbBullseyeCoordinates.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbBullseyeCoordinates.Size = new System.Drawing.Size(659, 75);
			this.TbBullseyeCoordinates.TabIndex = 7;
			this.TbBullseyeCoordinates.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.TbBullseyeCoordinates.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TbBullseyeCoordinates_MouseDoubleClick);
			// 
			// TbBullseyeDescription
			// 
			this.TbBullseyeDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TbBullseyeDescription.Location = new System.Drawing.Point(3, 95);
			this.TbBullseyeDescription.Multiline = true;
			this.TbBullseyeDescription.Name = "TbBullseyeDescription";
			this.TbBullseyeDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.TbBullseyeDescription.Size = new System.Drawing.Size(659, 56);
			this.TbBullseyeDescription.TabIndex = 8;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(6, 637);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(118, 23);
			this.button1.TabIndex = 9;
			this.button1.Text = "Frequency plan";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// UcBriefingCoalition
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.TbBullseyeDescription);
			this.Controls.Add(this.TbBullseyeCoordinates);
			this.Controls.Add(this.LbFlights);
			this.Controls.Add(this.LbBullseye);
			this.Controls.Add(this.DgvFlights);
			this.Controls.Add(this.TbTask);
			this.Controls.Add(this.LbTask);
			this.Name = "UcBriefingCoalition";
			this.Size = new System.Drawing.Size(668, 663);
			((System.ComponentModel.ISupportInitialize)(this.DgvFlights)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.TextBox TbTask;
		private System.Windows.Forms.Label LbTask;
		private System.Windows.Forms.DataGridView DgvFlights;
		private System.Windows.Forms.Label LbBullseye;
		private System.Windows.Forms.Label LbFlights;
		private System.Windows.Forms.TextBox TbBullseyeCoordinates;
		private System.Windows.Forms.TextBox TbBullseyeDescription;
		private System.Windows.Forms.Button button1;
	}
}
