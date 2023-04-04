using DcsBriefop.Tools;
using GMap.NET.MapProviders;
using System.Collections.Generic;

namespace DcsBriefop.Map
{
	internal class MapProviders
	{
		#region Fields
		private static List<GMapProvider> m_providers;
		public static readonly WMSProvider WMSProvider;
		#endregion

		#region CTOR
		static MapProviders()
		{
			WMSProvider = WMSProvider.Instance;

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
				GMapProviders.ArcGIS_World_Topo_Map,
				WMSProvider
			};
		}
		#endregion

		#region Methods
		public static void FillCombo(ComboBox cb, EventHandler selectedValueChanged)
		{
			ToolsControls.FillCombo(cb, m_providers, "Name", null, selectedValueChanged);
		}

		public static GMapProvider TryGetProvider(string providerName)
		{
			if (m_providers.Exists((GMapProvider x) => x.Name == providerName))
			{
				return m_providers.Find((GMapProvider x) => x.Name == providerName);
			}
			return null;
		}
		#endregion
	}
}
