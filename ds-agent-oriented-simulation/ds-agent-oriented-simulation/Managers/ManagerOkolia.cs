using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;
using OSPRNG;

namespace ds_agent_oriented_simulation.Managers
{
	//meta! id="15"
	public class ManagerOkolia : Manager
	{
        // priklad
        EmpiricRNG<int> _empiricRng = new EmpiricRNG<int>(
            new EmpiricPair<int>(new UniformDiscreteRNG(100, 200), 0.2));
        
		public ManagerOkolia(int id, OSPABA.Simulation mySim, Agent myAgent) :
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

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! sender="PlanovacDovozMaterialu", id="55", type="Finish"
		public void ProcessFinish(MessageForm message)
		{
		}

		//meta! sender="AgentModelu", id="86", type="Call"
	    public void ProcessInicializacia(MessageForm message)
	    {
            // pre S3 StartContinualAssistant(message);


        } 

	    //meta! userInfo="Generated code: do not modify", tag="begin"
		public void Init()
		{
		}

		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Finish:
				ProcessFinish(message);
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
		public new AgentOkolia MyAgent
		{
			get
			{
				return (AgentOkolia)base.MyAgent;
			}
		}
	}
}