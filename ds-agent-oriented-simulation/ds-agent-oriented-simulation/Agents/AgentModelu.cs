using ds_agent_oriented_simulation.Managers;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;
using OSPABA;

namespace ds_agent_oriented_simulation.Agents
{
    //meta! id="13"
    public class AgentModelu : Agent
    {
        public int[] SelectedCars { get; set; }

        public AgentModelu(int id, OSPABA.Simulation mySim, Agent parent) :
            base(id, mySim, parent)
        {
            Init();
        }

        override public void PrepareReplication()
        {
            base.PrepareReplication();
            // Setup component for the next replication


            //SelectedCars = FormAgentSimulation.SelectedCars;
            MyMessage sprava = new MyMessage(MySim, SelectedCars);

            /* Hard setup of cars
            int[] selectedCars = new int[5];
            selectedCars[0] = 1;
            selectedCars[1] = 1;
            selectedCars[2] = 1;
            selectedCars[3] = 1;
            selectedCars[4] = 1;
            */

            //int[] selectedCars = new int[5] { 0, 20, 0, 0, 0 };

            // MyMessage sprava = new MyMessage(MySim, selectedCars);


            sprava.Code = Mc.Inicializacia;
            sprava.Addressee = MySim.FindAgent(SimId.AgentModelu);
            MyManager.Call(sprava);
        }

        public double CostOfVehicles()
        {
            double cost = 0;
            cost = SelectedCars[0] * Constants.PriceCarA
                          + SelectedCars[1] * Constants.PriceCarB
                          + SelectedCars[2] * Constants.PriceCarC
                          + SelectedCars[3] * Constants.PriceCarD
                          + SelectedCars[4] * Constants.PriceCarE;
            return cost;
        }

        //meta! userInfo="Generated code: do not modify", tag="begin"
        private void Init()
        {
            //SelectedCars = Program.CarConfig();
            new ManagerModelu(SimId.ManagerModelu, MySim, this);
            AddOwnMessage(Mc.OdvozMaterialu);
            AddOwnMessage(Mc.DovozMaterialu);
        }
        //meta! tag="end"
    }
}
