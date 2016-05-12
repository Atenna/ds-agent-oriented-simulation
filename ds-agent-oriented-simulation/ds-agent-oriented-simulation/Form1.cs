using System;
using System.Drawing;
using System.Windows.Forms;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;

namespace ds_agent_oriented_simulation
{
    public partial class FormAgentSimulation : Form
    {
        public MySimulation Sim { get; private set; }
        public FormAgentSimulation()
        {
            InitializeComponent();
            InitializeToolTips();
        }

        private Color ArrowHoverButtonsColor { get; set; }
        private Color ArrowButtonsColor { get; set; }


        private void InitializeToolTips()
        {
            ToolTip toolTipOverCars = new ToolTip();

            // Set up the delays for the ToolTip.
            toolTipOverCars.AutoPopDelay = 5000;
            toolTipOverCars.InitialDelay = 1000;
            toolTipOverCars.ReshowDelay = 500;

            // Force the ToolTip text to be displayed whether or not the form is active.
            toolTipOverCars.ShowAlways = true;
            toolTipOverCars.IsBalloon = true;

            // Set up the ToolTip text for the Button and Checkbox.
            toolTipOverCars.SetToolTip(this.pictureCarA, "Volume: " + Constants.VolumeOfVehicleA + ", Speed: " + Constants.SpeedOfVehicleA);
            toolTipOverCars.SetToolTip(this.pictureCarB, "Volume: " + Constants.VolumeOfVehicleB + ", Speed: " + Constants.VolumeOfVehicleB);
            toolTipOverCars.SetToolTip(this.pictureCarC, "Volume: " + Constants.VolumeOfVehicleC + ", Speed: " + Constants.VolumeOfVehicleC);
            toolTipOverCars.SetToolTip(this.pictureCarD, "Volume: " + Constants.VolumeOfVehicleD + ", Speed: " + Constants.VolumeOfVehicleD);
            toolTipOverCars.SetToolTip(this.pictureCarE, "Volume: " + Constants.VolumeOfVehicleE + ", Speed: " + Constants.VolumeOfVehicleE);
        }


        private void buttonRun_Click(object sender, System.EventArgs e)
        {
            // simulation start
            int userSeed = 0;
            if (userSeed > 0 || textBoxSeed.Text != "")
            {
                Constants.Seed = userSeed;
            }
            else
            {
                Constants.Seed = 0;
            }

            System.Action<MySimulation> updateGuiAction = new System.Action<MySimulation>((s) => UpdateGui(s));
            Sim = new MySimulation();
            Sim.SetSimSpeed(0.5, 2);
            Sim.OnRefreshUI(s => this.Invoke(updateGuiAction, s));
            Sim.SimulateAsync(1);
        }

        private void UpdateGui(MySimulation mySimulation)
        {
            this.labelSimTime.Text = "Simulation time: " + Sim.CurrentTime.ToString("#.000");
            this.labelQueueLoad.Text = "Queue at Loader: ";
            if (Sim.AgentSkladky.AutaSkladkaQueue != null && Sim.AgentSkladky.AutaSkladkaQueue.First != null)
            {
                foreach (var vehicle in Sim.AgentSkladky.AutaSkladkaQueue)
                {
                    labelQueueLoad.Text += vehicle.Name + " ";
                }
                 
            }
            this.labelLoaderA.Text = Sim.AgentSkladky.CarAtLoaderA != null ? "Loads Car: " + Sim.AgentSkladky.CarAtLoaderA.ToString() : "Loads Car: empty";
            this.labelLoaderB.Text = Sim.AgentSkladky.CarAtLoaderB != null ? "Loads Car: " + Sim.AgentSkladky.CarAtLoaderB.ToString() : "Loads Car: empty";
            this.labelUnloaderA.Text = Sim.AgentStavby.CarAtUnloaderA != null ? "Unloads Car: " + Sim.AgentStavby.CarAtUnloaderA.ToString() : "Unloads Car: empty";
            this.labelUnloaderB.Text = Sim.AgentStavby.CarAtUnloaderB != null ? "Unloads Car: " + Sim.AgentStavby.CarAtUnloaderB.ToString() : "Unloads Car: empty";

            this.labelQueueUnload.Text = "Queue at Unloader: ";
            if (Sim.AgentStavby.AutaStavbaQueue != null && Sim.AgentStavby.AutaStavbaQueue.First != null)
            {
                foreach (var vehicle in Sim.AgentStavby.AutaStavbaQueue)
                {
                    labelQueueUnload.Text += vehicle.Name + " ";
                }

            }

            this.labelMaterialSkladka.Text = Sim.AgentSkladky.MaterialNaSkladke.ToString("####.0");
            this.labelMaterialStavba.Text = Sim.AgentStavby.MaterialNaStavbe.ToString("####.0");
        }

        private void buttonSlowUp_Click(object sender, System.EventArgs e)
        {
            Sim.SetSimSpeed(0.5,0.2);
        }

        private void buttonPause_Click(object sender, System.EventArgs e)
        {
            if (Sim.IsPaused())
            {
                Sim.ResumeSimulation();
            }
            else
            {
                Sim.PauseSimulation();
            }
        }

        private void buttonStop_Click(object sender, System.EventArgs e)
        {
            Sim.StopSimulation();
            resetGUI();
        }

        private void buttonSlowDown_Click(object sender, System.EventArgs e)
        {
            Sim.SetSimSpeed(0.2, 10);
        }

        private void resetGUI()
        {
            this.labelSimTime.Text = "Simulation time: ";
            this.labelQueueLoad.Text = "Queue at Loader: ";

            this.labelLoaderA.Text = "Loads Car: ";
            this.labelLoaderB.Text = "Loads Car: ";
            this.labelUnloaderA.Text = "Unloads Car: ";
            this.labelUnloaderB.Text = "Unloads Car: ";
            this.labelQueueLoad.Text = "Queue at Unoader: ";
        }
    }
}
