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
            InitializeColorSchema();
        }

        private Color ArrowHoverButtonsColor { get; set; }
        private Color ArrowButtonsColor { get; set; }

        private void InitializeColorSchema()
        {
            ArrowHoverButtonsColor = Color.LightGray;
            removeCarE.BackColor = Color.White;
        }

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

        private void ArrowRemoveCarEHover(object sender, System.EventArgs e)
        {
            removeCarE.BackColor = ArrowHoverButtonsColor;
        }

        private void ArrowRemoveCarELeave(object sender, System.EventArgs e)
        {
            removeCarE.BackColor = ArrowButtonsColor;
        }

        private void ArrowAddCarEHover(object sender, System.EventArgs e)
        {
            addCarE.BackColor = ArrowHoverButtonsColor;
        }

        private void ArrowAddCarELeave(object sender, System.EventArgs e)
        {
            addCarE.BackColor = ArrowButtonsColor;
        }

        private void ArrowRemoveCarAHover(object sender, System.EventArgs e)
        {
            removeCarA.BackColor = ArrowHoverButtonsColor;
        }

        private void ArrowRemoveCarALeave(object sender, System.EventArgs e)
        {
            removeCarA.BackColor = ArrowButtonsColor;
        }

        private void ArrowAddCarAHover(object sender, System.EventArgs e)
        {
            addCarA.BackColor = ArrowHoverButtonsColor;
        }

        private void ArrowAddCarALeave(object sender, System.EventArgs e)
        {
            addCarA.BackColor = ArrowButtonsColor;
        }

        private void AddCarEClick(object sender, System.EventArgs e)
        {
            if (CurrentRun.CarE && CurrentRun.CarsE < Constants.MaxNumberOfCarsE && CurrentRun.CarsE > 0)
            {
                CurrentRun.CarsE++;
                labelCarsE.Text = CurrentRun.CarsE + "/" + Constants.MaxNumberOfCarsE;
            }
            else if(!CurrentRun.CarE)
            {
                CurrentRun.CarE = true;
                checkBoxCarE.Checked = true;
                CurrentRun.CarsE++;
                labelCarsE.Text = CurrentRun.CarsE + "/" + Constants.MaxNumberOfCarsE;
            }
        }

        private void RemoveCarEClick(object sender, System.EventArgs e)
        {
            if (CurrentRun.CarE && CurrentRun.CarsE > 1 && CurrentRun.CarsE <= Constants.MaxNumberOfCarsE)
            {
                CurrentRun.CarsE--;
                labelCarsE.Text = CurrentRun.CarsE + "/" + Constants.MaxNumberOfCarsE;
            }
            else if (CurrentRun.CarsE == 1)
            {
                CurrentRun.CarE = false;
                checkBoxCarE.Checked = false;
                CurrentRun.CarsE = 0;
                labelCarsE.Text = CurrentRun.CarsE + "/" + Constants.MaxNumberOfCarsE;
            }
        }

        private void IsUsedCarEChanged(object sender, System.EventArgs e)
        {
            if (checkBoxCarE.Checked)
            {
                AddCarEClick(sender,e);
                CurrentRun.CarE = true;
            }
            else
            {
                CurrentRun.CarsE = 0;
                CurrentRun.CarE = false;
                labelCarsE.Text = CurrentRun.CarsE + "/" + Constants.MaxNumberOfCarsE;
            }
        }

        private void addCarA_Click(object sender, System.EventArgs e)
        {
            if (CurrentRun.CarA && CurrentRun.CarsA < Constants.MaxNumberOfCarsA && CurrentRun.CarsA > 0)
            {
                CurrentRun.CarsA++;
                labelCarsA.Text = CurrentRun.CarsA + "/" + Constants.MaxNumberOfCarsA;
            }
            else if (!CurrentRun.CarA)
            {
                CurrentRun.CarA = true;
                checkBoxCarA.Checked = true;
                CurrentRun.CarsA++;
                labelCarsA.Text = CurrentRun.CarsA + "/" + Constants.MaxNumberOfCarsA;
            }
        }

        private void removeCarA_Click(object sender, System.EventArgs e)
        {
            if (CurrentRun.CarA && CurrentRun.CarsA > 1 && CurrentRun.CarsA <= Constants.MaxNumberOfCarsA)
            {
                CurrentRun.CarsA--;
                labelCarsA.Text = CurrentRun.CarsA + "/" + Constants.MaxNumberOfCarsA;
            }
            else if (CurrentRun.CarsA == 1)
            {
                CurrentRun.CarA = false;
                checkBoxCarA.Checked = false;
                CurrentRun.CarsA = 0;
                labelCarsA.Text = CurrentRun.CarsA + "/" + Constants.MaxNumberOfCarsA;
            }
        }

        private void checkBoxCarA_CheckedChanged(object sender, System.EventArgs e)
        {
            if (checkBoxCarA.Checked)
            {
                addCarA_Click(sender, e);
                CurrentRun.CarA = true;
            }
            else
            {
                CurrentRun.CarsA = 0;
                CurrentRun.CarA = false;
                labelCarsA.Text = CurrentRun.CarsA + "/" + Constants.MaxNumberOfCarsA;
            }
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
            Sim.SetSimSpeed(0.5, 2);
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
