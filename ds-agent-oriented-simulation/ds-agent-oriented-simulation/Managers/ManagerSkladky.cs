using ds_agent_oriented_simulation.Agents;
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

		//meta! sender="AgentDopravy", id="36", type="Request"
		public void ProcessNalozAuto(MessageForm message)
		{
		    MyAgent.Queue.AddLast(((MyMessage)message).Car);
		}

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
			}
		}

		//meta! sender="ProcesNakladacA", id="64", type="Finish"
		public void ProcessFinishProcesNakladacA(MessageForm message)
		{
		}

		//meta! sender="ProcesNakladacB", id="70", type="Finish"
		public void ProcessFinishProcesNakladacB(MessageForm message)
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
				case SimId.ProcesNakladacA:
					ProcessFinishProcesNakladacA(message);
				break;

				case SimId.ProcesNakladacB:
					ProcessFinishProcesNakladacB(message);
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