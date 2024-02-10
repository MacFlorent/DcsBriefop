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
		private DotSpatial.Projections.ProjectionInfo projWgs84 = DotSpatial.Projections.ProjectionInfo.FromProj4String("+proj=latlong +datum=WGS84 +no_defs");
		private void CoordinatesInitialize()
		{
			CbMap.DataSource = new List<string>()
			{
				"Caucasus",
				"MarianaIslands",
				"Nevada",
				"PersianGulf",
				"SinaiMap",
				"Syria"
			};
		}

		private void BtConvertDcsToGeo_Click(object sender, EventArgs e)
		{
			Theatre theatre = new Theatre(CbMap.Text);
			CoordinateSharp.Coordinate c = theatre.GetCoordinate((double)NudX.Value, (double)NudY.Value);
			CoordinateSharp.Coordinate cCtrl = theatre.GetCoordinateOld((double)NudY.Value, (double)NudX.Value);

			NudLat.Value = (decimal)c.Latitude.DecimalDegree;
			NudLong.Value = (decimal)c.Longitude.DecimalDegree;

			CoordinateFormatOptions cfo = new CoordinateFormatOptions();
			cfo.Format = CoordinateFormatType.Decimal;
			cfo.Round = 10;
			cfo.Display_Leading_Zeros = true;
			LbControlCoord.Text = cCtrl.ToString(cfo);
		}
		#endregion

		private void BtConvetGeoToDcs_Click(object sender, EventArgs e)
		{
			Theatre theatre = new Theatre(CbMap.Text);
			CoordinateSharp.Coordinate c = new CoordinateSharp.Coordinate((double)NudLat.Value, (double)NudLong.Value);
			theatre.GetDcsXY(out double dX, out double dY, c);
			theatre.GetDcsZXOld(out double dZCtrl, out double dXCtrl, c);

			NudX.Value = (decimal)dX;
			NudY.Value = (decimal)dY;

			LbControlCoord.Text = $"X={dXCtrl}  Y(Z)={dZCtrl}";

		}
	}
}
