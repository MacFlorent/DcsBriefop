using CoordinateSharp;
using DcsBriefop.MasterData;
using System;

namespace DcsBriefop.Briefing
{
	internal class BriefingPageOperations : BaseBriefingPage
	{
		//public Coordinate Bullseye
		//{
		//	get { return m_manager.BriefingPack.Theatre.GetCoordinate(dY, dX); }
		//}


		public CoordinateSharp.Coordinate Bullseye { get; private set; }

		public BriefingPageOperations(MissionManager manager, string sCoalition) : base(manager, sCoalition)
		{
			Title = "OPERATIONS";

			//Bullseye = 
			//decimal dY = lsdCoalition["bullseye"]["y"].GetDecimal();
			//decimal dX = lsdCoalition["bullseye"]["x"].GetDecimal();
			//Bullseye = mission.Theatre.GetCoordinate(dY, dX);

			//Bullseye = new BriefingWeather(m_manager);
		}

	}
}
