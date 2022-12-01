namespace ttomiek_zadaca_1.klase
{
    internal class MolVez
    {
        public static string PATTERN_INFO_RETKA_CSV = "^(?=.*(?<id_mol>id_mol;?))(?=.*(?<id_vezovi>id_vezovi;?)).+";
        public int idMol { get; set; }
        public int idVez { get; set; }
    }
}
