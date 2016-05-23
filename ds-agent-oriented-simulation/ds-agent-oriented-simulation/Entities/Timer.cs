using System;

namespace ds_agent_oriented_simulation.Entities
{
    public static class Timer
    {
        public static double NewWorkDayStartsAt(double currentTime, double startsAt)
        {
            var days = Math.Floor((currentTime / 60.0) / 24);
            var hours = Math.Floor(ToHours(currentTime));
            var minutes = currentTime % 60;

            // find closest e.g. 7am
            var next = hours * 60 + minutes;
            if (next < startsAt * 60)
            {
                return startsAt * 60 - next;
            }
            if (next >= startsAt * 60)
            {
                return (startsAt * 60 + 24 * 60) - next;
            }
            return -1;
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

        public static bool IsWorking(double simTime, double startsAt, double finishAt)
        {
            double hours = (simTime/60)%24; // minuty => hodiny, % 24 hodinovy format
            if (hours >= startsAt && hours <= finishAt)
            {
                return true;
            }
            return false;
        }
    }
}
