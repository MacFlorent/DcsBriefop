using CoordinateSharp;
using System.Collections.Generic;
using System.Linq;

namespace DcsBriefop.Briefing
{
	internal class AssetMapPoint : BaseBriefing
	{
		#region Fields
		protected CustomDataAssetMapPoint m_customData;
		#endregion

		#region Properties
		public int Number { get; set; }
		public virtual string Name { get; set; }
		public string Notes
		{
			get { return m_customData.Notes; }
			set { m_customData.Notes = value; }
		}

		public virtual Coordinate Coordinate { get; set; }
		#endregion

		#region CTOR
		public AssetMapPoint(BriefingPack briefingPack, Asset asset, int iNumber) : base(briefingPack)
		{
			Number = iNumber;

			m_customData = asset.CustomData.AssetMapPoints?.Where(_mp => _mp.Id == Number).FirstOrDefault();
			if (m_customData is null)
			{
				m_customData = new CustomDataAssetMapPoint() { Id = Number };
				if (asset.CustomData.AssetMapPoints is null)
					asset.CustomData.AssetMapPoints = new List<CustomDataAssetMapPoint>();
				asset.CustomData.AssetMapPoints.Add(m_customData);
			}

		}
		#endregion

		#region Methods
		public virtual bool IsOrbitStart()
		{
			return false;
		}
		#endregion
	}
}