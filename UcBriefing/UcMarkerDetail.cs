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

			GMarkerBriefopTemplate.FillCombo(CbMarkerType);
			DataToScreen();
		}

		public void DataToScreen()
		{
			CbMarkerType.Text = m_marker.MarkerType;
			TbLabel.Text = m_marker.Label;
			TbColor.Text = ColorTranslator.ToHtml(m_marker.Color);
		}

		public void ScreenToData()
		{
			m_marker.LoadTemplate (CbMarkerType.Text);
			m_marker.Label = TbLabel.Text;

			string sCurrentColor = ColorTranslator.ToHtml(m_marker.Color);
			m_marker.Color = ColorTranslator.FromHtml(TbColor.Text);

			m_marker.LoadBitmap();
			m_map.Refresh();
		}

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
			try { Color c = ColorTranslator.FromHtml(TbColor.Text); }
			catch (Exception)
			{
				if (sender is TextBox tb)
				{
					tb.Text = ColorTranslator.ToHtml(m_marker.Color);
					tb.Select(0, tb.Text.Length);
				}
				e.Cancel = true;
			}
		}

		private void BtColor_Click(object sender, EventArgs e)
		{
			ColorDialog MyDialog = new ColorDialog();
			// Keeps the user from selecting a custom color.
			MyDialog.AllowFullOpen = false;
			// Sets the initial color select to the current text color.
			MyDialog.Color = m_marker.Color;

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
	}
}
