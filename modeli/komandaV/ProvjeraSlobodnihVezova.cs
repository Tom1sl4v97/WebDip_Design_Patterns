using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;
using ttomiek_zadaca_2.view;
using ttomiek_zadaca_3.modeli;

namespace ttomiek_zadaca_1.komandaV
{
    public class ProvjeraSlobodnihVezova
    {
        public static List<ZahtjevRezervacije> provjeraVezova(Vez vez, DateTime datumOd, DateTime datumDo, List<ZahtjevRezervacije> pomocnaListaZahtjeva, int index)
        {
            string dioOdgovora = "SLOBODAN\t";
            bool provjeraVeza = true;
            bool nemojPreskocitiZapis = true;
            foreach (ZahtjevRezervacije zahtjev in pomocnaListaZahtjeva)
            {
                DateTime pocetnoVrijeme = zahtjev.datumVrijemeOd;
                DateTime krajnjeVrijeme = zahtjev.datumVrijemeOd.AddHours(zahtjev.trajanjePrivezaUH);
                if (pocetnoVrijeme <= datumOd && krajnjeVrijeme >= datumOd && krajnjeVrijeme <= datumDo)
                {
                    provjeraVeza = false;
                    dioOdgovora += "od: " + formDate(krajnjeVrijeme) + " do: " + formDate(datumDo);
                }
                else if (pocetnoVrijeme >= datumOd && pocetnoVrijeme <= datumDo && krajnjeVrijeme >= datumOd && krajnjeVrijeme <= datumDo)
                {
                    provjeraVeza = false;
                    dioOdgovora += "od: " + formDate(datumOd) + " do: " + formDate(pocetnoVrijeme) + " i od: " + formDate(krajnjeVrijeme) + " do: " + formDate(datumDo);
                }
                else if (pocetnoVrijeme >= datumOd && pocetnoVrijeme <= datumDo && krajnjeVrijeme >= datumDo)
                {
                    provjeraVeza = false;
                    dioOdgovora += "od: " + formDate(datumOd) + " do: " + formDate(krajnjeVrijeme);
                }
                else if (pocetnoVrijeme <= datumOd && krajnjeVrijeme >= datumDo)
                    nemojPreskocitiZapis = false;

                pomocnaListaZahtjeva.Remove(zahtjev);
                break;
            }

            if (provjeraVeza) dioOdgovora += "u potpunosti";
            if (nemojPreskocitiZapis)
            {
                KomandeKorisnika.dodajRedakZaPomocnuListu(IspisRedovaRada.ispisVeza(vez, dioOdgovora, index));
            }

            return pomocnaListaZahtjeva;
        }

        private static string formDate(DateTime datumOd)
        {
            return ZajednickeMetode.formDate(datumOd);
        }
    }
}
