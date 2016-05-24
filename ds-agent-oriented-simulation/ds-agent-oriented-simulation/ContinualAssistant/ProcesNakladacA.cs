using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.ContinualAssistant
{
    //meta! id="63"
    public class ProcesNakladacA : Process
    {
        private Vehicle _naNalozenie;
        public double StartsAt { get; private set; }
        public double EndsAt { get; private set; }
        public ProcesNakladacA(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
            base(id, mySim, myAgent)
        {
            StartsAt = Constants.NakladacAStartsAt;
            EndsAt = Constants.NakladacAEndsAt;
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication

        }

        //meta! sender="AgentSkladky", id="64", type="Start"
        public void ProcessStart(MessageForm message)
        {
            MyAgent.NakladacAIsOccupied = true;
            ((MyMessage)message).Car.ZaciatokNakladania = MySim.CurrentTime;
            _naNalozenie = ((MyMessage)message).Car;
            _naNalozenie.JeNakladane = true;
            MyAgent.CarAtLoaderA = _naNalozenie;
            double timeOfLoading = _naNalozenie.ToUnload / Constants.LoadMachinePerformance;
            _naNalozenie.RealVolume += _naNalozenie.ToUnload;
            message.Code = Mc.NalozenieUkoncene;
            Hold(timeOfLoading, message);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.NalozenieUkoncene:
                    _naNalozenie.JeNakladane = false;
                    MyAgent.CarAtLoaderA = null;
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
