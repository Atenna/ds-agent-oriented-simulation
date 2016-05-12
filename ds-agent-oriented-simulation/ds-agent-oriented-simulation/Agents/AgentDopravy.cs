using System;
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

        public Vehicle A { get; private set; }
        public Vehicle B { get; private set; }
        public Vehicle C { get; private set; }
        public Vehicle D { get; private set; }
        public Vehicle E { get; private set; }

        public Vehicle X { get; private set; }

        public AgentDopravy(int id, OSPABA.Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
        }

        public void PrepareCars(Random seedGenerator)
        {
            A = new CarA(seedGenerator);
            B = new CarB(seedGenerator);
            C = new CarC(seedGenerator);
            D = new CarD(seedGenerator);
            E = new CarE(seedGenerator);
            X = new CarA(seedGenerator);
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
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
        }
        //meta! tag="end"
    }
}
