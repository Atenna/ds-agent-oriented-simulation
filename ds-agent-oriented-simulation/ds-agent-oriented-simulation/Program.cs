using System;
using System.Windows.Forms;
using ds_agent_oriented_simulation.Simulation;

namespace ds_agent_oriented_simulation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            MySimulation sim = new MySimulation();
            sim.Simulate(1);
        }
    }
}
