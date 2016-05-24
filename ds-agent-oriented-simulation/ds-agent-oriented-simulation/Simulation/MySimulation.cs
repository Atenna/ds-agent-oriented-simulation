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
        public bool buyUnloader { get; private set; }

        public double ExportSuccessRate { get; set; }
        public MySimulation()
        {
            SeedGenerator = new Random(Constants.Seed);
            Init();

            buyUnloader = false;
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
            //SkladkaWStat = AgentSkladky.SkladkaWStat;
            Console.WriteLine("Koniec replikacie");


            // prida do kumulativnych statistik v Agentovi stavby statistiku z aktualnej replikacie
            ExportSuccessRate = (AgentStavby.PocetUspesnyExport/AgentStavby.PocetExport);
            AgentStavby.OdoberMaterialKumulativny.AddSample(ExportSuccessRate);
            ExportSuccessRate = 0.0;
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
