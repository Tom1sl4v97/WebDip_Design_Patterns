using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.zajednickeMetode
{
    public class ZajednickeMetode
    {
        public static void ispisVeza(Vez vez, string ostatakPoruke)
        {
            Console.WriteLine(vez.id + "\t" + vez.oznakaVeza + "\t" + vez.vrsta + "\t" + vez.cijenaVezaPoSatu + "\t" + vez.maksimalnaDuljina + "\t" + vez.maksimalnaSirina + "\t" + vez.maksimalnaDubina + "\t" + ostatakPoruke);
        }

        public static string formDate(DateTime vrijeme)
        {
            return vrijeme.ToString("dd.MM.yyyy. HH:mm:ss");
        }

        public enum daniUTjednu
        {
            NEDJELJA,
            PONEDJELJAK,
            UTORAK,
            SRIJEDA,
            CETVRTAK,
            PETAK,
            SUBOTA
        }
    }
}
