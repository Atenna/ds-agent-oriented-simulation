using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Managers
{
    //meta! id="18"
    public class ManagerStavby : Manager
    {
        public ManagerStavby(int id, OSPABA.Simulation mySim, Agent myAgent) :
            base(id, mySim, myAgent)
        {
            Init();
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication

            if (PetriNet != null)
            {
                PetriNet.Clear();
            }
        }

        //meta! sender="ProcesVykladacA", id="67", type="Finish"
        public void ProcessFinishProcesVykladacA(MessageForm message)
        {
            MyAgent.VykladacAIsWorking = false;
            message.Code = Mc.VylozAuto;
            message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            Response(message);

            if (!MyAgent.AutaStavbaQueue.IsEmpty())
            {
                Vehicle naVylozenie;
                lock (Constants.queue2Lock)
                {
                    naVylozenie = MyAgent.AutaStavbaQueue.First.Value;
                    MyAgent.AutaStavbaQueue.RemoveFirst();
                }
                MyMessage msg = MyAgent.MessageStavbaQueue.First.Value;
                MyAgent.MessageStavbaQueue.RemoveFirst();
                msg.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacA);
                msg.Code = Mc.VylozAuto;

                // ukoncenie cakania
                naVylozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naVylozenie.ZaciatokCakania);
                // pridanie casu cakania na skladke do statistik
                MyAgent.WaitingTimePerCar.AddSample(naVylozenie.CasCakaniaNaSkladke);

                msg.Car = naVylozenie;
                MyAgent.VykladacAIsWorking = true;
                StartContinualAssistant(msg);
            }
        }

        //meta! sender="ProcesVykladacB", id="72", type="Finish"
        public void ProcessFinishProcesVykladacB(MessageForm message)
        {
            MyAgent.VykladacBIsWorking = false;
            message.Code = Mc.VylozAuto;
            message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            Response(message);

            if (!MyAgent.AutaStavbaQueue.IsEmpty())
            {
                Vehicle naVylozenie;
                lock (Constants.queue2Lock)
                {
                    naVylozenie = MyAgent.AutaStavbaQueue.First.Value;
                    MyAgent.AutaStavbaQueue.RemoveFirst();
                }
                MyMessage msg = MyAgent.MessageStavbaQueue.First.Value;
                MyAgent.MessageStavbaQueue.RemoveFirst();
                msg.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacB);
                msg.Code = Mc.VylozAuto;
                //msg.Addressee = MySim.FindAgent(SimId.AgentStavby);
                //Request(msg);

                // ukoncenie cakania
                naVylozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naVylozenie.ZaciatokCakania);
                // pridanie casu cakania na skladke do statistik
                MyAgent.WaitingTimePerCar.AddSample(naVylozenie.CasCakaniaNaSkladke);

                msg.Car = naVylozenie;
                MyAgent.VykladacBIsWorking = true;
                StartContinualAssistant(msg);
            }
        }

        //meta! sender="AgentDopravy", id="37", type="Request"
        public void ProcessVylozAuto(MessageForm message)
        {
            Vehicle naVylozenie = ((MyMessage)message).Car;
            // zaciatok cakania v rade
            naVylozenie.ZaciatokCakania = MySim.CurrentTime;

            // to-do
            double volumeToUnload = naVylozenie.RealVolume;

            if (MyAgent.VykladacAIsWorking && MyAgent.VykladacBIsWorking)
            {
                MyAgent.AutaStavbaQueue.AddLast(naVylozenie);
                MyAgent.MessageStavbaQueue.AddLast((MyMessage)message);
            }
            else
            {
                if (MyAgent.VykladacAIsWorking)
                {
                    message.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacB);
                    MyAgent.VykladacBIsWorking = true;
                    // koniec cakania
                    naVylozenie.CasCakaniaNaStavbe = (MySim.CurrentTime - naVylozenie.ZaciatokCakania);
                    // pridanie do statistik
                    MyAgent.WaitingTimePerCar.AddSample(naVylozenie.CasCakaniaNaSkladke);
                    StartContinualAssistant(message);
                }
                else
                {
                    message.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacA);
                    MyAgent.VykladacAIsWorking = true;
                    // koniec cakania
                    naVylozenie.CasCakaniaNaStavbe = (MySim.CurrentTime - naVylozenie.ZaciatokCakania);
                    // pridanie do statistik
                    MyAgent.WaitingTimePerCar.AddSample(naVylozenie.CasCakaniaNaSkladke);
                    StartContinualAssistant(message);
                }

            }
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.OdvozMaterialu:
                    ProcessOdvozMaterialu(message);
                    break;
            }
        }

        private void ProcessOdvozMaterialu(MessageForm message)
        {
            // tu sa od materialu odpocita objem a posle sa spat sprava, kolko je materialu
            MyAgent.MaterialNaStavbe -= ((MyMessage) message).Volume;
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        public void Init()
        {
        }

        override public void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.VylozAuto:
                    ProcessVylozAuto(message);
                    break;

                case Mc.Finish:
                    switch (message.Sender.Id)
                    {
                        case SimId.ProcesVykladacA:
                            ProcessFinishProcesVykladacA(message);
                            break;

                        case SimId.ProcesVykladacB:
                            ProcessFinishProcesVykladacB(message);
                            break;
                    }
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
