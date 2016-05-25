using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ds_agent_oriented_simulation.Entities;
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

            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormAgentSimulation());
            */

            //Combiner.Combine();
            CarConfig();
        }

        public static void CarConfig()
        {
            string line = "";

            System.IO.StreamReader file = new System.IO.StreamReader("configuration.txt");

            while ((line = file.ReadLine()) != null)
            {
                int[] SelectedCars = ToIntArray(line, ',');
                MySimulation ms = new MySimulation();
                ms.AgentModelu.SelectedCars = SelectedCars;
                ms.SetMaxSimSpeed();
                ms.Simulate(1, 788400);

                using (StreamWriter w = File.AppendText("results.txt"))
                {
                    w.WriteLine(line + " " + ms.AgentStavby.OdoberMaterialKumulativny.Mean().ToString("P") + " ");
                }

                using (StreamWriter w = File.AppendText("resultsTop.txt"))
                {
                    if (ms.AgentStavby.OdoberMaterialKumulativny.Mean() >= 0.95)
                    {
                        w.WriteLine(line + " " + ms.AgentStavby.OdoberMaterialKumulativny.Mean().ToString("P") + ms.AgentModelu.CostOfVehicles().ToString("C") + " ");
                    }
                }
            }

            file.Close();
        }

        static int[] ToIntArray(this string value, char separator)
        {
            return Array.ConvertAll(value.Split(separator), s => int.Parse(s));
        }
    }
}
