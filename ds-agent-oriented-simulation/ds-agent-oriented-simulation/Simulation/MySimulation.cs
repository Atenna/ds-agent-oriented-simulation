using OSPABA;
using agents;
namespace simulation
{
	public class MySimulation : Simulation
	{
		public MySimulation()
		{
			Init();
		}

		override public void PrepareSimulation()
		{
			base.PrepareSimulation();
			// Create global statistcis
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Reset entities, queues, local statistics, etc...
		}

		override public void ReplicationFinished()
		{
			// Collect local statistics into global, update UI, etc...
			base.ReplicationFinished();
		}

		override public void SimulationFinished()
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
			AgentPrejazdu = new AgentPrejazdu(SimId.AgentPrejazdu, this, AgentDopravy);
			AgentSkladky = new AgentSkladky(SimId.AgentSkladky, this, AgentDopravy);
			AgentStavby = new AgentStavby(SimId.AgentStavby, this, AgentDopravy);
		}
		public AgentModelu AgentModelu
		{ get; set; }
		public AgentOkolia AgentOkolia
		{ get; set; }
		public AgentDopravy AgentDopravy
		{ get; set; }
		public AgentPrejazdu AgentPrejazdu
		{ get; set; }
		public AgentSkladky AgentSkladky
		{ get; set; }
		public AgentStavby AgentStavby
		{ get; set; }
		//meta! tag="end"
	}
}
