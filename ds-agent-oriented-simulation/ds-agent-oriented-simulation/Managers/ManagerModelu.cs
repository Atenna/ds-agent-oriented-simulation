using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Managers
{
	//meta! id="13"
	public class ManagerModelu : Manager
	{
		public ManagerModelu(int id, OSPABA.Simulation mySim, Agent myAgent) :
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

		//meta! sender="AgentOkolia", id="35", type="Notice"
		public void ProcessDovozMaterialu(MessageForm message)
		{
		}

		//meta! sender="AgentDopravy", id="39", type="Response"
		public void ProcessOdvezMaterial(MessageForm message)
		{
            MyMessage sprava = new MyMessage(MySim, 1);
		    sprava.Code = Mc.OdvezMaterial;
            sprava.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            Request(message);
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
            case Mc.Inicializacia:
                message.Addressee = MySim.FindAgent(SimId.AgentOkolia);
                Call(message);
            break;

			case Mc.OdvezMaterial:
				ProcessOdvezMaterial(message);
			break;

			case Mc.DovozMaterialu:
				ProcessDovozMaterialu(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}
		//meta! tag="end"
		public new AgentModelu MyAgent
		{
			get
			{
				return (AgentModelu)base.MyAgent;
			}
		}
	}
}