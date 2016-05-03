using ds_agent_oriented_simulation.ContinualAssistant;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.InstantAssistant;
using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;
using OSPDataStruct;

namespace ds_agent_oriented_simulation.Agents
{
	//meta! id="18"
	public class AgentStavby : Agent
	{
        public SimQueue<Vehicle> AutaStavbaQueue { get; private set; }
        public bool VykladacAIsWorking { get; internal set; }
        public bool VykladacBIsWorking { get; internal set; }

        public AgentStavby(int id, OSPABA.Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerStavby(SimId.ManagerStavby, MySim, this);
			new ProcesVykladacB(SimId.ProcesVykladacB, MySim, this);
			new PlanovacOdoberMaterial(SimId.PlanovacOdoberMaterial, MySim, this);
			new ProcesVykladacA(SimId.ProcesVykladacA, MySim, this);
			new AckciaZakupNakladac(SimId.AckciaZakupNakladac, MySim, this);
			AddOwnMessage(Mc.VylozAuto);
		}
		//meta! tag="end"
	}
}