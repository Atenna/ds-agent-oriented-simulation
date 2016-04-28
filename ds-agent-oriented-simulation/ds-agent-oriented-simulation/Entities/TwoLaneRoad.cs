namespace ds_agent_oriented_simulation.Entities
{
    public class TwoLaneRoad
    {
        private double _timeOfArrival;
        public TwoLaneRoad()
        {
            _timeOfArrival = 0;
        }
        // najdlhsi cas prichodu aktualnych aut na ceste
        public double RealTime(double expectedTime)
        {
            if (expectedTime < _timeOfArrival)
            {
                return _timeOfArrival;
            }
            _timeOfArrival = expectedTime;
            return expectedTime;
        }
    }
}
