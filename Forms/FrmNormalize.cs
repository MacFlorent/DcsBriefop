using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using System.Data;

namespace DcsBriefop.Forms
{
	internal partial class FrmNormalize : Form
	{
		#region Fields
		private BriefopManager m_bopManager;
		private List<BopUnit> Units;
		private GridManagerUnits m_gmUnits;
		#endregion

		#region CTOR
		public FrmNormalize(BriefopManager bopManager)
		{
			m_bopManager = bopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			Units = new List<BopUnit>();
			foreach (BopGroupFlight group in m_bopManager.BopMission.Groups.OfType<BopGroupFlight>())
			{
				foreach (BopUnitFlight unit in group.Units)
				{
					Units.Add(unit);
				}
			}

			m_gmUnits = new GridManagerUnits(DgvUnits, Units);
			m_gmUnits.ColumnsDisplayed = new List<string>()
			{
				GridManagerUnits.GridColumn.Id,
				GridManagerUnits.GridColumn.DisplayName,
				GridManagerUnits.GridColumn.Type,
				GridManagerUnits.GridColumn.Callsign,
				GridManagerUnits.GridColumn.CallsignGroup,
				GridManagerUnits.GridColumn.CallsignElement,
				GridManagerUnits.GridColumn.DatalinkType,
				GridManagerUnits.GridColumn.DatalinkCallsign,
				GridManagerUnits.GridColumn.DatalinkId
			};
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_gmUnits.Refresh();
		}

		private void ScreenToData()
		{
		}

		private void NormalizeCallsigns(int iMaxGroupNumber)
		{
			using (new WaitDialog(this))
			{
				int i = 1;
				foreach (BopUnitFlight flight in Units.OfType<BopUnitFlight>().Where(_f => _f.Callsign is not null))
				{
					if (flight.Callsign.Group is int iGroup && (iGroup <= 0 || iGroup > iMaxGroupNumber))
					{
						flight.Callsign.Group = i++;
						if (i > iMaxGroupNumber)
							i = 1;
					}
				}

				DataToScreen();
			}
		}

		private void NormalizeDatalink(ElementDatalinkType datalinkType)
		{
			using (new WaitDialog(this))
			{
				int iIdLink16 = 0, iIdSadl = 0, iIdIdm = 0;

				foreach (BopUnit unit in Units.Where(_u => _u.DatalinkId is not null))
				{
					if (datalinkType == ElementDatalinkType.Link16 || datalinkType == ElementDatalinkType.None)
					{
						NormalizeUnitIfLink16(ref iIdLink16, unit);
					}
					if (datalinkType == ElementDatalinkType.Sadl || datalinkType == ElementDatalinkType.None)
					{
						NormalizeUnitIfSadl(ref iIdSadl, unit);
					}
					if (datalinkType == ElementDatalinkType.Idm || datalinkType == ElementDatalinkType.None)
					{
						NormalizeUnitIfIdm(ref iIdIdm, unit);
					}
				}

				DataToScreen();
			}
		}

		private void NormalizeUnitIfLink16(ref int iId, BopUnit unit)
		{
			if (unit.DatalinkId.DatalinkType != ElementDatalinkType.Link16)
				return;

			iId++;
			string sBase8 = Convert.ToString(iId, 8);
			unit.DatalinkId.Id = sBase8.PadLeft(5, '0');
		}

		private void NormalizeUnitIfSadl(ref int iId, BopUnit unit)
		{
			if (unit.DatalinkId.DatalinkType != ElementDatalinkType.Sadl)
				return;

			iId++;
			string sBase8 = Convert.ToString(iId, 8);
			unit.DatalinkId.Id = sBase8.PadLeft(4, '0');
		}

		private void NormalizeUnitIfIdm(ref int iId, BopUnit unit)
		{
			if (unit.DatalinkId.DatalinkType != ElementDatalinkType.Idm)
				return;

			iId++;
			unit.DatalinkId.Id = Base36Converter.ConvertTo(iId);
		}
		#endregion

		#region Events
		private void FrmDatalink_Shown(object sender, EventArgs e)
		{
			using (new WaitDialog(this))
				DataToScreen();
		}

		private void BtNormalize_MouseDown(object sender, MouseEventArgs e)
		{
			ContextMenuStrip menu = new ContextMenuStrip();
			menu.Items.Clear();

			menu.Items.AddMenuItem("All", (object _sender, EventArgs _e) => { NormalizeDatalink(ElementDatalinkType.None); });
			menu.Items.AddMenuSeparator();
			menu.Items.AddMenuItem(ElementDatalinkType.Link16.ToString(), (object _sender, EventArgs _e) => { NormalizeDatalink(ElementDatalinkType.Link16); });
			menu.Items.AddMenuItem(ElementDatalinkType.Sadl.ToString(), (object _sender, EventArgs _e) => { NormalizeDatalink(ElementDatalinkType.Sadl); });
			menu.Items.AddMenuItem(ElementDatalinkType.Idm.ToString(), (object _sender, EventArgs _e) => { NormalizeDatalink(ElementDatalinkType.Idm); });

			if (menu.Items.Count > 0)
			{
				menu.Show(BtNormalizeDatalink, new Point(0, BtNormalizeDatalink.Height));
			}
		}

		private void BtNormalizeCallsign_MouseDown(object sender, MouseEventArgs e)
		{
			ContextMenuStrip menu = new ContextMenuStrip();
			menu.Items.Clear();

			menu.Items.AddMenuItem("Over 9", (object _sender, EventArgs _e) => { NormalizeCallsigns(9); });
			menu.Items.AddMenuItem("Over 79", (object _sender, EventArgs _e) => { NormalizeCallsigns(79); });

			if (menu.Items.Count > 0)
			{
				menu.Show(BtNormalizeCallsign, new Point(0, BtNormalizeCallsign.Height));
			}
		}
		#endregion
	}
}
