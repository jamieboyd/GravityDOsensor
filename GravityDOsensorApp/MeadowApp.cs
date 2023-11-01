using Meadow;
using Meadow.Devices;
using GravityDO;
using System;
using System.Threading.Tasks;
using Meadow.Units;

namespace DOsensor_Sample
{
    // Change F7FeatherV2 to F7FeatherV1 for V1.x boards  
    // Use F7CoreComputeV2 for ProjectLab V3
    public class MeadowApp : App<F7CoreComputeV2>
    {
        //<!=SNIP=>

        private GravityDOsensor sensor;

        public override Task Initialize()
        {
            Console.WriteLine("Initialize...");

            sensor = new GravityDOsensor(pin: Device.Pins.A04, sampleCount: 100, sampleInterval: TimeSpan.FromMilliseconds(10), voltage: new Voltage(3.3));

            var consumer = GravityDOsensor.CreateObserver(
                handler: result => Console.WriteLine($"Observer filter satisfied: {result.New.Millivolts:N2}mV, old: {result.Old?.Millivolts:N2}mV"),
                filter: result =>
                {
                    if (result.Old is { } old)
                    { //c# 8 pattern match syntax. checks for !null and assigns var.
                        return (result.New - old).Abs().Millivolts > 500;
                    }
                    return false;
                });
            sensor.Subscribe(consumer);

            // classical .NET events can also be used:
            sensor.Updated += (sender, result) =>
            {
                Console.WriteLine($"Voltage Changed, new: {result.New.Millivolts:N2}mV, old: {result.Old?.Millivolts:N2}mV");
            };

            return Task.CompletedTask;
        }

        public override async Task Run()
        {
            var result = await sensor.Read();
            Console.WriteLine($"Initial read: {result.Millivolts:N2}mV");

            sensor.StartUpdating(TimeSpan.FromMilliseconds(1000));
        }

        //<!=SNOP=>
    }
}