using System;
using System.Threading.Tasks;
using Meadow;
using Meadow.Foundation.Sensors.Base;
using Meadow.Hardware;
using Meadow.Units;

namespace GravityDO  // TODO:submit code to WildernessLabs and fit in their space
/// <summary>
{
    /// Represents an Atlas Scientific Gravity Dissolved Oxygen Meter with
    /// analog voltage output
    /// </summary>
    public class GravityDOsensor : AnalogSamplingBase
    {
        /// <summary>
        /// Creates a new GravityDOsensor object
        /// </summary>
        /// <param name="port"></param>
        public GravityDOsensor(IAnalogInputPort port) : base(port)
        { }

        public GravityDOsensor(IPin pin,
            int sampleCount = 5,
            TimeSpan? sampleInterval = null,
            Voltage? voltage = null)
            : this (pin.CreateAnalogInputPort(
                      sampleCount,
                      sampleInterval ?? TimeSpan.FromMilliseconds(40),
                      voltage ?? new Voltage(3.3)))
        { }

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
    }
}