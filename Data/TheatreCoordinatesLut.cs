﻿using DcsBriefop.Tools;

namespace DcsBriefop.Data
{
	internal class TheatreCoordinateLut
	{
		#region Properties
		public enum ElementLutWay { DcsToLl, LlToDcs }

		private class PointCoordinate
		{
			public double InputHorizontal { get; set; }
			public double InputVertical { get; set; }
			public double OutputHorizontal { get; set; }
			public double OutputVertical { get; set; }
		}

		public string Name { get; private set; }
		private string LutResourceName { get { return $"Points{Name}"; } }

		private ElementLutWay m_lutWay;
		private List<PointCoordinate> m_coordinatesLut;
		private List<double> m_coordinatesLutValuesHorizontal;
		private List<double> m_coordinatesLutValuesVertical;
		#endregion

		#region CTOR
		public TheatreCoordinateLut(string sName, ElementLutWay lutWay)
		{
			Name = sName;
			m_lutWay = lutWay;
			InitializeCoordinatesLut();
		}
		#endregion

		#region Methods
		public void GetCoordinates(out double? dOutputHorizontal, out double? dOutputVertical, double dInputHorizontal, double dInputVertical)
		{
			PointCoordinate pc = GetPointInterpolated(dInputHorizontal, dInputVertical);
			dOutputHorizontal = pc?.OutputHorizontal;
			dOutputVertical = pc?.OutputVertical;
		}

		private PointCoordinate GetPointInterpolated(double dHorizontal, double dVertical)
		{
			if (m_coordinatesLut is null || m_coordinatesLut.Count < 0)
				return null;

			double ? dLeft = null, dRight = null;
			double? dLower = null, dUpper = null;

			foreach (double d in m_coordinatesLutValuesHorizontal)
			{
				if (dLeft is not null && dRight is not null)
					break;
				else if (d < dHorizontal)
					dLeft = d;
				else
					dRight = d;
			}
			foreach (double d in m_coordinatesLutValuesVertical)
			{
				if (dLower is not null && dUpper is not null)
					break;
				else if (d < dVertical)
					dLower = d;
				else
					dUpper = d;
			}

			PointCoordinate lowerLeft = null, lowerRight = null;
			PointCoordinate upperLeft = null, upperRight = null;

			foreach (PointCoordinate c in m_coordinatesLut)
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
				throw new ExceptionBop($"Requested map position cannot be converted to coordinates because it is outside of the lookup table {LutResourceName}{Environment.NewLine}Horizontal={dHorizontal} Vertical={dVertical}");

			double dRatioHorizontal = (dHorizontal - lowerLeft.InputHorizontal) / (lowerRight.InputHorizontal - lowerLeft.InputHorizontal);
			double dRatioVertical = (dVertical - lowerLeft.InputVertical) / (upperLeft.InputVertical - lowerLeft.InputVertical);

			PointCoordinate lowerInterpolation = new();
			lowerInterpolation.OutputHorizontal = lowerRight.OutputHorizontal * dRatioHorizontal + lowerLeft.OutputHorizontal * (1 - dRatioHorizontal);
			lowerInterpolation.OutputVertical = lowerRight.OutputVertical * dRatioHorizontal + lowerLeft.OutputVertical * (1 - dRatioHorizontal);

			PointCoordinate upperInterpolation = new();
			upperInterpolation.OutputHorizontal = upperRight.OutputHorizontal * dRatioHorizontal + upperLeft.OutputHorizontal * (1 - dRatioHorizontal);
			upperInterpolation.OutputVertical = upperRight.OutputVertical * dRatioHorizontal + upperLeft.OutputVertical * (1 - dRatioHorizontal);

			PointCoordinate interpolation = new();
			interpolation.OutputHorizontal = upperInterpolation.OutputHorizontal * dRatioVertical + upperInterpolation.OutputHorizontal * (1 - dRatioVertical);
			interpolation.OutputVertical = upperInterpolation.OutputVertical * dRatioVertical + lowerInterpolation.OutputVertical * (1 - dRatioVertical);
			interpolation.InputHorizontal =  dHorizontal;
			interpolation.InputVertical = dVertical;

			return interpolation;
		}
		#endregion

		#region Initialization
		private void InitializeCoordinatesLut()
		{
			try
			{
				m_coordinatesLut = new List<PointCoordinate>();
				m_coordinatesLutValuesHorizontal = new List<double>();
				m_coordinatesLutValuesVertical = new List<double>();

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
					else if(bStarted)
						InitializeCoordinatesLut_Line(sLine);
				}

				m_coordinatesLutValuesHorizontal.Sort();
				m_coordinatesLutValuesVertical.Sort();
			}
			catch (Exception e)
			{
				ToolsControls.ShowMessageBoxAndLogException("Failed to build points LUT. Coordinates will not be managed.", e);
				m_coordinatesLut = null;
				m_coordinatesLutValuesHorizontal = m_coordinatesLutValuesVertical = null;
			}
		}

		private void InitializeCoordinatesLut_Line(string sLine)
		{
			double Z = InitializeCoordinatesLut_GetLineItem(sLine, "Z=");
			double X = InitializeCoordinatesLut_GetLineItem(sLine, "X=");
			double Latitude = InitializeCoordinatesLut_GetLineItem(sLine, "Latitude=");
			double Longitude = InitializeCoordinatesLut_GetLineItem(sLine, "Longitude=");

			PointCoordinate pc = new();
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

			m_coordinatesLut.Add(pc);

			if (!m_coordinatesLutValuesHorizontal.Contains(pc.InputHorizontal))
				m_coordinatesLutValuesHorizontal.Add(pc.InputHorizontal);
			if (!m_coordinatesLutValuesVertical.Contains(pc.InputVertical))
				m_coordinatesLutValuesVertical.Add(pc.InputVertical);
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

