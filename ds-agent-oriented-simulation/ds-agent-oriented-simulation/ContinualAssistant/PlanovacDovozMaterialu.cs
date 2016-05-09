using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;
using OSPRNG;

namespace ds_agent_oriented_simulation.ContinualAssistant
{
    //meta! id="54"
    public class PlanovacDovozMaterialu : Scheduler
    {

        public OSPRNG.ExponentialRNG GenCasA;
        public OSPRNG.EmpiricRNG<double> GenMaterialA;
        public OSPRNG.ExponentialRNG GenCasB;
        public OSPRNG.EmpiricRNG<double> GenMaterialB;
        public OSPRNG.ExponentialRNG GenCasC;
        public OSPRNG.EmpiricRNG<double> GenMaterialC;

        private double volumeA, volumeB, volumeC;
        private double timeA, timeB, timeC;

        public PlanovacDovozMaterialu(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
            base(id, mySim, myAgent)
        {
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
            GenCasA = new ExponentialRNG(46.9, 0.99);
            GenCasB = new ExponentialRNG(36.8, 3);
            GenCasC = new ExponentialRNG(25.8, 0.99);

            GenMaterialA = new EmpiricRNG<double>(
                new EmpiricPair<double>(new UniformContinuousRNG(4, 4.5), 0),
                new EmpiricPair<double>(new UniformContinuousRNG(4.5, 5.5), 0.03),
                new EmpiricPair<double>(new UniformContinuousRNG(5.5, 6.5), 0.016),
                new EmpiricPair<double>(new UniformContinuousRNG(6.5, 7.5), 0.055),
                new EmpiricPair<double>(new UniformContinuousRNG(7.5, 8.5), 0.105),
                new EmpiricPair<double>(new UniformContinuousRNG(8.5, 9.5), 0.167),
                new EmpiricPair<double>(new UniformContinuousRNG(9.5, 10.5), 0.242),
                new EmpiricPair<double>(new UniformContinuousRNG(10.5, 11.5), 0.337),
                new EmpiricPair<double>(new UniformContinuousRNG(11.5, 12.5), 0.444),
                new EmpiricPair<double>(new UniformContinuousRNG(12.5, 13.5), 0.575),
                new EmpiricPair<double>(new UniformContinuousRNG(13.5, 14.5), 0.724),
                new EmpiricPair<double>(new UniformContinuousRNG(14.5, 15.5), 0.884),
                new EmpiricPair<double>(new UniformContinuousRNG(15.5, 16.5), 1.000),
                new EmpiricPair<double>(new UniformContinuousRNG(16.5, 17.5), 1.000),
                new EmpiricPair<double>(new UniformContinuousRNG(17.5, 18.5), 1.000)
            );
            GenMaterialB = new EmpiricRNG<double>(
                new EmpiricPair<double>(new UniformContinuousRNG(4.5, 5.5), 0),
                new EmpiricPair<double>(new UniformContinuousRNG(5.5, 6.5), 0),
                new EmpiricPair<double>(new UniformContinuousRNG(6.5, 7.5), 0.001),
                new EmpiricPair<double>(new UniformContinuousRNG(7.5, 8.5), 0.004),
                new EmpiricPair<double>(new UniformContinuousRNG(8.5, 9.5), 0.007),
                new EmpiricPair<double>(new UniformContinuousRNG(9.5, 10.5), 0.012),
                new EmpiricPair<double>(new UniformContinuousRNG(10.5, 11.5), 0.019),
                new EmpiricPair<double>(new UniformContinuousRNG(11.5, 12.5), 0.026),
                new EmpiricPair<double>(new UniformContinuousRNG(12.5, 13.5), 0.034),
                new EmpiricPair<double>(new UniformContinuousRNG(13.5, 14.5), 0.046),
                new EmpiricPair<double>(new UniformContinuousRNG(14.5, 15.5), 0.056),
                new EmpiricPair<double>(new UniformContinuousRNG(15.5, 16.5), 0.120),
                new EmpiricPair<double>(new UniformContinuousRNG(16.5, 17.5), 0.303),
                new EmpiricPair<double>(new UniformContinuousRNG(17.5, 18.5), 0.497),
                new EmpiricPair<double>(new UniformContinuousRNG(18.5, 19.5), 0.707),
                new EmpiricPair<double>(new UniformContinuousRNG(19.5, 20.5), 0.929),
                new EmpiricPair<double>(new UniformContinuousRNG(20.5, 21.5), 1.000)
            );
            GenMaterialC = new EmpiricRNG<double>(
                new EmpiricPair<double>(new UniformContinuousRNG(4.5, 5.5), 0),
                new EmpiricPair<double>(new UniformContinuousRNG(5.5, 6.5), 0.004),
                new EmpiricPair<double>(new UniformContinuousRNG(6.5, 7.5), 0.007),
                new EmpiricPair<double>(new UniformContinuousRNG(7.5, 8.5), 0.013),
                new EmpiricPair<double>(new UniformContinuousRNG(8.5, 9.5), 0.024),
                new EmpiricPair<double>(new UniformContinuousRNG(9.5, 10.5), 0.031),
                new EmpiricPair<double>(new UniformContinuousRNG(10.5, 11.5), 0.047),
                new EmpiricPair<double>(new UniformContinuousRNG(11.5, 12.5), 0.066),
                new EmpiricPair<double>(new UniformContinuousRNG(12.5, 13.5), 0.085),
                new EmpiricPair<double>(new UniformContinuousRNG(13.5, 14.5), 0.103),
                new EmpiricPair<double>(new UniformContinuousRNG(14.5, 15.5), 0.125),
                new EmpiricPair<double>(new UniformContinuousRNG(15.5, 16.5), 0.148),
                new EmpiricPair<double>(new UniformContinuousRNG(16.5, 17.5), 0.166),
                new EmpiricPair<double>(new UniformContinuousRNG(17.5, 18.5), 0.191),
                new EmpiricPair<double>(new UniformContinuousRNG(18.5, 19.5), 0.217),
                new EmpiricPair<double>(new UniformContinuousRNG(19.5, 20.5), 0.238),
                new EmpiricPair<double>(new UniformContinuousRNG(20.5, 21.5), 0.382),
                new EmpiricPair<double>(new UniformContinuousRNG(21.5, 22.5), 0.588),
                new EmpiricPair<double>(new UniformContinuousRNG(22.5, 23.5), 0.813),
                new EmpiricPair<double>(new UniformContinuousRNG(23.5, 24.5), 0.813)
            );
        }

        //meta! sender="AgentOkolia", id="55", type="Start"
        public void ProcessStart(MessageForm message)
        {
            string comp = ((MyMessage) message).Name;
            switch (comp)
            {
                case "A":
                    NaplanujA((MyMessage)message);
                    break;
                case "B":
                    NaplanujB((MyMessage)message);
                    break;
                case "C":
                    NaplanujC((MyMessage)message);
                    break;
                default:
                    // process default
                    break;
            }
        }

        private void NaplanujC(MyMessage message)
        {
            message.Volume = GenMaterialC.Sample();
            message.Time = GenCasC.Sample();
            message.Code = Mc.DovozMaterialu;
            Hold(message.Time, message);
        }

        private void NaplanujB(MyMessage message)
        {
            message.Volume = GenMaterialB.Sample();
            message.Time = GenCasB.Sample();
            message.Code = Mc.DovozMaterialu;
            Hold(message.Time, message);
        }

        private void NaplanujA(MyMessage message)
        {
            message.Volume = GenMaterialA.Sample();
            message.Time = GenCasA.Sample();
            message.Code = Mc.DovozMaterialu;
            Hold(message.Time, message);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.DovozMaterialu:
                    //ProcessDovozMaterialu((MyMessage)message);
                    AssistantFinished(message);
                    break;
            }
        }

        private void ProcessDovozMaterialu(MyMessage message)
        {
            
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
