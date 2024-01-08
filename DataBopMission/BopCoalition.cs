using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace DcsBriefop.DataBopMission
{
	internal class BopCoalition : BaseBop
	{
		#region Fields
		private MizCoalition m_mizCoalition;
		private MizBopCoalition m_mizBopCoalition;
		#endregion

		#region Properties
		public string CoalitionName { get; set; }
		public string Task { get; set; }
		public string BullseyeDescription { get; set; }
		public ElementBullseyeWaypoint BullseyeWaypoint { get; set; }
		public Coordinate Bullseye { get; set; }
		public bool NoCallsignForPlayableFlights { get; set; }
		//public ListComPreset ComPresets { get; set; }

		public MizBopMap MapData { get { return m_mizBopCoalition.MapData; } }
		#endregion

		#region CTOR
		public BopCoalition(Miz miz, Theatre theatre, string sCoalitionName) : base(miz, theatre)
		{
			CoalitionName = sCoalitionName;
			m_mizCoalition = Miz.RootMission.Coalitions.Where(_c => _c.Name == CoalitionName).FirstOrDefault();
			InitializeMizBopCustom();

			if (CoalitionName == ElementCoalition.Red)
				Task = ToolsLua.DcsTextToDisplay(Miz.RootDictionary.RedTask);
			else if (CoalitionName == ElementCoalition.Blue)
				Task = ToolsLua.DcsTextToDisplay(Miz.RootDictionary.BlueTask);
			else if (CoalitionName == ElementCoalition.Neutral)
				Task = ToolsLua.DcsTextToDisplay(Miz.RootDictionary.NeutralTask);

			Bullseye = Theatre.GetCoordinate(m_mizCoalition.BullseyeY, m_mizCoalition.BullseyeX);
			Theatre.GetDcsZX(out double dZ, out double dX, Bullseye);
			if (dZ != m_mizCoalition.BullseyeY || dX != m_mizCoalition.BullseyeX)
			{

			}

			BullseyeDescription = m_mizBopCoalition.BullseyeDescription;
			BullseyeWaypoint = m_mizBopCoalition.BullseyeWaypoint;
		}
		#endregion

		#region Miz
		public override void ToMiz()
		{
			base.ToMiz();

			if (CoalitionName == ElementCoalition.Red)
				Miz.RootDictionary.RedTask = ToolsLua.DisplayToDcsText(Task);
			else if (CoalitionName == ElementCoalition.Blue)
				Miz.RootDictionary.BlueTask = ToolsLua.DisplayToDcsText(Task);
			else if (CoalitionName == ElementCoalition.Neutral)
				Miz.RootDictionary.NeutralTask = ToolsLua.DisplayToDcsText(Task);

			m_mizBopCoalition.BullseyeDescription = BullseyeDescription;
			m_mizBopCoalition.BullseyeWaypoint = BullseyeWaypoint;
		}

		private void InitializeMizBopCustom()
		{
			m_mizBopCoalition = Miz.MizBopCustom.MizBopCoalitions.Where(_c => _c.CoalitionName == CoalitionName).FirstOrDefault();
			if (m_mizBopCoalition is null)
			{
				Coordinate centerCoordinate = Theatre.GetCoordinate(m_mizCoalition.BullseyeY, m_mizCoalition.BullseyeX);
				m_mizBopCoalition = new MizBopCoalition() { CoalitionName = CoalitionName };
				m_mizBopCoalition.MapData = new MizBopMap();
				m_mizBopCoalition.MapData.CenterLatitude = centerCoordinate.Latitude.DecimalDegree;
				m_mizBopCoalition.MapData.CenterLongitude = centerCoordinate.Longitude.DecimalDegree;
				m_mizBopCoalition.MapData.Zoom = PreferencesManager.Preferences.Map.Zoom;
				m_mizBopCoalition.BullseyeWaypoint = PreferencesManager.Preferences.Mission.BullseyeWaypoint;

				Miz.MizBopCustom.MizBopCoalitions.Add(m_mizBopCoalition);
			}
		}
		#endregion

		#region Methods
		public GMapOverlay BuildStaticMapOverlay()
		{
			GMapOverlay staticMapOverlay = new GMapOverlay();
			staticMapOverlay.Markers.Add(GMarkerBriefop.NewFromTemplateName(new PointLatLng(Bullseye.Latitude.DecimalDegree, Bullseye.Longitude.DecimalDegree), ElementMapTemplateMarker.Bullseye, ToolsBriefop.GetCoalitionColor(CoalitionName), null, 1, 0));
			//ToolsMap.AddMizDrawingLayers(Theatre, MapOverlay, Miz.RootMission.DrawingLayers.Where(_dl => string.Compare(_dl.Name, ElementDrawingLayer.Common, true) == 0).ToList());
			ToolsMap.AddMizDrawingLayers(Theatre, staticMapOverlay, Miz.RootMission.DrawingLayers.Where(_dl => string.Compare(_dl.Name, CoalitionName, true) == 0).ToList());

			return staticMapOverlay;
		}

		public void UpdateBullseyeRoutePoint(BopRoutePoint bullseyeRoutePoint)
		{
			bullseyeRoutePoint.SetYX(m_mizCoalition.BullseyeY, m_mizCoalition.BullseyeX);
		}
		#endregion
	}
}
