using System;
using System.IO;
using System.Windows.Forms;
using ds_agent_oriented_simulation.Simulation;

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
            

            //Combiner.Combine();

            //CarConfig();
        }

        public static void CarConfig()
        {

            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            string line = "";

            System.IO.StreamReader file = new System.IO.StreamReader("configurationA.txt");

            while ((line = file.ReadLine()) != null)
            {
                int[] SelectedCars = ToIntArray(line, ',');
                MySimulation ms = new MySimulation();
                ms.AgentModelu.SelectedCars = SelectedCars;
                ms.SetMaxSimSpeed();
                ms.Simulate(5, 788400);

                using (StreamWriter w = File.AppendText("resultsConfigurationA.txt"))
                {
                    w.WriteLine(line + ", " +
                    ms.AgentSkladky.WaitingTimeSimulacia.Mean().ToString() + ", " +
                    ms.AgentSkladky.LengthOfQueueSimulacia.Mean().ToString() + ", " +
                    (ms.AgentSkladky.WaitingTimeSimulacia.Mean() / SelectedCars.Length).ToString() + ", " +
                    ms.AgentSkladky.RealWorkingTimeASimulacia.Mean().ToString() + ", " +
                    ms.AgentSkladky.RealWorkingTimeBSimulacia.Mean().ToString() + ", " +

                    "<"+ ms.AgentSkladky.WaitingTimeSimulacia.ConfidenceInterval90[0].ToString() + ";" + ms.AgentSkladky.WaitingTimeSimulacia.ConfidenceInterval90[1].ToString() + ">" + ", " +
                    "<" + ms.AgentSkladky.LengthOfQueueSimulacia.ConfidenceInterval90[0].ToString() + ";" + ms.AgentSkladky.LengthOfQueueSimulacia.ConfidenceInterval90[1].ToString() + ">" + ", " +
                    "<" + ms.AgentSkladky.RealWorkingTimeASimulacia.ConfidenceInterval90[0].ToString() + ";" + ms.AgentSkladky.RealWorkingTimeASimulacia.ConfidenceInterval90[1].ToString() + ">" + ", " +
                    "<" + ms.AgentSkladky.RealWorkingTimeBSimulacia.ConfidenceInterval90[0].ToString() + ";" + ms.AgentSkladky.RealWorkingTimeBSimulacia.ConfidenceInterval90[1].ToString() + ">" + ", " +

                    ms.AgentStavby.WaitingTimeSimulacia.Mean().ToString() + ", " +
                    ms.AgentStavby.LengthOfQueueSimulacia.Mean().ToString() + ", " +
                    (ms.AgentStavby.WaitingTimeSimulacia.Mean() / SelectedCars.Length).ToString() + ", " +
                    ms.AgentStavby.RealWorkingTimeASimulacia.Mean().ToString() + ", " +
                    //ms.AgentStavby.RealWorkingTimeBSimulacia.Mean().ToString("P") + ", " +

                    "<" + ms.AgentStavby.WaitingTimeSimulacia.ConfidenceInterval90[0].ToString() + ";" + ms.AgentStavby.WaitingTimeSimulacia.ConfidenceInterval90[1].ToString() + ">" + ", " +
                    "<" + ms.AgentStavby.LengthOfQueueSimulacia.ConfidenceInterval90[0].ToString() + ";" + ms.AgentStavby.LengthOfQueueSimulacia.ConfidenceInterval90[1].ToString() + ">" + ", " +
                    "<" + ms.AgentStavby.RealWorkingTimeASimulacia.ConfidenceInterval90[0].ToString() + ";" + ms.AgentStavby.RealWorkingTimeASimulacia.ConfidenceInterval90[1].ToString() + ">" + ", "


                    /*
                    if (!ms.AgentStavby.VykladacBIsDisabled)
                    {
                        "<"+ms.AgentStavby.RealWorkingTimeBSimulacia.ConfidenceInterval90[0].ToString("####.00") + ";" + ms.AgentStavby.RealWorkingTimeBSimulacia.ConfidenceInterval90[1].ToString("####.00") + ">" + ", "
                    }
                    */
                    )
                ;
            }



                /*

                using (StreamWriter w = File.AppendText("results.txt"))
                {
                    w.WriteLine(line + ", " + ms.AgentStavby.OdoberMaterialKumulativny.Mean() + " ");
                }

                using (StreamWriter w = File.AppendText("resultsTop.txt"))
                {
                    if (ms.AgentStavby.OdoberMaterialKumulativny.Mean() >= 0.95)
                    {
                        w.WriteLine(line + " " + ms.AgentStavby.OdoberMaterialKumulativny.Mean() + ms.AgentModelu.CostOfVehicles().ToString("C") + " ");
                    }
                }
                */
            }

            file.Close();
        }

        static int[] ToIntArray(this string value, char separator)
        {
            return Array.ConvertAll(value.Split(separator), s => int.Parse(s));
        }
    }
}
