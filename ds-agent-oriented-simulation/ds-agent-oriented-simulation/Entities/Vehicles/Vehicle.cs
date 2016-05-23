using System;
using ds_agent_oriented_simulation.Settings;

namespace ds_agent_oriented_simulation.Entities.Vehicles
{
    public class Vehicle
    {
        private readonly Random _failureGenerator;
        public string Name { get; set; }
        public int Volume { get; set; }
        public double RealVolume { get; set; }

        public double ToUnload { get; set; }
        public int Speed { get; set; }
        private readonly double _probabilityOfCrash;
        public int TimeOfRepair;

        public double CasCakaniaNaSkladke { get; set; }
        public double CasCakaniaNaStavbe { get; set; }
        public double ZaciatokCakania { get; set; }

        public double PocetAutVoFronteNaSkladke { get; set; }
        public double PocetAutVoFronteNaStavbe { get; set; }
        public bool JeNakladane { get; set; }
        public bool JeVykladane { get; set; }
        public double ZaciatokNakladania { get; set; }
        public double ZaciatokVykladania { get; set; }

        public Vehicle(string name, int pVolume, int pSpeed, double pProbability, int pTime, Random generator)
        {
            this.Name = name;
            this.Volume = pVolume;
            this.Speed = pSpeed;
            this._probabilityOfCrash = pProbability;
            this.TimeOfRepair = pTime;
            this._failureGenerator = generator;
            this.RealVolume = 0;
            this.ToUnload = 0;
            this.JeNakladane = false;
            this.JeVykladane = false;
        }
        public double GetMeanWaitingOnDepo()
        {
            return CasCakaniaNaSkladke / PocetAutVoFronteNaSkladke;
        }
        public double GetMeanWaitingOnBuilding()
        {
            return CasCakaniaNaStavbe / PocetAutVoFronteNaStavbe;
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

