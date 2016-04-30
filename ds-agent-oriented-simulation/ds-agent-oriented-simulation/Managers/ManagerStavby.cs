using ds_agent_oriented_simulation.Agents;
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

		//meta! sender="AgentDopravy", id="37", type="Request"
		public void ProcessPresunNaStavbu(MessageForm message)
		{
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! sender="ProcesVykladacA", id="67", type="Finish"
		public void ProcessFinishProcesVykladacA(MessageForm message)
		{
		}

		//meta! sender="PlanovacOdoberMaterial", id="75", type="Finish"
		public void ProcessFinishPlanovacOdoberMaterial(MessageForm message)
		{
		}

		//meta! sender="ProcesVykladacB", id="72", type="Finish"
		public void ProcessFinishProcesVykladacB(MessageForm message)
		{
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
				case SimId.ProcesVykladacA:
					ProcessFinishProcesVykladacA(message);
				break;

				case SimId.PlanovacOdoberMaterial:
					ProcessFinishPlanovacOdoberMaterial(message);
				break;

				case SimId.ProcesVykladacB:
					ProcessFinishProcesVykladacB(message);
				break;
				}
			break;

			case Mc.PresunNaStavbu:
				ProcessPresunNaStavbu(message);
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