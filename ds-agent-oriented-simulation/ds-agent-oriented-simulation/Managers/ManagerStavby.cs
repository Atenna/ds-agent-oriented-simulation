using System;
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
            Vehicle auto = ((MyMessage)message).Car;

            if (auto.RealVolume == 0)
            {
                MyAgent.VykladacAIsOccupied = false;
                message.Code = Mc.VylozAuto;
                message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
                Response(message);

                if (!MyAgent.AutaStavbaQueue.IsEmpty() && MyAgent.VykladacAIsWorking() &&
                    MyAgent.MaterialNaStavbe < Constants.MaxMaterialAtBuilding)
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

                    naVylozenie.ToUnload = LoadCarWith(naVylozenie.RealVolume);
                    msg.Car = naVylozenie;
                    MyAgent.VykladacAIsOccupied = true;
                    StartContinualAssistant(msg);
                }
            }
            else
            {
                if (MyAgent.VykladacAIsWorking() && MyAgent.MaterialNaStavbe < Constants.MaxMaterialAtBuilding)
                {
                    ((MyMessage)message).Car.ToUnload = LoadCarWith(auto.RealVolume);
                    message.Code = Mc.VylozAuto;
                    message.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacA);
                    MyAgent.VykladacAIsOccupied = true;
                    StartContinualAssistant(message);
                }
                else
                {
                    MyAgent.AutaStavbaQueue.AddFirst(((MyMessage)message).Car);
                    MyAgent.MessageStavbaQueue.AddFirst((MyMessage)message);
                    MyAgent.VykladacAIsOccupied = false;
                }
            }
        }

        //meta! sender="ProcesVykladacB", id="72", type="Finish"
        public void ProcessFinishProcesVykladacB(MessageForm message)
        {
            Vehicle auto = ((MyMessage)message).Car;

            if (auto.RealVolume == 0)
            {
                MyAgent.VykladacBIsOccupied = false;
                message.Code = Mc.VylozAuto;
                message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
                Response(message);

                if (!MyAgent.AutaStavbaQueue.IsEmpty() && MyAgent.VykladacBIsWorking() && MyAgent.MaterialNaStavbe < Constants.MaxMaterialAtBuilding)
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

                    naVylozenie.ToUnload = LoadCarWith(naVylozenie.RealVolume);
                    msg.Car = naVylozenie;
                    MyAgent.VykladacBIsOccupied = true;
                    StartContinualAssistant(msg);
                }
            }
            else
            {
                // je volna kapacita?
                if (MyAgent.VykladacBIsWorking() && MyAgent.MaterialNaStavbe < Constants.MaxMaterialAtBuilding)
                {
                    ((MyMessage)message).Car.ToUnload = LoadCarWith(auto.RealVolume);
                    message.Code = Mc.VylozAuto;
                    message.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacB);
                    MyAgent.VykladacBIsOccupied = true;
                    StartContinualAssistant(message);
                }
                else
                {
                    MyAgent.AutaStavbaQueue.AddFirst(((MyMessage)message).Car);
                    MyAgent.MessageStavbaQueue.AddFirst((MyMessage)message);
                    MyAgent.VykladacBIsOccupied = false;
                }
            }
        }

        //meta! sender="AgentDopravy", id="37", type="Request"
        public void ProcessVylozAuto(MessageForm message)
        {
            Vehicle naVylozenie = ((MyMessage)message).Car;

            // zaciatok cakania v rade

            // to do - podmienka aby sa cakalo iba do konca pracovnej doby Vykladaca a potom sa prirataval cas od zaciatku pracovnej doby
            naVylozenie.ZaciatokCakania = MySim.CurrentTime;

            // ak A nepracuje alebo naklada a B nepracuje alebo naklada alebo je zakazany
            if ((!(!MyAgent.VykladacBIsOccupied && !MyAgent.VykladacBIsDisabled && MyAgent.VykladacBIsWorking()) && !(!MyAgent.VykladacAIsOccupied && MyAgent.VykladacAIsWorking())) || MyAgent.MaterialNaStavbe == Constants.MaxMaterialAtBuilding)
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

                    naVylozenie.ToUnload = LoadCarWith(naVylozenie.RealVolume);
                    MyAgent.WaitingTimePerCar.AddSample(naVylozenie.CasCakaniaNaStavbe);
                    StartContinualAssistant(message);
                }
                // ak A pracuje a nenaklada
                else if (!MyAgent.VykladacAIsOccupied && MyAgent.VykladacAIsWorking())
                {
                    message.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacA);
                    MyAgent.VykladacAIsOccupied = true;
                    // koniec cakania
                    naVylozenie.CasCakaniaNaStavbe = (MySim.CurrentTime - naVylozenie.ZaciatokCakania);
                    // pridanie do statistik

                    naVylozenie.ToUnload = LoadCarWith(naVylozenie.RealVolume);
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
                    ProcessOdvozMaterialu((MyMessage)message);
                    break;
                case Mc.ZaciatokPracovnejDoby:
                    ProcessStartWorkDay(message);
                    break;
                case Mc.KoniecPracovnejDoby:
                    ProcessEndWorkDay(message);
                    break;
            }
        }

        public double LoadCarWith(double volume)
        {
            if (MyAgent.MaterialNaStavbe + volume <= Constants.MaxMaterialAtBuilding)
            {
                MyAgent.MaterialNaStavbe += volume;
                return volume;
            }
            double material = Constants.MaxMaterialAtBuilding - MyAgent.MaterialNaStavbe;
            MyAgent.MaterialNaStavbe = Constants.MaxMaterialAtBuilding;
            return material;
        }

        public void NejakaMetoda(MyMessage message)
        {
            Vehicle naNalozenie = null;

            // ak  B ma pracovnu dobu a nenaklada nikoho
            if (!MyAgent.VykladacBIsOccupied && !MyAgent.AutaStavbaQueue.IsEmpty() && MyAgent.VykladacBIsWorking() && MyAgent.MaterialNaStavbe < Constants.MaxMaterialAtBuilding && !MyAgent.VykladacBIsDisabled)
            {
                MyMessage sprava = MyAgent.MessageStavbaQueue.First.Value;
                MyAgent.MessageStavbaQueue.RemoveFirst();

                naNalozenie = MyAgent.AutaStavbaQueue.First.Value;
                MyAgent.AutaStavbaQueue.RemoveFirst();

                sprava.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacB);
                MyAgent.VykladacBIsOccupied = true;
                // koniec cakania
                naNalozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naNalozenie.ZaciatokCakania);
                // nalozime mnozstvo ktore je aktualne na skladke
                naNalozenie.ToUnload = (int)LoadCarWith(naNalozenie.RealVolume);
                // pridanie casu cakania na skladke do statistik

                sprava.Car = naNalozenie;
                MyAgent.WaitingTimePerCar.AddSample(naNalozenie.CasCakaniaNaSkladke);
                StartContinualAssistant(sprava);
            }
            else if (!MyAgent.VykladacAIsOccupied && !MyAgent.AutaStavbaQueue.IsEmpty() && MyAgent.VykladacAIsWorking() && MyAgent.MaterialNaStavbe < Constants.MaxMaterialAtBuilding)
            {
                MyMessage sprava = MyAgent.MessageStavbaQueue.First.Value;
                MyAgent.MessageStavbaQueue.RemoveFirst();

                naNalozenie = MyAgent.AutaStavbaQueue.First.Value;
                MyAgent.AutaStavbaQueue.RemoveFirst();

                sprava.Addressee = MyAgent.FindAssistant(SimId.ProcesVykladacA);
                MyAgent.VykladacAIsOccupied = true;
                // koniec cakania
                naNalozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naNalozenie.ZaciatokCakania);
                // nalozime mnozstvo ktore je aktualne na skladke
                naNalozenie.ToUnload = (int)LoadCarWith(naNalozenie.RealVolume);
                // pridanie casu cakania na skladke do statistik

                sprava.Car = naNalozenie;
                MyAgent.WaitingTimePerCar.AddSample(naNalozenie.CasCakaniaNaSkladke);
                StartContinualAssistant(sprava);
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
            AddUsageStats(message);

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
                naVylozenie.ToUnload = (int)LoadCarWith(naVylozenie.RealVolume);
                // ukoncenie cakania
                naVylozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naVylozenie.ZaciatokCakania);
                // pridanie casu cakania na skladke do statistik
                MyAgent.WaitingTimePerCar.AddSample(naVylozenie.CasCakaniaNaSkladke);

                zFrontu.Car = naVylozenie;
                StartContinualAssistant(zFrontu);
            }
        }
        private void AddUsageStats(MessageForm msg)
        {
            if (MySim.CurrentTime > 1440)
            {
                if (((MyMessage)msg).Name == "A")
                {
                    double workingA = MyAgent.RealWorkingA / 870;
                    MyAgent.RealWorkingTimeA.AddSample(workingA);
                    MyAgent.RealWorkingA = 0;
                }
                if (((MyMessage)msg).Name == "B")
                {
                    double workingB = MyAgent.RealWorkingB / 870;
                    MyAgent.RealWorkingTimeB.AddSample(workingB);
                    MyAgent.RealWorkingB = 0;
                }
            }
        }
        private void ProcessOdvozMaterialu(MyMessage message)
        {
            MyAgent.PocetExport++;

            // tu sa od materialu odpocita objem a posle sa spat sprava, kolko je materialu
            if (MyAgent.MaterialNaStavbe < ((MyMessage)message).Volume)
            {
                MyAgent.MaterialNaStavbe = 0;
                // neuspesny pokus 
            }
            else
            {
                MyAgent.MaterialNaStavbe -= ((MyMessage)message).Volume;
                // uspesny pokus, pridanie do statistik
                MyAgent.PocetUspesnyExport++;
            }
            if (!MyAgent.AutaStavbaQueue.IsEmpty())
            {
                // ak je opat miesto na skladke stavby :D tak moze auto, ktore caka, opat nakladat material
                NejakaMetoda(message);
            }
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
