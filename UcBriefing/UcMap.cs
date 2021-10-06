using DcsBriefop.Briefing;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System.Windows.Forms;

//https://stackoverflow.com/questions/9308673/how-to-draw-circle-on-the-map-using-gmap-net-in-c-sharp
//http://www.independent-software.com/gmap-net-beginners-tutorial-maps-markers-polygons-routes-updated-for-vs2015-and-gmap1-7.html

namespace DcsBriefop.UcBriefing
{
	internal partial class UcMap : UserControl
	{
		#region Properties
		public CustomDataMap MapData { get; private set; }
		#endregion

		#region CTOR
		public UcMap()
		{
			InitializeComponent();

			Map.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
			GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
			//Map.ShowTileGridLines = true;
			Map.Position = new GMap.NET.PointLatLng(26.1702778, 56.24);
		}
		#endregion

		#region Methods
		public void SetMapData(CustomDataMap cdm)
		{
			MapData = cdm;

			Map.Overlays.Clear();

			Map.Overlays.Add(MapData.MapOverlayCustom);

			foreach (GMapOverlay gmo in MapData.AdditionalMapOverlays)
				Map.Overlays.Add(gmo);

			Map.Position = new GMap.NET.PointLatLng(MapData.CenterLatitude, MapData.CenterLongitude);
			Map.Zoom = MapData.Zoom;
		}
		#endregion

		#region Events
		private void BtAreaSet_Click(object sender, System.EventArgs e)
		{
			MapData.CenterLatitude = Map.Position.Lat;
			MapData.CenterLongitude = Map.Position.Lng;
			MapData.Zoom = Map.Zoom;
		}

		private void BtAreaRecall_Click(object sender, System.EventArgs e)
		{
			Map.Position = new GMap.NET.PointLatLng(MapData.CenterLatitude, MapData.CenterLongitude);
			Map.Zoom = MapData.Zoom;
		}

		private void Map_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left && CkAddMarker.Checked)
			{
				double lat = Map.FromLocalToLatLng(e.X, e.Y).Lat;
				double lng = Map.FromLocalToLatLng(e.X, e.Y).Lng;

				PointLatLng p = new PointLatLng(lat, lng);
				GMarkerBriefop mk = new GMarkerBriefop(p, "test", null);
				MapData.MapOverlayCustom.Markers.Add(mk);
			}
		}

		private void BeTestSave_Click(object sender, System.EventArgs e)
		{
			//NEWTONSOFT JSON (cannot deserialize arrays)
			//string s = JsonConvert.SerializeObject(MapData, Formatting.Indented, new GMapOverlayJsonConverter(), new GMarkerGoogleJsonConverter());
			//CustomDataMap c = JsonConvert.DeserializeObject<CustomDataMap>(s, new GMapOverlayJsonConverter(), new GMarkerGoogleJsonConverter());

			//TEXT.JSON
			//var options = new JsonSerializerOptions();
			//options.Converters.Add(new GMarkerGoogleJsonConverter());
			//options.Converters.Add(new GMapOverlayJsonConverter());
			//options.WriteIndented = true;
			//string jsonString = JsonSerializer.Serialize(MapData, options);
			//CustomDataMap cdm = JsonSerializer.Deserialize<CustomDataMap>(jsonString, options);

			// XML (missing default constructor)
			//string sXml = null;
			//System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(GMapOverlay));
			//using (StringWriter textWriter = new StringWriter())
			//{
			//	serializer.Serialize(textWriter, MapData.MapOverlayCustom);
			//	sXml = textWriter.ToString();
			//}

			//using (StringReader textReader = new StringReader(sXml))
			//{
			//	var o = (GMapOverlay)serializer.Deserialize(textReader);
			//}

			// BINARY (unsafe)
			//using (var ms = new MemoryStream())
			//{
			//	var formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();

			//	formatter.Serialize(ms, MapData.MapOverlayCustom);
			//	ms.Position = 0;

			//	var o = (GMapOverlay)formatter.Deserialize(ms);
			//}
		}
		#endregion

		//public void AddOverlay(GMapOverlay gmo)
		//{
		//	if (gmo is null || gmo == OverlayMain)
		//		return;

		//	if (Overlays.Contains(gmo))
		//		return;

		//	Overlays.Add(gmo);
		//}

		//public void RemoveOverlay(GMapOverlay gmo)
		//{
		//	if (gmo == OverlayMain)
		//		OverlayMain = null;

		//	if (Overlays.Contains(gmo))
		//		Overlays.Remove(gmo);
		//}

		//public void RemoveOverlay(string sId)
		//{
		//	if (string.IsNullOrEmpty (sId))
		//		return;

		//	if (OverlayMain is object && OverlayMain.Id == sId)
		//		OverlayMain = null;

		//	GMapOverlay gmoToRemove = null;
		//	foreach (GMapOverlay gmo in Overlays)
		//	{
		//		if (gmo.Id == sId)
		//			gmoToRemove = gmo;
		//	}
		//	RemoveOverlay(gmoToRemove);
		//}

		//public void ClearOverlays()
		//{
		//	OverlayMain = null;
		//	Overlays.Clear();
		//}

		//public void RefreshMap(string sCenterOverlayId)
		//{
		//	Map.Overlays.Clear();

		//	if (OverlayMain is object)
		//		Map.Overlays.Add(OverlayMain);

		//	foreach(GMapOverlay gmo in Overlays)
		//		Map.Overlays.Add(gmo);

		//	Map.ZoomAndCenterMarkers(sCenterOverlayId);
		//}
	}
}
