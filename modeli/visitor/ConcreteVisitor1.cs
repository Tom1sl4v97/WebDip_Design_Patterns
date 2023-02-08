using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;
using ttomiek_zadaca_2.view;
using ttomiek_zadaca_3.modeli;

namespace ttomiek_zadaca_1.visitor
{
    public class ConcreteVisitor1 : Visitor
    {
        private int broj = 1;
        private int brojPremaVrsti = 0;

        public override void VisitConcreteElementZauzetost(ConcreteElementZauzetost concreteElementZauzetost)
        {
            List<Vez> popisSvihVezove = new List<Vez>(PodaciDatoteka.Instance.getListaVeza());
            List<Vez> popisOdredenihVezova = popisSvihVezove.FindAll(x => x.vrsta == concreteElementZauzetost.vrstaVeza);
            List<Vez> popisZauzetihVezova = ispisTrenutnogStatusa(popisOdredenihVezova, concreteElementZauzetost.vrijeme);

            foreach (Vez vez in popisZauzetihVezova)
            {
                KomandeKorisnika.dodajRedakZaPomocnuListu(IspisRedovaRada.ispisVeza(vez, "ZAUZETI", broj++));
            }

            brojPremaVrsti = popisZauzetihVezova.Count;
            KomandeKorisnika.povecajUkupniBrojZauzetihVezova(broj - 1);
        }

        public override void VisitConcreteElementRedniBroj(ConcreteElementRedniBroj concreteElementRedniBroj)
        {
            if (Tablice.Instance.RB)
            {
                KomandeKorisnika.dodajRedakZaPomocnuListu((String.Format("{0,-21} {1,52}", "UKUPNO VRSTE VEZA " + concreteElementRedniBroj.vrstaVeza + ":", brojPremaVrsti)));
            }
        }

        public static List<Vez> ispisTrenutnogStatusa(List<Vez> popisSvihVezova, DateTime trenutnoVrijeme)
        {
            List<Vez> popisZauzetihVezova = new List<Vez>();
            List<Raspored> sviRasporedi = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());
            List<ZahtjevRezervacije> popisZahtjeva = new List<ZahtjevRezervacije>(PodaciDatoteka.Instance.getListaZahtjevaRezervacije());
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
                    popisZauzetihVezova.Add(vez);
            }
            return popisZauzetihVezova;
        }
    }
}
