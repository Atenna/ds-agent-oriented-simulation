using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.ContinualAssistant
{
    //meta! id="66"
    public class ProcesVykladacA : Process
    {

        private Vehicle _naVylozenie;
        public double StartsAt { get; private set; }
        public double EndsAt { get; private set; }

        public ProcesVykladacA(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
            base(id, mySim, myAgent)
        {
            StartsAt = Constants.VykladacAStartsAt;
            EndsAt = Constants.VykladacAEndsAt;
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
            
        }

        //meta! sender="AgentStavby", id="67", type="Start"
        public void ProcessStart(MessageForm message)
        {
            MyAgent.VykladacAIsWorking = true;
            _naVylozenie = ((MyMessage)message).Car;
            _naVylozenie.jeVykladane = true;
            MyAgent.CarAtUnloaderA = _naVylozenie;
            double timeOfUnloading = _naVylozenie.RealVolume / Constants.UnloadMachinePerformance;
            message.Code = Mc.VylozenieUkoncene;
            _naVylozenie.RealVolume = 0;
            Hold(timeOfUnloading, message);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.VylozenieUkoncene:
                    MyAgent.CarAtUnloaderA = null;
                    _naVylozenie.jeVykladane = false;
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
        public new AgentStavby MyAgent
        {
            get
            {
                return (AgentStavby)base.MyAgent;
            }
        }
    }
}
