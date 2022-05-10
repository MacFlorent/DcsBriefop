using DcsBriefop.DataMiz;
using System.Linq;
using DcsBriefop.Map;
using System.Text;
using DcsBriefop.Tools;

namespace DcsBriefop.Data
{
	internal class AssetShip : AssetGroup
	{
		#region Fields
		private int m_iFrequencyRatio = 1000000;
		#endregion

		#region Properties
		private MizGroupShip GroupShip { get { return m_mizGroup as MizGroupShip; } }
		public MizUnitShip MainUnit { get; set; }
		public string UnitName { get { return MainUnit.Name; } }
		public Radio Radio { get; set; }

		#endregion

		#region CTOR
		public AssetShip(BaseBriefingCore core, BriefingCoalition coalition, ElementAssetSide side, MizGroupShip group) : base(core, coalition, side, group) { }
		#endregion

		#region Initialize
		protected override void InitializeData()
		{
			base.InitializeData();
			
			MapMarker = GetMarkerFromUnit() ?? ElementMapTemplateMarker.Ship;

				MainUnit = GroupShip.Units.OfType<MizUnitShip>().Where(_us => _us.Type.StartsWith("CVN")).FirstOrDefault();
			if (MainUnit is null)
				MainUnit = GroupShip.Units.OfType<MizUnitShip>().FirstOrDefault();

			Type = MainUnit.Type;
			Radio = new Radio() { Frequency = MainUnit.RadioFrequency / m_iFrequencyRatio, Modulation = MainUnit.RadioModulation ?? ElementRadioModulation.AM };

			if (Type.StartsWith("CVN"))
				Function = ElementAssetFunction.Base;
		}

		protected override void InitializeDataCustom()
		{
			m_briefopCustomGroup = Core.Miz.BriefopCustomData.GetGroup(Id, Coalition.CoalitionName);

			if (m_briefopCustomGroup is null)
			{
				m_briefopCustomGroup = new BriefopCustomGroup(Id, Coalition.CoalitionName);
				Core.Miz.BriefopCustomData.AssetGroups.Add(m_briefopCustomGroup);

				if (Function == ElementAssetFunction.Base)
				{
					m_briefopCustomGroup.Included = (Side == ElementAssetSide.Own);
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.Point;
				}
				else
				{
					m_briefopCustomGroup.Included = false;
					m_briefopCustomGroup.MapDisplay = (int)ElementAssetMapDisplay.None;
				}

				m_briefopCustomGroup.SetDefaultData();
			}

			Included = m_briefopCustomGroup.Included;
			MapDisplay = (ElementAssetMapDisplay)m_briefopCustomGroup.MapDisplay;
		}
		#endregion

		#region Methods
		public override void Persist()
		{
			base.Persist();

			MainUnit.RadioFrequency = Radio.Frequency * m_iFrequencyRatio;
			MainUnit.RadioModulation = Radio.Modulation;
		}

		protected override string GetDefaultInformation()
		{
			string sInformation = "";
			if (Side == ElementAssetSide.Own)
			{
				string sIcls = null, sLink4 = null;
				foreach (AssetRoutePoint routePoint in MapPoints.OfType<AssetRoutePoint>())
				{
					if (string.IsNullOrEmpty(sIcls))
					{
						MizRouteTask taskIcls = routePoint.MizRoutePoint.RouteTaskHolder.Tasks.Where(_rt => _rt.Params.Action?.Id == ElementRouteTask.ActivateIcls).FirstOrDefault();
						if (taskIcls?.Params.Action is MizRouteTaskAction rta)
							sIcls = $"ICLS {rta.ParamChannel}";
					}
					if (string.IsNullOrEmpty(sLink4))
					{
						MizRouteTask taskLink4 = routePoint.MizRoutePoint.RouteTaskHolder.Tasks.Where(_rt => _rt.Params.Action?.Id == ElementRouteTask.ActivateLink4).FirstOrDefault();
						if (taskLink4?.Params.Action is MizRouteTaskAction rta)
							sLink4 = $"LNK4 {rta.ParamFrequency / m_iFrequencyRatio:###.00}";
					}
				}

				StringBuilder sb = new StringBuilder($"{GetTacanString()}");
				sb.AppendWithSeparator(sIcls, " ");
				sb.AppendWithSeparator(sLink4, " ");

				sInformation = sb.ToString();
			}

			return sInformation;
		}

		public override string GetRadioString()
		{
			return Radio.ToString();
		}
		#endregion
	}
}
