using CoordinateSharp;
using DcsBriefop.DataBopMission;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using Mapsui;
using Mapsui.Layers;

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
		public UcGroupUnits(BriefopManager briefopManager, BopGroup bopGroup, Mapsui.UI.WindowsForms.MapControl mapControl) : base(briefopManager, bopGroup, mapControl)
		{
			InitializeComponent();

			m_gridManagerUnits = new GridManagerUnits(DgvUnits, null);
			m_gridManagerUnits.ColumnsDisplayed = GridManagerUnits.ColumnsDisplayedGroup;
			m_gridManagerUnits.SelectionChanged += SelectionChangedEvent;
		}
		#endregion

		#region Methods
		public override void DataToScreen()
		{
			m_gridManagerUnits.SelectionChanged -= SelectionChangedEvent;

			m_gridManagerUnits.Elements = m_bopGroup.Units;
			m_gridManagerUnits.Refresh();
			DataToScreenDetail();

			m_gridManagerUnits.SelectionChanged += SelectionChangedEvent;
		}

		private void DataToScreenDetail()
		{
			IEnumerable<BopUnit> selectedBopUnits = m_gridManagerUnits.GetSelectedElements();
			if (selectedBopUnits.Count() == 1)
			{
				BopUnit selectedBopUnit = selectedBopUnits.First();
				if (PnUnitDetail.Controls.Count > 0 && !(PnUnitDetail.Controls[0] is UcUnit))
				{
					PnUnitDetail.Controls.Clear();
				}
				if (m_ucUnit is null)
				{
					m_ucUnit = new UcUnit(m_briefopManager, this);
				}
				if (PnUnitDetail.Controls.Count == 0)
				{
					PnUnitDetail.Controls.Add(m_ucUnit);
					m_ucUnit.Dock = DockStyle.Fill;
				}

				m_ucUnit.BopUnit = selectedBopUnit;
			}
			else
			{
				PnUnitDetail.Controls.Clear();
			}
		}

		public override void DataToScreenMap()
		{
			BopUnit selectedBopUnit = m_gridManagerUnits.GetSelectedElements().FirstOrDefault();
			Coordinate coordinate = selectedBopUnit?.Coordinate ?? m_bopGroup.Coordinate;

			foreach (MemoryLayer mapLayer in m_mapControl.Map.Layers.OfType<MemoryLayer>().ToList())
				m_mapControl.Map.Layers.Remove(mapLayer);

			m_mapControl.Map.Layers.Add(m_bopGroup.GetUnitsMapLayer(selectedBopUnit?.Id));

			MPoint center = MapProjection.ToMPoint(coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);
			m_mapControl.Map.Navigator.CenterOnAndZoomTo(center, MapProjection.ZoomToResolution((int)PreferencesManager.Preferences.Map.Zoom), 0, null);
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
		private void SelectionChangedEvent(object sender, EventArgs e)
		{
			ScreenToDataDetail();
			DataToScreenMap();
			DataToScreenDetail();
		}
		#endregion
	}
}
