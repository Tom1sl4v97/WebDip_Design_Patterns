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

        public static List<Vez> ispisTrenutnogStatusa(List<Vez> popisSvihVezova, DateTime trenutnoVrijeme, bool sviVezovi)
        {
            List<Vez> popisZauzetihVezova = new List<Vez>();
            List<Raspored> sviRasporedi = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());
            List<ZahtjevRezervacije> popisZahtjeva = new List<ZahtjevRezervacije>(PodaciDatoteka.Instance.getListaZahtjevaRezervacije());
            
            foreach (Vez vez in popisSvihVezova)
            {
                int danUTjednu = (int)trenutnoVrijeme.DayOfWeek;
                TimeOnly vrijeme = TimeOnly.FromDateTime(trenutnoVrijeme);
                List<Raspored> preklapanjeRasporedom = sviRasporedi.FindAll(x => x.idVez == vez.id && x.daniUTjednu == danUTjednu);
                bool provjera = true;

                foreach (Raspored r in preklapanjeRasporedom)
                {
                    if (r.vrijemeOd <= vrijeme && r.vrijemeDo >= vrijeme)
                    {
                        provjera = false;
                    }
                }

                List<ZahtjevRezervacije> popisOdgovarajucihZahtjeva = popisZahtjeva.FindAll(x => x.idVeza == vez.id);

                foreach (ZahtjevRezervacije zr in popisOdgovarajucihZahtjeva)
                {
                    DateTime pocetnoVrijeme = zr.datumVrijemeOd;
                    DateTime krajnjeVrijeme = zr.datumVrijemeOd.AddHours(zr.trajanjePrivezaUH);
                    if (pocetnoVrijeme <= trenutnoVrijeme && krajnjeVrijeme >= trenutnoVrijeme)
                    {
                        popisZahtjeva.Remove(zr);
                        provjera = false;
                        break;
                    }
                }
                
                if (sviVezovi)
                {
                    if (!provjera) 
                        ispisVeza(vez, "ZAUZETI");
                    else 
                        ispisVeza(vez, "SLOBODAN");
                }else
                {
                    if (!provjera) 
                        popisZauzetihVezova.Add(vez);
                }
                
            }
            return popisZauzetihVezova;
        }

        public static void ispisSvihRasporeda()
        {
            List<Raspored> sviRasporedi = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());

            Raspored prethodni = new Raspored();

            Console.WriteLine(String.Format("{0,-7} | {1,-7} | {2,-6} | {3,-6} | {4,-20}", "ID-VEZ", "ID-BROD", "OD", "DO", "DANI U TJEDNU"));
            Console.WriteLine("---------------------------------------------------------------------------");

            string daniUtjednu = sviRasporedi[0].daniUTjednu.ToString();
            foreach (Raspored r in sviRasporedi)
            {
                if (prethodni.idVez == r.idVez && prethodni.idBrod == r.idBrod && prethodni.vrijemeOd == r.vrijemeOd && prethodni.vrijemeDo == r.vrijemeDo)
                {
                    daniUtjednu += ", " + r.daniUTjednu;
                }
                else
                {
                    Console.WriteLine(String.Format("{0,-7} | {1,-7} | {2,-6} | {3,-6} | {4,-20}", prethodni.idVez, prethodni.idBrod, prethodni.vrijemeOd, prethodni.vrijemeDo, daniUtjednu));
                    daniUtjednu = r.daniUTjednu.ToString();
                }
                prethodni = r;
            }
        }
    }
}
