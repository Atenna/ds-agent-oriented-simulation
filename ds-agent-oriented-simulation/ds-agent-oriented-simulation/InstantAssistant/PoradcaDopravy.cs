using ds_agent_oriented_simulation.Agents;
using OSPABA;

namespace ds_agent_oriented_simulation.InstantAssistant
{
	//meta! id="83"
	public class PoradcaDopravy : Adviser
	{
		public PoradcaDopravy(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void Execute(MessageForm message)
		{
		}
		public new AgentDopravy MyAgent
		{
			get
			{
				return (AgentDopravy)base.MyAgent;
			}
		}
	}
}
