namespace ds_agent_oriented_simulation.Settings
{
    class Constants
    {
        public const double MaterialToLoad = 5000;
        public const double LoadMachinePerformance = 180 / 60.0;
        public const double LoadMachine2Performance = 180 / 60.0; //250 / 60.0;
        public const double UnloadMachinePerformance = 190 / 60.0;

        public const int AbLength = 45;
        public const int BcLength = 15;
        public const int CaLength = 35;

        public const int VolumeOfVehicleA = 10;
        public const int VolumeOfVehicleB = 20;
        public const int VolumeOfVehicleC = 25;
        public const int VolumeOfVehicleD = 5;
        public const int VolumeOfVehicleE = 40;

        public const int SpeedOfVehicleA = 60;
        public const int SpeedOfVehicleB = 50;
        public const int SpeedOfVehicleC = 45;
        public const int SpeedOfVehicleD = 70;
        public const int SpeedOfVehicleE = 30;

        public const double ProbabilityOfCrashOfVehicleA = 0.12;
        public const double ProbabilityOfCrashOfVehicleB = 0.04;
        public const double ProbabilityOfCrashOfVehicleC = 0.04;
        public const double ProbabilityOfCrashOfVehicleD = 0.11;
        public const double ProbabilityOfCrashOfVehicleE = 0.06;

        public const int TimeOfRepairOfVehicleA = 80;
        public const int TimeOfRepairOfVehicleB = 50;
        public const int TimeOfRepairOfVehicleC = 100;
        public const int TimeOfRepairOfVehicleD = 44;
        public const int TimeOfRepairOfVehicleE = 170;

        public const int MaxNumberOfCarsA = 3;
        public const int MaxNumberOfCarsE = 2;

        public static readonly object queueLock = new object();
        public static readonly object queue2Lock = new object();
        public static int Seed { get; set; }
        public static double MaterialAtDepo = 3500;
        public static double MaterialAtBuilding = 1500;
    }
}
