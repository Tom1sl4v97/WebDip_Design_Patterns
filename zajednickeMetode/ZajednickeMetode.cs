using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.singelton_class;

namespace ttomiek_zadaca_1.zajednickeMetode
{
    public class ZajednickeMetode
    {
        public static void ispisZaglavljaStatusa()
        {
            if (Tablice.Instance.Z)
            {
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.WriteLine("Ispis statusa");
                Console.WriteLine(String.Format("{0,-5} | {1,-10} | {2,-5} | {3,-6} | {4,-7} | {5,-6} | {6,-6} | {7,-10}", "ID", "OZN. VEZA", "VRSTA", "CIJENA", "DULJINA", "ŠIRINA", "DUBINA", "STATUS"));
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public static void ispisVeza(Vez vez, string ostatakPoruke)
        {
            Aplikacija.povecajCounterVezova();
            Console.WriteLine(String.Format("{0,5} | {1,-10} | {2,-5} | {3,6} | {4,7} | {5,6} | {6,6} | {7,-10}", vez.id, vez.oznakaVeza, vez.vrsta, vez.cijenaVezaPoSatu, vez.maksimalnaDuljina, vez.maksimalnaSirina, vez.maksimalnaDubina, ostatakPoruke));
        }

        public static void ispisPodnozjaStatusa(int broj)
        {
            if (Tablice.Instance.P)
            {
                List<Vez> popisVezova = PodaciDatoteka.Instance.getListaVeza();
                Console.WriteLine("---------------------------------------------------------------------------");
                Console.WriteLine(String.Format("{0,-14} {1,59}", "UKUPNO REDAKA:", broj));
                Console.WriteLine("---------------------------------------------------------------------------");
            }
        }

        public static string formDate(DateTime vrijeme)
        {
            return vrijeme.ToString("dd.MM.yyyy. HH:mm:ss");
        }

        public static string dohvatiVrstuLuke(string vrstaBroda)
        {
            switch (vrstaBroda)
            {
                case "TR" or "KA" or "KL" or "KR":
                    return "PU";
                case "RI" or "TE":
                    return "PO";
                case "JA" or "BR" or "RO":
                    return "OS";
                default:
                    throw new Exception("Neispravna oznaka vrste broda: " + vrstaBroda);
            }
        }

        public static void zapisiDnevnikRada(bool odobrenZahtjev, DateTime vrijeme, int idBroda, int idKanala, bool slobodan)
        {
            DnevnikRada noviZapis = new DnevnikRada();

            noviZapis.odobrenZahtjev = odobrenZahtjev;
            noviZapis.vrijeme = vrijeme;
            noviZapis.idBrod = idBroda;
            noviZapis.idKanala = idKanala;
            noviZapis.slobodan = slobodan;

            PodaciDatoteka.Instance.addNoviDnevnikRada(noviZapis);
        }
    }
}
