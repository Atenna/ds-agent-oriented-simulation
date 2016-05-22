using System.Collections.Generic;
using ds_agent_oriented_simulation.ContinualAssistant;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Agents
{
    //meta! id="19"
    public class AgentDopravy : Agent
    {

        public List<Vehicle> EnabledCars;
        private readonly MySimulation _mine;
        public AgentDopravy(int id, OSPABA.Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
            _mine = (MySimulation) mySim;
        }

        public void PrepareCars(int count, int type)
        {
            for (int i = 0; i < count; i++)
            {
                if (type == 0)
                {
                    EnabledCars.Add(new CarA(_mine.SeedGenerator));
                }
                if (type == 1) 
                {
                    EnabledCars.Add(new CarB(_mine.SeedGenerator));
                }
                if (type == 2)
                {
                    EnabledCars.Add(new CarC(_mine.SeedGenerator));
                }
                if (type == 3)
                {
                    EnabledCars.Add(new CarD(_mine.SeedGenerator));
                }
                if (type == 4)
                {
                    EnabledCars.Add(new CarE(_mine.SeedGenerator));
                }
            }
        }

        public override void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
            EnabledCars = new List<Vehicle>();
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            new ManagerDopravy(SimId.ManagerDopravy, MySim, this);
            new ProcessPresunNaStavbu(SimId.ProcessPresunNaStavbu, MySim, this);
            new ProcessPresunNaPrejazd(SimId.ProcessPresunNaPrejazd, MySim, this);
            new ProcessPresunNaSkladku(SimId.ProcessPresunNaSkladku, MySim, this);
            AddOwnMessage(Mc.Inicializacia);
            AddOwnMessage(Mc.NalozAuto);
            AddOwnMessage(Mc.VylozAuto);
            AddOwnMessage(Mc.PrejazdUkonceny);
            AddOwnMessage(Mc.DovozMaterialu);
            AddOwnMessage(Mc.OdvozMaterialu);
            AddOwnMessage(Mc.KoniecPracovnejDoby);
        }
        //meta! tag="end"
    }
}
