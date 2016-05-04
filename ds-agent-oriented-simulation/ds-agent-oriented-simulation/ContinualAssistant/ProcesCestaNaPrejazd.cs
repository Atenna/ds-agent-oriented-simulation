using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.ContinualAssistant
{
    class ProcesCestaNaPrejazd : Process
    {
        public ProcesCestaNaPrejazd(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
        }

        //meta! sender="AgentStavby", id="72", type="Start"
        public void ProcessStart(MessageForm message)
        {
            Vehicle naNalozenie = ((MyMessage)message).Car;
            double casPrejazdu = (Constants.BcLength / (double)(naNalozenie.Speed / 60.0));
            if (naNalozenie.HasFailed())
            {
                casPrejazdu += naNalozenie.GetTimeOfRepair();
            }
            message.Code = Mc.PrejazdUkonceny;
            Hold(casPrejazdu, message);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.PrejazdUkonceny:
                    AssistantFinished(message);
                break;
            }
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        override public void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.Start:
                    ProcessStart(message);
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
