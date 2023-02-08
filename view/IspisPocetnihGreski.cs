using ttomiek_zadaca_1;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.singelton_class;
using ttomiek_zadaca_2.composite;

namespace ttomiek_zadaca_2.view
{
    public class IspisPocetnihGreski
    {
        public const string ANSI_ESC = "\x1b[";

        public static void ispisPocetnihUcitavanja()
        {
            Console.Write(ANSI_ESC + "2J");
            ispisiInfoPodatke(KonfiguracijskiPodaci.Instance.GetPocetakRedaRada());
            
            ispisGresaka();

            ispisPocetneKomande();
        }

        private static void ispisiInfoPodatke(int pocetakPisanjaRada)
        {
            prikazi(pocetakPisanjaRada++, 0, 32, "Učitani podaci za LUKE: " + PodaciDatoteka.Instance.getLuka().naziv);
            prikazi(pocetakPisanjaRada++, 0, 32, "Učitani podaci za BRODOVE: " + PodaciDatoteka.Instance.getListaBrodova().Count);
            prikazi(pocetakPisanjaRada++, 0, 32, "Učitani podaci za VEZOVE: " + PodaciDatoteka.Instance.getListaVeza().Count);

            string? nazivRasporeda = NaziviDatoteka.Instance.raspored;
            if (nazivRasporeda != "" && nazivRasporeda != null)
            {
                prikazi(pocetakPisanjaRada++, 0, 32, "Učitani podaci za RASPORED: " + PodaciDatoteka.Instance.getListaRasporeda().Count);
            }

            List<Kanal> popisKanala = new List<Kanal>(PodaciDatoteka.Instance.getListaKanala());
            prikazi(pocetakPisanjaRada++, 0, 32, "Učitani podaci za KANAL: " + popisKanala.Count);
            prikazi(pocetakPisanjaRada++, 0, 32, "Učitani podaci za MOL: " + BrodskaLuka.Instance.dohvatiKorijen().dohvatiPopisMolova().Count);
            prikazi(pocetakPisanjaRada, 0, 32, "Učitani podaci za MOL VEZOVI: " + PodaciDatoteka.Instance.getListaMolVezova().Count);
        }

        public static void ispisGresaka()
        {
            ispisSeparatoraEkrana(KonfiguracijskiPodaci.Instance.GetRedSeparatora());

            int pocetakPisanjaGreski = KonfiguracijskiPodaci.Instance.GetPocetakRedaGreski();
            Queue<Greske> popisGresaka = BrojacGreske.Instance.dohvatiPopisGreski();

            foreach (Greske greska in popisGresaka)
            {
                string porukaGreske = "Broj greske: " + greska.id + " " + greska.opisGreske;
                postavi(pocetakPisanjaGreski, 0);
                Console.Write(ANSI_ESC + "2K");
                prikazi(pocetakPisanjaGreski, 0, 31, porukaGreske);
                pocetakPisanjaGreski++;
            }
            ispisKrajaEkrana(KonfiguracijskiPodaci.Instance.brojRedaka + 2);
            //Thread.Sleep(ZajednickeMetode.VRIJEME_CEKANJA_MS);
        }
        public static void ispisPocetneKomande()
        {
            int maksimalniBrojRedaka = KonfiguracijskiPodaci.Instance.brojRedaka;
            int redakUpisaKomande;

            if (KonfiguracijskiPodaci.Instance.podjelaEkrana)
                redakUpisaKomande = KonfiguracijskiPodaci.Instance.GetRedSeparatora() - 1;
            else
                redakUpisaKomande = maksimalniBrojRedaka + 1;
            string virtualnoVrijeme = VirtualniSat.Instance.virtualnoVrijeme().ToString("dd.MM.yyyy. HH:mm:ss ");
            prikazi(redakUpisaKomande - 1, 0, 0, "Virtualno vrijeme: " + virtualnoVrijeme);
            prikazi(redakUpisaKomande, 0, 0, "Unesite komandu: ");
        }
        
        private static void ispisSeparatoraEkrana(int redniBrojRetka)
        {
            prikazi(redniBrojRetka, 0, 33, "-------------------------------------------------------------------------------------------------------------------------------------------------------------------------");
        }

        public static void ispisKrajaEkrana(int redniBrojRetka)
        {
            prikazi(redniBrojRetka, 0, 33, "KRAJ EKRANA, TE OVA LINIJA SE NE RAČUNA ZA UKUPNI BROJ REDAKA. OVO SLUZI SAMO ZA LAKŠE UOČAVANJE CIJELOG EKRANA--------------------------------------------------------");
        }

        static void postavi(int x, int y)
        {
            Console.Write(ANSI_ESC + x + ";" + y + "f");
        }

        static void prikazi(int x, int y, int boja, String tekst)
        {
            postavi(x, y);
            Console.Write(ANSI_ESC + boja + "m");
            Console.Write(tekst);
        }
    }
}
