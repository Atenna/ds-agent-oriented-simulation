using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.ContinualAssistant
{
    public class PlanovacPracovnejDoby : Scheduler
    {
        public PlanovacPracovnejDoby(int id, OSPABA.Simulation mySim, CommonAgent myAgent) : base(id, mySim, myAgent)
        {
        }

        public override void PrepareReplication()
        {
            MyMessage sprava = new MyMessage(MySim);
            double delTime =
                    Math.Min(Timer.NewWorkDayStartsAt(MySim.CurrentTime, Settings.Constants.NakladacAStartsAt),
                        Timer.NewWorkDayStartsAt(MySim.CurrentTime, Settings.Constants.NakladacBStartsAt));
            sprava.Code = Mc.KoniecPracovnejDoby;
            sprava.Addressee = MyAgent.FindAssistant(SimId.PlanovacPracovnejDoby);
            Hold(delTime, sprava);
        }

        public override void ProcessMessage(MessageForm message)
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

        public void ProcessStart(MessageForm message)
        {
            MessageForm zaciatokPracovnejDobyA = new MyMessage(MySim);
            zaciatokPracovnejDobyA.Addressee = MyAgent;
            zaciatokPracovnejDobyA.Code = Mc.ZaciatokPracovnejDoby;
            Notice(zaciatokPracovnejDobyA);
            /*
            MyMessage sprava = new MyMessage(MySim);
            double delTime =
                    Math.Min(Timer.NewWorkDayStartsAt(MySim.CurrentTime, Settings.Constants.NakladacAStartsAt),
                        Timer.NewWorkDayStartsAt(MySim.CurrentTime, Settings.Constants.NakladacBStartsAt));
            sprava.Code = Mc.KoniecPracovnejDoby;
            sprava.Addressee = MyAgent.FindAssistant(SimId.PlanovacPracovnejDoby);
            Hold(delTime, sprava);
            */
        }

        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.KoniecPracovnejDoby:
                    ProcessStart(message);
                    break;
            }
        }

        public new AgentSkladky MyAgent
        {
            get
            {
                return (AgentSkladky)base.MyAgent;
            }
        }
    }
}
