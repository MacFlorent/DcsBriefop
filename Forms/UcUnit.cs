using DcsBriefop.DataBopMission;
using DcsBriefop.Map;
using System;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal partial class UcUnit : UserControl
	{
		#region Fields
		protected BriefopManager m_briefopManager;
		protected BopUnit m_bopUnit;
		#endregion

		#region Properties
		public BopUnit BopUnit
		{
			protected get { return m_bopUnit; }
			set
			{
				m_bopUnit = value;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcUnit(BriefopManager briefopManager, BopUnit bopUnit)
		{
			m_briefopManager = briefopManager;
			m_bopUnit = bopUnit;

			InitializeComponent();

			MapTemplateMarker.FillCombo(CbMapMarker, CbMapMarker_SelectedValueChanged);
		}
		#endregion

		#region Methods
		public void DataToScreen()
		{
			CbMapMarker.SelectedValueChanged -= CbMapMarker_SelectedValueChanged;

			m_bopUnit.FinalizeFromMiz();

			TbId.Text = m_bopUnit.Id.ToString();
			CkPlayable.Checked = m_bopUnit.Playable;
			TbName.Text = m_bopUnit.Name;
			TbDisplayName.Text = m_bopUnit.ToStringDisplayName();
			TbType.Text = m_bopUnit.Type;
			TbAttributes.Text = m_bopUnit.Attributes.ToString();
			//TbOther.Text = m_bopUnit.ToStringAdditionnal();
			TbCoordinates.Text = m_bopUnit.ToStringLocalisation();
			CbMapMarker.Text = m_bopUnit.MapMarker;

			CbMapMarker.SelectedValueChanged += CbMapMarker_SelectedValueChanged;
		}

		public void ScreenToData()
		{
			m_bopUnit.MapMarker = CbMapMarker.Text;
		}
		#endregion

		#region Events
		private void CbMapMarker_SelectedValueChanged(object sender, EventArgs e)
		{
			ScreenToData();
		}
		#endregion

	}
}
