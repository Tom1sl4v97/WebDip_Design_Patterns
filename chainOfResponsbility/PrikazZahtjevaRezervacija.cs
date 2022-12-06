using System.Text.RegularExpressions;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;

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
            List<ZahtjevRezervacije> popisSvihZahtjevaRezervacija = new List<ZahtjevRezervacije>(PodaciDatoteka.Instance.getListaZahtjevaRezervacije());
            ispisZaglavlja();
            int index = 1;
            foreach(ZahtjevRezervacije zr in popisSvihZahtjevaRezervacija)
            {
                ispisRetka(zr, index++);
            }
            ZajednickeMetode.ispisPodnozjaStatusa(index - 1);
        }
        private static void ispisZaglavlja()
        {
            if (Tablice.Instance.Z)
            {
                string podaciZaglavlja;
                if (Tablice.Instance.RB)
                    podaciZaglavlja = String.Format("{0,4} | {1,-7} | {2,-20} | {3,-20}", "RB", "ID-BROD", "VRIJEME", "TRAJANJE PRIVEZA U H");
                else
                    podaciZaglavlja = String.Format("{0,-7} | {1,-20} | {2,-20}", "ID-BROD", "VRIJEME", "TRAJANJE PRIVEZA U H");

                Console.WriteLine(podaciZaglavlja);
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        private static void ispisRetka(ZahtjevRezervacije zahtjevRezervacija, int index)
        {
            string podaci;
            if (Tablice.Instance.RB)
                podaci = String.Format("{0,4} | {1,7} | {2,-20} | {3,20}", index, zahtjevRezervacija.idBrod, zahtjevRezervacija.datumVrijemeOd, zahtjevRezervacija.trajanjePrivezaUH);
            else
                podaci = String.Format("{0,7} | {1,-20} | {2,20}", zahtjevRezervacija.idBrod, zahtjevRezervacija.datumVrijemeOd, zahtjevRezervacija.trajanjePrivezaUH);
            Console.WriteLine(podaci);
        }
    }
}
