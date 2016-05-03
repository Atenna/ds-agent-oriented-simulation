using System;
using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.InstantAssistant;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Managers
{
	//meta! id="19"
	public class ManagerDopravy : Manager
	{
	    private Vehicle[] _enabledCars;
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


		//meta! sender="AgentStavby", id="37", type="Response"
		public void ProcessVylozAuto(MessageForm message)
		{
		}

		//meta! sender="AgentSkladky", id="36", type="Response"
		public void ProcessNalozAuto(MessageForm message)
		{
		    // pride odpoved
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

			case Mc.Inicializacia:
				ProcessInicializacia(message);
			break;

			default:
				ProcessDefault(message);
			break;
			}
		}

        private void ProcessInicializacia(MessageForm message)
        {
            MyMessage sprava = (MyMessage)message;
            InicializujAutaPodlaVariantu(sprava.Variant);

            foreach( Vehicle i in _enabledCars)
            {
                sprava = new MyMessage(MySim, i);
                sprava.Addressee = MySim.FindAgent(SimId.AgentSkladky);
                sprava.Code = Mc.NalozAuto;
                Request(message);
            }          
        }

        private void InicializujAutaPodlaVariantu(int variant)
        {
            if (variant == 1)
            {
                _enabledCars = new Vehicle[3];
                _enabledCars[0] = MyAgent.A;
                _enabledCars[1] = MyAgent.B;
                _enabledCars[2] = MyAgent.C;
            } else if (variant == 2)
            {
                _enabledCars = new Vehicle[4];
                _enabledCars[0] = MyAgent.A;
                _enabledCars[1] = MyAgent.B;
                _enabledCars[2] = MyAgent.C;
                _enabledCars[3] = MyAgent.D
            }
            // etc, to-do
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