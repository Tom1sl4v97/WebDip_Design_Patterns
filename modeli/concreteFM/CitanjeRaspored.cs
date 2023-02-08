using System.Text.RegularExpressions;
using ttomiek_zadaca_1.@interface;
using ttomiek_zadaca_1.klase;

namespace ttomiek_zadaca_1.ConcrreteFM
{
    public class CitanjeRaspored : OsnovneMetode
    {
        public override void provjeriDohvacenePodatke(string[] lines)
        {
            foreach (string line in lines)
            {
                if (line == lines.First()) continue;

                string[] podaciRetka = line.Split(';');
                try
                {
                    kreirajObjekt(podaciRetka);
                }
                catch (Exception e)
                {
                    BrojacGreske.Instance.dodajGreskeUcitavanjaCSVDatoteki("Neispravni redak: " + line + " u datoteci: " + NaziviDatoteka.Instance.raspored + " GRESKA: " + e.Message);
                }
            }
        }

        private void kreirajObjekt(string[] podaciRetka)
        {
            Raspored noviRaspored = new();
            string[] pripremaDana;

            noviRaspored.idVez = int.Parse(podaciRetka[0]);
            noviRaspored.idBrod = int.Parse(podaciRetka[1]);
            noviRaspored.vrijemeOd = TimeOnly.Parse(podaciRetka[3]);
            noviRaspored.vrijemeDo = TimeOnly.Parse(podaciRetka[4]);

            string daniUTjednu = podaciRetka[2];
            string regexPattern = "^[0-6]$";

            if (Regex.IsMatch(daniUTjednu, regexPattern))
                pripremaDana = new string[] { daniUTjednu };
            else
                try
                {
                    pripremaDana = daniUTjednu.Split(',');
                }
                catch (Exception)
                {
                    throw new Exception("Neispravan format dana u tjednu!");
                }
            provjeriVelicinuBroda(noviRaspored);
            provjeriPodatkeIUnesi(noviRaspored, pripremaDana);
        }

        private void provjeriVelicinuBroda(Raspored noviRaspored)
        {
            Brod? brod = PodaciDatoteka.Instance.getListaBrodova().Find(x => x.id == noviRaspored.idBrod);
            if (brod == null) throw new Exception("Ne postoji brod sa navedenim ID: " + noviRaspored.idBrod);
            
            Vez? vez = PodaciDatoteka.Instance.getListaVeza().Find(x => x.id == noviRaspored.idVez);
            
            if (vez == null) throw new Exception("Ne postoji vez sa navedenim ID: " + noviRaspored.idVez);

            if (brod.duljina >= vez.maksimalnaDuljina || brod.sirina >= vez.maksimalnaSirina || brod.gaz >= vez.maksimalnaDubina)
                throw new Exception("Navedeni vez s ID: " + noviRaspored.idVez + " je premaleni za brod s ID: " + noviRaspored.idBrod);
        }

        private void provjeriPodatkeIUnesi(Raspored noviRaspored, string[] pripremaDana)
        {
            foreach (string dan in pripremaDana)
            {
                List<Raspored> listaSvihRasporeda = new List<Raspored>(PodaciDatoteka.Instance.getListaRasporeda());
                List<Raspored> listaSlicnih2 = listaSvihRasporeda.FindAll(x => x.idVez == noviRaspored.idVez && x.idBrod == noviRaspored.idBrod && x.daniUTjednu == int.Parse(dan));
                if (listaSlicnih2.Count > 0)
                {
                    foreach (Raspored slicanRaspored in listaSlicnih2)
                    {
                        if ((noviRaspored.vrijemeOd >= slicanRaspored.vrijemeOd && noviRaspored.vrijemeOd <= slicanRaspored.vrijemeDo) ||
                            (noviRaspored.vrijemeDo >= slicanRaspored.vrijemeOd && noviRaspored.vrijemeDo <= slicanRaspored.vrijemeDo))
                            throw new Exception("Pojednini dan/dani se preklapaju u rasporedu!");
                    }
                }
            }

            for (int i = 0; i < pripremaDana.Length; i++)
            {
                Raspored pomocniRaspored = new Raspored();
                pomocniRaspored.idVez = noviRaspored.idVez;
                pomocniRaspored.idBrod = noviRaspored.idBrod;
                int danUTjednu = int.Parse(pripremaDana[i]);
                pomocniRaspored.daniUTjednu = danUTjednu;

                if (noviRaspored.vrijemeOd > noviRaspored.vrijemeDo)
                    throw new Exception("Vrijeme od: " + noviRaspored.vrijemeOd + " je veće od vremena do: " + noviRaspored.vrijemeDo);

                pomocniRaspored.vrijemeOd = noviRaspored.vrijemeOd;
                pomocniRaspored.vrijemeDo = noviRaspored.vrijemeDo;

                PodaciDatoteka.Instance.addNoviRaspored(pomocniRaspored);
            }
        }
    }
}
