using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.ContinualAssistant
{
	//meta! id="69"
	public class ProcesNakladacB : Process
	{
		public ProcesNakladacB(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentSkladky", id="70", type="Start"
		public void ProcessStart(MessageForm message)
		{
            Vehicle naNalozenie = ((MyMessage)message).Car;
            MyAgent.carAtLoaderB = naNalozenie;
            double timeOfLoading = naNalozenie.Volume / Constants.LoadMachine2Performance;
            message.Code = Mc.NalozenieUkoncene;
            Hold(timeOfLoading, message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.NalozenieUkoncene:
                    MyAgent.carAtLoaderB = null;
                    AssistantFinished(message);
                break;
            }
		}

		//meta! userInfo="Generated code: do not modify", tag="begin"
		override public void ProcessMessage(MessageForm message)
		{
			switch (message.Code)
			{
			case Mc.Start:
				ProcessStart(message);
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
