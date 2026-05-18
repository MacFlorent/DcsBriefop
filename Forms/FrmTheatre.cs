using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using Mapsui;
using Mapsui.Layers;
using Mapsui.Manipulations;

namespace DcsBriefop.Forms
{
	internal partial class FrmTheatre : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private Theatre m_theatre;
		private Color m_LayerColor = Color.OrangeRed;

		private ComboBox m_cbMapProvider;
		private UcCheckedDropDown m_dropDownOverlays;
		#endregion

		#region CTOR
		public FrmTheatre(BriefopManager briefopManager)
		{
			OverlayProviderOpenAIP.ApiKey = "xxxx";

			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			BuildProviderAndOverlayControls();

			CbTheatre.ValueMember = "Value";
			CbTheatre.DisplayMember = "Key";
			CbTheatre.DataSource = new Dictionary<string, string>()
			{
				{ "Caucasus", ElementTheatreName.Caucasus},
				{ "Falklands", ElementTheatreName.Falklands},
				{ "Marianas", ElementTheatreName.Marianas},
				{ "Nevada", ElementTheatreName.Nevada},
				{ "Normandy", ElementTheatreName.Normandy},
				{ "Sinai", ElementTheatreName.Sinai},
				{ "Syria", ElementTheatreName.Syria},
				{ "The Channel", ElementTheatreName.TheChannel},
			}.ToList();

			string sProviderName = m_briefopManager?.BopMission.PreferencesMap.ProviderName ?? PreferencesManager.Preferences.Map.ProviderName;
			MapControl.InitializeMapControl(sProviderName);
		}

		private void BuildProviderAndOverlayControls()
		{
			Label lbMapProvider = new() { Text = "Provider:", AutoSize = true, Location = new Point(244, 13) };
			Controls.Add(lbMapProvider);
			lbMapProvider.BringToFront();

			m_cbMapProvider = new ComboBox { Location = new Point(308, 10), Size = new Size(180, 23), DropDownStyle = ComboBoxStyle.DropDownList };
			MapProviders.FillCombo(m_cbMapProvider, CbMapProvider_SelectedValueChanged);
			Controls.Add(m_cbMapProvider);
			m_cbMapProvider.BringToFront();

			Label lbOverlays = new() { Text = "Overlays:", AutoSize = true, Location = new Point(500, 13) };
			Controls.Add(lbOverlays);
			lbOverlays.BringToFront();

			m_dropDownOverlays = new UcCheckedDropDown { Location = new Point(562, 8), Size = new Size(200, 23), Label = "Overlays" };
			foreach (MapOverlays.OverlayRecord overlay in MapOverlays.All)
				m_dropDownOverlays.CheckedListBox.Items.Add(overlay.Name, false);
			m_dropDownOverlays.ItemCheckedChanged += DropDownOverlays_ItemCheckedChanged;
			Controls.Add(m_dropDownOverlays);
			m_dropDownOverlays.BringToFront();
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_cbMapProvider.SelectedValueChanged -= CbMapProvider_SelectedValueChanged;
			CbTheatre.SelectedIndexChanged -= CbTheatre_SelectedIndexChanged;

			string sProviderName = m_briefopManager?.BopMission.PreferencesMap.ProviderName ?? PreferencesManager.Preferences.Map.ProviderName;
			m_cbMapProvider.SelectedItem = MapProviders.TryGetProviderOrDefault(sProviderName);

			if (m_briefopManager is not null)
				CbTheatre.Text = m_briefopManager.BopMission.Theatre.Name;
			else
				CbTheatre.Text = ElementTheatreName.Caucasus;

			m_theatre = new(CbTheatre.SelectedValue as string);
			DisplayCurrentTheatre();

			m_cbMapProvider.SelectedValueChanged += CbMapProvider_SelectedValueChanged;
			CbTheatre.SelectedIndexChanged += CbTheatre_SelectedIndexChanged;
		}

		private void DisplayCurrentTheatre()
		{
			TbProjection.Text = m_theatre.TheatreSpatialReference.ToStringProj4();

			Coordinate centerCoordinate = m_theatre.GetCoordinate(0, 0);
			MapControl.Map.Navigator.CenterOnAndZoomTo(
				MapProjection.ToMPoint(centerCoordinate.Latitude.DecimalDegree, centerCoordinate.Longitude.DecimalDegree),
				MapProjection.ZoomToResolution(6), 0, null);
			TbMapDataStatic.Clear();
			LbMapDataDynamic.Text = null;

			foreach (MemoryLayer layer in MapControl.Map.Layers.OfType<MemoryLayer>().ToList())
				MapControl.Map.Layers.Remove(layer);

			List<IFeature> features = [];
			foreach (Airdrome airdrome in m_theatre.Airdromes)
			{
				GeoPoint pos = new(airdrome.Latitude, airdrome.Longitude);
				BriefopMarker marker = BriefopMarker.NewFromTemplateName(pos, ElementMapTemplateMarker.Airdrome, m_LayerColor, airdrome.Name, null, 0f, 1, 0);
				PointFeature feature = new(MapProjection.ToMPoint(marker.Position));
				feature.Styles.Add(new BriefopMarkerStyle(marker));
				features.Add(feature);
			}
			MapControl.Map.Layers.Add(new MemoryLayer { Style = null, Features = features });
		}

		private string GetStringCoordinates(Coordinate coordinate)
		{
			m_theatre.GetDcsXY(out double dDcX, out double dDcsY, coordinate);
			return $"{coordinate.ToStringDMS()} / {coordinate.ToStringDDM()} / {coordinate.ToStringMGRS()} / X={dDcX:0.00} Z(Y)={dDcsY:0.00}";
		}

		private IEnumerable<string> GetEnabledOverlayNames() => m_dropDownOverlays.CheckedNames;
		#endregion

		#region Events
		private void FrmTheatre_Shown(object sender, EventArgs e)
		{
			using (new WaitDialog(this))
				DataToScreen();
		}

		private void CbTheatre_SelectedIndexChanged(object sender, EventArgs e)
		{
			m_theatre = new Theatre(CbTheatre.SelectedValue as string);
			DisplayCurrentTheatre();
		}

		private void CbMapProvider_SelectedValueChanged(object sender, EventArgs e)
		{
			string sProviderName = (m_cbMapProvider.SelectedItem as MapProviders.MapProviderRecord)?.Name;
			MapControl.InitializeMapControl(sProviderName);
			MapControl.RefreshOverlayLayers(GetEnabledOverlayNames());
			DisplayCurrentTheatre();
		}

		private void DropDownOverlays_ItemCheckedChanged(object sender, EventArgs e)
		{
			MapControl.RefreshOverlayLayers(GetEnabledOverlayNames());
		}

		private void MapControl_MapPointerMoved(object sender, MapEventArgs e)
		{
			GeoPoint geoPoint = MapProjection.ToGeoPoint(e.WorldPosition);
			m_theatre.GetDcsXY(out double dDcX, out double dDcsY, geoPoint.Latitude, geoPoint.Longitude);
			LbMapDataDynamic.Text = $"Lat={geoPoint.Latitude:F6} Lng={geoPoint.Longitude:F6}  {{X={dDcX:0.00}, Z(Y)={dDcsY:0.00}}}";
		}

		private void MapControl_MapTapped(object sender, MapEventArgs e)
		{
			if (e.GestureType != GestureType.DoubleTap)
				return;

			GeoPoint geoPoint = MapProjection.ToGeoPoint(e.WorldPosition);
			Coordinate mapCoordinate = new(geoPoint.Latitude, geoPoint.Longitude);
			TbMapDataStatic.Text = GetStringCoordinates(mapCoordinate);

			foreach (MemoryLayer layer in MapControl.Map.Layers.OfType<MemoryLayer>().Where(_l => _l.Name == "ClickPoint").ToList())
				MapControl.Map.Layers.Remove(layer);

			BriefopMarker clickMarker = BriefopMarker.NewFromTemplateName(geoPoint, ElementMapTemplateMarker.Waypoint, m_LayerColor, null, null, 0f, 1, 0);
			PointFeature clickFeature = new(MapProjection.ToMPoint(clickMarker.Position));
			clickFeature.Styles.Add(new BriefopMarkerStyle(clickMarker));
			MapControl.Map.Layers.Add(new MemoryLayer { Name = "ClickPoint", Style = null, Features = [clickFeature] });
		}

		private void BtProjectionApply_Click(object sender, EventArgs e)
		{
			string sCurrentProjString = m_theatre.TheatreSpatialReference.ToStringProj4();
			if (sCurrentProjString == TbProjection.Text)
				return;

			m_theatre.TheatreSpatialReference = new SpatialReference(TbProjection.Text);
			DisplayCurrentTheatre();

			TbProjection.Text = m_theatre.TheatreSpatialReference.ToStringProj4();
		}

		private void BtProjectionReset_Click(object sender, EventArgs e)
		{
			m_theatre = new Theatre(CbTheatre.SelectedValue as string);
			DisplayCurrentTheatre();
		}
		#endregion
	}
}
