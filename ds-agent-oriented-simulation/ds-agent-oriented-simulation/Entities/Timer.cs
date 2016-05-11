using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ds_agent_oriented_simulation.Simulation;

namespace ds_agent_oriented_simulation.Entities
{
    public static class Timer
    {
        public static double NewWorkDayStartsAt(double startsAt)
        {
            return startsAt + LengthOfOneDay();
        }

        public static double NewWorkDayEndsAt(double endsAt)
        {
            return endsAt + LengthOfOneDay();
        }

        public static double ToMinutes(double hours)
        {
            return hours*60;
        }

        public static double LengthOfOneDay()
        {
            return 24*60;
        }
    }
}
