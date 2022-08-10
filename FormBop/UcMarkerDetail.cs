using DcsBriefop.Map;
using GMap.NET.WindowsForms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace DcsBriefop.FormBop
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
			UcTintColor.ColorChanged -= UcTintColor_ColorChanged;
			TbLabel.TextChanged -= TbLabel_TextChanged;
			UdScale.ValueChanged -= UdScale_ValueChanged;
			UdAngle.ValueChanged -= UdAngle_ValueChanged;

			CbMarkerType.Text = m_marker.MarkerTemplate;
			TbLabel.Text = m_marker.Label;
			UdScale.Value = m_marker.Scale;
			UdAngle.Value = m_marker.Angle;

			UcTintColor.SelectedColor = m_marker.TintColor;

			UcTintColor.ColorChanged += UcTintColor_ColorChanged;
			TbLabel.TextChanged += TbLabel_TextChanged;
			UdScale.ValueChanged += UdScale_ValueChanged;
			UdAngle.ValueChanged += UdAngle_ValueChanged;
		}

		public void ScreenToData()
		{
			m_marker.LoadTemplate(CbMarkerType.SelectedValue?.ToString());
			m_marker.Label = TbLabel.Text;
			m_marker.Scale = (int)UdScale.Value;
			m_marker.Angle = (int)UdAngle.Value;

			m_marker.TintColor = UcTintColor.SelectedColor;

			m_marker.LoadBitmap();
			m_map.Refresh();
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

		private void UdScale_ValueChanged(object sender, EventArgs e)
		{
			ScreenToData();
		}

		private void UdAngle_ValueChanged(object sender, EventArgs e)
		{
			ScreenToData();
		}
		private void UcTintColor_ColorChanged(object sender, EventArgs e)
		{
			ScreenToData();
		}
		#endregion


	}
}
