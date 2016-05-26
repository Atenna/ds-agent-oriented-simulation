using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ds_agent_oriented_simulation.Agents;
using ds_agent_oriented_simulation.Entities.Vehicles;
using ds_agent_oriented_simulation.Settings;
using ds_agent_oriented_simulation.Simulation;

namespace ds_agent_oriented_simulation
{
    public partial class FormAgentSimulation : Form
    {
        public MySimulation Sim { get; private set; }
        public static int[] SelectedCars { get; private set; }
        public static int NumberOfReplications { get; private set; }
        public static long GeneratorSeed { get; private set; }

        private Vehicle _selectedVehicle;

        private static decimal _costForCars;
        public static decimal CostForUnloaders;
        private double _exportRate;
        private double _interval;
        private readonly double _duration;
        private bool _first;

        private double storageAStat = 0;
        private double storageBStat = 0;
        private int count = 0;
        private double maxA = 0;
        private double minA = 90000;
        private double maxB = 0;
        private double minB = 90000;

        public static bool UnloaderBDisabled { get; private set; }

        public FormAgentSimulation()
        {
            InitializeComponent();
            InitializeToolTips();
            SelectedCars = new int[5];
            NumberOfReplications = 1;
            _costForCars = 0;
            CostForUnloaders = 0;
            GeneratorSeed = 22;
            UnloaderBDisabled = true;
            _exportRate = 0.0;
            _interval = 0.5;
            _duration = 2;
            chart1.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart1.ChartAreas[0].AxisY.LabelStyle.Enabled = false;

            chart1.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            chart1.ChartAreas[0].AxisY.MinorTickMark.Enabled = false;

            chart3.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
            chart3.ChartAreas[0].AxisY.LabelStyle.Enabled = false;

            chart3.ChartAreas[0].AxisY.MajorTickMark.Enabled = false;
            chart3.ChartAreas[0].AxisY.MinorTickMark.Enabled = false;
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
            toolTipOverCars.SetToolTip(this.pictureCarA,
                "Volume: " + Constants.VolumeOfVehicleA + ", Speed: " + Constants.SpeedOfVehicleA);
            toolTipOverCars.SetToolTip(this.pictureCarB,
                "Volume: " + Constants.VolumeOfVehicleB + ", Speed: " + Constants.SpeedOfVehicleB);
            toolTipOverCars.SetToolTip(this.pictureCarC,
                "Volume: " + Constants.VolumeOfVehicleC + ", Speed: " + Constants.SpeedOfVehicleC);
            toolTipOverCars.SetToolTip(this.pictureCarD,
                "Volume: " + Constants.VolumeOfVehicleD + ", Speed: " + Constants.SpeedOfVehicleD);
            toolTipOverCars.SetToolTip(this.pictureCarE,
                "Volume: " + Constants.VolumeOfVehicleE + ", Speed: " + Constants.SpeedOfVehicleE);
        }


        private void buttonRun_Click(object sender, System.EventArgs e)
        {
            try
            {
                NumberOfReplications = Int32.Parse(TextBoxReplications.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter a valid number of replications", "Invalid format", MessageBoxButtons.OK);
                return;
            }

            try
            {
                GeneratorSeed = Int32.Parse(textBoxSeed.Text);
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
            }
            catch (Exception ex)
            {

                MessageBox.Show("Enter a valid generator seed", "Invalid format", MessageBoxButtons.OK);
                return;
            }
            _first = true;

            DisableChanges();

            Sim = new MySimulation();
            Sim.SetSimSpeed(_interval, _duration);
            Sim.AgentModelu.SelectedCars = SelectedCars;

            System.Action<MySimulation> updateGuiAction = new System.Action<MySimulation>((s) => UpdateGui(s));
            System.Action<MySimulation> ReplicationFinished = new Action<MySimulation>((s) => UpdateUIAfterReplication());
            System.Action<MySimulation> SimulationFinished = new Action<MySimulation>((s) => UpdateUIAfterSimulation());
            Sim.OnReplicationDidFinish(simulation => simulation.InvokeAsync(UpdateUIAfterReplication));
            Sim.OnSimulationDidFinish(simulation => simulation.InvokeAsync(UpdateUIAfterSimulation));

            try
            {
                Sim.OnRefreshUI(s => this.Invoke(updateGuiAction, s));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            // 777 600
            Sim.SimulateAsync(NumberOfReplications, 788400);

            DrawChart();
            System.Action<MySimulation> enableChangesAction = new Action<MySimulation>((s) => EnableChanges());
            // nefunguje
            //Sim.OnSimulationDidFinish(s => this.Invoke(enableChangesAction));
        }

        private void SetupTracing()
        {
            List<Vehicle> vehicles = ((AgentDopravy) Sim.FindAgent(SimId.AgentDopravy)).EnabledCars;
            _selectedVehicle = vehicles[0];
            comboBoxTracking.DataSource = vehicles;
            comboBoxTracking.DisplayMember = "Name";
            comboBoxTracking.ValueMember = "Name";
        }

        private void DisableChanges()
        {
            checkBox1.Enabled = false;
            checkBoxCarA.Enabled = false;
            checkBoxCarB.Enabled = false;
            checkBoxCarC.Enabled = false;
            checkBoxCarD.Enabled = false;
            checkBoxCarE.Enabled = false;
            TextBoxReplications.Enabled = false;
            textBoxSeed.Enabled = false;

            buttonMaxSpeed.Enabled = true;
            buttonPause.Enabled = true;
            buttonSlowDown.Enabled = true;
            buttonSlowUp.Enabled = true;
            buttonStop.Enabled = true;
        }

        private void EnableChanges()
        {
            checkBox1.Enabled = true;
            checkBoxCarA.Enabled = true;
            checkBoxCarB.Enabled = true;
            checkBoxCarC.Enabled = true;
            checkBoxCarD.Enabled = true;
            checkBoxCarE.Enabled = true;
            TextBoxReplications.Enabled = true;
            textBoxSeed.Enabled = true;

            buttonMaxSpeed.Enabled = false;
            buttonPause.Enabled = false;
            buttonSlowDown.Enabled = false;
            buttonSlowUp.Enabled = false;
            buttonStop.Enabled = false;
        }

        private void DrawChart()
        {

        }

        private void UpdateGui(MySimulation mySimulation)
        {
            
            if (!this.checkBoxVizual.Checked)
            {
                this.labelSimTime.Text = "Simulation time: " + Sim.CurrentTime.ToString("#.000");
                this.labelReplication.Text = "Replication: " + (Sim.CurrentReplication + 1);
                this.labelSimTime.Text = "Simulation time: " + Sim.CurrentTime.ToString("#.000");
            }
            else
            {
                this.labelSimTime.Text = "Simulation time: " + Sim.CurrentTime.ToString("#.000");
                this.labelReplication.Text = "Replication: " + Sim.CurrentReplication;
                this.labelQueueLoad.Text = "Queue at Loader: ";
                if (Sim.AgentSkladky.AutaSkladkaQueue != null && Sim.AgentSkladky.AutaSkladkaQueue.First != null)
                {
                    foreach (var vehicle in Sim.AgentSkladky.AutaSkladkaQueue)
                    {
                        this.labelQueueLoad.Text += vehicle.Name + " ";
                    }

                }

                this.labelLoaderA.Text = !Sim.AgentSkladky.NakladacAIsWorking()
                    ? "Loads Car: Not working"
                    : Sim.AgentSkladky.CarAtLoaderA != null
                        ? ("Loads Car: " + Sim.AgentSkladky.CarAtLoaderA.Name + ": [" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderA)[0].ToString("##.0") + "/" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderA)[1] + "] ")
                        : "Loads Car: Empty";
                this.labelLoaderB.Text = !Sim.AgentSkladky.NakladacBIsWorking()
                    ? "Loads Car: Not working"
                    : Sim.AgentSkladky.CarAtLoaderB != null
                        ? ("Loads Car: " + Sim.AgentSkladky.CarAtLoaderB.Name + ": [" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderB)[0].ToString("##.0") + "/" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderB)[1] + "] ")
                        : "Loads Car: Empty";
                this.labelUnloaderA.Text = !Sim.AgentStavby.VykladacAIsWorking()
                    ? "Unloads Car: Not working"
                    : Sim.AgentStavby.CarAtUnloaderA != null
                        ? ("Unloads Car: " + Sim.AgentStavby.CarAtUnloaderA.Name + ": [" +
                           GetProgressOfUnloading(Sim.AgentStavby.CarAtUnloaderA)[0].ToString("##.0") + "/" +
                           GetProgressOfLoading(Sim.AgentStavby.CarAtUnloaderA)[1] + "] ")
                        : "Unloads Car: Empty";
                this.labelUnloaderB.Text = Sim.AgentStavby.VykladacBIsDisabled
                    ? "Unloads Car: Disabled"
                    : !Sim.AgentStavby.VykladacBIsWorking()
                        ? "Unloads Car: Not working"
                        : Sim.AgentStavby.CarAtUnloaderB != null
                            ? ("Unloads Car: " + Sim.AgentStavby.CarAtUnloaderB.Name + ": [" +
                               GetProgressOfUnloading(Sim.AgentStavby.CarAtUnloaderB)[0].ToString("##.0") + "/" +
                               GetProgressOfLoading(Sim.AgentStavby.CarAtUnloaderB)[1] + "] ")
                            : "Unloads Car: Empty";

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

                this.labelLoaderStatsTime.Text = "Average waiting time: " +
                                                 Sim.AgentSkladky.WaitingTimePerCar.Mean().ToString("####.00");
                this.labelLoaderStatsLen.Text = "Average length of queue: " +
                                                Sim.AgentSkladky.LengthOfQueue.Mean().ToString("####.00");
                this.labelUnloaderStatsTime.Text = "Average waiting time: " +
                                                   Sim.AgentStavby.WaitingTimePerCar.Mean().ToString("####.00");
                this.labelUnloaderStatsLen.Text = "Average length of queue: " +
                                                  Sim.AgentStavby.LengthOfQueue.Mean().ToString("####.00");
            }

            labelTotalAttempts.Text = "Total attempts: " + Sim.AgentStavby.PocetExport;
            if (Sim.AgentStavby.PocetExport > 0)
            {
                _exportRate = Sim.AgentStavby.PocetUspesnyExport/Sim.AgentStavby.PocetExport;
                labelExportRate.Text = "Successful export rate: " + _exportRate.ToString("P");
            }
            if (Sim.ReplicationCount > 1 && Sim.AgentStavby.OdoberMaterialKumulativny.SampleSize > 1)
            {
                labelConfInterval.Text = "Confidence interval: <" +
                                         Sim.AgentStavby.OdoberMaterialKumulativny.ConfidenceInterval95[0].ToString("P") +
                                         ", " +
                                         mySimulation.AgentStavby.OdoberMaterialKumulativny.ConfidenceInterval95[1]
                                             .ToString("P") + ">";
            }

            //mySimulation.AgentSkladky.UsageLoaderA.

            if (movingPicture.Location.X < 280)
            {
                movingPicture.Location = new Point(
                    movingPicture.Location.X + 5,
                    this.movingPicture.Location.Y);
            }

            if (_first)
            {
                SetupTracing();
                _first = false;
            }

            labelUsageLA.Text = "Usage A: " + Sim.AgentSkladky.RealWorkingTimeA.Mean().ToString("p");
            labelUsageLB.Text = "Usage B: " + Sim.AgentSkladky.RealWorkingTimeB.Mean().ToString("p");
            labelUsageUA.Text = "Usage A: " + Sim.AgentStavby.RealWorkingTimeA.Mean().ToString("p");
            labelUsageUB.Text = "Usage B: " + Sim.AgentStavby.RealWorkingTimeB.Mean().ToString("p");


            storageAStat += Sim.AgentSkladky.MaterialNaSkladke;
            storageBStat += Sim.AgentSkladky.MaterialNaStavbe;
            count++;
            if (maxA < storageAStat / count)
            {
                maxA = storageAStat / count;
            }
            if (maxB < storageBStat / count)
            {
                maxB = storageBStat / count;
            }
            if (minA > storageAStat / count)
            {
                minA = storageAStat / count;
            }
            if (minB > storageBStat / count)
            {
                minB = storageBStat / count;
            }
            chart1.ChartAreas[0].AxisY.Maximum = maxA + 30;
            chart1.ChartAreas[0].AxisY.Minimum = minA - 30;
            chart1.Series["StorageA"].Points.AddXY(Sim.CurrentTime, storageAStat / count);
            chart3.ChartAreas[0].AxisY.Maximum = maxB + 30;
            chart3.ChartAreas[0].AxisY.Minimum = minB - 30;
            chart3.Series["StorageB"].Points.AddXY(Sim.CurrentTime, storageBStat / count);

            UpdateCarInfo(_selectedVehicle);
            UpdateSimulationStats(Sim);
        }

        private void UpdateSimulationStats(MySimulation sim)
        {
            labelSimLoaderWaiting.Text = "Average waiting time: " + sim.AgentSkladky.WaitingTimeSimulacia.Mean().ToString("####.00");
            labelSimLoaderQueue.Text = "Average length of queue: " + sim.AgentSkladky.LengthOfQueueSimulacia.Mean().ToString("####.00");
            labelSimLoadWaitingCar.Text = "Average waiting time per car: " + (sim.AgentSkladky.WaitingTimeSimulacia.Mean()/SelectedCars.Length).ToString("####.00");
            labelSimUsageLA.Text = "Usage A: " + sim.AgentSkladky.RealWorkingTimeASimulacia.Mean().ToString("P");
            labelSimUsageLB.Text = "Usage B: " + sim.AgentSkladky.RealWorkingTimeBSimulacia.Mean().ToString("P");

            labelSimUnloaderWaiting.Text = "Average waiting time: " + sim.AgentStavby.WaitingTimeSimulacia.Mean().ToString("####.00");
            labelSimUnloaderQueue.Text = "Average length of queue: " + sim.AgentStavby.LengthOfQueueSimulacia.Mean().ToString("####.00");
            labelSimUnloadWaitingTimeCar.Text = "Average waiting time per car: " + (sim.AgentStavby.WaitingTimeSimulacia.Mean() / SelectedCars.Length).ToString("####.00");
            labelSimUsageUA.Text = "Usage A: " + sim.AgentStavby.RealWorkingTimeASimulacia.Mean().ToString("P");
            labelSimUsageUB.Text = "Usage B: " + sim.AgentStavby.RealWorkingTimeBSimulacia.Mean().ToString("P");


            if (sim.AgentSkladky.WaitingTimeSimulacia.SampleSize > 2 && sim.AgentSkladky.RealWorkingTimeBSimulacia.SampleSize > 2 &&
                sim.AgentStavby.LengthOfQueueSimulacia.SampleSize > 2 && sim.AgentSkladky.LengthOfQueueSimulacia.SampleSize > 2)
            {

                labelCILavg.Text = "Confidence interval: <" + sim.AgentSkladky.WaitingTimeSimulacia.ConfidenceInterval90[0].ToString("####.00") + ";" +sim.AgentSkladky.WaitingTimeSimulacia.ConfidenceInterval90[1].ToString("####.00") +">";
                labelCIUlength.Text = "Confidence interval: <" +sim.AgentSkladky.LengthOfQueueSimulacia.ConfidenceInterval90[0].ToString("####.00") +";" +sim.AgentSkladky.LengthOfQueueSimulacia.ConfidenceInterval90[1].ToString("####.00") +">";
                labelCILA.Text = "Confidence interval: <" +sim.AgentSkladky.RealWorkingTimeASimulacia.ConfidenceInterval90[0].ToString("P") +";" + sim.AgentSkladky.RealWorkingTimeASimulacia.ConfidenceInterval90[1].ToString("P") +">";
                labelCILB.Text = "Confidence interval: <" +sim.AgentSkladky.RealWorkingTimeBSimulacia.ConfidenceInterval90[0].ToString("P") +";" +sim.AgentSkladky.RealWorkingTimeBSimulacia.ConfidenceInterval90[1].ToString("P") +">";

                labelCIUavg.Text = "Confidence interval: <" +sim.AgentStavby.WaitingTimeSimulacia.ConfidenceInterval90[0].ToString("####.00") +";" +sim.AgentStavby.WaitingTimeSimulacia.ConfidenceInterval90[1].ToString("####.00") +">";
                labelCILlength.Text = "Confidence interval: <" +sim.AgentStavby.LengthOfQueueSimulacia.ConfidenceInterval90[0].ToString("####.00") +";" +sim.AgentStavby.LengthOfQueueSimulacia.ConfidenceInterval90[1].ToString("####.00") +">";
                labelCIUA.Text = "Confidence interval: <" +sim.AgentStavby.RealWorkingTimeASimulacia.ConfidenceInterval90[0].ToString("P") +";" +sim.AgentStavby.RealWorkingTimeASimulacia.ConfidenceInterval90[1].ToString("P") +">";
            }

            if (!sim.AgentStavby.VykladacBIsDisabled)
            {
                labelCIUB.Text = "Confidence interval: <" +sim.AgentStavby.RealWorkingTimeBSimulacia.ConfidenceInterval90[0].ToString("####.00") +";" + sim.AgentStavby.RealWorkingTimeBSimulacia.ConfidenceInterval90[1].ToString("####.00") +">";
            }
        }

        public void UpdateCarInfo(Vehicle car)
        {
            Invoke((MethodInvoker) delegate
            {
                labelPosition.Text = "Position: " + car?.Position;
                labelAction.Text = "Activity: " + car?.Activity;
                labelVolume.Text = "Volume: " + car?.RealVolume + "/" + car?.Volume;
                labelProgres.Text = "Progress: " + car?.Progress;
                labelWaiting.Text = "Waiting: " + car?.Waiting;
            });
        }

        public double[] GetProgressOfLoading(Vehicle car)
        {
            double[] pole = new double[2];
            pole[0] = (Sim.CurrentTime - car.ZaciatokNakladania)*Constants.LoadMachinePerformance;
            pole[1] = car.Volume;
            return pole;
        }

        public double[] GetProgressOfUnloading(Vehicle car)
        {
            double[] pole = new double[2];
            pole[0] = (Sim.CurrentTime - car.ZaciatokVykladania)*Constants.LoadMachinePerformance;
            pole[1] = car.Volume;
            return pole;
        }


        private void buttonSpeedUp_Click(object sender, System.EventArgs e)
        {
            _interval += 1;
            //duration -= 0.001;
            Sim.SetSimSpeed(_interval, 0.1);
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

            Sim.StopReplication();
            Sim.StopSimulation();
            EnableChanges();
            ResetGui();
        }

        private void buttonSlowDown_Click(object sender, System.EventArgs e)
        {
            if (_interval - 1 >= 0)
            {
                _interval -= 1;
                //duration += 0.2;
                Sim.SetSimSpeed(_interval, 0.1);
            }
        }

        private void ResetGui()
        {
            this.labelSimTime.Text = "Simulation time: ";
            this.labelQueueLoad.Text = "Queue at Loader: ";

            this.labelLoaderA.Text = "Loads Car: ";
            this.labelLoaderB.Text = "Loads Car: ";
            this.labelUnloaderA.Text = "Unloads Car: ";
            this.labelUnloaderB.Text = "Unloads Car: ";
            this.labelQueueLoad.Text = "Queue at Unloader: ";
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                pictureUnloaderB.Image = ds_agent_oriented_simulation.Properties.Resources.l_Vykladac_A;
                pictureUnloaderB.SizeMode = PictureBoxSizeMode.StretchImage;
                labelUnloaderB.ForeColor = Color.Black;
                UnloaderBDisabled = false;
                labelCostUnloaders.Text = "Cost for unloaders: " + Constants.PriceForSecondUnloader.ToString("c");
            }
            else
            {
                pictureUnloaderB.Image = ds_agent_oriented_simulation.Properties.Resources.l_Vykladac_A_grey;
                pictureUnloaderB.SizeMode = PictureBoxSizeMode.StretchImage;
                labelUnloaderB.ForeColor = Color.DarkGray;
                UnloaderBDisabled = true;
                labelCostUnloaders.Text = "Cost for unloaders: ";
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
                _costForCars += Constants.PriceCarA;
                ChangeCostOfVehicles();
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
                _costForCars += Constants.PriceCarB;
                ChangeCostOfVehicles();
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
                _costForCars += Constants.PriceCarC;
                ChangeCostOfVehicles();
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
                _costForCars += Constants.PriceCarD;
                ChangeCostOfVehicles();
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
                _costForCars += Constants.PriceCarE;
                ChangeCostOfVehicles();
            }
            else
            {
                checkBoxCarE.Checked = false;
            }
        }

        private void ChangeCostOfVehicles()
        {
            _costForCars = 0;
            labelCostVehicles.Text = "Cost for vehicles: ";
            _costForCars = numericUpDown1.Value*Constants.PriceCarA
                           + numericUpDown2.Value*Constants.PriceCarB
                           + numericUpDown3.Value*Constants.PriceCarC
                           + numericUpDown4.Value*Constants.PriceCarD
                           + numericUpDown5.Value*Constants.PriceCarE;
            labelCostVehicles.Text += _costForCars.ToString("C");
        }

        private void checkBoxVizual_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxVizual.Enabled)
            {
                // if not null
                if (Sim != null)
                {
                    Sim.SetMaxSimSpeed();
                }
            }
        }

        private void checkBoxCarA_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCarA.Checked)
            {
                numericUpDown1.Value = 1;
            }
            else
            {
                numericUpDown1.Value = 0;
            }
            ChangeCostOfVehicles();
        }

        private void checkBoxCarB_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCarB.Checked)
            {
                numericUpDown2.Value = 1;
            }
            else
            {
                numericUpDown2.Value = 0;
            }
            ChangeCostOfVehicles();
        }

        private void checkBoxCarC_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCarC.Checked)
            {
                numericUpDown3.Value = 1;
            }
            else
            {
                numericUpDown3.Value = 0;
            }
            ChangeCostOfVehicles();
        }

        private void checkBoxCarD_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCarD.Checked)
            {
                numericUpDown4.Value = 1;
            }
            else
            {
                numericUpDown4.Value = 0;
            }
            ChangeCostOfVehicles();
        }

        private void checkBoxCarE_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCarE.Checked)
            {
                numericUpDown5.Value = 1;
            }
            else
            {
                numericUpDown5.Value = 0;
            }
            ChangeCostOfVehicles();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sim.SetMaxSimSpeed();
        }

        private void UpdateUIAfterReplication()
        {
            UpdateCarInfo(_selectedVehicle);
            Invoke((MethodInvoker) delegate
            {
                this.labelSimTime.Text = "Simulation time: " + Sim.CurrentTime.ToString("#.000");
                this.labelReplication.Text = "Replication: " + (Sim.CurrentReplication + 1);
                this.labelQueueLoad.Text = "Queue at Loader: ";
                if (Sim.AgentSkladky.AutaSkladkaQueue != null && Sim.AgentSkladky.AutaSkladkaQueue.First != null)
                {
                    foreach (var vehicle in Sim.AgentSkladky.AutaSkladkaQueue)
                    {
                        this.labelQueueLoad.Text += vehicle.Name + " ";
                    }
                }

                this.labelLoaderA.Text = !Sim.AgentSkladky.NakladacAIsWorking()
                    ? "Loads Car: Not working"
                    : Sim.AgentSkladky.CarAtLoaderA != null
                        ? ("Loads Car: " + Sim.AgentSkladky.CarAtLoaderA.Name + ": [" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderA)[0].ToString("##.0") + "/" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderA)[1] + "] ")
                        : "Loads Car: Empty";
                this.labelLoaderB.Text = !Sim.AgentSkladky.NakladacBIsWorking()
                    ? "Loads Car: Not working"
                    : Sim.AgentSkladky.CarAtLoaderB != null
                        ? ("Loads Car: " + Sim.AgentSkladky.CarAtLoaderB.Name + ": [" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderB)[0].ToString("##.0") + "/" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderB)[1] + "] ")
                        : "Loads Car: Empty";
                this.labelUnloaderA.Text = !Sim.AgentStavby.VykladacAIsWorking()
                    ? "Unloads Car: Not working"
                    : Sim.AgentStavby.CarAtUnloaderA != null
                        ? ("Unloads Car: " + Sim.AgentStavby.CarAtUnloaderA.Name + ": [" +
                           GetProgressOfUnloading(Sim.AgentStavby.CarAtUnloaderA)[0].ToString("##.0") + "/" +
                           GetProgressOfLoading(Sim.AgentStavby.CarAtUnloaderA)[1] + "] ")
                        : "Unloads Car: Empty";
                this.labelUnloaderB.Text = Sim.AgentStavby.VykladacBIsDisabled
                    ? "Unloads Car: Disabled"
                    : !Sim.AgentStavby.VykladacBIsWorking()
                        ? "Unloads Car: Not working"
                        : Sim.AgentStavby.CarAtUnloaderB != null
                            ? ("Unloads Car: " + Sim.AgentStavby.CarAtUnloaderB.Name + ": [" +
                               GetProgressOfUnloading(Sim.AgentStavby.CarAtUnloaderB)[0].ToString("##.0") + "/" +
                               GetProgressOfLoading(Sim.AgentStavby.CarAtUnloaderB)[1] + "] ")
                            : "Unloads Car: Empty";

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

                this.labelLoaderStatsTime.Text = "Average waiting time: " +
                                                 Sim.AgentSkladky.WaitingTimePerCar.Mean().ToString("####.00");
                this.labelLoaderStatsLen.Text = "Average length of queue: " +
                                                Sim.AgentSkladky.LengthOfQueue.Mean().ToString("####.00");
                this.labelUnloaderStatsTime.Text = "Average waiting time: " +
                                                   Sim.AgentStavby.WaitingTimePerCar.Mean().ToString("####.00");
                this.labelUnloaderStatsLen.Text = "Average length of queue: " +
                                                  Sim.AgentStavby.LengthOfQueue.Mean().ToString("####.00");


                labelTotalAttempts.Text = "Total attempts: " + Sim.AgentStavby.PocetExport;
                if (Sim.AgentStavby.PocetExport > 0)
                {
                    _exportRate = Sim.AgentStavby.PocetUspesnyExport/Sim.AgentStavby.PocetExport;
                    labelExportRate.Text = "Successful export rate: " + _exportRate.ToString("P");
                }
                if (Sim.ReplicationCount > 1 && Sim.AgentStavby.OdoberMaterialKumulativny.SampleSize >= 2)
                {
                    labelConfInterval.Text = "Confidence interval: <" +
                                             Sim.AgentStavby.OdoberMaterialKumulativny.ConfidenceInterval95[0]
                                                 .ToString("P") + ", " +
                                             Sim.AgentStavby.OdoberMaterialKumulativny.ConfidenceInterval95[1]
                                                 .ToString("P") + ">";
                }

                //mySimulation.AgentSkladky.UsageLoaderA.

                if (movingPicture.Location.X < 280)
                {
                    movingPicture.Location = new Point(
                        movingPicture.Location.X + 5,
                        this.movingPicture.Location.Y);
                }

                if (_first)
                {
                    SetupTracing();
                    _first = false;
                }

                labelUsageLA.Text = "Usage A: " + Sim.AgentSkladky.RealWorkingTimeA.Mean().ToString("p");
                labelUsageLB.Text = "Usage B: " + Sim.AgentSkladky.RealWorkingTimeB.Mean().ToString("p");
                labelUsageUA.Text = "Usage A: " + Sim.AgentStavby.RealWorkingTimeA.Mean().ToString("p");
                labelUsageUB.Text = "Usage B: " + Sim.AgentStavby.RealWorkingTimeB.Mean().ToString("p");

                storageAStat = 0;
                storageBStat = 0;
                count = 0;
                maxA = 0;
                minA = 9000;
                maxB = 0;
                minB = 9000;
                chart1.Series["StorageA"].Points.Clear();
                chart3.Series["StorageB"].Points.Clear();

            });
        }

        private void UpdateUIAfterSimulation()
        {
            EnableChanges();
            Invoke((MethodInvoker) delegate
            {
                this.labelSimTime.Text = "Simulation time: " + Sim.CurrentTime.ToString("#.000");
                this.labelQueueLoad.Text = "Queue at Loader: ";
                this.labelReplication.Text = "Replication: " + (Sim.CurrentReplication + 1);
                if (Sim.AgentSkladky.AutaSkladkaQueue != null && Sim.AgentSkladky.AutaSkladkaQueue.First != null)
                {
                    foreach (var vehicle in Sim.AgentSkladky.AutaSkladkaQueue)
                    {
                        this.labelQueueLoad.Text += vehicle.Name + " ";
                    }
                }

                this.labelLoaderA.Text = !Sim.AgentSkladky.NakladacAIsWorking()
                    ? "Loads Car: Not working"
                    : Sim.AgentSkladky.CarAtLoaderA != null
                        ? ("Loads Car: " + Sim.AgentSkladky.CarAtLoaderA.Name + ": [" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderA)[0].ToString("##.0") + "/" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderA)[1] + "] ")
                        : "Loads Car: Empty";
                this.labelLoaderB.Text = !Sim.AgentSkladky.NakladacBIsWorking()
                    ? "Loads Car: Not working"
                    : Sim.AgentSkladky.CarAtLoaderB != null
                        ? ("Loads Car: " + Sim.AgentSkladky.CarAtLoaderB.Name + ": [" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderB)[0].ToString("##.0") + "/" +
                           GetProgressOfLoading(Sim.AgentSkladky.CarAtLoaderB)[1] + "] ")
                        : "Loads Car: Empty";
                this.labelUnloaderA.Text = !Sim.AgentStavby.VykladacAIsWorking()
                    ? "Unloads Car: Not working"
                    : Sim.AgentStavby.CarAtUnloaderA != null
                        ? ("Unloads Car: " + Sim.AgentStavby.CarAtUnloaderA.Name + ": [" +
                           GetProgressOfUnloading(Sim.AgentStavby.CarAtUnloaderA)[0].ToString("##.0") + "/" +
                           GetProgressOfLoading(Sim.AgentStavby.CarAtUnloaderA)[1] + "] ")
                        : "Unloads Car: Empty";
                this.labelUnloaderB.Text = Sim.AgentStavby.VykladacBIsDisabled
                    ? "Unloads Car: Disabled"
                    : !Sim.AgentStavby.VykladacBIsWorking()
                        ? "Unloads Car: Not working"
                        : Sim.AgentStavby.CarAtUnloaderB != null
                            ? ("Unloads Car: " + Sim.AgentStavby.CarAtUnloaderB.Name + ": [" +
                               GetProgressOfUnloading(Sim.AgentStavby.CarAtUnloaderB)[0].ToString("##.0") + "/" +
                               GetProgressOfLoading(Sim.AgentStavby.CarAtUnloaderB)[1] + "] ")
                            : "Unloads Car: Empty";

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

                this.labelLoaderStatsTime.Text = "Average waiting time: " +
                                                 Sim.AgentSkladky.WaitingTimePerCar.Mean().ToString("####.00");
                this.labelLoaderStatsLen.Text = "Average length of queue: " +
                                                Sim.AgentSkladky.LengthOfQueue.Mean().ToString("####.00");
                this.labelUnloaderStatsTime.Text = "Average waiting time: " +
                                                   Sim.AgentStavby.WaitingTimePerCar.Mean().ToString("####.00");
                this.labelUnloaderStatsLen.Text = "Average length of queue: " +
                                                  Sim.AgentStavby.LengthOfQueue.Mean().ToString("####.00");


                labelTotalAttempts.Text = "Total attempts: " + Sim.AgentStavby.PocetExport;
                if (Sim.AgentStavby.PocetExport > 0)
                {
                    _exportRate = Sim.AgentStavby.PocetUspesnyExport/Sim.AgentStavby.PocetExport;
                    labelExportRate.Text = "Successful export rate: " + _exportRate.ToString("P");
                }
                if (Sim.ReplicationCount > 1 && Sim.AgentStavby.OdoberMaterialKumulativny.SampleSize >= 2)
                {
                    labelConfInterval.Text = "Confidence interval: <" +
                                             Sim.AgentStavby.OdoberMaterialKumulativny.ConfidenceInterval95[0]
                                                 .ToString("P") + ", " +
                                             Sim.AgentStavby.OdoberMaterialKumulativny.ConfidenceInterval95[1]
                                                 .ToString("P") + ">";
                }

                //mySimulation.AgentSkladky.UsageLoaderA.

                if (movingPicture.Location.X < 280)
                {
                    movingPicture.Location = new Point(
                        movingPicture.Location.X + 5,
                        this.movingPicture.Location.Y);
                }

                if (_first)
                {
                    SetupTracing();
                    _first = false;
                }

                labelUsageLA.Text = "Usage A: " + Sim.AgentSkladky.RealWorkingTimeA.Mean().ToString("p");
                labelUsageLB.Text = "Usage B: " + Sim.AgentSkladky.RealWorkingTimeB.Mean().ToString("p");
                labelUsageUA.Text = "Usage A: " + Sim.AgentStavby.RealWorkingTimeA.Mean().ToString("p");
                labelUsageUB.Text = "Usage B: " + Sim.AgentStavby.RealWorkingTimeB.Mean().ToString("p");
            });
        }

        private void comboBoxTracking_SelectedIndexChanged(object sender, EventArgs e)
        {
            _selectedVehicle = (Vehicle) comboBoxTracking.SelectedItem;
            UpdateCarInfo(_selectedVehicle);
        }
    }
}