using DcsBriefop.Tools;
using Newtonsoft.Json;

namespace DcsBriefop.Data
{
	internal class Theatre
	{
		#region Properties
		public string Name { get; private set; }
		private string LutResourceName { get { return $"Points{Name}"; } }

		private TheatreCoordinateLut m_coordinatesLutDcsToLl;
		private TheatreCoordinateLut m_coordinatesLutLlToDcs;
		public List<Airdrome> Airdromes;

		#endregion

		#region CTOR
		public Theatre(string sName)
		{
			Name = sName;
			m_coordinatesLutDcsToLl = new TheatreCoordinateLut(sName, TheatreCoordinateLut.ElementLutWay.DcsToLl);
			m_coordinatesLutLlToDcs = new TheatreCoordinateLut(sName, TheatreCoordinateLut.ElementLutWay.LlToDcs);
			InitializeAirdromes();
		}
		#endregion

		#region Methods
		public Airdrome GetAirdrome(int iId)
		{
			return Airdromes.Where(_ad => _ad.Id == iId).FirstOrDefault();
		}

		public CoordinateSharp.Coordinate GetCoordinate(double dZ, double dX)
		{
			double? dOutputLongitude = null, dOutputLatitude = null;

			try
			{
				m_coordinatesLutDcsToLl.GetCoordinates(out dOutputLongitude, out dOutputLatitude, dZ, dX);
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
			}

			if (dOutputLongitude is null || dOutputLatitude is null)
				m_coordinatesLutDcsToLl.GetCoordinates(out dOutputLongitude, out dOutputLatitude, 0, 0);

			return new CoordinateSharp.Coordinate(dOutputLatitude.Value, dOutputLongitude.Value);
		}

		public void GetDcsZX(out double dZ, out double dX, CoordinateSharp.Coordinate coordinate)
		{
			double? dOutputZ = null, dOutputX = null;
			try
			{
				m_coordinatesLutLlToDcs.GetCoordinates(out dOutputZ, out dOutputX, coordinate.Longitude.DecimalDegree, coordinate.Latitude.DecimalDegree);
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
			}

			dZ = dOutputZ.GetValueOrDefault(0);
			dX = dOutputX.GetValueOrDefault(0);
		}
		#endregion

		#region Initialization
		private void InitializeAirdromes()
		{
			try
			{
				string sResource = $"Airdromes{Name}";
				string sJsonStream = ToolsResources.GetJsonResourceContent(sResource, null);
				if (string.IsNullOrEmpty(sJsonStream))
					throw new ExceptionBop($"Empty or absent airdrome resource: {sResource}.");

				Airdromes = JsonConvert.DeserializeObject<List<Airdrome>>(sJsonStream);
			}
			catch (Exception e)
			{
				ToolsControls.ShowMessageBoxAndLogException("Failed to build airdrome data. Airdrome informations will not be available.", e);
			}

			if (Airdromes is null)
				Airdromes = new List<Airdrome>();
		}
		#endregion
	}
}

