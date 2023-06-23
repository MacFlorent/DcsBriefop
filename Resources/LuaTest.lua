local veaf = {}
---comment
---@param mach number the mach number
---@param altitude any in feet, defaults to 10000
---@param temperature any in celsius, defaults to ISA temperature at altitude
function veaf.convertMachSpeed(mach, altitude, temperature)
    return veaf.convertSpeeds(mach, nil, nil, altitude, temperature)
end

---comment
---@param ktas number the true airspeed in knots
---@param altitude any in feet, defaults to 10000
---@param temperature any in celsius, defaults to ISA temperature at altitude
function veaf.convertTrueAirSpeed(ktas, altitude, temperature)
    return veaf.convertSpeeds(nil, nil, ktas, altitude, temperature)
end

---comment
---@param kias number the indicated airspeed in knots
---@param altitude any in feet, defaults to 10000
---@param temperature any in celsius, defaults to ISA temperature at altitude
function veaf.convertIndicatedAirSpeed(kias, altitude, temperature)
    return veaf.convertSpeeds(nil, kias, nil, altitude, temperature)
end

---Computes speeds based on a speed parameter (mach, tas, ias) and altitude/temperature
---@param mach number? the mach number
---@param kias number? the indicated airspeed in knots
---@param ktas number? the true airspeed in knots
---@param altitude any in meters, defaults to 10000
---@param temperature any in celsius, defaults to ISA temperature at altitude
---@param pressure any in pa, defaults to ISA temperature at altitude
---@return table result containing KTAS, KIAS, Mach, IAS_ms and TAS_ms
function veaf.convertSpeeds(mach, kias, ktas, altitude, temperature, pressure)
    --veaf.loggers.get(veaf.Id):debug("veaf.convertSpeeds(mach=%s, kias=%s, ktas=%s, altitude=%s, temperature=%s, pressure=%s) -> initial", veaf.p(mach), veaf.p(kias), veaf.p(ktas), veaf.p(altitude), veaf.p(temperature), veaf.p(pressure))

    local result = {
        KTAS = 0,
        KIAS = 0,
        Mach = 0,
    }

    local h_tropopause = 11000 --m, tropopause start altitude<

    local altitude = altitude
    if not altitude then
        altitude = 10000 -- default to 10000m
    end

    local T0 = 288.15 --K, ISA+0 altitude, may need to be corrected for mission ground temp
    local Tz = -0.0065 --K/m, ISA temperature gradient in troposphere
    local T_tropopause = 216.65 --K, temperature at the border between tropopause and troposphere (temperature in the tropopause)
    local P0 = 101325 --Pa, standard pressure
    local Gamma = 1.4 --Air heat capacity ratio
    local r = 287.03 --J/kg/K Perfect Gas constant for air
    local g = 9.81 --m/s^2 gravity constant on earth, might need to account for which planet ED is on

    local temperature = temperature
    if not temperature then
        -- compute ISA temperature based on altitude
        if altitude<h_tropopause then
            temperature = T0 + Tz*altitude --troposphere (temp in K)
        else
            temperature = T_tropopause --tropopause (max altitude 20000m) (temp in K)
        end
    else
        temperature = temperature + 273.15 --conversion to Kelvin
    end

    local function P_troposphere(temperature)
        return P0*(1+(temperature-T0)/T0)^(-g/(r*Tz))
    end

    local pressure = pressure
    if not pressure then
        -- compute pressure based on altitude and ISA temperature
        if altitude<h_tropopause then
            pressure = P_troposphere(temperature)
        else
            pressure = P_troposphere(T_tropopause) * math.exp(-g*(altitude-h_tropopause)/(r*T_tropopause))
        end
    end

    ---comment
    ---@param temperature number temperature in K
    ---@return number speed of sound in m/s
    local function speedOfSound(temperature)
        return math.sqrt(Gamma*r*temperature)
    end

    local B = Gamma/(Gamma-1)

    ---comment
    ---@param mach number mach number to calculate (Pt-Ps)/Ps with Pt/Ps given by isentropic relations (NOTE : (Pt-Ps)=deltaP)
    ---@return number returns the ratio deltaP/P (DPP) (what a pitot tube would measure for M<1)
    local function isentropicDPP(mach)
        return (1+(Gamma-1)*mach^2/2)^B-1;
    end

    ---comment
    ---@param mach number mach number to calculate (Pt-Ps)/Ps after a normal shock (M>1) (NOTE : (Pt-Ps)=deltaP)
    ---@return number returns the ratio deltaP/P (DPP) after the normal shock (what a pitot tube would measure for M>1)
    local function lord_rayleighDPP(mach)
        local A = ((Gamma+1)*mach^2/2)^B
        local C = ((Gamma+1)/(2*Gamma*mach^2-Gamma+1))^(B/Gamma);
        return A*C-1;
    end

    ---comment
    ---@param mach1 number the starting mach (mach_0 or mach_p) which determines the deltaP/P1 being computed (for a pitot tube at sea level, subscript 0 (IAS) or at altitude (TAS), subscript p)
    ---@param getTAS boolean? if true, switches to conversion mode from IAS to TAS
    ---@return number so if you provide only mach_P (TAS), this will return mach_0 (IAS), and if you provide mach_0 and getTAS true (IAS), this will return mach_P (TAS)
    local function getConvertedMach(mach1, getTAS)

        --veaf.loggers.get(veaf.Id):debug("getConvertedMach(mach1 = %s, getTAS = %s", veaf.p(mach1), veaf.p(getTAS))

        local DPP1 = 0;
        if mach1 > 1 then
            DPP1 = lord_rayleighDPP(mach1); --At this point it's still deltaP / Pp (DPPP) (subscript p = at pitot tube, subscript 0 = at sea level)
        else
            DPP1 = isentropicDPP(mach1); --At this point it's still deltaP / Pp (DPPP) (subscript p = at pitot tube, subscript 0 = at sea level)
        end

        --veaf.loggers.get(veaf.Id):debug("DPP1 = %s -> initial", veaf.p(DPP1))

        if getTAS then
            DPP1 = P0*DPP1/pressure --conversion from DPP0 to DPPP
        else
            DPP1 = pressure*DPP1/P0 --conversion from DPPP to DPP0
        end

        --veaf.loggers.get(veaf.Id):debug("DPP1 = %s -> final", veaf.p(DPP1))

        local mach2 = 1

        local function converge_2_DPP(machStep)
            while(lord_rayleighDPP(mach2) < DPP1) do --DPP2 = lord_rayleighDPP(mach2)
                mach2 = mach2+machStep
            end

            return mach2
        end

        if DPP1 > lord_rayleighDPP(1) then

            mach2 = converge_2_DPP(0.25) - 0.25 --coarse
            --veaf.loggers.get(veaf.Id):debug("coarse mach2 = %s", veaf.p(mach2))
            mach2 = converge_2_DPP(0.0125) - 0.0125 --medium
            --veaf.loggers.get(veaf.Id):debug("medium mach2 = %s", veaf.p(mach2))
            mach2 = converge_2_DPP(0.00625) --fine
            --veaf.loggers.get(veaf.Id):debug("fine mach2 = %s", veaf.p(mach2))

        else
            mach2 = math.sqrt(2*((DPP1+1)^(1/B)-1)/(Gamma-1))
            --veaf.loggers.get(veaf.Id):debug("subsonic mach2 = %s", veaf.p(mach2))
        end

        return mach2
    end

    local ms_2_kt = 1.94384
    local a1 = speedOfSound(temperature)
    local a0 = speedOfSound(T0)
    --veaf.loggers.get(veaf.Id):debug("a0 = %s, a1 = %s", veaf.p(a0), veaf.p(a1))


    if mach then
        -- compute speeds from mach number
        result.Mach = mach

        result.TAS_ms = mach * a1
        result.KTAS = result.TAS_ms * ms_2_kt

        result.IAS_ms = getConvertedMach(result.Mach)*a0
        result.KIAS = result.IAS_ms * ms_2_kt
    elseif kias then
        -- compute speeds from ias
        result.KIAS = kias
        result.IAS_ms = result.KIAS / ms_2_kt

        result.TAS_ms = getConvertedMach(result.IAS_ms/a0, true)*a1
        result.KTAS = result.TAS_ms * ms_2_kt

        result.Mach = result.TAS_ms / a1
    elseif ktas then
        -- compute speeds from tas
        result.KTAS = ktas
        result.TAS_ms = result.KTAS / ms_2_kt

        result.Mach = result.TAS_ms / a1

        result.IAS_ms = getConvertedMach(result.Mach)*a0
        result.KIAS = result.IAS_ms * ms_2_kt
    end

    --veaf.loggers.get(veaf.Id):debug("veaf.convertSpeeds(mach=%s, kias=%s, ktas=%s, altitude=%s, temperature=%s, pressure=%s) -> final", veaf.p(result.Mach), veaf.p(result.KIAS), veaf.p(result.KTAS), veaf.p(altitude), veaf.p(temperature), veaf.p(pressure))

    return result
end

local tasTest = 250
local altitudeTest = 10000
local computedSpeeds = veaf.convertSpeeds(nil, nil, tasTest, altitudeTest, nil, nil)

print("KTAS " .. computedSpeeds.KTAS)
print("TAS_ms " .. computedSpeeds.TAS_ms)
print("IAS_ms " .. computedSpeeds.IAS_ms)
print("KIAS " .. computedSpeeds.KIAS)
