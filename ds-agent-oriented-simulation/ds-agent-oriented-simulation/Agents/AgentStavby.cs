using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Agents
{
	//meta! id="18"
	public class AgentStavby : Agent
	{
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
			AddOwnMessage(Mc.PresunNaStavbu);
		}
		//meta! tag="end"
	}
}
