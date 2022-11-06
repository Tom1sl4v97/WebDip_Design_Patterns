namespace ttomiek_zadaca_1.singelton_class
{
    internal class VirtualniSat
    {
        private VirtualniSat() { }

        private static VirtualniSat? instance = null;
        public static VirtualniSat Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new VirtualniSat();
                }
                return instance;
            }
        }

        private DateTime _pocetnoVrijeme = DateTime.Now;
        private DateTime _virtualnoVrijeme = DateTime.Now;

        public DateTime virtualnoVrijeme()
        {
            TimeSpan razlika = DateTime.Now.Subtract(_pocetnoVrijeme);
            return _virtualnoVrijeme.Add(razlika);
        }

        public void virtualnoVrijeme(DateTime novoVrijeme)
        {
            _virtualnoVrijeme = novoVrijeme;
            _pocetnoVrijeme = DateTime.Now;
        }
    }
}
