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
        public static int[] SelectedCars { get; private set; }
        public FormAgentSimulation()
        {
            InitializeComponent();
            InitializeToolTips();
            SelectedCars = new int[5];
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
            toolTipOverCars.SetToolTip(this.pictureCarB, "Volume: " + Constants.VolumeOfVehicleB + ", Speed: " + Constants.SpeedOfVehicleB);
            toolTipOverCars.SetToolTip(this.pictureCarC, "Volume: " + Constants.VolumeOfVehicleC + ", Speed: " + Constants.SpeedOfVehicleC);
            toolTipOverCars.SetToolTip(this.pictureCarD, "Volume: " + Constants.VolumeOfVehicleD + ", Speed: " + Constants.SpeedOfVehicleD);
            toolTipOverCars.SetToolTip(this.pictureCarE, "Volume: " + Constants.VolumeOfVehicleE + ", Speed: " + Constants.SpeedOfVehicleE);
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
            // 777 600
            Sim.SimulateAsync(1, 100);
        }

        private void UpdateGui(MySimulation mySimulation)
        {
            this.labelSimTime.Text = "Simulation time: " + Sim.CurrentTime.ToString("#.000");
            this.labelQueueLoad.Text = "Queue at Loader: ";
            if (Sim.AgentSkladky.AutaSkladkaQueue != null && Sim.AgentSkladky.AutaSkladkaQueue.First != null)
            {
                foreach (var vehicle in Sim.AgentSkladky.AutaSkladkaQueue)
                {
                    this.labelQueueLoad.Text += vehicle.Name + " ";
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

            this.labelQueueLoaderStats.Text = "Queue at Loader: " + mySimulation.AgentSkladky.SkladkaWStat.Mean();
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pictureUnloaderB.Image = ds_agent_oriented_simulation.Properties.Resources.l_Vykladac_A;
                pictureUnloaderB.SizeMode = PictureBoxSizeMode.StretchImage;
                labelUnloaderB.ForeColor = Color.Black;
            }
            else
            {
                pictureUnloaderB.Image = ds_agent_oriented_simulation.Properties.Resources.l_Vykladac_A_grey;
                pictureUnloaderB.SizeMode = PictureBoxSizeMode.StretchImage;
                labelUnloaderB.ForeColor = Color.DarkGray;
            }
        }

        private void buttonSaveSettings_Click(object sender, EventArgs e)
        {
            // save settings
            SelectedCars[0] = (int) numericUpDown1.Value;
            SelectedCars[1] = (int) numericUpDown2.Value;
            SelectedCars[2] = (int) numericUpDown3.Value;
            SelectedCars[3] = (int) numericUpDown4.Value;
            SelectedCars[4] = (int) numericUpDown5.Value;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown1.Value > 0)
            {
                checkBoxCarA.Checked = true;
            }
            else
            {
                checkBoxCarA.Checked = false;
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown2.Value > 0)
            {
                checkBoxCarB.Checked = true;
            }
            else
            {
                checkBoxCarB.Checked = false;
            }
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown3.Value > 0)
            {
                checkBoxCarC.Checked = true;
            }
            else
            {
                checkBoxCarC.Checked = false;
            }
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown4.Value > 0)
            {
                checkBoxCarD.Checked = true;
            }
            else
            {
                checkBoxCarD.Checked = false;
            }
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (numericUpDown5.Value > 0)
            {
                checkBoxCarE.Checked = true;
            }
            else
            {
                checkBoxCarE.Checked = false;
            }
        }
    }
}
