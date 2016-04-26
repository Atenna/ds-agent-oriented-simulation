using OSPABA;
using simulation;
using agents;
using continualAssistants;
using instantAssistants;
namespace managers
{
	//meta! id="19"
	public class ManagerDopravy : Manager
	{
		public ManagerDopravy(int id, Simulation mySim, Agent myAgent) :
			base(id, mySim, myAgent)
		{
			Init();
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication

			if (PetriNet != null)
			{
				PetriNet.Clear();
			}
		}

		//meta! sender="AgentPrejazdu", id="44", type="Notice"
		public void ProcessPokazenieAuta(MessageForm message)
		{
		}

		//meta! sender="AgentPrejazdu", id="40", type="Response"
		public void ProcessPresunCezPrejazd(MessageForm message)
		{
		}

		//meta! sender="AgentStavby", id="37", type="Response"
		public void ProcessPresunNaStavbu(MessageForm message)
		{
		}

		//meta! sender="AgentSkladky", id="36", type="Response"
		public void ProcessPresunNaSkladku(MessageForm message)
		{
		}

		//meta! sender="AgentModelu", id="39", type="Request"
		public void ProcessOdvezMaterial(MessageForm message)
		{
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.PresunNaSkladku:
				ProcessPresunNaSkladku(message);
			break;

			case Mc.PresunNaStavbu:
				ProcessPresunNaStavbu(message);
			break;

			case Mc.OdvezMaterial:
				ProcessOdvezMaterial(message);
			break;

			case Mc.PokazenieAuta:
				ProcessPokazenieAuta(message);
			break;

			case Mc.PresunCezPrejazd:
				ProcessPresunCezPrejazd(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentDopravy MyAgent
		{
			get
			{
				return (AgentDopravy)base.MyAgent;
			}
		}
	}
}
