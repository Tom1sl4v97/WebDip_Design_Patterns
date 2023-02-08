using ttomiek_zadaca_1.singelton_class;

namespace ttomiek_zadaca_1.klase
{
    public class StanjeZahtjevaRezervacije
    {
        public string nazivStanja { get; set; }
        public DateTime vrijeme { get; set; }
        public List<ZahtjevRezervacije> listaZahtjevaRezervacije { get; set; }

        public StanjeZahtjevaRezervacije(string nazivStanja)
        {
            this.nazivStanja = nazivStanja;
            this.vrijeme = VirtualniSat.Instance.virtualnoVrijeme();
            List<ZahtjevRezervacije> popisZR = new List<ZahtjevRezervacije>(PodaciDatoteka.Instance.getListaZahtjevaRezervacije());
            this.listaZahtjevaRezervacije = popisZR;
        }
    }
}
