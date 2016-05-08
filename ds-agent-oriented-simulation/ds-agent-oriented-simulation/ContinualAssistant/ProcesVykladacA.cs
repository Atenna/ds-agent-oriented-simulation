using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.ContinualAssistant
{
	//meta! id="66"
	public class ProcesVykladacA : Process
	{
		public ProcesVykladacA(int id, OSPABA.Simulation mySim, CommonAgent myAgent) :
			base(id, mySim, myAgent)
		{
		}

		override public void PrepareReplication()
		{
			base.PrepareReplication();
			// Setup component for the next replication
		}

		//meta! sender="AgentStavby", id="67", type="Start"
		public void ProcessStart(MessageForm message)
		{
            Vehicle naVylozenie = ((MyMessage)message).Car;
		    MyAgent.carAtUnloaderA = naVylozenie;
            double timeOfUnloading = naVylozenie.Volume / Constants.LoadMachinePerformance;
            message.Code = Mc.VylozenieUkoncene;

		    naVylozenie.RealVolume = 0;
            Hold(timeOfUnloading, message);
        }

		//meta! userInfo="Process messages defined in code", id="0"
		public void ProcessDefault(MessageForm message)
		{
			switch (message.Code)
			{
                case Mc.VylozenieUkoncene:
			        MyAgent.carAtUnloaderA = null;
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
