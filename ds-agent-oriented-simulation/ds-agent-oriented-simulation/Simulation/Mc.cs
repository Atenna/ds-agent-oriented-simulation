using OSPABA;

namespace ds_agent_oriented_simulation.Simulation
{
    public class Mc : IdList
    {
        //meta! userInfo="Generated code: do not modify", tag="begin"
        public const int DovozMaterialu = 1002;
        public const int NalozAuto = 1003;
        public const int VylozAuto = 1004;
        public const int OdvozMaterialu = 1015;
        public const int Inicializacia = 1014;
        //meta! tag="end"

        // 1..1000 range reserved for user
        public const int NalozenieUkoncene = 100;
        public const int VylozenieUkoncene = 200;
        public const int PrejazdUkonceny = 300;
        public const int ExportUkonceny = 400;
        public const int ExportZacaty = 500;
        public const int ZaciatokPracovnejDoby = 600;
        public const int KoniecPracovnejDoby = 700;
    }
}
