using System;
using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Managers
{
    //meta! id="19"
    public class ManagerDopravy : Manager
    {

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
            message.Addressee = MyAgent.FindAssistant(SimId.ProcessPresunNaSkladku);
            message.Code = Mc.NalozAuto;
            StartContinualAssistant(message);
        }

        //meta! sender="ProcessPresunNaStavbu", id="108", type="Finish"
        public void ProcessFinishProcessPresunNaStavbu(MessageForm message)
        {
            foreach (Vehicle car in ((MyMessage)message).cars)
            {
                MyMessage sprava = new MyMessage(MySim);
                sprava.Code = Mc.VylozAuto;
                sprava.Car = car;
                sprava.Addressee = MySim.FindAgent(SimId.AgentStavby);
                Request(sprava);

                /*car.RealVolume = 0;
                message.Addressee = MyAgent.FindAssistant(SimId.ProcessPresunNaPrejazd);
                sprava.Car = car;
                StartContinualAssistant(sprava);*/
            }
        }

        //meta! sender="ProcessPresunNaSkladku", id="106", type="Finish"
        public void ProcessFinishProcessPresunNaSkladku(MessageForm message)
        {
            foreach (Vehicle car in ((MyMessage)message).cars)
            {
                MyMessage sprava = new MyMessage(MySim);
                sprava.Code = Mc.NalozAuto;
                sprava.Car = car;
                sprava.Addressee = MySim.FindAgent(SimId.AgentSkladky);
                Request(sprava);
            }
        }

        //meta! sender="AgentModelu", id="91", type="Call"
        public void ProcessInicializacia(MessageForm message)
        {
            MyMessage sprava = (MyMessage)message;
            InicializujAuta(sprava.SelectedCars);

            foreach (Vehicle i in MyAgent.EnabledCars)
            {
                sprava = new MyMessage(MySim, i);
                sprava.Addressee = MySim.FindAgent(SimId.AgentSkladky);
                sprava.Code = Mc.NalozAuto;
                Request(sprava);
            }

            //ProcessNaplanujZaciatok();
        }

        //meta! sender="AgentSkladky", id="36", type="Response"
        public void ProcessNalozAuto(MessageForm message)
        {
            // auto skoncilo nakladanie 

            MyMessage sprava = new MyMessage(MySim);
            sprava.Volume = ((MyMessage)message).Volume;
            sprava.Car = ((MyMessage)message).Car;
            sprava.Addressee = MyAgent.FindAssistant(SimId.ProcessPresunNaStavbu);
            StartContinualAssistant(sprava);
        }

        //meta! sender="AgentStavby", id="37", type="Response"
        public void ProcessVylozAuto(MessageForm message)
        {
            // vykladanie skoncilo, auto pojde na prejazd
            message.Addressee = MyAgent.FindAssistant(SimId.ProcessPresunNaPrejazd);
            StartContinualAssistant(message);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.DovozMaterialu:
                    ProcessDovozMaterialu(message);
                    break;
                case Mc.OdvozMaterialu:
                    ProcessOdvozMaterialu(message);
                    break;
            }
        }

        private void ProcessOdvozMaterialu(MessageForm message)
        {
            MyMessage spravaDolava = new MyMessage(MySim);
            spravaDolava.Code = Mc.OdvozMaterialu;
            spravaDolava.Volume = ((MyMessage)message).Volume;
            spravaDolava.Addressee = MySim.FindAgent(SimId.AgentStavby);
            Notice(spravaDolava);
        }

        private void ProcessDovozMaterialu(MessageForm message)
        {
            message.Addressee = MySim.FindAgent(SimId.AgentSkladky);
            Notice(message);
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

        private void InicializujAuta(int[] setup)
        {
            for (int i = 0; i < setup.Length; i++)
            {
                MyAgent.PrepareCars(setup[i], i);
            }
        }
    }
}
