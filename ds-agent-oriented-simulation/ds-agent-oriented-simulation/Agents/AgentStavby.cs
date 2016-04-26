using OSPABA;
using simulation;
using managers;
using continualAssistants;
using instantAssistants;
namespace agents
{
	//meta! id="18"
	public class AgentStavby : Agent
	{
		public AgentStavby(int id, Simulation mySim, Agent parent) :
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
