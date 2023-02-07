using DcsBriefop.Tools;
using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DcsBriefop.Map
{
	internal class MapProviders
	{
		#region Fields
		private static List<GMapProvider> m_providers;
		#endregion

		#region CTOR
		static MapProviders()
		{
			m_providers = new List<GMapProvider>()
			{
				GMapProviders.OpenCycleMap,
				GMapProviders.OpenCycleLandscapeMap,
				GMapProviders.OpenCycleTransportMap,
				GMapProviders.OpenStreetMap,
				GMapProviders.OpenStreetMapGraphHopper,
				GMapProviders.OpenSeaMapHybrid,
				GMapProviders.WikiMapiaMap,
				GMapProviders.BingMap,
				GMapProviders.BingSatelliteMap,
				GMapProviders.BingHybridMap,
				GMapProviders.BingOSMap,
				GMapProviders.GoogleMap,
				GMapProviders.GoogleSatelliteMap,
				GMapProviders.GoogleHybridMap,
				GMapProviders.GoogleTerrainMap,
				GMapProviders.ArcGIS_World_Shaded_Relief_Map,
				GMapProviders.ArcGIS_World_Street_Map,
				GMapProviders.ArcGIS_World_Topo_Map
			};
		}
		#endregion

		#region Methods
		public static void FillCombo(ComboBox cb, EventHandler selectedValueChanged)
		{
			ToolsControls.FillCombo(cb, m_providers, "Name", null, selectedValueChanged);
		}
		#endregion
	}
}
