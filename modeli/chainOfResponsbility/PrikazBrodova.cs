using System.Text.RegularExpressions;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;
using ttomiek_zadaca_2.view;
using ttomiek_zadaca_3.modeli;

namespace ttomiek_zadaca_1.chainOfResponsbility
{
    public class PrikazBrodova : HandlerVlastiteFunkcije
    {
        public override void ProcesRequest(Match matcherKomande)
        {
            if (matcherKomande.Groups["prikazBrodova"].Success)
            {
                ispisSvihRasporeda();
            }
            if (successor != null)
                successor.ProcesRequest(matcherKomande);
        }

        public static void ispisSvihRasporeda()
        {
            List<string> popisIspisa = new();
            List<Brod> popisBrodova = new List<Brod>(PodaciDatoteka.Instance.getListaBrodova());
            
            popisIspisa.AddRange(OstaliPrikaziIspisa.ispisZaglavljaBroda());
            
            int index = 1;
            foreach (Brod brod in popisBrodova)
            {
                popisIspisa.Add(OstaliPrikaziIspisa.ispisRetkaBroda(brod, index++));
            }

            popisIspisa.AddRange(IspisRedovaRada.ispisPodnozja(index - 1));
            KomandeKorisnika.dodajRedakZaPomocnuListu(popisIspisa);
        }
    }
}
