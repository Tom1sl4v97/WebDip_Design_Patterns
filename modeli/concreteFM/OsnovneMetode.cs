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
                ispisGreskiIExit("Neispravna putanja do datoteke: " + nazivDatoteke);
            }

            var regex = new Regex(pattern);
            Match match = regex.Match(lines.First());
            if (match.Success) 
                provjeriDohvacenePodatke(lines);
            else
            {
                ispisGreskiIExit("Neispravan format ili nedostaje informativni redak: " + nazivDatoteke);
            }
        }

        private void ispisGreskiIExit(string opis)
        {
            BrojacGreske.Instance.dodajGreskeUcitavanjaCSVDatoteki(opis);
            BrojacGreske.Instance.dodajNovuGesku("Izlazak iz aplikacije!");
            Console.Write("\x1b[" + KonfiguracijskiPodaci.Instance.brojRedaka + 2 + ";0f");
            Environment.Exit(0);
        }

        public virtual void provjeriDohvacenePodatke(string[] lines)
        {
        }

    }
}
