using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.LsonStructure;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class AssetRoutePoint : AssetMapPoint
	{
		#region Fields
		protected RoutePoint m_routePoint;
		protected CustomDataAssetMissionPoint m_customData;
		#endregion

		#region Properties
		public string Notes
		{
			get { return m_customData.Notes; }
			set { m_customData.Notes = value; }
		}

		public string AltitudeFeet{ get { return $"{UnitsNet.UnitConverter.Convert(m_routePoint.Altitude, UnitsNet.Units.LengthUnit.Meter, UnitsNet.Units.LengthUnit.Foot):0} ft"; } }
		public string Action { get { return m_routePoint.Action; } }

		public List<RouteTask> RouteTasks
		{
			get { return m_routePoint.RouteTasks; }
		}
		#endregion

		#region CTOR
		public AssetRoutePoint(BriefingPack briefingPack, int iNumber, AssetGroup asset, RoutePoint routePoint) : base(briefingPack, iNumber)
		{
			m_routePoint = routePoint;

			if (!string.IsNullOrEmpty(m_routePoint.Name))
				Name =  m_routePoint.Name;
			else
				Name = $"WP{Number}";

			Coordinate = Theatre.GetCoordinate(m_routePoint.Y, m_routePoint.X);

			m_customData = asset.CustomData.AssetMissionPoints?.Where(_mp => _mp.Id == Number).FirstOrDefault();
			if (m_customData is null)
			{
				m_customData = new CustomDataAssetMissionPoint() { Id = Number };
				if (asset.CustomData.AssetMissionPoints is null)
					asset.CustomData.AssetMissionPoints = new List<CustomDataAssetMissionPoint>();
				asset.CustomData.AssetMissionPoints.Add(m_customData);
			}

		}
		#endregion

		#region Methods
		public override bool IsOrbitStart()
		{
			return RouteTasks.Where(_rt => _rt.Id == ElementRouteTask.Orbit).Any();
		}
		#endregion
	}
}