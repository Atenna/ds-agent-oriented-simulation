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
            MyAgent.VykladacAIsOccupied = false;
            message.Code = Mc.VylozAuto;
            message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            Response(message);

            if (!MyAgent.AutaStavbaQueue.IsEmpty() && MyAgent.VykladacAIsWorking())
            {
                Vehicle naVylozenie;
                lock (Constants.Queue2Lock)
                {
                    naVylozenie = MyAgent.AutaStavbaQueue.First.Value;
                    MyAgent.AutaStavbaQueue.RemoveFirst();
                }
                MyMessage msg = MyAgent.MessageStavbaQueue.First.Value;
                MyAgent.MessageStavbaQueue.RemoveFirst();
                msg.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacA);
                msg.Code = Mc.VylozAuto;

                // ukoncenie cakania
                naVylozenie.CasCakaniaNaStavbe = (MySim.CurrentTime - naVylozenie.ZaciatokCakania);
                // pridanie casu cakania na skladke do statistik
                MyAgent.WaitingTimePerCar.AddSample(naVylozenie.CasCakaniaNaStavbe);

                msg.Car = naVylozenie;
                MyAgent.VykladacAIsOccupied = true;
                StartContinualAssistant(msg);
            }
        }

        //meta! sender="ProcesVykladacB", id="72", type="Finish"
        public void ProcessFinishProcesVykladacB(MessageForm message)
        {
            MyAgent.VykladacBIsOccupied = false;
            message.Code = Mc.VylozAuto;
            message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            Response(message);

            if (!MyAgent.AutaStavbaQueue.IsEmpty() && MyAgent.VykladacBIsWorking())
            {
                Vehicle naVylozenie;
                lock (Constants.Queue2Lock)
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
                naVylozenie.CasCakaniaNaStavbe = (MySim.CurrentTime - naVylozenie.ZaciatokCakania);
                // pridanie casu cakania na skladke do statistik
                MyAgent.WaitingTimePerCar.AddSample(naVylozenie.CasCakaniaNaStavbe);

                msg.Car = naVylozenie;
                MyAgent.VykladacBIsOccupied = true;
                StartContinualAssistant(msg);
            }
        }

        //meta! sender="AgentDopravy", id="37", type="Request"
        public void ProcessVylozAuto(MessageForm message)
        {
            Vehicle naVylozenie = ((MyMessage)message).Car;
            // zaciatok cakania v rade

            // to do - podmienka aby sa cakalo iba do konca pracovnej doby Vykladaca a potom sa prirataval cas od zaciatku pracovnej doby
            naVylozenie.ZaciatokCakania = MySim.CurrentTime;

            // to-do - 
            double volumeToUnload = naVylozenie.RealVolume;

            // ak A nepracuje alebo naklada a B nepracuje alebo naklada alebo je zakazany
            if ((MyAgent.VykladacAIsOccupied && MyAgent.VykladacBIsOccupied || MyAgent.VykladacBIsDisabled) || (!MyAgent.VykladacAIsWorking() && !MyAgent.VykladacBIsWorking() || MyAgent.VykladacBIsDisabled))
            {
                MyAgent.AutaStavbaQueue.AddLast(naVylozenie);
                MyAgent.MessageStavbaQueue.AddLast((MyMessage)message);
            }
            else
            {
                // ak B pracuje, nie je zakazany a nenaklada
                if (!MyAgent.VykladacBIsOccupied && !MyAgent.VykladacBIsDisabled && MyAgent.VykladacBIsWorking())
                {
                    message.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacB);
                    MyAgent.VykladacBIsOccupied = true;
                    // koniec cakania
                    naVylozenie.CasCakaniaNaStavbe = (MySim.CurrentTime - naVylozenie.ZaciatokCakania);
                    // pridanie do statistik
                    MyAgent.WaitingTimePerCar.AddSample(naVylozenie.CasCakaniaNaStavbe);
                    StartContinualAssistant(message);
                }
                // ak A pracuje a nenaklada
                else if(!MyAgent.VykladacAIsOccupied && MyAgent.VykladacAIsWorking())
                {
                    message.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacA);
                    MyAgent.VykladacAIsOccupied = true;
                    // koniec cakania
                    naVylozenie.CasCakaniaNaStavbe = (MySim.CurrentTime - naVylozenie.ZaciatokCakania);
                    // pridanie do statistik
                    MyAgent.WaitingTimePerCar.AddSample(naVylozenie.CasCakaniaNaStavbe);
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
                case Mc.ZaciatokPracovnejDoby:
                    ProcessStartWorkDay(message);
                    break;
                case Mc.KoniecPracovnejDoby:
                    ProcessEndWorkDay(message);
                    break;
            }
        }

        private void ProcessEndWorkDay(MessageForm message)
        {
            MyMessage naplanujZaciatokPrace = new MyMessage(MySim);
            naplanujZaciatokPrace.Addressee = MyAgent.FindAssistant(SimId.PlanovacPracovnejDoby2);
            naplanujZaciatokPrace.Code = Mc.Start;
            StartContinualAssistant(naplanujZaciatokPrace);
        }

        private void ProcessStartWorkDay(MessageForm message)
        {
            Vehicle naVylozenie;
            if (!MyAgent.AutaStavbaQueue.IsEmpty())
            {
                lock (Constants.QueueLock)
                {
                    naVylozenie = MyAgent.AutaStavbaQueue.First.Value;
                    MyAgent.AutaStavbaQueue.RemoveFirst();
                }
                MyMessage zFrontu = MyAgent.MessageStavbaQueue.First.Value;
                MyAgent.MessageStavbaQueue.RemoveFirst();

                if (((MyMessage)message).Name == "A")
                {
                    zFrontu.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacA);
                }
                else
                {
                    zFrontu.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacB);
                }

                zFrontu.Code = Mc.VylozAuto;

                // ukoncenie cakania
                naVylozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naVylozenie.ZaciatokCakania);
                // pridanie casu cakania na skladke do statistik
                MyAgent.WaitingTimePerCar.AddSample(naVylozenie.CasCakaniaNaSkladke);

                zFrontu.Car = naVylozenie;
                StartContinualAssistant(zFrontu);
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
