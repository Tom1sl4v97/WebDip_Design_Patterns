using System.Text.RegularExpressions;
using ttomiek_zadaca_1.builder;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeLuka : OsnovneMetode
    {
        public override void provjeriDohvacenePodatke(string[] lines)
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
                    BrojacGreske.Instance.dodajGreskeUcitavanjaCSVDatoteki("Neispravni redak: " + podatakLuke + " u datoteci: " + NaziviDatoteka.Instance.luka + " GRESKA: " + e.Message);
                    Console.WriteLine("Izlazak iz aplikacije!");
                    Environment.Exit(0);
                }
            }
            else
            {
                BrojacGreske.Instance.dodajGreskeUcitavanjaCSVDatoteki("Postoji više luka, a traži se samo jedna luka za pratiti u datoteci: " + NaziviDatoteka.Instance.luka);
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
