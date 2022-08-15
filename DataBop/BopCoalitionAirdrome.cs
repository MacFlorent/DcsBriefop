using DcsBriefop.Data;
using DcsBriefop.DataBopCustom;
using System.Collections.Generic;
using System.Drawing;

namespace DcsBriefop.DataBop
{
	internal class BopCoalitionAirdrome : BaseBop
	{
		#region Fields
		private BopCoalition m_bopCoalition;
		private BopCustomCoalitionAirdrome m_BopCustomCoalitionAirdrome;
		#endregion

		#region Properties
		public BopAirdrome BopAirdrome;
		public bool MapDisplay { get; set; }
		public Color MapColor { get; set; }
		public string Information { get; set; }
		public List<Radio> RadiosOverride { get; set; }
		public Tacan TacanOverride { get; set; }

		#endregion
		#region CTOR
		public BopCoalitionAirdrome(BopManager parentManager, BopCoalition bopCoalition, BopAirdrome bopAirdrome) : base(parentManager)
		{
			m_bopCoalition = bopCoalition;
			BopAirdrome = bopAirdrome;

			InitializeCustom();

			MapDisplay = m_BopCustomCoalitionAirdrome.MapDisplay;
			MapColor = ColorTranslator.FromHtml(m_BopCustomCoalitionAirdrome.MapColor);
			Information = m_BopCustomCoalitionAirdrome.Information;
			RadiosOverride = m_BopCustomCoalitionAirdrome.RadiosOverride;
			TacanOverride = m_BopCustomCoalitionAirdrome.TacanOverride;
		}
		#endregion

		#region Initialize & Persist
		private void InitializeCustom()
		{
			m_BopCustomCoalitionAirdrome = ParentManager.BopCustomMain.GetCoalition(m_bopCoalition.CoalitionName).GetAirdrome(BopAirdrome.Id);
			if (m_BopCustomCoalitionAirdrome is null)
			{
				m_BopCustomCoalitionAirdrome = new BopCustomCoalitionAirdrome(BopAirdrome.Id);
				ParentManager.BopCustomMain.GetCoalition(m_bopCoalition.CoalitionName).BopCustomCoalitionAirdromes.Add(m_BopCustomCoalitionAirdrome);
			}

			//if (m_briefopCustomCoalition.ComPresets is object)
			//	ComPresets = m_briefopCustomCoalition.ComPresets.GetCopy();
		}

		public override void PostInitialize()
		{
		}

		public override void Persist()
		{
			//if (CoalitionName == ElementCoalition.Red)
			//	Core.Miz.RootDictionary.RedTask = ToolsLua.DisplayToDcsText(Task);
			//else if (CoalitionName == ElementCoalition.Blue)
			//	Core.Miz.RootDictionary.BlueTask = ToolsLua.DisplayToDcsText(Task);
			//else if (CoalitionName == ElementCoalition.Neutral)
			//	Core.Miz.RootDictionary.NeutralTask = ToolsLua.DisplayToDcsText(Task);

			//m_briefopCustomCoalition.Included = Included;
			//m_briefopCustomCoalition.BullseyeDescription = BullseyeDescription;
			//m_briefopCustomCoalition.BullseyeWaypoint = BullseyeWaypoint;

			//if (ComPresets is object && ComPresets.Count > 0)
			//	m_briefopCustomCoalition.ComPresets = ComPresets.GetCopy();

			//foreach (Asset asset in OwnAssets)
			//	asset.Persist();
			//foreach (Asset asset in OpposingAssets)
			//	asset.Persist();
			//foreach (Asset asset in Airdromes)
			//	asset.Persist();
		}
		#endregion

		#region Methods
		#endregion

	}
}
