using System.Text.RegularExpressions;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;

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
            List<Brod> popisBrodova = new List<Brod>(PodaciDatoteka.Instance.getListaBrodova());
            ispisZaglavlja();
            int index = 1;
            foreach (Brod brod in popisBrodova)
            {
                ispisRetka(brod, index++);
            }
            ZajednickeMetode.ispisPodnozjaStatusa(index - 1);
        }

        private static void ispisZaglavlja()
        {
            if (Tablice.Instance.Z)
            {
                string podaciZaglavlja;
                if (Tablice.Instance.RB)
                    podaciZaglavlja = String.Format("{0,4} | {1,-12} | {2,-20} | {3,-5} | {4,-7} | {5,-6} | {6,-5} | {7,-10} | {8,-12} | {9,-15} | {10,-11}",
                        "RB", "OZNAKA BRODA", "NAZIV", "VRSTA", "DULJINA", "ŠIRINA", "GAZ", "MAX BRZINA", "KAP. PUTNIKA", "KAP. OS. VOZILA", "KAP. TERETA");
                else
                    podaciZaglavlja = String.Format("{0,-12} | {1,-20} | {2,-5} | {3,-7} | {4,-6} | {5,-5} | {6,-10} | {7,-12} | {8,-15} | {9,-11}",
                        "OZNAKA BRODA", "NAZIV", "VRSTA", "DULJINA", "ŠIRINA", "GAZ", "MAX BRZINA", "KAP. PUTNIKA", "KAP. OS. VOZILA", "KAP. TERETA");

                Console.WriteLine(podaciZaglavlja);
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        private static void ispisRetka(Brod brod, int index)
        {
            string podaci;
            if (Tablice.Instance.RB)
                podaci = String.Format("{0,4} | {1,-12} | {2,-20} | {3,-5} | {4,7} | {5,6} | {6,5} | {7,10} | {8,12} | {9,15} | {10,11}",
                index, brod.oznakaBroda, brod.naziv, brod.vrsta, brod.duljina, brod.sirina, brod.gaz, brod.maksimalnaBrzina, brod.kapacitetPutnika, brod.kapacitetOsobnihVozila, brod.kapacitetTereta);
            else
                podaci = String.Format("{0,-12} | {1,-20} | {2,-5} | {3,7} | {4,6} | {5,5} | {6,10} | {7,12} | {8,15} | {9,11}",
                    brod.oznakaBroda, brod.naziv, brod.vrsta, brod.duljina, brod.sirina, brod.gaz, brod.maksimalnaBrzina, brod.kapacitetPutnika, brod.kapacitetOsobnihVozila, brod.kapacitetTereta);
            Console.WriteLine(podaci);
        }
    }
}
