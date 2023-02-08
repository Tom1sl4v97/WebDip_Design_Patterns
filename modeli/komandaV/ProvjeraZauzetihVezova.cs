using System;
using System.Diagnostics.Metrics;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;
using ttomiek_zadaca_2.view;
using ttomiek_zadaca_3.modeli;

namespace ttomiek_zadaca_1.komandaV
{
    public class ProvjeraZauzetihVezova
    {
        public static List<ZahtjevRezervacije> provjeraVezova(Vez vez, DateTime datumOd, DateTime datumDo, List<ZahtjevRezervacije> pomocnaListaZahtjeva, int index)
        {
            int pocetniDan = (int)datumOd.DayOfWeek;
            int krajnjiDan = (int)datumDo.DayOfWeek;

            if (krajnjiDan < pocetniDan)
            {
                ispisiZautetostRaspored(0, krajnjiDan, vez, datumOd, datumDo);
                krajnjiDan = 6;
            }

            ispisiZautetostRaspored(pocetniDan, krajnjiDan, vez, datumOd, datumDo);

            foreach (ZahtjevRezervacije zahtjev in pomocnaListaZahtjeva)
            {
                DateTime pocetnoVrijeme = zahtjev.datumVrijemeOd;
                DateTime krajnjeVrijeme = zahtjev.datumVrijemeOd.AddHours(zahtjev.trajanjePrivezaUH);
                if (pocetnoVrijeme <= datumDo && krajnjeVrijeme >= datumOd)
                {
                    pomocnaListaZahtjeva.Remove(zahtjev);
                    KomandeKorisnika.dodajRedakZaPomocnuListu(IspisRedovaRada.ispisVeza(vez, "ZAUZET\tod: " + formDate(pocetnoVrijeme) + "\tdo: " + formDate(krajnjeVrijeme), index));
                    break;
                }
            }
            return pomocnaListaZahtjeva;
        }

        private static void ispisiZautetostRaspored(int pocetniDan, int krajnjiDan, Vez vez, DateTime datumOd, DateTime datumDo)
        {
            List<Raspored> sviRasporedi = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());
            int index = 1;
            for (int i = pocetniDan; i <= krajnjiDan; i++)
            {
                List<Raspored> popisZauzetih = new List<Raspored>();
                List<Raspored> raspored = new List<Raspored>(sviRasporedi.FindAll(x => x.idVez == vez.id && x.daniUTjednu == i));
                foreach (Raspored r in raspored)
                {
                    TimeOnly pocetnoVrijeme = TimeOnly.FromDateTime(datumOd);
                    TimeOnly krajnjeVrijeme = TimeOnly.FromDateTime(datumDo);
                    if (r.vrijemeOd <= pocetnoVrijeme && r.vrijemeDo >= krajnjeVrijeme)
                    {
                        popisZauzetih.Add(r);
                    }
                }
                if (popisZauzetih.Count > 0)
                {
                    KomandeKorisnika.dodajRedakZaPomocnuListu(IspisRedovaRada.ispisVeza(vez, "ZAUZET\tod: " + formDate(datumOd) + "\tdo: " + formDate(datumDo), index++));
                }
            }

        }

        private static string formDate(DateTime datumOd)
        {
            return ZajednickeMetode.formDate(datumOd);
        }
    }
}
