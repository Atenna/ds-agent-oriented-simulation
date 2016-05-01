using ds_agent_oriented_simulation.Agents;
using OSPABA;

namespace ds_agent_oriented_simulation.InstantAssistant
{
	//meta! id="76"
	public class AckciaZakupNakladac : Action
	{
		public AckciaZakupNakladac(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void Execute(MessageForm message)
		{
		}
		public new AgentStavby MyAgent
		{
			get
			{
				return (AgentStavby)base.MyAgent;
			}
		}
	}
}
