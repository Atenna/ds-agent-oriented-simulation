using OSPABA;
using simulation;
using managers;
using continualAssistants;
using instantAssistants;
namespace agents
{
	//meta! id="19"
	public class AgentDopravy : Agent
	{
		public AgentDopravy(int id, Simulation mySim, Agent parent) :
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
			AddOwnMessage(Mc.PokazenieAuta);
			AddOwnMessage(Mc.PresunCezPrejazd);
			AddOwnMessage(Mc.PresunNaStavbu);
			AddOwnMessage(Mc.PresunNaSkladku);
			AddOwnMessage(Mc.OdvezMaterial);
		}
		//meta! tag="end"
	}
}
