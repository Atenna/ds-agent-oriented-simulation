using System;
using ds_agent_oriented_simulation.Simulation;

namespace ds_agent_oriented_simulation.Entities.Vehicles
{
    public class Vehicle
    {
        private Random _failureGenerator;
        public string Name { get; set; }
        public int Volume { get; set; }
        public double RealVolume { get; set; }
        public int Speed { get; set; }
        private readonly double _probabilityOfCrash;
        private readonly int _timeOfRepair;

        private double _timeOfWaitingOnDepo = 0;
        private double _timeOfWaitingOnBuilding = 0;
        private double _startOfWaiting = 0;

        private double _numberOfWaitingOnDepo = 0;
        private double _numberOfWaitingOnBuilding = 0;
        public bool jeNakladane { get; set; }
        public bool jeVykladane { get; set; }

        public Vehicle(string name, int pVolume, int pSpeed, double pProbability, int pTime, Random generator)
        {
            this.Name = name;
            this.Volume = pVolume;
            this.Speed = pSpeed;
            this._probabilityOfCrash = pProbability;
            this._timeOfRepair = pTime;
            this._failureGenerator = generator;
            this.RealVolume = 0;
            this.jeNakladane = false;
            this.jeVykladane = false;
        }

        public int GetTimeOfRepair()
        {
            return _timeOfRepair;
        }

        public double GetWaitingOnDepo()
        {
            return _timeOfWaitingOnDepo;
        }
        public double GetWaitingOnBuilding()
        {
            return _timeOfWaitingOnBuilding;
        }
        public double GetMeanWaitingOnDepo()
        {
            return _timeOfWaitingOnDepo / _numberOfWaitingOnDepo;
        }
        public double GetMeanWaitingOnBuilding()
        {
            return _timeOfWaitingOnBuilding / _numberOfWaitingOnBuilding;
        }

        public bool HasFailed()
        {
            double failed = _failureGenerator.NextDouble();

            return failed < _probabilityOfCrash;
        }

        public override string ToString()
        {
            return Name + ": [" + RealVolume + "/" + Volume + "], " + Speed + " ";
        }
    }
}

