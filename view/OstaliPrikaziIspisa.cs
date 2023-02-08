using ttomiek_zadaca_1;
using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_2.view
{
    public class OstaliPrikaziIspisa
    {
        public static List<string> ispisZaglavljaBroda()
        {
            List<string> ispisZaglavlja = new();
            if (Tablice.Instance.Z)
            {
                string podaciZaglavlja;
                if (Tablice.Instance.RB)
                    podaciZaglavlja = String.Format("{0,4} | {1,-12} | {2,-20} | {3,-5} | {4,-7} | {5,-6} | {6,-5} | {7,-10} | {8,-12} | {9,-15} | {10,-11}",
                        "RB", "OZNAKA BRODA", "NAZIV", "VRSTA", "DULJINA", "ŠIRINA", "GAZ", "MAX BRZINA", "KAP. PUTNIKA", "KAP. OS. VOZILA", "KAP. TERETA");
                else
                    podaciZaglavlja = String.Format("{0,-12} | {1,-20} | {2,-5} | {3,-7} | {4,-6} | {5,-5} | {6,-10} | {7,-12} | {8,-15} | {9,-11}",
                        "OZNAKA BRODA", "NAZIV", "VRSTA", "DULJINA", "ŠIRINA", "GAZ", "MAX BRZINA", "KAP. PUTNIKA", "KAP. OS. VOZILA", "KAP. TERETA");

                ispisZaglavlja.Add(podaciZaglavlja);
                ispisZaglavlja.Add("---------------------------------------------------------------------------");
            }
            return ispisZaglavlja;
        }

        public static string ispisRetkaBroda(Brod brod, int index)
        {
            string podaci;
            if (Tablice.Instance.RB)
                podaci = String.Format("{0,4} | {1,-12} | {2,-20} | {3,-5} | {4,7} | {5,6} | {6,5} | {7,10} | {8,12} | {9,15} | {10,11}",
                index, brod.oznakaBroda, brod.naziv, brod.vrsta, brod.duljina, brod.sirina, brod.gaz, brod.maksimalnaBrzina, brod.kapacitetPutnika, brod.kapacitetOsobnihVozila, brod.kapacitetTereta);
            else
                podaci = String.Format("{0,-12} | {1,-20} | {2,-5} | {3,7} | {4,6} | {5,5} | {6,10} | {7,12} | {8,15} | {9,11}",
                    brod.oznakaBroda, brod.naziv, brod.vrsta, brod.duljina, brod.sirina, brod.gaz, brod.maksimalnaBrzina, brod.kapacitetPutnika, brod.kapacitetOsobnihVozila, brod.kapacitetTereta);
            return podaci;
        }

        public static List<string> ispisZaglavljaRasporeda()
        {
            List<string> ispisZaglavlja = new();
            if (Tablice.Instance.Z)
            {
                string podaciZaglavlja;
                if (Tablice.Instance.RB)
                    podaciZaglavlja = String.Format("{0,4} | {1,-7} | {2,-7} | {3,-6} | {4,-6} | {5,-20}", "RB", "ID-VEZ", "ID-BROD", "OD", "DO", "DANI U TJEDNU");
                else
                    podaciZaglavlja = String.Format("{0,-7} | {1,-7} | {2,-6} | {3,-6} | {4,-20}", "ID-VEZ", "ID-BROD", "OD", "DO", "DANI U TJEDNU");

                ispisZaglavlja.Add(podaciZaglavlja);
                ispisZaglavlja.Add("---------------------------------------------------------------------------");
            }
            return ispisZaglavlja;
        }

        public static string ispisRetkaRasporeda(Raspored prethodni, string daniUtjednu, int index)
        {
            string podaci;
            if (Tablice.Instance.RB)
                podaci = String.Format("{0,4} | {1,-7} | {2,-7} | {3,-6} | {4,-6} | {5,-20}", index, prethodni.idVez, prethodni.idBrod, prethodni.vrijemeOd, prethodni.vrijemeDo, daniUtjednu);
            else
                podaci = String.Format("{0,-7} | {1,-7} | {2,-6} | {3,-6} | {4,-20}", prethodni.idVez, prethodni.idBrod, prethodni.vrijemeOd, prethodni.vrijemeDo, daniUtjednu);
            return podaci;
        }

        public static List<string> ispisZaglavljaDnevnika()
        {
            List<string> popisZaglavlja = new();
            if (Tablice.Instance.Z)
            {
                string podaciZaglavlja;
                if (Tablice.Instance.RB)
                    podaciZaglavlja = String.Format("{0,4} | {1,-9} | {2,-7} | {3,-20} | {4,-7} | {5,-20}", "RB", "ID-KANALA", "ID-BROD", "VRIJEME", "ODOBREN", "SLOBODAN VEZ?");
                else
                    podaciZaglavlja = String.Format("{0,-9} | {1,-7} | {2,-20} | {3,-7} | {4,-20}", "ID-KANALA", "ID-BROD", "VRIJEME", "ODOBREN", "SLOBODAN VEZ?");

                popisZaglavlja.Add(podaciZaglavlja);
                popisZaglavlja.Add("---------------------------------------------------------------------------");
            }
            return popisZaglavlja;
        }

        public static string ispisRetkaDnevnika(DnevnikRada dnevnikRada, int index)
        {
            string podaci;
            if (Tablice.Instance.RB)
                podaci = String.Format("{0,4} | {1,9} | {2,7} | {3,-20} | {4,-7} | {5,-20}", index, dnevnikRada.idKanala, dnevnikRada.idBrod, dnevnikRada.vrijeme, dnevnikRada.odobrenZahtjev, dnevnikRada.slobodan);
            else
                podaci = String.Format("{0,9} | {1,7} | {2,-20} | {3,-7} | {4,-20}", dnevnikRada.idKanala, dnevnikRada.idBrod, dnevnikRada.vrijeme, dnevnikRada.odobrenZahtjev, dnevnikRada.slobodan);
            return podaci;
        }

        public static List<string> ispisZaglavljaZahtjevaRezervacije()
        {
            List<string> popisZaglavlja = new();
            if (Tablice.Instance.Z)
            {
                string podaciZaglavlja;
                if (Tablice.Instance.RB)
                    podaciZaglavlja = String.Format("{0,4} | {1,-7} | {2,-20} | {3,-20}", "RB", "ID-BROD", "VRIJEME", "TRAJANJE PRIVEZA U H");
                else
                    podaciZaglavlja = String.Format("{0,-7} | {1,-20} | {2,-20}", "ID-BROD", "VRIJEME", "TRAJANJE PRIVEZA U H");

                popisZaglavlja.Add(podaciZaglavlja);
                popisZaglavlja.Add("---------------------------------------------------------------------------");
            }
            return popisZaglavlja;
        }

        public static string ispisRetkaZahtjevaRezervacije(ZahtjevRezervacije zahtjevRezervacija, int index)
        {
            string podaci;
            if (Tablice.Instance.RB)
                podaci = String.Format("{0,4} | {1,7} | {2,-20} | {3,20}", index, zahtjevRezervacija.idBrod, zahtjevRezervacija.datumVrijemeOd, zahtjevRezervacija.trajanjePrivezaUH);
            else
                podaci = String.Format("{0,7} | {1,-20} | {2,20}", zahtjevRezervacija.idBrod, zahtjevRezervacija.datumVrijemeOd, zahtjevRezervacija.trajanjePrivezaUH);
            return podaci;
        }
    }
}
