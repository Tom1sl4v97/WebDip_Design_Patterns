using ttomiek_zadaca_1;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;
using ttomiek_zadaca_3.modeli;

namespace ttomiek_zadaca_2.view
{
    public class IspisRedovaRada
    {
        public const string ANSI_ESC = "\x1b[";

        public static List<string> ispisZaglavlja()
        {
            List<string> popisZaglavlja = new();
            if (Tablice.Instance.Z)
            {
                string izgledInfoRetka;

                if (Tablice.Instance.RB)
                    izgledInfoRetka = String.Format("{0, 4} | {1,-5} | {2,-10} | {3,-5} | {4,-6} | {5,-7} | {6,-6} | {7,-6} | {8,-10}", "RB", "ID", "OZN. VEZA", "VRSTA", "CIJENA", "DULJINA", "ŠIRINA", "DUBINA", "STATUS");
                else
                    izgledInfoRetka = String.Format("{0,-5} | {1,-10} | {2,-5} | {3,-6} | {4,-7} | {5,-6} | {6,-6} | {7,-10}", "ID", "OZN. VEZA", "VRSTA", "CIJENA", "DULJINA", "ŠIRINA", "DUBINA", "STATUS");

                popisZaglavlja.Add("---------------------------------------------------------------------------");
                popisZaglavlja.Add("Ispis statusa");
                popisZaglavlja.Add(izgledInfoRetka);
                popisZaglavlja.Add("---------------------------------------------------------------------------");
            }
            return popisZaglavlja;
        }

        public static string ispisVeza(Vez vez, string ostatakPoruke, int counter)
        {
            string ispis;

            if (Tablice.Instance.RB)
                ispis = String.Format("{0, 4} | {1,5} | {2,-10} | {3,-5} | {4,6} | {5,7} | {6,6} | {7,6} | {8,-10}", counter, vez.id, vez.oznakaVeza, vez.vrsta, vez.cijenaVezaPoSatu, vez.maksimalnaDuljina, vez.maksimalnaSirina, vez.maksimalnaDubina, ostatakPoruke);
            else
                ispis = String.Format("{0,5} | {1,-10} | {2,-5} | {3,6} | {4,7} | {5,6} | {6,6} | {7,-10}", vez.id, vez.oznakaVeza, vez.vrsta, vez.cijenaVezaPoSatu, vez.maksimalnaDuljina, vez.maksimalnaSirina, vez.maksimalnaDubina, ostatakPoruke);

            KomandeKorisnika.povecajCounterVezova();
            return ispis;
        }

        public static List<string> ispisPodnozja(int broj)
        {
            List<string> popisPodnozja = new();
            if (Tablice.Instance.P)
            {
                popisPodnozja.Add("---------------------------------------------------------------------------");
                popisPodnozja.Add(String.Format("{0,-14} {1,59}", "UKUPNO REDAKA:", broj));
                popisPodnozja.Add("---------------------------------------------------------------------------");
            }
            return popisPodnozja;
        }

        public static void ispisRedaka(List<string> popisIspisa, int brojRedovaStareListe)
        {
            IspisPocetnihGreski.ispisGresaka();

            int brojPocetkaRedaRada = KonfiguracijskiPodaci.Instance.GetPocetakRedaRada();
            int brojRedovaRada = ZajednickeMetode.DohvatiBrojRedakaRada() - 2;

            Queue<string> popisRedaka = new();
            int pomocniBrojacRedova = brojPocetkaRedaRada;
            List<string> listaViskovaRedaka = new();
            int brojacListe = 0;

            foreach (string ispis in popisIspisa)
            {
                popisRedaka.Enqueue(ispis);
                brojacListe++;

                if (popisRedaka.Count <= brojRedovaRada)
                {
                    if (brojRedovaStareListe <= brojRedovaRada)
                    {
                        Thread.Sleep(ZajednickeMetode.VRIJEME_CEKANJA_MS);
                        prikazi(pomocniBrojacRedova++, 0, 32, ispis);
                    }
                }
                else
                {
                    //Micanje redova prema gore
                    string visakRetka = popisRedaka.Dequeue();
                    listaViskovaRedaka.Add(visakRetka);

                    if (brojacListe < brojRedovaStareListe)
                        continue;

                    pomocniBrojacRedova = brojPocetkaRedaRada;
                    
                    //Ispis ispravnog ekrana
                    foreach (string ispravniRedovi in popisRedaka)
                    {
                        Console.Write(ANSI_ESC + pomocniBrojacRedova + ";" + 0 + "f");
                        Console.Write(ANSI_ESC + "2K");
                        prikazi(pomocniBrojacRedova++, 0, 32, ispravniRedovi);
                    }

                    IspisPocetnihGreski.ispisGresaka();

                    Thread.Sleep(ZajednickeMetode.VRIJEME_CEKANJA_MS);
                }
            }

            ispisPosljednjeLinije(listaViskovaRedaka, popisRedaka, brojPocetkaRedaRada);
        }

        private static void ispisPosljednjeLinije(List<string> listaViskovaRedaka, Queue<string> popisRedaka, int pomocniBrojacRedova)
        {
            Console.Clear();
            Console.Write(ANSI_ESC + "0m");
            Console.WriteLine("Povijest ispisa");
            //Ispis viskova, iznad ekrana
            foreach (string viskovi in listaViskovaRedaka)
                Console.WriteLine(viskovi);

            Console.Write(ANSI_ESC + "33m");
            Console.WriteLine("POČETAK EKRANA, TE OVA LINIJA SE NE RAČUNA ZA UKUPNI BROJ REDAKA. OVO SLUZI SAMO ZA LAKŠE UOČAVANJE CIJELOG EKRANA--------------------------------------------------------");
            
            prikazi(0, 0, 0, "");
            Console.Write(ANSI_ESC + "E");
            Console.Write(ANSI_ESC + "2J");
            foreach (string ispravniRedovi in popisRedaka)
            {
                Console.Write(ANSI_ESC + pomocniBrojacRedova + ";" + 0 + "f");
                Console.Write(ANSI_ESC + "2K");
                prikazi(pomocniBrojacRedova++, 0, 32, ispravniRedovi);
            }
            IspisPocetnihGreski.ispisGresaka();

            IspisPocetnihGreski.ispisPocetneKomande();
        }

        static void postavi(int x, int y)
        {
            Console.Write(ANSI_ESC + x + ";" + y + "f");
        }

        static void prikazi(int x, int y, int boja, String tekst)
        {
            postavi(x, y);
            Console.Write(ANSI_ESC + boja + "m");
            Console.Write(tekst);
        }
    }
}
