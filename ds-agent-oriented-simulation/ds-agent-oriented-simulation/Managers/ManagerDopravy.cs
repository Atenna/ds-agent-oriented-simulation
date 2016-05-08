using System;
using System.Windows.Forms;
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
	    private MyMessage requestCopyMessage;
	    
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
        //meta! sender="AgentSkladky", id="36", type="Response"
        public void ProcessNalozAuto(MessageForm message)
        {
            // auto skoncilo nakladanie 
            Console.WriteLine("Auto nalozene");
            requestCopyMessage = (MyMessage)message.CreateCopy();
            requestCopyMessage.Car = ((MyMessage) message).Car;
            requestCopyMessage.Code = Mc.VylozAuto;
            requestCopyMessage.Addressee = MySim.FindAgent(SimId.AgentStavby);

            message.Addressee = ((AgentDopravy)MyAgent).ProcesCestaNaStavbu;
            StartContinualAssistant(message);
        }

        //meta! sender="ProcesCestaNaStavbu", id="neviem", type="Finish"
        public void ProcessFinishProcesCestaNaStavbu(MessageForm message)
        {
            Console.WriteLine("Auto na stavbe");
            // agent stavby vyloz auto
            Request(requestCopyMessage);
        }

        //meta! sender="AgentStavby", id="37", type="Response"
        public void ProcessVylozAuto(MessageForm message)
		{
            Console.WriteLine("Auto vylozene");
            // vytvori sa kopia spravy, ktoru bude poslana potom s autom na skladku
            requestCopyMessage = (MyMessage) message;
            requestCopyMessage.Code = Mc.NalozAuto;
            requestCopyMessage.Addressee = MySim.FindAgent(SimId.AgentStavby);
            // vykladanie skoncilo, auto pojde na prejazd
            message.Addressee = ((AgentDopravy)MyAgent).ProcesCestaNaPrejazd;
            StartContinualAssistant(message);
		}

        //meta! sender="ProcesCestaNaPrejazd", id="neviem", type="Finish"
        public void ProcessFinishProcesCestaNaPrejazd(MessageForm message)
        {
            Console.WriteLine("Auto na prejazde");
            message.Addressee = ((AgentDopravy)MyAgent).ProcesCestaNaSkladku;
            message.Code = Mc.NalozAuto;
            StartContinualAssistant(message);
        }

        //meta! sender="CestaNaSkladku", id="neviem", type="Finish"
        public void ProcessFinishProcesCestaNaSkladku(MessageForm message)
        {
            Console.WriteLine("Auto na skladke");
            requestCopyMessage.Code = Mc.NalozAuto;
            requestCopyMessage.Addressee = MySim.FindAgent(SimId.AgentSkladky);
            Request(requestCopyMessage);
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

            case Mc.Finish:
                switch (message.Sender.Id)
                {
                    case SimId.ProcesCestaNaSkladku:
                        ProcessFinishProcesCestaNaSkladku(message);
                    break;

                    case SimId.ProcesCestaNaStavbu:
                        ProcessFinishProcesCestaNaStavbu(message);
                    break;

                    case SimId.ProcesCestaNaPrejazd:
                        ProcessFinishProcesCestaNaPrejazd(message);
                    break;
                }
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
                Request(sprava);
            }          
        }

        private void InicializujAutaPodlaVariantu(int variant)
        {
            if (_enabledCars == null)
            {
                if (variant == 1)
                {
                    _enabledCars = new Vehicle[1];
                    _enabledCars[0] = MyAgent.A;
                    
                    //_enabledCars[1] = MyAgent.B;
                    //_enabledCars[2] = MyAgent.C;
                }
                else if (variant == 2)
                {
                    _enabledCars = new Vehicle[4];
                    _enabledCars[0] = MyAgent.A;
                    _enabledCars[1] = MyAgent.B;
                    _enabledCars[2] = MyAgent.C;
                    _enabledCars[3] = MyAgent.D;
                }
                // etc, to-do
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