using System.Drawing;
using System.Windows.Forms;

namespace DcsBriefop.Tools
{
	public interface ICustomStylable
	{
		void ApplyCustomStyle();
	}

	internal static class ToolsStyle
	{
		#region Properties
		public static readonly Font TextFont = SystemFonts.DefaultFont;
		public static readonly Font TextFontHeader = new Font(TextFont.FontFamily, TextFont.Size + 1, FontStyle.Bold);
		public static readonly Font TextFontTitle = new Font(TextFont.FontFamily, TextFont.Size + 4, FontStyle.Bold);
		public static readonly Font TextFontSmall = new Font(TextFont.FontFamily, TextFont.Size - 1, FontStyle.Regular);

		public static readonly Color ColorMain = Color.Blue;
		public static readonly Color ColorDark = Color.Black.Lerp(ColorMain, 0.15f);
		public static readonly Color ColorLight = Color.Gainsboro.Lerp(ColorMain, 0.15f);
		public static readonly Color ColorLightLight = Color.WhiteSmoke.Lerp(ColorMain, 0.05f);
		#endregion

		#region General
		public static void ApplyStyle(Control control)
		{
			if (control is Label label)
				LabelDefault(label);
			else if (control is TextBox textBox)
				TextBoxDefault(textBox);
			else if (control is CheckBox checkBox)
				CheckBoxDefault(checkBox);
			else if (control is Button button)
				ButtonDefault(button);
			else if (control is DataGridView dgv)
				GridDefault(dgv);
			else
			{
				if (control is Form form)
					FormDefault(form);
				else if (control is UserControl uc)
					UserControlDefault(uc);
				else if (control is TabControl tc)
					TabControlDefault(tc);

				ApplyStyleToChildren(control);
				(control as ICustomStylable)?.ApplyCustomStyle();
			}
		}

		public static void ApplyStyleToChildren(Control parent)
		{
			foreach (Control child in parent.Controls)
				ApplyStyle(child);
		}
		#endregion

		#region Containers
		public static void FormDefault(Form form)
		{
			form.BackColor = ColorLightLight;
		}

		public static void UserControlDefault(UserControl uc)
		{
			uc.BackColor = ColorLightLight;
		}

		public static void TabControlDefault(TabControl tc)
		{
			tc.Font = TextFont;
		}

		#endregion

		#region Labels
		public static void LabelDefault(Label label)
		{
			label.ForeColor = ColorDark;
			label.Font = TextFont;
		}

		public static void LabelTitle(Label label)
		{
			LabelDefault(label);
			label.Font = TextFontTitle;
		}

		public static void LabelHeader(Label label)
		{
			LabelDefault(label);
			label.Font = TextFontHeader;
		}

		public static void LabelSmall(Label label)
		{
			LabelDefault(label);
			label.Font = TextFontSmall;
		}
		#endregion

		#region TextBoxes
		public static void TextBoxDefault(TextBox textBox)
		{
			textBox.Font = TextFont;
		}

		public static void TextBoxSmall(TextBox textBox)
		{
			textBox.Font = TextFontSmall;
		}
		#endregion

		#region CheckBoxes
		public static void CheckBoxDefault(CheckBox checkBox)
		{
			checkBox.Font = TextFont;
		}
		#endregion

		#region Buttons
		public static void ButtonDefault(Button button)
		{
			button.ForeColor = Color.Black;
			button.BackColor = ColorLight;
			button.Font = TextFont;
		}

		public static void ButtonOk(Button button)
		{
			ButtonDefault(button);
			button.BackColor = Color.DarkSeaGreen;
		}

		public static void ButtonCancel(Button button)
		{
			ButtonDefault(button);
			button.BackColor = Color.LightPink;
		}

		#endregion

		#region Grids
		public static void GridDefault(DataGridView dgv)
		{
			dgv.Font = TextFont;
			dgv.BackgroundColor = Color.WhiteSmoke;
			dgv.ForeColor = ColorDark;

			dgv.DefaultCellStyle.ForeColor = ColorDark;
			dgv.DefaultCellStyle.BackColor = Color.WhiteSmoke;
			dgv.DefaultCellStyle.SelectionForeColor = ColorDark;
			dgv.DefaultCellStyle.SelectionBackColor = ColorLight;
		}
		#endregion

		#region Forms
		public static void SetBackgroundImage(Control c)
		{
			c.BackgroundImage = ToolsImage.SetImageOpacity(ToolsResources.GetImageResource("icon256"), 0.15f);
			c.BackgroundImageLayout = ImageLayout.Tile;
		}
		#endregion
	}
}
