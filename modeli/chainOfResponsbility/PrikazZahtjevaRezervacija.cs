using System.Text.RegularExpressions;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;
using ttomiek_zadaca_2.view;
using ttomiek_zadaca_3.modeli;

namespace ttomiek_zadaca_1.chainOfResponsbility
{
    public class PrikazZahtjevaRezervacija : HandlerVlastiteFunkcije
    {
        public override void ProcesRequest(Match matcherKomande)
        {
            if (matcherKomande.Groups["prikazZahtjevaRezervacija"].Success)
            {
                prikazZahtjevaRezervacija();
            }
            else if (successor != null)
                successor.ProcesRequest(matcherKomande);
        }

        private void prikazZahtjevaRezervacija()
        {
            List<string> popisIspisa = new();
            List<ZahtjevRezervacije> popisSvihZahtjevaRezervacija = new List<ZahtjevRezervacije>(PodaciDatoteka.Instance.getListaZahtjevaRezervacije());
            popisIspisa.AddRange(OstaliPrikaziIspisa.ispisZaglavljaZahtjevaRezervacije());
            
            int index = 1;
            foreach(ZahtjevRezervacije zr in popisSvihZahtjevaRezervacija)
            {
                popisIspisa.Add(OstaliPrikaziIspisa.ispisRetkaZahtjevaRezervacije(zr, index++));
            }

            popisIspisa.AddRange(IspisRedovaRada.ispisPodnozja(index - 1));
            KomandeKorisnika.dodajRedakZaPomocnuListu(popisIspisa);
        }
    }
}
