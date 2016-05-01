using OSPABA;

namespace ds_agent_oriented_simulation.Agents
{
	//meta! id="57"
	public class AgentVozidiel : Agent
	{
		public AgentVozidiel(int id, OSPABA.Simulation mySim, Agent parent) :
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
			new ManagerVozidiel(SimId.ManagerVozidiel, MySim, this);
		}
		//meta! tag="end"
	}
}