using System;
using ds_agent_oriented_simulation.Settings;

namespace ds_agent_oriented_simulation.Entities.Vehicles
{
    class CarB : Vehicle
    {
        public CarB(Random generator) :
            base("B", Constants.VolumeOfVehicleB, Constants.SpeedOfVehicleB, Constants.ProbabilityOfCrashOfVehicleB, Constants.TimeOfRepairOfVehicleB, generator)
        {

        }
    }
}
