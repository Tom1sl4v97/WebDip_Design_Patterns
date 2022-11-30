using System.Text.RegularExpressions;
using ttomiek_zadaca_1.builder;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeLuka : CitanjeDatotekeInterface
    {
        public void dohvatiPodatkeDatoteke()
        {
            string? putanjaDatoteke = NaziviDatoteka.Instance.luka;
            string[]? lines = null;
            try
            {
                lines = File.ReadAllLines(putanjaDatoteke);
            }
            catch (Exception)
            {
                BrojacGreske.Instance.IspisGreske("Neispravna putanja do csv datoteke LUKE.");
                Console.WriteLine("Izlazak iz aplikacije");
                Environment.Exit(0);
            }

            var regex = new Regex(Luka.PATTERN_INFO_RETKA_CSV);
            Match match = regex.Match(lines.First());
            if (match.Success)
                provjeriDohvacenePodatke(lines);
            else
            {
                BrojacGreske.Instance.IspisGreske("Neispravan format ili nedostaje informativni redak u csv datoteci LUKE.");
                Console.WriteLine("Izlazak iz aplikacije!");
                Environment.Exit(0);
            }
        }

        private void provjeriDohvacenePodatke(string[] lines)
        {
            if (lines.Length == 2)
            {
                string podatakLuke = lines[1];
                string[] podaciRetka = podatakLuke.Split(';');
                try
                {
                    kreirajObjekt(podaciRetka);
                }
                catch (Exception e)
                {
                    BrojacGreske.Instance.IspisGreske("Neispravni redak: " + podatakLuke + " u csv datoteci LUKE, GRESKA: " + e.Message);
                    Console.WriteLine("Izlazak iz aplikacije!");
                    Environment.Exit(0);
                }
            }
            else
            {
                BrojacGreske.Instance.IspisGreske("Postoji više luka, a traži se samo jedna luka za pratiti u csv datoteci LUKE.");
                Console.WriteLine("Izlazak iz aplikacije!");
                Environment.Exit(0);
            }
        }

        private void kreirajObjekt(string[] podaciRetka)
        {
            LukaBuilder novaLukaBuilder = new LukaBuilder();
            Luka novaLuka = novaLukaBuilder.addNaziv(podaciRetka[0])
                .addGpsSirina(double.Parse(podaciRetka[1]))
                .addGpsVisina(double.Parse(podaciRetka[2]))
                .addDubinaLuke(double.Parse(podaciRetka[3]))
                .addUkupniBrojPutnickihVezova(int.Parse(podaciRetka[4]))
                .addUkupniBrojPoslovnihVezova(int.Parse(podaciRetka[5]))
                .addUkupniBrojOstalihVezova(int.Parse(podaciRetka[6]))
                .addVirtualnoVrijeme(DateTime.Parse(podaciRetka[7]))
                .Build();

            PodaciDatoteka.Instance.setNovaLuka(novaLuka);
        }

    }
}
