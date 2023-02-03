using DcsBriefop.DataBopMission;
using System.Text;
using System;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal partial class UcRoutePoint : UserControl
	{
		#region Fields
		protected BriefopManager m_briefopManager;
		protected BopRoutePoint m_bopRoutePoint;
		#endregion

		#region Properties
		public BopRoutePoint BopRoutePoint
		{
			protected get { return m_bopRoutePoint; }
			set
			{
				m_bopRoutePoint = value;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcRoutePoint(BriefopManager briefopManager, BopRoutePoint bopRoutePoint)
		{
			m_briefopManager = briefopManager;
			m_bopRoutePoint = bopRoutePoint;

			InitializeComponent();
		}
		#endregion

		#region Methods
		public void DataToScreen()
		{
			m_bopRoutePoint.FinalizeFromMiz();

			TbNumber.Text = m_bopRoutePoint.Number.ToString();
			TbName.Text = m_bopRoutePoint.Name;
			TbType.Text = m_bopRoutePoint.Type;
			TbAction.Text = m_bopRoutePoint.Action;
			TbAltitude.Text = $"{m_bopRoutePoint.AltitudeFeet:0} ft";
			TbCoordinates.Text = m_bopRoutePoint.ToStringCoordinate();
			TbAdditionnal.Text = m_bopRoutePoint.ToStringAdditionnal();
		}

		public void ScreenToData()
		{
		}
		#endregion
	}
}
