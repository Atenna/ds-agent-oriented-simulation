using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;
using OSPRNG;

namespace ds_agent_oriented_simulation.ContinualAssistant
{
    //meta! id="124"
    public class PlanovacOdvozMaterialu : Scheduler
    {
        public OSPRNG.EmpiricRNG<int> GenCas;
        public PlanovacOdvozMaterialu(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
            base(id, mySim, myAgent)
        {
            GenCas =    new EmpiricRNG<int>(new EmpiricPair<int>(new UniformDiscreteRNG(10, 20), 0.02),
                        new EmpiricPair<int>(new UniformDiscreteRNG(21, 48), 0.2),
                        new EmpiricPair<int>(new UniformDiscreteRNG(49, 65), 0.33),
                        new EmpiricPair<int>(new UniformDiscreteRNG(66, 79), 0.3),
                        new EmpiricPair<int>(new UniformDiscreteRNG(80, 99), 0.15));
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
        }

        //meta! sender="AgentOkolia", id="125", type="Start"
        public void ProcessStart(MessageForm message)
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
        public new AgentOkolia MyAgent
        {
            get
            {
                return (AgentOkolia)base.MyAgent;
            }
        }
    }
}
