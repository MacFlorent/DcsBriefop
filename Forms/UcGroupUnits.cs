using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal partial class UcGroupUnits : UcGroupBase
	{
		#region Fields
		private GridManagerUnits m_gridManagerUnits;
		private UcUnit m_ucUnit;
		#endregion

		#region Properties
		#endregion

		#region CTOR
		public UcGroupUnits(BriefopManager briefopManager, BopGroup bopGroup, GMapControl mapControl) : base(briefopManager, bopGroup, mapControl)
		{
			InitializeComponent();

			m_gridManagerUnits = new GridManagerUnits(DgvUnits, m_bopGroup.Units);
			m_gridManagerUnits.ColumnsDisplayed = GridManagerUnits.ColumnsDisplayedGroup;
			m_gridManagerUnits.SelectionChangedBopUnits += SelectionChangedBopUnitsEvent;

			DataToScreen();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			m_gridManagerUnits.Units = m_bopGroup.Units;
			m_gridManagerUnits.Initialize();
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopUnit> selectedBopUnits = m_gridManagerUnits.GetSelectedBopUnits();
			if (selectedBopUnits.Count() == 1)
			{
				BopUnit selectedBopUnit = selectedBopUnits.First();
				if (PnUnitDetail.Controls.Count > 0 && !(PnUnitDetail.Controls[0] is UcUnit))
				{
					PnUnitDetail.Controls.Clear();
				}
				if (m_ucUnit is null)
				{
					m_ucUnit = new UcUnit(m_briefopManager, selectedBopUnit);
				}
				else
				{
					m_ucUnit.BopUnit = selectedBopUnit;
				}

				if (PnUnitDetail.Controls.Count == 0)
				{
					PnUnitDetail.Controls.Add(m_ucUnit);
					m_ucUnit.Dock = DockStyle.Fill;
				}
			}
			else
			{
				PnUnitDetail.Controls.Clear();
			}
		}

		public override void DataToScreenMap()
		{
			m_mapControl.Overlays.Clear();
			m_mapControl.Overlays.Add(m_bopGroup.GetMapOverlayUnits(m_gridManagerUnits.GetSelectedBopUnits().FirstOrDefault()?.Id));

			m_mapControl.Position = new PointLatLng(m_bopGroup.Coordinate.Latitude.DecimalDegree, m_bopGroup.Coordinate.Longitude.DecimalDegree);
			m_mapControl.ForceRefresh();
		}

		public override void ScreenToData()
		{
			//m_ucGroupInformation.ScreenToData();
		}
		#endregion

		#region Events
		private void SelectionChangedBopUnitsEvent(object sender, GridManagerUnits.EventArgsBopUnit e)
		{
			DataToScreenMap();
			DataToScreenDetail();
		}
		#endregion
	}
}
