using ds_agent_oriented_simulation.ContinualAssistant;
using ds_agent_oriented_simulation.Entities;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;
using OSPDataStruct;
using OSPStat;

namespace ds_agent_oriented_simulation.Agents
{
    //meta! id="17"
    public class AgentSkladky : Agent
    {

        public SimQueue<Vehicle> AutaSkladkaQueue { get; private set; }
        public SimQueue<MyMessage> MessageSkladkaQueue { get; private set; }
        public Stat SkladkaWStat { get; internal set; }
        public Vehicle CarAtLoaderA { get; set; }
        public Vehicle CarAtLoaderB { get; set; }
        public WStat LengthOfQueue { get; internal set; }
        public double MaterialNaSkladke { get; set; }
        public double MaterialNaStavbe { get; set; }
        public bool NakladacAIsOccupied { get; set; }
        public bool NakladacBIsOccupied { get; set; }

        public double Material;

        public PlanovacPracovnejDoby PlanovacPracovnejDoby { get; set; }

        public AgentSkladky(int id, OSPABA.Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
            SkladkaWStat = new Stat();
            LengthOfQueue = new WStat(mySim);
            AutaSkladkaQueue = new SimQueue<Vehicle>(LengthOfQueue);
            MessageSkladkaQueue = new SimQueue<MyMessage>();
            MaterialNaSkladke = Constants.MaterialAtDepo;
            Material = Settings.Constants.MaterialToLoad;
            NakladacAIsOccupied = false;
            NakladacBIsOccupied = false;
        }

        public override void PrepareReplication()
        {
            base.PrepareReplication();
            // odoberie vozidla z radu
            if (!AutaSkladkaQueue.IsEmpty())
            {
                AutaSkladkaQueue.Dequeue();
            }
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            new ManagerSkladky(SimId.ManagerSkladky, MySim, this);
            PlanovacPracovnejDoby = new PlanovacPracovnejDoby(SimId.PlanovacPracovnejDoby, MySim, this);
            //PlanovacPracovnejDoby.PrepareReplication();
            new ProcesNakladacA(SimId.ProcesNakladacA, MySim, this);
            new ProcesNakladacB(SimId.ProcesNakladacB, MySim, this);
            AddOwnMessage(Mc.NalozAuto);
            AddOwnMessage(Mc.NalozenieUkoncene);
            AddOwnMessage(Mc.DovozMaterialu);
            AddOwnMessage(Mc.ZaciatokPracovnejDoby);
            AddOwnMessage(Mc.KoniecPracovnejDoby);
        }
        //meta! tag="end"

        public bool NakladacAIsWorking()
        {
            return Timer.IsWorking(MySim.CurrentTime, Constants.NakladacAStartsAt, Constants.NakladacAEndsAt);
        }

        public bool NakladacBIsWorking()
        {
            return Timer.IsWorking(MySim.CurrentTime, Constants.NakladacBStartsAt, Constants.NakladacBEndsAt);
        }
    }
}
