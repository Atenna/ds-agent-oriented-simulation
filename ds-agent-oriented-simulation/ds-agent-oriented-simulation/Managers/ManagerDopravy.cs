using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Managers
{
	//meta! id="19"
	public class ManagerDopravy : Manager
	{
	    private Vehicle[] enabledCars;
		public ManagerDopravy(int id, OSPABA.Simulation mySim, Agent myAgent) :
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

		//meta! userInfo="Removed from model"
		public void ProcessPokazenieAuta(MessageForm message)
		{
		}


		//meta! sender="AgentStavby", id="37", type="Response"
		public void ProcessVylozAuto(MessageForm message)
		{
		}

		//meta! sender="AgentSkladky", id="36", type="Response"
		public void ProcessNalozAuto(MessageForm message)
		{
		}

		//meta! sender="AgentModelu", id="39", type="Request"
		public void ProcessOdvezMaterial(MessageForm message)
		{
		    MyMessage sprava = (MyMessage) message;
            // case podla variantu
            enabledCars = new Vehicle[3];
		    enabledCars[0] = MyAgent.A;
		    enabledCars[1] = MyAgent.B;
		    enabledCars[2] = MyAgent.C;

            MyMessage nalozAuto = new MyMessage(MySim, enabledCars[0]);
		    nalozAuto.Code = Mc.NalozAuto;
		    nalozAuto.Addressee = MySim.FindAgent(SimId.AgentSkladky);

            Request(nalozAuto);

            nalozAuto = new MyMessage(MySim, enabledCars[1]);
            Request(nalozAuto);

            nalozAuto = new MyMessage(MySim, enabledCars[2]);
            Request(nalozAuto);

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
			case Mc.VylozAuto:
				ProcessVylozAuto(message);
			break;

			case Mc.NalozAuto:
				ProcessNalozAuto(message);
			break;

			case Mc.OdvezMaterial:
				ProcessOdvezMaterial(message);
			break;

			case Mc.Inicializacia:
				ProcessInicializacia(message);
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