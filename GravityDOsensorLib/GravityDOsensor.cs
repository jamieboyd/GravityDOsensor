using System;
using System.Threading.Tasks;
using Meadow;
using Meadow.Foundation.Sensors.Base;
using Meadow.Hardware;
using Meadow.Peripherals.Sensors;
using Meadow.Units;


namespace GravityDO  // TODO:submit code to WildernessLabs and fit in their space
{
    /// <summary>
    /// Represents an Atlas Scientific Gravity Dissolved Oxygen Meter with
    /// analog voltage output
    /// </summary>
    public class GravityDOsensor : AnalogSamplingBase
    {
        // reference voltage from 100% saturated reading
        private Voltage referenceVoltage;
        private Voltage currentVoltage;

        /// <summary>
        /// Creates a new GravityDOsensor object from existing analog input port
        /// </summary>
        /// <param name="port">an IAnalogInputPort.</param>
        public GravityDOsensor(IAnalogInputPort port) : base(port)
        {
            this.referenceVoltage = new Voltage(-1);  // init to -1
        }

        /// <summary>
        /// Creates a new GravityDOsensor object, creating the analog input port
        /// from the given pin, with 10 samples, 10 ms sample interval, and
        /// 3.3V reference voltage
        /// </summary>
        /// <param name="pin">AnalogChannel connected to the sensor.</param>
        public GravityDOsensor(IPin pin) : base(pin.CreateAnalogInputPort(10,
            TimeSpan.FromMilliseconds(10), new Voltage(3.3)))
        { }


        /// <summary>
        /// Reads the sensor and saves the voltage as the referenceVoltage
        /// </summary>
        public async void SetReferenceVoltage()
        {
           referenceVoltage = await Read();
        }

        /// <summary>
        /// Reads the sensor and sets field for current voltage
        /// by referenceVoltage
        /// </summary>
        public async void SetCurrentVoltage()
        {
            currentVoltage=await Read();
        }

        /// <summary>
        /// Calculates and returns %saturation (current/reference * 100)
        /// </summary>
        /// <returns></returns>
        public double GetSaturation()
        {
            return (currentVoltage.Volts / referenceVoltage.Volts) * 100;
        }

    }
}



    /*
    public override TimeSpan UpdateInterval { get => base.UpdateInterval; protected set => base.UpdateInterval = value; }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override Task<Voltage> Read()
    {
        return base.Read();
    }

    public override void StartUpdating(TimeSpan? updateInterval)
    {
        base.StartUpdating(updateInterval);
    }

    public override void StopUpdating()
    {
        base.StopUpdating();
    }

    public override string ToString()
    {
        return base.ToString();
    }

    protected override void RaiseEventsAndNotify(IChangeResult<Voltage> changeResult)
    {
        base.RaiseEventsAndNotify(changeResult);
    }

    protected override Task<Voltage> ReadSensor()
    {
        return base.ReadSensor();
    }
    */