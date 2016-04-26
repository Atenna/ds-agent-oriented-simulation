using OSPABA;
using simulation;
using managers;
using continualAssistants;
using instantAssistants;
namespace agents
{
	//meta! id="20"
	public class AgentPrejazdu : Agent
	{
		public AgentPrejazdu(int id, Simulation mySim, Agent parent) :
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
			new ManagerPrejazdu(SimId.ManagerPrejazdu, MySim, this);
			AddOwnMessage(Mc.PresunCezPrejazd);
		}
		//meta! tag="end"
	}
}
