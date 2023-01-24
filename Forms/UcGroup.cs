using DcsBriefop.Data;
using DcsBriefop.DataBopMission;
using DcsBriefop.Tools;
using System.Windows.Forms;

namespace DcsBriefop.Forms
{
	internal partial class UcGroup : UserControl
	{
		#region Fields
		private BriefopManager m_briefopManager;
		private BopGroup m_bopGroup;
		#endregion

		#region Properties
		public BopGroup BopGroup
		{
			private get { return m_bopGroup; }
			set
			{
				m_bopGroup = value;
				DataToScreen();
			}
		}
		#endregion

		#region CTOR
		public UcGroup(BriefopManager briefopManager)
		{
			m_briefopManager = briefopManager;

			InitializeComponent();
			ToolsStyle.ApplyStyle(this);
			ToolsStyle.LabelTitle(LbCoalition);
			ToolsStyle.LabelHeader(LbClass);
		}
		#endregion

		#region Methods
		private void DataToScreen()
		{
			m_bopGroup.FinalizeFromMiz();

			LbCoalition.Text = $"{m_bopGroup.CoalitionName} | {m_bopGroup.CountryName}";
			LbCoalition.BackColor = ToolsBriefop.GetCoalitionColor(m_bopGroup.CoalitionName);
			LbClass.Text = $"{m_bopGroup.Class} | {m_bopGroup.DcsObject}";
			TbId.Text = m_bopGroup.Id.ToString();
			CkLateActivation.Checked = m_bopGroup.LateActivation;
			CkPlayable.Checked = m_bopGroup.Playable;
			TbName.Text  = m_bopGroup.Name;
			TbDisplayName.Text = m_bopGroup.ToStringDisplayName();
			TbType.Text = m_bopGroup.Type;
			TbAttributes.Text = m_bopGroup.Attributes.ToString();
			TbFullDescription.Text = m_bopGroup.ToStringDescription();
		}

		public void ScreenToData()
		{
		}
		#endregion
	}
}
