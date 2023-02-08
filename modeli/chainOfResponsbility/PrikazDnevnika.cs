using System.Text.RegularExpressions;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;
using ttomiek_zadaca_2.view;
using ttomiek_zadaca_3.modeli;

namespace ttomiek_zadaca_1.chainOfResponsbility
{
    public class PrikazDnevnika : HandlerVlastiteFunkcije
    {
        public override void ProcesRequest(Match matcherKomande)
        {
            if (matcherKomande.Groups["prikazDnevnika"].Success)
            {
                prikaziDnevnik();
            }
            if (successor != null)
                successor.ProcesRequest(matcherKomande);
        }

        private void prikaziDnevnik()
        {
            List<string> popisIspisa = new();
            List<DnevnikRada> popisSvihDnevnika = new List<DnevnikRada>(PodaciDatoteka.Instance.getListaDnevnikaRada());

            popisIspisa.AddRange(OstaliPrikaziIspisa.ispisZaglavljaDnevnika());
            
            int index = 1;
            foreach(DnevnikRada dr in popisSvihDnevnika)
            {
                popisIspisa.Add(OstaliPrikaziIspisa.ispisRetkaDnevnika(dr, index++)); 
            }

            popisIspisa.AddRange(IspisRedovaRada.ispisPodnozja(index - 1));
            KomandeKorisnika.dodajRedakZaPomocnuListu(popisIspisa);
        }
    }
}
