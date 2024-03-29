﻿using DcsBriefop.Data;
using DcsBriefop.Tools;

namespace DcsBriefop.DataBopBriefing
{
	public class BopBriefingStyle
	{
		#region Fields
		private string m_sCssFilePath;
		#endregion

		#region Properties
		public string Name { get; set; }
		#endregion

		#region CTOR
		public BopBriefingStyle(string sCssFilePath)
		{
			m_sCssFilePath = sCssFilePath;
			Name = Path.GetFileNameWithoutExtension(m_sCssFilePath);
		}
		#endregion

		#region Methods
		public override string ToString()
		{
			return Name;
		}

		public string GetCss()
		{
			string sCss = null;
			if (File.Exists(m_sCssFilePath))
			{
				sCss = File.ReadAllText(m_sCssFilePath);
			}

			return sCss;
		}
		#endregion

		#region Static
		private static List<BopBriefingStyle> m_templatesList = new();
		private static string m_sCssFilePathDefault;

		static BopBriefingStyle()
		{
			string sBaseDirectory = ToolsResources.GetResourceDirectoryPath(ElementBopResource.DirectoryHtml);
			m_sCssFilePathDefault = Path.Combine(sBaseDirectory, "simple-dark.css");

			if (Directory.Exists(sBaseDirectory))
			{
				foreach (string sFilePath in Directory.GetFiles(sBaseDirectory, "*.css", SearchOption.TopDirectoryOnly))
				{
					m_templatesList.Add(new BopBriefingStyle(sFilePath));
				}
			}
		}

		public static BopBriefingStyle GetElement(string sName)
		{
			return m_templatesList.Where(_e => _e.Name == sName).FirstOrDefault();
		}

		public static string GetCssDefault()
		{
			string sCss = null;
			if (File.Exists(m_sCssFilePathDefault))
			{
				sCss = File.ReadAllText(m_sCssFilePathDefault);
			}

			return sCss;
		}

		public static void FillCombo(ComboBox cb, EventHandler selectedValueChanged)
		{
			ToolsControls.FillCombo(cb, m_templatesList, "Name", "Name", selectedValueChanged);
		}
		#endregion
	}
}
