using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_2.view;

namespace ttomiek_zadaca_1.zajednickeMetode
{
    public class ZajednickeMetode
    {
        public const int VRIJEME_CEKANJA_MS = 150;

        public static string formDate(DateTime vrijeme)
        {
            return vrijeme.ToString("dd.MM.yyyy. HH:mm:ss");
        }

        public static string dohvatiVrstuLuke(string vrstaBroda)
        {
            switch (vrstaBroda)
            {
                case "TR" or "KA" or "KL" or "KR":
                    return "PU";
                case "RI" or "TE":
                    return "PO";
                case "JA" or "BR" or "RO":
                    return "OS";
                default:
                    throw new Exception("Neispravna oznaka vrste broda: " + vrstaBroda);
            }
        }

        public static void provjeriVrstuVeza(string vrstaVeza)
        {
            if (vrstaVeza != "PU" && vrstaVeza != "PO" && vrstaVeza != "OS")
                throw new Exception("Unesena je neispravna vrsta veza!");
        }

        public static void zapisiDnevnikRada(bool odobrenZahtjev, DateTime vrijeme, int idBroda, int idKanala, bool slobodan)
        {
            DnevnikRada noviZapis = new DnevnikRada();

            noviZapis.odobrenZahtjev = odobrenZahtjev;
            noviZapis.vrijeme = vrijeme;
            noviZapis.idBrod = idBroda;
            noviZapis.idKanala = idKanala;
            noviZapis.slobodan = slobodan;

            PodaciDatoteka.Instance.addNoviDnevnikRada(noviZapis);
        }

        public static List<string> ispisTrenutnogStatusa(List<Vez> popisSvihVezova, DateTime trenutnoVrijeme)
        {
            List<string> popisIspisa = new();
            List<Raspored> sviRasporedi = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());
            List<ZahtjevRezervacije> popisZahtjeva = new List<ZahtjevRezervacije>(PodaciDatoteka.Instance.getListaZahtjevaRezervacije());
            int index = 1;
            foreach (Vez vez in popisSvihVezova)
            {
                int danUTjednu = (int)trenutnoVrijeme.DayOfWeek;
                TimeOnly vrijeme = TimeOnly.FromDateTime(trenutnoVrijeme);
                List<Raspored> preklapanjeRasporedom = sviRasporedi.FindAll(x => x.idVez == vez.id && x.daniUTjednu == danUTjednu);
                bool provjera = true;

                foreach (Raspored r in preklapanjeRasporedom)
                {
                    if (r.vrijemeOd <= vrijeme && r.vrijemeDo >= vrijeme)
                    {
                        provjera = false;
                    }
                }

                List<ZahtjevRezervacije> popisOdgovarajucihZahtjeva = popisZahtjeva.FindAll(x => x.idVeza == vez.id);

                foreach (ZahtjevRezervacije zr in popisOdgovarajucihZahtjeva)
                {
                    DateTime pocetnoVrijeme = zr.datumVrijemeOd;
                    DateTime krajnjeVrijeme = zr.datumVrijemeOd.AddHours(zr.trajanjePrivezaUH);
                    if (pocetnoVrijeme <= trenutnoVrijeme && krajnjeVrijeme >= trenutnoVrijeme)
                    {
                        popisZahtjeva.Remove(zr);
                        provjera = false;
                        break;
                    }
                }
                if (!provjera)
                    popisIspisa.Add(IspisRedovaRada.ispisVeza(vez, "ZAUZETI", index++));
                else
                    popisIspisa.Add(IspisRedovaRada.ispisVeza(vez, "SLOBODAN", index++));
            }

            return popisIspisa;
        }

        public static int DohvatiBrojRedakaGreski()
        {
            int omjerGreske;
            double maksimalniBrojRekada = KonfiguracijskiPodaci.Instance.brojRedaka;
            if (KonfiguracijskiPodaci.Instance.podjelaEkrana)
                omjerGreske = 100 - KonfiguracijskiPodaci.Instance.omjerEkrana;
            else
                omjerGreske = KonfiguracijskiPodaci.Instance.omjerEkrana;

            double omjerEkranaDecimalni = maksimalniBrojRekada * omjerGreske / 100;

            return (int)Math.Round(omjerEkranaDecimalni, MidpointRounding.ToZero);
        }

        public static int DohvatiBrojRedakaRada()
        {
            int omjerGreske;
            double maksimalniBrojRekada = KonfiguracijskiPodaci.Instance.brojRedaka;
            if (KonfiguracijskiPodaci.Instance.podjelaEkrana)
                omjerGreske = KonfiguracijskiPodaci.Instance.omjerEkrana;
            else
                omjerGreske = 100 - KonfiguracijskiPodaci.Instance.omjerEkrana;

            double omjerEkranaDecimalni = maksimalniBrojRekada * omjerGreske / 100;

            return (int)Math.Round(omjerEkranaDecimalni + 0.5, MidpointRounding.ToEven);
        }
    }
}
