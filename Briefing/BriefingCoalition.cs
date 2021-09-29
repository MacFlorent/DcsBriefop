using CoordinateSharp;
using DcsBriefop.LsonStructure;
using DcsBriefop.MasterData;
using DcsBriefop.Tools;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class BriefingCoalition : BaseBriefing
	{
		#region Fields
		private Coalition m_coalition;
		#endregion

		#region Properties
		public Coordinate Bullseye
		{
			get { return Theatre.GetCoordinate(m_coalition.BullseyeY, m_coalition.BullseyeX); }
		}

		public string BullseyeCoordinates
		{
			get { return $"{Bullseye.ToStringDMS()}{Environment.NewLine}{Bullseye.ToStringDDM()}{Environment.NewLine}{Bullseye.ToStringMGRS()}"; }
		}

		public string BullseyeDescription
		{
			get { return RootCustom.GetCoalition(m_coalition.Code)?.BullseyeDescription; }
			set { RootCustom.GetCoalition(m_coalition.Code).BullseyeDescription = value; }
		}

		public string Name
		{
			get { return m_coalition.Code; }
		}

		public string Task
		{
			get
			{
				if (Name == ElementCoalition.Red)
					return RootDictionary.RedTask;
				else if (Name == ElementCoalition.Blue)
					return RootDictionary.BlueTask;
				else if (Name == ElementCoalition.Neutral)
					return RootDictionary.NeutralTask;
				else
					return null;
			}
			set
			{
				if (Name == ElementCoalition.Red)
					RootDictionary.RedTask = value;
				else if (Name == ElementCoalition.Blue)
					RootDictionary.BlueTask = value;
				else if (Name == ElementCoalition.Neutral)
					RootDictionary.NeutralTask = value;
			}
		}

		public List<BriefingFlight> GroupAirs { get; private set; } = new List<BriefingFlight>();
		public List<BriefingShip> GroupShips { get; private set; } = new List<BriefingShip>();
		// ships
		// planes

		public GMapOverlay MapOverlay { get; private set; }
		#endregion

		#region CTOR
		public BriefingCoalition(BriefingPack bp, string sCoalitionName) : base(bp)
		{
			m_coalition = RootMission.Coalitions.Where(c => c.Code == sCoalitionName).FirstOrDefault();

			foreach (Country c in m_coalition.Countries)
			{
				foreach (GroupFlight ga in c.GroupAirs)
				{
					GroupAirs.Add(new BriefingFlight(bp, ga));
				}
				foreach (GroupShip gs in c.GroupShips)
				{
					GroupShips.Add(new BriefingShip(bp, gs));
				}
			}

			InitializeMapOverlay();
		}
		#endregion

		#region Methods
		private void InitializeMapOverlay()
		{
			MapOverlay = new GMapOverlay("coalition");
			PointLatLng p = new PointLatLng(Bullseye.Latitude.DecimalDegree, Bullseye.Longitude.DecimalDegree);
			GMapMarker mkBullseye = new GMarkerGoogle(p, GMarkerGoogleType.orange_dot);
			mkBullseye.ToolTipText = BullseyeDescription;
			MapOverlay.Markers.Add(mkBullseye);
		}
		#endregion
	}
}
