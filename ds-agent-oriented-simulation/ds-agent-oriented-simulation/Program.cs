using System;
using System.Windows.Forms;
using ds_agent_oriented_simulation.Simulation;
using MathNet.Numerics;

namespace ds_agent_oriented_simulation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            /*int[] SelectedCars = new int[]{0,20,0,0,0};
            
            MySimulation ms = new MySimulation();
            ms.Simulate(1, 788400);*/

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormAgentSimulation());

            //MySimulation Sim = new MySimulation();
            //Sim.SetMaxSimSpeed();
            //Sim.Simulate(1, 800000);
        }
    }
}
