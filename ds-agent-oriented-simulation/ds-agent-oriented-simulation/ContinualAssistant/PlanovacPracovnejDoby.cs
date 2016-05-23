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
            double delTime = Timer.NewWorkDayStartsAt(MySim.CurrentTime, Settings.Constants.NakladacAStartsAt);
            sprava.Name = "A";
            sprava.Code = Mc.KoniecPracovnejDoby;
            sprava.Addressee = MyAgent.FindAssistant(SimId.PlanovacPracovnejDoby);
            Hold(delTime, sprava);

            MyMessage sprava2 = new MyMessage(MySim);
            double delTime2 = Timer.NewWorkDayStartsAt(MySim.CurrentTime, Settings.Constants.NakladacBStartsAt);
            sprava2.Name = "B";
            sprava2.Code = Mc.KoniecPracovnejDoby;
            sprava2.Addressee = MyAgent.FindAssistant(SimId.PlanovacPracovnejDoby);
            Hold(delTime2, sprava2);
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
            MyMessage zaciatokPracovnejDobyA = new MyMessage(MySim);
            zaciatokPracovnejDobyA.Addressee = MyAgent;
            zaciatokPracovnejDobyA.Code = Mc.ZaciatokPracovnejDoby;
            zaciatokPracovnejDobyA.Name = ((MyMessage) message).Name;
            Notice(zaciatokPracovnejDobyA);


            MyMessage sprava = new MyMessage(MySim);
            sprava.Name = ((MyMessage)message).Name;
            double delTime;
            if (sprava.Name == "A")
            {
                delTime = Timer.NewWorkDayStartsAt(MySim.CurrentTime, Settings.Constants.NakladacAStartsAt);
            }
            else
            {
                delTime = Timer.NewWorkDayStartsAt(MySim.CurrentTime, Settings.Constants.NakladacBStartsAt);
            }
            sprava.Code = Mc.KoniecPracovnejDoby;
            sprava.Addressee = MyAgent.FindAssistant(SimId.PlanovacPracovnejDoby);
            Hold(delTime, sprava);
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
