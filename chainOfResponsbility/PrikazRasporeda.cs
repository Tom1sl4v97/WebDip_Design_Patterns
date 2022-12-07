using System.Text.RegularExpressions;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;

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
            List<Raspored> sviRasporedi = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());
            Raspored prethodni = new Raspored();

            ispisZaglavlja();

            if (sviRasporedi.Count == 0)
            {
                BrojacGreske.Instance.IspisGreske("Raspored nije učitani.");
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
                        ispisRetka(prethodni, daniUtjednu, index++);
                        daniUtjednu = r.daniUTjednu.ToString();
                    }
                }
                prethodni = r;
            }

            ZajednickeMetode.ispisPodnozjaStatusa(index - 1);
        }

        private static void ispisZaglavlja()
        {
            if (Tablice.Instance.Z)
            {
                string podaciZaglavlja;
                if (Tablice.Instance.RB)
                    podaciZaglavlja = String.Format("{0,4} | {1,-7} | {2,-7} | {3,-6} | {4,-6} | {5,-20}", "RB", "ID-VEZ", "ID-BROD", "OD", "DO", "DANI U TJEDNU");
                else
                    podaciZaglavlja = String.Format("{0,-7} | {1,-7} | {2,-6} | {3,-6} | {4,-20}", "ID-VEZ", "ID-BROD", "OD", "DO", "DANI U TJEDNU");

                Console.WriteLine(podaciZaglavlja);
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        private static void ispisRetka(Raspored prethodni, string daniUtjednu, int index)
        {
            string podaci;
            if (Tablice.Instance.RB)
                podaci = String.Format("{0,4} | {1,-7} | {2,-7} | {3,-6} | {4,-6} | {5,-20}", index, prethodni.idVez, prethodni.idBrod, prethodni.vrijemeOd, prethodni.vrijemeDo, daniUtjednu);
            else
                podaci = String.Format("{0,-7} | {1,-7} | {2,-6} | {3,-6} | {4,-20}", prethodni.idVez, prethodni.idBrod, prethodni.vrijemeOd, prethodni.vrijemeDo, daniUtjednu);
            Console.WriteLine(podaci);
        }
    }
}
