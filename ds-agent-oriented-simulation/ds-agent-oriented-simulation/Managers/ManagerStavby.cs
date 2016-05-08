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
        private MyMessage requestCopyMessage;
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
		public void ProcessVylozAuto(MessageForm message)
		{
            Vehicle naVylozenie = ((MyMessage)message).Car;

		    requestCopyMessage = (MyMessage) message.CreateCopy();
		    requestCopyMessage.Car = naVylozenie;

            // to-do
		    double volumeToUnload = naVylozenie.RealVolume;
            
            if (MyAgent.VykladacAIsWorking && MyAgent.VykladacBIsWorking)
            {
                MyAgent.AutaStavbaQueue.AddLast(naVylozenie);
            }
            else
            {
                if (MyAgent.VykladacAIsWorking)
                {
                    message.Addressee = ((AgentStavby)MyAgent).ProcesVykladacB;
                    MyAgent.VykladacBIsWorking = true;
                    StartContinualAssistant(message);
                }
                else
                {
                    message.Addressee = ((AgentStavby)MyAgent).ProcesVykladacA;
                    MyAgent.VykladacAIsWorking = true;
                    StartContinualAssistant(message);
                }
                
            }
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
		    MyAgent.VykladacAIsWorking = false;
            Response(requestCopyMessage);
		}

		//meta! sender="PlanovacOdoberMaterial", id="75", type="Finish"
		public void ProcessFinishPlanovacOdoberMaterial(MessageForm message)
		{
		   
		}

		//meta! sender="ProcesVykladacB", id="72", type="Finish"
		public void ProcessFinishProcesVykladacB(MessageForm message)
		{
            MyAgent.VykladacBIsWorking = false;
            Response(requestCopyMessage);
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
				case SimId.ProcesVykladacB:
					ProcessFinishProcesVykladacB(message);
				break;

				case SimId.ProcesVykladacA:
					ProcessFinishProcesVykladacA(message);
				break;

				case SimId.PlanovacOdoberMaterial:
					ProcessFinishPlanovacOdoberMaterial(message);
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