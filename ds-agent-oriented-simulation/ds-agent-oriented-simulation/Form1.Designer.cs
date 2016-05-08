namespace ds_agent_oriented_simulation
{
    partial class FormAgentSimulation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAgentSimulation));
            this.groupBoxVehicles = new System.Windows.Forms.GroupBox();
            this.removeCarE = new System.Windows.Forms.Label();
            this.addCarE = new System.Windows.Forms.Label();
            this.removeCarA = new System.Windows.Forms.Label();
            this.addCarA = new System.Windows.Forms.Label();
            this.labelCarsE = new System.Windows.Forms.Label();
            this.labelCarsA = new System.Windows.Forms.Label();
            this.checkBoxCarE = new System.Windows.Forms.CheckBox();
            this.pictureCarD = new System.Windows.Forms.PictureBox();
            this.pictureCarE = new System.Windows.Forms.PictureBox();
            this.checkBoxCarD = new System.Windows.Forms.CheckBox();
            this.pictureCarC = new System.Windows.Forms.PictureBox();
            this.checkBoxCarC = new System.Windows.Forms.CheckBox();
            this.pictureCarB = new System.Windows.Forms.PictureBox();
            this.pictureCarA = new System.Windows.Forms.PictureBox();
            this.checkBoxCarB = new System.Windows.Forms.CheckBox();
            this.checkBoxCarA = new System.Windows.Forms.CheckBox();
            this.groupBoxSetup = new System.Windows.Forms.GroupBox();
            this.textBoxSeed = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxVizual = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelReplications = new System.Windows.Forms.Label();
            this.TextBoxReplications = new System.Windows.Forms.TextBox();
            this.groupBoxSim = new System.Windows.Forms.GroupBox();
            this.labelSimTime = new System.Windows.Forms.Label();
            this.labelQueueUnload = new System.Windows.Forms.Label();
            this.labelQueueLoad = new System.Windows.Forms.Label();
            this.labelUnloaderB = new System.Windows.Forms.Label();
            this.labelUnloaderA = new System.Windows.Forms.Label();
            this.labelLoaderB = new System.Windows.Forms.Label();
            this.labelLoaderA = new System.Windows.Forms.Label();
            this.pictureUnloaderB = new System.Windows.Forms.PictureBox();
            this.pictureUnloaderA = new System.Windows.Forms.PictureBox();
            this.pictureLoaderB = new System.Windows.Forms.PictureBox();
            this.pictureLoaderA = new System.Windows.Forms.PictureBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonSlowUp = new System.Windows.Forms.Button();
            this.buttonRun = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.buttonSlowDown = new System.Windows.Forms.Button();
            this.groupBoxStats = new System.Windows.Forms.GroupBox();
            this.groupBoxVehicles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCarD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCarE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCarC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCarB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCarA)).BeginInit();
            this.groupBoxSetup.SuspendLayout();
            this.groupBoxSim.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUnloaderB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUnloaderA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLoaderB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLoaderA)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxVehicles
            // 
            this.groupBoxVehicles.BackColor = System.Drawing.Color.White;
            this.groupBoxVehicles.Controls.Add(this.removeCarE);
            this.groupBoxVehicles.Controls.Add(this.addCarE);
            this.groupBoxVehicles.Controls.Add(this.removeCarA);
            this.groupBoxVehicles.Controls.Add(this.addCarA);
            this.groupBoxVehicles.Controls.Add(this.labelCarsE);
            this.groupBoxVehicles.Controls.Add(this.labelCarsA);
            this.groupBoxVehicles.Controls.Add(this.checkBoxCarE);
            this.groupBoxVehicles.Controls.Add(this.pictureCarD);
            this.groupBoxVehicles.Controls.Add(this.pictureCarE);
            this.groupBoxVehicles.Controls.Add(this.checkBoxCarD);
            this.groupBoxVehicles.Controls.Add(this.pictureCarC);
            this.groupBoxVehicles.Controls.Add(this.checkBoxCarC);
            this.groupBoxVehicles.Controls.Add(this.pictureCarB);
            this.groupBoxVehicles.Controls.Add(this.pictureCarA);
            this.groupBoxVehicles.Controls.Add(this.checkBoxCarB);
            this.groupBoxVehicles.Controls.Add(this.checkBoxCarA);
            this.groupBoxVehicles.Location = new System.Drawing.Point(12, 12);
            this.groupBoxVehicles.Name = "groupBoxVehicles";
            this.groupBoxVehicles.Size = new System.Drawing.Size(355, 444);
            this.groupBoxVehicles.TabIndex = 1;
            this.groupBoxVehicles.TabStop = false;
            this.groupBoxVehicles.Text = "Vehicles";
            // 
            // removeCarE
            // 
            this.removeCarE.AutoSize = true;
            this.removeCarE.BackColor = System.Drawing.Color.White;
            this.removeCarE.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.removeCarE.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.removeCarE.Location = new System.Drawing.Point(305, 403);
            this.removeCarE.Name = "removeCarE";
            this.removeCarE.Size = new System.Drawing.Size(22, 22);
            this.removeCarE.TabIndex = 15;
            this.removeCarE.Text = "˅";
            this.removeCarE.Click += new System.EventHandler(this.RemoveCarEClick);
            this.removeCarE.MouseLeave += new System.EventHandler(this.ArrowRemoveCarELeave);
            this.removeCarE.MouseHover += new System.EventHandler(this.ArrowRemoveCarEHover);
            // 
            // addCarE
            // 
            this.addCarE.AutoSize = true;
            this.addCarE.BackColor = System.Drawing.Color.White;
            this.addCarE.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.addCarE.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addCarE.Location = new System.Drawing.Point(305, 370);
            this.addCarE.Name = "addCarE";
            this.addCarE.Size = new System.Drawing.Size(22, 22);
            this.addCarE.TabIndex = 14;
            this.addCarE.Text = "˄";
            this.addCarE.Click += new System.EventHandler(this.AddCarEClick);
            this.addCarE.MouseLeave += new System.EventHandler(this.ArrowAddCarELeave);
            this.addCarE.MouseHover += new System.EventHandler(this.ArrowAddCarEHover);
            // 
            // removeCarA
            // 
            this.removeCarA.AutoSize = true;
            this.removeCarA.BackColor = System.Drawing.Color.White;
            this.removeCarA.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.removeCarA.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.removeCarA.Location = new System.Drawing.Point(305, 122);
            this.removeCarA.Name = "removeCarA";
            this.removeCarA.Size = new System.Drawing.Size(22, 22);
            this.removeCarA.TabIndex = 13;
            this.removeCarA.Text = "˅";
            this.removeCarA.Click += new System.EventHandler(this.removeCarA_Click);
            this.removeCarA.MouseLeave += new System.EventHandler(this.ArrowRemoveCarALeave);
            this.removeCarA.MouseHover += new System.EventHandler(this.ArrowRemoveCarAHover);
            // 
            // addCarA
            // 
            this.addCarA.AutoSize = true;
            this.addCarA.BackColor = System.Drawing.Color.White;
            this.addCarA.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.addCarA.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.addCarA.Location = new System.Drawing.Point(305, 89);
            this.addCarA.Name = "addCarA";
            this.addCarA.Size = new System.Drawing.Size(22, 22);
            this.addCarA.TabIndex = 2;
            this.addCarA.Text = "˄";
            this.addCarA.Click += new System.EventHandler(this.addCarA_Click);
            this.addCarA.MouseLeave += new System.EventHandler(this.ArrowAddCarALeave);
            this.addCarA.MouseHover += new System.EventHandler(this.ArrowAddCarAHover);
            // 
            // labelCarsE
            // 
            this.labelCarsE.AutoSize = true;
            this.labelCarsE.Location = new System.Drawing.Point(238, 395);
            this.labelCarsE.Name = "labelCarsE";
            this.labelCarsE.Size = new System.Drawing.Size(46, 29);
            this.labelCarsE.TabIndex = 12;
            this.labelCarsE.Text = "0/2";
            // 
            // labelCarsA
            // 
            this.labelCarsA.AutoSize = true;
            this.labelCarsA.Location = new System.Drawing.Point(238, 123);
            this.labelCarsA.Name = "labelCarsA";
            this.labelCarsA.Size = new System.Drawing.Size(46, 29);
            this.labelCarsA.TabIndex = 11;
            this.labelCarsA.Text = "0/3";
            // 
            // checkBoxCarE
            // 
            this.checkBoxCarE.AutoSize = true;
            this.checkBoxCarE.Location = new System.Drawing.Point(194, 391);
            this.checkBoxCarE.Name = "checkBoxCarE";
            this.checkBoxCarE.Size = new System.Drawing.Size(28, 27);
            this.checkBoxCarE.TabIndex = 4;
            this.checkBoxCarE.UseVisualStyleBackColor = true;
            this.checkBoxCarE.CheckedChanged += new System.EventHandler(this.IsUsedCarEChanged);
            // 
            // pictureCarD
            // 
            this.pictureCarD.Image = ((System.Drawing.Image)(resources.GetObject("pictureCarD.Image")));
            this.pictureCarD.Location = new System.Drawing.Point(73, 289);
            this.pictureCarD.Name = "pictureCarD";
            this.pictureCarD.Size = new System.Drawing.Size(93, 64);
            this.pictureCarD.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureCarD.TabIndex = 8;
            this.pictureCarD.TabStop = false;
            // 
            // pictureCarE
            // 
            this.pictureCarE.Image = ((System.Drawing.Image)(resources.GetObject("pictureCarE.Image")));
            this.pictureCarE.Location = new System.Drawing.Point(18, 357);
            this.pictureCarE.Name = "pictureCarE";
            this.pictureCarE.Size = new System.Drawing.Size(148, 67);
            this.pictureCarE.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureCarE.TabIndex = 9;
            this.pictureCarE.TabStop = false;
            // 
            // checkBoxCarD
            // 
            this.checkBoxCarD.AutoSize = true;
            this.checkBoxCarD.Location = new System.Drawing.Point(194, 323);
            this.checkBoxCarD.Name = "checkBoxCarD";
            this.checkBoxCarD.Size = new System.Drawing.Size(28, 27);
            this.checkBoxCarD.TabIndex = 3;
            this.checkBoxCarD.UseVisualStyleBackColor = true;
            // 
            // pictureCarC
            // 
            this.pictureCarC.Image = ((System.Drawing.Image)(resources.GetObject("pictureCarC.Image")));
            this.pictureCarC.Location = new System.Drawing.Point(33, 223);
            this.pictureCarC.Name = "pictureCarC";
            this.pictureCarC.Size = new System.Drawing.Size(133, 60);
            this.pictureCarC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureCarC.TabIndex = 7;
            this.pictureCarC.TabStop = false;
            // 
            // checkBoxCarC
            // 
            this.checkBoxCarC.AutoSize = true;
            this.checkBoxCarC.Location = new System.Drawing.Point(194, 255);
            this.checkBoxCarC.Name = "checkBoxCarC";
            this.checkBoxCarC.Size = new System.Drawing.Size(28, 27);
            this.checkBoxCarC.TabIndex = 2;
            this.checkBoxCarC.UseVisualStyleBackColor = true;
            // 
            // pictureCarB
            // 
            this.pictureCarB.Image = ((System.Drawing.Image)(resources.GetObject("pictureCarB.Image")));
            this.pictureCarB.Location = new System.Drawing.Point(47, 158);
            this.pictureCarB.Name = "pictureCarB";
            this.pictureCarB.Size = new System.Drawing.Size(119, 59);
            this.pictureCarB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureCarB.TabIndex = 6;
            this.pictureCarB.TabStop = false;
            // 
            // pictureCarA
            // 
            this.pictureCarA.Image = ((System.Drawing.Image)(resources.GetObject("pictureCarA.Image")));
            this.pictureCarA.Location = new System.Drawing.Point(72, 88);
            this.pictureCarA.Name = "pictureCarA";
            this.pictureCarA.Size = new System.Drawing.Size(94, 64);
            this.pictureCarA.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureCarA.TabIndex = 5;
            this.pictureCarA.TabStop = false;
            // 
            // checkBoxCarB
            // 
            this.checkBoxCarB.AutoSize = true;
            this.checkBoxCarB.Location = new System.Drawing.Point(194, 187);
            this.checkBoxCarB.Name = "checkBoxCarB";
            this.checkBoxCarB.Size = new System.Drawing.Size(28, 27);
            this.checkBoxCarB.TabIndex = 1;
            this.checkBoxCarB.UseVisualStyleBackColor = true;
            // 
            // checkBoxCarA
            // 
            this.checkBoxCarA.AutoSize = true;
            this.checkBoxCarA.Location = new System.Drawing.Point(194, 119);
            this.checkBoxCarA.Name = "checkBoxCarA";
            this.checkBoxCarA.Size = new System.Drawing.Size(28, 27);
            this.checkBoxCarA.TabIndex = 0;
            this.checkBoxCarA.UseVisualStyleBackColor = true;
            this.checkBoxCarA.CheckedChanged += new System.EventHandler(this.checkBoxCarA_CheckedChanged);
            // 
            // groupBoxSetup
            // 
            this.groupBoxSetup.Controls.Add(this.textBoxSeed);
            this.groupBoxSetup.Controls.Add(this.label4);
            this.groupBoxSetup.Controls.Add(this.label3);
            this.groupBoxSetup.Controls.Add(this.label1);
            this.groupBoxSetup.Controls.Add(this.checkBoxVizual);
            this.groupBoxSetup.Controls.Add(this.label2);
            this.groupBoxSetup.Controls.Add(this.labelReplications);
            this.groupBoxSetup.Controls.Add(this.TextBoxReplications);
            this.groupBoxSetup.Location = new System.Drawing.Point(12, 462);
            this.groupBoxSetup.Name = "groupBoxSetup";
            this.groupBoxSetup.Size = new System.Drawing.Size(355, 444);
            this.groupBoxSetup.TabIndex = 2;
            this.groupBoxSetup.TabStop = false;
            this.groupBoxSetup.Text = "Simulation Setup";
            // 
            // textBoxSeed
            // 
            this.textBoxSeed.Location = new System.Drawing.Point(38, 206);
            this.textBoxSeed.Name = "textBoxSeed";
            this.textBoxSeed.Size = new System.Drawing.Size(260, 35);
            this.textBoxSeed.TabIndex = 20;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(33, 174);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(185, 29);
            this.label4.TabIndex = 19;
            this.label4.Text = "Generator Seed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(71, 351);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(117, 29);
            this.label3.TabIndex = 18;
            this.label3.Text = "Trim 40%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.Location = new System.Drawing.Point(34, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 22);
            this.label1.TabIndex = 17;
            this.label1.Text = "˅";
            // 
            // checkBoxVizual
            // 
            this.checkBoxVizual.AutoSize = true;
            this.checkBoxVizual.Location = new System.Drawing.Point(38, 279);
            this.checkBoxVizual.Name = "checkBoxVizual";
            this.checkBoxVizual.Size = new System.Drawing.Size(140, 33);
            this.checkBoxVizual.TabIndex = 2;
            this.checkBoxVizual.Text = "Vizualize";
            this.checkBoxVizual.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(34, 337);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 22);
            this.label2.TabIndex = 16;
            this.label2.Text = "˄";
            // 
            // labelReplications
            // 
            this.labelReplications.AutoSize = true;
            this.labelReplications.Location = new System.Drawing.Point(33, 89);
            this.labelReplications.Name = "labelReplications";
            this.labelReplications.Size = new System.Drawing.Size(265, 29);
            this.labelReplications.TabIndex = 1;
            this.labelReplications.Text = "Number of Replications";
            // 
            // TextBoxReplications
            // 
            this.TextBoxReplications.Location = new System.Drawing.Point(38, 120);
            this.TextBoxReplications.Name = "TextBoxReplications";
            this.TextBoxReplications.Size = new System.Drawing.Size(260, 35);
            this.TextBoxReplications.TabIndex = 0;
            // 
            // groupBoxSim
            // 
            this.groupBoxSim.Controls.Add(this.labelSimTime);
            this.groupBoxSim.Controls.Add(this.labelQueueUnload);
            this.groupBoxSim.Controls.Add(this.labelQueueLoad);
            this.groupBoxSim.Controls.Add(this.labelUnloaderB);
            this.groupBoxSim.Controls.Add(this.labelUnloaderA);
            this.groupBoxSim.Controls.Add(this.labelLoaderB);
            this.groupBoxSim.Controls.Add(this.labelLoaderA);
            this.groupBoxSim.Controls.Add(this.pictureUnloaderB);
            this.groupBoxSim.Controls.Add(this.pictureUnloaderA);
            this.groupBoxSim.Controls.Add(this.pictureLoaderB);
            this.groupBoxSim.Controls.Add(this.pictureLoaderA);
            this.groupBoxSim.Controls.Add(this.buttonStop);
            this.groupBoxSim.Controls.Add(this.buttonSlowUp);
            this.groupBoxSim.Controls.Add(this.buttonRun);
            this.groupBoxSim.Controls.Add(this.buttonPause);
            this.groupBoxSim.Controls.Add(this.buttonSlowDown);
            this.groupBoxSim.Location = new System.Drawing.Point(392, 12);
            this.groupBoxSim.Name = "groupBoxSim";
            this.groupBoxSim.Size = new System.Drawing.Size(492, 894);
            this.groupBoxSim.TabIndex = 3;
            this.groupBoxSim.TabStop = false;
            this.groupBoxSim.Text = "Simulation";
            // 
            // labelSimTime
            // 
            this.labelSimTime.AutoSize = true;
            this.labelSimTime.Location = new System.Drawing.Point(41, 659);
            this.labelSimTime.Name = "labelSimTime";
            this.labelSimTime.Size = new System.Drawing.Size(184, 29);
            this.labelSimTime.TabIndex = 25;
            this.labelSimTime.Text = "Simulation time:";
            // 
            // labelQueueUnload
            // 
            this.labelQueueUnload.AutoSize = true;
            this.labelQueueUnload.Location = new System.Drawing.Point(25, 325);
            this.labelQueueUnload.Name = "labelQueueUnload";
            this.labelQueueUnload.Size = new System.Drawing.Size(222, 29);
            this.labelQueueUnload.TabIndex = 24;
            this.labelQueueUnload.Text = "Queue at Unloader:";
            // 
            // labelQueueLoad
            // 
            this.labelQueueLoad.AutoSize = true;
            this.labelQueueLoad.Location = new System.Drawing.Point(25, 89);
            this.labelQueueLoad.Name = "labelQueueLoad";
            this.labelQueueLoad.Size = new System.Drawing.Size(199, 29);
            this.labelQueueLoad.TabIndex = 23;
            this.labelQueueLoad.Text = "Queue at Loader:";
            // 
            // labelUnloaderB
            // 
            this.labelUnloaderB.AutoSize = true;
            this.labelUnloaderB.Location = new System.Drawing.Point(41, 469);
            this.labelUnloaderB.Name = "labelUnloaderB";
            this.labelUnloaderB.Size = new System.Drawing.Size(152, 29);
            this.labelUnloaderB.TabIndex = 22;
            this.labelUnloaderB.Text = "Unloads Car:";
            // 
            // labelUnloaderA
            // 
            this.labelUnloaderA.AutoSize = true;
            this.labelUnloaderA.Location = new System.Drawing.Point(41, 403);
            this.labelUnloaderA.Name = "labelUnloaderA";
            this.labelUnloaderA.Size = new System.Drawing.Size(152, 29);
            this.labelUnloaderA.TabIndex = 21;
            this.labelUnloaderA.Text = "Unloads Car:";
            // 
            // labelLoaderB
            // 
            this.labelLoaderB.AutoSize = true;
            this.labelLoaderB.Location = new System.Drawing.Point(154, 234);
            this.labelLoaderB.Name = "labelLoaderB";
            this.labelLoaderB.Size = new System.Drawing.Size(123, 29);
            this.labelLoaderB.TabIndex = 20;
            this.labelLoaderB.Text = "Loads Car";
            // 
            // labelLoaderA
            // 
            this.labelLoaderA.AutoSize = true;
            this.labelLoaderA.Location = new System.Drawing.Point(154, 152);
            this.labelLoaderA.Name = "labelLoaderA";
            this.labelLoaderA.Size = new System.Drawing.Size(135, 29);
            this.labelLoaderA.TabIndex = 19;
            this.labelLoaderA.Text = "Loads Car: ";
            // 
            // pictureUnloaderB
            // 
            this.pictureUnloaderB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureUnloaderB.BackgroundImage")));
            this.pictureUnloaderB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureUnloaderB.Location = new System.Drawing.Point(368, 451);
            this.pictureUnloaderB.Name = "pictureUnloaderB";
            this.pictureUnloaderB.Size = new System.Drawing.Size(103, 69);
            this.pictureUnloaderB.TabIndex = 18;
            this.pictureUnloaderB.TabStop = false;
            // 
            // pictureUnloaderA
            // 
            this.pictureUnloaderA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pictureUnloaderA.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureUnloaderA.BackgroundImage")));
            this.pictureUnloaderA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureUnloaderA.Location = new System.Drawing.Point(368, 376);
            this.pictureUnloaderA.Name = "pictureUnloaderA";
            this.pictureUnloaderA.Size = new System.Drawing.Size(103, 69);
            this.pictureUnloaderA.TabIndex = 17;
            this.pictureUnloaderA.TabStop = false;
            // 
            // pictureLoaderB
            // 
            this.pictureLoaderB.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureLoaderB.BackgroundImage")));
            this.pictureLoaderB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureLoaderB.Location = new System.Drawing.Point(30, 207);
            this.pictureLoaderB.Name = "pictureLoaderB";
            this.pictureLoaderB.Size = new System.Drawing.Size(118, 85);
            this.pictureLoaderB.TabIndex = 16;
            this.pictureLoaderB.TabStop = false;
            // 
            // pictureLoaderA
            // 
            this.pictureLoaderA.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("pictureLoaderA.BackgroundImage")));
            this.pictureLoaderA.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureLoaderA.Location = new System.Drawing.Point(30, 137);
            this.pictureLoaderA.Name = "pictureLoaderA";
            this.pictureLoaderA.Size = new System.Drawing.Size(118, 64);
            this.pictureLoaderA.TabIndex = 15;
            this.pictureLoaderA.TabStop = false;
            // 
            // buttonStop
            // 
            this.buttonStop.BackColor = System.Drawing.Color.White;
            this.buttonStop.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonStop.BackgroundImage")));
            this.buttonStop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonStop.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonStop.FlatAppearance.BorderSize = 0;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.ForeColor = System.Drawing.Color.White;
            this.buttonStop.Location = new System.Drawing.Point(328, 556);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(49, 49);
            this.buttonStop.TabIndex = 4;
            this.buttonStop.UseVisualStyleBackColor = false;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // buttonSlowUp
            // 
            this.buttonSlowUp.BackColor = System.Drawing.Color.White;
            this.buttonSlowUp.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSlowUp.BackgroundImage")));
            this.buttonSlowUp.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSlowUp.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonSlowUp.FlatAppearance.BorderSize = 0;
            this.buttonSlowUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSlowUp.ForeColor = System.Drawing.Color.White;
            this.buttonSlowUp.Location = new System.Drawing.Point(273, 556);
            this.buttonSlowUp.Name = "buttonSlowUp";
            this.buttonSlowUp.Size = new System.Drawing.Size(49, 49);
            this.buttonSlowUp.TabIndex = 3;
            this.buttonSlowUp.UseVisualStyleBackColor = false;
            this.buttonSlowUp.Click += new System.EventHandler(this.buttonSlowUp_Click);
            // 
            // buttonRun
            // 
            this.buttonRun.BackColor = System.Drawing.Color.White;
            this.buttonRun.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonRun.BackgroundImage")));
            this.buttonRun.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonRun.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonRun.FlatAppearance.BorderSize = 0;
            this.buttonRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRun.ForeColor = System.Drawing.Color.White;
            this.buttonRun.Location = new System.Drawing.Point(108, 556);
            this.buttonRun.Name = "buttonRun";
            this.buttonRun.Size = new System.Drawing.Size(49, 49);
            this.buttonRun.TabIndex = 2;
            this.buttonRun.UseVisualStyleBackColor = false;
            this.buttonRun.Click += new System.EventHandler(this.buttonRun_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.BackColor = System.Drawing.Color.White;
            this.buttonPause.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonPause.BackgroundImage")));
            this.buttonPause.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonPause.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonPause.FlatAppearance.BorderSize = 0;
            this.buttonPause.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPause.ForeColor = System.Drawing.Color.White;
            this.buttonPause.Location = new System.Drawing.Point(218, 556);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(49, 49);
            this.buttonPause.TabIndex = 1;
            this.buttonPause.UseVisualStyleBackColor = false;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // buttonSlowDown
            // 
            this.buttonSlowDown.BackColor = System.Drawing.Color.White;
            this.buttonSlowDown.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("buttonSlowDown.BackgroundImage")));
            this.buttonSlowDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.buttonSlowDown.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.buttonSlowDown.FlatAppearance.BorderSize = 0;
            this.buttonSlowDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSlowDown.ForeColor = System.Drawing.Color.White;
            this.buttonSlowDown.Location = new System.Drawing.Point(163, 556);
            this.buttonSlowDown.Name = "buttonSlowDown";
            this.buttonSlowDown.Size = new System.Drawing.Size(49, 49);
            this.buttonSlowDown.TabIndex = 0;
            this.buttonSlowDown.UseVisualStyleBackColor = false;
            this.buttonSlowDown.Click += new System.EventHandler(this.buttonSlowDown_Click);
            // 
            // groupBoxStats
            // 
            this.groupBoxStats.Location = new System.Drawing.Point(904, 12);
            this.groupBoxStats.Name = "groupBoxStats";
            this.groupBoxStats.Size = new System.Drawing.Size(644, 894);
            this.groupBoxStats.TabIndex = 4;
            this.groupBoxStats.TabStop = false;
            this.groupBoxStats.Text = "Statistics";
            // 
            // FormAgentSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1560, 921);
            this.Controls.Add(this.groupBoxStats);
            this.Controls.Add(this.groupBoxSim);
            this.Controls.Add(this.groupBoxSetup);
            this.Controls.Add(this.groupBoxVehicles);
            this.MinimumSize = new System.Drawing.Size(28, 1000);
            this.Name = "FormAgentSimulation";
            this.Text = "Agent Based Simulation";
            this.groupBoxVehicles.ResumeLayout(false);
            this.groupBoxVehicles.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCarD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCarE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCarC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCarB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureCarA)).EndInit();
            this.groupBoxSetup.ResumeLayout(false);
            this.groupBoxSetup.PerformLayout();
            this.groupBoxSim.ResumeLayout(false);
            this.groupBoxSim.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUnloaderB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureUnloaderA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLoaderB)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureLoaderA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxVehicles;
        private System.Windows.Forms.CheckBox checkBoxCarE;
        private System.Windows.Forms.PictureBox pictureCarD;
        private System.Windows.Forms.PictureBox pictureCarE;
        private System.Windows.Forms.CheckBox checkBoxCarD;
        private System.Windows.Forms.PictureBox pictureCarC;
        private System.Windows.Forms.CheckBox checkBoxCarC;
        private System.Windows.Forms.PictureBox pictureCarB;
        private System.Windows.Forms.PictureBox pictureCarA;
        private System.Windows.Forms.CheckBox checkBoxCarB;
        private System.Windows.Forms.CheckBox checkBoxCarA;
        private System.Windows.Forms.Label labelCarsE;
        private System.Windows.Forms.Label labelCarsA;
        private System.Windows.Forms.Label addCarA;
        private System.Windows.Forms.Label removeCarA;
        private System.Windows.Forms.Label removeCarE;
        private System.Windows.Forms.Label addCarE;
        private System.Windows.Forms.GroupBox groupBoxSetup;
        private System.Windows.Forms.TextBox TextBoxReplications;
        private System.Windows.Forms.CheckBox checkBoxVizual;
        private System.Windows.Forms.Label labelReplications;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxSeed;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBoxSim;
        private System.Windows.Forms.Button buttonRun;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Button buttonSlowDown;
        private System.Windows.Forms.GroupBox groupBoxStats;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonSlowUp;
        private System.Windows.Forms.PictureBox pictureUnloaderB;
        private System.Windows.Forms.PictureBox pictureUnloaderA;
        private System.Windows.Forms.PictureBox pictureLoaderB;
        private System.Windows.Forms.PictureBox pictureLoaderA;
        public System.Windows.Forms.Label labelSimTime;
        public System.Windows.Forms.Label labelQueueUnload;
        public System.Windows.Forms.Label labelQueueLoad;
        public System.Windows.Forms.Label labelUnloaderB;
        public System.Windows.Forms.Label labelUnloaderA;
        public System.Windows.Forms.Label labelLoaderB;
        public System.Windows.Forms.Label labelLoaderA;
    }
}

