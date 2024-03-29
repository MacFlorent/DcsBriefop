﻿using DcsBriefop.Map;
using DcsBriefop.Tools;
using LsonLib;
using Newtonsoft.Json;

namespace DcsBriefop.Data
{
	#region Elements
	internal static class ElementDcsGroupType
	{
		public static readonly string Flight = "Flight";
		public static readonly string Ship = "Ship";
		public static readonly string Vehicle = "Vehicle";
		public static readonly string Static = "Static";
	}

	[Flags]
	internal enum ElementDcsObjectAttribute
	{
		None = 0,
		AircraftCarrier = 2,
		Artillery = 4,
		Tank = 16,
		AAW = 32,
		Radar = 64,
		EWR = 128,
		SAM = 256,
		AAA = 512,
		Infantry = 1024,
		AWACS = 2048,
		Tanker = 4096,
		Helicopters = 8192
	}
	#endregion

	#region DcsObject
	internal class DcsObject
	{
		#region Fields
		private DcsObjectJsonCustom m_objectJsonCustom;
		private List<DcsObjectJsonCustom> m_objectsJsonCustom;
		private List<string> m_lsonRawAttributes = new List<string>();
		private string m_rawDisplayName;
		#endregion

		#region Properties
		public string TypeName { get; private set; }

		public ElementGroupClass GroupClass { get; private set; }
		public ElementDcsObjectAttribute Attributes { get; private set; }

		public string DisplayName
		{
			get { return m_objectJsonCustom?.DisplayName ?? m_rawDisplayName; }
			set { GetOrAddJsonCustom().DisplayName = value; }
		}
		public string CustomMapMarker
		{
			get { return m_objectJsonCustom?.MapMarker; }
			set { GetOrAddJsonCustom().MapMarker = value; }
		}
		public string MapMarker { get { return CustomMapMarker ?? GetDefaultMarker(); } }
		public string Information
		{
			get { return m_objectJsonCustom?.Information; }
			set { GetOrAddJsonCustom().Information = value; }
		}
		public bool MainInGroup
		{
			get { return (m_objectJsonCustom?.MainInGroup).GetValueOrDefault(false); }
			set { GetOrAddJsonCustom().MainInGroup = value; }
		}
		#endregion

		#region CTOR
		public DcsObject(LsonDict lsd, List<DcsObjectJsonCustom> objectsJsonCustom)
		{
			LsonDict lsdDesc = lsd["desc"].GetDict();

			TypeName = ToolsLson.IfExistsString(lsdDesc, "typeName");
			m_rawDisplayName = ToolsLson.IfExistsString(lsdDesc, "displayName");

			LsonDict lsdAttributes = ToolsLson.IfExists(lsdDesc, "attributes")?.GetDictSafe();
			if (lsdAttributes is object)
			{
				foreach (KeyValuePair<LsonValue, LsonValue> kvp in lsdAttributes)
				{
					if (kvp.Value.GetBool())
						InitializeAttribute(kvp.Key.GetString());
				}
			}

			m_objectsJsonCustom = objectsJsonCustom;
			m_objectJsonCustom = m_objectsJsonCustom.Where(_u => _u.TypeName == TypeName).FirstOrDefault();
		}
		#endregion

		#region Methods
		public override string ToString() { return DisplayName; }

		private void InitializeAttribute(string sLsonAttribute)
		{
			m_lsonRawAttributes.Add(sLsonAttribute);

			if (sLsonAttribute == "Ground Units")
				GroupClass = ElementGroupClass.Ground;
			else if (sLsonAttribute == "Naval")
				GroupClass = ElementGroupClass.Sea;
			if (sLsonAttribute == "Air")
				GroupClass = ElementGroupClass.Air;

			if (sLsonAttribute == "AircraftCarrier")
				Attributes |= ElementDcsObjectAttribute.AircraftCarrier;
			if (sLsonAttribute == "Tanks")
				Attributes |= ElementDcsObjectAttribute.Tank;
			if (sLsonAttribute == "Infantry")
				Attributes |= ElementDcsObjectAttribute.Infantry;
			if (sLsonAttribute == "Artillery")
				Attributes |= ElementDcsObjectAttribute.Artillery;

			if (sLsonAttribute == "Air Defence")
				Attributes |= ElementDcsObjectAttribute.AAW;
			if (sLsonAttribute == "EWR")
				Attributes |= ElementDcsObjectAttribute.EWR;
			if (sLsonAttribute == "AAA")
				Attributes |= ElementDcsObjectAttribute.AAA;
			if (sLsonAttribute == "SAM")
				Attributes |= ElementDcsObjectAttribute.SAM;
			if (sLsonAttribute == "EWR" || sLsonAttribute == "SAM SR" || sLsonAttribute == "SR SAM" || sLsonAttribute == "SAM TR")
				Attributes |= ElementDcsObjectAttribute.Radar;

			if (sLsonAttribute == "AWACS")
				Attributes |= ElementDcsObjectAttribute.AWACS;
			if (sLsonAttribute == "Tankers")
				Attributes |= ElementDcsObjectAttribute.Tanker;
			if (sLsonAttribute == "Helicopters")
				Attributes |= ElementDcsObjectAttribute.Helicopters;
		}

		private DcsObjectJsonCustom GetOrAddJsonCustom()
		{
			if (m_objectJsonCustom is null)
			{
				m_objectJsonCustom = new DcsObjectJsonCustom() { TypeName = TypeName };
				m_objectsJsonCustom.Add(m_objectJsonCustom);
			}
			return m_objectJsonCustom;
		}

		private string GetDefaultMarker()
		{
			if ((Attributes & ElementDcsObjectAttribute.AAA) != 0)
				return "P91000015";
			if ((Attributes & ElementDcsObjectAttribute.SAM) != 0)
				return "P91000082";
			if ((Attributes & ElementDcsObjectAttribute.Radar) != 0)
				return "P91000036";
			if ((Attributes & ElementDcsObjectAttribute.AAW) != 0)
				return "P91000009";

			if ((Attributes & ElementDcsObjectAttribute.Tank) != 0)
				return "P91000001";
			if ((Attributes & ElementDcsObjectAttribute.Artillery) != 0)
				return "P91000006";
			if ((Attributes & ElementDcsObjectAttribute.Infantry) != 0)
				return "P91000201";

			if ((Attributes & ElementDcsObjectAttribute.AircraftCarrier) != 0)
				return "P91000065";

			if ((Attributes & ElementDcsObjectAttribute.AWACS) != 0)
				return "P91000061";
			if ((Attributes & ElementDcsObjectAttribute.Tanker) != 0)
				return "P91000060";
			if ((Attributes & ElementDcsObjectAttribute.Helicopters) != 0)
				return ElementMapTemplateMarker.Helicopter;

			if (GroupClass == ElementGroupClass.Ground)
				return ElementMapTemplateMarker.Ground;
			if (GroupClass == ElementGroupClass.Sea)
				return ElementMapTemplateMarker.Ship;
			if (GroupClass == ElementGroupClass.Air)
				return ElementMapTemplateMarker.Aircraft;

			return ElementMapTemplateMarker.DefaultMark;
		}

		public void CleanJsonCustom()
		{
			if (m_objectJsonCustom is null)
				return;

			if (m_objectJsonCustom.DisplayName == DisplayName)
				m_objectJsonCustom.DisplayName = null;
			if (string.IsNullOrEmpty(m_objectJsonCustom.DisplayName))
				m_objectJsonCustom.DisplayName = null;
			if (string.IsNullOrEmpty(m_objectJsonCustom.MapMarker))
				m_objectJsonCustom.MapMarker = null;
			if (string.IsNullOrEmpty(m_objectJsonCustom.Information))
				m_objectJsonCustom.Information = null;

			if (m_objectJsonCustom.DisplayName is object)
				return;
			if (m_objectJsonCustom.MapMarker is object)
				return;
			if (m_objectJsonCustom.Information is object)
				return;
			if (m_objectJsonCustom.MainInGroup)
				return;

			m_objectsJsonCustom.Remove(m_objectJsonCustom);
			m_objectJsonCustom = null;
		}
		#endregion
	}
	#endregion

	#region DcsObjectJsonCustom
	internal class DcsObjectJsonCustom
	{
		public string TypeName { get; set; }
		public string DisplayName { get; set; }
		public string MapMarker { get; set; }
		public string Information { get; set; }
		public bool MainInGroup { get; set; }
	}
	#endregion

	#region DcsObjectManager
	internal static class DcsObjectManager
	{
		public static List<DcsObject> DcsObjects { get; set; } = new List<DcsObject>();
		private static List<DcsObjectJsonCustom> m_objectsJsonCustom = new List<DcsObjectJsonCustom>();

		static DcsObjectManager()
		{
			try
			{
				Initialize();
			}
			catch (Exception e)
			{
				ToolsControls.ShowMessageBoxAndLogException("Failed to build unit data. Unit informations will not be available", e);
				Log.Exception(e);
			}
		}

		public static void Initialize()
		{
			string sJsonStream = ToolsResources.GetJsonResourceContent(ElementBopResource.DcsObjectCustom, null);
			if (!string.IsNullOrEmpty(sJsonStream))
				m_objectsJsonCustom = JsonConvert.DeserializeObject<List<DcsObjectJsonCustom>>(sJsonStream);

			string sLson = ToolsResources.GetTextResourceContent(ElementBopResource.DcsObject, "lua", null);   //https://github.com/mrSkortch/DCS-miscScripts/tree/master/ObjectDB
			Dictionary<string, LsonValue> rootLua = LsonVars.Parse(sLson);
			LsonDict lsd = rootLua["everyObject"].GetDict();

			foreach (LsonValue lsv in lsd.Values)
			{
				DcsObjects.Add(new DcsObject(lsv.GetDict(), m_objectsJsonCustom));
			}
		}

		public static void SaveJsonCustom()
		{
			foreach (DcsObject dcsObject in DcsObjects)
			{
				dcsObject.CleanJsonCustom();
			}
			string sResourceFilePath = ToolsResources.GetResourceFilePath(ElementBopResource.DcsObjectCustom, "json", null);
			File.WriteAllText(sResourceFilePath, JsonConvert.SerializeObject(m_objectsJsonCustom, Formatting.Indented));
		}

		public static DcsObject GetObject(string sType)
		{
			return DcsObjects.Where(_u => _u.TypeName == sType).FirstOrDefault();
		}
	}
	#endregion
}
