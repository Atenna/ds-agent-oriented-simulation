using System;
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
        public OSPRNG.EmpiricRNG<int> GenMaterialA;
        public OSPRNG.ExponentialRNG GenCasB;
        public OSPRNG.EmpiricRNG<int> GenMaterialB;
        public OSPRNG.ExponentialRNG GenCasC;
        public OSPRNG.EmpiricRNG<int> GenMaterialC;

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

            GenMaterialA = new EmpiricRNG<int>(
                new EmpiricPair<int>(new UniformDiscreteRNG(4, 4), 0),
                new EmpiricPair<int>(new UniformDiscreteRNG(5, 5), 0.0033085),
                new EmpiricPair<int>(new UniformDiscreteRNG(6, 6), 0.0128205),
                new EmpiricPair<int>(new UniformDiscreteRNG(7, 7), 0.0384615),
                new EmpiricPair<int>(new UniformDiscreteRNG(8, 8), 0.0500414),
                new EmpiricPair<int>(new UniformDiscreteRNG(9, 9), 0.0624483),
                new EmpiricPair<int>(new UniformDiscreteRNG(10, 10), 0.0744417),
                new EmpiricPair<int>(new UniformDiscreteRNG(11, 11), 0.0959471),
                new EmpiricPair<int>(new UniformDiscreteRNG(12, 12), 0.1066998),
                new EmpiricPair<int>(new UniformDiscreteRNG(13, 13), 0.1306865),
                new EmpiricPair<int>(new UniformDiscreteRNG(14, 14), 0.1488834),
                new EmpiricPair<int>(new UniformDiscreteRNG(15, 15), 0.1600496),
                new EmpiricPair<int>(new UniformDiscreteRNG(16, 16), 0.115798),
                new EmpiricPair<int>(new UniformDiscreteRNG(19, 19), 0.0004136));


            GenMaterialB = new EmpiricRNG<int>(
                new EmpiricPair<int>(new UniformDiscreteRNG(6, 6), 0.0003509),
                    new EmpiricPair<int>(new UniformDiscreteRNG(7, 7), 0.0010526),
                    new EmpiricPair<int>(new UniformDiscreteRNG(8, 8), 0.0028070),
                    new EmpiricPair<int>(new UniformDiscreteRNG(9, 9), 0.0031579),
                    new EmpiricPair<int>(new UniformDiscreteRNG(10, 10), 0.0045614),
                    new EmpiricPair<int>(new UniformDiscreteRNG(11, 11), 0.0073684),
                    new EmpiricPair<int>(new UniformDiscreteRNG(12, 12), 0.0070175),
                    new EmpiricPair<int>(new UniformDiscreteRNG(13, 13), 0.0077193),
                    new EmpiricPair<int>(new UniformDiscreteRNG(14, 14), 0.0115789),
                    new EmpiricPair<int>(new UniformDiscreteRNG(15, 15), 0.0105263),
                    new EmpiricPair<int>(new UniformDiscreteRNG(16, 16), 0.0638596),
                    new EmpiricPair<int>(new UniformDiscreteRNG(17, 17), 0.1828070),
                    new EmpiricPair<int>(new UniformDiscreteRNG(18, 18), 0.1940351),
                    new EmpiricPair<int>(new UniformDiscreteRNG(19, 19), 0.2098246),
                    new EmpiricPair<int>(new UniformDiscreteRNG(20, 20), 0.2224561),
                    new EmpiricPair<int>(new UniformDiscreteRNG(21, 11), 0.0708772));

            GenMaterialC = new EmpiricRNG<int>(
                new EmpiricPair<int>(new UniformDiscreteRNG(5, 5), 0.0004735),
                    new EmpiricPair<int>(new UniformDiscreteRNG(6, 6), 0.0030777),
                    new EmpiricPair<int>(new UniformDiscreteRNG(7, 7), 0.0037879),
                    new EmpiricPair<int>(new UniformDiscreteRNG(8, 8), 0.0059186),
                    new EmpiricPair<int>(new UniformDiscreteRNG(9, 9), 0.0104167),
                    new EmpiricPair<int>(new UniformDiscreteRNG(10, 10), 0.0073390),
                    new EmpiricPair<int>(new UniformDiscreteRNG(11, 11), 0.0163352),
                    new EmpiricPair<int>(new UniformDiscreteRNG(12, 12), 0.0187027),
                    new EmpiricPair<int>(new UniformDiscreteRNG(13, 13), 0.0184660),
                    new EmpiricPair<int>(new UniformDiscreteRNG(14, 14), 0.0184659),
                    new EmpiricPair<int>(new UniformDiscreteRNG(15, 15), 0.0217803),
                    new EmpiricPair<int>(new UniformDiscreteRNG(16, 16), 0.0229640),
                    new EmpiricPair<int>(new UniformDiscreteRNG(17, 17), 0.0187027),
                    new EmpiricPair<int>(new UniformDiscreteRNG(18, 18), 0.0246212),
                    new EmpiricPair<int>(new UniformDiscreteRNG(19, 19), 0.0262784),
                    new EmpiricPair<int>(new UniformDiscreteRNG(20, 20), 0.0210701),
                    new EmpiricPair<int>(new UniformDiscreteRNG(21, 21), 0.1439394),
                    new EmpiricPair<int>(new UniformDiscreteRNG(22, 22), 0.2057292),
                    new EmpiricPair<int>(new UniformDiscreteRNG(23, 23), 0.2249053),
                    new EmpiricPair<int>(new UniformDiscreteRNG(24, 24), 0.1870265));        }

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
