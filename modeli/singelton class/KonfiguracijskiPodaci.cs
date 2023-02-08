using ttomiek_zadaca_1.zajednickeMetode;

namespace ttomiek_zadaca_1
{
    public sealed class KonfiguracijskiPodaci
    {
        private KonfiguracijskiPodaci() { }

        private static KonfiguracijskiPodaci? instance = null;
        public static KonfiguracijskiPodaci Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KonfiguracijskiPodaci();
                }
                return instance;
            }
        }
    
        public int brojRedaka { get; set; }
        public int omjerEkrana { get; set; }
        public bool podjelaEkrana { get; set; }

        private int _pocetakRedaGreski;
        private int _pocetakRedaRada;
        private int _redSeparatora;

        public int GetPocetakRedaGreski()
        {
            return _pocetakRedaGreski;
        }

        public int GetPocetakRedaRada()
        {
            return _pocetakRedaRada;
        }

        public int GetRedSeparatora()
        {
            return _redSeparatora;
        }

        public void PostaviPocetkeReda()
        {
            int brojRedakaGreski = ZajednickeMetode.DohvatiBrojRedakaGreski();
            int brojRedakaRada = ZajednickeMetode.DohvatiBrojRedakaRada();

            int pocetakPisanjaGreski = 1;
            int pocetakSeparatora = brojRedakaGreski + 1;
            int pocetakPisanjaRada = pocetakSeparatora + 1;
            
            if (KonfiguracijskiPodaci.Instance.podjelaEkrana)
            {
                pocetakPisanjaRada = 1;
                pocetakSeparatora = brojRedakaRada + 1;
                pocetakPisanjaGreski = pocetakSeparatora + 1;
            }

            _pocetakRedaGreski = pocetakPisanjaGreski;
            _pocetakRedaRada = pocetakPisanjaRada;
            _redSeparatora = pocetakSeparatora;
        }
            
    }
}
