using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DcsBriefop.MasterData
{
	internal static class ElementGroupStatusId
	{
		public static readonly int NotSet = -1;
		public static readonly int Excluded = 0;
		public static readonly int FullRoute = 1;
		public static readonly int Orbit = 2;
		public static readonly int Point = 3;
	}

	internal class GroupStatus
	{
		public int Id { get; set; }
		public string Label { get; set; }
		
		public override string ToString() { return Label; }

		#region Static data
		private static List<GroupStatus> m_elements = new List<GroupStatus>();

		static GroupStatus()
		{
			m_elements.Add(new GroupStatus() { Id = ElementGroupStatusId.Excluded, Label = "Excluded" });
			m_elements.Add(new GroupStatus() { Id = ElementGroupStatusId.FullRoute, Label = "Full briefing" });
			m_elements.Add(new GroupStatus() { Id = ElementGroupStatusId.Orbit, Label = "Orbit asset" });
			m_elements.Add(new GroupStatus() { Id = ElementGroupStatusId.Point, Label = "Point asset" });
		}

		public static GroupStatus GetById(int iId)
		{
			return m_elements.Where(_e => _e.Id == iId).FirstOrDefault();
		}

		public static void FillCombo(ComboBox cb)
		{
			cb.Items.Clear();
			cb.ValueMember = "Id";
			cb.DisplayMember = "Label";
			cb.DataSource = m_elements;
		}
		#endregion
	}
}
