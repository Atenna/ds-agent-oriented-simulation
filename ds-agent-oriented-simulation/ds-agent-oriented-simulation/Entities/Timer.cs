using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ds_agent_oriented_simulation.Simulation;

namespace ds_agent_oriented_simulation.Entities
{
    public static class Timer
    {
        public static double NewWorkDayStartsAt(double currentTime, double startsAt)
        {
            var days = Math.Floor((currentTime/60.0)/24);
            var hours = Math.Floor(ToHours(currentTime));
            var minutes = currentTime%60;

            // find closest 7am
            var next = ((days*24*60) + ToMinutes(startsAt));
            if (next >= currentTime)
            {
                return next;
            }
            next += LengthOfOneDay();
            return next;
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
            return ToMinutes(24);
        }

        public static double ToHours(double currentTime)
        {
            return ((currentTime/60)%24);
        }
    }
}
