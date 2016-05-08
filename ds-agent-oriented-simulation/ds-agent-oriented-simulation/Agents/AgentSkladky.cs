using ds_agent_oriented_simulation.ContinualAssistant;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;
using OSPDataStruct;
using OSPStat;

namespace ds_agent_oriented_simulation.Agents
{
    //meta! id="17"
    public class AgentSkladky : Agent
    {
        public SimQueue<Vehicle> AutaSkladkaQueue { get; private set; }
        public WStat SkladkaWStat { get; internal set; }
        public bool NakladacAIsWorking { get; internal set; }
        public bool NakladacBIsWorking { get; internal set; }
        public ProcesNakladacB procesNakladacB { get; private set; }
        public ProcesNakladacA procesNakladacA { get; private set; }

        public Vehicle CarAtLoaderA { get; set; }
        public Vehicle CarAtLoaderB { get; set; }

        private double material = Settings.Constants.MaterialToLoad;
        public AgentSkladky(int id, OSPABA.Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
            SkladkaWStat = new WStat(mySim);
            AutaSkladkaQueue = new SimQueue<Vehicle>(SkladkaWStat);
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
            NakladacAIsWorking = false;
            NakladacBIsWorking = false;
            // odoberie vozidla z radu
            if (!AutaSkladkaQueue.IsEmpty())
            {
                AutaSkladkaQueue.Dequeue();
            }
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            new ManagerSkladky(SimId.ManagerSkladky, MySim, this);
            procesNakladacB = new ProcesNakladacB(SimId.ProcesNakladacB, MySim, this);
            procesNakladacA = new ProcesNakladacA(SimId.ProcesNakladacA, MySim, this);
            AddOwnMessage(Mc.NalozAuto);
            AddOwnMessage(Mc.NalozenieUkoncene);
        }
        //meta! tag="end"
    }
}