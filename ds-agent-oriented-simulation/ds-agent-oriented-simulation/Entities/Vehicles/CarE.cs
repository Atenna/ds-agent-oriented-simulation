using System;
using ds_agent_oriented_simulation.Settings;

namespace ds_agent_oriented_simulation.Entities.Vehicles
{
    class CarE : Vehicle
    {
        public CarE(Random generator) :
            base("E", Constants.VolumeOfVehicleE, Constants.SpeedOfVehicleE, Constants.ProbabilityOfCrashOfVehicleE, Constants.TimeOfRepairOfVehicleE, generator)
        {

        }
    }
}
