﻿using System;
using ds_agent_oriented_simulation.Settings;

namespace ds_agent_oriented_simulation.Entities.Vehicles
{
    class CarA : Vehicle
    {
        public CarA(Random generator) :
            base("A", Constants.VolumeOfVehicleA, Constants.SpeedOfVehicleA, Constants.ProbabilityOfCrashOfVehicleA, Constants.TimeOfRepairOfVehicleA, generator)
        {

        }
    }
}
