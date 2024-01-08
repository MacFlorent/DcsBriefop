using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.DataMiz;
using DcsBriefop.Map;
using DcsBriefop.Tools;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using Newtonsoft.Json;

namespace DcsBriefop.Forms
{
	internal partial class FrmMissionMaps : Form
	{
		#region Fields
		private BriefopManager m_briefopManager;

		private UcMap m_ucMap;
		#endregion

		#region CTOR
		public FrmMissionMaps(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);

			RbMapSelectionRed.Tag = ElementCoalition.Red;
			RbMapSelectionBlue.Tag = ElementCoalition.Blue;
			RbMapSelectionNeutral.Tag = ElementCoalition.Neutral;

			MapProviders.FillCombo(CbMapProvider, CbMapProvider_SelectedValueChanged);
		}

		public static void CreateModal(BriefopManager briefopManager, Form parentForm)
		{
			using FrmMissionMaps f = new(briefopManager);
			f.ShowDialog(parentForm);
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			CbMapProvider.SelectedValueChanged -= CbMapProvider_SelectedValueChanged;

			CbMapProvider.SelectedItem = MapProviders.TryGetProvider(m_briefopManager.BopMission.PreferencesMap.ProviderName);

			m_ucMap = new UcMap();
			m_ucMap.Dock = DockStyle.Fill;
			PnMap.Controls.Clear();
			PnMap.Controls.Add(m_ucMap);

			DataToScreenDetail();

			CbMapProvider.SelectedValueChanged += CbMapProvider_SelectedValueChanged;
		}

		private void DataToScreenDetail()
		{
			string sCoalition = PnMapSelection.Controls.OfType<RadioButton>().Where(_rb => _rb.Checked).FirstOrDefault()?.Tag as string ?? "global";

			MizBopMap mapData = null;
			List<GMapOverlay> staticOverlays = new List<GMapOverlay>();
			if (sCoalition is not null && m_briefopManager.BopMission.Coalitions.ContainsKey(sCoalition))
			{
				BopCoalition bopCoalition = m_briefopManager.BopMission.Coalitions[sCoalition];
				mapData = bopCoalition.MapData;
				staticOverlays.Add(bopCoalition.BuildStaticMapOverlay());
			}
			else
			{
				mapData = m_briefopManager.BopMission.MapData;
				staticOverlays.Add(m_briefopManager.BopMission.BuildStaticMapOverlay());
			}

			m_ucMap.MapData = mapData;
			m_ucMap.StaticOverlays = staticOverlays;
			m_ucMap.MapProviderName = m_briefopManager.BopMission.PreferencesMap.ProviderName;
			m_ucMap.DataToScreen();
		}

		private void ScreenToData()
		{
			m_briefopManager.BopMission.PreferencesMap.ProviderName = (CbMapProvider.SelectedItem as GMapProvider)?.Name;
		}
		#endregion

		#region Events
		private void FrmMissionMaps_Shown(object sender, EventArgs e)
		{
			using (new WaitDialog(this))
				DataToScreen();
		}

		private void FrmMissionMaps_FormClosed(object sender, FormClosedEventArgs e)
		{
			ScreenToData();
		}

		private void RbMapSelection_CheckedChanged(object sender, EventArgs e)
		{
			DataToScreenDetail();
		}

		private void CbMapProvider_SelectedValueChanged(object sender, EventArgs e)
		{
			ScreenToData();
			DataToScreenDetail();
		}
		#endregion

		#region POC LOTATC drawings
		private void button1_Click(object sender, EventArgs e)
		{
			using (OpenFileDialog ofd = new OpenFileDialog())
			{
				ofd.InitialDirectory = PreferencesManager.Preferences.Application.WorkingDirectory;
				ofd.Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*";
				ofd.RestoreDirectory = true;

				if (ofd.ShowDialog() == DialogResult.OK)
				{
					string sJson = File.ReadAllText(ofd.FileName);
					LoadJsonDrawings(sJson);
				}
			}
		}

		private string HtmlToDcsColor(string sHtmlColor)
		{
			return "0x8000ffff";// $"0x{sHtmlColor.Substring(1, 6)}ff";
		}

		private void LoadJsonDrawings(string sJson)
		{
			JsonDrawings jsonDrawings = JsonConvert.DeserializeObject<JsonDrawings>(sJson);

			MizDrawingLayer mizDrawingLayerCommon = m_briefopManager.BopMission.Miz.RootMission.DrawingLayers.Where(_l => _l.Name == ElementDrawingLayer.Common).FirstOrDefault();
			if (mizDrawingLayerCommon is null)
				throw new ExceptionBop("No common drawing layer in mission. The layer must exist in the mission to allow for external drawings import.");

			mizDrawingLayerCommon.Objects.RemoveAll(_o => _o.Name.StartsWith("lotatc_"));

			foreach (JsonDrawing jsd in jsonDrawings.drawings)
			{
				MizDrawingObject mizDrawing = MizDrawingObject.NewFromLuaTemplate();
				mizDrawingLayerCommon.Objects.Add(mizDrawing);
				mizDrawing.Name = $"lotatc_{jsd.name}";
				mizDrawing.LayerName = ElementDrawingLayer.Common;
				mizDrawing.Visible = jsd.visible;

				if (jsd.type == "polygon")
				{
					mizDrawing.PrimitiveType = ElementDrawingPrimitive.Line;
					mizDrawing.Style = "solid";
					mizDrawing.LineMode = "segment";
					mizDrawing.ColorString = HtmlToDcsColor(jsd.color);
					mizDrawing.FillColorString = HtmlToDcsColor(jsd.colorBg);
					mizDrawing.Thickness = jsd.lineWidth;

					bool bFirst = true;
					foreach (JsonDrawingPoint point in jsd.points)
					{
						MizDrawingPoint mizDrawingPoint = MizDrawingPoint.NewFromLuaTemplate();
						mizDrawing.Points.Add(mizDrawingPoint);

						CoordinateSharp.Coordinate c = new CoordinateSharp.Coordinate(point.latitude, point.longitude);
						m_briefopManager.BopMission.Theatre.GetDcsZX(out double dZ, out double dX, c);

						if (bFirst)
						{
							mizDrawing.MapY = dZ;
							mizDrawing.MapX = dX;
							bFirst = false;
						}

						mizDrawingPoint.Y = dZ - mizDrawing.MapY;
						mizDrawingPoint.X = dX - mizDrawing.MapX;
					}

					break;
				}
			}

		}
		#endregion

	}

	internal class JsonDrawings
	{
		public string author;
		public List<JsonDrawing> drawings;
	}

	internal class JsonDrawing
	{
		public string author;
		public int brushStyle;
		public string color;
		public string colorBg;
		public Guid id;
		public int lineWidth;
		public string name;

		public List<JsonDrawingPoint> points;
		public bool shared;
		public string timestamp;
		public string type;
		public bool visible;
	}

	internal class JsonDrawingPoint
	{
		public double latitude;
		public double longitude;
	}

}
