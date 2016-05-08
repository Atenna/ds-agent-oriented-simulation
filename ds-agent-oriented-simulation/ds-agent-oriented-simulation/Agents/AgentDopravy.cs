using System;
using ds_agent_oriented_simulation.ContinualAssistant;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.InstantAssistant;
using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;
using OSPDataStruct;

namespace ds_agent_oriented_simulation.Agents
{
	//meta! id="19"
	public class AgentDopravy : Agent
	{
	    public Vehicle A { get; private set; }
	    public Vehicle B { get; private set; }
	    public Vehicle C { get; private set; }
	    public Vehicle D { get; private set; }
	    public Vehicle E { get; private set; }

        public ProcesCestaNaPrejazd ProcesCestaNaPrejazd { get; private set; }
        public ProcesCestaNaSkladku ProcesCestaNaSkladku { get; private set; }
        public ProcesCestaNaStavbu ProcesCestaNaStavbu { get; private set; }

        public AgentDopravy(int id, OSPABA.Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

        public void PrepareCars(Random seedGenerator)
        {
            A = new CarA(seedGenerator);
            B = new CarB(seedGenerator);
            C = new CarC(seedGenerator);
            D = new CarD(seedGenerator);
            E = new CarE(seedGenerator);
        }

        override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerDopravy(SimId.ManagerDopravy, MySim, this);
			//new PoradcaDopravy(SimId.PoradcaDopravy, MySim, this);
		    ProcesCestaNaPrejazd = new ProcesCestaNaPrejazd(SimId.ProcesCestaNaPrejazd, MySim, this);
            ProcesCestaNaSkladku = new ProcesCestaNaSkladku(SimId.ProcesCestaNaSkladku, MySim, this);
            ProcesCestaNaStavbu = new ProcesCestaNaStavbu(SimId.ProcesCestaNaStavbu, MySim, this);
			AddOwnMessage(Mc.Inicializacia);
			AddOwnMessage(Mc.NalozAuto);
			AddOwnMessage(Mc.VylozAuto);
            AddOwnMessage(Mc.PrejazdUkonceny);
		}
		//meta! tag="end"
	}
}