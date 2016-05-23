using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Managers
{
    //meta! id="17"
    public class ManagerSkladky : Manager
    {
        public ManagerSkladky(int id, OSPABA.Simulation mySim, Agent myAgent) :
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

        //meta! sender="ProcesNakladacA", id="64", type="Finish"
        public void ProcessFinishProcesNakladacA(MessageForm message)
        {
            MyAgent.NakladacAIsOccupied = false;

            message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            message.Code = Mc.NalozAuto;
            Response(message);

            
            // ak v rade niekto dalsi caka, zacne sa znova nakladanie
            Vehicle naNalozenie;
            if (!MyAgent.AutaSkladkaQueue.IsEmpty() && MyAgent.NakladacAIsWorking() && MyAgent.MaterialNaSkladke!=0)
            {
                lock (Constants.QueueLock)
                {
                    naNalozenie = MyAgent.AutaSkladkaQueue.First.Value;
                    MyAgent.AutaSkladkaQueue.RemoveFirst();
                }
                MyMessage zFrontu = MyAgent.MessageSkladkaQueue.First.Value;
                MyAgent.MessageSkladkaQueue.RemoveFirst();
                zFrontu.Addressee = MyAgent.FindAssistant(SimId.ProcesNakladacA);
                zFrontu.Code = Mc.NalozAuto;

                // nalozime mnozstvo ktore je aktualne na skladke
                naNalozenie.RealVolume = (int) LoadCarWith(naNalozenie.Volume);

                // ukoncenie cakania
                naNalozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naNalozenie.ZaciatokCakania);
                // pridanie casu cakania na skladke do statistik
                MyAgent.SkladkaWStat.AddSample(naNalozenie.CasCakaniaNaSkladke);

                zFrontu.Car = naNalozenie;
                MyAgent.NakladacAIsOccupied = true;
                StartContinualAssistant(zFrontu);
            }
            
        }

        //meta! sender="ProcesNakladacB", id="70", type="Finish"
        public void ProcessFinishProcesNakladacB(MessageForm message)
        {
            MyAgent.NakladacBIsOccupied = false;

            message.Addressee = MySim.FindAgent(SimId.AgentDopravy);
            message.Code = Mc.NalozAuto;
            Response(message);
            
            Vehicle naNalozenie;
            if (!MyAgent.AutaSkladkaQueue.IsEmpty() && MyAgent.NakladacBIsWorking() && MyAgent.MaterialNaSkladke != 0)
            {
                lock (Constants.QueueLock)
                {
                    naNalozenie = MyAgent.AutaSkladkaQueue.First.Value;
                    MyAgent.AutaSkladkaQueue.RemoveFirst();
                }
                MyMessage zFrontu = MyAgent.MessageSkladkaQueue.First.Value;
                MyAgent.MessageSkladkaQueue.RemoveFirst();
                zFrontu.Addressee = MyAgent.FindAssistant(SimId.ProcesNakladacB);
                zFrontu.Code = Mc.NalozAuto;

                // nalozime mnozstvo ktore je aktualne na skladke
                naNalozenie.RealVolume = (int)LoadCarWith(naNalozenie.Volume);

                // ukoncenie cakania
                naNalozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naNalozenie.ZaciatokCakania);
                // pridanie casu cakania na skladke do statistik
                MyAgent.SkladkaWStat.AddSample(naNalozenie.CasCakaniaNaSkladke);

                zFrontu.Car = naNalozenie;
                StartContinualAssistant(zFrontu);
            }
            
        }

        //meta! sender="AgentDopravy", id="36", type="Request"
        public void ProcessNalozAuto(MessageForm message)
        {
            Vehicle naNalozenie = ((MyMessage)message).Car;
            // zaciatok cakania 
            // to do dorobit ak cakalo cez noc tak +=
            naNalozenie.ZaciatokCakania = MySim.CurrentTime;

            // TO=DO - KOLKO SA BUDE NAKLADAT NA AUTO ak bude na skladke menej materialu? Pocka na dovoz????

            // ak nakladace prave niekoho nakladaju alebo nemaju pracovnu dobu
            if (!(!MyAgent.NakladacBIsOccupied && MyAgent.NakladacBIsWorking()) && !(!MyAgent.NakladacAIsOccupied && MyAgent.NakladacAIsWorking()) && MyAgent.MaterialNaSkladke != 0)
            {
                lock (naNalozenie)
                {
                    MyAgent.AutaSkladkaQueue.AddLast(naNalozenie);
                    MyAgent.MessageSkladkaQueue.AddLast((MyMessage)message);
                }
            }
            else
            {
                // ak  B ma pracovnu dobu a nenaklada nikoho
                if (!MyAgent.NakladacBIsOccupied && MyAgent.NakladacBIsWorking())
                {
                    message.Addressee = MyAgent.FindAssistant(SimId.ProcesNakladacB);
                    MyAgent.NakladacBIsOccupied = true;
                    // koniec cakania
                    naNalozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naNalozenie.ZaciatokCakania);
                    // nalozime mnozstvo ktore je aktualne na skladke
                    naNalozenie.RealVolume = (int)LoadCarWith(naNalozenie.Volume);
                    // pridanie casu cakania na skladke do statistik
                    MyAgent.SkladkaWStat.AddSample(naNalozenie.CasCakaniaNaSkladke);
                    StartContinualAssistant(message);
                }
                else if(!MyAgent.NakladacAIsOccupied && MyAgent.NakladacAIsWorking())
                {
                    message.Addressee = MyAgent.FindAssistant(SimId.ProcesNakladacA);
                    MyAgent.NakladacAIsOccupied = true;
                    // koniec cakania
                    naNalozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naNalozenie.ZaciatokCakania);
                    // nalozime mnozstvo ktore je aktualne na skladke
                    naNalozenie.RealVolume = (int)LoadCarWith(naNalozenie.Volume);
                    // pridanie casu cakania na skladke do statistik
                    MyAgent.SkladkaWStat.AddSample(naNalozenie.CasCakaniaNaSkladke);
                    StartContinualAssistant(message);
                }
            }
        }

        public double LoadCarWith(double volume)
        {
            if (MyAgent.MaterialNaSkladke > volume)
            {
                MyAgent.MaterialNaSkladke -= volume;
                return volume;
            }
            double material = MyAgent.MaterialNaSkladke;
            MyAgent.MaterialNaSkladke = 0;
            return material;
        }

        public void NejakaMetoda(MyMessage message)
        {
            Vehicle naNalozenie = null; 
            
            // ak  B ma pracovnu dobu a nenaklada nikoho
            if (!MyAgent.NakladacBIsOccupied && MyAgent.NakladacBIsWorking() && MyAgent.MaterialNaSkladke!=0)
            {
                MyMessage sprava = MyAgent.MessageSkladkaQueue.First.Value;
                MyAgent.MessageSkladkaQueue.RemoveFirst();

                naNalozenie = MyAgent.AutaSkladkaQueue.First.Value;
                MyAgent.AutaSkladkaQueue.RemoveFirst();

                sprava.Addressee = MyAgent.FindAssistant(SimId.ProcesNakladacB);
                MyAgent.NakladacBIsOccupied = true;
                // koniec cakania
                naNalozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naNalozenie.ZaciatokCakania);
                // nalozime mnozstvo ktore je aktualne na skladke
                naNalozenie.RealVolume = (int)LoadCarWith(naNalozenie.Volume);
                // pridanie casu cakania na skladke do statistik

                sprava.Car = naNalozenie;
                MyAgent.SkladkaWStat.AddSample(naNalozenie.CasCakaniaNaSkladke);
                StartContinualAssistant(sprava);
            }
            else if (!MyAgent.NakladacAIsOccupied && MyAgent.NakladacAIsWorking())
            {
                MyMessage sprava = MyAgent.MessageSkladkaQueue.First.Value;
                MyAgent.MessageSkladkaQueue.RemoveFirst();

                naNalozenie = MyAgent.AutaSkladkaQueue.First.Value;
                MyAgent.AutaSkladkaQueue.RemoveFirst();

                sprava.Addressee = MyAgent.FindAssistant(SimId.ProcesNakladacA);
                MyAgent.NakladacAIsOccupied = true;
                // koniec cakania
                naNalozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naNalozenie.ZaciatokCakania);
                // nalozime mnozstvo ktore je aktualne na skladke
                naNalozenie.RealVolume = (int)LoadCarWith(naNalozenie.Volume);
                // pridanie casu cakania na skladke do statistik

                sprava.Car = naNalozenie;
                MyAgent.SkladkaWStat.AddSample(naNalozenie.CasCakaniaNaSkladke);
                StartContinualAssistant(sprava);
            }
        }

        //meta! userInfo="Process messages defined in code", id="0"
        public void ProcessDefault(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.DovozMaterialu:
                    ProcessDovozMaterialu((MyMessage)message);
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
            naplanujZaciatokPrace.Addressee = MyAgent.FindAssistant(SimId.PlanovacPracovnejDoby);
            naplanujZaciatokPrace.Code = Mc.Start;
            StartContinualAssistant(naplanujZaciatokPrace);
        }

        private void ProcessDovozMaterialu(MyMessage message)
        {
            MyAgent.MaterialNaSkladke += message.Volume;
            //
            NejakaMetoda(message);
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        public void Init()
        {
            
        }

        override public void ProcessMessage(MessageForm message)
        {
            switch (message.Code)
            {
                case Mc.Finish:
                    switch (message.Sender.Id)
                    {
                        case SimId.ProcesNakladacA:
                            ProcessFinishProcesNakladacA(message);
                            break;

                        case SimId.ProcesNakladacB:
                            ProcessFinishProcesNakladacB(message);
                            break;
                        case SimId.PlanovacPracovnejDoby:
                            ProcessStartWorkDay(message);
                            break;
                    }
                    break;

                case Mc.NalozAuto:
                    ProcessNalozAuto(message);
                    break;

                default:
                    ProcessDefault(message);
                    break;
            }
        }

        private void ProcessStartWorkDay(MessageForm message)
        {
            Vehicle naNalozenie;
            if (!MyAgent.AutaSkladkaQueue.IsEmpty())
            {
                lock (Constants.QueueLock)
                {
                    naNalozenie = MyAgent.AutaSkladkaQueue.First.Value;
                    MyAgent.AutaSkladkaQueue.RemoveFirst();
                }
                MyMessage zFrontu = MyAgent.MessageSkladkaQueue.First.Value;
                MyAgent.MessageSkladkaQueue.RemoveFirst();

                if (((MyMessage) message).Name == "A")
                {
                    zFrontu.Addressee = MyAgent.FindAssistant(SimId.ProcesNakladacA);
                }
                else
                {
                    zFrontu.Addressee = MyAgent.FindAssistant(SimId.ProcesNakladacB);
                }
                
                zFrontu.Code = Mc.NalozAuto;

                // ukoncenie cakania
                naNalozenie.CasCakaniaNaSkladke = (MySim.CurrentTime - naNalozenie.ZaciatokCakania);
                // pridanie casu cakania na skladke do statistik
                MyAgent.SkladkaWStat.AddSample(naNalozenie.CasCakaniaNaSkladke);

                zFrontu.Car = naNalozenie;
                StartContinualAssistant(zFrontu);
            }
        }

        //meta! tag="end"
        public new AgentSkladky MyAgent
        {
            get
            {
                return (AgentSkladky)base.MyAgent;
            }
        }
    }
}
