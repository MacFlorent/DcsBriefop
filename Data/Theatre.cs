using DcsBriefop.Tools;
using Newtonsoft.Json;
using OSGeo.OSR;

namespace DcsBriefop.Data
{
	internal class Theatre
	{
		#region Properties
		public string Name { get; private set; }
		public SpatialReference TheatreSpatialReference { get; set; }

		private TheatrePointLut m_pointsLutDcsToLl;
		private TheatrePointLut m_pointsLutLlToDcs;
		public List<Airdrome> Airdromes;
		#endregion

		#region CTOR
		public Theatre(string sName)
		{
			Name = sName;

			TheatreSpatialReference = new SpatialReference("");
			string sProj4 = TheatreProjectionManager.GetProjection(Name);
			if (!string.IsNullOrEmpty(sProj4))
				TheatreSpatialReference.ImportFromProj4(TheatreProjectionManager.GetProjection(Name));

			m_pointsLutDcsToLl = new TheatrePointLut(sName, TheatrePointLut.ElementLutWay.DcsToLl);
			m_pointsLutLlToDcs = new TheatrePointLut(sName, TheatrePointLut.ElementLutWay.LlToDcs);

			InitializeAirdromes();
		}
		#endregion

		#region Methods
		public Airdrome GetAirdrome(int iId)
		{
			return Airdromes.Where(_ad => _ad.Id == iId).FirstOrDefault();
		}

		public CoordinateSharp.Coordinate GetCoordinate(double dDcsX, double dDcsY)
		{
			GetCoordinate(out double dOutputLatitude, out double dOutputLongitude, dDcsX, dDcsY);
			return new CoordinateSharp.Coordinate(dOutputLatitude, dOutputLongitude);
		}

		public void GetCoordinate(out double dOutputLatitude, out double dOutputLongitude, double dDcsX, double dDcsY)
		{
			// Coordinates in DCS: X vertical ; Y(Z) horizontal
			// Coordinates in the reprojection tool: Item1(x) is horizontal ; Item2(y) vertical

			Tuple<double, double> output = null;
			try
			{
				Tuple<double, double> input = new(dDcsY, dDcsX);
				output = ToolsCoordinate.TransformPoint(TheatreSpatialReference, TheatreProjectionManager.BriefopSpatialReference, input);
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
			}

			dOutputLongitude = output?.Item1 ?? 0;
			dOutputLatitude = output?.Item2 ?? 0;
		}

		public CoordinateSharp.Coordinate GetCoordinateOld(double dZ, double dX)
		{
			double? dOutputLongitude = null, dOutputLatitude = null;

			try
			{
				m_pointsLutDcsToLl.GetPoint(out dOutputLongitude, out dOutputLatitude, dZ, dX);
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
			}

			if (dOutputLongitude is null || dOutputLatitude is null)
				m_pointsLutDcsToLl.GetPoint(out dOutputLongitude, out dOutputLatitude, 0, 0);

			return new CoordinateSharp.Coordinate(dOutputLatitude.Value, dOutputLongitude.Value);
		}

		public void GetDcsXY(out double dX, out double dY, CoordinateSharp.Coordinate coordinate)
		{
			GetDcsXY(out dX, out dY, coordinate.Latitude.DecimalDegree, coordinate.Longitude.DecimalDegree);
		}

		public void GetDcsXY(out double dX, out double dY, double dLatitude, double dLongitude)
		{
			// Coordinates in DCS: X vertical ; Y(Z) horizontal
			// Coordinates in the DotSpatial reprojection: Item1(x) is horizontal ; Item2(y) vertical

			Tuple<double, double> output = null;
			try
			{
				Tuple<double, double> input = new(dLongitude, dLatitude);
				output = ToolsCoordinate.TransformPoint(TheatreProjectionManager.BriefopSpatialReference, TheatreSpatialReference, input);
			}
			catch (Exception ex)
			{
				Log.Exception(ex);
			}

			dY = output?.Item1 ?? 0;
			dX = output?.Item2 ?? 0;
		}

		public void GetDcsZXOld(out double dZ, out double dX, CoordinateSharp.Coordinate coordinate)
		{
			double? dOutputZ = null, dOutputX = null;
			try
			{
				m_pointsLutLlToDcs.GetPoint(out dOutputZ, out dOutputX, coordinate.Longitude.DecimalDegree, coordinate.Latitude.DecimalDegree);
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

			Airdromes ??= new List<Airdrome>();
		}
		#endregion
	}
}

