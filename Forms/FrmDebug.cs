using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.Tools;
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
