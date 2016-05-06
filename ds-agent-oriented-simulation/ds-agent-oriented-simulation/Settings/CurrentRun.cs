using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ds_agent_oriented_simulation.Entities.Vehicles;

namespace ds_agent_oriented_simulation.Settings
{
    public static class CurrentRun
    {
        public static bool CarA { get; set; }
        public static bool CarB { get; set; }
        public static bool CarC { get; set; }
        public static bool CarD { get; set; }
        public static bool CarE { get; set; }
        public static int CarsA { get; set; }
        public static int CarsE { get; set; }

        public static void initializeCurrentRun()
        {
            CarA = false;
            CarB = false;
            CarC = false;
            CarD = false;
            CarE = false;
            CarsA = 0;
            CarsE = 0;
        }
    }
}
