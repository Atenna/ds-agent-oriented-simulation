using ds_agent_oriented_simulation.InstantAssistant;
using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Agents
{
	//meta! id="19"
	public class AgentDopravy : Agent
	{
		public AgentDopravy(int id, OSPABA.Simulation mySim, Agent parent) :
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
			new ManagerDopravy(SimId.ManagerDopravy, MySim, this);
			new PoradcaDopravy(SimId.PoradcaDopravy, MySim, this);
			AddOwnMessage(Mc.PresunNaStavbu);
			AddOwnMessage(Mc.PresunNaSkladku);
			AddOwnMessage(Mc.OdvezMaterial);
		}
		//meta! tag="end"
	}
}