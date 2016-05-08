using System;
using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
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

        //meta! sender="ProcessPresunNaPrejazd", id="110", type="Finish"
        public void ProcessFinishProcessPresunNaPrejazd(MessageForm message)
        {
            Console.WriteLine("Auto na prejazde");
            message.Addressee = MyAgent.FindAssistant(SimId.ProcessPresunNaSkladku);
            message.Code = Mc.NalozAuto;
            StartContinualAssistant(message);
        }

        //meta! sender="ProcessPresunNaStavbu", id="108", type="Finish"
        public void ProcessFinishProcessPresunNaStavbu(MessageForm message)
        {
            Console.WriteLine("Auto na stavbe");
            // agent stavby vyloz auto
            message.Addressee = MySim.FindAgent(SimId.AgentStavby);
            message.Code = Mc.VylozAuto;
            Request(message);
        }

        //meta! sender="ProcessPresunNaSkladku", id="106", type="Finish"
        public void ProcessFinishProcessPresunNaSkladku(MessageForm message)
        {
            Console.WriteLine("Auto na skladke");
            message.Code = Mc.NalozAuto;
            message.Addressee = MySim.FindAgent(SimId.AgentSkladky);
            Request(message);
        }

        //meta! sender="AgentModelu", id="91", type="Call"
        public void ProcessInicializacia(MessageForm message)
        {
            MyMessage sprava = (MyMessage)message;
            InicializujAutaPodlaVariantu(sprava.Variant);

            foreach (Vehicle i in _enabledCars)
            {
                sprava = new MyMessage(MySim, i);
                sprava.Addressee = MySim.FindAgent(SimId.AgentSkladky);
                sprava.Code = Mc.NalozAuto;
                Request(sprava);
            }
        }

        //meta! sender="AgentSkladky", id="36", type="Response"
        public void ProcessNalozAuto(MessageForm message)
        {
            // auto skoncilo nakladanie 
            Console.WriteLine("Auto nalozene");
            requestCopyMessage = (MyMessage)message.CreateCopy();
            requestCopyMessage.Car = ((MyMessage)message).Car;
            requestCopyMessage.Code = Mc.VylozAuto;
            requestCopyMessage.Addressee = MySim.FindAgent(SimId.AgentStavby);

            message.Addressee = MyAgent.FindAssistant(SimId.ProcessPresunNaStavbu);
            StartContinualAssistant(message);
        }

        //meta! sender="AgentStavby", id="37", type="Response"
        public void ProcessVylozAuto(MessageForm message)
        {
            Console.WriteLine("Auto vylozene");
            // vytvori sa kopia spravy, ktoru bude poslana potom s autom na skladku
            requestCopyMessage = (MyMessage)message;
            requestCopyMessage.Code = Mc.NalozAuto;
            requestCopyMessage.Addressee = MySim.FindAgent(SimId.AgentStavby);
            // vykladanie skoncilo, auto pojde na prejazd
            message.Addressee = MyAgent.FindAssistant(SimId.ProcessPresunNaPrejazd);
            StartContinualAssistant(message);
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
                case Mc.Finish:
                    switch (message.Sender.Id)
                    {
                        case SimId.ProcessPresunNaPrejazd:
                            ProcessFinishProcessPresunNaPrejazd(message);
                            break;

                        case SimId.ProcessPresunNaStavbu:
                            ProcessFinishProcessPresunNaStavbu(message);
                            break;

                        case SimId.ProcessPresunNaSkladku:
                            ProcessFinishProcessPresunNaSkladku(message);
                            break;
                    }
                    break;

                case Mc.VylozAuto:
                    ProcessVylozAuto(message);
                    break;

                case Mc.Inicializacia:
                    ProcessInicializacia(message);
                    break;

                case Mc.NalozAuto:
                    ProcessNalozAuto(message);
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
    }
}
