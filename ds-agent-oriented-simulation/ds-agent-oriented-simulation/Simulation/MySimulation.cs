using System;
using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities;
using ds_agent_oriented_simulation.Settings;
using OSPStat;

namespace ds_agent_oriented_simulation.Simulation
{
    public class MySimulation : OSPABA.Simulation
    {

        private Statistics _statistics;
        public Random SeedGenerator { get; private set; }
        public WStat SkladkaWStat { get; private set; }
        public bool BuyUnloader { get; private set; }

        public double ExportSuccessRate { get; set; }
        public MySimulation()
        {
            SeedGenerator = new Random(Constants.Seed);
            Init();

            BuyUnloader = false;
            ExportSuccessRate = 0.0;
        }

        protected override void PrepareSimulation()
        {
            base.PrepareSimulation();
            // Create global statistcis
            CurrentRun.initializeCurrentRun();
            _statistics = new Statistics();

            // inicializacia aut
            // 
            //AgentDopravy.PrepareCars(SeedGenerator);
            SkladkaWStat = new WStat(this);
        }

        protected override void PrepareReplication()
        {
            base.PrepareReplication();
            // Reset entities, queues, local statistics, etc...
        }

        protected override void ReplicationFinished()
        {
            // Collect local statistics into global, update UI, etc...
            base.ReplicationFinished();
            //WaitingTimePerCar = AgentSkladky.WaitingTimePerCar;
            //Console.WriteLine("Koniec replikacie");


            // prida do kumulativnych statistik v Agentovi stavby statistiku z aktualnej replikacie
            ExportSuccessRate = (AgentStavby.PocetUspesnyExport/AgentStavby.PocetExport);
            AgentStavby.OdoberMaterialKumulativny.AddSample(ExportSuccessRate);
            ExportSuccessRate = 0.0;

            // Statistiky agenta skladky
            AgentSkladky.WaitingTimeSimulacia.AddSamples(AgentSkladky.WaitingTimePerCar);
            AgentSkladky.RealWorkingTimeASimulacia.AddSamples(AgentSkladky.RealWorkingTimeA);
            AgentSkladky.RealWorkingTimeBSimulacia.AddSamples(AgentSkladky.RealWorkingTimeB);
            AgentSkladky.LengthOfQueueSimulacia.AddSamples(AgentSkladky.LengthOfQueue);

            // Statistiky agenta stavby
            AgentStavby.WaitingTimeSimulacia.AddSamples(AgentStavby.WaitingTimePerCar);
            AgentStavby.RealWorkingTimeASimulacia.AddSamples(AgentStavby.RealWorkingTimeA);
            AgentStavby.RealWorkingTimeBSimulacia.AddSamples(AgentStavby.RealWorkingTimeB);
            AgentStavby.LengthOfQueueSimulacia.AddSamples(AgentStavby.LengthOfQueue);
        }

        protected override void SimulationFinished()
        {
            // Dysplay simulation results
            base.SimulationFinished();
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            AgentModelu = new AgentModelu(SimId.AgentModelu, this, null);
            AgentOkolia = new AgentOkolia(SimId.AgentOkolia, this, AgentModelu);
            AgentDopravy = new AgentDopravy(SimId.AgentDopravy, this, AgentModelu);
            AgentSkladky = new AgentSkladky(SimId.AgentSkladky, this, AgentDopravy);
            AgentStavby = new AgentStavby(SimId.AgentStavby, this, AgentDopravy);
        }
        public AgentModelu AgentModelu
        { get; set; }
        public AgentOkolia AgentOkolia
        { get; set; }
        public AgentDopravy AgentDopravy
        { get; set; }
        public AgentSkladky AgentSkladky
        { get; set; }
        public AgentStavby AgentStavby
        { get; set; }
        //meta! tag="end"
    }
}
