using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using System.IO.Compression;
using System.Text;

namespace DcsBriefop.Tools
{
	internal static class ToolsBriefop
	{
		public static Color GetCoalitionColor(string sCoalitionName)
		{
			if (sCoalitionName == ElementCoalition.Red)
				return ElementCoalitionColor.Red;
			else if (sCoalitionName == ElementCoalition.Blue)
				return ElementCoalitionColor.Blue;
			else
				return ElementCoalitionColor.Neutral;
		}

		public static string GetOpposingCoalitionName(string sCoalitionName)
		{
			if (sCoalitionName == ElementCoalition.Blue)
				return ElementCoalition.Red;
			else if (sCoalitionName == ElementCoalition.Red)
				return ElementCoalition.Blue;
			else
				return null;
		}

		public static string GetDefaultMapMarker(ElementGroupClass groupClass)
		{
			if (groupClass == ElementGroupClass.Air)
				return ElementMapTemplateMarker.Aircraft;
			else if (groupClass == ElementGroupClass.Sea)
				return ElementMapTemplateMarker.Ship;
			else if (groupClass == ElementGroupClass.Ground)
				return ElementMapTemplateMarker.Ground;
			else
				return ElementMapTemplateMarker.DefaultMark;
		}

		public static string MizCheck(string sMizFilePath)
		{
			if (!File.Exists(sMizFilePath))
				throw new ExceptionBop($"Mission miz file not found : {sMizFilePath}");

			int iCount = 0;
			StringBuilder sb = new StringBuilder();

			using (ZipArchive zipArchive = ZipFile.Open(sMizFilePath, ZipArchiveMode.Read))
			{
				foreach (ZipArchiveEntry zipEntryBriefopCustom in zipArchive.Entries.Where(_ze => _ze.FullName == Miz.BopCustomFileName).ToList())
				{
					iCount++;
					sb.AppendWithSeparator($"{zipEntryBriefopCustom.FullName} {zipEntryBriefopCustom.LastWriteTime.ToString(Thread.CurrentThread.CurrentCulture.DateTimeFormat)}", Environment.NewLine);
				}
			}

			if (iCount <= 0)
			{
				return "No Briefop data detected";
			}
			else if (iCount == 1)
			{
				return "Briefop data detected and correct";
			}
			else
			{
				throw new ExceptionBop($"Briefop data detected and incorrect{Environment.NewLine}{sb}");
			}
		}

		public static string GetUnitAltitude(ElementMeasurementSystem measurementSystem)
		{
			return measurementSystem == ElementMeasurementSystem.Imperial || measurementSystem == ElementMeasurementSystem.Hybrid ? "ft" : "m";
		}

		public static string GetUnitVisibility(ElementMeasurementSystem measurementSystem)
		{
			return measurementSystem == ElementMeasurementSystem.Imperial ? "SM" : "km";
		}

		public static string GetUnitSpeed(ElementMeasurementSystem measurementSystem)
		{
			return measurementSystem == ElementMeasurementSystem.Imperial || measurementSystem == ElementMeasurementSystem.Hybrid ? "kts" : "km/h";
		}

		public static string GetUnitTemperature(ElementMeasurementSystem measurementSystem)
		{
			return measurementSystem == ElementMeasurementSystem.Imperial ? "°F" : "°C";
		}
	}
}
