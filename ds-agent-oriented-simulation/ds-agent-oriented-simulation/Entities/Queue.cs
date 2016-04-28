using System.Collections.Generic;
using System.Linq;
using ds_agent_oriented_simulation.Entities.Vehicles;

namespace ds_agent_oriented_simulation.Entities
{
    public class Queue
    {
        private List<Vehicle> _queue;
        private double _sumOfTime; // in queue * number of cars
        private double _lastChange;

        public Queue()
        {
            _queue = new List<Vehicle>();
            _sumOfTime = 0.0;
            _lastChange = 0.0;
        }

        public void AddVehicleToEnd(Vehicle car, double simulationTime)
        {
            UpdateQueue(simulationTime);
            _queue.Add(car);
        }

        public Vehicle GetVehicleFromQueue(double simulationTime)
        {
            UpdateQueue(simulationTime);

            if (_queue.Any())
            {
                Vehicle toReturn = _queue.First();

                _queue.RemoveAt(0);
                return toReturn;
            }
            else return null;

        }

        public void UpdateQueue(double simulationTime)
        {
            _sumOfTime += (simulationTime - _lastChange) * _queue.Count();
            _lastChange = simulationTime;
        }

        public int GetSize()
        {
            return _queue.Count();
        }

        public double GetMeanQueueLength(double simulationTime)
        {
            UpdateQueue(simulationTime);
            return (_sumOfTime / simulationTime);
        }

        public void Reset()
        {
            _queue = new List<Vehicle>();
            _sumOfTime = 0.0;
            _lastChange = 0.0;
        }

        public Vehicle GetAt(int i)
        {
            return _queue.ElementAt(i);
        }

        public List<Vehicle> GetList()
        {
            return _queue;
        }
    }
}
