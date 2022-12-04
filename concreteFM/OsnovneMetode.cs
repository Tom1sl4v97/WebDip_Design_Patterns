using System.Text.RegularExpressions;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;
using ttomiek_zadaca_1.zajednickeMetode;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public abstract class OsnovneMetode : CitanjeDatotekeInterface
    {
        public  void dohvatiPodatkeDatoteke(string nazivDatoteke, string pattern)
        {
            string putanjaDatoteke = NaziviDatoteka.Instance.putanjaPrograma + "\\" + nazivDatoteke;
            string[]? lines = null;

            try
            {
                lines = File.ReadAllLines(putanjaDatoteke);
            }
            catch (Exception)
            {
                BrojacGreske.Instance.IspisGreske("Neispravna putanja do datoteke: " + nazivDatoteke);
                Console.WriteLine("Izlazak iz aplikacije");
                Environment.Exit(0);
            }

            var regex = new Regex(pattern);
            Match match = regex.Match(lines.First());
            if (match.Success) 
                provjeriDohvacenePodatke(lines);
            else
            {
                BrojacGreske.Instance.IspisGreske("Neispravan format ili nedostaje informativni redak: " + nazivDatoteke);
                Console.WriteLine("Izlazak iz aplikacije!");
                Environment.Exit(0);
            }
        }

        public virtual void provjeriDohvacenePodatke(string[] lines)
        {
        }

    }
}
