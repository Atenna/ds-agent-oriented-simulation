using System;
using ds_agent_oriented_simulation.Settings;

namespace ds_agent_oriented_simulation.Entities.Vehicles
{
    class CarC : Vehicle
    {
        public CarC(Random generator, int ID) :
            base("C" + ID, Constants.VolumeOfVehicleC, Constants.SpeedOfVehicleC, Constants.ProbabilityOfCrashOfVehicleC, Constants.TimeOfRepairOfVehicleC, generator)
        {

        }
    }
}
