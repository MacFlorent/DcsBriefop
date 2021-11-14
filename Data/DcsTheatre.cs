using DcsBriefop.Tools;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace DcsBriefop.Data
{
	internal class DcsTheatre
	{
		private class PointCoordinate
		{
			public decimal Y { get; set; }
			public decimal X { get; set; }
			public decimal Latitude { get; set; }
			public decimal Longitude { get; set; }
		}

		public string Name { get; private set; }

		private List<PointCoordinate> m_coordinatesLut;
		private List<decimal> m_coordinatesLutValuesY;
		private List<decimal> m_coordinatesLutValuesX;

		public List<DcsAirdrome> Airdromes;

		public DcsTheatre(string sName)
		{
			Name = sName;
			InitializeCoordinatesLut();
			InitializeAirdromes();

			//if (Name == Dcs.TheaterName.PersianGulf)
			//{
			//}

		}

		public CoordinateSharp.Coordinate GetCoordinate(decimal dY, decimal dX)
		{
			PointCoordinate pc = GetPointInterpolated(dY, dX);
			return new CoordinateSharp.Coordinate(decimal.ToDouble(pc.Latitude), decimal.ToDouble(pc.Longitude));
		}

		private PointCoordinate GetPointInterpolated(decimal dY, decimal dX)
		{
			if (m_coordinatesLut is null || m_coordinatesLut.Count < 0)
				return null;

			decimal? dLeftY = null, dRightY = null;
			decimal? dLowerX = null, dUpperX = null;

			foreach (decimal d in m_coordinatesLutValuesY)
			{
				if (dLeftY is object && dRightY is object)
					break;
				else if (d < dY)
				{
					dLeftY = d;
				}
				else
				{
					dRightY = d;
				}
			}
			foreach (decimal d in m_coordinatesLutValuesX)
			{
				if (dLowerX is object && dUpperX is object)
					break;
				else if (d < dX)
				{
					dLowerX = d;
				}
				else
				{
					dUpperX = d;
				}
			}

			// In mission files y is west-east and x is south-north
			PointCoordinate lowerLeft = null, lowerRight = null;
			PointCoordinate upperLeft = null, upperRight = null;

			foreach (PointCoordinate c in m_coordinatesLut)
			{
				if (lowerLeft is object && lowerRight is object && upperLeft is object && upperRight is object)
					break;
				else if (lowerLeft is null && c.Y == dLeftY && c.X == dLowerX)
					lowerLeft = c;
				else if (lowerRight is null && c.Y == dRightY && c.X == dLowerX)
					lowerRight = c;
				if (upperLeft is null && c.Y == dLeftY && c.X == dUpperX)
					upperLeft = c;
				if (upperRight is null && c.Y == dRightY && c.X == dUpperX)
					upperRight = c;
			}

			decimal dRatioY = (dY - lowerLeft.Y) / (lowerRight.Y - lowerLeft.Y);
			decimal dRatioX = (dX - lowerLeft.X) / (upperLeft.X - lowerLeft.X);

			PointCoordinate lowerInterpolation = new PointCoordinate
			{
				Latitude = lowerRight.Latitude * dRatioY + lowerLeft.Latitude * (1 - dRatioY),
				Longitude = lowerRight.Longitude * dRatioY + lowerLeft.Longitude * (1 - dRatioY)
			};
			PointCoordinate upperInterpolation = new PointCoordinate
			{
				Latitude = upperRight.Latitude * dRatioY + upperLeft.Latitude * (1 - dRatioY),
				Longitude = upperRight.Longitude * dRatioY + upperLeft.Longitude * (1 - dRatioY)
			};

			PointCoordinate interpolation = new PointCoordinate
			{
				Y = dY,
				X = dX,
				Latitude = upperInterpolation.Latitude * dRatioX + lowerInterpolation.Latitude * (1 - dRatioX),
				Longitude = upperInterpolation.Longitude * dRatioX + lowerInterpolation.Longitude * (1 - dRatioX)
			};

			return interpolation;
		}

		private void InitializeCoordinatesLut()
		{
			try
			{
				m_coordinatesLut = new List<PointCoordinate>();
				m_coordinatesLutValuesY = new List<decimal>();
				m_coordinatesLutValuesX = new List<decimal>();

				string sResourceContent = ToolsResources.GetTextResourceContent($"Points{Name}", "txt");
				string[] resourceContentLines = sResourceContent.Split('\n');
				foreach (string sLine in resourceContentLines)
				{
					InitializeCoordinatesLut_Line(sLine);
				}

				m_coordinatesLutValuesY.Sort();
				m_coordinatesLutValuesX.Sort();
			}
			catch (Exception e)
			{
				Log.Error("Failed to build point LUT. Coordinates will not be managed");
				Log.Exception(e);
				m_coordinatesLut = null;
				m_coordinatesLutValuesY = m_coordinatesLutValuesX = null;
			}
		}

		private void InitializeCoordinatesLut_Line(string sLine)
		{
			PointCoordinate pc = new PointCoordinate
			{
				Y = InitializeCoordinatesLut_GetLineItem(sLine, "Y="),
				X = InitializeCoordinatesLut_GetLineItem(sLine, "X="),
				Latitude = InitializeCoordinatesLut_GetLineItem(sLine, "Latitude="),
				Longitude = InitializeCoordinatesLut_GetLineItem(sLine, "Longitude="),
			};

			m_coordinatesLut.Add(pc);

			if (!m_coordinatesLutValuesY.Contains(pc.Y))
				m_coordinatesLutValuesY.Add(pc.Y);
			if (!m_coordinatesLutValuesX.Contains(pc.X))
				m_coordinatesLutValuesX.Add(pc.X);

		}

		private decimal InitializeCoordinatesLut_GetLineItem(string sLine, string sItem)
		{
			int iIndexStart = sLine.IndexOf(sItem) + sItem.Length;
			int iIndexEnd = sLine.IndexOf(',', iIndexStart);
			if (iIndexEnd < 0)
				iIndexEnd = sLine.Length;

			int iLength = iIndexEnd - iIndexStart;

			if (!decimal.TryParse(sLine.Substring(iIndexStart, iLength), out decimal dItemValue))
			{
				throw new ExceptionDcsBriefop($"Item {sItem} was not decoded from line {sLine} in point LUT resource");
			}

			return dItemValue;
		}

		private void InitializeAirdromes()
		{
			try
			{
				string sJsonStream = ToolsResources.GetJsonResourceContent($"Airdromes{Name}");
				Airdromes = JsonConvert.DeserializeObject<List<DcsAirdrome>>(sJsonStream);
			}
			catch (Exception e)
			{
				Log.Error("Failed to build airdrome data. Airdrome informations will not be available");
				Log.Exception(e);
				Airdromes = null;
			}
		}

		public DcsAirdrome GetAirdrome(int iId)
		{
			return Airdromes.Where(_ad => _ad.Id == iId).FirstOrDefault();
		}
	}
}

