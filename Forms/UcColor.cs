using DcsBriefop.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	public partial class UcColor : UserControl, ICustomStylable
	{
		#region Properties
		private Color m_defaultColor = Color.Black;
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

		public string SelectedColorHtml
		{
			get
			{
				if (SelectedColor is object)
					return ColorTranslator.ToHtml(SelectedColor.Value);
				else
					return null;
			}
			set
			{
				SelectedColor = GetColorFromHtml(value);
			}
		}
		#endregion

		#region CTOR
		public UcColor()
		{
			InitializeComponent();
		}
		#endregion

		#region ICustomStylable
		public void ApplyCustomStyle()
		{
			ToolsStyle.TextBoxDefault(TbColor);
			ToolsStyle.ButtonDefault(BtColor);
		}
		#endregion

		#region Methods
		private void UpdateSelectedColorFromTextbox()
		{
			SelectedColor = GetColorFromHtml(TbColor.Text);
		}

		private Color? GetColorFromHtml(string sHtmlColor)
		{
			Color? color = null;
			if (!string.IsNullOrEmpty(sHtmlColor))
			{
				try { color = ColorTranslator.FromHtml(sHtmlColor); }
				catch (Exception) { color = null; }
			}

			return color;
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
			cd.Color = SelectedColor ?? m_defaultColor;

			if (cd.ShowDialog() == DialogResult.OK)
			{
				SelectedColor = cd.Color;
				ColorChanged?.Invoke(this, e);
			}
		}

		private void TbColor_TextChanged(object sender, EventArgs e)
		{
			UpdateSelectedColorFromTextbox();
			ColorChanged?.Invoke(this, e);
		}
		#endregion
	}
}
