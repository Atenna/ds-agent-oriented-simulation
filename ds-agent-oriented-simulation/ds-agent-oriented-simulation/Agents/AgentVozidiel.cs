using System;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;
using OSPRNG;

namespace ds_agent_oriented_simulation.Agents
{
	//meta! id="57"
	public class AgentVozidiel : Agent
	{
	    Vehicle carA;
        Vehicle carB;
        Vehicle carC;
        Vehicle carD;
        Vehicle carE;
        public AgentVozidiel(int id, OSPABA.Simulation mySim, Agent parent) :
			base(id, mySim, parent)
		{
			Init();
		}

	    public void PrepareCars(Random seedGenerator)
	    {
            carA = new CarA(seedGenerator);
            carB = new CarB(seedGenerator);
            carC = new CarC(seedGenerator);
            carD = new CarD(seedGenerator);
            carE = new CarE(seedGenerator);
        }

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		private void Init()
		{
			new ManagerVozidiel(SimId.ManagerVozidiel, MySim, this);
		}
		//meta! tag="end"
	}
}