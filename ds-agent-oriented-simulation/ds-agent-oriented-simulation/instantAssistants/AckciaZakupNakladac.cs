using OSPABA;
using simulation;
using agents;
namespace instantAssistants
{
	//meta! id="76"
	public class AckciaZakupNakladac : Action
	{
		public AckciaZakupNakladac(int id, Simulation mySim, CommonAgent myAgent) :
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
