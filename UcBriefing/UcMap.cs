using DcsBriefop.Briefing;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

//https://stackoverflow.com/questions/9308673/how-to-draw-circle-on-the-map-using-gmap-net-in-c-sharp
//http://www.independent-software.com/gmap-net-beginners-tutorial-maps-markers-polygons-routes-updated-for-vs2015-and-gmap1-7.html
//https://icon-icons.com/fr/icone/bullseye/73647https://icon-icons.com/fr/icone/bullseye/73647

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
			GMaps.Instance.Mode = AccessMode.ServerOnly;
			//Map.ShowTileGridLines = true;
			Map.Position = new PointLatLng(26.1702778, 56.24);
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

			Map.Position = new PointLatLng(MapData.CenterLatitude, MapData.CenterLongitude);
			Map.Zoom = MapData.Zoom;
		}

		private void AddMarker(double dLat, double dLng)
		{
			PointLatLng p = new PointLatLng(dLat, dLng);
			MapData.MapOverlayCustom.Markers.Add(new GMarkerBriefop(p, GMarkerBriefopType.Pin, Color.Orange, null));
			CkAddMarker.Checked = false;
		}

		private void DeleteMarker(GMarkerBriefop gmb)
		{
			if (gmb.Overlay == MapData.MapOverlayCustom)
			MapData.MapOverlayCustom.Markers.Remove(gmb);
		}

		private void UnselectMarkers()
		{
			foreach (GMarkerBriefop gmb in MapData.MapOverlayCustom.Markers.OfType<GMarkerBriefop>())
				gmb.IsSelected = false;
		}

		private GMarkerBriefop GetMarkerMouseOver()
		{
			foreach (GMarkerBriefop gmb in MapData.MapOverlayCustom.Markers.OfType<GMarkerBriefop>())
				if (gmb.IsMouseOver)
					return gmb;

			return null;
		}
		#endregion

		#region Events
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

		private void BtAreaSet_Click(object sender, System.EventArgs e)
		{
			MapData.CenterLatitude = Map.Position.Lat;
			MapData.CenterLongitude = Map.Position.Lng;
			MapData.Zoom = Map.Zoom;
		}

		private void BtAreaRecall_Click(object sender, System.EventArgs e)
		{
			Map.Position = new PointLatLng(MapData.CenterLatitude, MapData.CenterLongitude);
			Map.Zoom = MapData.Zoom;
		}

		private void Map_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				if (CkAddMarker.Checked)
					AddMarker(Map.FromLocalToLatLng(e.X, e.Y).Lat, Map.FromLocalToLatLng(e.X, e.Y).Lng);
				else if (GetMarkerMouseOver() is null)
					UnselectMarkers();
			}
		}

		private void Map_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				GMarkerBriefop gmb = GetMarkerMouseOver();

				if (gmb is object)
				{
					DeleteMarker(gmb);
				}
			}
		}

		private void Map_OnMarkerClick(GMapMarker item, MouseEventArgs e)
		{
			CkAddMarker.Checked = false;

			if (item.Overlay != MapData.MapOverlayCustom)
				return;

			UnselectMarkers();

			if (item is GMarkerBriefop selectedGmb)
				selectedGmb.IsSelected = true;
		}
		#endregion

		private void Map_OnMarkerEnter(GMapMarker item)
		{
			if (item.Overlay == MapData.MapOverlayCustom && item is GMarkerBriefop gmb)
				gmb.IsHovered = true;

		}

		private void Map_OnMarkerLeave(GMapMarker item)
		{
			if (item is GMarkerBriefop gmb)
				gmb.IsHovered = false;
		}

		private void Map_MouseMove(object sender, MouseEventArgs e)
		{

		}
	}
}
