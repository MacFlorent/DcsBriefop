﻿using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using System.Linq;

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
		public bool BullseyeWaypoint { get; set; }
		public Coordinate Bullseye { get; set; }

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

		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			Bullseye = Theatre.GetCoordinate(m_mizCoalition.BullseyeY, m_mizCoalition.BullseyeX);
		}

		private void InitializeMizBopCustom()
		{
			m_mizBopCoalition = Miz.MizBopCustom.MizBopCoalitions.Where(_c => _c.CoalitionName == CoalitionName).FirstOrDefault();
			if (m_mizBopCoalition is null)
			{
				FinalizeFromMiz();
				m_mizBopCoalition = new MizBopCoalition() { CoalitionName = CoalitionName };
				m_mizBopCoalition.MapData = new MizBopMap();
				m_mizBopCoalition.MapData.CenterLatitude = Bullseye.Latitude.DecimalDegree;
				m_mizBopCoalition.MapData.CenterLongitude = Bullseye.Longitude.DecimalDegree;
				m_mizBopCoalition.MapData.Zoom = PreferencesManager.Preferences.Map.DefaultZoom;
				m_mizBopCoalition.MapData.MapOverlay = new GMapOverlay();

				m_mizBopCoalition.SetDefaultData();
				Miz.MizBopCustom.MizBopCoalitions.Add(m_mizBopCoalition);
			}
		}
		#endregion

		//#region Initialize & Persist
		//private void InitializeCustom()
		//{
		//	m_customCoalition = ParentManager.BopCustomMain.GetCoalition(m_mizCoalition.Name);
		//	if (m_customCoalition is null)
		//	{
		//		m_customCoalition = new MizBopCoalition() { CoalitionName = CoalitionName };
		//		ParentManager.BopCustomMain.BopCoalitions.Add(m_customCoalition);
		//	}

		//	//if (m_briefopCustomCoalition.ComPresets is object)
		//	//	ComPresets = m_briefopCustomCoalition.ComPresets.GetCopy();
		//}


		//#endregion

		//#region Methods


		//public string GetBullseyeCoordinatesString()
		//{
		//	return $"{Bullseye.ToStringDMS()}{Environment.NewLine}{Bullseye.ToStringDDM()}{Environment.NewLine}{Bullseye.ToStringMGRS()}";
		//}

		//public void ResetBullseyeMarkerDescription()
		//{
		//	if (m_markerkBullseye is object)
		//		m_markerkBullseye.Label = BullseyeDescription;
		//}

		//public void SetMapProvider(string sProviderName)
		//{
		//	MapData.Provider = sProviderName;
		//}
		//#endregion
	}
}
