using ds_agent_oriented_simulation.ContinualAssistant;
using ds_agent_oriented_simulation.Entities;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;
using OSPDataStruct;
using OSPStat;

namespace ds_agent_oriented_simulation.Agents
{
    //meta! id="18"
    public class AgentStavby : Agent
    {

        public Vehicle CarAtUnloaderB { get; set; }
        public Vehicle CarAtUnloaderA { get; set; }
        public SimQueue<Vehicle> AutaStavbaQueue { get; private set; }
        public SimQueue<MyMessage> MessageStavbaQueue { get; private set; }
        public Stat WaitingTimePerCar { get; internal set; }
        public WStat WaitingTimeInQueue { get; internal set; }
        public WStat LengthOfQueue { get; internal set; }
        public double MaterialNaStavbe { get; set; }
        public bool VykladacBIsDisabled { get; set; }
        public bool VykladacAIsOccupied { get; set; }
        public bool VykladacBIsOccupied { get; set; }

        public AgentStavby(int id, OSPABA.Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
            WaitingTimePerCar = new Stat();
            LengthOfQueue = new WStat(mySim);
            AutaStavbaQueue = new SimQueue<Vehicle>(LengthOfQueue);
            MessageStavbaQueue = new SimQueue<MyMessage>(LengthOfQueue);
            VykladacBIsDisabled = FormAgentSimulation.UnloaderBDisabled;
            VykladacAIsOccupied = false;
            VykladacAIsOccupied = false;
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
            //VykladacBIsWorking = false;
            //VykladacAIsWorking = false;

            // odobratie prvkov z radu
            if (!AutaStavbaQueue.IsEmpty())
            {
                AutaStavbaQueue.Dequeue();
            }

            MaterialNaStavbe = Constants.MaterialAtBuilding;
            WaitingTimePerCar.Clear();
            LengthOfQueue.Clear();
            AutaStavbaQueue.Clear();
            MessageStavbaQueue.Clear();
        }

        public bool VykladacAIsWorking()
        {
            return Timer.IsWorking(MySim.CurrentTime, Constants.VykladacAStartsAt, Constants.VykladacAEndsAt);
        }

        public bool VykladacBIsWorking()
        {
            return Timer.IsWorking(MySim.CurrentTime, Constants.VykladacBStartsAt, Constants.VykladacBEndsAt);
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            new ManagerStavby(SimId.ManagerStavby, MySim, this);
            new ProcesVykladacA(SimId.ProcesVykladacA, MySim, this);
            new ProcesVykladacB(SimId.ProcesVykladacB, MySim, this);
            AddOwnMessage(Mc.VylozAuto);
            AddOwnMessage(Mc.VylozenieUkoncene);
            AddOwnMessage(Mc.OdvozMaterialu);
        }
        //meta! tag="end"
    }
}
