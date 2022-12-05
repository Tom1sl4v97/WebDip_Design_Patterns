namespace ttomiek_zadaca_1.klase
{
    internal class Brod
    {
        public static string PATTERN_INFO_RETKA_CSV = "^(?=.*(?<id>id;?))(?=.*(?<oznaka_broda>oznaka_broda;?))(?=.*(?<naziv>naziv;?))(?=.*(?<vrsta>vrsta;?))(?=.*(?<duljina>duljina;?))(?=.*(?<sirina>sirina;?))(?=.*(?<gaz>gaz;?))(?=.*(?<maksimalna_brzina>maksimalna_brzina;?))(?=.*(?<kapacitet_putnika>kapacitet_putnika;?))(?=.*(?<kapacitet_osobnih_vozila>kapacitet_osobnih_vozila;?))(?=.*(?<kapacitet_tereta>kapacitet_tereta;?)).+";
        public int id { get; set; }
        private string _oznakaBroda = "";
        public string oznakaBroda 
        {
            get
            {
                return _oznakaBroda;
            }
            set
            {
                if (value == null || value == "")
                    throw new Exception("Oznaka broda ne smije biti prazna!");
                _oznakaBroda = value;
            }
        }
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
                    throw new Exception("Naziv ne smije biti prazan!");
                _naziv = value;
            }
        }
        private string _vrsta = "";
        public string vrsta 
        {
            get
            {
                return _vrsta;
            }
            set
            {
                if (value == null || value == "")
                    throw new Exception("Vrsta ne smije biti prazna!");
                _vrsta = value;
            }
        }
        public double duljina { get; set; }
        public double sirina { get; set; }
        public double gaz { get; set; }
        public double maksimalnaBrzina { get; set; }
        public int kapacitetPutnika { get; set; }
        public int kapacitetOsobnihVozila { get; set; }
        public double kapacitetTereta { get; set; }
    }
}
