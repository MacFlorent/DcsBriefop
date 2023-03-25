using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using HtmlTags;
using System.Text;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartGroups : BaseBopBriefingPart
	{
		#region Fields
		private static class TableColumns
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
		public static List<string> AvailableTableColumns = new()
		{
			TableColumns.Coalition,
			TableColumns.GroupName,
			TableColumns.Name,
			TableColumns.NameType,
			TableColumns.Type,
			TableColumns.Radio,
			TableColumns.Localisation,
			TableColumns.Notes
		};
		#endregion

		#region Properties
		public ElementBriefingPartGroupType PartGroupType { get; set; } = ElementBriefingPartGroupType.GroupsAndUnits;
		public string Header { get; set; }
		public List<BopBriefingPartGroupOrUnit> GroupOrUnits { get; set; } = new();
		public List<string> SelectedTableColumns { get; set; } = new();
		#endregion

		#region CTOR
		public BopBriefingPartGroups() : base(ElementBriefingPartType.Groups, "table") { }
		#endregion

		#region Methods
		public override void InitializeDefault()
		{
			base.InitializeDefault();
			SelectedTableColumns = new List<string> { TableColumns.Coalition, TableColumns.GroupName, TableColumns.Name, TableColumns.Type, TableColumns.Notes };
		}

		public override string ToStringAdditional()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendWithSeparator(Header, " ");
			sb.AppendWithSeparator($"{GroupOrUnits.Count} groups", " - ");
			return sb.ToString();
		}

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

						if (sColumn == TableColumns.Coalition)
							tagTr.Add("td").AppendText(element.Coalition);
						else if (sColumn == TableColumns.GroupName)
							tagTr.Add("td").AppendText(element.Group);
						else if (sColumn == TableColumns.Name)
							tagTr.Add("td").AppendText(element.DisplayName);
						else if (sColumn == TableColumns.NameType)
							tagTr.Add("td").Append($"{element.DisplayName}{Environment.NewLine}{element.Type.Truncate(20)}".HtmlLineBreaks());
						else if (sColumn == TableColumns.Type)
							tagTr.Add("td").AppendText(element.Type.Truncate(20));
						else if (sColumn == TableColumns.Radio)
							tagTr.Add("td").AppendText(element.Radio.ToString());
						else if (sColumn == TableColumns.Localisation)
							tagTr.Add("td").Append(element.ToStringLocalisation(bopBriefingFolder.CoordinateDisplay, bopBriefingFolder.MeasurementSystem)?.HtmlLineBreaks());
						else if (sColumn == TableColumns.Notes)
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
			if (SelectedTableColumns is not null && SelectedTableColumns.Count > 0)
				return AvailableTableColumns.Where(_c => SelectedTableColumns.Contains(_c));
			else
				return AvailableTableColumns;
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
