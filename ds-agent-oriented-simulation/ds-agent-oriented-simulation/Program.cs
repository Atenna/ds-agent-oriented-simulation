using System;
using System.Windows.Forms;
using ds_agent_oriented_simulation.Simulation;
using MathNet.Numerics;

namespace ds_agent_oriented_simulation
{
    static class Program
    {
        public static int[] CarSetup;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            /*
            int[] SelectedCars = new int[]{0,20,0,0,0};
            
            MySimulation ms = new MySimulation();
            ms.SetMaxSimSpeed();
            ms.Simulate(1, 788400);
            */
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormAgentSimulation());
            
        }

        public static int[] CarConfig()
        {
            CarSetup = new int[5];

            for (int i = 0; i < 4; i++)
            {
                CarSetup[i] = 1;
            }

            return new[] {1, 2};
        }
    }
}
