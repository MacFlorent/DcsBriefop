using DcsBriefop.Map;
using GMap.NET.WindowsForms;
using System;
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

			MapTemplateMarker.FillCombo(CbMarkerType);
			DataToScreen();
		}

		public void DataToScreen()
		{
			TbColor.TextChanged -= TbColor_TextChanged;
			TbLabel.TextChanged -= TbLabel_TextChanged;

			CbMarkerType.Text = m_marker.MarkerTemplate;
			TbLabel.Text = m_marker.Label;

			if (m_marker.TintColor is object)
				TbColor.Text = ColorTranslator.ToHtml(m_marker.TintColor.Value);
			else
				TbColor.Text = "";

			TbColor.TextChanged += TbColor_TextChanged;
			TbLabel.TextChanged += TbLabel_TextChanged;
		}

		public void ScreenToData()
		{
			m_marker.LoadTemplate(CbMarkerType.SelectedValue?.ToString());
			m_marker.Label = TbLabel.Text;
			m_marker.TintColor = GetSelectedColor();

			m_marker.LoadBitmap();
			m_map.Refresh();
		}

		private Color? GetSelectedColor()
		{
			Color? color = null;
			if (!string.IsNullOrEmpty(TbColor.Text))
			{
				try { color = ColorTranslator.FromHtml(TbColor.Text); }
				catch (Exception) { color = null; }
			}

			return color;
		}
		#region Events
		private void CbMarkerType_SelectionChangeCommitted(object sender, EventArgs e)
		{
			ScreenToData();
		}

		private void TbLabel_TextChanged(object sender, EventArgs e)
		{
			ScreenToData();
		}

		private void TbColor_TextChanged(object sender, EventArgs e)
		{
			ScreenToData();
		}

		private void BtColor_Click(object sender, EventArgs e)
		{
			ColorDialog cd = new ColorDialog();
			cd.AllowFullOpen = false;
			cd.Color = GetSelectedColor() ?? Color.Black;

			if (cd.ShowDialog() == DialogResult.OK)
			{
				TbColor.Text = ColorTranslator.ToHtml(cd.Color);
				ScreenToData();
			}

		}

		#endregion


	}
}
