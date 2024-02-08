namespace DcsBriefop.Forms
{
	partial class FrmTheatre
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
			CbMap = new ComboBox();
			label2 = new Label();
			label1 = new Label();
			textBox1 = new TextBox();
			SuspendLayout();
			// 
			// CbMap
			// 
			CbMap.FormattingEnabled = true;
			CbMap.Location = new Point(53, 10);
			CbMap.Name = "CbMap";
			CbMap.Size = new Size(121, 23);
			CbMap.TabIndex = 19;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(16, 13);
			label2.Name = "label2";
			label2.Size = new Size(31, 15);
			label2.TabIndex = 18;
			label2.Text = "Map";
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(307, 42);
			label1.Name = "label1";
			label1.Size = new Size(38, 15);
			label1.TabIndex = 20;
			label1.Text = "label1";
			// 
			// textBox1
			// 
			textBox1.Location = new Point(373, 29);
			textBox1.Name = "textBox1";
			textBox1.Size = new Size(295, 23);
			textBox1.TabIndex = 21;
			// 
			// FrmTheatre
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(textBox1);
			Controls.Add(label1);
			Controls.Add(CbMap);
			Controls.Add(label2);
			Name = "FrmTheatre";
			ShowIcon = false;
			Text = "Theatre data";
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private ComboBox CbMap;
		private Label label2;
		private Label label1;
		private TextBox textBox1;
	}
}