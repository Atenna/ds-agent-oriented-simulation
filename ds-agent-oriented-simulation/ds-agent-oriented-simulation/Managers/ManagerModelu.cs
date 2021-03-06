using ds_agent_oriented_simulation.Agents;
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

        //meta! sender="AgentOkolia", id="116", type="Notice"
        public void ProcessOdvozMaterialu(MessageForm message)
        {
            MyMessage spravaDoprava = new MyMessage(MySim);
            spravaDoprava.Volume = ((MyMessage)message).Volume;
            spravaDoprava.Code = Mc.OdvozMaterialu;
            spravaDoprava.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            Notice(spravaDoprava);
        }

        //meta! sender="AgentOkolia", id="35", type="Notice"
        public void ProcessDovozMaterialu(MessageForm message)
        {
            message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            Notice(message);
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
                    ProcessInicializacia(message);
                    break;

                case Mc.OdvozMaterialu:
                    ProcessOdvozMaterialu(message);
                    break;

                case Mc.DovozMaterialu:
                    ProcessDovozMaterialu(message);
                    break;

                default:
                    ProcessDefault(message);
                    break;
            }
        }

        private void ProcessInicializacia(MessageForm message)
        {
            // vytvoria sa auta pre konkretny variant simulacie a poslu sa spravy na nakladanie auta agentovi skladky
            message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            Call(message);
            // v AgentOkolia sa nainicializuju generatory pre dovozcov materialu
            MyMessage sprava = new MyMessage(MySim);
            sprava.Code = Mc.Inicializacia;
            sprava.Addressee = MySim.FindAgent(SimId.AgentOkolia);
            Call(sprava);
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
