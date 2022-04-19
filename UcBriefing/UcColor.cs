using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	public partial class UcColor : UserControl
	{
		#region Properties
		private Color? m_color;
		public Color? SelectedColor
		{
			get { return m_color; }
			set
			{
				TbColor.TextChanged -= TbColor_TextChanged;

				m_color = value;
				if (m_color is null)
					TbColor.Text = null;
				else
					TbColor.Text = ColorTranslator.ToHtml(m_color.Value);

				TbColor.TextChanged += TbColor_TextChanged;
			}
		}
		#endregion

		#region CTOR
		public UcColor()
		{
			InitializeComponent();
		}
		#endregion

		#region Methods
		private void UpdateSelectedColorFromTextbox ()
		{
			if (string.IsNullOrEmpty(TbColor.Text))
				SelectedColor = null;
			else
			{
				try { SelectedColor = ColorTranslator.FromHtml(TbColor.Text); }
				catch (Exception) { SelectedColor = null; }
			}
		}
		#endregion

		#region Events
		[Browsable(true)]
		[Category("Action")]
		[Description("Occurs when the selected color is changed")]
		public event EventHandler ColorChanged;

		private void BtColor_Click(object sender, EventArgs e)
		{
			ColorDialog cd = new ColorDialog();
			cd.AllowFullOpen = false;
			cd.Color = SelectedColor ?? Color.Black;

			if (cd.ShowDialog() == DialogResult.OK)
			{
				SelectedColor = cd.Color;
				ColorChanged?.Invoke(this, e);
			}
		}
		#endregion

		private void TbColor_TextChanged(object sender, EventArgs e)
		{
			UpdateSelectedColorFromTextbox();
			ColorChanged?.Invoke(this, e);
		}
	}
}
