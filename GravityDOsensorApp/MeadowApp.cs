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

        private GravityDOsensor DOsensor;

        double lastSaturation=100, thisSaturation, difference;

        public override Task Initialize()
        {
            Console.WriteLine("Initialize...");
            //DOsensor = new GravityDOsensor(pin: Device.Pins.A04);
            DOsensor = new GravityDOsensor(Device.Pins.A04.CreateAnalogInputPort(1));
            Console.WriteLine("Setting Reference Value...");
            DOsensor.SetReferenceVoltage();
            var consumer = GravityDOsensor.CreateObserver(
                    handler: result => Console.WriteLine($"Saturation = {thisSaturation}% : {difference}% change in saturation"),
                    filter: result =>
                    {
                        DOsensor.SetCurrentVoltage();
                        thisSaturation = DOsensor.GetSaturation();
                        difference = thisSaturation - lastSaturation;
                        if ((difference < -2) || (difference > 2))
                            {
                            lastSaturation = thisSaturation;
                            return true;
                        }else
                        {
                            return false;
                        }
                    });
            DOsensor.Subscribe(consumer);
            return Task.CompletedTask;
        }
        public override async Task Run()
        {
            var result = await DOsensor.Read();
            Console.WriteLine($"Initial read: {result.Millivolts:N2}mV");

            DOsensor.StartUpdating(TimeSpan.FromMilliseconds(1000));
        }

        //<!=SNOP=>
    }
}