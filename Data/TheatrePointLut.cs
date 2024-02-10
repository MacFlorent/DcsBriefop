using DcsBriefop.Tools;
using Microsoft.Extensions.Primitives;
using System.Text;

namespace DcsBriefop.Data
{
	internal class TheatrePointLut
	{
		#region Properties
		public enum ElementLutWay { DcsToLl, LlToDcs }

		private class PointLut
		{
			public double InputHorizontal { get; set; }
			public double InputVertical { get; set; }
			public double OutputHorizontal { get; set; }
			public double OutputVertical { get; set; }
		}

		public string Name { get; private set; }
		private string LutResourceName { get { return $"Points{Name}"; } }

		private ElementLutWay m_lutWay;
		private List<PointLut> m_pointsLut;
		private List<double> m_pointsLutValuesHorizontal;
		private List<double> m_pointsLutValuesVertical;
		#endregion

		#region CTOR
		public TheatrePointLut(string sName, ElementLutWay lutWay)
		{
			Name = sName;
			m_lutWay = lutWay;
			Initialize();
		}
		#endregion

		#region Methods
		public void GetPoint(out double? dOutputHorizontal, out double? dOutputVertical, double dInputHorizontal, double dInputVertical)
		{
			PointLut pc = GetPointInterpolated(dInputHorizontal, dInputVertical);
			dOutputHorizontal = pc?.OutputHorizontal;
			dOutputVertical = pc?.OutputVertical;
		}

		public List<Tuple<double, double>> GetCornerPointsClockwise()
		{
			if (m_pointsLut is null || m_pointsLut.Count < 0)
				return null;

			return new List<Tuple<double, double>>()
			{
				new (m_pointsLutValuesHorizontal.First(), m_pointsLutValuesVertical.First()),
				new (m_pointsLutValuesHorizontal.Last(), m_pointsLutValuesVertical.First()),
				new (m_pointsLutValuesHorizontal.Last(), m_pointsLutValuesVertical.Last()),
				new (m_pointsLutValuesHorizontal.First(), m_pointsLutValuesVertical.Last())
			};
		}

		public string CheckLut(Theatre theatre)
		{
			double dDeviationLatitudeTotal = 0, dDeviationLongitudeTotal = 0;
			double? dDeviationLatitudeMax = null, dDeviationLongitudeMax = null;
			string sDeviationLatitudeMax = null, sDeviationLongitudeMax = null;

			foreach (PointLut pl in m_pointsLut)
			{
				if (m_lutWay == ElementLutWay.DcsToLl)
				{
					double dLutLatitude = pl.OutputVertical;
					double dLutLongitude = pl.OutputHorizontal;
					double dLutX = pl.InputVertical;
					double dLutY = pl.InputHorizontal;
					theatre.GetCoordinate(out double dRpLatitude, out double dRpLongitude, dLutX, dLutY);

					double dDeviationLatitude = Math.Abs(dLutLatitude - dRpLatitude);
					double dDeviationLongitude = Math.Abs(dLutLongitude - dRpLongitude);
					dDeviationLatitudeTotal += dDeviationLatitude;
					dDeviationLongitudeTotal += dDeviationLongitude;

					string sLine = $"\tX={dLutX} Z(Y)={dLutY}\tlatdev={dDeviationLatitude:0.000000} lngdev={dDeviationLongitude:0.000000}\tdirect[ {dLutLatitude:0.000000} {dLutLongitude:0.000000} ]\treproj[ {dRpLatitude:0.000000} {dRpLongitude:0.000000} ]";
					Log.Info(sLine);

					if (dDeviationLatitudeMax is null || dDeviationLatitudeMax < dDeviationLatitude)
					{
						dDeviationLatitudeMax = dDeviationLatitude;
						sDeviationLatitudeMax = $"Max latdev={dDeviationLatitudeMax:0.000000} @ {sLine}";
					}
					if (dDeviationLongitudeMax is null || dDeviationLongitudeMax < dDeviationLongitude)
					{
						dDeviationLongitudeMax = dDeviationLongitude;
						sDeviationLongitudeMax = $"Max lngdev={dDeviationLongitudeMax:0.000000} @ {sLine}";
					}
				}
			}

			StringBuilder sb = new StringBuilder();
			sb.AppendLine(theatre.Name);
			sb.AppendLine($"Total dev={dDeviationLatitudeTotal + dDeviationLongitudeTotal:0.000000} ; total latdev={dDeviationLatitudeTotal:0.000000} ; total lngdev={dDeviationLongitudeTotal:0.000000}");
			sb.AppendLine(sDeviationLatitudeMax);
			sb.AppendLine(sDeviationLongitudeMax);
			string sOutput = sb.ToString();
			Log.Info(sOutput);

			return $"{sOutput}{Environment.NewLine}{Environment.NewLine}Check log file for full report.";
		}

		private PointLut GetPointInterpolated(double dHorizontal, double dVertical)
		{
			if (m_pointsLut is null || m_pointsLut.Count < 0)
				return null;

			double? dLeft = null, dRight = null;
			double? dLower = null, dUpper = null;

			foreach (double d in m_pointsLutValuesHorizontal)
			{
				if (dLeft is not null && dRight is not null)
					break;
				else if (d < dHorizontal)
					dLeft = d;
				else
					dRight = d;
			}
			foreach (double d in m_pointsLutValuesVertical)
			{
				if (dLower is not null && dUpper is not null)
					break;
				else if (d < dVertical)
					dLower = d;
				else
					dUpper = d;
			}

			PointLut lowerLeft = null, lowerRight = null;
			PointLut upperLeft = null, upperRight = null;

			foreach (PointLut c in m_pointsLut)
			{
				double dCurrentHorizontal = c.InputHorizontal;
				double dCurrentVertical = c.InputVertical;

				if (lowerLeft is not null && lowerRight is not null && upperLeft is not null && upperRight is not null)
					break;
				else if (lowerLeft is null && dCurrentHorizontal == dLeft && dCurrentVertical == dLower)
					lowerLeft = c;
				else if (lowerRight is null && dCurrentHorizontal == dRight && dCurrentVertical == dLower)
					lowerRight = c;
				if (upperLeft is null && dCurrentHorizontal == dLeft && dCurrentVertical == dUpper)
					upperLeft = c;
				if (upperRight is null && dCurrentHorizontal == dRight && dCurrentVertical == dUpper)
					upperRight = c;
			}

			if (lowerLeft is null || lowerRight is null || upperLeft is null || upperRight is null)
				throw new ExceptionBop($"Requested map position cannot be found in the lookup table {LutResourceName}{Environment.NewLine}Horizontal={dHorizontal} Vertical={dVertical}");

			double dRatioHorizontal = (dHorizontal - lowerLeft.InputHorizontal) / (lowerRight.InputHorizontal - lowerLeft.InputHorizontal);
			double dRatioVertical = (dVertical - lowerLeft.InputVertical) / (upperLeft.InputVertical - lowerLeft.InputVertical);

			PointLut lowerInterpolation = new();
			lowerInterpolation.OutputHorizontal = lowerRight.OutputHorizontal * dRatioHorizontal + lowerLeft.OutputHorizontal * (1 - dRatioHorizontal);
			lowerInterpolation.OutputVertical = lowerRight.OutputVertical * dRatioHorizontal + lowerLeft.OutputVertical * (1 - dRatioHorizontal);

			PointLut upperInterpolation = new();
			upperInterpolation.OutputHorizontal = upperRight.OutputHorizontal * dRatioHorizontal + upperLeft.OutputHorizontal * (1 - dRatioHorizontal);
			upperInterpolation.OutputVertical = upperRight.OutputVertical * dRatioHorizontal + upperLeft.OutputVertical * (1 - dRatioHorizontal);

			PointLut interpolation = new();
			interpolation.OutputHorizontal = upperInterpolation.OutputHorizontal * dRatioVertical + lowerInterpolation.OutputHorizontal * (1 - dRatioVertical);
			interpolation.OutputVertical = upperInterpolation.OutputVertical * dRatioVertical + lowerInterpolation.OutputVertical * (1 - dRatioVertical);
			interpolation.InputHorizontal = dHorizontal;
			interpolation.InputVertical = dVertical;

			return interpolation;
		}
		#endregion

		#region Initialization
		private void Initialize()
		{
			try
			{
				m_pointsLut = new List<PointLut>();
				m_pointsLutValuesHorizontal = new List<double>();
				m_pointsLutValuesVertical = new List<double>();

				string sLutResource = LutResourceName;
				string sResourceContent = ToolsResources.GetTextResourceContent(sLutResource, "txt", null);
				if (string.IsNullOrEmpty(sResourceContent))
					throw new ExceptionBop($"Empty or absent LUT data resource: {sLutResource}.");

				string[] resourceContentLines = sResourceContent.Split('\n');
				bool bStarted = false;
				string sStartText = m_lutWay == ElementLutWay.DcsToLl ? ">>> DCS TO LL" : ">>> LL TO DCS";

				foreach (string sLine in resourceContentLines)
				{
					if (sLine.Contains("BRIEFOP MAP POINTS"))
					{
						if (bStarted)
							break;
						else if (sLine.Contains(sStartText))
							bStarted = true;
					}
					else if (bStarted)
						Initialize_Line(sLine);
				}

				m_pointsLutValuesHorizontal.Sort();
				m_pointsLutValuesVertical.Sort();
			}
			catch (Exception e)
			{
				ToolsControls.ShowMessageBoxAndLogException("Failed to build points LUT. Coordinates will not be managed.", e);
				m_pointsLut = null;
				m_pointsLutValuesHorizontal = m_pointsLutValuesVertical = null;
			}
		}

		private void Initialize_Line(string sLine)
		{
			double Z = InitializeCoordinatesLut_GetLineItem(sLine, "Z=");
			double X = InitializeCoordinatesLut_GetLineItem(sLine, "X=");
			double Latitude = InitializeCoordinatesLut_GetLineItem(sLine, "Latitude=");
			double Longitude = InitializeCoordinatesLut_GetLineItem(sLine, "Longitude=");

			PointLut pc = new();
			if (m_lutWay == ElementLutWay.LlToDcs)
			{
				pc.InputHorizontal = Longitude;
				pc.InputVertical = Latitude;
				pc.OutputHorizontal = Z;
				pc.OutputVertical = X;
			}
			else
			{
				pc.InputHorizontal = Z;
				pc.InputVertical = X;
				pc.OutputHorizontal = Longitude;
				pc.OutputVertical = Latitude;
			}

			m_pointsLut.Add(pc);

			if (!m_pointsLutValuesHorizontal.Contains(pc.InputHorizontal))
				m_pointsLutValuesHorizontal.Add(pc.InputHorizontal);
			if (!m_pointsLutValuesVertical.Contains(pc.InputVertical))
				m_pointsLutValuesVertical.Add(pc.InputVertical);
		}

		private double InitializeCoordinatesLut_GetLineItem(string sLine, string sItem)
		{
			int iIndexStart = sLine.IndexOf(sItem) + sItem.Length;
			int iIndexEnd = sLine.IndexOf(',', iIndexStart);
			if (iIndexEnd < 0)
				iIndexEnd = sLine.Length;

			int iLength = iIndexEnd - iIndexStart;

			if (!double.TryParse(sLine.AsSpan(iIndexStart, iLength), out double dItemValue))
			{
				throw new ExceptionBop($"Item {sItem} was not decoded from line {sLine} in point LUT resource.");
			}

			return dItemValue;
		}
		#endregion
	}
}

