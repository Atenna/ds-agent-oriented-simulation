using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Managers
{
    //meta! id="15"
    public class ManagerOkolia : Manager
    {
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

        //meta! sender="PlanovacDovozMaterialu", id="55", type="Finish"
        public void ProcessFinishPlanovacDovozMaterialu(MessageForm message)
        {
        }

        //meta! sender="PlanovacOdvozMaterialu", id="125", type="Finish"
        public void ProcessFinishPlanovacOdvozMaterialu(MessageForm message)
        {
        }

        //meta! sender="AgentModelu", id="88", type="Call"
        public void ProcessInicializacia(MessageForm message)
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
                case Mc.Finish:
                    switch (message.Sender.Id)
                    {
                        case SimId.PlanovacDovozMaterialu:
                            ProcessFinishPlanovacDovozMaterialu(message);
                            break;

                        case SimId.PlanovacOdvozMaterialu:
                            ProcessFinishPlanovacOdvozMaterialu(message);
                            break;
                    }
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
