using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Forms;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using HtmlTags;
using System.Text;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartGroups : BopBriefingPartBase
	{
		#region Fields
		private static class HtmlColumns
		{
			public static readonly string Coalition = "Coalition";
			public static readonly string GroupName = "Group name";
			public static readonly string Name = "Name";
			public static readonly string NameType = "Name/type";
			public static readonly string Type = "Type";
			public static readonly string Radio = "Radio";
			public static readonly string Localisation = "Localisation";
			public static readonly string Notes = "Notes";
		}
		public static List<string> AvailableHtmlColumns = new()
		{
			HtmlColumns.Coalition,
			HtmlColumns.GroupName,
			HtmlColumns.Name,
			HtmlColumns.NameType,
			HtmlColumns.Type,
			HtmlColumns.Radio,
			HtmlColumns.Localisation,
			HtmlColumns.Notes
		};
		#endregion

		#region Properties
		public ElementBriefingPartGroupType PartGroupType { get; set; } = ElementBriefingPartGroupType.GroupsAndUnits;
		public string Header { get; set; }
		public List<BopBriefingPartGroupOrUnit> GroupOrUnits { get; set; } = new();
		public List<string> SelectedHtmlColumns { get; set; } = new();
		#endregion

		#region CTOR
		public BopBriefingPartGroups() : base(ElementBriefingPartType.Groups, "table") { }
		#endregion

		#region Methods
		public override void InitializeDefault()
		{
			base.InitializeDefault();
			SelectedHtmlColumns = new List<string> { HtmlColumns.Coalition, HtmlColumns.GroupName, HtmlColumns.Name, HtmlColumns.Type, HtmlColumns.Notes };
		}

		public override string ToStringAdditional()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendWithSeparator(Header, " ");
			sb.AppendWithSeparator($"{GroupOrUnits.Count} groups", " - ");
			return sb.ToString();
		}

		//public static void GetPartGroupTypeDataGrid(out List<string> gridColumns, out List<BopGroupOrUnit> availableGroupOrUnits, ElementBriefingPartGroupType partGroupType, BopMission bopMission)
		//{
		//	availableGroupOrUnits = bopMission.GetGroupOrUnits();

		//	if (partGroupType == ElementBriefingPartGroupType.GroupsOnly)
		//	{
		//		gridColumns = new List<string>() { GridManagerGroupOrUnits.GridColumn.Coalition, GridManagerGroupOrUnits.GridColumn.Id, GridManagerGroupOrUnits.GridColumn.DisplayName, GridManagerGroupOrUnits.GridColumn.Type, GridManagerGroupOrUnits.GridColumn.Attributes };
		//		availableGroupOrUnits = availableGroupOrUnits.Where(_gou => _gou.GroupOrUnit == ElementGroupOrUnit.Group).ToList();
		//	}
		//	else if (partGroupType == ElementBriefingPartGroupType.UnitsOnly)
		//	{
		//		gridColumns = new List<string>() { GridManagerGroupOrUnits.GridColumn.Coalition, GridManagerGroupOrUnits.GridColumn.Id, GridManagerGroupOrUnits.GridColumn.DisplayName, GridManagerGroupOrUnits.GridColumn.Group, GridManagerGroupOrUnits.GridColumn.Type, GridManagerGroupOrUnits.GridColumn.Attributes };
		//		availableGroupOrUnits = availableGroupOrUnits.Where(_gou => _gou.GroupOrUnit == ElementGroupOrUnit.Unit).ToList();
		//	}
		//	else
		//	{
		//		gridColumns = new List<string>() { GridManagerGroupOrUnits.GridColumn.Coalition, GridManagerGroupOrUnits.GridColumn.GroupOrUnit, GridManagerGroupOrUnits.GridColumn.Id, GridManagerGroupOrUnits.GridColumn.DisplayName, GridManagerGroupOrUnits.GridColumn.Group, GridManagerGroupOrUnits.GridColumn.Type, GridManagerGroupOrUnits.GridColumn.Attributes };
		//	}
		//}

		//public static List<string> GetPartGroupTypeDataHtml(ElementBriefingPartGroupType partGroupType)
		//{
		//	List<string> htmlColumns;
		//	if (partGroupType == ElementBriefingPartGroupType.GroupsOnly)
		//	{
		//		htmlColumns = new List<string> { TableColumns.Coalition, TableColumns.Name, TableColumns.Type, TableColumns.Notes };
		//	}
		//	else
		//	{
		//		htmlColumns = new List<string> { TableColumns.Coalition, TableColumns.GroupName, TableColumns.Name, TableColumns.Type, TableColumns.Notes };
		//	}

		//	return htmlColumns;
		//}

		protected override IEnumerable<HtmlTag> BuildHtmlContent(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<HtmlTag> tags = new List<HtmlTag>();

			if (!string.IsNullOrEmpty(Header))
			{
				tags.Add(new HtmlTag("h2").Append(Header.HtmlLineBreaks()));
			}

			IEnumerable<string> columns = GetColumns();
			HtmlTag tagTable = new HtmlTag("table").Attr("width", "100%");
			HtmlTag tagThead = tagTable.Add("thead");
			foreach (string sColumn in columns)
			{
				tagThead.Add("td").AddClass("header").AppendText(sColumn);
			}

			foreach (BopGroupOrUnit element in GetBopGroupOrUnits(bopMission))
			{
				if (element is object)
				{
					HtmlTag tagTr = tagTable.Add("tr");
					foreach (string sColumn in columns)
					{
						element.FinalizeFromMiz();

						if (sColumn == HtmlColumns.Coalition)
							tagTr.Add("td").AppendText(element.Coalition);
						else if (sColumn == HtmlColumns.GroupName)
							tagTr.Add("td").AppendText(element.Group);
						else if (sColumn == HtmlColumns.Name)
							tagTr.Add("td").AppendText(element.DisplayName);
						else if (sColumn == HtmlColumns.NameType)
							tagTr.Add("td").Append($"{element.DisplayName}{Environment.NewLine}{element.Type.Truncate(20)}".HtmlLineBreaks());
						else if (sColumn == HtmlColumns.Type)
							tagTr.Add("td").AppendText(element.Type.Truncate(20));
						else if (sColumn == HtmlColumns.Radio)
							tagTr.Add("td").AppendText(element.Radio.ToString());
						else if (sColumn == HtmlColumns.Localisation)
							tagTr.Add("td").Append(element.ToStringLocalisation(bopBriefingFolder.CoordinateDisplay, bopBriefingFolder.MeasurementSystem)?.HtmlLineBreaks());
						else if (sColumn == HtmlColumns.Notes)
							tagTr.Add("td").AppendText(element.Additional);
					}
				}
			}

			tags.Add(tagTable);
			return tags;
		}

		public override IEnumerable<GMapOverlay> BuildMapOverlays(BopMission bopMission, BopBriefingFolder bopBriefingFolder)
		{
			List<GMapOverlay> partOverlays = new List<GMapOverlay>();
			foreach (BopGroupOrUnit element in GetBopGroupOrUnits(bopMission))
			{
				element.FinalizeFromMiz();
				partOverlays.Add(element.GetMapOverlay());
			}
			return partOverlays;
		}

		private IEnumerable<string> GetColumns()
		{
			if (SelectedHtmlColumns is not null && SelectedHtmlColumns.Count > 0)
				return AvailableHtmlColumns.Where(_c => SelectedHtmlColumns.Contains(_c));
			else
				return AvailableHtmlColumns;
		}

		public List<BopGroupOrUnit> GetBopGroupOrUnits(BopMission bopMission)
		{
			List<BopGroupOrUnit> missionGroupOrUnits = bopMission.GetGroupOrUnits();

			List<BopGroupOrUnit> groupOrUnits = new List<BopGroupOrUnit>();
			foreach (BopBriefingPartGroupOrUnit briefingGroupOrUnit in GroupOrUnits)
			{
				BopGroupOrUnit missionGroupOrUnit = null;
				if (briefingGroupOrUnit.Object == "unit")
					missionGroupOrUnit = missionGroupOrUnits.Where(_gou => (_gou.BopUnit?.Id).GetValueOrDefault(0) == briefingGroupOrUnit.Id).FirstOrDefault();
				else
					missionGroupOrUnit = missionGroupOrUnits.Where(_gou => (_gou.BopGroup?.Id).GetValueOrDefault(0) == briefingGroupOrUnit.Id).FirstOrDefault();

				if (missionGroupOrUnit is not null)
				{
					groupOrUnits.Add(missionGroupOrUnit);
				}
			}
			return groupOrUnits;
		}

		public void SetBopGroupOrUnits(IEnumerable<BopGroupOrUnit> bopGroupOrUnits)
		{
			GroupOrUnits.Clear();
			foreach (BopGroupOrUnit groupOrUnit in bopGroupOrUnits)
			{
				if (groupOrUnit.BopUnit is not null)
					GroupOrUnits.Add(new BopBriefingPartGroupOrUnit() { Id = groupOrUnit.BopUnit.Id, Object = "unit" });
				else
					GroupOrUnits.Add(new BopBriefingPartGroupOrUnit() { Id = groupOrUnit.BopGroup.Id, Object = "group" });
			}
		}
		#endregion
	}

	internal class BopBriefingPartGroupOrUnit
	{
		public int Id { get; set; }
		public string Object { get; set; }
	}
}
