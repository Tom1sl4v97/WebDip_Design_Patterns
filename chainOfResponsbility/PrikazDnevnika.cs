using System.Text.RegularExpressions;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;

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
            List<DnevnikRada> popisSvihDnevnika = new List<DnevnikRada>(PodaciDatoteka.Instance.getListaDnevnikaRada());
            ispisZaglavlja();
            int index = 1;
            foreach(DnevnikRada dr in popisSvihDnevnika)
            {
                ispisRetka(dr, index++);
            }
            ZajednickeMetode.ispisPodnozjaStatusa(index - 1);
        }
        private static void ispisZaglavlja()
        {
            if (Tablice.Instance.Z)
            {
                string podaciZaglavlja;
                if (Tablice.Instance.RB)
                    podaciZaglavlja = String.Format("{0,4} | {1,-9} | {2,-7} | {3,-20} | {4,-7} | {5,-20}", "RB", "ID-KANALA", "ID-BROD", "VRIJEME", "ODOBREN", "SLOBODAN VEZ?");
                else
                    podaciZaglavlja = String.Format("{0,-9} | {1,-7} | {2,-20} | {3,-7} | {4,-20}", "ID-KANALA", "ID-BROD", "VRIJEME", "ODOBREN", "SLOBODAN VEZ?");

                Console.WriteLine(podaciZaglavlja);
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        private static void ispisRetka(DnevnikRada dnevnikRada, int index)
        {
            string podaci;
            if (Tablice.Instance.RB)
                podaci = String.Format("{0,4} | {1,9} | {2,7} | {3,-20} | {4,-7} | {5,-20}", index, dnevnikRada.idKanala, dnevnikRada.idBrod, dnevnikRada.vrijeme, dnevnikRada.odobrenZahtjev, dnevnikRada.slobodan);
            else
                podaci = String.Format("{0,9} | {1,7} | {2,-20} | {3,-7} | {4,-20}", dnevnikRada.idKanala, dnevnikRada.idBrod, dnevnikRada.vrijeme, dnevnikRada.odobrenZahtjev, dnevnikRada.slobodan);
            Console.WriteLine(podaci);
        }
    }
}
