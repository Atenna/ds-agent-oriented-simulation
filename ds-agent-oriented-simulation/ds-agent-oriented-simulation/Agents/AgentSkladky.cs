using ds_agent_oriented_simulation.ContinualAssistant;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;
using OSPDataStruct;

namespace ds_agent_oriented_simulation.Agents
{
	//meta! id="17"
	public class AgentSkladky : Agent
	{
	    public SimQueue<Vehicle> Queue { get; }
	    private double material = Settings.Constants.MaterialToLoad;
		public AgentSkladky(int id, OSPABA.Simulation mySim, Agent parent) :
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
			new ManagerSkladky(SimId.ManagerSkladky, MySim, this);
			new ProcesNakladacB(SimId.ProcesNakladacB, MySim, this);
			new ProcesNakladacA(SimId.ProcesNakladacA, MySim, this);
			AddOwnMessage(Mc.NalozAuto);
		}
		//meta! tag="end"
	}
}