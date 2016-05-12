using ds_agent_oriented_simulation.ContinualAssistant;
using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Agents
{
    //meta! id="15"
    public class AgentOkolia : Agent
    {
        public AgentOkolia(int id, OSPABA.Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            new ManagerOkolia(SimId.ManagerOkolia, MySim, this);
            new PlanovacOdvozMaterialu(SimId.PlanovacOdvozMaterialu, MySim, this);
            new PlanovacDovozMaterialu(SimId.PlanovacDovozMaterialu, MySim, this);
            AddOwnMessage(Mc.Inicializacia);
            AddOwnMessage(Mc.DovozMaterialu);
            AddOwnMessage(Mc.ExportUkonceny);
        }
        //meta! tag="end"
    }
}
