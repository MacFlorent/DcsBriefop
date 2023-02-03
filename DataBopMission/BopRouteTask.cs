using DcsBriefop.Data;
using DcsBriefop.DataMiz;
using DcsBriefop.Tools;
using System.Runtime.CompilerServices;
using System.Text;
using UnitsNet.Units;

namespace DcsBriefop.DataBopMission
{
	internal class BopRouteTask : BaseBop
	{
		#region Fields
		protected MizRouteTask m_mizRouteTask;
		#endregion

		#region Properties
		public bool Enabled { get; set; }
		public int? Number { get; set; }
		public string Id { get; set; }
		public int? UnitId { get; set; }
		#endregion

		#region CTOR
		public BopRouteTask(Miz miz, Theatre theatre, MizRouteTask mizRouteTask) : base(miz, theatre)
		{
			m_mizRouteTask = mizRouteTask;

			Enabled = m_mizRouteTask.Enabled;
		}

		#endregion

		#region Methods
		public override string ToString()
		{
			return Id;
		}

		public string ToStringDisplayName()
		{
			return $"{Number?.ToString() ?? "x"}:{ToString()}";
		}

		public virtual string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder(Enabled ? "" : "[disabled]");

			if (UnitId is object)
				sb.AppendWithSeparator($"Unit[{UnitId}]", " ");

			return sb.ToString();
		}
		#endregion
	}

	internal class BopRouteTaskOrbit : BopRouteTask
	{
		#region Properties
		public string Pattern { get; set; }
		public decimal? AltitudeFeet { get; set; }
		public decimal? SpeedKnots { get; set; }
		#endregion

		#region CTOR
		public BopRouteTaskOrbit(Miz miz, Theatre theatre, MizRouteTask mizRouteTask) : base(miz, theatre, mizRouteTask)
		{
			Id = m_mizRouteTask.Id;
			Number = m_mizRouteTask.Number;
			if (m_mizRouteTask.Params is MizRouteTaskParams taskParams)
			{
				Pattern = taskParams.Pattern;
			}
		}
		#endregion

		#region Miz
		protected override void FinalizeFromMizInternal()
		{
			base.FinalizeFromMizInternal();

			if (m_mizRouteTask.Params is MizRouteTaskParams taskParams)
			{
				if (taskParams.Altitude is object)
					AltitudeFeet = (decimal)UnitsNet.UnitConverter.Convert(taskParams.Altitude.Value, LengthUnit.Meter, LengthUnit.Foot);
				if (taskParams.Speed is object)
					SpeedKnots = (decimal)UnitsNet.UnitConverter.Convert(taskParams.Speed.Value, SpeedUnit.MeterPerSecond, SpeedUnit.Knot);
			}

		}
		#endregion

		#region Methods
		public override string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder(base.ToStringAdditionnal());

			if (!string.IsNullOrEmpty(Pattern))
				sb.AppendWithSeparator($"Pattern:{Pattern}", " ");
			if (AltitudeFeet is object)
				sb.AppendWithSeparator($"Altitude:{AltitudeFeet:0}", " ");
			if (SpeedKnots is object)
				sb.AppendWithSeparator($"Speed:{SpeedKnots:0}", " ");

			return sb.ToString();
		}

		#endregion
	}

	internal class BopRouteTaskFac : BopRouteTask
	{
		#region Properties
		public BopCallsign Callsign { get; set; }
		public Radio Radio { get; set; }
		#endregion

		#region CTOR
		public BopRouteTaskFac(Miz miz, Theatre theatre, MizRouteTask mizRouteTask) : base(miz, theatre, mizRouteTask)
		{
			Id = m_mizRouteTask.Id;
			if (m_mizRouteTask.Params is MizRouteTaskParams taskParams)
			{
				Number = taskParams.Number;
				Callsign = BopCallsign.NewFromJtacId(taskParams.Callname, taskParams.Number);
				Radio = new Radio(taskParams.Frequency.GetValueOrDefault(0) / ElementRadio.UnitFrequencyRatio, taskParams.Modulation.GetValueOrDefault(ElementRadioModulation.AM));
			}
		}
		#endregion

		#region Methods
		public override string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder(base.ToStringAdditionnal());

			if (Callsign is object)
				sb.AppendWithSeparator($"Callsign:{Callsign}", " ");
			if (Radio is object)
				sb.AppendWithSeparator($"Radio:{Radio}", " ");

			return sb.ToString();
		}

		#endregion
	}

	internal class BopRouteTaskCallsign : BopRouteTask
	{
		#region Properties
		public BopCallsign Callsign { get; set; }
		#endregion

		#region CTOR
		public BopRouteTaskCallsign(Miz miz, Theatre theatre, MizRouteTask mizRouteTask) : base(miz, theatre, mizRouteTask)
		{
			Number = m_mizRouteTask.Number;

			if (m_mizRouteTask.Params?.Action is MizRouteTaskAction taskAction)
			{
				UnitId = taskAction.ParamUnitId;
				Id = taskAction.Id;
				Callsign = BopCallsign.NewFromJtacId(taskAction.ParamCallname, taskAction.ParamNumber);
			}
		}
		#endregion

		#region Methods
		public override string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder(base.ToStringAdditionnal());

			if (Callsign is object)
				sb.AppendWithSeparator($"Callsign:{Callsign}", " ");

			return sb.ToString();
		}

		#endregion
	}

	internal class BopRouteTaskRadio : BopRouteTask
	{
		#region Properties
		public Radio Radio { get; set; }
		#endregion

		#region CTOR
		public BopRouteTaskRadio(Miz miz, Theatre theatre, MizRouteTask mizRouteTask) : base(miz, theatre, mizRouteTask)
		{
			Number = m_mizRouteTask.Number;

			if (m_mizRouteTask.Params?.Action is MizRouteTaskAction taskAction)
			{
				UnitId = taskAction.ParamUnitId;
				Id = taskAction.Id;
				Radio = new Radio(taskAction.ParamFrequency.GetValueOrDefault(0) / ElementRadio.UnitFrequencyRatio, taskAction.ParamModulation.GetValueOrDefault(ElementRadioModulation.AM));
			}
		}
		#endregion

		#region Methods
		public override string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder(base.ToStringAdditionnal());

			if (Radio is object)
				sb.AppendWithSeparator($"Radio:{Radio}", " ");

			return sb.ToString();
		}

		#endregion
	}

	internal class BopRouteTaskBeacon : BopRouteTask
	{
		#region Properties
		public Tacan Tacan { get; set; }
		#endregion

		#region CTOR
		public BopRouteTaskBeacon(Miz miz, Theatre theatre, MizRouteTask mizRouteTask) : base(miz, theatre, mizRouteTask)
		{
			Number = m_mizRouteTask.Number;

			if (m_mizRouteTask.Params?.Action is MizRouteTaskAction taskAction)
			{
				UnitId = taskAction.ParamUnitId;
				Id = taskAction.Id;
				Tacan = new Tacan() { Channel = taskAction.ParamChannel.GetValueOrDefault(0), Mode = taskAction.ParamModeChannel, Identifier = taskAction.ParamCallsign };
			}
		}

		#endregion

		#region Methods
		public override string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder(base.ToStringAdditionnal());

			if (Tacan is object)
				sb.AppendWithSeparator($"TACAN:{Tacan}", " ");

			return sb.ToString();
		}

		#endregion
	}

	internal class BopRouteTaskIcls : BopRouteTask
	{
		#region Properties
		public int? Icls { get; set; }
		#endregion

		#region CTOR
		public BopRouteTaskIcls(Miz miz, Theatre theatre, MizRouteTask mizRouteTask) : base(miz, theatre, mizRouteTask)
		{
			Number = m_mizRouteTask.Number;

			if (m_mizRouteTask.Params?.Action is MizRouteTaskAction taskAction)
			{
				UnitId = taskAction.ParamUnitId;
				Id = taskAction.Id;
				Icls = taskAction.ParamChannel;
			}
		}

		#endregion

		#region Methods
		public override string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder(base.ToStringAdditionnal());

			if (Icls is object)
				sb.AppendWithSeparator($"ICLS:{Icls}", " ");

			return sb.ToString();
		}

		#endregion
	}

	internal class BopRouteTaskLink4 : BopRouteTask
	{
		#region Properties
		public decimal? Link4 { get; set; }
		#endregion

		#region CTOR
		public BopRouteTaskLink4(Miz miz, Theatre theatre, MizRouteTask mizRouteTask) : base(miz, theatre, mizRouteTask)
		{
			Number = m_mizRouteTask.Number;

			if (m_mizRouteTask.Params?.Action is MizRouteTaskAction taskAction)
			{
				UnitId = taskAction.ParamUnitId;
				Id = taskAction.Id;
				Link4 = (decimal)taskAction.ParamFrequency / ElementRadio.UnitFrequencyRatio;
			}
		}

		#endregion

		#region Methods
		public override string ToStringAdditionnal()
		{
			StringBuilder sb = new StringBuilder(base.ToStringAdditionnal());

			if (Link4 is object)
				sb.AppendWithSeparator($"LNK4:{Link4:###.000}", " ");

			return sb.ToString();
		}

		#endregion
	}
}
