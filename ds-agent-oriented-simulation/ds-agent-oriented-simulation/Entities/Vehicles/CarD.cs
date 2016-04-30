﻿using System;
using ds_agent_oriented_simulation.Settings;

namespace ds_agent_oriented_simulation.Entities.Vehicles
{
    class CarD : Vehicle
    {
        public CarD(Random generator) :
            base("D", Constants.VolumeOfVehicleD, Constants.SpeedOfVehicleD, Constants.ProbabilityOfCrashOfVehicleD, Constants.TimeOfRepairOfVehicleD, generator)
        {

        }
    }
}