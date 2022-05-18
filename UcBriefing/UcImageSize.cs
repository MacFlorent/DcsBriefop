using DcsBriefop.Tools;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DcsBriefop.UcBriefing
{
	public partial class UcImageSize : UserControl, ICustomStylable
	{
		#region Properties
		private Size m_size = new Size(1, 1);
		public Size SelectedSize
		{
			get { return m_size; }
			set
			{
				m_size = value;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcImageSize()
		{
			InitializeComponent();

			CkRatioLock.Checked = true;
		}
		#endregion

		#region ICustomStylable
		public void ApplyCustomStyle() { }
		#endregion

		#region Methods
		private void DataToScreen()
		{
			UdWidth.ValueChanged -= UdImageWidth_ValueChanged;
			UdHeight.ValueChanged -= UdImageHeight_ValueChanged;
			UdRatio.ValueChanged -= UdImageRatio_ValueChanged;

			decimal iWidth = m_size.Width;
			if (iWidth < UdWidth.Minimum)
				iWidth = UdWidth.Minimum;
			else if (iWidth > UdWidth.Maximum)
				iWidth = UdWidth.Maximum;

			decimal iHeight = m_size.Height;
			if (iHeight < UdHeight.Minimum)
				iHeight = UdHeight.Minimum;
			else if (iHeight > UdHeight.Maximum)
				iHeight = UdHeight.Maximum;

			UdWidth.Value = iWidth;
			UdHeight.Value = iHeight;
			UdRatio.Value = (decimal)m_size.Width / m_size.Height;

			UdWidth.ValueChanged += UdImageWidth_ValueChanged;
			UdHeight.ValueChanged += UdImageHeight_ValueChanged;
			UdRatio.ValueChanged += UdImageRatio_ValueChanged;
		}

		private void ScreenToData()
		{
			m_size.Width = (int)UdWidth.Value;
			m_size.Height = (int)UdHeight.Value;
		}
		#endregion

		#region Events
		[Browsable(true)]
		[Category("Action")]
		[Description("Occurs when the selected image size is changed")]
		public event EventHandler ImageSizeChanged;

		private void UdImageWidth_ValueChanged(object sender, EventArgs e)
		{
			if (CkRatioLock.Checked)
			{
				decimal dRatio = (UdRatio.Value as decimal?).GetValueOrDefault(1);
				decimal dWidth = (UdWidth.Value as decimal?).GetValueOrDefault(0);
				decimal dHeight = dWidth / dRatio;
				UdHeight.Value = dHeight;
			}
			else
			{
				decimal dWidth = (UdWidth.Value as decimal?).GetValueOrDefault(0);
				decimal dHeight = (UdHeight.Value as decimal?).GetValueOrDefault(1);
				decimal dRatio = dWidth / dHeight;
				UdRatio.Value = dRatio;
			}

			ImageSizeChanged?.Invoke(this, e);
			ScreenToData();
		}

		private void UdImageHeight_ValueChanged(object sender, EventArgs e)
		{
			if (CkRatioLock.Checked)
			{
				decimal dRatio = (UdRatio.Value as decimal?).GetValueOrDefault(1);
				decimal dHeight = (UdHeight.Value as decimal?).GetValueOrDefault(0);
				decimal dWidth = dHeight * dRatio;
				UdWidth.Value = dWidth;
			}
			else
			{
				decimal dWidth = (UdWidth.Value as decimal?).GetValueOrDefault(0);
				decimal dHeight = (UdHeight.Value as decimal?).GetValueOrDefault(1);
				decimal dRatio = dWidth / dHeight;
				UdRatio.Value = dRatio;
			}

			ImageSizeChanged?.Invoke(this, e);
			ScreenToData();
		}

		private void UdImageRatio_ValueChanged(object sender, EventArgs e)
		{
			decimal dRatio = (UdRatio.Value as decimal?).GetValueOrDefault(1);
			decimal dWidth = (UdWidth.Value as decimal?).GetValueOrDefault(0);
			decimal dHeight = dWidth / dRatio;
			UdHeight.Value = dHeight;

			ImageSizeChanged?.Invoke(this, e);
			ScreenToData();
		}
		#endregion
	}
}
