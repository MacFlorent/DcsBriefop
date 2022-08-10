
namespace DcsBriefop.FormBop
{
	partial class UcBriefopMain
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
			this.TcMissionData = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.TcMissionData.SuspendLayout();
			this.SuspendLayout();
			// 
			// TcMissionData
			// 
			this.TcMissionData.Controls.Add(this.tabPage1);
			this.TcMissionData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TcMissionData.Location = new System.Drawing.Point(0, 0);
			this.TcMissionData.Name = "TcMissionData";
			this.TcMissionData.SelectedIndex = 0;
			this.TcMissionData.Size = new System.Drawing.Size(834, 838);
			this.TcMissionData.TabIndex = 1;
			this.TcMissionData.SelectedIndexChanged += new System.EventHandler(this.TcMissionData_SelectedIndexChanged);
			// 
			// tabPage1
			// 
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(826, 812);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "tabPage1";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// UcBriefingPack
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.TcMissionData);
			this.Name = "UcBriefingPack";
			this.Size = new System.Drawing.Size(834, 838);
			this.TcMissionData.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl TcMissionData;
		private System.Windows.Forms.TabPage tabPage1;
	}
}
