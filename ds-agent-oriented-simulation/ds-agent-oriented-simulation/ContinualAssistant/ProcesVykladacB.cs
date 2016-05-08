using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.ContinualAssistant
{
	//meta! id="71"
	public class ProcesVykladacB : Process
	{
		public ProcesVykladacB(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentStavby", id="72", type="Start"
		public void ProcessStart(MessageForm message)
		{
            Vehicle naVylozenie = ((MyMessage)message).Car;
		    MyAgent.carAtUnloaderB = naVylozenie;
            double timeOfUnloading = naVylozenie.Volume / Constants.LoadMachine2Performance;
            message.Code = Mc.VylozenieUkoncene;
            Hold(timeOfUnloading, message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.VylozenieUkoncene:
			        MyAgent.carAtUnloaderB = null;
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
		public new AgentStavby MyAgent
		{
			get
			{
				return (AgentStavby)base.MyAgent;
			}
		}
	}
}
