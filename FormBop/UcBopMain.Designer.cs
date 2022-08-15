
namespace DcsBriefop.FormBop
{
	partial class UcBopMain
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
			this.TcMain = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.TcMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// TcMissionData
			// 
			this.TcMain.Controls.Add(this.tabPage1);
			this.TcMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TcMain.Location = new System.Drawing.Point(0, 0);
			this.TcMain.Name = "TcMissionData";
			this.TcMain.SelectedIndex = 0;
			this.TcMain.Size = new System.Drawing.Size(834, 838);
			this.TcMain.TabIndex = 1;
			this.TcMain.SelectedIndexChanged += new System.EventHandler(this.TcMissionData_SelectedIndexChanged);
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
			this.Controls.Add(this.TcMain);
			this.Name = "UcBriefingPack";
			this.Size = new System.Drawing.Size(834, 838);
			this.TcMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl TcMain;
		private System.Windows.Forms.TabPage tabPage1;
	}
}
