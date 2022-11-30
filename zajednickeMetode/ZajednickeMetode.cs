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
    }
}
