using System.Windows.Forms;
using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;
using OSPRNG;
using Timer = ds_agent_oriented_simulation.Entities.Timer;

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
            MyMessage sprava = new MyMessage(MySim);
            sprava.Code = Mc.ExportUkonceny;
            sprava.Volume = GenCas.Sample();
            Hold(Timer.ToMinutes(Constants.TimeBetweenExports), sprava);
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.ExportZacaty:
                    ProcessExportZacaty(message);
                    break;

                case Mc.ExportUkonceny:
                    // to-do
                    // poslat spravu agentovi okolia
                    ProcessExportDokonceny((MyMessage)message);
                    break;
            }
        }

        private void ProcessExportZacaty(MessageForm message)
        {
            MyMessage sprava = new MyMessage(MySim);
            sprava.Code = Mc.ExportUkonceny;
            sprava.Volume = GenCas.Sample();
            Hold(Timer.NewWorkDayStartsAt(MySim.CurrentTime, Constants.ExportStartsAt), sprava);
        }

        private void ProcessExportDokonceny(MyMessage message)
        {
            // ak je v pracovnej dobe, posle sa assistant finished
            // To=DO podmienka ci je na stavbe aj material, ktory mozme odviezt
            if (IsWorking(MySim.CurrentTime))
            {
                AssistantFinished(message);
            }
            // ak je noc, hold do noveho dna
            else
            {
                Hold(GetNextWorkingTime(MySim.CurrentTime), message);
            }
        }

        private double GetNextWorkingTime(double currentTime)
        {
            double future = Timer.NewWorkDayStartsAt(MySim.CurrentTime, Constants.ExportStartsAt);

            return (future-currentTime);
        }

        private bool IsWorking(double currentTime)
        {
            //ak je currentTime medzi 7:00 a 10:00
            if (Timer.ToHours(currentTime) >= Constants.ExportStartsAt && Timer.ToHours(currentTime) <= Constants.ExportEndsAt)
            {
                return true;
            }
            return false;
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
