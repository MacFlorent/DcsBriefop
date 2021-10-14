using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.MasterData
{
	internal static class ElementBriefingInclusionId
	{
		public static readonly int NotSet = -1;
		public static readonly int Excluded = 0;
		public static readonly int FullRoute = 1;
		public static readonly int Orbit = 2;
		public static readonly int Point = 3;
	}

	internal class BriefingInclusion
	{
		public int Id { get; set; }
		public string Label { get; set; }
		
		public override string ToString() { return Label; }

		#region Static data
		private static List<BriefingInclusion> m_elements = new List<BriefingInclusion>();

		static BriefingInclusion()
		{
			m_elements.Add(new BriefingInclusion() { Id = ElementBriefingInclusionId.Excluded, Label = "Excluded" });
			m_elements.Add(new BriefingInclusion() { Id = ElementBriefingInclusionId.FullRoute, Label = "Full briefing" });
			m_elements.Add(new BriefingInclusion() { Id = ElementBriefingInclusionId.Orbit, Label = "Orbit asset" });
			m_elements.Add(new BriefingInclusion() { Id = ElementBriefingInclusionId.Point, Label = "Point asset" });
		}

		public static BriefingInclusion GetById(int iId)
		{
			return m_elements.Where(_e => _e.Id == iId).FirstOrDefault();
		}

		public static void FillCombo(ComboBox cb)
		{
			cb.Items.Clear();
			foreach (BriefingInclusion bi in m_elements)
			{
				cb.Items.Add(bi);
			}
		}
		#endregion
	}
}
