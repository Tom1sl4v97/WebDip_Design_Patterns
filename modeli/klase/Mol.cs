namespace ttomiek_zadaca_1.klase
{
    public class Mol
    {
        public static string PATTERN_INFO_RETKA_CSV = "^(?=.*(?<id_mol>id_mol;?))(?=.*(?<naziv>naziv;?)).+";
        public int idMol { get; set; }
        private string _naziv = "";
        public string naziv
        {
            get
            {
                return _naziv;
            }
            set
            {
                if (value == null || value == "")
                    throw new Exception("Naziv mola ne smije biti prazan!");
                _naziv = value;
            }
        }
    }
}
