using OSPABA;
using simulation;
using managers;
using continualAssistants;
using instantAssistants;
namespace agents
{
	//meta! id="17"
	public class AgentSkladky : Agent
	{
		public AgentSkladky(int id, Simulation mySim, Agent parent) :
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
			AddOwnMessage(Mc.PresunNaSkladku);
		}
		//meta! tag="end"
	}
}
