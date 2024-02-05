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
		private DotSpatial.Projections.ProjectionInfo projWgs84 = DotSpatial.Projections.ProjectionInfo.FromProj4String("+proj=latlong +datum=WGS84 +no_defs");
		private void CoordinatesInitialize()
		{
			CbMap.ValueMember = "Value";
			CbMap.DisplayMember = "Key";
			CbMap.DataSource = new Dictionary<string, string>()
			{
				{"Caucasus", "+proj=tmerc +lat_0=0 +lon_0=33 +k_0=0.9996 +x_0=-99517 +y_0=-4998115"},
				{"Syria", "+proj=tmerc +lat_0=35.21917 +lon_0=35.9055 +k_0=0.9996 +x_0=0 +y_0=0"},
				{"PersianGulf", "+proj=tmerc +lat_0=26.1718 +lon_0=56.2419 +k_0=0.9996 +x_0=0 +y_0=0"},
				{"MarianaIslands", "+proj=tmerc +lat_0=35.21917 +lon_0=35.9055 +k_0=0.9996 +x_0=0 +y_0=0"},
			}.ToList();
		}

		private Tuple<double, double> Reproject(DotSpatial.Projections.ProjectionInfo src, DotSpatial.Projections.ProjectionInfo trg, Tuple<double, double> coordSrc)
		{
			double[] xy = { coordSrc.Item1 , coordSrc.Item2 };
			double[] z = { 0 };

			DotSpatial.Projections.Reproject.ReprojectPoints(xy, z, src, trg, 0, 1);

			return new Tuple<double, double>(xy[0], xy[1]);
		}

		private void BtConvertDcsToGeo_Click(object sender, EventArgs e)
		{
			string sSrcStringProj = CbMap.SelectedValue?.ToString();
			if (string.IsNullOrEmpty(sSrcStringProj))
				return;

			DotSpatial.Projections.ProjectionInfo src = DotSpatial.Projections.ProjectionInfo.FromProj4String(sSrcStringProj);

			Tuple<double, double> res = Reproject(src, projWgs84, new Tuple<double, double>((double)NudX.Value, (double)NudY.Value));

			NudLat.Value = (decimal)res.Item1;
			NudLong.Value = (decimal)res.Item2;
		}
		#endregion
	}
}
