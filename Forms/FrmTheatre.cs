using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using OSGeo.OSR;

namespace DcsBriefop.Forms
{
	internal partial class FrmTheatre : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private Theatre m_theatre;
		private TheatrePointLut m_pointsLutDcsToLl;
		private GMapOverlay m_mapOverlay;
		private GMapOverlay m_mapOverlayDynamic;
		private Color m_OverlayColor = Color.OrangeRed;
		#endregion


		#region CTOR
		public FrmTheatre(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			MapControl.InitializeMapControl(m_briefopManager?.BopMission.PreferencesMap.ProviderName ?? PreferencesManager.Preferences.Map.ProviderName);
			m_mapOverlay = new GMapOverlay();
			m_mapOverlayDynamic = new GMapOverlay();
			MapControl.Overlays.Add(m_mapOverlay);
			MapControl.Overlays.Add(m_mapOverlayDynamic);

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
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CbTheatre.SelectedIndexChanged -= CbTheatre_SelectedIndexChanged;

			if (m_briefopManager is not null)
			{
				CbTheatre.Text = m_briefopManager.BopMission.Theatre.Name;
			}
			else
			{
				CbTheatre.Text = ElementTheatreName.Caucasus;
			}

			m_theatre = new Theatre(CbTheatre.SelectedValue as string);
			DisplayCurrentTheatre();

			CbTheatre.SelectedIndexChanged += CbTheatre_SelectedIndexChanged;
		}

		private void DisplayCurrentTheatre()
		{
			m_pointsLutDcsToLl = new TheatrePointLut(m_theatre.Name, TheatrePointLut.ElementLutWay.DcsToLl);

			TbProjection.Text = m_theatre.TheatreSpatialReference.ToStringProj4();

			Coordinate centerCoordinate = m_theatre.GetCoordinate(0, 0);
			MapControl.Position = new PointLatLng(centerCoordinate.Latitude.DecimalDegree, centerCoordinate.Longitude.DecimalDegree);
			MapControl.Zoom = 6;
			m_mapOverlay.Clear();
			m_mapOverlayDynamic.Clear();
			TbMapDataStatic.Clear();
			LbMapDataDynamic.Text = null;

			m_mapOverlay.Markers.Add(GMarkerBriefop.NewFromTemplateName(new PointLatLng(centerCoordinate.Latitude.DecimalDegree, centerCoordinate.Longitude.DecimalDegree), ElementMapTemplateMarker.Mark, m_OverlayColor, "c", 1, 0));

			List<Tuple<double, double>> tuples = m_pointsLutDcsToLl.GetCornerPointsClockwise();

			List<PointLatLng> points = new List<PointLatLng>();
			PointLatLng? pFirst = null;
			foreach (Tuple<double, double> tuple in tuples)
			{
				Coordinate c = m_theatre.GetCoordinate(tuple.Item2, tuple.Item1);
				PointLatLng p = new PointLatLng(c.Latitude.DecimalDegree, c.Longitude.DecimalDegree);
				points.Add(p);

				if (pFirst is null)
					pFirst = p;

			}
			points.Add(pFirst.Value);
			GLineBriefop border = GLineBriefop.NewLineFromTemplateName(points, "", ElementMapTemplateLine.DashLine, m_OverlayColor, 3, m_OverlayColor, "");
			m_mapOverlay.Routes.Add(border);

			foreach (Airdrome ad in m_theatre.Airdromes)
			{
				GMarkerBriefop airdromeMarker = GMarkerBriefop.NewFromTemplateName(new PointLatLng(ad.Latitude, ad.Longitude), ElementMapTemplateMarker.Airdrome, m_OverlayColor, ad.Name, 1, 0);
				m_mapOverlay.Markers.Add(airdromeMarker);
			}
		}

		private string GetStringCoordinates(Coordinate coordinate)
		{
			m_theatre.GetDcsXY(out double dDcX, out double dDcsY, coordinate);
			return $"{coordinate.ToStringDMS()} / {coordinate.ToStringDDM()} / {coordinate.ToStringMGRS()} / X={dDcX:0.00} Z(Y)={dDcsY:0.00}";
		}

		private void CheckProjection()
		{
			string sCheck;
			using (new WaitDialog(this))
			{
				sCheck = m_pointsLutDcsToLl.CheckLut(m_theatre);
			}
			MessageBox.Show(sCheck);
		}
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

		private void BtCheck_Click(object sender, EventArgs e)
		{
			CheckProjection();
		}

		private void MapControl_MouseMove(object sender, MouseEventArgs e)
		{
			PointLatLng mapPoint = MapControl.FromLocalToLatLng(e.X, e.Y);
			m_theatre.GetDcsXY(out double dDcX, out double dDcsY, mapPoint.Lat, mapPoint.Lng);
			LbMapDataDynamic.Text = $"{mapPoint}  {{X={dDcX:0.00}, Z(Y)={dDcsY:0.00}}}";
		}

		private void MapControl_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			PointLatLng mapPoint = MapControl.FromLocalToLatLng(e.X, e.Y);
			Coordinate mapCoordinate = new Coordinate(mapPoint.Lat, mapPoint.Lng);

			m_mapOverlayDynamic.Markers.Clear();
			m_mapOverlayDynamic.Markers.Add(GMarkerBriefop.NewFromTemplateName(new PointLatLng(mapCoordinate.Latitude.DecimalDegree, mapCoordinate.Longitude.DecimalDegree), ElementMapTemplateMarker.Waypoint, m_OverlayColor, "", 1, 0));
			TbMapDataStatic.Text = GetStringCoordinates(mapCoordinate);
		}

		private void MapControl_OnMarkerClick(GMapMarker item, MouseEventArgs e)
		{
			if (item is GMarkerBriefop markerBriefop && item.Overlay == m_mapOverlay)
			{
				Airdrome airdrome = m_theatre.Airdromes.Where(_a => _a.Name == markerBriefop.Label).FirstOrDefault();
				if (airdrome is not null)
				{
					Coordinate adCoordinate = new Coordinate(airdrome.Latitude, airdrome.Longitude);
					TbMapDataStatic.Text = $"{GetStringCoordinates(adCoordinate)} / {airdrome.Name}[{airdrome.Id}] {airdrome.Tacan}";
				}

			}
		}

		private void BtProjectionApply_Click(object sender, EventArgs e)
		{
			string sCurrentProjString = m_theatre.TheatreSpatialReference.ToStringProj4();
			if (sCurrentProjString == TbProjection.Text)
				return;

			SpatialReference sr = new SpatialReference("");
			sr.ImportFromProj4(TbProjection.Text);
			m_theatre.TheatreSpatialReference = sr;
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
