using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using GMap.NET.WindowsForms;
using HtmlTags;
using System.Text;

namespace DcsBriefop.DataBopBriefing
{
	internal class BopBriefingPartGroups : BopBriefingPartBase
	{
		#region Fields
		private static class TableColumns
		{
			public static readonly string Coalition = "Coalition";
			public static readonly string Name = "Name";
			public static readonly string Class = "Class";
			public static readonly string Radio = "Radio";
			public static readonly string Localisation = "Localisation";
			public static readonly string Notes = "Notes";
		}
		public static List<string> AvailableColumns = new()
		{
			TableColumns.Coalition,
			TableColumns.Name,
			TableColumns.Class,
			TableColumns.Radio,
			TableColumns.Localisation,
			TableColumns.Notes
		};
		#endregion

		#region Properties
		public string Header { get; set; }
		public List<int> Groups { get; set; } = new();
		public List<string> SelectedColumns { get; set; } = new();
		#endregion

		#region CTOR
		public BopBriefingPartGroups() : base(ElementBriefingPartType.Groups, "table") { }
		#endregion

		#region Methods
		public override void InitializeDefault()
		{
			base.InitializeDefault();
			SelectedColumns.AddRange(new List<string> { TableColumns.Name, TableColumns.Class, TableColumns.Notes });
		}

		public override string ToStringAdditional()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendWithSeparator(Header, " ");
			sb.AppendWithSeparator($"{Groups.Count} groups", " - ");
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

			foreach (BopGroup element in GetOrderedElements(bopMission))
			{
				if (element is object)
				{
					HtmlTag tagTr = tagTable.Add("tr");
					foreach (string sColumn in columns)
					{
						element.FinalizeFromMiz();
						if (sColumn == TableColumns.Coalition)
							tagTr.Add("td").AppendText(element.CoalitionName);
						else if (sColumn == TableColumns.Name)
							tagTr.Add("td").AppendText(element.ToStringDisplayName());
						else if (sColumn == TableColumns.Class)
							tagTr.Add("td").AppendText(element.GroupClass.ToString());
						else if (sColumn == TableColumns.Radio)
							tagTr.Add("td").AppendText(element.Radio.ToString());
						else if (sColumn == TableColumns.Localisation)
							tagTr.Add("td").Append(element.Coordinate.ToString(bopBriefingFolder.CoordinateDisplay)?.HtmlLineBreaks());
						else if (sColumn == TableColumns.Name)
							tagTr.Add("td").AppendText(element.ToStringAdditional());
					}
				}
			}

			tags.Add(tagTable);
			return tags;
		}

		public override IEnumerable<GMapOverlay> BuildMapOverlays(BopMission bopMission)
		{
			List<GMapOverlay> partOverlays = new List<GMapOverlay>();
			foreach (BopGroup element in GetOrderedElements(bopMission))
			{
				element.FinalizeFromMiz();
				partOverlays.Add(element.GetMapOverlay());
			}
			return partOverlays;
		}

		private IEnumerable<string> GetColumns()
		{
			if (SelectedColumns is not null && SelectedColumns.Count > 0)
				return AvailableColumns.Where(_c => SelectedColumns.Contains(_c));
			else
				return AvailableColumns;
		}

		private IEnumerable<BopGroup> GetOrderedElements(BopMission bopMission)
		{
			return bopMission.Groups.Where(_e => Groups.Contains(_e.Id));
		}
		#endregion
	}

	internal class BopBriefingPartGroup
	{

	}
	}
