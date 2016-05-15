using System;
using System.Collections.Generic;
using ds_agent_oriented_simulation.Entities.Vehicles;
using MathNet.Numerics.Distributions;

namespace ds_agent_oriented_simulation.Entities
{
    public class Statistics
    {
        private List<Vehicle> _cars;
        private Queue _load;
        private Queue _unload;
        
        private int _iterator;
        private double _loadSize; // kumulativne sa tam bude ukladat priemerna dlzka radu 
        private double _unloadSize;
        private double _timeOfWaitingOnBuilding;
        private double _timeOfWaitingOnDepo;
        private double _simulationTimeCumulative;
        private double _simulationTimePower;

        private double _meanWaitingOnDepo;
        private double _meanWaitingOnBuilding;

        public Statistics()
        {
            _cars = new List<Vehicle>();
            _iterator = 0;
            _loadSize = 0;
            _unloadSize = 0;
            _timeOfWaitingOnBuilding = 0;
            _timeOfWaitingOnDepo = 0;
        }

        public void AddVehicleToStats(Vehicle car)
        {
            _cars.Add(car);
        }

        public void SetLoad(Queue load)
        {
            this._load = load;
        }

        public void SetUnload(Queue unload)
        {
            this._unload = unload;
        }
        /*
        public void UpdateStatistics(double simulationTime)
        {
            _simulationTimeCumulative += simulationTime;
            _simulationTimePower += Math.Pow(simulationTime, 2);

            _loadSize += _load.GetMeanQueueLength(simulationTime);
            _unloadSize += _unload.GetMeanQueueLength(simulationTime);

            foreach (Vehicle v in _cars)
            {
                _timeOfWaitingOnBuilding += v.GetWaitingOnBuilding();
                _timeOfWaitingOnDepo += v.GetWaitingOnDepo();

                _meanWaitingOnDepo += v.GetMeanWaitingOnDepo();
                _meanWaitingOnBuilding += v.GetMeanWaitingOnBuilding();
            }

            _iterator++;
        }
        */
        public double GetStatsMeanSimulationTime()
        {
            return _simulationTimeCumulative/_iterator;
        }

        public double GetStatsMeanLoadQueueLength()
        {
            return _loadSize/_iterator;
        }

        public double GetStatsMeanUnloadQueueLength()
        {
            return _unloadSize/_iterator;
        }

        // statistika pre vsetky auta - priemerna dlza cakania pred nakladacom
        public double GetStatsSumMeanLoadQueueTime()
        {
            // asi nebude fungovat hned
            return (_timeOfWaitingOnDepo/_iterator);
        }

        // statistika pre jedno auto - priemerna dlzka cakania pred nakladacom
        public double GetStatsMeanLoadQueueTime()
        {
            return (_meanWaitingOnDepo/_iterator)/_cars.Count;
        }

        // statistika pre vsetky auta - priemerna dlzka cakania pred vykladacom
        public double GetStatsSumMeanUnloadQueueTime()
        {
            return (_timeOfWaitingOnBuilding/_iterator);
        }

        public double GetStatsMeanUnloadQueueTime()
        {
            return (_meanWaitingOnBuilding/_iterator)/_cars.Count;
        }

        public double[] ConfidenceIntervalSimulationTime(double confidence)
        {
            confidence = 1 - ((1 - confidence)/2);
            double[] interval = new double[2];
            double avg = _simulationTimeCumulative/_iterator;
            double standardDeviation = Math.Sqrt((_simulationTimePower/_iterator) - Math.Pow(avg, 2));

            double value = 0;

            if (_iterator < 30)
            {
                value = StudentT.InvCDF(0.0, 1.0, _iterator, confidence);
            }
            else
            {
                value = Normal.InvCDF(0, 1, confidence);
            }

            interval[0] = avg - (value*standardDeviation/Math.Sqrt((_iterator - 1)));
            interval[1] = avg + (value*standardDeviation/Math.Sqrt((_iterator - 1)));

            return interval;
        }
    }
}
