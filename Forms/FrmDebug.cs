using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.Tools;
using Microsoft.VisualBasic.Logging;
using OSGeo.GDAL;
using OSGeo.OSR;
using static DcsBriefop.Tools.ToolsSpeeds;

namespace DcsBriefop.Forms
{
	internal partial class FrmDebug : Form
	{
		#region Fields
		#endregion

		#region CTOR
		public FrmDebug()
		{
			InitializeComponent();
			CoordinatesInitialize();
		}
		#endregion

		#region Methods
		#endregion

		#region Events
		private void FrmDebug_Shown(object sender, EventArgs e)
		{
			NudKtas.Value = 250;
			NudAltitude.Value = 10000;
		}

		private void BtConvertSpeed_Click(object sender, EventArgs e)
		{
			double dKtas = (double)NudKtas.Value;
			double dAltitudeMeters = (double)NudAltitude.Value;
			//double dAltitudeMeters = UnitConverter.Convert(dAltitudeFeet, UnitsNet.Units.LengthUnit.Foot, UnitsNet.Units.LengthUnit.Meter);
			ComputedSpeeds speeds = ToolsSpeeds.ConvertTrueAirSpeed(dKtas, dAltitudeMeters);
			NudKias.Value = (decimal)speeds.KIAS;
		}
		#endregion

		#region Coordinates
		//https://www.codeproject.com/Tips/1072197/Coordinate-Transformation-Using-Proj-in-NET
		//https://proj.org/en/9.3/usage/ellipsoids.html
		//    "EPSG:4326" => "+title=long/lat:WGS84 +proj=longlat +a=6378137.0 +b=6356752.31424518 +ellps=WGS84 +datum=WGS84 +units=degrees",
		private Theatre m_theatreProjection;
		private void CoordinatesInitialize()
		{
			CbTheatre.ValueMember = "Value";
			CbTheatre.DisplayMember = "Key";
			CbTheatre.DataSource = new Dictionary<string, string>()
			{
				{ "Caucasus", ElementTheatreName.Caucasus},
				{ "Marianas", ElementTheatreName.Marianas},
				{ "Nevada", ElementTheatreName.Nevada},
				{ "Sinai", ElementTheatreName.Sinai},
				{ "Syria", ElementTheatreName.Syria}
			}.ToList();

			m_theatreProjection = new Theatre(CbTheatre.SelectedValue as string);
			TbProjectionDcs.Text = m_theatreProjection.ProjectionInfo.ToProj4String();
			TbProjectionBriefop.Text = TheatreProjectionManager.BriefopProjection.ToProj4String();

			NudX.Value = -240857m;
			NudY.Value = 515429m;
		}

		private void BtConvertDcsToGeo_Click(object sender, EventArgs e)
		{
			//https://gis.stackexchange.com/questions/427277/convert-local-coordinates-to-wgs84-in-c
			SpatialReference projDcs = new SpatialReference("");
			string proj = TbProjectionDcs.Text;//"+proj=omerc +lat_0=46.325454996 +lonc=7.99050231+alpha=60.4026421358 +gamma=90 +k=1 +x_0=350 +y_0=200 +datum=WGS84 +units=m +no_defs +type=crs";
			projDcs.ImportFromProj4(proj);

			SpatialReference wgs84Reference = new SpatialReference("");
			//wgs84Reference.ImportFromEPSG(4326);
			wgs84Reference.ImportFromProj4("+proj=longlat +a=6378137.0 +b=6356752.31424518 +ellps=WGS84 +datum=WGS84");

			CoordinateTransformation coordinateTransform = new CoordinateTransformation(projDcs, wgs84Reference);
			double[] xy = { (double)NudY.Value, (double)NudX.Value };
			double[] z = { 0 };
			coordinateTransform.TransformPoint(xy);
			NudLat.Value = (decimal)xy[1];
			NudLong.Value = (decimal)xy[0];

			Coordinate c = new Coordinate(xy[1], xy[0]);
			LbControlCoord.Text = c.ToStringDDM();
			/*
			Gdal.AllRegister();
			Dataset dataset = Gdal.Open(@"D:\TifExample\raster.tif", Access.GA_ReadOnly);
			String WKTFromTif = dataset.GetProjectionRef();

			Double[] gt = new double[6];
			dataset.GetGeoTransform(gt);
			Int32 Rows = dataset.RasterYSize;
			Int32 Cols = dataset.RasterXSize;
			Double upperLeftX = gt[0];
			Double upperLeftY = gt[3];
			Double lowerRightX = gt[0] + Cols * gt[1] + Rows * gt[2];
			Double lowerRightY = gt[3] + Cols * gt[4] + Rows * gt[5];

			Double[] upperLeftPoint = { upperLeftX, upperLeftY };
			Double[] lowerRightPoint = { lowerRightX, lowerRightY };

			SpatialReference currentSpatialReference = new SpatialReference(WKTFromTif);

			String EPSG4326WKT = "GEOGCS[\"WGS84 datum, Latitude-Longitude; Degrees\", DATUM[\"WGS_1984\", SPHEROID[\"World Geodetic System of 1984, GEM 10C\",6378137,298.257223563, AUTHORITY[\"EPSG\",\"7030\"]], AUTHORITY[\"EPSG\",\"6326\"]], PRIMEM[\"Greenwich\",0], UNIT[\"degree\",0.0174532925199433], AUTHORITY[\"EPSG\",\"4326\"]]";
			SpatialReference newSpatialReference = new SpatialReference(EPSG4326WKT);

			CoordinateTransformation coordinateTransform = new CoordinateTransformation(currentSpatialReference, newSpatialReference);

			coordinateTransform.TransformPoint(upperLeftPoint);
			coordinateTransform.TransformPoint(lowerRightPoint);
			*/
		/*
	DotSpatial.Projections.ProjectionInfo piOri = DotSpatial.Projections.ProjectionInfo.FromProj4String(TbProjectionDcs.Text);
	DotSpatial.Projections.ProjectionInfo piDest = DotSpatial.Projections.ProjectionInfo.FromProj4String(TbProjectionBriefop.Text);

	TbProjectionDcs.Text = piOri.ToProj4String();
	TbProjectionBriefop.Text = piDest.ToProj4String();

	double[] xy = { (double)NudY.Value, (double)NudX.Value };
	double[] z = { 0 };

	DotSpatial.Projections.Reproject.ReprojectPoints(xy, z, piOri, piDest, 0, 1);
	//Tuple<double, double> input = new((double)NudY.Value, (double)NudX.Value);
	//Tuple<double, double> output = ToolsCoordinate.ReprojectPoint(piOri, piDest, input);

	NudLat.Value = (decimal)xy[1];
	NudLong.Value = (decimal)xy[0];

	Coordinate c = new Coordinate(xy[1], xy[0]);
	LbControlCoord.Text = c.ToStringDDM();
		*/
	}
	#endregion

	private void BtConvetGeoToDcs_Click(object sender, EventArgs e)
		{
			//Theatre theatre = new Theatre(CbTheatre.Text);
			//CoordinateSharp.Coordinate c = new CoordinateSharp.Coordinate((double)NudLat.Value, (double)NudLong.Value);
			//theatre.GetDcsXY(out double dX, out double dY, c);
			//theatre.GetDcsZXOld(out double dZCtrl, out double dXCtrl, c);

			//NudX.Value = (decimal)dX;
			//NudY.Value = (decimal)dY;

			//LbControlCoord.Text = $"X={dXCtrl}  Y(Z)={dZCtrl}";

		}

		private void CbTheatre_SelectedIndexChanged(object sender, EventArgs e)
		{
			m_theatreProjection = new Theatre(CbTheatre.SelectedValue as string);
			TbProjectionDcs.Text = m_theatreProjection.ProjectionInfo.ToProj4String();
		}

		private void BtProjDcsReset_Click(object sender, EventArgs e)
		{
			TbProjectionDcs.Text = m_theatreProjection.ProjectionInfo.ToProj4String();
		}

		private void BtProjBriefopReset_Click(object sender, EventArgs e)
		{
			TbProjectionBriefop.Text = TheatreProjectionManager.BriefopProjection.ToProj4String();
		}
	}
}
