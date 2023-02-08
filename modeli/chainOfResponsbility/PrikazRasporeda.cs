using System.Text.RegularExpressions;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;
using ttomiek_zadaca_2.view;
using ttomiek_zadaca_3.modeli;

namespace ttomiek_zadaca_1.chainOfResponsbility
{
    public class PrikazRasporeda : HandlerVlastiteFunkcije
    {
        public override void ProcesRequest(Match matcherKomande)
        {
            if (matcherKomande.Groups["prikazRasporeda"].Success)
            {
                ispisSvihRasporeda();
            }
            if (successor != null)
                successor.ProcesRequest(matcherKomande);
        }

        public static void ispisSvihRasporeda()
        {
            List<string> popisIspisa = new();
            List<Raspored> sviRasporedi = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());
            Raspored prethodni = new Raspored();

            popisIspisa.AddRange(OstaliPrikaziIspisa.ispisZaglavljaRasporeda());

            if (sviRasporedi.Count == 0)
            {
                BrojacGreske.Instance.dodajNovuGesku("Raspored nije učitani.");
                return;
            }

            string daniUtjednu = sviRasporedi[0].daniUTjednu.ToString();
            int index = 0;
            foreach (Raspored r in sviRasporedi)
            {
                if (prethodni.idVez == r.idVez && prethodni.idBrod == r.idBrod && prethodni.vrijemeOd == r.vrijemeOd && prethodni.vrijemeDo == r.vrijemeDo)
                {
                    daniUtjednu += ", " + r.daniUTjednu;
                }
                else
                {
                    if (index == 0)
                    {
                        index++;
                        daniUtjednu = r.daniUTjednu.ToString();
                        prethodni = r;
                        continue;
                    }
                    else
                    {
                        popisIspisa.Add(OstaliPrikaziIspisa.ispisRetkaRasporeda(prethodni, daniUtjednu, index++));
                        daniUtjednu = r.daniUTjednu.ToString();
                    }
                }
                prethodni = r;
            }

            popisIspisa.AddRange(IspisRedovaRada.ispisPodnozja(index - 1));
            KomandeKorisnika.dodajRedakZaPomocnuListu(popisIspisa);
        }
    }
}
