using CoordinateSharp;
using DcsBriefop.Data;
using DcsBriefop.Tools;
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

			
			m_theatre.TheatreSpatialReference.ExportToProj4(out string sProj4);
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
			SpatialReference projDcs = new SpatialReference("");
			string sProj4 = TbProjectionDcs.Text;
			projDcs.ImportFromProj4(sProj4);

			SpatialReference wgs84Reference = new SpatialReference("");
			wgs84Reference.ImportFromProj4(TbProjectionBriefop.Text);
			
			CoordinateTransformation coordinateTransform = new CoordinateTransformation(projDcs, wgs84Reference);
			double[] xy = { (double)NudY.Value, (double)NudX.Value };
			coordinateTransform.TransformPoint(xy);
			NudLat.Value = (decimal)xy[1];
			NudLong.Value = (decimal)xy[0];

			Coordinate c = new Coordinate(xy[1], xy[0]);
			LbControlCoord.Text = c.ToStringDDM();
	}
	#endregion

	private void BtConvetGeoToDcs_Click(object sender, EventArgs e)
		{
			SpatialReference projDcs = new SpatialReference("");
			string sProj4 = TbProjectionDcs.Text;
			projDcs.ImportFromProj4(sProj4);

			SpatialReference wgs84Reference = new SpatialReference("");
			wgs84Reference.ImportFromProj4(TbProjectionBriefop.Text);

			CoordinateTransformation coordinateTransform = new CoordinateTransformation(wgs84Reference, projDcs);
			double[] xy = { (double)NudLong.Value, (double)NudLat.Value };
			coordinateTransform.TransformPoint(xy);
			NudX.Value = (decimal)xy[1];
			NudY.Value = (decimal)xy[0];

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
	}
}
