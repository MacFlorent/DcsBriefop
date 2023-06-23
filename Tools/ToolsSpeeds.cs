namespace DcsBriefop.Tools
{
	internal static class ToolsSpeeds
	{
		// this is taken from Rex excellent code in the VEAF mission tools
		// https://github.com/VEAF/VEAF-Mission-Creation-Tools/blob/master/src/scripts/veaf/veaf.lua
		
		public class ComputedSpeeds
		{
			public double KTAS;
			public double TAS_ms;
			public double KIAS;
			public double IAS_ms;
			public double Mach;
		}

		public static ComputedSpeeds ConvertMachSpeed(double dMach, double dAltitudeMeters)
		{
			return ConvertSpeeds(dMach, null, null, dAltitudeMeters, null, null);
		}

		public static ComputedSpeeds ConvertTrueAirSpeed(double dKtas, double dAltitudeMeters)
		{
			return ConvertSpeeds(null, null, dKtas, dAltitudeMeters, null, null);
		}

		public static ComputedSpeeds ConvertIndicatedAirSpeed(double dKias, double dAltitudeMeters)
		{
			return ConvertSpeeds(null, dKias, null, dAltitudeMeters, null, null);
		}

		private static ComputedSpeeds ConvertSpeeds(double? dMach, double? dKias, double? dKtas, double dAltitudeMeters, double? dTemperatureCelsius, double? dPressurePa)
		{
			ComputedSpeeds result = new ComputedSpeeds();

			double h_tropopause = 11000; //m, tropopause start altitude <

			double T0 = 288.15;  // K, ISA + 0 altitude, may need to be corrected for mission ground temp
			double Tz = -0.0065; // K / m, ISA temperature gradient in troposphere
			double T_tropopause = 216.65; // K, temperature at the border between tropopause and troposphere(temperature in the tropopause)
			double P0 = 101325; // Pa, standard pressure
			double Gamma = 1.4; // Air heat capacity ratio
			double r = 287.03; //J/kg / K Perfect Gas constant for air
			double g = 9.81; //m / s ^ 2 gravity constant on earth, might need to account for which planet ED is on

			double dTemperatureKelvin;
			if (dTemperatureCelsius is null)
			{
				// compute ISA temperature based on altitude
				if (dAltitudeMeters < h_tropopause)
				{
					dTemperatureKelvin = T0 + Tz * dAltitudeMeters; // troposphere(temp in K)
				}
				else
				{
					dTemperatureKelvin = T_tropopause; //tropopause(max altitude 20000m)(temp in K)
				}
			}
			else
			{
				dTemperatureKelvin = dTemperatureCelsius.Value + 273.15; // conversion to Kelvin
			}

			double P_troposphere(double dTemperatureKelvin)
			{
				return P0 * Math.Pow((1 + (dTemperatureKelvin - T0) / T0), -g / (r * Tz));
			}

			double dFinalPressurePa;
			if (dPressurePa is null)
			{
				// compute pressure based on altitude and ISA temperature
				if (dAltitudeMeters < h_tropopause)
					dFinalPressurePa = P_troposphere(dTemperatureKelvin);
				else
					dFinalPressurePa = P_troposphere(T_tropopause) * Math.Exp(-g * (dAltitudeMeters - h_tropopause) / (r * T_tropopause));
			}
			else
				dFinalPressurePa = dPressurePa.Value;


			double speedOfSound(double dTemperatureKelvin)
			{
				return Math.Sqrt(Gamma * r * dTemperatureKelvin);
			}

			double B = Gamma / (Gamma - 1);

			// - @param mach number mach number to calculate(Pt-Ps)/ Ps with Pt/ Ps given by isentropic relations(NOTE : (Pt - Ps) = deltaP)
			//@return number returns the ratio deltaP / P(DPP)(what a pitot tube would measure for M < 1)
			double isentropicDPP(double mach)
			{
				return Math.Pow(1 + (Gamma - 1) * Math.Pow(mach, 2) / 2, B) - 1;
			}

			// - @param mach number mach number to calculate(Pt-Ps)/ Ps after a normal shock(M> 1) (NOTE: (Pt - Ps) = deltaP)
			// - @return number returns the ratio deltaP / P(DPP) after the normal shock(what a pitot tube would measure for M > 1)
			double lord_rayleighDPP(double mach)
			{
				double A = Math.Pow(((Gamma + 1) * Math.Pow(mach, 2) / 2), B);
				double C = Math.Pow(((Gamma + 1) / (2 * Gamma * Math.Pow(mach, 2) - Gamma + 1)), (B / Gamma));
				return A * C - 1;
			}

			// - @param mach1 number the starting mach(mach_0 or mach_p) which determines the deltaP/ P1 being computed(for a pitot tube at sea level, subscript 0(IAS) or at altitude(TAS), subscript p)
			//-@param getTAS boolean? if true, switches to conversion mode from IAS to TAS
			// - @return number so if you provide only mach_P(TAS), this will return mach_0(IAS), and if you provide mach_0 and getTAS true(IAS), this will return mach_P(TAS)

			double getConvertedMach(double mach1, bool getTAS)
			{
				//veaf.loggers.get(veaf.Id):debug("getConvertedMach(mach1 = %s, getTAS = %s", veaf.p(mach1), veaf.p(getTAS))
				double DPP1 = 0;
				if (mach1 > 1)
				{
					DPP1 = lord_rayleighDPP(mach1); // At this point it's still deltaP / Pp (DPPP) (subscript p = at pitot tube, subscript 0 = at sea level)
				}
				else
				{
					DPP1 = isentropicDPP(mach1); // At this point it's still deltaP / Pp (DPPP) (subscript p = at pitot tube, subscript 0 = at sea level)
				}

				//veaf.loggers.get(veaf.Id):debug("DPP1 = %s -> initial", veaf.p(DPP1))

				if (getTAS)
				{
					DPP1 = P0 * DPP1 / dFinalPressurePa; // conversion from DPP0 to DPPP
				}
				else
				{
					DPP1 = dFinalPressurePa * DPP1 / P0; // conversion from DPPP to DPP0
				}

				//veaf.loggers.get(veaf.Id):debug("DPP1 = %s -> final", veaf.p(DPP1))


				double mach2 = 1;


				double converge_2_DPP(double machStep)
				{
					while (lord_rayleighDPP(mach2) < DPP1) //DPP2 = lord_rayleighDPP(mach2)
					{
						mach2 = mach2 + machStep;
					}

					return mach2;
				}


				if (DPP1 > lord_rayleighDPP(1))
				{
					mach2 = converge_2_DPP(0.25) - 0.25; // coarse
																							 //veaf.loggers.get(veaf.Id):debug("coarse mach2 = %s", veaf.p(mach2))

					mach2 = converge_2_DPP(0.0125) - 0.0125; // medium
																									 //veaf.loggers.get(veaf.Id):debug("medium mach2 = %s", veaf.p(mach2))

					mach2 = converge_2_DPP(0.00625); // fine
																					 //veaf.loggers.get(veaf.Id):debug("fine mach2 = %s", veaf.p(mach2))
				}
				else
				{
					mach2 = Math.Sqrt(2 * (Math.Pow((DPP1 + 1), (1 / B)) - 1) / (Gamma - 1));
					//veaf.loggers.get(veaf.Id):debug("subsonic mach2 = %s", veaf.p(mach2))
				}

				return mach2;
			}


			double ms_2_kt = 1.94384;
			double a1 = speedOfSound(dTemperatureKelvin);
			double a0 = speedOfSound(T0);

			//	veaf.loggers.get(veaf.Id):debug("a0 = %s, a1 = %s", veaf.p(a0), veaf.p(a1))



			if (dMach is not null)
			{
				// compute speeds from mach number
				result.Mach = dMach.Value;
				result.TAS_ms = result.Mach * a1;
				result.KTAS = result.TAS_ms * ms_2_kt;
				result.IAS_ms = getConvertedMach(result.Mach, false) * a0;
				result.KIAS = result.IAS_ms * ms_2_kt;
				}
			else if (dKias is not null)
			{
				// compute speeds from ias
				result.KIAS = dKias.Value;
				result.IAS_ms = result.KIAS / ms_2_kt;
				result.TAS_ms = getConvertedMach(result.IAS_ms / a0, true) * a1;
				result.KTAS = result.TAS_ms * ms_2_kt;
				result.Mach = result.TAS_ms / a1;
	}
			else if (dKtas is not null)
			{
				// compute speeds from tas
				result.KTAS = dKtas.Value;
				result.TAS_ms = result.KTAS / ms_2_kt;
				result.Mach = result.TAS_ms / a1;
				result.IAS_ms = getConvertedMach(result.Mach, false) * a0;
				result.KIAS = result.IAS_ms * ms_2_kt;
		}
			//veaf.loggers.get(veaf.Id):debug("veaf.convertSpeeds(mach=%s, kias=%s, ktas=%s, altitude=%s, temperature=%s, pressure=%s) -> final", veaf.p(result.Mach), veaf.p(result.KIAS), veaf.p(result.KTAS), veaf.p(altitude), veaf.p(temperature), veaf.p(pressure))


			return result;
}
	}
}
