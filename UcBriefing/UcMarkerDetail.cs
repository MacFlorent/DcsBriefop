using GMap.NET.WindowsForms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	public partial class UcMarkerDetail : UserControl
	{
		private GMarkerBriefop m_marker;
		private GMapControl m_map;

		public UcMarkerDetail()
		{
			InitializeComponent();
		}

		public UcMarkerDetail(GMarkerBriefop marker, GMapControl map) : this()
		{
			m_marker = marker;
			m_map = map;

			MarkerBriefopTemplate.FillCombo(CbMarkerType);
			DataToScreen();
		}

		public void DataToScreen()
		{
			CbMarkerType.Text = m_marker.MarkerTemplate;
			TbLabel.Text = m_marker.Label;

			if (m_marker.TintColor is object)
				TbColor.Text = ColorTranslator.ToHtml(m_marker.TintColor.Value);
			else
				TbColor.Text = "";
		}

		public void ScreenToData()
		{
			m_marker.LoadTemplate (CbMarkerType.Text);
			m_marker.Label = TbLabel.Text;
			m_marker.TintColor = GetSelectedColor();

			m_marker.LoadBitmap();
			m_map.Refresh();
		}

		private Color? GetSelectedColor()
		{
			Color? color = null;
			try { color = ColorTranslator.FromHtml(TbColor.Text); }
			catch (Exception)
			{
				color = null;
			}

			return color;
		}
			#region Events
			private void TbLabel_Validated(object sender, EventArgs e)
		{
			ScreenToData();
		}

		private void TbColor_Validated(object sender, EventArgs e)
		{
			ScreenToData();
		}

		private void TbColor_Validating(object sender, CancelEventArgs e)
		{
			//if (sender is TextBox tb && !string.IsNullOrEmpty(tb.Text))
			//{
			//	Color? color = GetSelectedColor();
			//	if (color = null)
			//	{
			//		tb.Text = "";
			//		tb.Select(0, tb.Text.Length);
			//		e.Cancel = true;
			//	}
			//}
		}

		private void BtColor_Click(object sender, EventArgs e)
		{
			ColorDialog MyDialog = new ColorDialog();
			// Keeps the user from selecting a custom color.
			MyDialog.AllowFullOpen = false;
			// Sets the initial color select to the current text color.
			MyDialog.Color = GetSelectedColor() ?? Color.Black;

			// Update the text box color if the user clicks OK 
			if (MyDialog.ShowDialog() == DialogResult.OK)
			{
				TbColor.Text = ColorTranslator.ToHtml(MyDialog.Color);
				ScreenToData();
			}

		}

		private void CbMarkerType_Validated(object sender, EventArgs e)
		{
			ScreenToData();
		}
		#endregion
	}
}
