using System.Text.RegularExpressions;
using ttomiek_zadaca_1.adapter;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeBrod : CitanjeDatotekeInterface
    {
        public void dohvatiPodatkeDatoteke()
        {
            string? nazivDatoteke = NaziviDatoteka.Instance.brod;
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

            var regex = new Regex(Brod.PATTERN_INFO_RETKA_CSV);
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

        private void provjeriDohvacenePodatke(string[] lines)
        {
            List<Brod> popisBrodova = PodaciDatoteka.Instance.getListaBrodova();
            foreach (string line in lines)
            {
                if (line == lines.First()) continue;

                string[] podaciRetka = line.Split(';');
                try
                {
                    int trenutniId = int.Parse(podaciRetka[0]);
                    Brod? brod = popisBrodova.Find(x => x.id == trenutniId);
                    if (brod == null)
                    {
                        kreirajObjekt(podaciRetka);
                        Brod noviBrod = popisBrodova.Last();

                        if (noviBrod.kapacitetPutnika == 0 && noviBrod.kapacitetTereta == 0 && noviBrod.kapacitetOsobnihVozila == 0)
                        {
                            BrojacGreske.Instance.IspisGreske("Navedeni ID broda: " + noviBrod.id + " nema postavljene kapacitete!");
                            continue;
                        }
                    }
                    else
                    {
                        BrojacGreske.Instance.IspisGreske("Navedeni ID broda: " + trenutniId + " već postoji u popisu brodova.");
                    }
                }
                catch (Exception e)
                {
                    BrojacGreske.Instance.IspisGreske("Neispravni redak: " + line + " u datoteci: " + NaziviDatoteka.Instance.brod + " GRESKA: " + e.Message);
                }
            }
        }

        private void kreirajObjekt(string[] podaciRetka)
        {
            Brod noviBrod = new();

            noviBrod.id = int.Parse(podaciRetka[0]);
            noviBrod.oznakaBroda = podaciRetka[1];
            noviBrod.naziv = podaciRetka[2];
            noviBrod.vrsta = podaciRetka[3];
            noviBrod.duljina = float.Parse(podaciRetka[4].Replace(',', '.'));
            noviBrod.sirina = float.Parse(podaciRetka[5].Replace(',', '.'));
            noviBrod.gaz = float.Parse(podaciRetka[6].Replace(',', '.'));
            noviBrod.maksimalnaBrzina = float.Parse(podaciRetka[7].Replace(',', '.'));
            noviBrod.kapacitetPutnika = int.Parse(podaciRetka[8]);
            noviBrod.kapacitetOsobnihVozila = int.Parse(podaciRetka[9]);
            noviBrod.kapacitetTereta = float.Parse(podaciRetka[10].Replace(',', '.'));

            VrstaLuke vrstaLuke = new DohvatiVrstuLuke(noviBrod.vrsta);
            vrstaLuke.dohvatiVrstu();

            PodaciDatoteka.Instance.addNoviBrod(noviBrod);
        }

    }
}
