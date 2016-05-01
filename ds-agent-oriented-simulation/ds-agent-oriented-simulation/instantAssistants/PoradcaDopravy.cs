using OSPABA;
using simulation;
using agents;
namespace instantAssistants
{
	//meta! id="83"
	public class PoradcaDopravy : Adviser
	{
		public PoradcaDopravy(int id, Simulation mySim, CommonAgent myAgent) :
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
