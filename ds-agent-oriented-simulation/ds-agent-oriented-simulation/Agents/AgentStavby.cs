using ds_agent_oriented_simulation.ContinualAssistant;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.InstantAssistant;
using ds_agent_oriented_simulation.Managers;
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
        public WStat StavbaWStat { get; internal set; }
        public bool VykladacAIsWorking { get; internal set; }
        public bool VykladacBIsWorking { get; internal set; }

        public ProcesVykladacA ProcesVykladacA { get; private set; }
        public ProcesVykladacB ProcesVykladacB { get; private set; }

        public AgentStavby(int id, OSPABA.Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
            StavbaWStat = new WStat(MySim);
            AutaStavbaQueue = new SimQueue<Vehicle>(StavbaWStat);
        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		    VykladacBIsWorking = false;
		    VykladacAIsWorking = false;

            // odobratie prvkov z radu
            if (!AutaStavbaQueue.IsEmpty())
            {
                AutaStavbaQueue.Dequeue();
            }
            
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerStavby(SimId.ManagerStavby, MySim, this);
			ProcesVykladacB = new ProcesVykladacB(SimId.ProcesVykladacB, MySim, this);
			new PlanovacOdoberMaterial(SimId.PlanovacOdoberMaterial, MySim, this);
			ProcesVykladacA = new ProcesVykladacA(SimId.ProcesVykladacA, MySim, this);
			//new AckciaZakupNakladac(SimId.AckciaZakupNakladac, MySim, this);
			AddOwnMessage(Mc.VylozAuto);
            AddOwnMessage(Mc.VylozenieUkoncene);
        }
		//meta! tag="end"
	}
}