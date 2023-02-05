using CoordinateSharp;
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
			m_gridManagerUnits.SelectionChangedTyped += SelectionChangedTypedEvent;

			DataToScreen();
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			m_gridManagerUnits.Units = m_bopGroup.Units;
			m_gridManagerUnits.Initialize();
			DataToScreenDetail();
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopUnit> selectedBopUnits = m_gridManagerUnits.GetSelected();
			if (selectedBopUnits.Count() == 1)
			{
				BopUnit selectedBopUnit = selectedBopUnits.First();
				if (PnUnitDetail.Controls.Count > 0 && !(PnUnitDetail.Controls[0] is UcUnit))
				{
					PnUnitDetail.Controls.Clear();
				}
				if (m_ucUnit is null)
				{
					m_ucUnit = new UcUnit(m_briefopManager, selectedBopUnit, this);
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
			BopUnit selectedBopUnit = m_gridManagerUnits.GetSelected().FirstOrDefault();
			Coordinate coordinate = selectedBopUnit?.Coordinate ?? m_bopGroup.Coordinate;
			m_mapControl.Overlays.Clear();
			m_mapControl.Overlays.Add(m_bopGroup.GetMapOverlayUnits(selectedBopUnit?.Id));

			m_mapControl.Position = new PointLatLng(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);
			m_mapControl.ForceRefresh();
		}

		public override void ScreenToData()
		{
			ScreenToDataDetail();
		}

		public void ScreenToDataDetail()
		{
			m_ucUnit.ScreenToData();
		}
		#endregion

		#region Events
		private void SelectionChangedTypedEvent(object sender, GridManagerUnits.EventArgsBopUnits e)
		{
			ScreenToDataDetail();
			DataToScreenMap();
			DataToScreenDetail();
		}
		#endregion
	}
}
