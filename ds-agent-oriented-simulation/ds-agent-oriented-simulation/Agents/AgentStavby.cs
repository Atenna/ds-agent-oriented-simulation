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
        public WStat RealWorkingTimeA { get; set; }
        public WStat RealWorkingTimeB { get; set; }
        public double RealWorkingA { get; set; }
        public double RealWorkingB { get; set; }
        public WStat UsageUnloaderA { get; set; }
        public WStat UsageUnloaderB { get; set; }
        public Stat OdoberMaterial { get; private set; }
        public double PocetUspesnyExport { get; set; } // per replikacia
        public double PocetExport { get; set; } // per replikacia - attempts
        public Stat OdoberMaterialKumulativny { get; private set; }
        public PlanovacPracovnejDoby2 PlanovacPracovnejDoby { get; set; }
        public double StartedWorkingA { get; set; }
        public double StartedWorkingB { get; set; }

        public AgentStavby(int id, OSPABA.Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
            WaitingTimePerCar = new Stat();
            LengthOfQueue = new WStat(mySim);
            UsageUnloaderA = new WStat(mySim);
            UsageUnloaderB = new WStat(mySim);
            AutaStavbaQueue = new SimQueue<Vehicle>(LengthOfQueue);
            MessageStavbaQueue = new SimQueue<MyMessage>(LengthOfQueue);
            VykladacBIsDisabled = !((MySimulation)MySim).buyUnloader;
            VykladacAIsOccupied = false;
            VykladacAIsOccupied = false;
            OdoberMaterial = new Stat();
            OdoberMaterialKumulativny = new Stat();
            PocetUspesnyExport = 0;
            PocetExport = 0;
            RealWorkingTimeA = new WStat(mySim);
            RealWorkingTimeB = new WStat(mySim);
            RealWorkingA = 0;
            RealWorkingB = 0;
            StartedWorkingA = 0;
            StartedWorkingB = 0;
        }

        public override void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
            VykladacBIsDisabled = !((MySimulation)MySim).buyUnloader;
            VykladacAIsOccupied = false;
            VykladacAIsOccupied = false;

            RealWorkingA = 0;
            RealWorkingB = 0;
            StartedWorkingA = 0;
            StartedWorkingB = 0;

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

            //VykladacBIsDisabled = FormAgentSimulation.UnloaderBDisabled;

            // vycisti statistiku odoberania materialu
            OdoberMaterial = new Stat();
            PocetUspesnyExport = 0;
            PocetExport = 0;

            CarAtUnloaderA = null;
            CarAtUnloaderB = null;
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
            new PlanovacPracovnejDoby2(SimId.PlanovacPracovnejDoby2, MySim, this);
            AddOwnMessage(Mc.VylozAuto);
            AddOwnMessage(Mc.VylozenieUkoncene);
            AddOwnMessage(Mc.OdvozMaterialu);
            AddOwnMessage(Mc.ZaciatokPracovnejDoby);
            AddOwnMessage(Mc.KoniecPracovnejDoby);
        }
        //meta! tag="end"

        
    }
}
