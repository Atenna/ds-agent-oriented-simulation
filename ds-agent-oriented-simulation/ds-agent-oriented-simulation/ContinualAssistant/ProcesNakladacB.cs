using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.ContinualAssistant
{
    //meta! id="69"
    public class ProcesNakladacB : Process
    {

        private Vehicle _naNalozenie;

        public ProcesNakladacB(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
            base(id, mySim, myAgent)
        {
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
        }

        //meta! sender="AgentSkladky", id="70", type="Start"
        public void ProcessStart(MessageForm message)
        {
            MyAgent.NakladacBIsWorking = true;
            _naNalozenie = ((MyMessage)message).Car;
            _naNalozenie.jeNakladane = true;
            MyAgent.CarAtLoaderB = _naNalozenie;
            _naNalozenie.RealVolume = _naNalozenie.Volume;
            double timeOfLoading = _naNalozenie.Volume / Constants.LoadMachine2Performance;
            message.Code = Mc.NalozenieUkoncene;
            Hold(timeOfLoading, message);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.NalozenieUkoncene:
                    MyAgent.CarAtLoaderB = null;
                    _naNalozenie.jeNakladane = false;
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
        public new AgentSkladky MyAgent
        {
            get
            {
                return (AgentSkladky)base.MyAgent;
            }
        }
    }
}
