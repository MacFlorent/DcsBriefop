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
		//https://github.com/pydcs/dcs/tree/master/dcs/terrain
		private Theatre m_theatre;
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

			m_theatre = new Theatre(CbTheatre.SelectedValue as string);

			TbProjectionDcs.Text = m_theatre.TheatreSpatialReference.ToStringProj4();
			TbProjectionBriefop.Text = TheatreProjectionManager.BriefopSpatialReference.ToStringProj4();

			//CbTheatre.Text = "Caucasus";
			//NudX.Value = -240857m;
			//NudY.Value = 515429m;

			CbTheatre.Text = "Syria";
			NudX.Value = 188286m;
			NudY.Value = -179143m;
		}

		private void BtConvertDcsToGeo_Click(object sender, EventArgs e)
		{
			//https://gis.stackexchange.com/questions/427277/convert-local-coordinates-to-wgs84-in-c
			SpatialReference projDcs = new SpatialReference(TbProjectionDcs.Text);
			SpatialReference wgs84Reference = new SpatialReference(TbProjectionBriefop.Text);

			Tuple<double, double> result = ToolsCoordinate.TransformPoint(projDcs, wgs84Reference, new Tuple<double, double>((double)NudY.Value, (double)NudX.Value));
			NudLat.Value = (decimal)result.Item2;
			NudLong.Value = (decimal)result.Item1;

			Coordinate c = new Coordinate(result.Item2, result.Item1);
			LbControlCoord.Text = c.ToStringDDM();
		}

		private void BtConvetGeoToDcs_Click(object sender, EventArgs e)
		{
			SpatialReference projDcs = new SpatialReference(TbProjectionDcs.Text);
			SpatialReference wgs84Reference = new SpatialReference(TbProjectionBriefop.Text);

			Tuple<double, double> result = ToolsCoordinate.TransformPoint(wgs84Reference, projDcs, new Tuple<double, double>((double)NudLong.Value, (double)NudLat.Value));
			NudX.Value = (decimal)result.Item2;
			NudY.Value = (decimal)result.Item1;

			Coordinate c = new Coordinate((double)NudLat.Value, (double)NudLong.Value);
			LbControlCoord.Text = c.ToStringDDM();
		}

		private void CbTheatre_SelectedIndexChanged(object sender, EventArgs e)
		{
			m_theatre = new Theatre(CbTheatre.SelectedValue as string);
			TbProjectionDcs.Text = m_theatre.TheatreSpatialReference.ToStringProj4();
		}

		private void BtProjDcsReset_Click(object sender, EventArgs e)
		{
			TbProjectionDcs.Text = m_theatre.TheatreSpatialReference.ToStringProj4();
		}

		private void BtProjBriefopReset_Click(object sender, EventArgs e)
		{
			TbProjectionBriefop.Text = TheatreProjectionManager.BriefopSpatialReference.ToStringProj4();
		}
		#endregion
	}
}
