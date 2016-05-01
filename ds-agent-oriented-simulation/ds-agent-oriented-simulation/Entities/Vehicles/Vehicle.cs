using System;
using OSPRNG;

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

        public Vehicle(string name, int pVolume, int pSpeed, double pProbability, int pTime, Random generator)
        {
            this.Name = name;
            this.Volume = pVolume;
            this.Speed = pSpeed;
            this._probabilityOfCrash = pProbability;
            this._timeOfRepair = pTime;
            this._failureGenerator = generator;
            this.RealVolume = 0;
        }

        public int GetVolume()
        {
            return Volume;
        }

        public int GetSpeed()
        {
            return Speed;
        }

        public double GetProbabilityOfCrash()
        {
            return _probabilityOfCrash;
        }

        public int GetTimeOfRepair()
        {
            return _timeOfRepair;
        }

        public void SetStartOfWaiting(double time)
        {
            this._startOfWaiting = time;
        }

        public void SetEndOfWaitingOnDepo(double time)
        {
            this._timeOfWaitingOnDepo += (time - _startOfWaiting);
            // kedze premenna sa pouzije este pri cakani pred uzlom B, treba vynulovat
            this._startOfWaiting = 0;
            _numberOfWaitingOnDepo++;
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

        public void SetEndOfWaitingOnBuilding(double time)
        {
            this._timeOfWaitingOnBuilding += (time - _startOfWaiting);
            this._startOfWaiting = 0;
            _numberOfWaitingOnBuilding++;
        }

        // vrati true ak sa auto pokazi
        public bool HasFailed()
        {
            double failed = _failureGenerator.NextDouble();

            return failed < _probabilityOfCrash;
        }

        public void ResetAttributes()
        {
            _timeOfWaitingOnDepo = 0;
            _timeOfWaitingOnBuilding = 0;
            _startOfWaiting = 0;
            _numberOfWaitingOnBuilding = 0;
            _numberOfWaitingOnDepo = 0;
        }

        public override string ToString()
        {
            return Name + ": [" + RealVolume + "/" + Volume + "], " + Speed + " ";
        }
    }
}

